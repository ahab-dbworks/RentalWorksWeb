using AutoMapper;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.ConfigSection;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogic
    {
        [JsonIgnore]
        public List<FwDataReadWriteRecord> dataRecords = new List<FwDataReadWriteRecord>();

        [JsonIgnore]
        public FwDataRecord dataLoader = null;

        //------------------------------------------------------------------------------------
        public void SetDbConfig(DatabaseConfig dbConfig)
        {
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                rec.SetDbConfig(dbConfig);
            }
            if (dataLoader != null)
            {
                dataLoader.SetDbConfig(dbConfig);
            }
        }
        //------------------------------------------------------------------------------------
        public FwJsonDataTable Browse(BrowseRequestDto request)
        {
            FwJsonDataTable browse = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    browse = dataRecords[0].Browse(request);
                }
            }
            else
            {
                browse = dataLoader.Browse(request);
            }
            return browse;

        }
        //------------------------------------------------------------------------------------
        public virtual IEnumerable<T> Select<T>(BrowseRequestDto request)
        {
            IEnumerable<T> records = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    records = dataRecords[0].Select<T>(request);
                }
            }
            else
            {
                records = dataLoader.Select<T>(request);
            }
            return records;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Load<T>(string[] primaryKeyValues) 
        {
            bool blLoaded = false;
            bool recLoaded = false;
            if (dataLoader == null)
            {
                int r = 0;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    recLoaded = rec.Load<T>(primaryKeyValues);
                    if (r == 0)
                    {
                        blLoaded = recLoaded;
                    }
                    r++;
                }
            }
            else
            {
                blLoaded = dataLoader.Load<T>(primaryKeyValues);
                Mapper.Map(dataLoader, this);
            }
            return blLoaded;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Load<T>()
        {
            int pkIndex = 0;
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();
            string[] ids = new string[pkProperties.Count];
            foreach (PropertyInfo pkProperty in pkProperties)
            {
                ids[pkIndex] = pkProperty.GetValue(this).ToString();
                pkIndex++;
            }
            return Load<T>(ids);
        }
        //------------------------------------------------------------------------------------
        protected virtual List<PropertyInfo> GetPrimaryKeyProperties()
        {
            List<PropertyInfo> primaryKeyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwBusinessLogicFieldAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwBusinessLogicFieldAttribute))
                        {
                            FwBusinessLogicFieldAttribute businessLogicFieldAttribute = (FwBusinessLogicFieldAttribute)attribute;
                            if (businessLogicFieldAttribute.IsPrimaryKey)
                            {
                                primaryKeyProperties.Add(property);
                            }
                        }
                    }
                }
            }
            return primaryKeyProperties;
        }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public virtual bool AllPrimaryKeysHaveValues
        {
            get
            {
                List<PropertyInfo> primaryKeys = GetPrimaryKeyProperties();
                bool hasPrimaryKeysSet = true;
                foreach (PropertyInfo property in primaryKeys)
                {
                    object propertyValue = property.GetValue(this);
                    if (propertyValue is string)
                    {
                        hasPrimaryKeysSet &= (propertyValue as string).Length > 0;
                    }
                    else
                    {
                        throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic.AllPrimaryKeysHaveValues]");
                    }
                }
                return hasPrimaryKeysSet;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void SetPrimaryKeys(string[] ids)
        {
            int pkIndex = 0;
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();
            foreach (PropertyInfo pkProperty in pkProperties)
            {
                pkProperty.SetValue(this, ids[pkIndex]);
                pkIndex++;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual int Save()
        {
            int rowsAffected = 0;
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                rowsAffected = rowsAffected + rec.Save();
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Delete()
        {
            bool success = true;
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                success = ((success) && rec.Delete());
            }
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}

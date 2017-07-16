﻿using AutoMapper;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.ConfigSection;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogic
    {
        [JsonIgnore]
        protected List<FwDataReadWriteRecord> dataRecords = new List<FwDataReadWriteRecord>();

        [JsonIgnore]
        protected FwDataRecord dataLoader = null;

        public FwBusinessLogicCustomValues _Custom = null;

        //------------------------------------------------------------------------------------
        public FwBusinessLogic()
        {
            _Custom = new FwBusinessLogicCustomValues(this.GetType().Name.Replace("Logic", ""));
        }
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
            _Custom.SetDbConfig(dbConfig);
        }
    //------------------------------------------------------------------------------------
    public async Task<FwJsonDataTable> BrowseAsync(BrowseRequestDto request)
        {
            FwJsonDataTable browse = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    browse = await dataRecords[0].BrowseAsync(request);
                }
            }
            else
            {
                browse = await dataLoader.BrowseAsync(request);
            }
            return browse;

        }
        //------------------------------------------------------------------------------------
        public virtual async Task<IEnumerable<T>> SelectAsync<T>(BrowseRequestDto request)
        {
            IEnumerable<T> records = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    records = await dataRecords[0].SelectAsync<T>(request);
                }
            }
            else
            {
                records = await dataLoader.SelectAsync<T>(request);
            }
            return records;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(string[] primaryKeyValues) 
        {
            bool blLoaded = false;
            bool recLoaded = false;
            if (dataLoader == null)
            {
                int r = 0;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    recLoaded = await rec.LoadAsync<T>(primaryKeyValues);
                    if (r == 0)
                    {
                        blLoaded = recLoaded;
                    }
                    r++;
                }
            }
            else
            {
                blLoaded = await dataLoader.LoadAsync<T>(primaryKeyValues);
                Mapper.Map(dataLoader, this);
            }
            if ((blLoaded) && (_Custom != null))
            {
                await _Custom.LoadAsync(primaryKeyValues);
            }

            return blLoaded;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>()
        {
            return await LoadAsync<T>(GetPrimaryKeys());
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
        public virtual string[] GetPrimaryKeys()
        {
            int pkIndex = 0;
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();
            string[] ids = new string[pkProperties.Count];
            foreach (PropertyInfo pkProperty in pkProperties)
            {
                ids[pkIndex] = pkProperty.GetValue(this).ToString();
                pkIndex++;
            }
            return ids;
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
        public virtual async Task<int> SaveAsync()
        {
            int rowsAffected = 0;
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                rowsAffected += await rec.SaveAsync();
            }
            if (_Custom != null)
            {
                await _Custom.SaveAsync(GetPrimaryKeys());
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> DeleteAsync()
        {
            bool success = true;
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                success &= await rec.DeleteAsync();
            }
            return success;
        }
        //------------------------------------------------------------------------------------
        protected virtual List<PropertyInfo> GetTitleProperties()
        {
            List<PropertyInfo> titleProperties = new List<PropertyInfo>();
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
                            if (businessLogicFieldAttribute.IsTitle)
                            {
                                titleProperties.Add(property);
                            }
                        }
                    }
                }
            }
            return titleProperties;
        }
        //------------------------------------------------------------------------------------
        public virtual string _Title
        {
            get
            {
                List<PropertyInfo> titles = GetTitleProperties();
                string title = "";
                foreach (PropertyInfo property in titles)
                {
                    object propertyValue = property.GetValue(this);
                    if (propertyValue != null)
                    {
                        if (!title.Equals(string.Empty))
                        {
                            title = title + " ";
                        }
                        if (propertyValue is string)
                        {
                            title = title + (propertyValue as string).TrimEnd(); ;
                        }
                        else
                        {
                            throw new Exception("Property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic._Title]");
                        }
                    }
                }
                return title;
            }
        }
        //------------------------------------------------------------------------------------
    }
}

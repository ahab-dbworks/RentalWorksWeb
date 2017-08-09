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

        public FwCustomValues _Custom = new FwCustomValues();  //todo: don't initialize here.  Instead, only initialize when custom fields exist for this module.  load custom fields in a static class.
        //------------------------------------------------------------------------------------
        public FwBusinessLogic() { }
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

            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));

            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    browse = await dataRecords[0].BrowseAsync(request, _Custom.CustomFields);
                }
            }
            else
            {
                browse = await dataLoader.BrowseAsync(request, _Custom.CustomFields);
            }
            return browse;

        }
        //------------------------------------------------------------------------------------
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequestDto request)
        {
            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));

            List<T> records = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    MethodInfo method = dataRecords[0].GetType().GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(dataRecords[0].GetType());
                    BrowseRequestDto browseRequest = request;
                    FwCustomFields customFields = _Custom.CustomFields;
                    dynamic result = generic.Invoke(dataRecords[0], new object[] { browseRequest, customFields });
                    dynamic dataRecordsResults = await result;
                    records = new List<T>(dataRecordsResults.Count);
                    Mapper.Map(dataRecordsResults, records);
                }
            }
            else
            {
                MethodInfo method = dataLoader.GetType().GetMethod("SelectAsync");
                MethodInfo generic = method.MakeGenericMethod(dataLoader.GetType());
                BrowseRequestDto browseRequest = request;
                FwCustomFields customFields = _Custom.CustomFields;
                dynamic result = generic.Invoke(dataLoader, new object[] { browseRequest, customFields });
                dynamic dataLoaderResults = await result;
                records = new List<T>(dataLoaderResults.Count);
                Mapper.Map(dataLoaderResults, records);
            }
            return records;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(string[] primaryKeyValues) 
        {
            bool blLoaded = false;
            bool recLoaded = false;

            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));

            if (dataLoader == null)
            {
                for (int i = 0; i < dataRecords.Count; i++)
                {
                    FwDataReadWriteRecord rec = dataRecords[i];
                    rec = await rec.GetAsync<T>(primaryKeyValues, _Custom.CustomFields);
                    dataRecords[i] = rec;
                    Mapper.Map(rec, this);
                    recLoaded = (rec != null);
                    if (i == 0)
                    {
                        blLoaded = recLoaded;
                    }
                    i++;
                }
            }
            else
            {
                dataLoader = await dataLoader.GetAsync<T>(primaryKeyValues, _Custom.CustomFields);
                blLoaded = (dataLoader != null);
                Mapper.Map(dataLoader, this);
            }
            //if (blLoaded) 
            //{
            //    await _Custom.LoadAsync(primaryKeyValues);
            //}

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
        public virtual void BeforeSave() { }
        //------------------------------------------------------------------------------------
        public virtual void AfterSave() { }
        //------------------------------------------------------------------------------------
        public virtual async Task<int> SaveAsync()
        {
            BeforeSave();
            int rowsAffected = 0;
            foreach (FwDataReadWriteRecord rec in dataRecords)
            {
                rowsAffected += await rec.SaveAsync();
            }
            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));
            await _Custom.SaveAsync(GetPrimaryKeys());
            AfterSave();
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
        protected virtual List<PropertyInfo> GetRecordTitleProperties()
        {
            List<PropertyInfo> recordTitleProperties = new List<PropertyInfo>();
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
                            if (businessLogicFieldAttribute.IsRecordTitle)
                            {
                                recordTitleProperties.Add(property);
                            }
                        }
                    }
                }
            }
            return recordTitleProperties;
        }
        //------------------------------------------------------------------------------------
        public virtual string RecordTitle
        {
            get
            {
                List<PropertyInfo> recordTitles = GetRecordTitleProperties();
                string title = "";
                foreach (PropertyInfo property in recordTitles)
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
                            throw new Exception("Property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic.RecordTitle]");
                        }
                    }
                }
                return title;
            }
        }
        //------------------------------------------------------------------------------------
    }
}

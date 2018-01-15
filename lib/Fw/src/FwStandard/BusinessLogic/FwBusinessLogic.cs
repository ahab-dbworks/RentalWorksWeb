using AutoMapper;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.Modules.Administrator.DuplicateRule;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FwStandard.BusinessLogic
{
    public enum TDataRecordSaveMode { smInsert, smUpdate };

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
        public void SetDbConfig(SqlServerConfig dbConfig)
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
        public async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request)
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
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequest request)
        {
            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));

            List<T> records = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    MethodInfo method = dataRecords[0].GetType().GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(dataRecords[0].GetType());
                    BrowseRequest browseRequest = request;
                    FwCustomFields customFields = _Custom.CustomFields;
                    dynamic result = generic.Invoke(dataRecords[0], new object[] { browseRequest, customFields });
                    dynamic dataRecordsResults = await result;
                    records = new List<T>(dataRecordsResults.Count);
                    Mapper.Map((object)dataRecordsResults, records, opts =>
                    {
                        opts.ConfigureMap(MemberList.None);
                    });
                }
            }
            else
            {
                MethodInfo method = dataLoader.GetType().GetMethod("SelectAsync");
                MethodInfo generic = method.MakeGenericMethod(dataLoader.GetType());
                BrowseRequest browseRequest = request;
                FwCustomFields customFields = _Custom.CustomFields;
                dynamic result = generic.Invoke(dataLoader, new object[] { browseRequest, customFields });
                dynamic dataLoaderResults = await result;
                records = new List<T>(dataLoaderResults.Count);
                //Mapper.Map(dataLoaderResults, records);
                Mapper.Map((object)dataLoaderResults, records, opts =>
                {
                    opts.ConfigureMap(MemberList.None);
                });
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
                    Mapper.Map(rec, this, opts =>
                    {
                        opts.ConfigureMap(MemberList.None);
                    });
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
                Mapper.Map(dataLoader, this, opts =>
                {
                    opts.ConfigureMap(MemberList.None);
                });
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
                    bool optionalPrimaryKey = false;

                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwBusinessLogicFieldAttribute))
                        {
                            FwBusinessLogicFieldAttribute businessLogicFieldAttribute = (FwBusinessLogicFieldAttribute)attribute;
                            if (businessLogicFieldAttribute.IsPrimaryKeyOptional)
                            {
                                optionalPrimaryKey = true;
                            }
                        }
                    }

                    if (!optionalPrimaryKey)
                    {
                        object propertyValue = property.GetValue(this);
                        if (propertyValue is string)
                        {
                            hasPrimaryKeysSet &= (propertyValue as string).Length > 0;
                        }
                        else if (propertyValue is Int32)
                        {
                            hasPrimaryKeysSet &= (((Int32)propertyValue) != 0);
                        }
                        else
                        {
                            throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic.AllPrimaryKeysHaveValues]");
                        }
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
                //pkProperty.SetValue(this, ids[pkIndex]);

                object propertyValue = pkProperty.GetValue(this);

                if (propertyValue is Int32)
                {
                    pkProperty.SetValue(this, Convert.ToInt32(ids[pkIndex]));
                }
                else
                {
                    pkProperty.SetValue(this, ids[pkIndex]);
                }

                pkIndex++;
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            string moduleName = this.GetType().Name;
            string module = moduleName.Substring(0, moduleName.Length - 5);

            BrowseRequest browseRequest = new BrowseRequest();
            browseRequest.module = "DuplicateRules";
            browseRequest.searchfieldoperators = new String[] { "=" };
            browseRequest.searchfields = new String[] { "ModuleName" };
            browseRequest.searchfieldvalues = new String[] { module };

            DuplicateRuleLogic l = new DuplicateRuleLogic();
            l.SetDbConfig(dataRecords[0].GetDbConfig());
            FwJsonDataTable rules = l.BrowseAsync(browseRequest).Result;

            if (rules.Rows.Count > 0)
            {
                foreach (var rule in rules.Rows)
                {
                    string fields = rule[4].ToString();
                    string[] field = fields.Split(',').ToArray();

                    BrowseRequest browseRequest2 = new BrowseRequest();
                    browseRequest2.module = module;

                    List<string> searchOperators = new List<string>();

                    for (int i = 0; i < field.Count(); i++)
                    {
                        searchOperators.Add("=");
                    }

                    browseRequest2.searchfieldoperators = searchOperators.ToArray();
                    browseRequest2.searchfields = field;

                    Type type = this.GetType();
                    PropertyInfo[] propertyInfo;
                    propertyInfo = type.GetProperties();

                    int dupes = 0;
                    List<string> searchFieldVals = new List<string>();

                    foreach (PropertyInfo property in propertyInfo)
                    {
                        if (fields.IndexOf(property.Name) != -1)
                        {
                            var value = this.GetType().GetProperty(property.Name).GetValue(this, null);

                            if (value != null)
                            {
                             searchFieldVals.Add(value.ToString());
                            } else
                            {
                                searchFieldVals.Add("");
                            }
                        }

                    }
                    browseRequest2.searchfieldvalues = searchFieldVals.ToArray();
                    FwBusinessLogic l2 = (FwBusinessLogic)Activator.CreateInstance(type);
                    l2.SetDbConfig(dataRecords[0].GetDbConfig());
                    FwJsonDataTable dt = l2.BrowseAsync(browseRequest2).Result;

                    if (dt.Rows.Count > 0)
                    {
                        dupes++;
                    }

                    if (dupes == field.Count())
                    {
                        throw new Exception("A record of this type already exists. " + "(" + rule[2] + ")");
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual bool ValidateBusinessLogic(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            validateMsg = "";
            if (isValid)
            {
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    isValid = rec.ValidateDataRecord(saveMode, ref validateMsg);
                    if (!isValid)
                    {
                        break;
                    }
                }
            }
            if (isValid)
            {
                //check for duplicate Business Logic here
            }
            if (isValid)
            {
                isValid = Validate(saveMode, ref validateMsg);
            }
            return isValid;
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
                            title = title + (propertyValue as string).TrimEnd(); 
                        }
                        else if (propertyValue is int)
                        {
                            title = title + ((int)propertyValue).ToString().TrimEnd(); 
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

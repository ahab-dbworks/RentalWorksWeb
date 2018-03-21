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

    public class BeforeSaveEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public bool PerformSave { get; set; } = true;
    }

    public class AfterSaveEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public bool SavePerformed { get; set; } = true;  //jh - I'm not sure this is necessary.  considering removing
    }

    public class BeforeValidateEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
    }

    public class BeforeDeleteEventArgs : EventArgs
    {
        public bool PerformDelete { get; set; } = true;
    }

    public class AfterDeleteEventArgs : EventArgs { }

    public class FwBusinessLogic
    {
        private FwApplicationConfig _appConfig = null;
        private FwUserSession _userSession = null;
        [JsonIgnore]
        public FwApplicationConfig AppConfig
        {
            get { return _appConfig; }
            set
            {
                _appConfig = value;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    rec.AppConfig = value;
                }
                if (dataLoader != null)
                {
                    dataLoader.AppConfig = value;
                }
                if (browseLoader != null)
                {
                    browseLoader.AppConfig = value;
                }
                _Custom.AppConfig = value;
            }
        }

        [JsonIgnore]
        //public FwUserSession UserSession = null;
        public FwUserSession UserSession
        {
            get { return _userSession; }
            set
            {
                _userSession = value;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    rec.UserSession = value;
                }
                if (dataLoader != null)
                {
                    dataLoader.UserSession = value;
                }
                if (browseLoader != null)
                {
                    browseLoader.UserSession = value;
                }
                _Custom.UserSession = value;
            }
        }

        [JsonIgnore]
        protected List<FwDataReadWriteRecord> dataRecords = new List<FwDataReadWriteRecord>();

        [JsonIgnore]
        protected FwDataRecord dataLoader = null;

        [JsonIgnore]
        protected FwDataRecord browseLoader = null;

        [JsonIgnore]
        protected static FwJsonDataTable duplicateRules = null;

        public FwCustomValues _Custom = new FwCustomValues();  //todo: don't initialize here.  Instead, only initialize when custom fields exist for this module.  load custom fields in a static class.

        public event EventHandler<BeforeSaveEventArgs> BeforeSave;
        public event EventHandler<AfterSaveEventArgs> AfterSave;
        public event EventHandler<BeforeValidateEventArgs> BeforeValidate;
        public event EventHandler<BeforeDeleteEventArgs> BeforeDelete;
        public event EventHandler<AfterDeleteEventArgs> AfterDelete;

        public delegate void BeforeSaveEventHandler(BeforeSaveEventArgs e);
        public delegate void AfterSaveEventHandler(AfterSaveEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateEventArgs e);
        public delegate void BeforeDeleteEventHandler(BeforeDeleteEventArgs e);
        public delegate void AfterDeleteEventHandler(AfterDeleteEventArgs e);

        protected virtual void OnBeforeSave(BeforeSaveEventArgs e)
        {
            EventHandler<BeforeSaveEventArgs> handler = BeforeSave;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterSave(AfterSaveEventArgs e)
        {
            EventHandler<AfterSaveEventArgs> handler = AfterSave;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnBeforeValidate(BeforeValidateEventArgs e)
        {
            EventHandler<BeforeValidateEventArgs> handler = BeforeValidate;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnBeforeDelete(BeforeDeleteEventArgs e)
        {
            EventHandler<BeforeDeleteEventArgs> handler = BeforeDelete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterDelete(AfterDeleteEventArgs e)
        {
            EventHandler<AfterDeleteEventArgs> handler = AfterDelete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        //------------------------------------------------------------------------------------
        public FwBusinessLogic() { }
        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request)
        {
            FwJsonDataTable browse = null;

            await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));

            if (browseLoader != null)
            {
                browseLoader.UserSession = this.UserSession;
                browse = await browseLoader.BrowseAsync(request, _Custom.CustomFields);
            }
            else if (dataLoader != null)
            {
                dataLoader.UserSession = this.UserSession;
                browse = await dataLoader.BrowseAsync(request, _Custom.CustomFields);
            }
            else
            {
                if (dataRecords.Count > 0)
                {
                    dataRecords[0].UserSession = this.UserSession;
                    browse = await dataRecords[0].BrowseAsync(request, _Custom.CustomFields);
                }
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
        public virtual async Task<bool> LoadAsync<T>(object[] primaryKeyValues) 
        {
            bool blLoaded = false;
            bool recLoaded = false;
            FwApplicationConfig tmpAppConfig = AppConfig;
            FwUserSession tmpUserSession = UserSession;


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
                this.AppConfig = tmpAppConfig;
                this.UserSession = tmpUserSession;
            }
            else
            {
                dataLoader = await dataLoader.GetAsync<T>(primaryKeyValues, _Custom.CustomFields);
                blLoaded = (dataLoader != null);
                Mapper.Map(dataLoader, this, opts =>
                {
                    opts.ConfigureMap(MemberList.None);
                });
                this.AppConfig = tmpAppConfig;
                this.UserSession = tmpUserSession;

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
                            if ((businessLogicFieldAttribute.IsPrimaryKey) || (businessLogicFieldAttribute.IsPrimaryKeyOptional))
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
        public virtual object[] GetPrimaryKeys()
        {
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();
            object[] ids                    = new object[pkProperties.Count];

            for (int i = 0; i < pkProperties.Count; i++)
            {
                PropertyInfo pkProperty = pkProperties[i];
                ids[i]                  = pkProperty.GetValue(this);
            }

            return ids;
        }
        //------------------------------------------------------------------------------------
        public virtual void SetPrimaryKeys(object[] ids)
        {
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();

            for (int i = 0; i < pkProperties.Count; i++)
            {
                PropertyInfo pkProperty = pkProperties[i];
                Type propertyType       = pkProperty.PropertyType;

                if ((propertyType == typeof(int?)) || (propertyType == typeof(Int32)))
                {
                    pkProperty.SetValue(this, Convert.ToInt32(ids[i]));
                }
                else if (propertyType == typeof(string))
                {
                    pkProperty.SetValue(this, ids[i]);
                }
                else
                {
                    throw new Exception("Primary key property type not implemented for " +  propertyType.ToString() + ". [FwBusinessLogic.SetPrimaryKeys]");
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual bool refreshDuplicateRules()
        {
            bool rulesLoaded = false;

            BrowseRequest browseRequest = new BrowseRequest();
            browseRequest.module = "DuplicateRules";
            DuplicateRuleLogic l = new DuplicateRuleLogic();
            if (dataRecords.Count > 0)
            {
                l.AppConfig = dataRecords[0].AppConfig;
                duplicateRules = l.BrowseAsync(browseRequest).Result;
                rulesLoaded = true;
            }

            return rulesLoaded;
        }
        //------------------------------------------------------------------------------------
        protected virtual bool CheckDuplicates(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            if (duplicateRules == null)
            {
                refreshDuplicateRules();
            }

            if (duplicateRules != null)
            {
                object[] ids = GetPrimaryKeys();
                string moduleName = this.GetType().Name;
                string module = moduleName.Substring(0, moduleName.Length - 5);

                var duplicateRows = duplicateRules.Rows;
                List<object> rulesList = new List<object>();

                foreach (var row in duplicateRows)
                {
                    if ((String)row[1] == module)
                    {
                        rulesList.Add(row);
                    }
                }

                if (rulesList.Count > 0)
                {
                    Type type = this.GetType();
                    PropertyInfo[] propertyInfo;
                    propertyInfo = type.GetProperties();
                    FwBusinessLogic l2 = (FwBusinessLogic)Activator.CreateInstance(type);
                    l2.AppConfig = dataRecords[0].AppConfig;

                    foreach (List<object> rule in rulesList)
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

                        List<string> searchFieldVals = new List<string>();


                        for (int i = 0; i < field.Count(); i++)
                        {
                            string fieldName = field[i];
                            foreach (PropertyInfo property in propertyInfo)
                            {
                                if (property.Name.Equals(fieldName))
                                {
                                    var value = this.GetType().GetProperty(property.Name).GetValue(this, null);
                                    if (value != null)
                                    {
                                        searchFieldVals.Add(value.ToString());
                                    }
                                    else
                                    {
                                        if (saveMode == TDataRecordSaveMode.smUpdate)
                                        {
                                            var recordFound = l2.LoadAsync<Type>(ids).Result;
                                            var databaseValue = l2.GetType().GetProperty(property.Name).GetValue(l2, null);
                                            searchFieldVals.Add(databaseValue.ToString());
                                        }
                                        else
                                        {
                                            searchFieldVals.Add("");
                                        }
                                    }

                                    if (searchOperators.Count == searchFieldVals.Count)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        browseRequest2.searchfieldvalues = searchFieldVals.ToArray();
                        FwBusinessLogic l3 = (FwBusinessLogic)Activator.CreateInstance(type);
                        l3.AppConfig = dataRecords[0].AppConfig;
                        FwJsonDataTable dt = l3.BrowseAsync(browseRequest2).Result;

                        bool isDuplicate = false;
                        for (int r = 0; r <= dt.Rows.Count - 1; r++)
                        {
                            isDuplicate = true;
                            if (saveMode == TDataRecordSaveMode.smUpdate)
                            {
                                var dtToArray = dt.Rows[r].Select(i => i.ToString()).ToArray();
                                bool pkFound = true;
                                foreach (object id in ids)
                                {
                                    int indexOfId = Array.IndexOf(dtToArray, id);
                                    pkFound = (indexOfId >= 0);
                                    if (!pkFound)
                                    {
                                        break;
                                    }
                                }
                                isDuplicate = (!pkFound);
                            }
                            if (isDuplicate)
                            {
                                break;
                            }
                        }
                        if (isDuplicate)
                        {
                            isValid = false;
                            validateMsg = "A record of this type already exists. " + "(" + rule[2] + ")";
                        }
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        protected virtual bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            //override this method on a derived class to implement custom validation logic
            bool isValid = true;
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
                isValid = CheckDuplicates(saveMode, ref validateMsg);
            }
            if (isValid)
            {
                isValid = Validate(saveMode, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<int> SaveAsync()
        {
            int rowsAffected = 0;
            TDataRecordSaveMode saveMode = (AllPrimaryKeysHaveValues ? TDataRecordSaveMode.smUpdate : TDataRecordSaveMode.smInsert);

            BeforeSaveEventArgs beforeSaveArgs = new BeforeSaveEventArgs();
            AfterSaveEventArgs afterSaveArgs = new AfterSaveEventArgs();
            beforeSaveArgs.SaveMode = saveMode;
            afterSaveArgs.SaveMode = saveMode;
            if (BeforeSave != null)
            {
                BeforeSave(this, beforeSaveArgs);
            }
            if (beforeSaveArgs.PerformSave)
            {
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    rowsAffected += await rec.SaveAsync();
                }
                await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));
                await _Custom.SaveAsync(GetPrimaryKeys());
                afterSaveArgs.SavePerformed = (rowsAffected > 0);
                if (AfterSave != null)
                {
                    AfterSave(this, afterSaveArgs);
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> DeleteAsync()
        {
            bool success = true;
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();
            if (BeforeDelete != null)
            {
                BeforeDelete(this, beforeDeleteArgs);
            }
            if (beforeDeleteArgs.PerformDelete)
            {
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    success &= await rec.DeleteAsync();
                }
                if (AfterDelete != null)
                {
                    AfterDelete(this, afterDeleteArgs);
                }
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
        public void LoadUserSession()
        {
            if (dataLoader != null)
            {
                dataLoader.UserSession = this.UserSession;
            }
            if (browseLoader != null)
            {
                browseLoader.UserSession = this.UserSession;
            }
            foreach (FwDataReadWriteRecord dataRecord in dataRecords)
            {
                dataRecord.UserSession = this.UserSession;
            }
        }
        //------------------------------------------------------------------------------------
        public void SetDependencies(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            if (dataLoader != null)
            {
                dataLoader.AppConfig = appConfig;
                dataLoader.UserSession = userSession;
            }
            if (browseLoader != null)
            {
                browseLoader.AppConfig = appConfig;
                browseLoader.UserSession = userSession;
            }
            for (int i = 0; i < dataRecords.Count; i++)
            {
                FwDataReadWriteRecord dataRecord = dataRecords[i];
                dataRecord.AppConfig = appConfig;
                dataRecord.UserSession = userSession;
            }
        }
        //------------------------------------------------------------------------------------
    }
}

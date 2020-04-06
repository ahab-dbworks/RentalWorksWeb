using AutoMapper;
using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.Modules.Administrator.Alert;
using FwStandard.Modules.Administrator.DuplicateRule;
using FwStandard.Modules.Administrator.WebAuditJson;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FwStandard.BusinessLogic
{
    public enum TDataRecordSaveMode { Auto, smInsert, smUpdate };

    public class BeforeSaveEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwBusinessLogic Original { get; set; }
        public bool PerformSave { get; set; } = true;
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class InsteadOfSaveEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwBusinessLogic Original { get; set; }
        public FwSqlConnection SqlConnection { get; set; }
        public bool SavePerformed { get; set; } = false;
    }

    public class AfterSaveEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwBusinessLogic Original { get; set; }
        public int RecordsAffected { get; set; }
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class BeforeSaveDataRecordEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwDataReadWriteRecord Original { get; set; }
        public bool PerformSave { get; set; } = true;
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class InsteadOfSaveDataRecordEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwDataReadWriteRecord Original { get; set; }
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class AfterSaveDataRecordEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwDataReadWriteRecord Original { get; set; }
        public int RecordsAffected { get; set; }
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class BeforeValidateEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwBusinessLogic Original { get; set; }
    }

    public class BeforeValidateDataRecordEventArgs : EventArgs
    {
        public TDataRecordSaveMode SaveMode { get; set; }
        public FwDataReadWriteRecord Original { get; set; }
    }

    public class InsteadOfDataRecordDeleteEventArgs : EventArgs
    {
        public bool Success { get; set; } = true;
    }

    public class BeforeDeleteEventArgs : EventArgs
    {
        public bool PerformDelete { get; set; } = true;
        public string ErrorMessage { get; set; } = "";
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class AfterDeleteEventArgs : EventArgs
    {
        public FwSqlConnection SqlConnection { get; set; }
    }

    public class AfterMapEventArgs : EventArgs
    {
        public FwDataRecord Source { get; set; }
    }

    //---------------------------------------------------------------------------------------------
    public class FwBusinessLogicFieldDefinition
    {
        public string Name { get; set; } = string.Empty;
        public FwDataTypes DataType { get; set; } = FwDataTypes.Text;
    }
    //---------------------------------------------------------------------------------------------


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
                    if (rec != null)
                    {
                        rec.AppConfig = value;
                    }
                }
                if (dataLoader != null)
                {
                    dataLoader.AppConfig = value;
                }
                if (browseLoader != null)
                {
                    browseLoader.AppConfig = value;
                }
                if (_Custom != null)
                {
                    _Custom.AppConfig = value;
                }
            }
        }

        [JsonIgnore]
        public FwUserSession UserSession
        {
            get { return _userSession; }
            set
            {
                _userSession = value;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    if (rec != null)
                    {
                        rec.UserSession = value;
                    }
                }
                if (dataLoader != null)
                {
                    dataLoader.UserSession = value;
                }
                if (browseLoader != null)
                {
                    browseLoader.UserSession = value;
                }
                if (_Custom != null)
                {
                    _Custom.UserSession = value;
                }
            }
        }

        [JsonIgnore]
        public string BusinessLogicModuleName
        {
            get { return GetType().Name.Replace("Logic", ""); }
        }


        [JsonIgnore]
        protected List<FwDataReadWriteRecord> dataRecords = new List<FwDataReadWriteRecord>();

        [JsonIgnore]
        protected FwDataRecord dataLoader = null;

        [JsonIgnore]
        protected FwDataRecord browseLoader = null;

        [JsonIgnore]
        protected static FwJsonDataTable duplicateRules = null;

        [JsonIgnore]
        protected static FwJsonDataTable alerts = null;

        [JsonIgnore]
        public static FwCustomFields customFields = null;

        [JsonIgnore]
        public bool ForceSave { get; set; } = false;

        [JsonIgnore]
        public bool ReloadOnSave { get; set; } = true;

        [JsonIgnore]
        public bool LoadOriginalBeforeSaving { get; set; } = true;

        [JsonIgnore]
        public bool LoadOriginalBeforeDeleting { get; set; } = true;

        [JsonIgnore]
        public bool HasAudit { get; set; } = true;

        [JsonIgnore]
        protected bool UseTransactionToSave { get; set; } = false;   //maybe set this value to "true" for any Logic that has more than one record within

        [JsonIgnore]
        protected bool UseTransactionToDelete { get; set; } = false;  //maybe set this value to "true" for any Logic that has more than one record within

        [JsonIgnore]
        public bool DeleteRecordsInReverseSequence { get; set; } = false;

        //static Mutex CustomFieldMutex = new Mutex(false, "LoadCustomFields");
        static Mutex CustomFieldMutex = new Mutex();

        public FwCustomValues _Custom = new FwCustomValues();  //todo: don't initialize here.  Instead, only initialize when custom fields exist for this module.  load custom fields in a static class.

        public event EventHandler<BeforeSaveEventArgs> BeforeSave;
        public event EventHandler<InsteadOfSaveEventArgs> InsteadOfSave;
        public event EventHandler<AfterSaveEventArgs> AfterSave;
        public event EventHandler<BeforeValidateEventArgs> BeforeValidate;
        public event EventHandler<BeforeDeleteEventArgs> BeforeDelete;
        public event EventHandler<AfterDeleteEventArgs> AfterDelete;
        public event EventHandler<AfterMapEventArgs> AfterMap;

        public delegate void BeforeSaveEventHandler(BeforeSaveEventArgs e);
        public delegate void InsteadOfSaveEventHandler(InsteadOfSaveEventArgs e);
        public delegate void AfterSaveEventHandler(AfterSaveEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateEventArgs e);
        public delegate void BeforeDeleteEventHandler(BeforeDeleteEventArgs e);
        public delegate void AfterDeleteEventHandler(AfterDeleteEventArgs e);
        public delegate void AfterMapEventHandler(AfterMapEventArgs e);

        protected virtual async Task BeforeSaveAsync(BeforeSaveEventArgs e)
        {
            BeforeSave?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task InsteadOfSaveAsync(InsteadOfSaveEventArgs e)
        {
            InsteadOfSave?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AfterSaveAsync(AfterSaveEventArgs e)
        {
            AfterSave?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task BeforeValidateAsync(BeforeValidateEventArgs e)
        {
            BeforeValidate?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task BeforeDeleteAsync(BeforeDeleteEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AfterDeleteAsync(AfterDeleteEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AfterMapAsync(AfterMapEventArgs e)
        {
            AfterMap?.Invoke(this, e);
            await Task.CompletedTask;
        }


        //------------------------------------------------------------------------------------
        public FwBusinessLogic() { }
        //------------------------------------------------------------------------------------
        public static FwBusinessLogic CreateBusinessLogic(Type type, FwApplicationConfig appConfig, FwUserSession userSession)
        {
            FwBusinessLogic bl = (FwBusinessLogic)Activator.CreateInstance(type);
            bl.AppConfig = appConfig;
            bl.UserSession = userSession;
            bl.SetDependencies(appConfig, userSession);
            return bl;
        }
        //------------------------------------------------------------------------------------
        public static T CreateBusinessLogic<T>(FwApplicationConfig appConfig, FwUserSession userSession) where T : FwBusinessLogic
        {
            T bl = (T)Activator.CreateInstance(typeof(T));
            bl.AppConfig = appConfig;
            bl.UserSession = userSession;
            bl.SetDependencies(appConfig, userSession);
            return bl;
        }
        //------------------------------------------------------------------------------------
        private void LoadCustomFields()
        {
            //string moduleName = GetType().Name.Replace("Logic", "");
            if (!BusinessLogicModuleName.Equals("CustomField"))
            {
                if (customFields == null)
                {
                    refreshCustomFields();
                }

                _Custom.CustomFields.Clear();
                foreach (FwCustomField f in customFields)
                {
                    //if (f.ModuleName.Equals(this.BusinessLogicModuleName))
                    if (String.Compare(f.ModuleName, this.BusinessLogicModuleName, true) == 0)
                    {
                        _Custom.CustomFields.Add(f);
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request)
        {
            FwJsonDataTable dt = null;

            LoadCustomFields();

            bool useBrowseLoader = (browseLoader != null);

            if (useBrowseLoader)
            {
                if (request != null)
                {
                    if (request.forexcel)
                    {
                        useBrowseLoader = false;
                    }
                }
            }

            if (useBrowseLoader)
            {
                if (request != null)
                {
                    if (request.searchfields.Count > 0)
                    {
                        // if searching for specific fields and one or more of the searched fields is not in the browseLoader, then use the regular dataLoader instead
                        PropertyInfo[] properties = browseLoader.GetType().GetProperties();
                        for (int i = 0; i < request.searchfields.Count; i++)
                        {
                            bool searchFieldDefined = false;
                            if (!searchFieldDefined)
                            {
                                foreach (PropertyInfo property in properties)
                                {
                                    if (property.Name.Equals(request.searchfields[i]))
                                    {
                                        searchFieldDefined = true;
                                        break;
                                    }
                                }
                            }
                            if (!searchFieldDefined)
                            {
                                foreach (FwCustomField customField in customFields)
                                {
                                    if (customField.FieldName.Equals(request.searchfields[i]))
                                    {
                                        searchFieldDefined = true;
                                        break;
                                    }
                                }
                            }
                            if (!searchFieldDefined)
                            {
                                useBrowseLoader = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (useBrowseLoader)
            {
                browseLoader.UserSession = this.UserSession;
                dt = await browseLoader.BrowseAsync(request, _Custom.CustomFields);
            }
            else if (dataLoader != null)
            {
                dataLoader.UserSession = this.UserSession;
                dt = await dataLoader.BrowseAsync(request, _Custom.CustomFields);
            }
            else
            {
                if (dataRecords.Count > 0)
                {
                    dataRecords[0].UserSession = this.UserSession;
                    dt = await dataRecords[0].BrowseAsync(request, _Custom.CustomFields);
                }
            }

            return dt;

        }
        //------------------------------------------------------------------------------------
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequest request, FwSqlConnection conn = null)
        {
            LoadCustomFields();

            List<T> records = null;
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    MethodInfo method = dataRecords[0].GetType().GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(dataRecords[0].GetType());
                    FwCustomFields customFields = _Custom.CustomFields;
                    dynamic result = generic.Invoke(dataRecords[0], new object[] { request, customFields, conn });
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
                FwCustomFields customFields = _Custom.CustomFields;
                dynamic result = generic.Invoke(dataLoader, new object[] { request, customFields, conn });
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
        public virtual async Task<GetResponse<T>> GetManyAsync<T>(GetRequest request, Func<FwSqlSelect, Task> beforeExecuteQuery = null, FwSqlConnection conn = null)
        {
            LoadCustomFields();

            GetResponse<T> response = new GetResponse<T>();
            if (dataLoader == null)
            {
                if (dataRecords.Count > 0)
                {
                    MethodInfo method = dataRecords[0].GetType().GetMethod("GetManyAsync");
                    MethodInfo generic = method.MakeGenericMethod(dataRecords[0].GetType());
                    FwCustomFields customFields = _Custom.CustomFields;
                    dynamic taskGetManyAsync = generic.Invoke(dataRecords[0], new object[] { request, customFields, beforeExecuteQuery, conn });
                    var result = await taskGetManyAsync;
                    if (result != null)
                    {
                        response.PageNo = result.PageNo;
                        response.PageSize = result.PageSize;
                        response.TotalRows = result.TotalRows;
                        response.Sort = request.Sort;
                        response.Items = new List<T>(result.Items.Count);
                        Mapper.Map((object)result.Items, response.Items, opts =>
                        {
                            opts.ConfigureMap(MemberList.None);
                        });
                    }
                }
            }
            else
            {
                MethodInfo method = dataLoader.GetType().GetMethod("GetManyAsync");
                MethodInfo generic = method.MakeGenericMethod(dataLoader.GetType());
                FwCustomFields customFields = _Custom.CustomFields;
                dynamic taskGetManyAsync = generic.Invoke(dataLoader, new object[] { request, customFields, beforeExecuteQuery, conn });
                var result = await taskGetManyAsync;
                if (result != null)
                {
                    response.PageNo = result.PageNo;
                    response.PageSize = result.PageSize;
                    response.TotalRows = result.TotalRows;
                    response.Sort = request.Sort;
                    response.Items = new List<T>(result.Items.Count);
                    Mapper.Map((object)result.Items, response.Items, opts =>
                    {
                        opts.ConfigureMap(MemberList.None);
                    });
                }
            }
            return response;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(object[] primaryKeyValues, FwSqlConnection conn = null)
        {
            bool businessLogicLoaded = false;
            bool recordLoaded = false;
            FwApplicationConfig tmpAppConfig = AppConfig;
            FwUserSession tmpUserSession = UserSession;

            LoadCustomFields();

            if (dataLoader == null)
            {
                for (int i = 0; i < dataRecords.Count; i++)
                {
                    FwDataReadWriteRecord rec = dataRecords[i];
                    rec = await rec.GetAsync<T>(primaryKeyValues, _Custom.CustomFields, conn);
                    dataRecords[i] = rec;
                    Mapper.Map(rec, this, opts =>
                    {
                        opts.ConfigureMap(MemberList.None);
                    });

                    AfterMapEventArgs afterMapArgs = new AfterMapEventArgs();
                    afterMapArgs.Source = rec;
                    await AfterMapAsync(afterMapArgs);

                    recordLoaded = (rec != null);

                    if (i == 0)
                    {
                        businessLogicLoaded = recordLoaded;
                    }
                    i++;
                }
                this.AppConfig = tmpAppConfig;
                this.UserSession = tmpUserSession;
            }
            else
            {
                dataLoader = await dataLoader.GetAsync<T>(primaryKeyValues, _Custom.CustomFields, conn);

                businessLogicLoaded = (dataLoader != null);
                Mapper.Map(dataLoader, this, opts =>
                {
                    opts.ConfigureMap(MemberList.None);
                });
                this.AppConfig = tmpAppConfig;
                this.UserSession = tmpUserSession;

                AfterMapEventArgs afterMapArgs = new AfterMapEventArgs();
                afterMapArgs.Source = dataLoader;
                await AfterMapAsync(afterMapArgs);

            }

            //justin hoffman 03/18/2020 hopefully temporary while we figure out why AutoMapper is mapping all fields as NULL
            ValidateMappedObject();

            return businessLogicLoaded;
        }
        //------------------------------------------------------------------------------------
        protected void ValidateMappedObject()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            List<PropertyInfo> nonPrimaryKeyFields = new List<PropertyInfo>();
            foreach (PropertyInfo property in properties)
            {
                bool isPrimaryKey = false;
                if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;

                            if (businessLogicFieldAttribute.IsPrimaryKey)
                            {
                                isPrimaryKey = true;
                            }
                        }
                    }
                }

                if (!isPrimaryKey)
                {
                    nonPrimaryKeyFields.Add(property);
                }
            }

            if (nonPrimaryKeyFields.Count > 0)
            {
                bool allNullValues = true;
                foreach (PropertyInfo property in nonPrimaryKeyFields)
                {
                    object propertyValue = property.GetValue(this);
                    if (propertyValue != null)
                    {
                        allNullValues = false;
                        break;
                    }
                }

                if (allNullValues)
                {
                    throw new Exception($"Internal object mapping error in {this.BusinessLogicModuleName}.");
                }
            }

        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(FwSqlConnection conn = null)
        {
            return await LoadAsync<T>(GetPrimaryKeys(), conn);
        }
        //------------------------------------------------------------------------------------
        protected virtual List<PropertyInfo> GetPrimaryKeyProperties(bool includeOptional = true)
        {
            List<PropertyInfo> primaryKeyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
                            if (includeOptional)
                            {
                                if ((businessLogicFieldAttribute.IsPrimaryKey) || (businessLogicFieldAttribute.IsPrimaryKeyOptional))
                                {
                                    primaryKeyProperties.Add(property);
                                }
                            }
                            else
                            {
                                if (businessLogicFieldAttribute.IsPrimaryKey)
                                {
                                    primaryKeyProperties.Add(property);
                                }
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
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
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
                        else if (propertyValue == null)
                        {
                            hasPrimaryKeysSet = false;
                        }
                        else
                        {
                            throw new Exception(property.Name + ": A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic.AllPrimaryKeysHaveValues]");
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
            object[] ids = new object[pkProperties.Count];

            for (int i = 0; i < pkProperties.Count; i++)
            {
                PropertyInfo pkProperty = pkProperties[i];
                ids[i] = pkProperty.GetValue(this);
            }

            return ids;
        }
        //------------------------------------------------------------------------------------
        public virtual void SetPrimaryKeys(object[] ids)
        {
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();

            if (!pkProperties.Count.Equals(ids.Length))
            {
                pkProperties = GetPrimaryKeyProperties(false);
            }

            if (!pkProperties.Count.Equals(ids.Length))
            {
                throw new Exception("Invalid number of primary key values provided.  Expected " + pkProperties.Count.ToString() + ", Received " + ids.Length.ToString() + ". [FwBusinessLogic.SetPrimaryKeys]");
            }

            for (int i = 0; i < pkProperties.Count; i++)
            {
                PropertyInfo pkProperty = pkProperties[i];
                Type propertyType = pkProperty.PropertyType;

                if ((propertyType == typeof(int?)) || (propertyType == typeof(Int32)))
                {
                    pkProperty.SetValue(this, Convert.ToInt32(ids[i]));
                }
                else if (propertyType == typeof(string))
                {
                    pkProperty.SetValue(this, ids[i]);
                }
                else if ((propertyType == typeof(bool?)) || (propertyType == typeof(bool)))
                {
                    bool b = false;
                    if (ids[i] != null)
                    {
                        b = FwConvert.ToBoolean(ids[i].ToString());
                    }
                    pkProperty.SetValue(this, b);
                }
                else
                {
                    throw new Exception("Primary key property type not implemented for " + propertyType.ToString() + ". [FwBusinessLogic.SetPrimaryKeys]");
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
        public bool refreshCustomFields()
        {
            bool customFieldsLoaded = false;

            // this is a protected area. we don't want two threads trying to manage the static "customFields" array
            CustomFieldMutex.WaitOne();

            customFields = new FwCustomFields();

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select modulename, fieldname, customtablename, customfieldname, fieldtype");
                    qry.Add("from customfieldview with (nolock)");
                    qry.Add("order by fieldname");

                    qry.AddColumn("modulename");
                    qry.AddColumn("fieldname");
                    qry.AddColumn("customtablename");
                    qry.AddColumn("customfieldname");
                    qry.AddColumn("fieldtype");

                    FwJsonDataTable table = qry.QueryToFwJsonTableAsync(true).Result;
                    for (int r = 0; r < table.Rows.Count; r++)
                    {
                        FwCustomField customField = new FwCustomField(table.Rows[r][0].ToString(), table.Rows[r][1].ToString(), table.Rows[r][2].ToString(), table.Rows[r][3].ToString(), table.Rows[r][4].ToString());
                        customFields.Add(customField);
                    }
                    customFieldsLoaded = true;
                }
            }

            // end protected area.
            CustomFieldMutex.ReleaseMutex();

            return customFieldsLoaded;
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task CheckDuplicatesAsync(TDataRecordSaveMode saveMode, FwBusinessLogic original, FwValidateResult result)
        {
            if (duplicateRules == null)
            {
                refreshDuplicateRules();
            }

            if (duplicateRules != null)
            {
                object[] ids = GetPrimaryKeys();

                var duplicateRows = duplicateRules.Rows;
                List<object> rulesList = new List<object>();

                foreach (var row in duplicateRows)
                {
                    if (row[1].ToString().Equals(this.BusinessLogicModuleName))
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
                    l2.UserSession = dataRecords[0].UserSession;

                    foreach (List<object> rule in rulesList)
                    {
                        bool considerBlanks = (bool)rule[8];
                        string[] fields = rule[5].ToString().Split(',').ToArray();
                        string[] datatypes = rule[6].ToString().Split(',').ToArray();

                        BrowseRequest browseRequest2 = new BrowseRequest();
                        browseRequest2.module = this.BusinessLogicModuleName;

                        List<string> searchOperators = new List<string>();
                        List<string> searchFieldVals = new List<string>();
                        List<string> searchFieldTypes = new List<string>();

                        var updatedFieldList = fields.ToList();

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            string fieldName = fields[i];
                            bool propertyFound = false;
                            if (!propertyFound)
                            {
                                foreach (PropertyInfo property in propertyInfo)
                                {
                                    if (property.Name.Equals(fieldName))
                                    {
                                        propertyFound = true;
                                        //var value = this.GetType().GetProperty(property.Name).GetValue(this, null);
                                        object value = this.GetType().GetProperty(property.Name).GetValue(this);
                                        object valueToCompare = null;

                                        //if (value != null)
                                        //{
                                        //    if (!considerBlanks)
                                        //    {
                                        //        if (string.IsNullOrWhiteSpace(value.ToString()))
                                        //        {
                                        //            updatedFieldList.Remove(fieldName);
                                        //            break;
                                        //        }
                                        //    }
                                        //}

                                        if (value != null)
                                        {
                                            valueToCompare = value;
                                            //searchFieldVals.Add(value.ToString());
                                            //searchOperators.Add("=");
                                            int datatypeIndex = Array.IndexOf(fields, fieldName);
                                            searchFieldTypes.Add(datatypes[datatypeIndex].ToLower());
                                        }
                                        else
                                        {
                                            if (saveMode == TDataRecordSaveMode.smUpdate)
                                            {
                                                bool b = await l2.LoadAsync<Type>(ids);
                                                //var databaseValue = l2.GetType().GetProperty(property.Name).GetValue(l2, null);
                                                //searchFieldVals.Add(databaseValue.ToString());
                                                valueToCompare = l2.GetType().GetProperty(property.Name).GetValue(l2);
                                            }
                                            else
                                            {
                                                searchFieldVals.Add("");
                                                searchOperators.Add("=");
                                                valueToCompare = "";
                                            }
                                        }


                                        //if ((!string.IsNullOrWhiteSpace(valueToCompare.ToString())) || (considerBlanks))
                                        //{
                                        //    searchFieldVals.Add(valueToCompare.ToString());
                                        //    searchOperators.Add("=");
                                        //}

                                        if (!string.IsNullOrWhiteSpace(valueToCompare.ToString()))
                                        {
                                            searchFieldVals.Add(valueToCompare.ToString());
                                            searchOperators.Add("=");
                                        }
                                        else  // (valueToCompare is blank)
                                        {
                                            if (considerBlanks)
                                            {
                                                searchFieldVals.Add(valueToCompare.ToString());
                                                searchOperators.Add("=");
                                            }
                                            else
                                            {
                                                searchFieldVals.Add(valueToCompare.ToString());
                                                searchOperators.Add(">");
                                            }
                                        }


                                        //if (searchOperators.Count == searchFieldVals.Count)
                                        //{
                                        //    break;
                                        //}
                                    }
                                }
                            }

                            if (!propertyFound)  // property not found, check Custom Fields
                            {
                                LoadCustomFields();

                                foreach (FwCustomField customField in _Custom.CustomFields)
                                {
                                    if (customField.FieldName.Equals(fieldName))
                                    {
                                        propertyFound = true;

                                        string value = null;
                                        for (int customFieldIndex = 0; customFieldIndex < _Custom.Count; customFieldIndex++)
                                        {
                                            if (_Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                                            {
                                                value = _Custom[customFieldIndex].FieldValue;
                                            }
                                        }

                                        if (value != null)
                                        {
                                            searchFieldVals.Add(value.ToString());
                                            searchOperators.Add("=");
                                            int datatypeIndex = Array.IndexOf(fields, fieldName);
                                            searchFieldTypes.Add(datatypes[datatypeIndex].ToLower());
                                        }
                                        else
                                        {
                                            if (saveMode == TDataRecordSaveMode.smUpdate)
                                            {
                                                bool b = l2.LoadAsync<Type>(ids).Result;
                                                var databaseValue = "";
                                                for (int customFieldIndex = 0; customFieldIndex < l2._Custom.Count; customFieldIndex++)
                                                {
                                                    if (l2._Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                                                    {
                                                        databaseValue = l2._Custom[customFieldIndex].FieldValue;
                                                    }
                                                }
                                                searchFieldVals.Add(databaseValue.ToString());
                                            }
                                            else
                                            {
                                                searchFieldVals.Add("");
                                                searchOperators.Add("=");
                                            }
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        browseRequest2.searchfieldtypes = searchFieldTypes;
                        browseRequest2.searchfields = updatedFieldList;
                        browseRequest2.searchfieldoperators = searchOperators;
                        browseRequest2.searchfieldvalues = searchFieldVals;
                        FwBusinessLogic l3 = (FwBusinessLogic)Activator.CreateInstance(type);
                        l3.AppConfig = dataRecords[0].AppConfig;
                        l3.UserSession = dataRecords[0].UserSession;
                        FwJsonDataTable dt = await l3.BrowseAsync(browseRequest2);

                        bool isDuplicate = false;
                        for (int r = 0; r <= dt.Rows.Count - 1; r++)
                        {
                            isDuplicate = true;
                            if (saveMode == TDataRecordSaveMode.smUpdate)
                            {
                                //var dtToArray = dt.Rows[r].Select(i => i.ToString()).ToArray();
                                bool pkFound = true;
                                foreach (object id in ids)
                                {
                                    string idString = id.ToString();
                                    //int indexOfId = Array.IndexOf(dtToArray, idString);
                                    //pkFound = (indexOfId >= 0);
                                    foreach (object objField in dt.Rows[r])
                                    {
                                        string strField = objField.ToString();
                                        if (strField.Equals(idString))
                                        {
                                            pkFound = true;
                                            break;
                                        }
                                    }
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
                            result.IsValid = false;
                            //validateMsg = "A record of this type already exists. " + "(" + rule[2] + ")";
                            result.ValidateMsg = this.BusinessLogicModuleName + " cannot be saved because of Duplicate Rule \"" + rule[2] + "\"";
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual bool IsValidStringValue(PropertyInfo property, string[] acceptableValues, ref string validateMsg, bool nullAcceptable = true)
        {
            bool isValidValue = false;
            object valueObj = property.GetValue(this);
            if (valueObj == null)
            {
                isValidValue = (nullAcceptable);
                if (!isValidValue)
                {
                    validateMsg = property.Name + " cannot be NULL.";
                }
            }
            else
            {
                string valueStr = valueObj.ToString();
                for (int i = 0; i < acceptableValues.Length; i++)
                {
                    if (valueStr.Equals(acceptableValues[i]))
                    {
                        isValidValue = true;
                        break;
                    }
                }
                if (!isValidValue)
                {
                    validateMsg = "Invalid " + property.Name + ": " + valueStr + ".  Acceptable values are " + string.Join(",", acceptableValues);
                }
            }
            return isValidValue;
        }
        //------------------------------------------------------------------------------------ 
        public bool ChangingProperty(PropertyInfo property, FwBusinessLogic original)
        {
            bool changing = false;
            object newValue = property.GetValue(this);
            if (newValue != null) // new value is specified
            {
                if (original == null)
                {
                    changing = !newValue.ToString().Equals(string.Empty);
                }
                else
                {
                    object oldValue = property.GetValue(original);

                    if (!newValue.Equals(oldValue))  // new value is different from old value
                    {
                        changing = true;
                    }
                }
            }
            return changing;
        }
        //------------------------------------------------------------------------------------ 
        public bool ValidateNotChangingProperty(PropertyInfo property, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = !ChangingProperty(property, original);
            if (!isValid)
            {
                validateMsg = "Cannot change the " + property.Name + " on this " + BusinessLogicModuleName + ".";
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public bool ValidateNotChangingProperty(List<PropertyInfo> properties, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            foreach (PropertyInfo property in properties)
            {
                isValid = ValidateNotChangingProperty(property, original, ref validateMsg);
                if (!isValid)
                {
                    break;
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        protected virtual List<PropertyInfo> GetUnassignableProperties()  //properties that cannot be assigned on New
        {
            List<PropertyInfo> unassignableProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
                            if (businessLogicFieldAttribute.DisableDirectAssign)
                            {
                                unassignableProperties.Add(property);
                            }
                        }
                    }
                }
            }
            return unassignableProperties;
        }
        //------------------------------------------------------------------------------------
        protected virtual List<PropertyInfo> GetUnmodifiableProperties()  //properties that cannot be modified on Edit
        {
            List<PropertyInfo> unmodifiableProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
                            if (businessLogicFieldAttribute.DisableDirectModify)
                            {
                                unmodifiableProperties.Add(property);
                            }
                        }
                    }
                }
            }
            return unmodifiableProperties;
        }
        //------------------------------------------------------------------------------------
        public bool ValidateNotChangingProhibitedProperties(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            List<PropertyInfo> properties;
            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                properties = GetUnassignableProperties();
            }
            else //if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                properties = GetUnmodifiableProperties();
            }
            isValid = ValidateNotChangingProperty(properties, original, ref validateMsg);

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        protected virtual bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            //override this method on a derived class to implement custom validation logic
            bool isValid = true;
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task ValidateBusinessLogicAsync(TDataRecordSaveMode saveMode, FwBusinessLogic original, FwValidateResult result)
        {
            BeforeValidateEventArgs args = new BeforeValidateEventArgs();
            args.SaveMode = saveMode;
            args.Original = original;
            //BeforeValidate?.Invoke(this, args);
            await BeforeValidateAsync(args);

            // validate not assigning any prohibited properties on New or Edit
            if (result.IsValid)
            {
                var validateMsg = result.ValidateMsg;
                result.IsValid = ValidateNotChangingProhibitedProperties(saveMode, original, ref validateMsg);
                result.ValidateMsg = validateMsg;
            }

            if (result.IsValid)
            {
                int r = 0;
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    FwDataReadWriteRecord originalRec = null;
                    if (original != null)
                    {
                        originalRec = original.dataRecords[r];
                    }
                    await rec.ValidateDataRecordAsync(saveMode, originalRec, result);
                    if (!result.IsValid)
                    {
                        break;
                    }
                    r++;
                }
            }
            if (result.IsValid)
            {
                //check for duplicate Business Logic here
                await CheckDuplicatesAsync(saveMode, original, result);
            }
            if (result.IsValid)
            {
                var validateMsg = result.ValidateMsg;
                result.IsValid = Validate(saveMode, original, ref validateMsg);
                result.ValidateMsg = validateMsg;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<int> SaveAsync(FwBusinessLogic original = null, FwSqlConnection conn = null, TDataRecordSaveMode saveMode = TDataRecordSaveMode.Auto)
        {
            bool success = false;
            int rowsAffected = 0;
            if (saveMode == TDataRecordSaveMode.Auto)
            {
                saveMode = (AllPrimaryKeysHaveValues ? TDataRecordSaveMode.smUpdate : TDataRecordSaveMode.smInsert);
            }
            bool transactionInitializedHere = false;
            try
            {
                if (ForceSave)
                {
                    foreach (FwDataReadWriteRecord rec in dataRecords)
                    {
                        rec.ForceSave = true;
                    }
                }

                if (UseTransactionToSave)
                {
                    if (conn == null)
                    {
                        conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                        await conn.GetConnection().OpenAsync();
                    }

                    if (conn.GetActiveTransaction() == null)
                    {
                        conn.BeginTransaction();
                        transactionInitializedHere = true;
                    }
                }

                BeforeSaveEventArgs beforeSaveArgs = new BeforeSaveEventArgs();
                beforeSaveArgs.SaveMode = saveMode;
                beforeSaveArgs.Original = original;
                beforeSaveArgs.SqlConnection = conn;

                InsteadOfSaveEventArgs insteadOfSaveArgs = new InsteadOfSaveEventArgs();
                insteadOfSaveArgs.SaveMode = saveMode;
                insteadOfSaveArgs.Original = original;
                insteadOfSaveArgs.SqlConnection = conn;

                AfterSaveEventArgs afterSaveArgs = new AfterSaveEventArgs();
                afterSaveArgs.SaveMode = saveMode;
                afterSaveArgs.Original = original;
                afterSaveArgs.SqlConnection = conn;

                //BeforeSave?.Invoke(this, beforeSaveArgs);
                await BeforeSaveAsync(beforeSaveArgs);
                if (beforeSaveArgs.PerformSave)
                {
                    bool savePerformed = false;
                    if (InsteadOfSave != null)
                    {
                        InsteadOfSave(this, insteadOfSaveArgs);
                        savePerformed = insteadOfSaveArgs.SavePerformed;
                    }
                    else
                    {
                        FwDataReadWriteRecord originalRec = null;
                        for (int r = 0; r < dataRecords.Count; r++)
                        {
                            var rec = dataRecords[r];
                            if (original != null)
                            {
                                originalRec = original.dataRecords[r];
                            }
                            rowsAffected += await rec.SaveAsync(originalRec, conn);
                        }
                        LoadCustomFields();

                        bool customFieldsSaved = false;
                        if (_Custom.Count > 0)
                        {
                            customFieldsSaved = await _Custom.SaveAsync(GetPrimaryKeys(), conn);
                            if (customFieldsSaved && rowsAffected == 0)
                            {
                                rowsAffected = 1;
                            }
                        }
                        savePerformed = ((rowsAffected > 0) || customFieldsSaved);
                    }

                    //justin 10/16/2018 CAS-23961-WQNG temporary fix to make the AfterSave fire whenever notes are supplied.  Notes are currently saved outside of this framework.
                    //justin 03/05/2019 would like to automate the saving of "notes" data to the appnote table instead of hand-coding the save function in each inherited BusinessLogic.
                    //#jhtodo
                    if (!savePerformed)
                    {
                        PropertyInfo p = null;
                        if (p == null)
                        {
                            p = this.GetType().GetProperty("Notes");
                        }
                        if (p == null)
                        {
                            p = this.GetType().GetProperty("Note");
                        }
                        if (p == null)
                        {
                            p = this.GetType().GetProperty("ToolBarJson"); //temporary
                        }
                        if (p == null)
                        {
                            p = this.GetType().GetProperty("Html"); //temporary
                        }
                        if (p != null)
                        {
                            savePerformed = (p.GetValue(this, null) != null);
                        }
                    }
                    if ((savePerformed) || (ForceSave))
                    {
                        //AfterSave?.Invoke(this, afterSaveArgs);
                        await AfterSaveAsync(afterSaveArgs);
                        if (rowsAffected == 0)
                        {
                            rowsAffected = afterSaveArgs.RecordsAffected;
                        }

                        // process alerts on another thread without blocking
                        _ = Task.Run(async () =>
                        {
                            await AlertFunc.ProcessAlertsAsync(this.AppConfig, this.UserSession, this.BusinessLogicModuleName, original, this, saveMode);
                        });
                    }
                }
                success = true;
            }
            finally
            {
                if (transactionInitializedHere)
                {
                    if (success)
                    {
                        conn.CommitTransaction();
                    }
                    else
                    {
                        // if a rollback occurs within a trigger, then this entire transaction will already be rolled back. 
                        // don't know if this is a bug in the database connection or not.
                        // but I am explicitly rolling back here incase a rollback does not occur within a trigger, for any reason.
                        conn.RollbackTransaction();
                    }
                    conn.Close();
                }
            }

            if ((success) && (HasAudit) && (rowsAffected > 0))
            {
                //// Add the audit record on another thread without blocking
                //_ = Task.Run(async () =>
                //{
                //    await WebAuditJsonFunc.AddAuditAsync(this.AppConfig, this.UserSession, original, this);
                //});
                AddAudit(original);
            }

            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual void AddAudit(FwBusinessLogic original)
        {
            // Add the audit record on another thread without blocking
            _ = Task.Run(async () =>
                    {
                        await WebAuditJsonFunc.AddAuditAsync(this.AppConfig, this.UserSession, original, this);
                    });
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> DeleteAsync(FwSqlConnection conn = null)
        {
            bool success = true;
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();

            bool transactionInitializedHere = false;
            try
            {
                if (UseTransactionToDelete)
                {
                    if (conn == null)
                    {
                        conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                        await conn.GetConnection().OpenAsync();
                    }

                    if (conn.GetActiveTransaction() == null)
                    {
                        conn.BeginTransaction();
                        transactionInitializedHere = true;
                    }
                }
                beforeDeleteArgs.SqlConnection = conn;
                afterDeleteArgs.SqlConnection = conn;

                await BeforeDeleteAsync(beforeDeleteArgs);
                if (beforeDeleteArgs.PerformDelete)
                {


                    if (DeleteRecordsInReverseSequence)
                    {
                        foreach (FwDataReadWriteRecord rec in dataRecords.Reverse<FwDataReadWriteRecord>())
                        {
                            success &= await rec.DeleteAsync(conn);
                        }
                    }
                    else
                    {
                        foreach (FwDataReadWriteRecord rec in dataRecords)
                        {
                            success &= await rec.DeleteAsync(conn);
                        }
                    }
                    await AfterDeleteAsync(afterDeleteArgs);

                    // process alerts on another thread without blocking
                    _ = Task.Run(async () =>
                    {
                        await AlertFunc.ProcessAlertsAsync(this.AppConfig, this.UserSession, this.BusinessLogicModuleName, this, null, null);
                    });
                }
            }
            finally
            {
                if (transactionInitializedHere)
                {
                    if (success)
                    {
                        // if a rollback occurs within a trigger, then this entire transaction will already be rolled back. 
                        // system will give an error here (This SqlTransaction has completed; it is no longer usable.).  Still working on a fix for this
                        conn.CommitTransaction();
                    }
                    else
                    {
                        // if a rollback occurs within a trigger, then this entire transaction will already be rolled back. 
                        // don't know if this is a bug in the database connection or not.
                        // but I am explicitly rolling back here incase a rollback does not occur within a trigger, for any reason.
                        conn.RollbackTransaction();
                    }
                    conn.Close();
                }
            }

            if (!beforeDeleteArgs.PerformDelete)
            {
                if (!string.IsNullOrEmpty(beforeDeleteArgs.ErrorMessage))
                {
                    throw new Exception(beforeDeleteArgs.ErrorMessage);
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
                if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                        {
                            FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
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
                        else if (propertyValue is decimal)
                        {
                            title = title + ((decimal)propertyValue).ToString().TrimEnd();
                        }
                        else if (propertyValue is DateTime)
                        {
                            title = title + ((DateTime)propertyValue).ToShortDateString();
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
            if (_Custom != null)
            {
                _Custom.UserSession = this.UserSession;
            }
            foreach (FwDataReadWriteRecord dataRecord in dataRecords)
            {
                dataRecord.UserSession = this.UserSession;
            }
        }
        //------------------------------------------------------------------------------------
        public void SetDependencies(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            AppConfig = appConfig;
            UserSession = userSession;

            LoadCustomFields();

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
            if (_Custom != null)
            {
                _Custom.AppConfig = appConfig;
                _Custom.UserSession = userSession;
            }
            for (int i = 0; i < dataRecords.Count; i++)
            {
                //FwDataReadWriteRecord dataRecord = dataRecords[i];
                //dataRecord.AppConfig = appConfig;
                //dataRecord.UserSession = userSession;
                if (dataRecords[i] != null)
                {
                    dataRecords[i].SetDependencies(appConfig, userSession);
                }
            }
        }
        //------------------------------------------------------------------------------------
        public List<FwBusinessLogicFieldDelta> GetChanges(FwBusinessLogic original)
        {
            List<FwBusinessLogicFieldDelta> deltas = new List<FwBusinessLogicFieldDelta>();
            if ((original == null) || (original.GetType().Equals(GetType())))  // "this" and original must be the same type.  or original can be null, which means we are Inserting a new record
            {
                PropertyInfo[] properties = this.GetType().GetProperties();
                object oldValue = null;
                object newValue = null;
                foreach (PropertyInfo property in properties)
                {
                    bool auditProperty = true;
                    bool isAuditMasked = false;

                    if (auditProperty)
                    {
                        if (property.IsDefined(typeof(JsonIgnoreAttribute)))
                        {
                            auditProperty = false;
                        }
                    }


                    if (auditProperty)
                    {
                        if (property.IsDefined(typeof(FwLogicPropertyAttribute)))
                        {
                            foreach (Attribute attribute in property.GetCustomAttributes())
                            {
                                if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                                {
                                    FwLogicPropertyAttribute businessLogicFieldAttribute = (FwLogicPropertyAttribute)attribute;
                                    if ((businessLogicFieldAttribute.IsPrimaryKey) || (businessLogicFieldAttribute.IsPrimaryKeyOptional))
                                    {
                                        auditProperty = false;
                                    }
                                    if (businessLogicFieldAttribute.IsNotAudited)
                                    {
                                        auditProperty = false;
                                    }
                                    isAuditMasked = businessLogicFieldAttribute.IsAuditMasked;
                                }
                            }
                        }
                    }

                    if (auditProperty)
                    {
                        if (property.Name.Equals("RecordTitle"))
                        {
                            auditProperty = false;
                        }
                        else if (property.Name.Contains("DateStamp"))
                        {
                            auditProperty = false;
                        }
                    }

                    if (auditProperty)
                    {
                        newValue = property.GetValue(this);
                        if (newValue != null)  // property value is not null, so it was supplied with the Post and probably changing
                        {
                            bool valueChanged = false;
                            Type propertyType = property.PropertyType;
                            if (original == null) // if original is null, then "this" is an inserted record
                            {
                                oldValue = "";
                                valueChanged = false;

                                if ((propertyType == typeof(int?)) || (propertyType == typeof(Int32)))
                                {
                                    valueChanged = (Convert.ToInt32(newValue) != 0);
                                }
                                else if ((propertyType == typeof(decimal?)) || (propertyType == typeof(Decimal)))
                                {
                                    valueChanged = (Convert.ToDecimal(newValue) != 0);
                                }
                                else if (propertyType == typeof(string))
                                {
                                    valueChanged = (!string.IsNullOrWhiteSpace(newValue.ToString()));
                                }
                                else if ((propertyType == typeof(Boolean?)) || (propertyType == typeof(bool)))
                                {
                                    valueChanged = (bool)newValue;
                                }
                                else if ((propertyType == typeof(DateTime?)) || (propertyType == typeof(DateTime)))
                                {
                                    valueChanged = ((newValue != null) && (Convert.ToDateTime(newValue) != DateTime.MinValue));
                                }
                                else
                                {
                                    valueChanged = false;
                                }
                            }
                            else
                            {
                                oldValue = original.GetType().GetProperty(property.Name).GetValue(original, null);
                                valueChanged = (!newValue.Equals(oldValue));
                            }
                            if (valueChanged)
                            {
                                if (isAuditMasked)
                                {
                                    oldValue = "##########";
                                    newValue = "##########";
                                }
                                deltas.Add(new FwBusinessLogicFieldDelta(property.Name, oldValue, newValue));
                            }
                        }
                    }
                }

                //Check for changes in customfields
                FwCustomValues newCustomFieldValues = this._Custom;
                FwCustomValues origCustomFieldValues = null;
                if (original != null)
                {
                    origCustomFieldValues = original._Custom;
                }
                if (newCustomFieldValues.Count > 0)
                {
                    foreach (FwCustomValue newCustomFieldValue in newCustomFieldValues)
                    {
                        bool valueChanged = false;
                        newValue = newCustomFieldValue.FieldValue;
                        if (newValue != null)
                        {
                            switch (newCustomFieldValue.FieldType)
                            {
                                case "Text":
                                    break;
                                case "Integer":
                                    Int32? j = 0;
                                    j = FwConvert.ToInt32(newValue);
                                    newValue = j;
                                    break;
                                case "Float":
                                    Decimal? d = 0;
                                    d = FwConvert.ToDecimal(newValue);
                                    newValue = d;
                                    break;
                                case "Date":
                                    DateTime? dt = null;
                                    dt = FwConvert.ToDateTime(newValue.ToString());
                                    newValue = dt.ToString();
                                    break;
                                case "True/False":
                                    newValue = FwConvert.LogicalToCharacter(FwConvert.ToBoolean(newValue.ToString()));
                                    break;
                            }

                            if (origCustomFieldValues == null)
                            {
                                valueChanged = true;
                            }
                            else
                            {
                                oldValue = "";
                                foreach (FwCustomValue origCustomFieldValue in origCustomFieldValues)
                                {
                                    if (origCustomFieldValue.FieldName.Equals(newCustomFieldValue.FieldName))
                                    {
                                        oldValue = origCustomFieldValue.FieldValue;

                                        switch (newCustomFieldValue.FieldType)
                                        {
                                            case "Text":
                                                break;
                                            case "Integer":
                                                Int32? j = 0;
                                                j = FwConvert.ToInt32(oldValue);
                                                oldValue = j;
                                                break;
                                            case "Float":
                                                Decimal? d = 0;
                                                d = FwConvert.ToDecimal(oldValue);
                                                oldValue = d;
                                                break;
                                            case "Date":
                                                DateTime? dt = null;
                                                dt = FwConvert.ToDateTime(oldValue.ToString());
                                                oldValue = dt.ToString();
                                                break;
                                            case "True/False":
                                                oldValue = FwConvert.LogicalToCharacter(FwConvert.ToBoolean(oldValue.ToString()));
                                                break;
                                        }

                                        valueChanged = (!newValue.Equals(oldValue));
                                        break;
                                    }
                                }
                            }
                        }

                        if (valueChanged)
                        {
                            deltas.Add(new FwBusinessLogicFieldDelta(newCustomFieldValue.FieldName, oldValue, newValue));
                        }
                    }
                }
            }

            //remove "id" fields where the corresponding display field is also in the list of fields
            List<int> removeIndexes = new List<int>();
            int index = 0;
            foreach (FwBusinessLogicFieldDelta delta in deltas)
            {
                if (delta.FieldName.ToLower().EndsWith("id"))
                {
                    foreach (FwBusinessLogicFieldDelta delta2 in deltas)
                    {
                        if ((delta2.FieldName.ToLower() + "id").Equals(delta.FieldName.ToLower()))
                        {
                            removeIndexes.Add(index);
                        }
                    }
                }
                index++;
            }

            for (int i = removeIndexes.Count - 1; i >= 0; i--)
            {
                deltas.RemoveAt(removeIndexes[i]);
            }

            deltas.Sort();

            return deltas;
        }
        //------------------------------------------------------------------------------------

        [JsonIgnore]
        public bool IsEmptyObject = false;

        public List<FwBusinessLogicFieldDefinition> _Fields
        {
            get
            {
                List<FwBusinessLogicFieldDefinition> fields = null;
                if (IsEmptyObject)
                {
                    fields = new List<FwBusinessLogicFieldDefinition>();

                    PropertyInfo[] properties = null;
                    if (dataLoader == null)
                    {
                        properties = dataRecords[0].GetType().GetProperties();
                    }
                    else
                    {
                        properties = dataLoader.GetType().GetProperties();
                    }

                    foreach (PropertyInfo property in properties)
                    {
                        if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                        {
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            FwBusinessLogicFieldDefinition field = new FwBusinessLogicFieldDefinition();
                            field.Name = property.Name;
                            field.DataType = sqlDataFieldAttribute.ModelType;
                            fields.Add(field);
                        }
                    }
                }
                return fields;
            }
        }

        //------------------------------------------------------------------------------------
        public virtual T MakeCopy<T>()
        {
            T newLogic = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(newLogic, property.GetValue(this));
                }
            }
            return newLogic;
        }
        //------------------------------------------------------------------------------------
        public bool ShouldSerialize_Fields()
        {
            // only serialize the _Fields property when this IsEmptyObject
            return IsEmptyObject;
        }
        //------------------------------------------------------------------------------------
    }

    public class FwBusinessLogicList : List<FwBusinessLogic> { }

}

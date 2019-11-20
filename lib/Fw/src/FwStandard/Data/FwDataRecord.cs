using AutoMapper;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using shortid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.Data
{

    public class BeforeBrowseEventArgs : EventArgs
    {
        public BrowseRequest Request { get; set; }
    }
    public class AfterBrowseEventArgs : EventArgs
    {
        public BrowseRequest Request { get; set; }
        public FwJsonDataTable DataTable { get; set; }
    }

    public class AfterLoadEventArgs : EventArgs
    {
        public dynamic Record { get; set; }
    }


    public class FwDataRecord : FwBaseRecord
    {
        [JsonIgnore]
        public FwUserSession UserSession = null;
        [JsonIgnore]
        public FwApplicationConfig AppConfig { get; set; }
        public FwCustomValues _Custom = new FwCustomValues(); // for mapping back to BusinessLogic class
        protected bool useWithNoLock = true;

        [JsonIgnore]
        public bool ForceSave { get; set; } = false;

        [JsonIgnore]
        public bool ReloadOnSave { get; set; }

        public event EventHandler<BeforeBrowseEventArgs> BeforeBrowse;
        public event EventHandler<AfterBrowseEventArgs> AfterBrowse;
        public event EventHandler<AfterLoadEventArgs> AfterLoad;

        public delegate void BeforeBrowseEventHandler(BeforeBrowseEventArgs e);
        public delegate void AfterBrowseEventHandler(AfterBrowseEventArgs e);
        public delegate void AfterLoadEventHandler(AfterLoadEventArgs e);

        protected virtual void OnBeforeBrowse(BeforeBrowseEventArgs e)
        {
            BeforeBrowse?.Invoke(this, e);
        }
        protected virtual void OnAfterBrowse(AfterBrowseEventArgs e)
        {
            AfterBrowse?.Invoke(this, e);
        }

        //note: this event only executes when loading a single object. 
        //      does not execute on GetMany loads or Browse loads
        protected virtual void OnAfterLoad(AfterLoadEventArgs e)
        {
            AfterLoad?.Invoke(this, e);
        }

        //------------------------------------------------------------------------------------
        public FwDataRecord() : base() { }
        //------------------------------------------------------------------------------------

        public void SetDependencies(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            AppConfig = appConfig;
            UserSession = userSession;
        }
        //------------------------------------------------------------------------------------


        [JsonIgnore]
        public virtual string TableName
        {
            get
            {
                //return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().Table;
                //justin 09/25/2018 make sure the Table attribute is defined before reading the value
                string tableName = null;
                if (!string.IsNullOrEmpty(OverrideTableName))
                {
                    tableName = OverrideTableName;
                }
                else
                {
                    FwSqlTableAttribute tableAttribute = (FwSqlTableAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(FwSqlTableAttribute));
                    if (tableAttribute != null)
                    {
                        tableName = this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().Table;
                    }
                }
                return tableName;
            }
        }
        //------------------------------------------------------------------------------------
        protected string TableAlias { get; set; } = "t";
        //------------------------------------------------------------------------------------
        protected string OverrideTableName { get; set; } = "";
        //------------------------------------------------------------------------------------
        protected virtual int PrimaryKeyCount
        {
            get
            {
                List<PropertyInfo> keys = GetPrimaryKeyProperties();
                int keyCount = keys.Count;
                return keyCount;
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual List<PropertyInfo> GetPrimaryKeyProperties()
        {
            List<PropertyInfo> primaryKeyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                        {
                            FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                            if ((dataFieldAttribute.IsPrimaryKey) || (dataFieldAttribute.IsPrimaryKeyOptional))
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
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                        {
                            FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                            if ((!dataFieldAttribute.IsPrimaryKeyOptional) && (!dataFieldAttribute.Identity))
                            {
                                object propertyValue = property.GetValue(this);
                                if (propertyValue == null)
                                {
                                    hasPrimaryKeysSet = false;
                                }
                                else if (propertyValue is string)
                                {
                                    hasPrimaryKeysSet &= (propertyValue as string).Length > 0;
                                }
                                else if (propertyValue is Int32)
                                {
                                    hasPrimaryKeysSet &= (((Int32)propertyValue) != 0);
                                }
                                else
                                {
                                    throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.AllPrimaryKeysHaveValues]");
                                }
                            }
                        }
                    }
                }
                return hasPrimaryKeysSet;
            }
        }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public virtual bool NoPrimaryKeysHaveValues
        {
            get
            {
                List<PropertyInfo> primaryKeys = GetPrimaryKeyProperties();
                bool noPrimaryKeysHaveValues = true;
                foreach (PropertyInfo property in primaryKeys)
                {
                    bool isPrimaryKeyOptional = false;
                    if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                    {
                        foreach (Attribute attribute in property.GetCustomAttributes())
                        {
                            if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                            {
                                FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                                if (dataFieldAttribute.IsPrimaryKeyOptional)
                                {
                                    isPrimaryKeyOptional = true;
                                }
                            }
                        }
                    }

                    if (!isPrimaryKeyOptional)
                    {
                        object propertyValue = property.GetValue(this);
                        if (propertyValue == null)
                        {
                            noPrimaryKeysHaveValues = true;
                        }
                        else if (propertyValue is string)
                        {
                            noPrimaryKeysHaveValues &= (propertyValue as string).Length == 0;
                        }
                        else if (propertyValue is Int32)
                        {
                            noPrimaryKeysHaveValues &= (((Int32)propertyValue) == 0);
                        }
                        else
                        {
                            throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.NoPrimaryKeysHaveValues]");
                        }
                    }

                }
                return noPrimaryKeysHaveValues;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual bool IsModified(FwDataRecord original)
        {
            bool isModified = false;
            if (original == null)
            {
                isModified = true;
            }
            else
            {
                PropertyInfo[] properties = this.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                    {
                        bool checkProperty = true;
                        if (checkProperty)
                        {
                            foreach (Attribute attribute in property.GetCustomAttributes())
                            {
                                if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                                {
                                    FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                                    if ((dataFieldAttribute.IsPrimaryKey) || (dataFieldAttribute.IsPrimaryKeyOptional))
                                    {
                                        checkProperty = false;
                                    }
                                }
                            }
                        }

                        if (checkProperty)
                        {
                            if (property.Name.ToLower().Equals("datestamp"))
                            {
                                checkProperty = false;
                            }
                        }

                        if (checkProperty)
                        {
                            object newValue = property.GetValue(this);
                            if (newValue != null)
                            {
                                object oldValue = original.GetType().GetProperty(property.Name).GetValue(original, null);

                                isModified = ((oldValue == null) || (!newValue.Equals(oldValue)));
                                if (isModified)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return isModified;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<FwValidateResult> AllFieldsValidAsync(TDataRecordSaveMode saveMode, FwValidateResult result)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if ((result.IsValid) && (property.IsDefined(typeof(FwSqlDataFieldAttribute))))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if ((result.IsValid) && (attribute.GetType() == typeof(FwSqlDataFieldAttribute)))
                        {
                            FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                            object propertyValue = property.GetValue(this);
                            if ((result.IsValid) && (dataFieldAttribute.Required))
                            {
                                if ((result.IsValid) && (saveMode == TDataRecordSaveMode.smInsert))
                                {
                                    if (propertyValue == null)
                                    {
                                        result.IsValid = false;
                                        result.ValidateMsg = property.Name + " is required.";
                                    }
                                }
                                if ((result.IsValid) && (propertyValue != null))
                                {
                                    if (propertyValue is string)
                                    {
                                        result.IsValid = ((propertyValue as string).Length > 0);
                                        if (!result.IsValid)
                                        {
                                            result.ValidateMsg = property.Name + " cannot be blank.";
                                        }
                                    }
                                    else if (propertyValue is Int32)
                                    {
                                        result.IsValid = (((Int32)propertyValue) != 0);
                                        if (!result.IsValid)
                                        {
                                            result.ValidateMsg = property.Name + " cannot be zero.";
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("A test for \"Required\" of property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.AllFieldsValid]");
                                    }
                                }
                            }

                            if ((result.IsValid) && (dataFieldAttribute.MaxLength > 0))
                            {
                                if (propertyValue != null)
                                {
                                    if (propertyValue is bool)
                                    {
                                        result.IsValid = true;
                                    }
                                    else if (propertyValue is string)
                                    {
                                        result.IsValid = ((propertyValue as string).Length <= dataFieldAttribute.MaxLength);
                                        if (!result.IsValid)
                                        {
                                            result.ValidateMsg = property.Name + " cannot be longer than " + dataFieldAttribute.MaxLength.ToString() + " characters.";
                                        }
                                    }
                                    else if (propertyValue is int)
                                    {
                                        result.IsValid = true;
                                    }
                                    else
                                    {
                                        throw new Exception("A test for \"MaxLength\" property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.AllFieldsValid]");
                                    }
                                }
                            }

                        }
                    }
                }
            }
            await Task.CompletedTask;
            return result;
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task SetPrimaryKeyIdsForInsertAsync(FwSqlConnection conn)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
            {
                foreach (Attribute attribute in primaryKeyProperty.GetCustomAttributes())
                {
                    if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                    {
                        FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                        if ((!dataFieldAttribute.IsPrimaryKeyOptional) && (!dataFieldAttribute.Identity))
                        {
                            string id = await FwSqlData.GetNextIdAsync(conn, AppConfig.DatabaseSettings);
                            if (primaryKeyProperty.GetValue(this) is string)
                            {
                                primaryKeyProperties[0].SetValue(this, id);
                            }
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select");
            PropertyInfo[] properties = this.GetType().GetTypeInfo().GetProperties();
            int colNo = 0;
            Dictionary<string, string> columns = new Dictionary<string, string>();
            string fullFieldName = "";

            int maxFieldNameLength = 0;
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                    string sqlColumnName = property.Name;
                    columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
                    if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                    {
                        sqlColumnName = sqlDataFieldAttribute.ColumnName;
                    }
                    fullFieldName = "[" + TableAlias + "].[" + sqlColumnName + "]";
                    maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
                }
            }


            colNo = 0;
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                    string sqlColumnName = property.Name;
                    if (!string.IsNullOrEmpty(sqlDataFieldAttribute.CalculatedColumnSql))
                    {
                        columns[sqlColumnName] = sqlDataFieldAttribute.CalculatedColumnSql;
                        fullFieldName = sqlDataFieldAttribute.CalculatedColumnSql;
                    }
                    else
                    {
                        columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
                        fullFieldName = "[" + TableAlias + "].[" + sqlDataFieldAttribute.ColumnName + "]";
                    }
                    string prefix = "";
                    if (colNo > 0)
                    {
                        prefix = ",\n      ";
                    }
                    qry.AddColumn(property.Name, property.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                    sb.Append(prefix + " " + fullFieldName.PadRight(maxFieldNameLength, ' ') + " as [" + property.Name + "]");

                    colNo++;
                }
            }

            List<FwCustomTable> customTables = new List<FwCustomTable>();
            if ((customFields != null) && (customFields.Count > 0))
            {
                int customTableIndex = 1;
                int customFieldIndex = 1;
                sb.Append(",\n");
                sb.Append("       --//---------- begin custom fields ------------------\n");

                maxFieldNameLength = 0;
                foreach (FwCustomField customField in customFields)
                {
                    fullFieldName = "[" + customField.FieldName + "]";
                    maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
                }

                colNo = 0;
                foreach (FwCustomField customField in customFields)
                {
                    //columns[customField.FieldName] = customField.FieldName;
                    bool customTableInQuery = false;
                    string customTableAlias = "";
                    foreach (FwCustomTable customTable in customTables)
                    {
                        if (customTable.TableName.Equals(customField.CustomTableName))
                        {
                            customTableInQuery = true;
                            customTableAlias = customTable.Alias;
                            break;
                        }
                    }
                    if (!customTableInQuery)
                    {
                        customTableAlias = "customtable" + customTableIndex.ToString().PadLeft(2, '0');
                        customTables.Add(new FwCustomTable(customField.CustomTableName, customTableAlias));
                        customTableIndex++;
                    }
                    string customSqlFieldName = "[" + customTableAlias + "].[" + customField.CustomFieldName + "]";
                    columns[customField.FieldName] = customSqlFieldName;

                    qry.AddColumn(customField.FieldName, customField.FieldName, customField.FwDataType, true, false, false);
                    if (colNo > 0)
                    {
                        sb.Append(",\n");
                    }
                    sb.Append("       ");
                    fullFieldName = "[" + customField.FieldName + "]";
                    sb.Append(fullFieldName.PadRight(maxFieldNameLength, ' ') + " = " + customSqlFieldName);

                    customFieldIndex++;
                    colNo++;
                }
                sb.Append("\n       --//---------- end custom fields --------------------");
            }
            select.Add(sb.ToString());

            string withNoLock = string.Empty;
            if (useWithNoLock)
            {
                withNoLock = " with (nolock)";
            }
            select.Add("from " + TableName + " [" + TableAlias + "]" + withNoLock);

            if ((customFields != null) && (customFields.Count > 0))
            {
                List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();

                select.Add("           --//---------- begin custom tables ------------------");
                foreach (FwCustomTable customTable in customTables)
                {
                    select.Add("           left outer join [" + customTable.TableName + "] [" + customTable.Alias + "] with (nolock) on ");
                    select.Add("                       ( ");

                    int k = 1;
                    foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = primaryKeyProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        string sqlColumnName = primaryKeyProperty.Name;
                        if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                        {
                            sqlColumnName = sqlDataFieldAttribute.ColumnName;
                        }
                        string customUniqueIdField = "uniqueid" + k.ToString().PadLeft(2, '0');
                        select.Add("                       [" + TableAlias + "].[" + sqlColumnName + "] = [" + customTable.Alias + "].[" + customUniqueIdField + "]");
                        if (k < primaryKeyProperties.Count)
                        {
                            select.Add(" and ");
                        }
                        k++;
                    }
                    select.Add("                       ) ");
                }
                select.Add("           --//---------- end custom tables --------------------");
            }

            if (request != null)
            {
                if (request.searchfields.Count > 0)
                {
                    select.Add("where");
                    for (int i = 0; i < request.searchfields.Count; i++)
                    {
                        if (!columns.ContainsKey(request.searchfields[i]))
                        {
                            throw new Exception("Searching is not supported on " + request.searchfields[i]);
                        }
                        string conditionConjunction = string.Empty;
                        if (i > 0)
                        {
                            conditionConjunction = "  and ";
                        }
                        //string parameterName = "@" + columns[request.searchfields[i]].Replace('.', '_');
                        //string parameterName = "@" + columns[request.searchfields[i]].Replace('.', '_').Replace("[", "").Replace("]", ""); // remove square brackets to work with custom fields
                        //string parameterName = "@" + columns[request.searchfields[i]].Replace('.', '_').Replace("[", "").Replace("]", "") + i.ToString(); // remove square brackets to work with custom fields
                        //string parameterName = "@" + request.searchfields[i].Replace('.', '_').Replace("[", "").Replace("]", "") + i.ToString(); // remove square brackets to work with custom fields
                        string parameterName = "@" + request.searchfields[i].Replace('.', '_').Replace("[", "").Replace("]", "").Replace(" ", "_") + i.ToString(); // remove square brackets to work with custom fields

                        bool doUpper = true;
                        string searchField = columns[request.searchfields[i]];
                        string searchFieldOperator = request.searchfieldoperators[i];
                        string searchFieldValue = request.searchfieldvalues[i];
                        string searchFieldType = "";
                        char searchSeparator = ',';

                        if (request.searchfieldtypes.Count > i)
                        {
                            searchFieldType = request.searchfieldtypes[i];
                        }
                        if (request.searchseparators.Count > i)
                        {
                            searchSeparator = request.searchseparators[i][0];
                            if (searchSeparator.Equals(' '))
                            {
                                searchSeparator = ',';
                            }
                        }
                        if (request.searchconjunctions.Count > i)
                        {
                            conditionConjunction = " " + request.searchconjunctions[i] + " ";
                        }

                        if (searchField.ToLower().Equals("inactive"))
                        {
                            doUpper = false;
                        }
                        if (searchField.EndsWith("no"))
                        {
                            doUpper = false;
                        }
                        if ((searchFieldType.ToLower().Equals("date")) || (searchFieldType.ToLower().Equals("number")) || (searchFieldType.ToLower().Equals("integer")) || (searchFieldType.ToLower().Equals("boolean")) || (searchFieldType.ToLower().Equals("true/false")))
                        {
                            doUpper = false;
                            if (searchFieldOperator.ToLower().Equals("like"))
                            {
                                searchFieldOperator = "=";
                            }
                        }

                        if ((searchFieldType.ToLower().Equals("boolean")) || (searchFieldType.ToLower().Equals("true/false")))
                        {
                            if ((searchFieldValue.ToLower().Equals("true")) || (searchFieldValue.ToLower().Equals("t")))
                            {
                                searchFieldValue = "T";
                            }
                            else
                            {
                                searchFieldValue = "F";
                            }
                        }


                        if (doUpper)
                        {
                            searchField = "upper(" + searchField + ")";
                        }

                        string searchcondition;
                        if (searchFieldOperator.ToLower().Equals("like"))
                        {
                            if (searchFieldValue.IndexOf(searchSeparator) >= 0)
                            {
                                searchcondition = conditionConjunction + "(";
                                int partialValueCounter = 0;
                                foreach (string partialValue in searchFieldValue.Split(searchSeparator))
                                {
                                    string partialParameterName = parameterName + partialValueCounter.ToString();
                                    if (partialValueCounter > 0)
                                    {
                                        if (!string.IsNullOrEmpty(request.searchcondition[i]))
                                        {
                                            searchcondition = searchcondition + " " + request.searchcondition[i] + " ";
                                        }
                                        else
                                        {
                                            searchcondition = searchcondition + " and ";
                                        }
                                    }
                                    searchcondition = searchcondition + searchField + " like " + partialParameterName;
                                    select.AddParameter(partialParameterName, "%" + partialValue.ToUpper() + "%");
                                    partialValueCounter++;
                                }
                                searchcondition = searchcondition + ")";
                            }
                            else
                            {
                                searchcondition = conditionConjunction + searchField + " like " + parameterName;
                                select.AddParameter(parameterName, "%" + searchFieldValue.ToUpper() + "%");
                            }

                        }
                        else if (searchFieldOperator.ToLower().Equals("startswith"))
                        {
                            searchcondition = conditionConjunction + searchField + " like " + parameterName;
                            select.AddParameter(parameterName, searchFieldValue.ToUpper() + "%");
                        }
                        else if (searchFieldOperator.ToLower().Equals("endswith"))
                        {
                            searchcondition = conditionConjunction + searchField + " like " + parameterName;
                            select.AddParameter(parameterName, "%" + searchFieldValue.ToUpper());
                        }
                        else if (searchFieldOperator.ToLower().Equals("doesnotcontain"))
                        {
                            searchcondition = conditionConjunction + searchField + " not like " + parameterName;
                            select.AddParameter(parameterName, "%" + searchFieldValue.ToUpper() + "%");
                        }
                        else if (searchFieldOperator.Equals("=") || searchFieldOperator.Equals("<>") || searchFieldOperator.Equals("<") || searchFieldOperator.Equals(">") || searchFieldOperator.Equals("<=") || searchFieldOperator.Equals(">="))
                        {
                            searchcondition = conditionConjunction + searchField + " " + searchFieldOperator + " " + parameterName;
                            if (searchFieldType.ToLower().Equals("date"))
                            {
                                select.AddParameter(parameterName, SqlDbType.Date, ParameterDirection.Input, 0, FwConvert.ToDateTime(searchFieldValue));
                            }
                            else if (searchFieldType.ToLower().Equals("number") || searchFieldType.ToLower().Equals("integer") || searchFieldType.ToLower().Equals("decimal"))
                            {
                                select.AddParameter(parameterName, SqlDbType.Float, ParameterDirection.Input, 0, searchFieldValue);
                            }
                            else
                            {
                                select.AddParameter(parameterName, searchFieldValue.ToUpper());
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid searchfieldoperator: " + searchFieldOperator);
                        }
                        select.Add(searchcondition);
                    }
                }

                if (request.orderby.Trim().Length > 0)
                {
                    // validate the user supplied order by expression to prevent SQL Injection attacks
                    List<string> tokens = new List<string>(request.orderby.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries));
                    foreach (string token in tokens)
                    {
                        if (!columns.ContainsKey(token) && token != "asc" && token != "desc")
                        {
                            throw new Exception("Order by expession is not permitted.");
                        }
                    }
                    if (request.orderby.Trim().Length == 0)
                    {
                        throw new Exception("At least one column must have a sort order.");
                    }

                    // need to convert the columns from the model names to sql names
                    StringBuilder orderbyBuilder = new StringBuilder();
                    string orderby = request.orderby.Trim();
                    StringBuilder tokenBuilder = new StringBuilder();
                    for (int index = 0; index < orderby.Length; index++)
                    {
                        char currentChar = orderby[index];

                        switch (currentChar)
                        {
                            case ' ':
                            case ',':
                                string token = tokenBuilder.ToString();
                                tokenBuilder = new StringBuilder();
                                switch (token)
                                {
                                    case "": break; //jh 09/29/2017 allows for spaces between field names in the "Order By" list
                                    case "asc":
                                    case "desc":
                                        orderbyBuilder.Append(token);
                                        orderbyBuilder.Append(currentChar);
                                        break;
                                    default:
                                        if (columns.ContainsKey(token))
                                        {
                                            // rownumber over paging needs to use column alias name if it has one
                                            if (request.pagesize > 0)
                                            {
                                                orderbyBuilder.Append(token);
                                                orderbyBuilder.Append(currentChar);
                                            }
                                            else
                                            {
                                                string sqlColumnName = columns[token];
                                                orderbyBuilder.Append(sqlColumnName);
                                                orderbyBuilder.Append(currentChar);
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Invalid token " + token + " in order by.");
                                        }
                                        break;
                                }
                                break;
                            default:
                                tokenBuilder.Append(currentChar);
                                break;
                        }
                    }
                    // add the token in the buffer if one exists
                    if (tokenBuilder.Length > 0)
                    {
                        string token = tokenBuilder.ToString();
                        if (columns.ContainsKey(token) || token == "asc" || token == "desc")
                        {
                            // rownumber over paging needs to use column alias name if it has one
                            if (request.pagesize > 0)
                            {
                                orderbyBuilder.Append(token);
                            }
                            else
                            {
                                string sqlColumnName = columns[token];
                                orderbyBuilder.Append(sqlColumnName);
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid token " + token + " in order by.");
                        }
                    }
                    select.Add("order by " + orderbyBuilder.ToString());
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual void SetBaseGetManyQuery(FwSqlSelect select, FwSqlCommand qry, GetManyRequest request, FwCustomFields customFields = null)
        {
            if (select == null) throw new ArgumentException("Argument 'select' cannot be null.");
            if (qry == null) throw new ArgumentException("Argument 'qry' cannot be null.");
            if (request == null) throw new ArgumentException("Argument 'request' cannot be null.");
            request.Parse();
            select.EnablePaging = request.pagesize > 0;
            select.PageNo = request.pageno;
            select.PageSize = request.pagesize;
            StringBuilder sb = new StringBuilder();
            sb.Append("select");

            // Get all the Properties of this Record
            PropertyInfo[] recordProperties = this.GetType().GetTypeInfo().GetProperties();
            int colNo = 0;
            Dictionary<string, string> columns = new Dictionary<string, string>();
            string fullFieldName = "";

            // compute the maxFieldNameLength for formatting the SQL statement
            int maxFieldNameLength = 0;
            foreach (PropertyInfo property in recordProperties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                    string sqlColumnName = property.Name;
                    columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
                    if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                    {
                        sqlColumnName = sqlDataFieldAttribute.ColumnName;
                    }
                    fullFieldName = "[" + TableAlias + "].[" + sqlColumnName + "]";
                    maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
                }
            }


            colNo = 0;
            foreach (PropertyInfo property in recordProperties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                    string sqlColumnName = property.Name;
                    if (!string.IsNullOrEmpty(sqlDataFieldAttribute.CalculatedColumnSql))
                    {
                        columns[sqlColumnName] = sqlDataFieldAttribute.CalculatedColumnSql;
                        fullFieldName = sqlDataFieldAttribute.CalculatedColumnSql;
                    }
                    else
                    {
                        columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
                        fullFieldName = "[" + TableAlias + "].[" + sqlDataFieldAttribute.ColumnName + "]";
                    }
                    string prefix = "";
                    if (colNo > 0)
                    {
                        prefix = ",\n      ";
                    }
                    qry.AddColumn(property.Name, property.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                    sb.Append(prefix + " " + fullFieldName.PadRight(maxFieldNameLength, ' ') + " as [" + property.Name + "]");

                    colNo++;
                }
            }

            List<FwCustomTable> customTables = new List<FwCustomTable>();
            if ((customFields != null) && (customFields.Count > 0))
            {
                int customTableIndex = 1;
                int customFieldIndex = 1;
                sb.Append(",\n");
                sb.Append("       --//---------- begin custom fields ------------------\n");

                maxFieldNameLength = 0;
                foreach (FwCustomField customField in customFields)
                {
                    fullFieldName = "[" + customField.FieldName + "]";
                    maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
                }

                colNo = 0;
                foreach (FwCustomField customField in customFields)
                {
                    //columns[customField.FieldName] = customField.FieldName;
                    bool customTableInQuery = false;
                    string customTableAlias = "";
                    foreach (FwCustomTable customTable in customTables)
                    {
                        if (customTable.TableName.Equals(customField.CustomTableName))
                        {
                            customTableInQuery = true;
                            customTableAlias = customTable.Alias;
                            break;
                        }
                    }
                    if (!customTableInQuery)
                    {
                        customTableAlias = "customtable" + customTableIndex.ToString().PadLeft(2, '0');
                        customTables.Add(new FwCustomTable(customField.CustomTableName, customTableAlias));
                        customTableIndex++;
                    }
                    string customSqlFieldName = "[" + customTableAlias + "].[" + customField.CustomFieldName + "]";
                    columns[customField.FieldName] = customSqlFieldName;

                    qry.AddColumn(customField.FieldName, customField.FieldName, FwDataTypes.Text, true, false, false);
                    if (colNo > 0)
                    {
                        sb.Append(",\n");
                    }
                    sb.Append("       ");
                    fullFieldName = "[" + customField.FieldName + "]";
                    sb.Append(fullFieldName.PadRight(maxFieldNameLength, ' ') + " = " + customSqlFieldName);

                    customFieldIndex++;
                    colNo++;
                }
                sb.Append("\n       --//---------- end custom fields --------------------");
            }
            select.Add(sb.ToString());

            string withNoLock = string.Empty;
            if (useWithNoLock)
            {
                withNoLock = " with (nolock)";
            }
            select.Add("from " + TableName + " [" + TableAlias + "]" + withNoLock);

            if ((customFields != null) && (customFields.Count > 0))
            {
                List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();

                select.Add("           --//---------- begin custom tables ------------------");
                foreach (FwCustomTable customTable in customTables)
                {
                    select.Add("           left outer join [" + customTable.TableName + "] [" + customTable.Alias + "] with (nolock) on ");
                    select.Add("                       ( ");

                    int k = 1;
                    foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = primaryKeyProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        string sqlColumnName = primaryKeyProperty.Name;
                        if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                        {
                            sqlColumnName = sqlDataFieldAttribute.ColumnName;
                        }
                        string customUniqueIdField = "uniqueid" + k.ToString().PadLeft(2, '0');
                        select.Add("                       [" + TableAlias + "].[" + sqlColumnName + "] = [" + customTable.Alias + "].[" + customUniqueIdField + "]");
                        if (k < primaryKeyProperties.Count)
                        {
                            select.Add(" and ");
                        }
                        k++;
                    }
                    select.Add("                       ) ");
                }
                select.Add("           --//---------- end custom tables --------------------");
            }

            if (request.filters.Count > 0)
            {
                var needsWhere = true;
                var conjunction = string.Empty;
                foreach (var filter in request.filters)
                {
                    var propInfo = recordProperties.Where<PropertyInfo>(p => p.Name == filter.Key).FirstOrDefault<PropertyInfo>();
                    if (filter.Value.ValidateFilter && propInfo == null)
                    {
                        throw new ArgumentException($"Invalid filter: '{filter.Key}'.");
                    }
                    else
                    {
                        var sqlFieldName = filter.Value.FieldName;
                        var sqlDataFieldAttribute = propInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        sqlFieldName = sqlDataFieldAttribute.ColumnName;
                        var parameterName = "@" + sqlFieldName + ShortId.Generate(true, false, 7);
                        var fieldSqlValue = filter.Value.FieldValue;
                        switch (sqlDataFieldAttribute.ModelType)
                        {
                            case FwDataTypes.Boolean:
                                switch (filter.Value.FieldValue.ToLower())
                                {
                                    case "true":
                                        fieldSqlValue = "T";
                                        break;
                                    case "false":
                                        fieldSqlValue = "F";
                                        break;
                                    default:
                                        throw new ArgumentException($"An invalid filter value: '{filter.Value.FieldValue}' was supplied for the field: {filter.Value.FieldName}");
                                }
                                break;
                            default:
                                break;
                        }
                        switch (filter.Value.ComparisonOperator)
                        {
                            case "eq":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} = {parameterName}");
                                select.AddParameter(parameterName, fieldSqlValue);
                                break;
                            case "ne":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} <> {parameterName}");
                                select.AddParameter(parameterName, fieldSqlValue);
                                break;
                            case "in":
                            case "ni":
                                {
                                    string filterValue = fieldSqlValue;
                                    bool inAQuote = false;
                                    bool inAFilterValue = false;
                                    List<string> filterValues = new List<string>();
                                    StringBuilder currentFilter = new StringBuilder();
                                    for (int i = 0; i < filterValue.Length; i++)
                                    {
                                        bool isAtTheEndOfTheFilter = (i >= filterValue.Length - 1);
                                        char c = filterValue[i];
                                        if (c == ',' && !inAQuote && inAFilterValue)
                                        {
                                            filterValues.Add(currentFilter.ToString());
                                            currentFilter = new StringBuilder();
                                            inAFilterValue = false;
                                        }
                                        else if (c == ',' && !inAQuote && !inAFilterValue)
                                        {

                                        }
                                        else
                                        {
                                            if (c == '\"')
                                            {
                                                if (inAFilterValue)
                                                {
                                                    if (inAQuote)
                                                    {
                                                        if (!isAtTheEndOfTheFilter)
                                                        {
                                                            //if (filterValue[i + 1] == '\"')
                                                            //{
                                                            //    currentFilter.Append(c);
                                                            //}
                                                            if (filterValue[i + 1] == ',')
                                                            {
                                                                filterValues.Add(currentFilter.ToString());
                                                                inAQuote = false;
                                                                inAFilterValue = false;
                                                                currentFilter = new StringBuilder();
                                                                i++;
                                                            }
                                                            else
                                                            {
                                                                inAQuote = false;
                                                                inAFilterValue = false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i + 1}) must be escaped with a double quote.");
                                                        //if (isAtTheEndOfTheFilter)
                                                        //{
                                                        //    throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i+1}) must be escaped with a double quote.");
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (filterValue[i + 1] == '\"')
                                                        //    {
                                                        //        currentFilter.Append(c);
                                                        //        i++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i+1}) must be escaped with a double quote.");
                                                        //    }
                                                        //}
                                                    }
                                                }
                                                else if (!inAFilterValue && !inAQuote && filterValue[i + 1] == '\"')
                                                {
                                                    currentFilter.Append(c);
                                                    i++;
                                                    inAFilterValue = true;
                                                }
                                                else
                                                {
                                                    inAQuote = true;
                                                    inAFilterValue = true;
                                                }
                                            }
                                            else if (c == '\\')
                                            {
                                                if (!isAtTheEndOfTheFilter)
                                                {
                                                    if (filterValue[i + 1] == '\"')
                                                    {
                                                        currentFilter.Append(c);
                                                        currentFilter.Append(filterValue[i + 1]);
                                                        i += 2;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                inAFilterValue = true;
                                                currentFilter.Append(c);
                                            }
                                            if (isAtTheEndOfTheFilter)
                                            {
                                                inAQuote = false;
                                                inAFilterValue = false;
                                                filterValues.Add(currentFilter.ToString());
                                                currentFilter = new StringBuilder();
                                                break;
                                            }
                                        }
                                    }
                                    if (filterValues.Count > 0)
                                    {
                                        if (needsWhere)
                                        {
                                            select.Add("where");
                                            needsWhere = false;
                                        }
                                        StringBuilder filterExpression = new StringBuilder();
                                        filterExpression.Append($"  {conjunction}{sqlFieldName} ");
                                        if (filter.Value.ComparisonOperator == "ni")
                                        {
                                            filterExpression.Append("not ");
                                        }
                                        filterExpression.Append("in (");
                                        bool firstParameter = true;
                                        foreach (var parameterValue in filterValues)
                                        {
                                            parameterName = '@' + sqlFieldName + ShortId.Generate(true, false, 7);
                                            if (!firstParameter)
                                            {
                                                filterExpression.Append(", ");
                                            }
                                            filterExpression.Append(parameterName);
                                            select.AddParameter(parameterName, parameterValue);
                                            firstParameter = false;
                                        }
                                        filterExpression.Append(")");
                                        select.Add(filterExpression.ToString());
                                    }
                                    break;
                                }
                            case "sw":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}");
                                break;
                            case "ew":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}");
                                break;
                            case "co":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                            case "dnc":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} not like {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                            case "gt":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} > {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                            case "gte":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} >= {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                            case "lt":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} < {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                            case "lte":
                                if (needsWhere)
                                {
                                    select.Add("where");
                                    needsWhere = false;
                                }
                                select.Add($"  {conjunction}{sqlFieldName} <= {parameterName}");
                                select.AddParameter(parameterName, $"%{fieldSqlValue}%");
                                break;
                        }
                        if (conjunction == string.Empty)
                        {
                            conjunction = " and ";
                        }
                    }
                }
            }

            if (request.sort.Length > 0)
            {
                var sqlSortExpression = string.Empty;
                var sortArray = request.sort.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sortExpression in sortArray)
                {
                    var sortExpressionParts = sortExpression.Split(new char[] { ':' });

                    var sortField = "";
                    var sortDirection = "";
                    if (sortExpressionParts.Length > 0)
                    {
                        sortField = sortExpressionParts[0];
                    }
                    if (sortExpressionParts.Length > 1)
                    {
                        // validate the user submitted data to protect against SQL injection attacks
                        if (sortExpressionParts[1] == "asc" || sortExpressionParts[1] == "desc")
                        {
                            sortDirection = " " + sortExpressionParts[1];
                        }
                        else if (sortExpressionParts[1] == "")
                        {
                            // do nothing
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid sort direction: '{sortExpressionParts[1]}'");
                        }
                    }

                    // validates and translate the Field Name
                    var recordPropInfo = recordProperties.Where<PropertyInfo>(p => p.Name == sortField).FirstOrDefault<PropertyInfo>();
                    var requestPropInfo = request.GetType().GetProperties()
                        .Where<PropertyInfo>(
                            p => p.Name == sortField &&
                            p.GetCustomAttribute<GetManyRequestPropertyAttribute>() != null &&
                            p.GetCustomAttribute<GetManyRequestPropertyAttribute>().EnableSorting == true)
                        .FirstOrDefault<PropertyInfo>();
                    if (recordPropInfo == null && requestPropInfo == null)
                    {
                        throw new ArgumentException($"Invalid column name: '{sortField}' in sort expression.");
                    }
                    else
                    {
                        var sqlDataFieldAttribute = recordPropInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlSortExpression.Length > 0)
                        {
                            sqlSortExpression += ", ";
                        }
                        sqlSortExpression += sqlDataFieldAttribute.ColumnName + sortDirection;
                    }
                }
                select.Add("order by " + sqlSortExpression);
            }
        }
        //------------------------------------------------------------------------------------
        public virtual FwSqlConnection GetDatabaseConnection()
        {
            return new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;

            BeforeBrowseEventArgs beforeBrowseArgs = new BeforeBrowseEventArgs();
            beforeBrowseArgs.Request = request;
            BeforeBrowse?.Invoke(this, beforeBrowseArgs);

            if (request.emptyobject)
            {
                dt = new FwJsonDataTable();
                PropertyInfo[] propertyInfos = this.GetType().GetProperties();
                int i = 0;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute)))
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        dt.Columns.Add(new FwJsonDataTableColumn(propertyInfo.Name, propertyInfo.Name, sqlDataFieldAttribute.ModelType));
                        dt.ColumnIndex[propertyInfo.Name] = i;
                        i++;
                    }
                }

                if ((customFields != null) && (customFields.Count > 0))
                {
                    foreach (FwCustomField customField in customFields)
                    {
                        dt.Columns.Add(new FwJsonDataTableColumn(customField.FieldName, customField.FieldName, customField.FwDataType));
                        dt.ColumnIndex[customField.FieldName] = i;
                        i++;
                    }
                }
            }
            else
            {
                //using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                using (FwSqlConnection conn = GetDatabaseConnection())
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.EnablePaging = request.pageno != 0 || request.pagesize > 0;
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Top = request.top;
                    select.TotalFields = request.totalfields;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        SetBaseSelectQuery(select, qry, customFields: customFields, request: request);
                        dt = await qry.QueryToFwJsonTableAsync(select, false);
                    }
                }
            }

            AfterBrowseEventArgs afterBrowseArgs = new AfterBrowseEventArgs();
            afterBrowseArgs.Request = request;
            afterBrowseArgs.DataTable = dt;
            AfterBrowse?.Invoke(this, afterBrowseArgs);

            return dt;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequest request, FwCustomFields customFields = null)
        {
            using (FwSqlConnection conn = GetDatabaseConnection())
            {
                FwSqlSelect select = new FwSqlSelect();
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry, customFields: customFields, request: request);
                    select.SetQuery(qry);
                    MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(this.GetType());
                    dynamic result = generic.Invoke(qry, new object[] { customFields });
                    dynamic records = await result;
                    return records;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<GetManyResponse<T>> GetManyAsync<T>(GetManyRequest request, FwCustomFields customFields = null, Func<FwSqlSelect, Task> beforeExecuteQuery = null)
        {
            //using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            using (FwSqlConnection conn = GetDatabaseConnection())
            {
                FwSqlSelect select = new FwSqlSelect();
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    SetBaseGetManyQuery(select, qry, request, customFields);
                    select.Parse();
                    if (beforeExecuteQuery != null)
                    {
                        await beforeExecuteQuery(select);
                    }
                    select.SetQuery(qry);

                    // call the generic method SelectAsync<T> on the qry using reflection
                    MethodInfo method = typeof(FwSqlCommand).GetMethod("GetManyAsync");
                    MethodInfo generic = method.MakeGenericMethod(this.GetType());
                    Task<GetManyResponse<T>> result = (Task<GetManyResponse<T>>)generic.Invoke(qry, new object[] { customFields });
                    var response = await result;
                    return response;
                }
            }

        }
        //------------------------------------------------------------------------------------
        public virtual async Task<dynamic> GetAsync<T>(object[] primaryKeyValues, FwCustomFields customFields = null, FwSqlConnection conn = null)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            for (int i = 0; i < primaryKeyProperties.Count; i++)
            {
                PropertyInfo pkProperty = primaryKeyProperties[i];
                Type propertyType = pkProperty.PropertyType;

                if ((propertyType == typeof(int?)) || (propertyType == typeof(Int32)))
                {
                    pkProperty.SetValue(this, Convert.ToInt32(primaryKeyValues[i]));
                }
                else if (propertyType == typeof(string))
                {
                    pkProperty.SetValue(this, primaryKeyValues[i]);
                }
                else if ((propertyType == typeof(bool?)) || (propertyType == typeof(bool)))
                {
                    bool b = false;
                    if (primaryKeyValues[i] != null)
                    {
                        b = FwConvert.ToBoolean(primaryKeyValues[i].ToString());
                    }
                    pkProperty.SetValue(this, b);

                }
                else
                {
                    throw new Exception("Primary key property type not implemented for " + propertyType.ToString() + ". [FwBusinessLogic.SetPrimaryKeys]");
                }
            }
            return await GetAsync<T>(customFields, conn);
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<dynamic> GetAsync<T>(FwCustomFields customFields = null, FwSqlConnection conn = null)
        {
            if (PrimaryKeyCount > 0)
            {
                if (AllPrimaryKeysHaveValues)
                {
                    if (conn == null)
                    {
                        conn = GetDatabaseConnection();
                    }
                    FwSqlSelect select = new FwSqlSelect();
                    using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        SetBaseSelectQuery(select, qry, customFields);
                        select.SetQuery(qry);

                        List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
                        int k = 0;
                        foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                        {
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = primaryKeyProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            string sqlColumnName = string.Empty;
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }
                            if (!sqlColumnName.Equals(string.Empty))
                            {
                                if ((k == 0) && (select.SelectStatements[0].Where.Count == 0))
                                {
                                    qry.Add("where ");
                                }
                                else
                                {
                                    qry.Add("and ");
                                }
                                qry.Add(sqlColumnName);
                                qry.Add(" = @keyvalue" + k.ToString());

                                qry.AddParameter("@keyvalue" + k.ToString(), primaryKeyProperty.GetValue(this));

                                k++;
                            }
                        }
                        //k = 0;
                        //foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                        //{
                        //    qry.AddParameter("@keyvalue" + k.ToString(), primaryKeyProperty.GetValue(this));
                        //    k++;
                        //}
                        MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                        MethodInfo generic = method.MakeGenericMethod(this.GetType());
                        dynamic result = generic.Invoke(qry, new object[] { customFields });
                        dynamic records = await result;
                        dynamic record = null;
                        if (records.Count > 0)
                        {
                            record = records[0];
                        }

                        AfterLoadEventArgs afterLoadArgs = new AfterLoadEventArgs();
                        afterLoadArgs.Record = record;
                        AfterLoad?.Invoke(this, afterLoadArgs);

                        return record;
                    }
                }
                else
                {
                    throw new Exception("One or more Primary Key values are missing on " + GetType().ToString() + ".GetAsync");
                }
            }
            else
            {
                throw new Exception("No Primary Keys have been defined on " + GetType().ToString());
            }
        }
        //------------------------------------------------------------------------------------
        protected void addStringFilterToSelect(string filterFieldName, string value, FwSqlSelect select, string filterCondition = "=", string paramName = "", bool addWhenBlank = false)
        {
            if (value != null)
            {
                if ((!value.Equals(string.Empty)) || addWhenBlank)
                {
                    if (string.IsNullOrEmpty(paramName))
                    {
                        paramName = filterFieldName;
                    }
                    select.AddWhere(filterFieldName + " " + filterCondition + " @" + paramName);
                    select.AddParameter("@" + paramName, value);
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected void addDateFilterToSelect(string filterFieldName, DateTime? value, FwSqlSelect select, string filterCondition = "=", string paramName = "")
        {
            if (value != null)
            {
                if (string.IsNullOrEmpty(paramName))
                {
                    paramName = filterFieldName;
                }
                select.AddWhere(filterFieldName + " " + filterCondition + " @" + paramName);
                select.AddParameter("@" + paramName, value);
            }
        }
        //------------------------------------------------------------------------------------
        protected void addBooleanFilterToSelect(string filterFieldName, bool? value, FwSqlSelect select, string filterCondition = "=", string paramName = "")
        {
            if (value != null)
            {
                if (string.IsNullOrEmpty(paramName))
                {
                    paramName = filterFieldName;
                }
                select.AddWhere(filterFieldName + " " + filterCondition + " @" + paramName);
                select.AddParameter("@" + paramName, value.GetValueOrDefault(false) ? "T" : "F");
            }
        }
        //------------------------------------------------------------------------------------
        //[Obsolete("Please call AddFilterFieldToSelect instead and make sure you push the filter in request.filterfields instead of request.uniqueids.")]
        protected void addFilterToSelect(string filterFieldName, string databaseFieldName, FwSqlSelect select, BrowseRequest request = null)
        {
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey(filterFieldName))
                {
                    if (uniqueIds[filterFieldName] is bool)
                    {
                        bool filterValue = (bool)uniqueIds[filterFieldName];
                        if (filterValue)
                        {
                            select.AddWhere(databaseFieldName + " = @" + databaseFieldName);
                        }
                        else
                        {
                            select.AddWhere(databaseFieldName + " <> @" + databaseFieldName);
                        }
                        select.AddParameter("@" + databaseFieldName, "T");
                    }
                    else
                    {
                        string filterValue = uniqueIds[filterFieldName].ToString();
                        if (filterValue.Contains(","))
                        {
                            select.AddWhereIn(databaseFieldName, filterValue, false);
                        }
                        else
                        {
                            select.AddWhere(databaseFieldName + " = @" + databaseFieldName);
                            select.AddParameter("@" + databaseFieldName, filterValue);
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected void AddFilterFieldToSelect(string filterFieldName, string databaseFieldName, FwSqlSelect select, BrowseRequest request = null)
        {
            if ((request != null) && (request.filterfields != null))
            {
                Dictionary<string, string> filterfields = ((Dictionary<string, string>)request.filterfields);
                if (filterfields.ContainsKey(filterFieldName))
                {
                    string value = filterfields[filterFieldName];
                    //select.AddWhere(databaseFieldName + " = @" + databaseFieldName);
                    //if (filterfields[filterFieldName] == "true" || filterfields[filterFieldName] == "false")
                    //{
                    //    select.AddParameter("@" + databaseFieldName, (filterfields[filterFieldName] == "true" ? "T" : "F"));
                    //}
                    //else
                    //{
                    //    select.AddParameter("@" + databaseFieldName, filterfields[filterFieldName].ToString());
                    //}

                    //justin 02/24/2019 added support for multiple values separated by comma
                    if (value.ToLower().Equals("true") || value.ToLower().Equals("false"))
                    {
                        select.AddWhere(databaseFieldName + " = @" + databaseFieldName);
                        select.AddParameter("@" + databaseFieldName, (value.ToLower().Equals("true") ? "T" : "F"));
                    }
                    else if (value.Contains(","))
                    {
                        select.AddWhereIn(" and ", databaseFieldName, value);
                    }
                    else
                    {
                        select.AddWhere(databaseFieldName + " = @" + databaseFieldName);
                        select.AddParameter("@" + databaseFieldName, value);
                    }

                }
            }
        }
        //------------------------------------------------------------------------------------
        protected void AddActiveViewFieldToSelect(string activeViewFieldName, string databaseFieldName, FwSqlSelect select, BrowseRequest request = null)
        {
            if ((request != null) && (request.activeviewfields != null))
            {
                if (request.activeviewfields.ContainsKey(activeViewFieldName))
                {
                    List<string> values = request.activeviewfields[activeViewFieldName];
                    if (values.Count == 1)
                    {
                        string value = values[0];
                        if (!value.ToUpper().Equals("ALL"))
                        {
                            string parameterName = activeViewFieldName.ToLower();
                            select.AddWhere("(" + databaseFieldName + " = @" + parameterName + ")");
                            select.AddParameter("@" + parameterName, value);
                        }
                    }
                    else if (values.Count > 1)
                    {
                        select.AddWhereIn(" and ", databaseFieldName, string.Join(",", values));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected string GetUniqueIdAsString(string uniqueIdFieldName, BrowseRequest request)
        {
            string fieldValue = null;
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueids = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueids.ContainsKey(uniqueIdFieldName))
                {
                    fieldValue = uniqueids[uniqueIdFieldName].ToString();
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected string GetMiscFieldAsString(string miscFieldName, BrowseRequest request)
        {
            string fieldValue = null;
            if ((request != null) && (request.miscfields != null))
            {
                IDictionary<string, object> miscfields = ((IDictionary<string, object>)request.miscfields);
                if (miscfields.ContainsKey(miscFieldName))
                {
                    fieldValue = miscfields[miscFieldName].ToString();
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected void AddMiscFieldToQueryAsString(string miscFieldName, string parameterName, FwSqlCommand qry, BrowseRequest request)
        {
            string fieldValue = GetMiscFieldAsString(miscFieldName, request);
            if (fieldValue != null)
            {
                if (!parameterName.StartsWith("@"))
                {
                    parameterName = "@" + parameterName;
                }
                qry.AddParameter(parameterName, SqlDbType.NVarChar, ParameterDirection.Input, fieldValue);
            }
        }
        //------------------------------------------------------------------------------------
        protected bool? GetUniqueIdAsBoolean(string uniqueIdFieldName, BrowseRequest request)
        {
            bool? fieldValue = null;
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueids = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueids.ContainsKey(uniqueIdFieldName))
                {
                    fieldValue = FwConvert.ToBoolean(uniqueids[uniqueIdFieldName].ToString());
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected bool? GetMiscFieldAsBoolean(string miscFieldName, BrowseRequest request)
        {
            bool? fieldValue = null;
            if ((request != null) && (request.miscfields != null))
            {
                IDictionary<string, object> miscfields = ((IDictionary<string, object>)request.miscfields);
                if (miscfields.ContainsKey(miscFieldName))
                {
                    fieldValue = FwConvert.ToBoolean(miscfields[miscFieldName].ToString());
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected void AddMiscFieldToQueryAsBoolean(string miscFieldName, string parameterName, FwSqlCommand qry, BrowseRequest request)
        {
            bool? fieldValue = GetMiscFieldAsBoolean(miscFieldName, request);
            if (fieldValue != null)
            {
                if (!parameterName.StartsWith("@"))
                {
                    parameterName = "@" + parameterName;
                }
                qry.AddParameter(parameterName, SqlDbType.NVarChar, ParameterDirection.Input, FwConvert.LogicalToCharacter(fieldValue.GetValueOrDefault(false)));
            }
        }
        //------------------------------------------------------------------------------------
        protected DateTime? GetUniqueIdAsDate(string uniqueIdFieldName, BrowseRequest request)
        {
            DateTime? fieldValue = null;
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueids = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueids.ContainsKey(uniqueIdFieldName))
                {
                    fieldValue = FwConvert.ToDateTime(uniqueids[uniqueIdFieldName].ToString());
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected DateTime? GetMiscFieldAsDate(string miscFieldName, BrowseRequest request)
        {
            DateTime? fieldValue = null;
            if ((request != null) && (request.miscfields != null))
            {
                IDictionary<string, object> miscfields = ((IDictionary<string, object>)request.miscfields);
                if (miscfields.ContainsKey(miscFieldName))
                {
                    fieldValue = FwConvert.ToDateTime(miscfields[miscFieldName].ToString());
                }
            }
            return fieldValue;
        }
        //------------------------------------------------------------------------------------
        protected void AddMiscFieldToQueryAsDate(string miscFieldName, string parameterName, FwSqlCommand qry, BrowseRequest request)
        {
            DateTime? fieldValue = GetMiscFieldAsDate(miscFieldName, request);
            if (fieldValue != null)
            {
                if (!parameterName.StartsWith("@"))
                {
                    parameterName = "@" + parameterName;
                }
                qry.AddParameter(parameterName, SqlDbType.Date, ParameterDirection.Input, fieldValue);
            }
        }
        //------------------------------------------------------------------------------------        
        public virtual async Task<object> GetCustomPrimaryKey(FwSqlConnection conn, SqlServerConfig _dbConfig)
        {
            object id = new object();

            await Task.Run(() => { });

            return id;
        }
        //------------------------------------------------------------------------------------
        public virtual object[] GetPrimaryKeys()
        {
            int pkIndex = 0;
            List<PropertyInfo> pkProperties = GetPrimaryKeyProperties();
            object[] ids = new object[pkProperties.Count];
            foreach (PropertyInfo pkProperty in pkProperties)
            {
                ids[pkIndex] = pkProperty.GetValue(this);
                pkIndex++;
            }
            return ids;
        }
        //------------------------------------------------------------------------------------
        protected void AddPropertiesAsQueryColumns(FwSqlCommand qry)
        {
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                if (sqlDataFieldAttribute != null)
                {
                    qry.AddColumn(sqlDataFieldAttribute.ColumnName, propertyInfo.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
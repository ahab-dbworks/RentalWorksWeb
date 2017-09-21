using AutoMapper;
using FwStandard.Options;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Threading;
using System.Text;
using FwStandard.BusinessLogic;

namespace FwStandard.DataLayer
{
    public class FwDataRecord : FwBaseRecord
    {
        protected SqlServerOptions _dbConfig { get; set; }
        public FwCustomValues _Custom = new FwCustomValues(); // for mapping back to BusinessLogic class

        //------------------------------------------------------------------------------------
        public FwDataRecord() : base() { }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public virtual string TableName
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().Table;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(SqlServerOptions dbConfig)
        {
            _dbConfig = dbConfig;
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
                            if (dataFieldAttribute.IsPrimaryKey)
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
                                if (propertyValue is string)
                                {
                                    hasPrimaryKeysSet &= (propertyValue as string).Length > 0;
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
                    object propertyValue = property.GetValue(this);
                    if (propertyValue is string)
                    {
                        noPrimaryKeysHaveValues &= (propertyValue as string).Length == 0;
                    }
                    else
                    {
                        throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.NoPrimaryKeysHaveValues]");
                    }

                }
                return noPrimaryKeysHaveValues;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual bool AllFieldsValid(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool valid = true;

            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if ((valid) && (property.IsDefined(typeof(FwSqlDataFieldAttribute))))
                {
                    foreach (Attribute attribute in property.GetCustomAttributes())
                    {
                        if ((valid) && (attribute.GetType() == typeof(FwSqlDataFieldAttribute)))
                        {
                            FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                            object propertyValue = property.GetValue(this);
                            if ((valid) && (dataFieldAttribute.Required))
                            {
                                if ((valid) && (saveMode == TDataRecordSaveMode.smInsert))
                                {
                                    if (propertyValue == null)
                                    {
                                        valid = false;
                                        validateMsg = property.Name + " is required.";
                                    }
                                }
                                if ((valid) && (propertyValue != null))
                                {
                                    if (propertyValue is string)
                                    {
                                        valid = ((propertyValue as string).Length > 0);
                                        if (!valid)
                                        {
                                            validateMsg = property.Name + " cannot be blank.";
                                        }
                                    }
                                    if (propertyValue is Int32)
                                    {
                                        valid = (((Int32)propertyValue) != 0);
                                        if (!valid)
                                        {
                                            validateMsg = property.Name + " cannot be zero.";
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("A test for \"Required\" of property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwDataRecord.AllFieldsValid]");
                                    }
                                }
                            }

                            if ((valid) && (dataFieldAttribute.MaxLength > 0))
                            {
                                if (propertyValue != null)
                                {
                                    if (propertyValue is bool)
                                    {
                                        valid = true;
                                    }
                                    else if (propertyValue is string)
                                    {
                                        valid = ((propertyValue as string).Length <= dataFieldAttribute.MaxLength);
                                        if (!valid)
                                        {
                                            validateMsg = property.Name + " cannot be longer than " + dataFieldAttribute.MaxLength.ToString() + " characters.";
                                        }
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
            return valid;
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
                            string id = await FwSqlData.GetNextIdAsync(conn, _dbConfig);
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
        protected virtual void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            select.Add("select");
            PropertyInfo[] properties = this.GetType().GetTypeInfo().GetProperties();
            int colNo = 0;
            Dictionary<string, string> columns = new Dictionary<string, string>();
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
                    string prefix = "";
                    if (colNo > 0)
                    {
                        prefix = ",";
                    }
                    qry.AddColumn("", property.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                    select.Add(prefix + " " + "t.[" + sqlColumnName + "] as " + property.Name);
                    colNo++;
                }
            }

            List<FwCustomTable> customTables = new List<FwCustomTable>();
            if ((customFields != null) && (customFields.Count > 0))
            {
                int customTableIndex = 1;
                int customFieldIndex = 1;
                foreach (FwCustomField customField in customFields)
                {
                    columns[customField.FieldName] = customField.FieldName;
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

                    qry.AddColumn("", customField.FieldName, FwDataTypes.Text, true, false, false);
                    select.Add(" ,[" + customField.FieldName + "] = " + customTableAlias + "." + customField.CustomFieldName);

                    customFieldIndex++;
                }
            }

            select.Add(" from " + TableName + " t with (nolock)");

            if ((customFields != null) && (customFields.Count > 0))
            {
                List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();

                foreach (FwCustomTable customTable in customTables)
                {
                    select.Add(" left outer join " + customTable.TableName + " " + customTable.Alias + " with (nolock) on ");
                    select.Add(" ( ");

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
                        select.Add("t." + sqlColumnName + " = " + customTable.Alias + "." + customUniqueIdField);
                        if (k < primaryKeyProperties.Count) {
                            select.Add(" and ");
                        }
                        k++;
                    }
                    select.Add(" ) ");
                }
            }

            if (request != null)
            {
                if (request.searchfields.Length > 0)
                {
                    select.Add("where");
                    for (int i = 0; i < request.searchfields.Length; i++)
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
                        string parameterName = "@" + columns[request.searchfields[i]];
                        if (request.searchfieldoperators[i] == "like")
                        {
                            // the upper function here will cause it to not use the index, this is not ideal
                            string searchcondition = conditionConjunction + "upper(" + columns[request.searchfields[i]] + ") like " + parameterName;
                            select.Add(searchcondition);
                            select.AddParameter(parameterName, "%" + request.searchfieldvalues[i].ToUpper() + "%");
                        }
                        else if (request.searchfieldoperators[i] == "=" || request.searchfieldoperators[i] == "<>")
                        {
                            // the upper function here will cause it to not use the index, this is not ideal
                            string searchcondition = conditionConjunction + "upper(" + columns[request.searchfields[i]] + ") " + request.searchfieldoperators[i] + " " + parameterName;
                            select.Add(searchcondition);
                            select.AddParameter(parameterName, request.searchfieldvalues[i].ToUpper());
                        }
                        else
                        {
                            throw new Exception("Invalid searchfieldoperator: " + request.searchfieldoperators[i]);
                        }
                    }
                }
                
                if (request.orderby.Trim().Length > 0)
                {
                    // validate the user supplied order by expression to prevent SQL Injection attacks
                    List<string> tokens = new List<string>(request.orderby.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries));
                    foreach(string token in tokens)
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
        public virtual async Task<FwJsonDataTable> BrowseAsync(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry, customFields: customFields, request: request);
                    select.SetQuery(qry);
                    dt = await qry.QueryToFwJsonTableAsync(includeAllColumns: false, pageNo: request.pageno, pageSize: request.pagesize);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                bool openAndCloseConnection = true;
                FwSqlSelect select = new FwSqlSelect();
                using (FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry, customFields: customFields, request: request);
                    select.SetQuery(qry);
                    MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(this.GetType());
                    dynamic result = generic.Invoke(qry, new object[] { openAndCloseConnection, customFields });
                    dynamic records = await result;
                    return records;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<dynamic> GetAsync<T>(string[] primaryKeyValues, FwCustomFields customFields = null)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            int k = 0;
            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
            {
                primaryKeyProperty.SetValue(this, primaryKeyValues[k]);
                k++;
            }
            return await GetAsync<T>(customFields);
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<dynamic> GetAsync<T>(FwCustomFields customFields = null)
        {
            if (AllPrimaryKeysHaveValues)
            {
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    using (FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        SetBaseSelectQuery(select, qry, customFields);
                        select.SetQuery(qry);

                        

                        List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
                        int k = 0;
                        foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                        {
                            //if (k == 0)
                            if ((k == 0) && (select.SelectStatements[0].Where.Count == 0))
                            {
                                qry.Add("where ");
                            }
                            else
                            {
                                qry.Add("and ");
                            }
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = primaryKeyProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            string sqlColumnName = primaryKeyProperty.Name;
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }
                            qry.Add(sqlColumnName);
                            qry.Add(" = @keyvalue" + k.ToString());
                            k++;
                        }
                        k = 0;
                        foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString(), primaryKeyProperty.GetValue(this));
                            k++;
                        }
                        MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                        MethodInfo generic = method.MakeGenericMethod(this.GetType());
                        object openAndCloseConnection = true;
                        dynamic result = generic.Invoke(qry, new object[] { openAndCloseConnection, customFields });
                        dynamic records = await result;
                        dynamic record = null;
                        if (records.Count > 0)
                        {
                            record = records[0];
                        }
                        return record;
                    }
                }
            }
            else
            {
                throw new Exception("One or more Primary Key values are missing on " + GetType().ToString() + ".Load");
            }
        }
        //------------------------------------------------------------------------------------
    }
}

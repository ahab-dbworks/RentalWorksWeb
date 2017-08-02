using AutoMapper;
using FwStandard.ConfigSection;
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

namespace FwStandard.DataLayer
{
    public class FwDataRecord : FwBaseRecord
    {
        protected DatabaseConfig _dbConfig { get; set; }
        public FwCustomValues _Custom = new FwCustomValues(); // for mapping back to BusinessLogic class

        //------------------------------------------------------------------------------------
        public FwDataRecord() : base() { }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public virtual string TableName
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().TableName;
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(DatabaseConfig dbConfig)
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
        protected virtual async Task SetPrimaryKeyIdsForInsertAsync(FwSqlConnection conn)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
            {
                string id = await FwSqlData.GetNextIdAsync(conn, _dbConfig);
                if (primaryKeyProperty.GetValue(this) is string)
                {
                    primaryKeyProperties[0].SetValue(this, id);
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual void SetBaseSelectQuery(FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            qry.Add("select");
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
                    qry.AddColumn("", property.Name, sqlDataFieldAttribute.DataType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                    qry.Add("  " + prefix + "t.[" + sqlColumnName + "] as " + property.Name);
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
                    qry.Add(" ,[" + customField.FieldName + "] = " + customTableAlias + "." + customField.CustomFieldName);

                    customFieldIndex++;
                }
            }

            qry.Add(" from " + TableName + " t with (nolock)");

            if ((customFields != null) && (customFields.Count > 0))
            {
                List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();

                foreach (FwCustomTable customTable in customTables)
                {
                    qry.Add(" left outer join " + customTable.TableName + " " + customTable.Alias + " with (nolock) on ");
                    qry.Add(" ( ");

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
                        qry.Add("t." + sqlColumnName + " = " + customTable.Alias + "." + customUniqueIdField);
                        if (k < primaryKeyProperties.Count) {
                            qry.Add(" and ");
                        }
                        k++;
                    }
                    qry.Add(" ) ");
                }
            }

            if (request != null)
            {
                if (request.searchfields.Length > 0)
                {
                    qry.Add("where");
                    for (int i = 0; i < request.searchfields.Length; i++)
                    {
                        if (!columns.ContainsKey(request.searchfields[i]))
                        {
                            throw new Exception("Searching is not supported on " + request.searchfields[i]);
                        }
                        if (request.searchfieldoperators[i] == "like")
                        {
                            string conditionConjunction = string.Empty;
                            if (i > 0)
                            {
                                conditionConjunction = "  and ";
                            }
                            string parameterName = "@" + columns[request.searchfields[i]];
                            string searchcondition = conditionConjunction + "upper(" + columns[request.searchfields[i]] + ") like " + parameterName;
                            qry.Add(searchcondition);
                            qry.AddParameter(parameterName, "%" + request.searchfieldvalues[i] + "%");
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
                    qry.Add("order by " + orderbyBuilder.ToString());
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<FwJsonDataTable> BrowseAsync(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry, customFields: customFields, request: request);
                dt = await qry.QueryToFwJsonTableAsync(includeAllColumns: false, pageNo: request.pageno, pageSize: request.pagesize);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<List<T>> SelectAsync<T>(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                bool openAndCloseConnection = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                {
                    SetBaseSelectQuery(qry, customFields: customFields, request: request);
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
                    FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    SetBaseSelectQuery(qry, customFields);
                    List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
                    int k = 0;
                    foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
                    {
                        if (k == 0)
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
            else
            {
                throw new Exception("One or more Primary Key values are missing on " + GetType().ToString() + ".Load");
            }
        }
        //------------------------------------------------------------------------------------
    }
}

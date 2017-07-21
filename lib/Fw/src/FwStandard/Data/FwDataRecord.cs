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



namespace FwStandard.DataLayer
{
    public class FwDataRecord : FwBaseRecord
    {
        protected DatabaseConfig _dbConfig { get; set; }
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
        protected virtual void SetBaseSelectQuery(FwSqlCommand qry, FwCustomFields customFields = null)
        {
            qry.Add("select");
            PropertyInfo[] properties = this.GetType().GetTypeInfo().GetProperties();
            int colNo = 0;
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                    string sqlColumnName = property.Name;
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
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<FwJsonDataTable> BrowseAsync(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry, customFields);
                dt = await qry.QueryToFwJsonTableAsync(false);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<IEnumerable<T>> SelectAsync<T>(BrowseRequestDto request, FwCustomFields customFields = null)
        {
            IEnumerable<T> results;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry, customFields);
                results = await qry.SelectAsync<T>(true, request.pageno, request.pagesize);
            }
            return results;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(string[] primaryKeyValues, FwCustomFields customFields = null)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            int k = 0;
            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
            {
                primaryKeyProperty.SetValue(this, primaryKeyValues[k]);
            }
            return await LoadAsync<T>();
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync<T>(FwCustomFields customFields = null)
        {
            bool loaded = false;
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
                    var record = await qry.SelectOneAsync<T>(true);
                    Mapper.Map(record, this);
                    loaded = (record != null);
                }
            }
            else
            {
                throw new Exception("One or more Primary Key values are missing on " + GetType().ToString() + ".Load");
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
    }
}

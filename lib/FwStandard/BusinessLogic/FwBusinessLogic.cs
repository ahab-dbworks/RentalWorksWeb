using AutoMapper;
using FwStandard.ConfigSection;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogic
    {
        protected DatabaseConfig _dbConfig { get; set; }
        public FwBusinessLogic() : base()
        {
            
        }

        [JsonIgnore]
        public virtual string TableName
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().TableName;
            }
        }

        [JsonIgnore]
        public virtual bool HasInsert
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().HasInsert;
            }
        }

        [JsonIgnore]
        public virtual bool HasUpdate
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().HasUpdate;
            }
        }

        [JsonIgnore]
        public virtual bool HasDelete
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<FwSqlTableAttribute>().HasDelete;
            }
        }
        public void SetDbConfig(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        protected List<PropertyInfo> GetPrimaryKeyProperties()
        {
            List<PropertyInfo> primaryKeyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                {
                    foreach(Attribute attribute in property.GetCustomAttributes())
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

        [JsonIgnore]
        public bool AllPrimaryKeysHaveValues
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

        [JsonIgnore]
        public bool NoPrimaryKeysHaveValues
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
                        throw new Exception("A test for property type " + propertyValue.GetType().ToString() + " needs to be implemented! [FwBusinessLogic.NoPrimaryKeysHaveValues]");
                    }

                }
                return noPrimaryKeysHaveValues;
            }
        }

        public virtual void SetPrimaryKeyIdsForInsert(FwSqlConnection conn)
        {
            List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();
            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
            {
                string id = FwSqlData.GetNextId(conn, _dbConfig);
                if (primaryKeyProperty.GetValue(this) is string)
                {
                    primaryKeyProperties[0].SetValue(this, id);
                }
            }
        }

        public void SetBaseSelectQuery(FwSqlCommand qry)
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
                    qry.Add("  " + prefix + "[" + sqlColumnName + "] as " + property.Name);
                    colNo++;
                }
            }
            qry.Add("from " + TableName + " with (nolock)");
        }

        public FwJsonDataTable Browse(BrowseRequestDto request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry);
                dt = qry.QueryToFwJsonTable(false);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        public IEnumerable<T> Select<T>(BrowseRequestDto request)
        {
            IEnumerable<T> results;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry);
                results = qry.Select<T>(true, request.pageno, request.pagesize);
            }
            return results;
        }
        //------------------------------------------------------------------------------------
        public void Load<T>(string primaryKeyValue) // should be an Array/List
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                SetBaseSelectQuery(qry);
                //qry.Add("where custstatusid = @custstatusid");
                //qry.Add("order by custstatus");
                //qry.AddParameter("@custstatusid", custstatusid);

                //jh 06/26/2017
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
                    qry.AddParameter("@keyvalue" + k.ToString(), primaryKeyValue);
                    k++;
                }

                var record = qry.SelectOne<T>(true);
                Mapper.Map(record, this);
            }
        }

        public virtual void Save()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                if (NoPrimaryKeysHaveValues)
                {
                    //insert
                    SetPrimaryKeyIdsForInsert(conn);
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    cmd.Insert(true, TableName, this, _dbConfig);
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    cmd.Update(true, TableName, this);

                }
                else
                {
                    throw new Exception("Primary key values were not supplied for all primary keys.");
                }
            }
        }

        public virtual bool Delete()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                //int rowcount = cmd.Delete(true, "custstatus", this);
                int rowcount = cmd.Delete(true, TableName, this);   //jh 06/26/2017
                return rowcount > 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FwCore.Utilities;
using FwCore.ValueTypes;

namespace FwCore.SqlServer
{
    public class FwSqlTable
    {
        //---------------------------------------------------------------------------------------------
        public string TableName {get;set;}
        private string PhysicalName {get;set;}
        public  bool   SaveData {get;set;}
        public  bool   UpdateInsteadOfInsert {get;set;}
        public  int    SaveOrder {get;set;}
        public FwApplicationSchema.FormTable TableSchema {get;set;}
        //---------------------------------------------------------------------------------------------
        public Dictionary<string, FwSqlColumn> UniqueIds {get;set;}
        public Dictionary<string, FwSqlColumn> Fields {get;set;}
        //---------------------------------------------------------------------------------------------
        public string GetTableName
        {
            get
            {
                return this.PhysicalName.Equals(string.Empty) ? this.TableName : this.PhysicalName;
            }
        }
        //---------------------------------------------------------------------------------------------
        public string Table
        {
            get
            {
                return this.TableName;
            }
            set
            {
                this.TableName = value;
                if (this.TableName.Contains("!"))
                {
                    this.PhysicalName = this.TableName.Substring(0, this.TableName.IndexOf("!"));
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlTable()
        {
            this.TableName        = string.Empty;
            this.PhysicalName     = string.Empty;
            //this.Columns          = new FwSqlColumnCollection();
            this.UniqueIds        = new Dictionary<string,FwSqlColumn>();
            this.Fields           = new Dictionary<string,FwSqlColumn>();
            this.SaveData         = true;
            UpdateInsteadOfInsert = false;
            SaveOrder             = 0;
            TableSchema           = null;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlTable(string table) : this()
        {
            this.Table = table;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlTable(string table, FwApplicationSchema.FormTable tableschema) : this()
        {
            this.Table        = table;
            this.TableSchema  = tableschema;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlColumn GetUniqueId(string name)
        {
            FwSqlColumn column = null;
            FwSqlColumnSchema colSchema = null;

            foreach (var uniqueIdItem in this.UniqueIds)
            {
                FwSqlColumn uniqueId;

                uniqueId = uniqueIdItem.Value;
                if (uniqueId.Column == name)
                {
                    column = uniqueId;
                    break;
                }
            }
            if (column == null)
            {
                column = new FwSqlColumn(this, name);
                if (TableSchema.UniqueIds.ContainsKey(name))
                {
                    colSchema = new FwSqlColumnSchema(this.TableSchema.UniqueIds[name]);
                }
                else
                {
                    throw new Exception("FwSqlTable.GetUniqueId: " + name + " does not exist in " + this.TableName + ".");
                }
                column.ColumnSchema = colSchema;
                this.UniqueIds[name] = column;
            }
            return column;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlColumn GetField(string name)
        {
            FwSqlColumn column;
            FwSqlColumnSchema colSchema;

            column = null;
            foreach (var fieldItem in this.Fields)
            {
                FwSqlColumn field;
                
                field = fieldItem.Value;
                if (field.Column == name)
                {
                    column = field;
                    break;
                }
            }
            if (column == null)
            {
                column = new FwSqlColumn(this, name);
                if (TableSchema.Columns.ContainsKey(name))
                {
                    colSchema = new FwSqlColumnSchema(TableSchema.Columns[name]);
                }
                else if (TableSchema.UniqueIds.ContainsKey(name))
                {
                    colSchema = new FwSqlColumnSchema(TableSchema.UniqueIds[name]);
                }
                else
                {
                    throw new Exception("FwSqlTable.GetField: Column '" + name + "' is not defined in the schema for Table '" + this.TableName + "'.");
                }
                column.ColumnSchema = colSchema;
                this.Fields[name] = column;
            }
            return column;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlCommand GetSelectStatement(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            string prefix;

            qry  = new FwSqlCommand(conn);
            qry.Add("select top 1 ");
            prefix = string.Empty;
            foreach(var fieldItem in this.Fields)
            {
                qry.Add(prefix + fieldItem.Value.Column);
                prefix = ","; 
            }
            qry.Add("from " + GetTableName + " with (nolock)");
            GetWhere(qry);

            return qry;
        }
        //---------------------------------------------------------------------------------------------
        public void UpdateDateStampColumn()
        {
            this.GetField("datestamp").Value = new FwDatabaseField(DateTime.UtcNow).ToDateTimeStringIso8601();
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlCommand GetInsertCommand(FwSqlConnection conn)
        {
            string prefix;
            FwSqlCommand qry;

            qry = null;
            if (SaveData)
            {
                qry  = new FwSqlCommand(conn);
                this.UpdateDateStampColumn();
                qry.Add("insert into " + this.GetTableName + "(");
                prefix = string.Empty;
                foreach(var fieldItem in this.Fields)
                {
                    if (!fieldItem.Value.ColumnSchema.IsIdentity)
                    {
                        qry.Add(prefix + fieldItem.Value.Column);
                        prefix = ",";
                    }
                }
                qry.Add(")");
                qry.Add("values(");
                prefix = string.Empty;
                foreach(var fieldItem in this.Fields)
                {
                    if (!fieldItem.Value.ColumnSchema.IsIdentity)
                    {
                        if (((fieldItem.Value.ColumnSchema.DataType == "varchar") || (fieldItem.Value.ColumnSchema.DataType == "char")) && (FwApplicationConfig.CurrentSite.ApplicationSettings.ForceUpperCase))
                        {
                            fieldItem.Value.Value = fieldItem.Value.Value.ToUpper();
                        }
                        qry.Add(prefix + "@" + fieldItem.Value.Column);
                        qry.AddParameter("@" + fieldItem.Value.Column, fieldItem.Value.SQLFormat());
                        prefix = ",";
                    }
                }
                qry.Add(")");
            }

            return qry;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlCommand GetUpdateCommand(FwSqlConnection conn)
        {
            string prefix;
            FwSqlCommand qry;
            bool hasModifiedField;
            FwSqlColumn column;

            qry = null;
            if (SaveData)
            {
                qry  = new FwSqlCommand(conn);
                UpdateDateStampColumn();
                qry.Add("update " + GetTableName);
                qry.Add("set ");
                hasModifiedField = false;
                prefix           = string.Empty;
                foreach(var fieldItem in this.Fields)
                {
                    column = fieldItem.Value;
                    if ((!column.EnableValidation) || ((!column.ColumnSchema.ReadOnly) && (!column.ColumnSchema.IsIdentity) && (column.Modified)))
                    {
                        if (((column.ColumnSchema.DataType == "varchar") || (column.ColumnSchema.DataType == "char")) && (FwApplicationConfig.CurrentSite.ApplicationSettings.ForceUpperCase))
                        {
                            column.Value = column.Value.ToUpper();
                        }
                        qry.Add(prefix + column.Column + "= @fld" + column.Column);
                        qry.AddParameter("@fld" + column.Column, column.SQLFormat());
                        hasModifiedField = true;
                        prefix = ",";
                    }
                }
                GetWhere(qry);
                if (!hasModifiedField)
                {
                    qry = null;
                }
            }

            return qry;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlCommand GetDeleteCommand(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            
            qry  = new FwSqlCommand(conn);
            qry.Add("delete " + GetTableName);
            GetWhere(qry);

            return qry;
        }
        //---------------------------------------------------------------------------------------------
        public void GetWhere(FwSqlCommand qry)
        {
            string prefix;
            FwSqlColumn column;

            qry.Add("where");
            prefix = string.Empty;
            foreach(var uniqueIdItem in this.UniqueIds)
            {
                column = uniqueIdItem.Value;
                if (column.UniqueIdentifier)
                {
                    qry.Add(prefix + " " + column.Column + " = @uid" + column.Column);
                    qry.AddParameter("@uid" + column.Column, column.SQLFormat());
                    prefix = " and";
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public bool HasUniqueIdentifier()
        {
            bool hasIdentifier;

            hasIdentifier = (this.UniqueIds.Count > 0);

            return hasIdentifier;
        }
        //---------------------------------------------------------------------------------------------
        public void SelectRow(FwSqlConnection conn)
        {
            FwSqlCommand qry;

            qry = GetSelectStatement(conn);
            if (qry != null)
            {
                qry.Execute();
                foreach (var columnItem in this.Fields)
                {
                    columnItem.Value.SetValue(qry);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void InsertRow(FwSqlConnection conn)
        {
            FwSqlCommand qry;

            if (!HasUniqueIdentifier())
            {
                throw new Exception("FwSqlTable.InsertRow: Unique identifier is missing on table " + Table);
            }
            qry = GetInsertCommand(conn);
            if (qry != null)
            {
                qry.ExecuteNonQuery();
            }
            foreach(var fieldItem in this.Fields)
            {
                if (fieldItem.Value.UniqueIdentifier == true)
                {
                    this.UniqueIds[fieldItem.Key] = fieldItem.Value;
                    this.Fields.Remove(fieldItem.Key);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void UpdateRow(FwSqlConnection conn)
        {
            FwSqlCommand qry;

            if (!HasUniqueIdentifier())
            {
                throw new Exception("FwSqlTable.UpdateRow: Unique identifier is missing on Table " + Table);
            }
            qry = GetUpdateCommand(conn);
            if (qry != null)
            {
                qry.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------------------------------------
        public bool HasIdentity()
        {
            bool identity = false;

            foreach (var columnItem in this.UniqueIds)
            {
                identity = columnItem.Value.ColumnSchema.IsIdentity;
                if (identity)
                {
                    break;
                }
            }
            return identity;
        }
        //---------------------------------------------------------------------------------------------
        public void DeleteRow(FwSqlConnection conn)
        {
            FwSqlCommand qry;

            if (!HasUniqueIdentifier())
            {
                throw new Exception("FwSqlTable.DeleteRow: Unique identifier is missing on table" + Table);
            }
            qry = GetDeleteCommand(conn);
            if (qry != null)
            {
                qry.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------------------------------------
        private bool IsDuplicateModified(List<FwSqlColumn> sqlColumns)
        {
            bool modified;

            modified = false;
            foreach (FwSqlColumn sqlColumn in sqlColumns)
            {
                modified = sqlColumn.Modified;
                if (modified)
                {
                    break;
                }
            }
            
            return modified;
        }
        //---------------------------------------------------------------------------------------------
        private string GetDuplicateSQL(List<FwSqlColumn> sqlColumns)
        {
            StringBuilder sb;
            bool hasWhere;
            string sql;

            hasWhere = false;
            sb = new StringBuilder();
            sb.AppendLine("select top 1 totalcount = count(*)");
            sb.AppendLine("from " + this.TableName);
            foreach (FwSqlColumn sqlColumn in sqlColumns)
            {
                sb.AppendLine((hasWhere ? " and " : " where ") + sqlColumn.Column + " = " + sqlColumn.SQLFormat());
                hasWhere = true;
            }
            sql = sb.ToString();
            
            return sql;
        }
        //---------------------------------------------------------------------------------------------
        public bool IsDuplicate(List<FwSqlColumn> sqlColumns)
        {
            bool isdupe;
            string sql;

            isdupe = false;
            sql    = string.Empty;
            if (IsDuplicateModified(sqlColumns))
            {
                sql    = GetDuplicateSQL(sqlColumns);
                isdupe = CheckDuplicate(sql);
            }
            
            return isdupe;
        }
        //---------------------------------------------------------------------------------------------
        private bool CheckDuplicate(string sql)
        {
            bool isdupe;

            isdupe = false;

            return isdupe;
        }
        //---------------------------------------------------------------------------------------------
    }
}



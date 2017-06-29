using Dapper;
using FwStandard.BusinessLogic;
using FwStandard.ConfigSection;
using FwStandard.Models;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace FwStandard.SqlServer
{
    //public enum FwQueryTimeouts {Default, Report}
    public class FwSqlCommand : IDisposable
    {
        //------------------------------------------------------------------------------------
        private FwSqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private StringBuilder qryText;
        private SqlDataReader reader;
        private bool eof;
        private List<FwJsonDataTableColumn> columns;
        private FwFields fields;
        private StringBuilder sql;
        private FwSqlLogEntry sqlLogEntry;
        //------------------------------------------------------------------------------------                
        public string Sql { get { return sql.ToString(); } }
        //------------------------------------------------------------------------------------                
        public int RowCount { get; private set; }
        public List<string> FieldNames { get { return new List<string>(fields.Keys); } }
        public SqlParameterCollection Parameters { get { return this.sqlCommand.Parameters; } }
        public SqlTransaction Transaction { get { return this.sqlCommand.Transaction; } set { this.sqlCommand.Transaction = value; } }
        public string QryTextDebug
        {
            get
            {
                StringBuilder sb;
                string result;

                sb = new StringBuilder();
                if (this.Parameters.Count > 0)
                {
                    sb.AppendLine("declare");
                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        else
                        {
                            sb.Append("  ");
                        }
                        sb.Append(Parameters[i].ParameterName);
                        sb.Append(" ");
                        switch (Parameters[i].SqlDbType)
                        {
                            // text
                            case SqlDbType.Char:
                            case SqlDbType.NChar:
                                if (Parameters[i].Size > 0)
                                {
                                    sb.AppendLine(Parameters[i].SqlDbType.ToString() + "(" + Parameters[i].Size + ")");
                                }
                                else
                                {
                                    sb.AppendLine(Parameters[i].SqlDbType.ToString() + "(255)");
                                }
                                break;
                            case SqlDbType.VarChar:
                            case SqlDbType.NVarChar:
                                if (Parameters[i].Size > 0)
                                {
                                    sb.AppendLine(Parameters[i].SqlDbType.ToString() + "(" + Parameters[i].Size + ")");
                                }
                                else
                                {
                                    sb.AppendLine(Parameters[i].SqlDbType.ToString() + "(max)");
                                }
                                break;
                            case SqlDbType.NText:
                            case SqlDbType.Text:
                            case SqlDbType.Date:
                            case SqlDbType.DateTime:
                            case SqlDbType.DateTime2:
                            case SqlDbType.DateTimeOffset:
                            case SqlDbType.SmallDateTime:
                            case SqlDbType.Time:
                            case SqlDbType.Timestamp:
                            case SqlDbType.UniqueIdentifier:
                            case SqlDbType.BigInt:
                            case SqlDbType.Bit:
                            case SqlDbType.Decimal:
                            case SqlDbType.Float:
                            case SqlDbType.Int:
                            case SqlDbType.Money:
                            case SqlDbType.Real:
                            case SqlDbType.SmallInt:
                            case SqlDbType.SmallMoney:
                            case SqlDbType.TinyInt:
                            case SqlDbType.Binary:
                            case SqlDbType.Image:
                            case SqlDbType.Structured:
                            case SqlDbType.Udt:
                            case SqlDbType.VarBinary:
                            case SqlDbType.Variant:
                            case SqlDbType.Xml:
                            default:
                                sb.AppendLine(Parameters[i].SqlDbType.ToString());
                                break;
                        }

                    }
                }
                if (this.Parameters.Count > 0)
                {
                    sb.AppendLine();
                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        if (this.sqlCommand.CommandType == CommandType.StoredProcedure && Parameters[i].Direction == ParameterDirection.Output)
                        {
                            continue;
                        }
                        sb.Append("set ");
                        sb.Append(Parameters[i].ParameterName);
                        sb.Append(" = ");
                        switch (Parameters[i].SqlDbType)
                        {
                            // text
                            case SqlDbType.Char:
                            case SqlDbType.Date:
                            case SqlDbType.DateTime:
                            case SqlDbType.DateTime2:
                            case SqlDbType.DateTimeOffset:
                            case SqlDbType.NChar:
                            case SqlDbType.NText:
                            case SqlDbType.NVarChar:
                            case SqlDbType.SmallDateTime:
                            case SqlDbType.Text:
                            case SqlDbType.Time:
                            case SqlDbType.Timestamp:
                            case SqlDbType.UniqueIdentifier:
                                if (Parameters[i].SqlValue != null)
                                {
                                    sb.Append("'");
                                    sb.Append(Parameters[i].SqlValue.ToString());
                                    sb.AppendLine("'");
                                }
                                else
                                {
                                    sb.AppendLine("null");
                                }
                                break;

                            // numbers
                            case SqlDbType.BigInt:
                            case SqlDbType.Bit:
                            case SqlDbType.Decimal:
                            case SqlDbType.Float:
                            case SqlDbType.Int:
                            case SqlDbType.Money:
                            case SqlDbType.Real:
                            case SqlDbType.SmallInt:
                            case SqlDbType.SmallMoney:
                            case SqlDbType.TinyInt:
                            case SqlDbType.VarChar:
                                if (Parameters[i].SqlValue != null)
                                {
                                    sb.Append("'");
                                    sb.Append(Parameters[i].SqlValue.ToString());
                                    sb.AppendLine("'");
                                }
                                else
                                {
                                    sb.AppendLine("null");
                                }
                                break;
                            // special types
                            case SqlDbType.Binary:
                            case SqlDbType.Image:
                            case SqlDbType.Structured:
                            case SqlDbType.Udt:
                            case SqlDbType.VarBinary:
                            case SqlDbType.Variant:
                            case SqlDbType.Xml:
                            default:
                                // this is probably not what we want, but maybe it will show the data at least
                                if (Parameters[i].SqlValue != null)
                                {
                                    sb.Append("'");
                                    sb.Append(Parameters[i].SqlValue.ToString());
                                    sb.AppendLine("'");
                                }
                                else
                                {
                                    sb.AppendLine("null");
                                }
                                break;
                        }
                    }
                }
                sb.AppendLine();
                if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    sb.Append("exec " + qryText.ToString());
                    if (this.Parameters.Count > 0)
                    {
                        sb.AppendLine();
                        for (int i = 0; i < Parameters.Count; i++)
                        {
                            sb.Append("  " + Parameters[i].ParameterName + " = " + Parameters[i].ParameterName);
                            if (Parameters[i].Direction == ParameterDirection.InputOutput || Parameters[i].Direction == ParameterDirection.Output)
                            {
                                sb.Append(" output");
                            }
                            if (i < Parameters.Count - 1)
                            {
                                sb.AppendLine(",");
                            }
                        }
                    }
                }
                else
                {
                    sb.AppendLine(qryText.ToString());
                }
                if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    sb.AppendLine();
                    if (this.Parameters.Count > 0)
                    {
                        sb.AppendLine();
                        sb.AppendLine("select");
                        for (int i = 0; i < Parameters.Count; i++)
                        {
                            if (Parameters[i].Direction == ParameterDirection.InputOutput || Parameters[i].Direction == ParameterDirection.Output)
                            {
                                sb.Append("  " + Parameters[i].ParameterName.Replace("@", "") + " = ");
                                sb.Append(Parameters[i].ParameterName);
                                if (i < Parameters.Count - 1)
                                {
                                    sb.AppendLine(",");
                                }
                            }
                        }
                    }
                }
                result = sb.ToString();

                return result;
            }
        }
        //------------------------------------------------------------------------------------
        public FwSqlCommand(FwSqlConnection conn, int timeout)
        {
            this.sqlConnection = conn;
            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection.GetConnection();
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.CommandTimeout = timeout;
            this.qryText = new StringBuilder();
            this.reader = null;
            this.eof = true;
            this.columns = new List<FwJsonDataTableColumn>();
            this.fields = new FwFields();
            this.RowCount = 0;
        }
        //------------------------------------------------------------------------------------
        public FwSqlCommand(FwSqlConnection conn, string storedProcedureName, int timeout)
        {
            this.sqlConnection = conn;
            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection.GetConnection();
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlCommand.CommandTimeout = timeout;
            this.qryText = new StringBuilder();
            this.qryText.Append(storedProcedureName);
            this.reader = null;
            this.eof = true;
            this.columns = new List<FwJsonDataTableColumn>();
            this.fields = new FwFields();
            this.RowCount = 0;
        }
        //------------------------------------------------------------------------------------
        public void Clear()
        {
            this.qryText = new StringBuilder();
            this.sqlCommand.CommandText = string.Empty;
        }
        //------------------------------------------------------------------------------------
        public void Add(string text)
        {
            if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
            {
                throw new Exception("Can't add query text to a stored procedure.");
            }
            this.qryText.AppendLine(text);
        }
        //------------------------------------------------------------------------------------
        public void AddParameter(string name, object value)
        {
            this.sqlCommand.Parameters.AddWithValue(name, value);
        }
        //------------------------------------------------------------------------------------
        public void AddParameter(string name, SqlDbType sqlDbType, ParameterDirection direction, object value)
        {
            SqlParameter parameter;

            parameter = sqlCommand.Parameters.Add(name, sqlDbType, -1);
            parameter.Direction = direction;
            parameter.Value = value;
        }
        //------------------------------------------------------------------------------------
        public void AddParameter(string name, SqlDbType sqlDbType, ParameterDirection direction)
        {
            SqlParameter parameter;

            parameter = sqlCommand.Parameters.Add(name, sqlDbType, -1);
            parameter.Direction = direction;
        }
        //------------------------------------------------------------------------------------
        public void AddParameter(string name, SqlDbType sqlDbType, ParameterDirection direction, byte precision, byte scale)
        {
            SqlParameter parameter;

            parameter = sqlCommand.Parameters.Add(name, sqlDbType, -1);
            parameter.Direction = direction;
            parameter.Precision = precision;
            parameter.Scale = scale;
        }
        //------------------------------------------------------------------------------------
        public FwDatabaseField GetParameter(string name)
        {
            return new FwDatabaseField(this.sqlCommand.Parameters[name].Value);
        }
        //------------------------------------------------------------------------------------
        public void AddColumn(string dataField)
        {
            AddColumn(dataField, false);
        }
        //------------------------------------------------------------------------------------
        public void AddColumn(string dataField, bool encrypt)
        {
            this.columns.Add(new FwJsonDataTableColumn(dataField, encrypt));
        }
        //------------------------------------------------------------------------------------
        public void AddColumn(string dataField, bool encrypt, FwDataTypes dataType)
        {
            FwJsonDataTableColumn column = new FwJsonDataTableColumn(dataField, encrypt);
            column.DataType = dataType;
            this.columns.Add(column);
        }
        //------------------------------------------------------------------------------------
        public void AddColumn(string name, string dataField, FwDataTypes dataType, bool isVisible, bool isUniqueId, bool encrypt)
        {
            FwJsonDataTableColumn column = new FwJsonDataTableColumn(dataField, encrypt);
            column.Name = name;
            column.IsVisible = isVisible;
            column.IsUniqueId = isUniqueId;
            column.DataType = dataType;
            this.columns.Add(column);
        }
        //------------------------------------------------------------------------------------
        public void AddColumn(string name, string dataField, FwDataTypes dataType, bool encrypt)
        {
            FwJsonDataTableColumn column = new FwJsonDataTableColumn(dataField, encrypt);
            column.Name = name;
            column.DataType = dataType;
            this.columns.Add(column);
        }
        //------------------------------------------------------------------------------------
        private void LogSql()
        {
            bool hasFirstDeclareParameter = false, hasFirstExecParameter = false, hasFirstSelectParameter = false;
            int maxParameterWidth = 0;

            sql = new StringBuilder();
            if (this.sqlCommand.Parameters.Count > 0)
            {
                sql.Append("declare\r\n  ");
                for (int i = 0; i < this.sqlCommand.Parameters.Count; i++)
                {
                    if (sqlCommand.Parameters[i].ParameterName.Length > maxParameterWidth)
                    {
                        maxParameterWidth = sqlCommand.Parameters[i].ParameterName.Length;
                    }
                }
                for (int i = 0; i < this.sqlCommand.Parameters.Count; i++)
                {
                    if (!hasFirstDeclareParameter)
                    {
                        hasFirstDeclareParameter = true;
                    }
                    else
                    {
                        if (i > 0)
                        {
                            sql.Append("\r\n, ");
                        }
                    }
                    sql.Append(this.sqlCommand.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));
                    sql.Append(" ");
                    switch (this.sqlCommand.Parameters[i].SqlDbType)
                    {
                        case SqlDbType.BigInt:
                            sql.Append("bigint");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Binary:
                            sql.Append("binary(8000)");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Bit:
                            sql.Append("bit");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Char:
                            sql.Append("char(8000)");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Date:
                            sql.Append("date");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTime:
                            sql.Append("datetime");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTime2:
                            sql.Append("datetime2");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTimeOffset:
                            sql.Append("datetimeoffset");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Decimal:
                            sql.Append("decimal(16,4)");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Float:
                            sql.Append("float");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Image:
                            sql.Append("image");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Int:
                            sql.Append("int");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Money:
                            sql.Append("money");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.NChar:
                            sql.Append("nchar(8000)");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.NText:
                            sql.Append("ntext");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.NVarChar:
                            sql.Append("nvarchar(max)");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Real:
                            sql.Append("real");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.SmallDateTime:
                            sql.Append("smalldatetime");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.SmallInt:
                            sql.Append("smallint");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.SmallMoney:
                            sql.Append("smallmoney");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Structured:
                            sql.Append("structured");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Time:
                            sql.Append("time");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Timestamp:
                            sql.Append("timestamp");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.TinyInt:
                            sql.Append("tinyint");
                            if (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString()))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Udt:
                            sql.Append("udt");
                            if ((this.sqlCommand.Parameters[i].Value != null) && (!string.IsNullOrEmpty(this.sqlCommand.Parameters[i].Value.ToString())))
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.UniqueIdentifier:
                            sql.Append("uniqueidentifier");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.VarBinary:
                            sql.Append("varbinary(max)");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Text:
                        case SqlDbType.VarChar:
                            sql.Append("varchar(max)");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Variant:
                            sql.Append("sql_variant");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = " + this.sqlCommand.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Xml:
                            sql.Append("xml");
                            if (this.sqlCommand.Parameters[i].Value != null)
                            {
                                sql.Append(" = '" + this.sqlCommand.Parameters[i].Value.ToString() + "'");
                            }
                            break;


                    }
                }
            }
            if (sqlCommand.CommandType == CommandType.Text)
            {
                sql.Append("\r\n\r\n" + qryText);
            }
            else if (sqlCommand.CommandType == CommandType.StoredProcedure)
            {
                sql.Append("\r\n\r\nexec " + qryText + "\r\n  ");
                for (int i = 0; i < sqlCommand.Parameters.Count; i++)
                {
                    if (!hasFirstExecParameter)
                    {
                        hasFirstExecParameter = true;
                    }
                    else
                    {
                        if (i > 0)
                        {
                            sql.Append("\r\n, ");
                        }
                    }
                    sql.Append(this.sqlCommand.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' ') + " = " + sqlCommand.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));
                    if ((sqlCommand.Parameters[i].Direction == ParameterDirection.InputOutput) || (sqlCommand.Parameters[i].Direction == ParameterDirection.Output))
                    {
                        sql.Append(" output");
                    }
                }
                if (sqlCommand.Parameters.Count > 0)
                {
                    sql.Append("\r\n\r\nselect\r\n  ");
                    for (int i = 0; i < sqlCommand.Parameters.Count; i++)
                    {
                        if ((sqlCommand.Parameters[i].Direction == ParameterDirection.InputOutput) || (sqlCommand.Parameters[i].Direction == ParameterDirection.Output))
                        {
                            if (!hasFirstSelectParameter)
                            {
                                hasFirstSelectParameter = true;
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    sql.Append("\r\n, ");
                                }
                            }
                            sql.Append(this.sqlCommand.Parameters[i].ParameterName.PadRight(maxParameterWidth - 2, ' ').Replace("@", string.Empty) + " = " + sqlCommand.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' ') + " --" + sqlCommand.Parameters[i].Direction.ToString());
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public int ExecuteNonQuery()
        {
            return ExecuteNonQuery(true);
        }
        //------------------------------------------------------------------------------------
        public int ExecuteNonQuery(bool closeConnection)
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteNonQuery()");
                if (closeConnection)
                {
                    this.sqlConnection.Open();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("Cannot insert duplicate key row in object"))
                {
                    throw new Exception("Cannot insert duplicate record.");
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (closeConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteNonQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public int ExecuteInsertQuery(string tablename)
        {
            StringBuilder insertColumnsNames, insertParameterNames;

            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");
                this.sqlConnection.Open();
                insertColumnsNames = new StringBuilder();
                insertParameterNames = new StringBuilder();
                for (int i = 0; i < this.sqlCommand.Parameters.Count; i++)
                {
                    if (i > 0)
                    {
                        insertColumnsNames.Append(",");
                        insertParameterNames.Append(",");
                    }
                    insertColumnsNames.Append("[");
                    insertColumnsNames.Append(this.sqlCommand.Parameters[i].ParameterName.Replace("@", string.Empty));
                    insertColumnsNames.Append("]");
                    insertParameterNames.Append(this.sqlCommand.Parameters[i]);
                }
                this.sqlCommand.CommandText = "insert into " + tablename + "(" + insertColumnsNames + ")\nvalues (" + insertParameterNames + ")";
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                this.sqlConnection.Close();
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public int ExecuteUpdateQuery(string tablename, string primarykeyname, string primarykeyvalue)
        {
            StringBuilder updateColumnsNames, updateParameterNames;

            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteUpdateQuery()");
                if (qryText.Length > 0)
                {
                    qryText = new StringBuilder();
                }
                this.sqlConnection.Open();
                updateColumnsNames = new StringBuilder();
                updateParameterNames = new StringBuilder();
                qryText.Append("update ");
                qryText.AppendLine(tablename);
                qryText.Append(" set ");
                for (int i = 0; i < this.sqlCommand.Parameters.Count; i++)
                {
                    if (i > 0)
                    {
                        qryText.Append(",");
                    }
                    qryText.Append("[");
                    qryText.Append(this.sqlCommand.Parameters[i].ParameterName.Replace("@", string.Empty));
                    qryText.Append("] = ");
                    qryText.AppendLine(this.sqlCommand.Parameters[i].ParameterName);
                }
                qryText.AppendLine("where [" + primarykeyname + "] = @primarykeyvalue");
                AddParameter("@primarykeyvalue", primarykeyvalue);
                this.sqlCommand.CommandText = qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                this.sqlConnection.Close();
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteUpdateQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public void ExecuteReader()
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteReader()");
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.reader = this.sqlCommand.ExecuteReader();
                this.sqlLogEntry.Stop();
                this.RowCount = this.reader.RecordsAffected;
            }
            finally
            {
                this.sqlConnection.Close();
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteReader(): " + sqlCommand.CommandText);
            }
        }
        //------------------------------------------------------------------------------------
        //public void Open()
        //{            
        //    FwFunc.WriteLog("Begin FwSqlCommand:Open()");
        //    this.RowCount = 0;
        //    this.sqlConnection.Open();
        //    this.sqlCommand.CommandText = this.qryText.ToString();
        //    FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
        //    if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
        //    {                
        //        this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
        //        this.sqlLogEntry.Start();
        //        this.RowCount = this.sqlCommand.ExecuteNonQuery();                
        //        this.sqlLogEntry.Stop();
        //    }
        //    else
        //    {
        //        this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
        //        this.sqlLogEntry.Start();
        //        this.reader = this.sqlCommand.ExecuteReader();
        //        this.sqlLogEntry.Stop();
        //        this.eof = (!this.reader.HasRows);
        //        if (!eof)
        //        {
        //            this.RowCount++;
        //            this.Next();
        //        }
        //        while (this.reader.Read())
        //        {
        //            this.RowCount++;
        //        }
        //    }
        //    FwFunc.WriteLog("End FwSqlCommand:Open()");
        //}        
        //------------------------------------------------------------------------------------
        public bool Next()
        {
            //this.RowCount++;
            this.eof = (!this.reader.Read());
            if (!this.eof)
            {
                this.SetFields();
            }

            return !this.eof;
        }
        //------------------------------------------------------------------------------------
        public void SetFields()
        {
            for (int i = 0; i < this.reader.FieldCount; i++)
            {
                this.fields.SetField(this.reader.GetName(i), this.reader[i]);
            }
        }
        //------------------------------------------------------------------------------------
        public FwDatabaseField GetField(string fieldName)
        {
            FwDatabaseField field;

            field = new FwDatabaseField();
            field.FieldValue = DBNull.Value;
            if (!this.eof)
            {
                field.FieldValue = this.fields.GetField(fieldName);
            }
            if (field.FieldValue == null || field.FieldValue == DBNull.Value)
            {
                field.FieldValue = string.Empty;
            }

            return field;
        }
        //------------------------------------------------------------------------------------
        public FwDatabaseField GetField(string fieldName, SqlDbType type)
        {
            FwDatabaseField field;

            field = new FwDatabaseField();
            field.FieldValue = this.fields.GetField(fieldName);
            if (field.FieldValue == null || field.FieldValue == DBNull.Value)
            {
                switch (type)
                {
                    case SqlDbType.DateTime:
                        field.FieldValue = DateTime.MinValue;
                        break;
                    default:
                        field.FieldValue = string.Empty;
                        break;
                }
            }

            return field;
        }
        //------------------------------------------------------------------------------------
        public Dictionary<string, FwDatabaseField> GetFwDatabaseFields()
        {
            Dictionary<string, FwDatabaseField> dbfields = this.fields.GetFwDatabaseFields();
            return dbfields;
        }
        //------------------------------------------------------------------------------------
        //public void Close()
        //{
        //    if (reader != null)
        //    {
        //        reader.Close();
        //        reader = null;
        //    }
        //    sqlConnection.Close();
        //}
        //------------------------------------------------------------------------------------
        //public DataTable QueryToTable()
        //{
        //    SqlDataAdapter adapter;
        //    DataTable dt;

        //    //FwFunc.WriteLog("Begin FwSqlCommand:QueryToTable()");
        //    sqlCommand.CommandText = qryText.ToString();
        //    //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
        //    adapter = new SqlDataAdapter(sqlCommand);
        //    dt = new DataTable();
        //    adapter.Fill(dt);
        //    //FwFunc.WriteLog("End FwSqlCommand:QueryToTable()");

        //    return dt;
        //}
        //------------------------------------------------------------------------------------
        //public FwJsonDataTable QueryToFwJsonTable()
        //{
        //    return QueryToFwJsonTable(0, 0);
        //}
        //------------------------------------------------------------------------------------
        //public FwJsonDataTable QueryToFwJsonTable(int pageNo, int pageSize)
        //{
        //    FwSqlSelect select;

        //    select = new FwSqlSelect();
        //    select.PageNo = pageNo;
        //    select.PageSize = pageSize;
        //    select.Add(this.qryText.ToString());
        //    select.Parse();

        //    return QueryToFwJsonTable(select, false);
        //}
        //------------------------------------------------------------------------------------
        //public FwJsonDataTable QueryToFwJsonTable(FwSqlSelect select, bool includeAllColumns)
        //{
        //    FwJsonDataTable dt;

        //    dt = new FwJsonDataTable();
        //    dt.PageNo = select.PageNo;
        //    dt.PageSize = select.PageSize;
        //    select.EnablePaging = ((select.PageNo != 0) && (select.PageSize != 0));
        //    select.SetQuery(this);

        //    QueryToFwJsonTable(dt, this.qryText.ToString(), includeAllColumns);

        //    return dt;
        //}
        //------------------------------------------------------------------------------------
        public FwJsonDataTable QueryToFwJsonTable(bool includeAllColumns)
        {
            FwJsonDataTable dt;

            dt = new FwJsonDataTable();

            QueryToFwJsonTable(dt, this.qryText.ToString(), includeAllColumns);

            return dt;
        }
        //------------------------------------------------------------------------------------
        private FwJsonDataTable QueryToFwJsonTable(FwJsonDataTable dt, string qryText, bool includeAllColumns)
        {
            List<string> readerColumns;
            List<object> row;
            int ordinal;
            string fieldName;
            object data;

            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToFwJsonTable()");
                this.sqlCommand.CommandText = qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);
                this.sqlCommand.Connection.Open();
                using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
                {
                    if (!includeAllColumns)
                    {
                        dt.Columns = columns;
                    }
                    else
                    {
                        for (int fieldno = 0; fieldno < reader.FieldCount; fieldno++)
                        {
                            bool found = false;
                            string colname = string.Empty;
                            for (int colno = 0; colno < columns.Count; colno++)
                            {
                                colname = reader.GetName(fieldno);
                                if (colname == columns[colno].DataField)
                                {
                                    found = true;
                                    dt.Columns.Add(columns[colno]);
                                    break;
                                }
                            }
                            if (!found)
                            {
                                dt.Columns.Add(new FwJsonDataTableColumn(colname, colname, FwDataTypes.Text));
                            }
                        }
                        columns = dt.Columns;
                    }
                    readerColumns = null;
                    dt.Rows = new List<List<object>>();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        dt.ColumnIndex[columns[i].DataField] = i;
                    }
                    while (reader.Read())
                    {
                        if (readerColumns == null)
                        {
                            readerColumns = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                fieldName = reader.GetName(i);
                                readerColumns.Add(fieldName);
                                if (fieldName == "totalrows")
                                {
                                    ordinal = reader.GetOrdinal(fieldName);
                                    dt.TotalRows = reader.GetInt32(ordinal);
                                    dt.TotalPages = (int)Math.Ceiling((double)dt.TotalRows / (double)dt.PageSize);
                                }
                            }
                        }
                        row = new List<object>();
                        for (int i = 0; i < columns.Count; i++)
                        {
                            if (readerColumns.Contains(dt.Columns[i].DataField))
                            {
                                ordinal = reader.GetOrdinal(dt.Columns[i].DataField);
                                data = string.Empty;
                                if (dt.Columns[i].IsUniqueId)
                                {
                                    //data = FwCryptography.AjaxEncrypt(reader.GetValue(ordinal).ToString().Trim());
                                    data = reader.GetValue(ordinal).ToString().Trim();
                                }
                                else
                                {
                                    switch (dt.Columns[i].DataType)
                                    {
                                        case FwDataTypes.Text:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = reader.GetValue(ordinal).ToString().Trim();
                                            }
                                            else
                                            {
                                                data = string.Empty;
                                            }
                                            break;
                                        case FwDataTypes.Date:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.Time:
                                            if (!reader.IsDBNull(ordinal) && !string.IsNullOrWhiteSpace(reader.GetValue(ordinal).ToString()))
                                            {
                                                data = FwConvert.ToShortTime12(reader.GetValue(ordinal).ToString());
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.DateTime:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateTimeString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.DateTimeOffset:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                //data = new FwDatabaseField(reader.GetDateTimeOffset(ordinal)).ToShortDateTimeString();
                                                data = (reader.GetDateTimeOffset(ordinal)).LocalDateTime;
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.Decimal:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = reader.GetDecimal(ordinal);
                                            }
                                            else
                                            {
                                                data = 0.0m;
                                            }
                                            break;
                                        case FwDataTypes.Boolean:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToBoolean(reader.GetString(ordinal).TrimEnd());
                                            }
                                            else
                                            {
                                                data = false;
                                            }
                                            break;
                                        case FwDataTypes.CurrencyString:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyString(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyString(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.CurrencyStringNoDollarSign:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.PhoneUS:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToPhoneUS(reader.GetString(ordinal));
                                            }
                                            else
                                            {
                                                data = String.Empty;
                                            }
                                            break;
                                        case FwDataTypes.ZipcodeUS:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToZipcodeUS(reader.GetString(ordinal));
                                            }
                                            else
                                            {
                                                data = String.Empty;
                                            }
                                            break;
                                        case FwDataTypes.Percentage:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(FwConvert.ToDecimal(reader.GetValue(ordinal))) + "%";
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(0.0m) + "%";
                                            }
                                            break;
                                        case FwDataTypes.OleToHtmlColor:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.OleToHex(reader.GetInt32(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.OleToHex(0);
                                            }
                                            break;
                                        case FwDataTypes.Integer:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToInt32(reader.GetValue(ordinal));
                                            }
                                            else
                                            {
                                                data = 0;
                                            }
                                            break;
                                        case FwDataTypes.JpgDataUrl:
                                            data = string.Empty;
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                byte[] buffer = reader.GetSqlBytes(ordinal).Value;
                                                bool isnull = (buffer.Length == 0) || ((buffer.Length == 1) && (buffer[0] == 255));
                                                if (!isnull)
                                                {
                                                    string base64data = Convert.ToBase64String(buffer);
                                                    data = "data:image/jpg;base64," + base64data;
                                                }
                                            }
                                            break;
                                        case FwDataTypes.UTCDateTime:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToUniversalIso8601DateTimeString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                    }
                                }
                                row.Add(data);
                            }
                        }
                        dt.Rows.Add(row);
                    }
                    if (dt.PageNo == 0 && dt.PageSize == 0)
                    {
                        dt.TotalRows = dt.Rows.Count;
                    }
                }
            }
            finally
            {
                this.sqlCommand.Connection.Close();
                //FwFunc.WriteLog("End FwSqlCommand:QueryToFwJsonTable()");
            }

            return dt;
        }
        //------------------------------------------------------------------------------------
        //public FwJsonDataTable QueryToFwJsonTable(FwSqlSelect select, FwApplicationSchema.Browse browseSchema)
        //{
        //    FwJsonDataTable dt;
        //    SqlDataReader reader;
        //    List<string> readerColumns;
        //    List<object> row;
        //    int ordinal;
        //    string fieldName, colname, dataType;
        //    object value;
        //    DataTable dtSchema;

        //    try
        //    {
        //        FwFunc.WriteLog("Begin FwSqlCommand:QueryToFwJsonTable(select): " + sqlCommand.CommandText);
        //        dt = new FwJsonDataTable();
        //        dt.PageNo = select.PageNo;
        //        dt.PageSize = select.PageSize;
        //        select.SetQuery(this);
        //        this.sqlCommand.CommandText = this.qryText.ToString();
        //        FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
        //        this.sqlCommand.Connection.Open();
        //        reader = this.sqlCommand.ExecuteReader();
        //        //dt.Columns = columns;
        //        dtSchema = reader.GetSchemaTable();
        //        for (int rowno = 0; rowno < dtSchema.Rows.Count; rowno++)
        //        {
        //            FwJsonDataTableColumn col;
        //            colname = dtSchema.Rows[rowno][0].ToString();
        //            for (int colno = 0; colno < columns.Count; colno++)
        //            {
        //                if (colname == columns[colno].DataField)
        //                {
        //                    col = columns[colno];
        //                    if (string.IsNullOrEmpty(col.Name))
        //                    {
        //                        if (browseSchema.UniqueIds.ContainsKey(colname))
        //                        {
        //                            col.IsUniqueId = true;
        //                            col.IsVisible = false;
        //                        }
        //                        if (browseSchema.Columns.ContainsKey(colname))
        //                        {
        //                            if (browseSchema.Columns[colname].ExportToExcel == false)
        //                            {
        //                                col.IsVisible = false;
        //                            }
        //                            col.Name = browseSchema.Columns[colname].Caption;
        //                        }
        //                        else
        //                        {
        //                            col.Name = colname;
        //                        }
        //                    }
        //                    dataType = string.Empty; 
        //                    if (browseSchema.UniqueIds.ContainsKey(colname))
        //                    {
        //                        dataType = browseSchema.UniqueIds[colname].DataType;
        //                        col.IsVisible = false;
        //                    }
        //                    if (browseSchema.Columns.ContainsKey(colname))
        //                    {
        //                        dataType = browseSchema.Columns[colname].DataType;
        //                    }
        //                    if (colname == "inactive")
        //                    {
        //                        col.IsVisible = false;
        //                    }
        //                    switch(dataType)
        //                    {
        //                        case "key":
        //                        case "appdocumentimage":
        //                        case "olecolor":
        //                        case "legend":
        //                        case "rowbackgroundcolor":
        //                        case "rowtextcolor":
        //                            col.IsVisible = false;
        //                            break;
        //                    }
        //                    dt.Columns.Add(col);
        //                    break;
        //                }
        //            }
        //        }
        //        columns = dt.Columns;
        //        readerColumns = null;
        //        dt.Rows = new List<List<object>>();
        //        for (int i = 0; i < columns.Count; i++)
        //        {
        //            dt.ColumnIndex[columns[i].DataField] = i;
        //        }
        //        while (reader.Read())
        //        {                
        //            if (readerColumns == null)
        //            {
        //                readerColumns = new List<string>();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    fieldName = reader.GetName(i);
        //                    readerColumns.Add(fieldName);
        //                    if (fieldName == "totalrows")
        //                    {
        //                        ordinal = reader.GetOrdinal(fieldName);
        //                        dt.TotalRows = reader.GetInt32(ordinal);
        //                        dt.TotalPages = (int)Math.Ceiling((double)dt.TotalRows / (double)dt.PageSize);
        //                    }
        //                }
        //            }
        //            row = new List<object>();
        //            for (int i = 0; i < columns.Count; i++)
        //            {
        //                if (readerColumns.Contains(dt.Columns[i].DataField))
        //                {
        //                    ordinal  = reader.GetOrdinal(dt.Columns[i].DataField);
        //                    value    = string.Empty;
        //                    dataType = string.Empty;
        //                    if (dt.Columns[i].IsUniqueId)
        //                    {
        //                        dataType = browseSchema.UniqueIds[dt.Columns[i].DataField].DataType;
        //                    }
        //                    else
        //                    {
        //                        dataType = browseSchema.Columns[dt.Columns[i].DataField].DataType;
        //                    }
        //                    if (!reader.IsDBNull(ordinal))
        //                    {
        //                        switch(dataType)
        //                        {
        //                            case "key":
        //                            case "validation":
        //                                value = FwCryptography.AjaxEncrypt(new FwDatabaseField(reader.GetValue(ordinal)).ToString().Trim());
        //                                break;
        //                            case "text":
        //                                value = new FwDatabaseField(reader.GetValue(ordinal)).ToString().Trim();
        //                                break;
        //                            case "utcdatetime":
        //                            case "utcdate":
        //                                //value = reader.GetDateTime(ordinal).ToString("yyyy-MM-dd HH:mm:ss UTC");   //MY 2015-10-15: This was causing IE and FireFox to put 'Invalid Date' in place of UTC DateTimes
        //                                value = reader.GetDateTime(ordinal).ToString("yyyy-MM-ddTHH:mm:ssZ");        //MY 2015-10-15: This is the correct format that works across all browsers
        //                                break;
        //                            case "utctime":
        //                                value = reader.GetValue(ordinal);
        //                                if (value is System.String)
        //                                {
        //                                    //value = Convert.ToDateTime(value);
        //                                    value = Convert.ToDateTime(value).ToString("yyyy-MM-ddTHH:mm:ssZ");
        //                                }
        //                                break;
        //                            case "date":
        //                                value = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateString();
        //                                break;
        //                            case "time":
        //                                value = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortTimeString();
        //                                break;
        //                            case "datetime":
        //                                value = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateTimeString();
        //                                break;
        //                            case "number":
        //                                value = reader.GetDecimal(ordinal);
        //                                break;
        //                            case "bool":
        //                                value = new FwDatabaseField(reader.GetString(ordinal)).ToBoolean();
        //                                break;
        //                            case "olecolor":
        //                            case "rowbackgroundcolor":
        //                            case "rowtextcolor":
        //                                value = new FwDatabaseField(reader.GetInt32(ordinal)).ToHtmlColor();
        //                                break;
        //                            case "appdocumentimage":
        //                                value = FwCryptography.AjaxEncrypt(reader.GetString(ordinal).TrimEnd());
        //                                break;
        //                            default:
        //                                value = new FwDatabaseField(reader.GetValue(ordinal)).ToString().Trim();
        //                                break;
        //                        }
        //                    }
        //                    else 
        //                    {
        //                        switch (dataType) {
        //                            case "rowbackgroundcolor":
        //                                value = "#ffffff";
        //                                break;
        //                            case "rowtextcolor":
        //                                value = "#000000";
        //                                break;
        //                        }
        //                    }
        //                    row.Add(value);
        //                }
        //            }
        //            dt.Rows.Add(row);
        //        }
        //        reader.Close();
        //    }
        //    finally
        //    {
        //        this.sqlCommand.Connection.Close();
        //        FwFunc.WriteLog("End FwSqlCommand:QueryToFwJsonTable(select)");
        //    }

        //    return dt;
        //}
        //------------------------------------------------------------------------------------
        public void Execute()
        {
            Execute(true);
        }
        //------------------------------------------------------------------------------------
        public void Execute(bool closeConnection)
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand: Execute()");
                this.RowCount = 0;
                if (closeConnection)
                {
                    this.sqlConnection.Open();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                    this.sqlLogEntry.Start();
                    this.RowCount = this.sqlCommand.ExecuteNonQuery();
                    this.sqlLogEntry.Stop();
                }
                else
                {
                    this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                    this.sqlLogEntry.Start();
                    using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
                    {
                        this.sqlLogEntry.Stop();
                        this.eof = (!this.reader.HasRows);
                        if (!eof)
                        {
                            this.RowCount++;
                            this.Next();
                        }
                        while (this.reader.Read())
                        {
                            this.RowCount++;
                        }
                    }
                }
            }
            finally
            {
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand: Execute()");
            }
        }
        //------------------------------------------------------------------------------------
        [System.Obsolete("Please use QueryToDynamicList2 instead, which adds support for column formatters.")]
        public List<dynamic> QueryToDynamicList()
        {
            return QueryToDynamicList(true);
        }
        //------------------------------------------------------------------------------------
        [System.Obsolete("Please use QueryToDynamicList2 instead, which adds support for column formatters.")]
        public List<dynamic> QueryToDynamicList(bool closeConnection)
        {
            List<dynamic> rows;
            dynamic rowObj;
            IDictionary<string, object> row;
            string fieldName;

            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToDynamicList()");
                rows = new List<dynamic>();
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Open();
                }
                using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rowObj = new ExpandoObject();
                        row = (IDictionary<string, object>)rowObj;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i);
                            row[fieldName] = reader.GetValue(i);
                            if (row[fieldName] is String)
                            {
                                row[fieldName] = (row[fieldName] as String).TrimEnd();
                            }
                            else if (row[fieldName] is DateTime)
                            {
                                row[fieldName] = new FwDatabaseField(row[fieldName]).ToShortDateTimeString();
                            }
                        }
                        rows.Add(row);
                    }
                }
            }
            finally
            {
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:QueryToDynamicList()");
            }

            return rows;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Renders to a javascript array of objects.  While easier to use from JavaScript that FwJsonDataTable, this will use a lot more bandwidth, because the column names are repeated with each row.  Uses the columns format the data that gets generated.  The only reason there is a second version of this function is because of concerns for breaking older code. This is a project for another day.
        /// </summary>
        /// <param name="closeConnection">False will cause it to not close the connection.  Useful for transactions.</param>
        /// <returns></returns>
        public List<dynamic> QueryToDynamicList2()
        {
            return QueryToDynamicList2(true);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Renders to a javascript array of objects.  While easier to use from JavaScript that FwJsonDataTable, this will use a lot more bandwidth, because the column names are repeated with each row.  Uses the columns format the data that gets generated.  The only reason there is a second version of this function is because of concerns for breaking older code. This is a project for another day.
        /// </summary>
        /// <param name="closeConnection">False will cause it to not close the connection.  Useful for transactions.</param>
        /// <returns></returns>
        public List<dynamic> QueryToDynamicList2(bool closeConnection)
        {
            List<dynamic> rows;
            dynamic rowObj;
            IDictionary<string, object> row;
            string fieldName;

            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToDynamicList()");
                rows = new List<dynamic>();
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Open();
                }
                using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
                {


                    Dictionary<string, FwJsonDataTableColumn> indexedColumns = new Dictionary<string, FwJsonDataTableColumn>();
                    for (int colno = 0; colno < columns.Count; colno++)
                    {
                        FwJsonDataTableColumn column = columns[colno];
                        indexedColumns[column.DataField] = column;
                    }
                    while (reader.Read())
                    {
                        rowObj = new ExpandoObject();
                        row = (IDictionary<string, object>)rowObj;
                        for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                        {
                            fieldName = reader.GetName(ordinal);
                            object data = string.Empty;
                            if (!indexedColumns.ContainsKey(fieldName))
                            {
                                // don't format the data
                                data = reader.GetValue(ordinal);
                                if (data is String)
                                {
                                    data = ((string)data).TrimEnd();
                                }
                                else if (data is DateTime)
                                {
                                    data = new FwDatabaseField(data).ToShortDateTimeString();
                                }

                                if (reader.IsDBNull(ordinal))
                                {
                                    data = string.Empty;
                                }
                            }
                            else
                            {
                                // use the specified column formatter
                                FwJsonDataTableColumn column = indexedColumns[fieldName];
                                if (indexedColumns[fieldName].IsUniqueId)
                                {
                                    //data = FwCryptography.AjaxEncrypt(reader.GetValue(ordinal).ToString().Trim());
                                    data = reader.GetValue(ordinal).ToString();
                                }
                                else
                                {
                                    switch (column.DataType)
                                    {
                                        case FwDataTypes.Text:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = reader.GetValue(ordinal).ToString().Trim();
                                            }
                                            else
                                            {
                                                data = string.Empty;
                                            }
                                            break;
                                        case FwDataTypes.Date:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.Time:
                                            if (!reader.IsDBNull(ordinal) && !string.IsNullOrWhiteSpace(reader.GetValue(ordinal).ToString()))
                                            {
                                                data = FwConvert.ToShortTime12(reader.GetValue(ordinal).ToString());
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.DateTime:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToShortDateTimeString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                        case FwDataTypes.Decimal:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = reader.GetDecimal(ordinal);
                                            }
                                            else
                                            {
                                                data = 0.0m;
                                            }
                                            break;
                                        case FwDataTypes.Boolean:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToBoolean(reader.GetString(ordinal).TrimEnd());
                                            }
                                            else
                                            {
                                                data = false;
                                            }
                                            break;
                                        case FwDataTypes.CurrencyString:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyString(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyString(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.CurrencyStringNoDollarSign:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(reader.GetDecimal(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(0.0m);
                                            }
                                            break;
                                        case FwDataTypes.PhoneUS:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToPhoneUS(reader.GetString(ordinal));
                                            }
                                            else
                                            {
                                                data = String.Empty;
                                            }
                                            break;
                                        case FwDataTypes.ZipcodeUS:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToZipcodeUS(reader.GetString(ordinal));
                                            }
                                            else
                                            {
                                                data = String.Empty;
                                            }
                                            break;
                                        case FwDataTypes.Percentage:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(FwConvert.ToDecimal(reader.GetValue(ordinal))) + "%";
                                            }
                                            else
                                            {
                                                data = FwConvert.ToCurrencyStringNoDollarSign(0.0m) + "%";
                                            }
                                            break;
                                        case FwDataTypes.OleToHtmlColor:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.OleToHex(reader.GetInt32(ordinal));
                                            }
                                            else
                                            {
                                                data = FwConvert.OleToHex(0);
                                            }
                                            break;
                                        case FwDataTypes.Integer:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = FwConvert.ToInt32(reader.GetValue(ordinal));
                                            }
                                            else
                                            {
                                                data = 0;
                                            }
                                            break;
                                        case FwDataTypes.JpgDataUrl:
                                            data = string.Empty;
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                byte[] buffer = reader.GetSqlBytes(ordinal).Value;
                                                bool isnull = (buffer.Length == 0) || ((buffer.Length == 1) && (buffer[0] == 255));
                                                if (!isnull)
                                                {
                                                    string base64data = Convert.ToBase64String(buffer);
                                                    data = "data:image/jpg;base64," + base64data;
                                                }
                                            }
                                            break;
                                        case FwDataTypes.UTCDateTime:
                                            if (!reader.IsDBNull(ordinal))
                                            {
                                                data = new FwDatabaseField(reader.GetDateTime(ordinal)).ToUniversalIso8601DateTimeString();
                                            }
                                            else
                                            {
                                                data = "";
                                            }
                                            break;
                                    }
                                }
                            }
                            row[fieldName] = data;
                        }
                        rows.Add(row);
                    }
                }
            }
            finally
            {
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:QueryToDynamicList()");
            }

            return rows;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Run a query and return a typed list, by using QueryToDynamicList2, and then serializing it to json than deserializing to the desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> QueryToTypedList<T>()
        {
            string json = JsonConvert.SerializeObject(QueryToDynamicList2());
            List<T> results = JsonConvert.DeserializeObject<List<T>>(json);
            return results;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Run a query and return a typed object, by using QueryToDynamicObject2, and then serializing it to json than deserializing to the desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryToTypedObject<T>()
        {
            string json = JsonConvert.SerializeObject(QueryToDynamicObject2());
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Render a query as a JavaScript object without using the column formatters
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("Please use QueryToDynamicObject2 instead, which adds support for column formatters.")]
        public dynamic QueryToDynamicObject()
        {
            dynamic result;
            List<dynamic> results;

            results = QueryToDynamicList();
            if (results.Count > 0)
            {
                result = results[0];
            }
            else
            {
                result = null;
            }
            return result;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Render a query as a JavaScript object using the column formatters
        /// </summary>
        /// <returns></returns>
        public dynamic QueryToDynamicObject2()
        {
            dynamic result;
            List<dynamic> results;

            results = QueryToDynamicList2();
            if (results.Count > 0)
            {
                result = results[0];
            }
            else
            {
                result = null;
            }
            return result;
        }
        //------------------------------------------------------------------------------------
        public void Dispose()
        {
            if (this.sqlCommand != null)
            {
                this.sqlCommand.Dispose();
            }
        }
        //------------------------------------------------------------------------------------
        public static FwDatabaseField GetData(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwSqlCommand qry;
            FwDatabaseField result;

            qry = new FwSqlCommand(conn, timeout);
            qry.Add("select top 1 " + selectcolumn);
            qry.Add("from " + tablename + " with (nolock)");
            qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
            qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            qry.Execute();
            result = (qry.RowCount == 1) ? qry.GetField(selectcolumn) : null;

            return result;
        }

        //------------------------------------------------------------------------------------
        public static string GetStringData(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            string result = string.Empty;

            field = FwSqlCommand.GetData(conn, timeout, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToString().TrimEnd() : string.Empty;

            return result;
        }
        //------------------------------------------------------------------------------------
        public static bool GetBoolData(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            bool result;

            field = FwSqlCommand.GetData(conn, timeout, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToBoolean() : false;

            return result;
        }
        //------------------------------------------------------------------------------------
        public static Dictionary<string, FwDatabaseField> GetRow(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, bool throwExceptionIfRowCountNot1)
        {
            FwSqlCommand qry;
            Dictionary<string, FwDatabaseField> result;

            qry = new FwSqlCommand(conn, timeout);
            qry.Add("select top 1 *");
            qry.Add("from " + tablename + " with (nolock)");
            qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
            qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            qry.Execute();
            result = qry.GetFwDatabaseFields();

            if (throwExceptionIfRowCountNot1 && qry.RowCount != 1)
            {
                throw new Exception(string.Format("Expected rowcount to be 1, but found {0} rows. Table/View: {1}, column: {2}, value: {3}", qry.RowCount, tablename, wherecolumn, wherecolumnvalue));
            }

            return result;
        }
        //------------------------------------------------------------------------------------
        public static int SetData(FwSqlConnection conn, int timeout, string table, string wherecolumn, string wherecolumnvalue, string updatecolumn, string updatecolumnvalue)
        {
            FwSqlCommand cmd;

            cmd = new FwSqlCommand(conn, timeout);
            cmd.Add("update " + table);
            cmd.Add("set " + updatecolumn + " = @updatecolumnvalue");
            cmd.Add("where " + wherecolumn + " = @wherecolumnvalue");
            cmd.AddParameter("@updatecolumnvalue", updatecolumnvalue);
            cmd.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            cmd.ExecuteNonQuery();

            return cmd.RowCount;
        }
        //------------------------------------------------------------------------------------
        public string GetParameterizedIn(string paramprefix, List<object> values)
        {
            StringBuilder parameters = new StringBuilder();
            string parameter;
            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    parameters.Append(",");
                }
                parameter = "@" + paramprefix + i.ToString();
                parameters.Append(parameter);
                this.AddParameter(parameter, values[i]);
            }
            return parameters.ToString();
        }
        //------------------------------------------------------------------------------------
        public IEnumerable<T> Select<T>(bool openAndCloseConnection, int pageNo, int pageSize)
        {
            IEnumerable<T> results = null;
            //this.Add("order by " + orderByColumn + " " + orderByDirection.ToString());
            //this.Add("offset @offsetrows rows fetch next @fetchsize rows only");
            //this.Add("for json path");
            this.AddParameter("@offsetrows", (pageNo - 1) * pageSize);
            this.AddParameter("@fetchsize", pageSize);
            if (openAndCloseConnection)
            {
                this.sqlCommand.Connection.Open();
            }
            // the commented code is if you use the "for json" in the sql query
            //this.sqlCommand.CommandText = this.qryText.ToString();
            //if (!this.sqlCommand.CommandText.Contains("order by"))
            //{
            //    throw new Exception("order by expression is required with paged queries.");
            //}
            //using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
            //{
            //    StringBuilder jsonResult = new StringBuilder();
            //    if (!reader.HasRows)
            //    {
            //        jsonResult.Append("[]");
            //    }
            //    else
            //    {
            //        while (reader.Read())
            //        {
            //            jsonResult.Append(reader.GetValue(0).ToString());
            //        }

            //    }
            //    results = JsonConvert.DeserializeObject<List<T>>(jsonResult.ToString());
            //    if (openAndCloseConnection)
            //    {
            //        this.sqlCommand.Connection.Close();
            //    }
            //    return results;
            //}
            // Uses Dapper to perform the conversion from query to object list
            var parameters = new DynamicParameters();
            foreach (SqlParameter p in this.Parameters)
            {
                parameters.Add(p.ParameterName, p.Value);
            }
            results = this.sqlConnection.GetConnection().Query<T>(this.qryText.ToString(), parameters);
            return results;
        }
        //------------------------------------------------------------------------------------
        public T SelectOne<T>(bool openAndCloseConnection)
        {
            IEnumerable<T> records = null;
            if (openAndCloseConnection)
            {
                this.sqlCommand.Connection.Open();
            }
            // Uses Dapper to perform the conversion from query to object list
            var parameters = new DynamicParameters();
            foreach (SqlParameter p in this.Parameters)
            {
                parameters.Add(p.ParameterName, p.Value);
            }
            records = this.sqlConnection.GetConnection().Query<T>(this.qryText.ToString(), parameters);
            T result = default(T);
            var results = records.AsList<T>();
            if (results.Count > 0)
            {
                result = results[0];    
            }
            
            return result;
        }
        //------------------------------------------------------------------------------------
        public int Insert(bool openAndCloseConnection, string tablename, object businessObject, DatabaseConfig dbConfig)
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Open();
                }
                var insertColumnsNames = new StringBuilder();
                var insertParameterNames = new StringBuilder();
                var i = 0;
                var propertyInfos = businessObject.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var hasJsonIgnoreAttribute = propertyInfo.IsDefined(typeof(JsonIgnoreAttribute));
                    if (!hasJsonIgnoreAttribute)
                    {
                        object propertyValue = propertyInfo.GetValue(businessObject);
                        bool hasFwSqlDataFieldAttribute = propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute));
                        string sqlColumnName = propertyInfo.Name;
                        if (hasFwSqlDataFieldAttribute)
                        {
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }
                            if (sqlDataFieldAttribute.IsPrimaryKey)
                            {
                                using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
                                {
                                    propertyValue = FwSqlData.GetNextId(conn, dbConfig);
                                }
                                propertyInfo.SetValue(businessObject, propertyValue);
                            }
                        }
                        if (sqlColumnName.ToLower() == "datestamp" || propertyValue != null)
                        {
                            if (i > 0)
                            {
                                insertColumnsNames.Append(",");
                                insertParameterNames.Append(",");
                            }
                            insertColumnsNames.Append("[");
                            insertColumnsNames.Append(sqlColumnName);
                            insertColumnsNames.Append("]");
                            insertParameterNames.Append("@" + propertyInfo.Name);
                            if (propertyInfo.Name.ToLower() == "datestamp")
                            {
                                propertyValue = DateTime.UtcNow;
                                businessObject.GetType().GetProperty(propertyInfo.Name).SetValue(businessObject, propertyValue);
                                this.AddParameter("@" + propertyInfo.Name, propertyValue);
                            }
                            else
                            {
                                this.AddParameter("@" + propertyInfo.Name, propertyValue);
                            }
                            i++;
                        }
                    }
                }
                this.sqlCommand.CommandText = "insert into " + tablename + "(" + insertColumnsNames + ")\nvalues (" + insertParameterNames + ")";
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public int Update(bool openAndCloseConnection, string tablename, object businessObject)
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Open();
                }
                var setStatements = new StringBuilder();
                var whereClause = new StringBuilder();
                var i = 0;
                var propertyInfos = businessObject.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var hasJsonIgnoreAttribute = propertyInfo.IsDefined(typeof(JsonIgnoreAttribute));
                    if (!hasJsonIgnoreAttribute)
                    {
                        var propertyValue = propertyInfo.GetValue(businessObject);
                        string sqlColumnName = propertyInfo.Name;
                        var hasSqlDataFieldAttribute = propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute));
                        if (hasSqlDataFieldAttribute)
                        {
                            //var sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();

                            FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }

                            if (sqlDataFieldAttribute.IsPrimaryKey)
                            {
                                if (whereClause.Length > 0)
                                {
                                    whereClause.Append("and ");
                                }
                                whereClause.Append(sqlColumnName);
                                whereClause.Append(" = @");
                                whereClause.AppendLine(sqlColumnName);

                                this.AddParameter("@" + sqlColumnName, propertyValue);
                            }

                            else if (propertyInfo.Name == "datestamp" || propertyValue != null)
                            {
                                if (i > 0)
                                {
                                    setStatements.Append("  ,");
                                }
                                setStatements.Append("[");
                                setStatements.Append(sqlColumnName);
                                setStatements.Append("] = @");
                                setStatements.AppendLine(sqlColumnName);
                                if (sqlColumnName == "datestamp")
                                {
                                    propertyValue = DateTime.UtcNow;
                                    businessObject.GetType().GetProperty("datestamp").SetValue(businessObject, propertyValue);
                                    this.AddParameter("@" + sqlColumnName, propertyValue);
                                }
                                else
                                {
                                    this.AddParameter("@" + sqlColumnName, propertyValue);
                                }
                                i++;
                            }
                        }
                    }
                }
                if (whereClause.Length == 0)
                {
                    throw new Exception("Unable to perform update.  Primary key is not defined.");
                }
                StringBuilder sql = new StringBuilder();
                sql.Append("update ");
                sql.AppendLine(tablename);
                sql.Append("set");
                if (setStatements.Length == 0)
                {
                    return 0;
                }
                sql.Append(setStatements);
                sql.AppendLine("where");
                sql.Append(whereClause);
                this.sqlCommand.CommandText = sql.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public int Delete(bool openAndCloseConnection, string tablename, object businessObject)
        {
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Open();
                }
                var setStatements = new StringBuilder();
                var whereClause = new StringBuilder();
                var propertyInfos = businessObject.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var hasJsonIgnoreAttribute = propertyInfo.IsDefined(typeof(JsonIgnoreAttribute));
                    if (!hasJsonIgnoreAttribute)
                    {

                        var propertyValue = propertyInfo.GetValue(businessObject);
                        string sqlColumnName = propertyInfo.Name;
                        var hasSqlDataFieldAttribute = propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute));
                        if (hasSqlDataFieldAttribute)
                        {
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }

                            if (sqlDataFieldAttribute.IsPrimaryKey)
                            {
                                if (whereClause.Length > 0)
                                {
                                    whereClause.Append("and ");
                                }
                                whereClause.Append(sqlColumnName);
                                whereClause.Append(" = @");
                                whereClause.AppendLine(sqlColumnName);

                                this.AddParameter("@" + sqlColumnName, propertyValue);
                            }
                        }
                    }
                }
                if (whereClause.Length == 0)
                {
                    throw new Exception("Unable to perform delete.  Primary key is not defined.");
                }
                var sql = new StringBuilder();
                sql.Append("delete from ");
                sql.AppendLine(tablename);
                sql.AppendLine("where");
                sql.Append(whereClause);
                this.sqlCommand.CommandText = sql.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public int Delete(bool openAndCloseConnection, string tablename, string whereClause, DeleteRequestDto request)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");
                sql.AppendLine("delete from " + tablename);
                sql.AppendLine(whereClause);
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Open();
                }
                this.sqlCommand.CommandText = sql.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand);
                this.sqlLogEntry.Start();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
                this.sqlLogEntry.Stop();
            }
            finally
            {
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
    }
}

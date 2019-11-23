using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.SqlServer
{
    //public enum FwQueryTimeouts {Default, Report}
    public class FwSqlCommand : IDisposable
    {
        //------------------------------------------------------------------------------------
        private FwSqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private StringBuilder qryText;
        //private SqlDataReader reader;
        private bool eof;
        private List<FwJsonDataTableColumn> columns;
        private FwFields fields;
        private StringBuilder sql;
        private FwSqlLogEntry sqlLogEntry;
        //------------------------------------------------------------------------------------
        public string Sql { get { return sql.ToString(); } }
        //------------------------------------------------------------------------------------
        public int RowCount { get; private set; }
        public int PageNo { get; set; } = 0;
        public int PageSize { get; set; } = 0;
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
                                try
                                {
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
                                }
                                catch (SqlTypeException ex)
                                {
                                    Console.WriteLine($"Parameter \"{Parameters[i].ParameterName}\" is invalid for sql type {Parameters[i].SqlDbType.ToString()}, value: \"{Parameters[i].Value.ToString()}\".\n\n{ex.Message}\n{ex.StackTrace}\n");
                                    if (Parameters[i].Value != null)
                                    {
                                        sb.Append("'");
                                        sb.Append(Parameters[i].Value.ToString());
                                        sb.AppendLine("'");
                                    }
                                    else
                                    {
                                        sb.AppendLine("null");
                                    }
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
                                try
                                {
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
                                }
                                catch (SqlTypeException ex)
                                {
                                    Console.WriteLine($"Parameter \"{Parameters[i].ParameterName}\" is invalid for sql type {Parameters[i].SqlDbType.ToString()}, value: \"{Parameters[i].Value.ToString()}\".\n\n{ex.Message}\n{ex.StackTrace}\n");
                                    if (Parameters[i].Value != null)
                                    {
                                        sb.Append("'");
                                        sb.Append(Parameters[i].Value.ToString());
                                        sb.AppendLine("'");
                                    }
                                    else
                                    {
                                        sb.AppendLine("null");
                                    }
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
                                try
                                {
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
                                }
                                catch (SqlTypeException ex)
                                {
                                    Console.WriteLine($"Parameter \"{Parameters[i].ParameterName}\" is invalid for sql type {Parameters[i].SqlDbType.ToString()}, value: \"{Parameters[i].Value.ToString()}\".\n\n{ex.Message}\n{ex.StackTrace}\n");
                                    if (Parameters[i].Value != null)
                                    {
                                        sb.Append("'");
                                        sb.Append(Parameters[i].Value.ToString());
                                        sb.AppendLine("'");
                                    }
                                    else
                                    {
                                        sb.AppendLine("null");
                                    }
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
            this.Transaction = conn.GetActiveTransaction();
            this.qryText = new StringBuilder();
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
            this.Transaction = conn.GetActiveTransaction();
            this.qryText = new StringBuilder();
            this.qryText.Append(storedProcedureName);
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
            if (value is bool?)
            {
                bool? b = (bool?)value;
                value = b.GetValueOrDefault(false) ? "T" : "F";
            }
            else if (value is bool)
            {
                bool b = (bool)value;
                value = b ? "T" : "F";
            }


            SqlParameter param = this.sqlCommand.Parameters.AddWithValue(name, value);

            //justin 05/07/2018  override the default type of nvarchar here.  We want varchar to avoid implicit conversion in the query optimizer
            if (value is string)
            {
                param.SqlDbType = SqlDbType.VarChar;
            }
        }
        //------------------------------------------------------------------------------------
        public void AddParameter(string name, SqlDbType sqlDbType, ParameterDirection direction, object value)
        {
            if (value is bool?)
            {
                bool? b = (bool?)value;
                value = b.GetValueOrDefault(false) ? "T" : "F";
            }
            else if (value is bool)
            {
                bool b = (bool)value;
                value = b ? "T" : "F";
            }
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
        public void AddColumn(string name, string dataField)
        {
            FwJsonDataTableColumn column = new FwJsonDataTableColumn(dataField, false);
            column.Name = name;
            //column.DataType = dataType;
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
        public async Task<int> ExecuteNonQueryAsync()
        {
            bool closeConnection = true;
            try
            {
                this.RowCount = 0;
                string methodName = "ExecuteNonQueryAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteNonQuery()");

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
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
                this.sqlLogEntry.Stop(this.RowCount);
                if (closeConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteNonQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public async Task<int> ExecuteInsertQueryAsync(string tablename)
        {
            bool closeConnection = true;
            StringBuilder insertColumnsNames, insertParameterNames;

            try
            {
                this.RowCount = 0;
                string methodName = "ExecuteInsertQueryAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteInsertQuery()");

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
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
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
                if (closeConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteInsertQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public async Task<int> ExecuteUpdateQueryAsync(string tablename, string primarykeyname, string primarykeyvalue)
        {
            bool closeConnection = true;
            StringBuilder updateColumnsNames, updateParameterNames;

            try
            {
                this.RowCount = 0;
                string methodName = "ExecuteUpdateQueryAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteUpdateQuery()");
                if (qryText.Length > 0)
                {
                    qryText = new StringBuilder();
                }

                closeConnection = !sqlConnection.IsOpen();
                if (closeConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
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
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);

                if (closeConnection)
                {
                    this.sqlConnection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:ExecuteUpdateQuery()");
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public async Task ExecuteReaderAsync()
        {
            bool closeConnection = true;
            try
            {
                this.RowCount = 0;
                string methodName = "ExecuteReaderAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);
                //FwFunc.WriteLog("Begin FwSqlCommand:ExecuteReader()");

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    this.RowCount = reader.RecordsAffected;
                }
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);

                if (closeConnection)
                {
                    this.sqlConnection.Close();
                }
            }
        }
        //------------------------------------------------------------------------------------
        public bool Next(SqlDataReader reader)
        {
            this.eof = (!reader.Read());
            if (!this.eof)
            {
                this.SetFields(reader);
            }

            return !this.eof;
        }
        //------------------------------------------------------------------------------------
        public void SetFields(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                this.fields.SetField(reader.GetName(i), reader[i]);
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
        public async Task<FwJsonDataTable> QueryToFwJsonTableAsync(bool includeAllColumns = true, int pageNo = 0, int pageSize = 0, int top = 0)
        {
            FwSqlSelect select = new FwSqlSelect();
            select.EnablePaging = (pageNo != 0) && (pageSize != 0);
            select.PageNo = pageNo;
            select.PageSize = pageSize;
            select.Top = top;
            select.Add(this.qryText.ToString());
            FwJsonDataTable dt = await QueryToFwJsonTableAsync(select, includeAllColumns);
            return dt;
        }
        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> QueryToFwJsonTableAsync(FwSqlSelect select, bool includeAllColumns)
        {
            if (!select.Parsed)
            {
                select.Parse();
            }
            select.SetQuery(this);
            return await QueryToFwJsonTableAsync(select.PageNo, select.PageSize, includeAllColumns, select.TotalFields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageno">This is the pageno you want returned in the DataTable.  This will not page your query for you.</param>
        /// <param name="pagesize">This is the size you want returned in the DataTable.  This will not page your query for you.</param>
        /// <param name="query">Pass null if you already set the query on this FwSqlCommand.</param>
        /// <param name="includeAllColumns"></param>
        /// <returns></returns>
        private async Task<FwJsonDataTable> QueryToFwJsonTableAsync(int pageNo, int pageSize, bool includeAllColumns, List<string> totalFields)
        {
            List<string> readerColumns;
            List<object> row;
            int ordinal;
            string fieldName;
            object data;
            FwJsonDataTable dt;

            bool closeConnection = true;
            try
            {
                this.RowCount = 0;
                string methodName = "QueryToFwJsonTableAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToFwJsonTable()");
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);
                this.sqlCommand.CommandText = this.qryText.ToString();
                dt = new FwJsonDataTable();
                dt.PageNo = pageNo;
                dt.PageSize = pageSize;

                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();

                closeConnection = (!this.sqlConnection.IsOpen());
                if (closeConnection)
                {
                    await this.sqlCommand.Connection.OpenAsync();
                }

                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    this.sqlLogEntry.WriteToConsole("opened", true);
                    if (!includeAllColumns)
                    {
                        dt.Columns = columns;
                    }
                    else
                    {
                        for (int fieldno = 0; fieldno < reader.FieldCount; fieldno++)
                        {
                            bool found = false;
                            bool pagingEnabled = (pageNo != 0 || pageSize != 0);
                            if (pagingEnabled && (fieldno == 0 || fieldno == reader.FieldCount - 1))
                            {
                                dt.Columns.Add(columns[0]);
                                found = true;
                            }
                            if (!found)
                            {
                                //string colname = string.Empty;
                                string colname = reader.GetName(fieldno);  //jason and justin 01/31/2019 
                                for (int colno = 0; colno < columns.Count; colno++)
                                {
                                    //colname = reader.GetName(fieldno);
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

                        }
                        columns = dt.Columns;
                    }
                    readerColumns = null;
                    dt.Rows = new List<List<object>>();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        dt.ColumnIndex[columns[i].DataField] = i;
                    }

                    // default all requested totals to zero
                    for (int i = 0; i < totalFields.Count; i++)
                    {
                        dt.Totals.Add(totalFields[i], 0);
                    }

                    while (reader.Read())
                    {
                        this.RowCount++;
                        if (readerColumns == null)
                        {
                            readerColumns = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                fieldName = reader.GetName(i);
                                readerColumns.Add(fieldName);
                                if (fieldName.Equals(FwSqlSelect.TOTAL_ROWS_FIELD))
                                {
                                    ordinal = reader.GetOrdinal(fieldName);
                                    dt.TotalRows = reader.GetInt32(ordinal);
                                    dt.TotalPages = (int)Math.Ceiling((double)dt.TotalRows / (double)dt.PageSize);
                                }
                                else if (totalFields.Contains(fieldName))
                                {
                                    ordinal = reader.GetOrdinal(FwSqlSelect.TOTAL_FIELD_PREFIX + fieldName);
                                    //dt.Totals.Add(fieldName, reader.GetDecimal(ordinal));
                                    dt.Totals[fieldName] = reader.GetDecimal(ordinal);  // just update the total value here instead
                                }
                            }
                        }
                        row = new List<object>();
                        for (int i = 0; i < columns.Count; i++)
                        {
                            //if (readerColumns.Contains(dt.Columns[i].DataField))
                            //{
                            //    ordinal = reader.GetOrdinal(dt.Columns[i].DataField);
                            //    data = string.Empty;
                            //    if (dt.Columns[i].IsUniqueId)
                            //    {
                            //        data = reader.GetValue(ordinal).ToString().Trim();
                            //    }
                            //    else
                            //    {
                            //        data = FormatReaderData(dt.Columns[i].DataType, ordinal, reader);
                            //    }
                            //    row.Add(data);
                            //}

                            ordinal = -1;
                            if (readerColumns.Contains(dt.Columns[i].DataField))
                            {
                                ordinal = reader.GetOrdinal(dt.Columns[i].DataField);
                            }
                            else if (readerColumns.Contains(dt.Columns[i].Name))
                            {
                                ordinal = reader.GetOrdinal(dt.Columns[i].Name);
                            }
                            if (ordinal >= 0)
                            {
                                data = string.Empty;
                                if (dt.Columns[i].IsUniqueId)
                                {
                                    data = reader.GetValue(ordinal).ToString().Trim();
                                }
                                else
                                {
                                    data = FormatReaderData(dt.Columns[i].DataType, ordinal, reader);
                                }
                                row.Add(data);
                            }
                            else
                            {
                                //justin 09/20/2018 the field that the FwJsonDataTable wants is not in the query result set
                                row.Add(null);
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
            catch (Exception ex)
            {
                string logTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine($"{logTime} - An error occured during the execution of an SQL Query.");
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine(this.QryTextDebug);
                Console.WriteLine("------------------------------------------------------------------------------------");
                throw ex;
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);

                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand:QueryToFwJsonTable()");
            }

            return dt;
        }

        object FormatReaderData(FwDataTypes dataType, int columnIndex, SqlDataReader reader)
        {
            object data = string.Empty;
            string numberFormat = "";
            switch (dataType)
            {
                case FwDataTypes.Text:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        //data = reader.GetValue(columnIndex).ToString().Trim();
                        data = reader.GetValue(columnIndex).ToString().TrimEnd();  //justin 02/20/2018 - preserve leading blanks for indenting in grids, etc.
                    }
                    else
                    {
                        data = string.Empty;
                    }
                    break;
                case FwDataTypes.Date:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = new FwDatabaseField(reader.GetDateTime(columnIndex)).ToShortDateString();
                    }
                    else
                    {
                        data = "";
                    }
                    break;
                case FwDataTypes.Time:
                    if (!reader.IsDBNull(columnIndex) && !string.IsNullOrWhiteSpace(reader.GetValue(columnIndex).ToString()))
                    {
                        data = FwConvert.ToShortTime12(reader.GetValue(columnIndex).ToString());
                    }
                    else
                    {
                        data = "";
                    }
                    break;
                case FwDataTypes.DateTime:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDateTime(columnIndex).ToString("yyyy-MM-dd hh:mm:ss tt");
                    }
                    else
                    {
                        data = "";
                    }
                    break;
                case FwDataTypes.DateTimeOffset:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        //data = new FwDatabaseField(reader.GetDateTimeOffset(ordinal)).ToShortDateTimeString();
                        data = (reader.GetDateTimeOffset(columnIndex)).LocalDateTime.ToString("yyyy-MM-dd hh:mm:ss tt");
                    }
                    else
                    {
                        data = "";
                    }
                    break;
                case FwDataTypes.Decimal:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex);
                    }
                    else
                    {
                        data = 0.0m;
                    }
                    break;
                case FwDataTypes.DecimalStringNoTrailingZeros:
                    numberFormat = "G29";
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex).ToString(numberFormat);
                    }
                    else
                    {
                        data = 0.0m.ToString(numberFormat);
                    }
                    break;
                case FwDataTypes.DecimalString1Digit:
                    numberFormat = "F1";
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex).ToString(numberFormat);
                    }
                    else
                    {
                        data = 0.0m.ToString(numberFormat);
                    }
                    break;
                case FwDataTypes.DecimalString2Digits:
                    numberFormat = "F2";
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex).ToString(numberFormat);
                    }
                    else
                    {
                        data = 0.0m.ToString(numberFormat);
                    }
                    break;
                case FwDataTypes.DecimalString3Digits:
                    numberFormat = "F3";
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex).ToString(numberFormat);
                    }
                    else
                    {
                        data = 0.0m.ToString(numberFormat);
                    }
                    break;
                case FwDataTypes.DecimalString4Digits:
                    numberFormat = "F4";
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = reader.GetDecimal(columnIndex).ToString(numberFormat);
                    }
                    else
                    {
                        data = 0.0m.ToString(numberFormat);
                    }
                    break;
                case FwDataTypes.Boolean:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToBoolean(reader.GetString(columnIndex).TrimEnd());
                    }
                    else
                    {
                        data = false;
                    }
                    break;
                case FwDataTypes.CurrencyString:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToCurrencyString(reader.GetDecimal(columnIndex));
                    }
                    else
                    {
                        data = FwConvert.ToCurrencyString(0.0m);
                    }
                    break;
                case FwDataTypes.CurrencyStringNoDollarSign:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSign(reader.GetDecimal(columnIndex));
                    }
                    else
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSign(0.0m);
                    }
                    break;
                case FwDataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(reader.GetDecimal(columnIndex));
                    }
                    else
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(0.0m);
                    }
                    break;
                case FwDataTypes.PhoneUS:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToPhoneUS(reader.GetString(columnIndex));
                    }
                    else
                    {
                        data = String.Empty;
                    }
                    break;
                case FwDataTypes.ZipcodeUS:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToZipcodeUS(reader.GetString(columnIndex));
                    }
                    else
                    {
                        data = String.Empty;
                    }
                    break;
                case FwDataTypes.Percentage:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSign(FwConvert.ToDecimal(reader.GetValue(columnIndex))) + "%";
                    }
                    else
                    {
                        data = FwConvert.ToCurrencyStringNoDollarSign(0.0m) + "%";
                    }
                    break;
                case FwDataTypes.OleToHtmlColor:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        var value = reader.GetValue(columnIndex);
                        int intValue = Convert.ToInt32(value);
                        data = FwConvert.OleColorToHtmlColor(intValue);
                    }
                    else
                    {
                        //data = FwConvert.OleColorToHtmlColor(0);
                        data = String.Empty;
                    }
                    break;
                case FwDataTypes.Integer:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = FwConvert.ToInt32(reader.GetValue(columnIndex));
                    }
                    else
                    {
                        data = 0;
                    }
                    break;
                case FwDataTypes.JpgDataUrl:
                    data = string.Empty;
                    if (!reader.IsDBNull(columnIndex))
                    {
                        byte[] buffer = reader.GetSqlBytes(columnIndex).Value;
                        bool isnull = (buffer.Length == 0) || ((buffer.Length == 1) && (buffer[0] == 255));
                        if (!isnull)
                        {
                            string base64data = Convert.ToBase64String(buffer);
                            data = "data:image/jpg;base64," + base64data;
                        }
                    }
                    break;
                case FwDataTypes.UTCDateTime:
                    if (!reader.IsDBNull(columnIndex))
                    {
                        data = new FwDatabaseField(reader.GetDateTime(columnIndex)).ToUniversalIso8601DateTimeString();
                    }
                    else
                    {
                        data = "";
                    }
                    break;
            }
            return data;
        }
        //------------------------------------------------------------------------------------
        public async Task ExecuteAsync()
        {
            bool closeConnection = true;
            try
            {
                string methodName = "ExecuteAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                //FwFunc.WriteLog("Begin FwSqlCommand: Execute()");
                this.RowCount = 0;

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + sqlCommand.CommandText);
                if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
                {
                    this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                    this.sqlLogEntry.Start();
                    this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                    this.sqlLogEntry.Start();
                    using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                    {
                        this.eof = (!reader.HasRows);
                        if (!eof)
                        {
                            this.RowCount++;
                            this.Next(reader);
                        }
                        while (await reader.ReadAsync())
                        {
                            this.RowCount++;
                        }
                    }
                }
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
                //FwFunc.WriteLog("End FwSqlCommand: Execute()");
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Renders to a javascript array of objects.  While easier to use from JavaScript that FwJsonDataTable, this will use a lot more bandwidth, because the column names are repeated with each row.  Uses the columns format the data that gets generated.  The only reason there is a second version of this function is because of concerns for breaking older code. This is a project for another day.
        /// </summary>
        /// <returns></returns>
        public async Task<List<dynamic>> QueryToDynamicList2Async()
        {
            List<dynamic> rows;
            dynamic rowObj;
            IDictionary<string, object> row;
            string fieldName;

            bool closeConnection = true;

            try
            {
                this.RowCount = 0;
                string methodName = "QueryToDynamicList2Async";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);
                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToDynamicList()");
                rows = new List<dynamic>();
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlCommand.Connection.OpenAsync();
                }

                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();


                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    Dictionary<string, FwJsonDataTableColumn> indexedColumns = new Dictionary<string, FwJsonDataTableColumn>();
                    for (int colno = 0; colno < columns.Count; colno++)
                    {
                        FwJsonDataTableColumn column = columns[colno];
                        indexedColumns[column.DataField] = column;
                    }
                    while (await reader.ReadAsync())
                    {
                        this.RowCount++;
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
                                    //data = new FwDatabaseField(data).ToShortDateTimeString();
                                    //justin 10/01/2018 - need to return a date string when the time is not part of the value
                                    if (((DateTime)data).TimeOfDay.TotalMilliseconds.Equals(0))
                                    {
                                        data = new FwDatabaseField(data).ToShortDateString();
                                    }
                                    else
                                    {
                                        data = new FwDatabaseField(data).ToShortDateTimeString();
                                    }
                                }
                                //justin hoffman #1332 trying to get image data into this object
                                else if (data is Byte[])
                                {
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
                                    data = FormatReaderData(column.DataType, ordinal, reader);
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
                this.sqlLogEntry.Stop(this.RowCount);
            }

            return rows;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Run a query and return a typed list, by using QueryToDynamicList2, and then serializing it to json than deserializing to the desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> QueryToTypedListAsync<T>()
        {
            List<T> rows;
            T row;
            string fieldName;

            bool closeConnection = true;

            try
            {
                this.RowCount = 0;
                string methodName = "QueryToTypedListAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);
                //FwFunc.WriteLog("Begin FwSqlCommand:QueryToDynamicList()");
                rows = new List<T>();
                this.sqlCommand.CommandText = this.qryText.ToString();
                //FwFunc.WriteLog("Query:\n" + this.sqlCommand.CommandText);

                closeConnection = !sqlConnection.IsOpen();

                if (closeConnection)
                {
                    await this.sqlCommand.Connection.OpenAsync();
                }

                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();


                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    Dictionary<string, FwJsonDataTableColumn> indexedColumns = new Dictionary<string, FwJsonDataTableColumn>();
                    for (int colno = 0; colno < columns.Count; colno++)
                    {
                        FwJsonDataTableColumn column = columns[colno];
                        indexedColumns[column.DataField] = column;
                    }
                    while (await reader.ReadAsync())
                    {
                        this.RowCount++;
                        //                     rowObj = new ExpandoObject();
                        row = Activator.CreateInstance<T>();
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
                                    //data = new FwDatabaseField(data).ToShortDateTimeString();
                                    //justin 10/01/2018 - need to return a date string when the time is not part of the value
                                    if (((DateTime)data).TimeOfDay.TotalMilliseconds.Equals(0))
                                    {
                                        data = new FwDatabaseField(data).ToShortDateString();
                                    }
                                    else
                                    {
                                        data = new FwDatabaseField(data).ToShortDateTimeString();
                                    }
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
                                    data = FormatReaderData(column.DataType, ordinal, reader);
                                }
                            }
                            var propertyInfo = typeof(T).GetProperty(fieldName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(row, data);
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
                this.sqlLogEntry.Stop(this.RowCount);
            }

            return rows;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Run a query and return a typed object, by using QueryToDynamicObject2, and then serializing it to json than deserializing to the desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QueryToTypedObjectAsync<T>()
        {
            var obj = await QueryToDynamicObject2Async();
            string json = JsonConvert.SerializeObject(obj);
            T result = JsonConvert.DeserializeObject<T>(json);

            //justin 09/25/2018
            // when deserializing FwDataRecord objects from a query result, we want to also map fields from the result to Properties on the object using the FwSqlDataFieldAttribute.ColumnName
            if (result is FwDataRecord)
            {
                var dictionary = (IDictionary<string, object>)obj;
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object propertyValue = property.GetValue(result);
                    if (propertyValue == null)
                    {
                        if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                        {
                            foreach (Attribute attribute in property.GetCustomAttributes())
                            {
                                if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))
                                {
                                    FwSqlDataFieldAttribute dataFieldAttribute = (FwSqlDataFieldAttribute)attribute;
                                    if (!dataFieldAttribute.ColumnName.Equals(string.Empty))
                                    {
                                        if (dictionary[dataFieldAttribute.ColumnName] != null)
                                        {
                                            if (dataFieldAttribute.ModelType.Equals(FwDataTypes.Boolean))  // special case for booleans. query result may be "T", "F", "" or other string. need to convert on-the-fly
                                            {
                                                bool b = false;
                                                if ((dictionary[dataFieldAttribute.ColumnName] != null) && (dictionary[dataFieldAttribute.ColumnName] is string))
                                                {
                                                    if (dictionary[dataFieldAttribute.ColumnName].ToString().Equals("T"))
                                                    {
                                                        b = true;
                                                    }
                                                }
                                                dictionary[dataFieldAttribute.ColumnName] = b;
                                            }
                                            property.SetValue(result, dictionary[dataFieldAttribute.ColumnName]);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Render a query as a JavaScript object using the column formatters
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> QueryToDynamicObject2Async()
        {
            dynamic result;
            List<dynamic> results;

            results = await QueryToDynamicList2Async();
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
        public static async Task<FwDatabaseField> GetDataAsync(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwSqlCommand qry;
            FwDatabaseField result;

            qry = new FwSqlCommand(conn, timeout);
            qry.Add("select top 1 " + selectcolumn);
            qry.Add("from " + tablename + " with (nolock)");
            qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
            qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            await qry.ExecuteAsync();
            result = (qry.RowCount == 1) ? qry.GetField(selectcolumn) : null;

            return result;
        }

        //------------------------------------------------------------------------------------
        public static async Task<string> GetStringDataAsync(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            string result = string.Empty;

            field = await FwSqlCommand.GetDataAsync(conn, timeout, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToString().TrimEnd() : string.Empty;

            return result;
        }
        //------------------------------------------------------------------------------------
        public static async Task<bool> GetBoolDataAsync(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            bool result;

            field = await FwSqlCommand.GetDataAsync(conn, timeout, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToBoolean() : false;

            return result;
        }
        //------------------------------------------------------------------------------------
        public static async Task<Dictionary<string, FwDatabaseField>> GetRowAsync(FwSqlConnection conn, int timeout, string tablename, string wherecolumn, string wherecolumnvalue, bool throwExceptionIfRowCountNot1)
        {
            FwSqlCommand qry;
            Dictionary<string, FwDatabaseField> result;

            qry = new FwSqlCommand(conn, timeout);
            qry.Add("select top 1 *");
            qry.Add("from " + tablename + " with (nolock)");
            qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
            qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            await qry.ExecuteAsync();
            result = qry.GetFwDatabaseFields();

            if (throwExceptionIfRowCountNot1 && qry.RowCount != 1)
            {
                throw new Exception(string.Format("Expected rowcount to be 1, but found {0} rows. Table/View: {1}, column: {2}, value: {3}", qry.RowCount, tablename, wherecolumn, wherecolumnvalue));
            }

            return result;
        }
        //------------------------------------------------------------------------------------
        public static async Task<int> SetDataAsync(FwSqlConnection conn, int timeout, string table, string wherecolumn, string wherecolumnvalue, string updatecolumn, string updatecolumnvalue)
        {
            FwSqlCommand cmd;

            cmd = new FwSqlCommand(conn, timeout);
            cmd.Add("update " + table);
            cmd.Add("set " + updatecolumn + " = @updatecolumnvalue");
            cmd.Add("where " + wherecolumn + " = @wherecolumnvalue");
            cmd.AddParameter("@updatecolumnvalue", updatecolumnvalue);
            cmd.AddParameter("@wherecolumnvalue", wherecolumnvalue);
            await cmd.ExecuteNonQueryAsync();

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
        public async Task<List<T>> SelectAsync<T>(FwCustomFields customFields = null) where T : FwDataRecord
        {
            string methodName = "SelectAsync";
            string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

            List<T> results = new List<T>();
            //this.Add("order by " + orderByColumn + " " + orderByDirection.ToString());
            //this.Add("offset @offsetrows rows fetch next @fetchsize rows only");
            //this.Add("for json path");
            //this.AddParameter("@offsetrows", (pageNo - 1) * pageSize);
            //this.AddParameter("@fetchsize", pageSize);

            bool openAndCloseConnection = !sqlConnection.IsOpen();

            if (openAndCloseConnection)
            {
                await this.sqlCommand.Connection.OpenAsync();
            }

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            Dictionary<string, PropertyInfo> sqlDataFieldPropertyInfos = new Dictionary<string, PropertyInfo>();
            Dictionary<string, FwSqlDataFieldAttribute> sqlDataFieldAttributes = new Dictionary<string, FwSqlDataFieldAttribute>();
            Dictionary<string, int> columnIndex = new Dictionary<string, int>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                if (sqlDataFieldAttribute != null)
                {
                    sqlDataFieldPropertyInfos[propertyInfo.Name] = propertyInfo;
                    sqlDataFieldAttributes[propertyInfo.Name] = sqlDataFieldAttribute;
                }
            }
            this.sqlCommand.CommandText = this.qryText.ToString();

            try
            {
                this.RowCount = 0;
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    for (int fieldno = 0; fieldno < reader.FieldCount; fieldno++)
                    {
                        columnIndex[reader.GetName(fieldno)] = fieldno;
                    }
                    while (await reader.ReadAsync())
                    {
                        this.RowCount++;
                        T obj = Activator.CreateInstance<T>();

                        foreach (KeyValuePair<string, FwSqlDataFieldAttribute> attribute in sqlDataFieldAttributes)
                        {
                            int i = -1;

                            //first, attempt to find the column index by the Key name (logical name)
                            if (i < 0)
                            {
                                i = columnIndex.ContainsKey(attribute.Key) ? columnIndex[attribute.Key] : -1;
                            }

                            //second, attempt to find the column index by the ColumnName (physical name)
                            if (i < 0)
                            {
                                i = columnIndex.ContainsKey(attribute.Value.ColumnName) ? columnIndex[attribute.Value.ColumnName] : -1;
                            }

                            //if neither fields are found, give a meaningful error message
                            if (i < 0)
                            {
                                throw new Exception("Invalid field name: " + attribute.Key + " or " + attribute.Value.ColumnName);
                            }
                            FwDatabaseField field = new FwDatabaseField(reader.GetValue(i));
                            object data = FormatReaderData(attribute.Value.ModelType, i, reader);
                            sqlDataFieldPropertyInfos[attribute.Key].SetValue(obj, data);
                        }

                        if ((customFields != null) && (customFields.Count > 0))
                        {
                            FwCustomValues customValues = null;
                            customValues = new FwCustomValues();
                            foreach (FwCustomField customField in customFields)
                            {
                                FwDatabaseField field = new FwDatabaseField(reader.GetValue(columnIndex[customField.FieldName]));
                                object data = FormatReaderData(FwDataTypes.Text, columnIndex[customField.FieldName], reader); //todo: support different data types
                                string str = data.ToString();
                                customValues.AddCustomValue(customField.FieldName, str, customField.FieldType);
                            }
                            obj._Custom = customValues;
                        }

                        results.Add(obj);
                    }
                }
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
            }

            if (openAndCloseConnection)
            {
                this.sqlCommand.Connection.Close();
            }

            return results;
        }
        //------------------------------------------------------------------------------------
        public async Task<GetManyResponse<T>> GetManyAsync<T>(FwCustomFields customFields = null) where T : FwDataRecord
        {
            string methodName = "GetManyAsync";
            string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

            GetManyResponse<T> response = new GetManyResponse<T>();
            response.PageNo = this.PageNo;
            response.PageSize = this.PageSize;
            //this.Add("order by " + orderByColumn + " " + orderByDirection.ToString());
            //this.Add("offset @offsetrows rows fetch next @fetchsize rows only");
            //this.Add("for json path");
            //this.AddParameter("@offsetrows", (pageNo - 1) * pageSize);
            //this.AddParameter("@fetchsize", pageSize);

            bool openAndCloseConnection = !sqlConnection.IsOpen();

            if (openAndCloseConnection)
            {
                await this.sqlCommand.Connection.OpenAsync();
            }

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            Dictionary<string, PropertyInfo> sqlDataFieldPropertyInfos = new Dictionary<string, PropertyInfo>();
            Dictionary<string, FwSqlDataFieldAttribute> sqlDataFieldAttributes = new Dictionary<string, FwSqlDataFieldAttribute>();
            Dictionary<string, int> columnIndex = new Dictionary<string, int>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                if (sqlDataFieldAttribute != null)
                {
                    sqlDataFieldPropertyInfos[propertyInfo.Name] = propertyInfo;
                    sqlDataFieldAttributes[propertyInfo.Name] = sqlDataFieldAttribute;
                }
            }
            this.sqlCommand.CommandText = this.qryText.ToString();

            try
            {
                this.RowCount = 0;
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                using (SqlDataReader reader = await this.sqlCommand.ExecuteReaderAsync())
                {
                    for (int fieldno = 0; fieldno < reader.FieldCount; fieldno++)
                    {
                        columnIndex[reader.GetName(fieldno)] = fieldno;
                    }
                    bool hasColTotalRows = false;
                    foreach (DataRow row in reader.GetSchemaTable().Rows)
                    {
                        if (row["ColumnName"].ToString().Equals(FwSqlSelect.TOTAL_ROWS_FIELD))
                            hasColTotalRows = true;
                    }
                    bool needsTotalRows = hasColTotalRows;
                    while (await reader.ReadAsync())
                    {
                        this.RowCount++;
                        T obj = Activator.CreateInstance<T>();

                        foreach (KeyValuePair<string, FwSqlDataFieldAttribute> attribute in sqlDataFieldAttributes)
                        {
                            int i = -1;

                            //first, attempt to find the column index by the Key name (logical name)
                            if (i < 0)
                            {
                                i = columnIndex.ContainsKey(attribute.Key) ? columnIndex[attribute.Key] : -1;
                            }

                            //second, attempt to find the column index by the ColumnName (physical name)
                            if (i < 0)
                            {
                                i = columnIndex.ContainsKey(attribute.Value.ColumnName) ? columnIndex[attribute.Value.ColumnName] : -1;
                            }

                            //if neither fields are found, give a meaningful error message
                            if (i < 0)
                            {
                                throw new Exception("Invalid field name: " + attribute.Key + " or " + attribute.Value.ColumnName);
                            }
                            FwDatabaseField field = new FwDatabaseField(reader.GetValue(i));
                            object data = FormatReaderData(attribute.Value.ModelType, i, reader);
                            sqlDataFieldPropertyInfos[attribute.Key].SetValue(obj, data);
                        }

                        if ((customFields != null) && (customFields.Count > 0))
                        {
                            FwCustomValues customValues = null;
                            customValues = new FwCustomValues();
                            foreach (FwCustomField customField in customFields)
                            {
                                FwDatabaseField field = new FwDatabaseField(reader.GetValue(columnIndex[customField.FieldName]));
                                object data = FormatReaderData(FwDataTypes.Text, columnIndex[customField.FieldName], reader); //todo: support different data types
                                string str = data.ToString();
                                customValues.AddCustomValue(customField.FieldName, str, customField.FieldType);
                            }
                            obj._Custom = customValues;
                        }

                        if (needsTotalRows && hasColTotalRows)
                        {
                            int colTotalRows = reader.GetOrdinal(FwSqlSelect.TOTAL_ROWS_FIELD);
                            if (colTotalRows >= 0)
                            {
                                response.TotalRows = reader.GetInt32(colTotalRows);
                            }
                            needsTotalRows = false;
                        }

                        response.Items.Add(obj);
                    }
                }
            }
            catch
            {
                Console.WriteLine(this.QryTextDebug);
                throw;
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
            }

            if (openAndCloseConnection)
            {
                this.sqlCommand.Connection.Close();
            }

            return response;
        }
        //------------------------------------------------------------------------------------

        private object getModelTypeData(object propertyValue, FwDataTypes modelType, string propertyName)
        {
            object data;
            switch (modelType)
            {
                case FwDataTypes.Text:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                case FwDataTypes.Date:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    //data = ((string)propertyValue).Trim();
                    //justin 02/26/2018 - dates to be saved as NULL when blank
                    string strDate = ((string)propertyValue).Trim();
                    if (strDate.Equals(string.Empty))
                    {
                        data = DBNull.Value;
                    }
                    else
                    {
                        data = strDate;
                    }
                    break;
                case FwDataTypes.Time:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                case FwDataTypes.DateTime:
                    //if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    //data = FwConvert.ToDateTime(((string)propertyValue).Trim()).ToString("yyyy-MM-dd hh:mm:ss tt");
                    if (propertyValue.GetType().Equals(typeof(DateTime)))
                    {
                        data = propertyValue;
                    }
                    else if (propertyValue.GetType().Equals(typeof(string)))
                    {
                        //data = FwConvert.ToDateTime(((string)propertyValue).Trim()).ToString("yyyy-MM-dd hh:mm:ss tt");
                        if (((string)propertyValue).Equals(string.Empty))
                        {
                            data = DBNull.Value;
                        }
                        else
                        {
                            data = FwConvert.ToDateTime(((string)propertyValue).Trim()).ToString("yyyy-MM-dd hh:mm:ss tt");
                        }
                    }
                    else
                    {
                        throw new Exception("Expected DateTime or string for " + propertyName + ".  Found " + propertyValue.GetType().ToString());
                    }
                    break;
                case FwDataTypes.DateTimeOffset:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                case FwDataTypes.Decimal:
                    data = propertyValue;
                    break;
                case FwDataTypes.Boolean:
                    if (propertyValue.GetType() != typeof(bool)) throw new Exception("Expected bool for " + propertyName);
                    data = ((bool)propertyValue) ? "T" : string.Empty;
                    break;
                case FwDataTypes.CurrencyString:
                    data = propertyValue;
                    break;
                case FwDataTypes.CurrencyStringNoDollarSign:
                    data = propertyValue;
                    break;
                case FwDataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                    data = propertyValue;
                    break;
                case FwDataTypes.PhoneUS:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                case FwDataTypes.ZipcodeUS:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                case FwDataTypes.Percentage:
                    data = propertyValue;
                    break;
                case FwDataTypes.OleToHtmlColor:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = FwConvert.HtmlColorToOleColor((string)propertyValue);
                    break;
                case FwDataTypes.Integer:
                    data = propertyValue;
                    break;
                case FwDataTypes.JpgDataUrl:
                    data = propertyValue;
                    break;
                case FwDataTypes.UTCDateTime:
                    if (propertyValue.GetType() != typeof(string)) throw new Exception("Expected string for " + propertyName);
                    data = ((string)propertyValue).Trim();
                    break;
                default:
                    data = propertyValue;
                    break;
            }
            return data;
        }
        //------------------------------------------------------------------------------------
        public async Task<int> InsertAsync(string tablename, object businessObject, SqlServerConfig dbConfig)
        {
            bool openAndCloseConnection = true;
            try
            {
                this.RowCount = 0;
                string methodName = "InsertAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                openAndCloseConnection = !sqlConnection.IsOpen();

                if (openAndCloseConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                var insertColumnsNames = new StringBuilder();
                var insertParameterNames = new StringBuilder();
                var i = 0;
                var propertyInfos = businessObject.GetType().GetProperties();
                PropertyInfo identityProperty = null;
                foreach (var propertyInfo in propertyInfos)
                {
                    var hasJsonIgnoreAttribute = propertyInfo.IsDefined(typeof(JsonIgnoreAttribute));
                    if (!hasJsonIgnoreAttribute)
                    {
                        object propertyValue = propertyInfo.GetValue(businessObject);
                        bool hasFwSqlDataFieldAttribute = propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute));
                        string sqlColumnName = propertyInfo.Name;
                        bool isIdentity = false;
                        FwDataTypes modelType = FwDataTypes.Text; ;
                        if (hasFwSqlDataFieldAttribute)
                        {
                            FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                            isIdentity = sqlDataFieldAttribute.Identity;
                            modelType = sqlDataFieldAttribute.ModelType;
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }
                        }
                        if (isIdentity)
                        {
                            identityProperty = propertyInfo;
                            this.AddParameter("@" + propertyInfo.Name, SqlDbType.Int, ParameterDirection.Output);
                        }
                        else // !isIdentity
                        {
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
                                    propertyValue = FwConvert.ToUtcIso8601DateTime(DateTime.UtcNow);
                                    businessObject.GetType().GetProperty(propertyInfo.Name).SetValue(businessObject, propertyValue);
                                    this.AddParameter("@" + propertyInfo.Name, propertyValue);
                                }
                                else
                                {
                                    // format the data and set the qry parameter value
                                    object data = getModelTypeData(propertyValue, modelType, propertyInfo.Name);
                                    this.AddParameter("@" + propertyInfo.Name, data);
                                }
                                i++;
                            }
                        }
                    }
                }

                this.sqlCommand.CommandText = "insert into " + tablename + "(" + insertColumnsNames + ")\nvalues (" + insertParameterNames + ")";

                if (identityProperty != null)
                {
                    this.sqlCommand.CommandText += "\nset @" + identityProperty.Name + " = scope_identity()";
                }

                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();

                //jh 02/19/2019 - need to capture the identity value here and update the property on this object
                if (identityProperty != null)
                {
                    object identityValue = this.sqlCommand.Parameters["@" + identityProperty.Name].Value;
                    if (identityValue == DBNull.Value)
                    {
                        identityValue = 0;
                    }
                    identityProperty.SetValue(businessObject, identityValue);
                }
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public async Task<int> UpdateAsync(string tablename, object businessObject)
        {
            bool openAndCloseConnection = true;
            try
            {
                this.RowCount = 0;
                int maxFieldNameLength = 0;
                string methodName = "UpdateAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                openAndCloseConnection = !sqlConnection.IsOpen();

                if (openAndCloseConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                var setStatements = new StringBuilder();
                var whereClause = new StringBuilder();
                var i = 0;
                var propertyInfos = businessObject.GetType().GetProperties();

                //determine the length of the longest field name
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
                            maxFieldNameLength = (sqlColumnName.Length > maxFieldNameLength ? sqlColumnName.Length : maxFieldNameLength);
                        }
                    }
                }


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
                                    whereClause.Append(" and   ");
                                }
                                string leftSide = "[" + sqlColumnName + "]";
                                leftSide = leftSide.PadRight(maxFieldNameLength + 2, ' ');
                                whereClause.Append(leftSide);
                                whereClause.Append(" = @");
                                whereClause.AppendLine(sqlColumnName);

                                this.AddParameter("@" + sqlColumnName, propertyValue);
                            }
                            // generate the set statements for the update query
                            else if (propertyValue != null || sqlColumnName.ToLower().Equals("datestamp"))
                            {
                                if (i == 0)
                                {
                                    setStatements.Append("   ");
                                }
                                else
                                {
                                    setStatements.Append(",");
                                    setStatements.AppendLine();
                                    setStatements.Append("       ");
                                }
                                string leftSide = "[" + sqlColumnName + "]";
                                leftSide = leftSide.PadRight(maxFieldNameLength + 2, ' ');
                                setStatements.Append(leftSide);
                                setStatements.Append(" = @");
                                setStatements.Append(sqlColumnName);
                                if (sqlColumnName.Equals("datestamp"))
                                {
                                    propertyValue = FwConvert.ToUtcIso8601DateTime(DateTime.UtcNow);
                                    businessObject.GetType().GetProperty(propertyInfo.Name).SetValue(businessObject, propertyValue);
                                    this.AddParameter("@" + sqlColumnName, propertyValue);
                                }
                                else
                                {
                                    object data = getModelTypeData(propertyValue, sqlDataFieldAttribute.ModelType, propertyInfo.Name);
                                    this.AddParameter("@" + sqlColumnName, data);
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
                sql.Append("[");
                sql.Append(tablename);
                sql.Append("]");
                sql.AppendLine();
                sql.Append(" set");
                if (setStatements.Length == 0)
                {
                    return 0;
                }
                sql.Append(setStatements);
                sql.AppendLine();
                sql.Append(" where ");
                sql.Append(whereClause);
                this.sqlCommand.CommandText = sql.ToString();
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        public async Task<int> DeleteAsync(string tablename, object businessObject)
        {
            bool openAndCloseConnection = true;
            try
            {
                this.RowCount = 0;
                string methodName = "DeleteAsync";
                string usefulLinesFromStackTrace = GetUsefulLinesFromStackTrace(methodName);

                openAndCloseConnection = !sqlConnection.IsOpen();

                if (openAndCloseConnection)
                {
                    await this.sqlConnection.OpenAsync();
                }
                var setStatements = new StringBuilder();
                var whereClause = new StringBuilder();
                var propertyInfos = businessObject.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var hasSqlDataFieldAttribute = propertyInfo.IsDefined(typeof(FwSqlDataFieldAttribute));
                    var propertyValue = propertyInfo.GetValue(businessObject);
                    string sqlColumnName = propertyInfo.Name;
                    if (hasSqlDataFieldAttribute)
                    {
                        var sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute.IsPrimaryKey)
                        {
                            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
                            {
                                sqlColumnName = sqlDataFieldAttribute.ColumnName;
                            }
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
                this.sqlLogEntry = new FwSqlLogEntry(this.sqlCommand, usefulLinesFromStackTrace);
                this.sqlLogEntry.Start();
                this.RowCount = await this.sqlCommand.ExecuteNonQueryAsync();
            }
            finally
            {
                this.sqlLogEntry.Stop(this.RowCount);
                if (openAndCloseConnection)
                {
                    this.sqlConnection.Close();
                }
            }

            return this.RowCount;
        }
        //------------------------------------------------------------------------------------
        private string GetUsefulLinesFromStackTrace(string methodName)
        {
            string result = string.Empty;
            if (FwSqlLogEntry.LogSqlContext)
            {
                List<string> usefulLinesList = new List<string>();
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
                var lines = System.Text.RegularExpressions.Regex.Split(trace.ToString(), "\r\n|\r|\n");
                foreach (var line in lines)
                {
                    // All we're interested in is what triggered the query, so filter out non-user code and anything else we don't want to see
                    if (
                        line.Contains("Async(") &&
                        !line.Contains("GetUsefulLinesFromStackTrace") &&
                        !line.Contains(methodName) &&
                        !line.Contains("MoveNext") &&
                        !line.Contains("at System") &&
                        !line.Contains("at Microsoft") &&
                        !line.Contains("at lambda_method") &&
                        !line.Contains(".ctor") &&
                        !line.Contains("at FwStandard.Security.FwSecurityTree.InitAsync()") &&
                        !line.Contains("at FwCore.Controllers.FwController.OnActionExecutionAsync") &&
                        !line.Contains("at FwStandard.DataLayer.FwDataRecord.BrowseAsync") &&
                        !line.Contains("at FwStandard.BusinessLogic.FwBusinessLogic.BrowseAsync")
                        //!line.Contains("") &&
                        )
                    {
                        if (line.Trim().Length > 0 && !usefulLinesList.Contains(line))
                        {
                            usefulLinesList.Add(line);
                        }
                    }
                }
                //if (usefulLines.Length == 0)
                //{
                //    usefulLines.Append(trace.ToString());
                //}
                result = String.Join(Environment.NewLine, usefulLinesList.ToArray()) + Environment.NewLine;
            }
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;

namespace QuartzScheduler.Utilities
{
    public class QuartzSqlCommand : IDisposable
    {
        //------------------------------------------------------------------------------------
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private StringBuilder qryText;
        private SqlDataReader reader;
        private bool eof;
        private FwFields fields;

        //private StringBuilder sql;
        //------------------------------------------------------------------------------------
        //public string Sql {get{return sql.ToString();}}
        //------------------------------------------------------------------------------------
        public int RowCount { get; private set; }
        public List<string> FieldNames { get { return new List<string>(fields.Keys); } }
        public SqlParameterCollection Parameters {get { return this.sqlCommand.Parameters; }}
        public SqlTransaction Transaction { get{return this.sqlCommand.Transaction;} set{this.sqlCommand.Transaction = value;} }
        //------------------------------------------------------------------------------------
        public QuartzSqlCommand(string connectionString)
        {
            this.sqlConnection = new SqlConnection(connectionString);
            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection;
            this.sqlCommand.CommandType = CommandType.Text;
            //this.sqlCommand.CommandTimeout = FwConvert.ToInt32(FwApplicationConfig.CurrentSite.DatabaseConnectionFinder[conn.DatabaseConnection].QueryTimeout);
            this.qryText = new StringBuilder();
            this.reader = null;
            this.eof = true;
            this.RowCount = 0;
            this.fields = new FwFields();
        }
        //------------------------------------------------------------------------------------
        public QuartzSqlCommand(string connectionString, string storedProcedureName)
        {
            this.sqlConnection = new SqlConnection(connectionString);
            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection;
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            //this.sqlCommand.CommandTimeout = FwConvert.ToInt32(FwApplicationConfig.CurrentSite.DatabaseConnectionFinder[conn.DatabaseConnection].QueryTimeout);
            this.qryText = new StringBuilder();
            this.qryText.Append(storedProcedureName);
            this.reader = null;
            this.eof = true;
            this.RowCount = 0;
            this.fields = new FwFields();
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
            parameter.Scale     = scale;
        }
        //------------------------------------------------------------------------------------
        public object GetParameter(string name)
        {
            return this.sqlCommand.Parameters[name].Value;
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
                if (closeConnection)
                {
                    this.sqlConnection.Open();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                this.RowCount = this.sqlCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
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
            }
            
            return this.RowCount;
        }
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
                this.RowCount = 0;
                if (closeConnection)
                {
                    this.sqlConnection.Open();
                }
                this.sqlCommand.CommandText = this.qryText.ToString();
                if (this.sqlCommand.CommandType == CommandType.StoredProcedure)
                {                
                    this.RowCount = this.sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    this.reader = this.sqlCommand.ExecuteReader();
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
            finally
            {
                if ((this.reader != null) && (!this.reader.IsClosed))
                {
                    reader.Close();
                }
                if (closeConnection)
                {
                    this.Close();
                }
            }
        }
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
        public void Close()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
            sqlConnection.Close();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Renders to a javascript array of objects.  While easier to use from JavaScript that FwJsonDataTable, this will use a lot more bandwidth, because the column names are repeated with each row.  Uses the columns format the data that gets generated.  The only reason there is a second version of this function is because of concerns for breaking older code. This is a project for another day.
        /// </summary>
        /// <param name="closeConnection">False will cause it to not close the connection.  Useful for transactions.</param>
        /// <returns></returns>
        public List<dynamic> QueryToDynamicList()
        {
            return QueryToDynamicList(true);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Renders to a javascript array of objects.  While easier to use from JavaScript that FwJsonDataTable, this will use a lot more bandwidth, because the column names are repeated with each row.  Uses the columns format the data that gets generated.  The only reason there is a second version of this function is because of concerns for breaking older code. This is a project for another day.
        /// </summary>
        /// <param name="closeConnection">False will cause it to not close the connection.  Useful for transactions.</param>
        /// <returns></returns>
        public List<dynamic> QueryToDynamicList(bool closeConnection)
        {
            List<dynamic> rows;
            dynamic rowObj;
            IDictionary<string, object> row;
            SqlDataReader reader;
            string fieldName;
            
            try
            {
                rows = new List<dynamic>();
                this.sqlCommand.CommandText = this.qryText.ToString();
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Open();
                }
                reader = this.sqlCommand.ExecuteReader();
                while (reader.Read())
                {                
                    rowObj = new ExpandoObject();
                    row = (IDictionary<string, object>)rowObj;
                    for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                    {
                        fieldName = reader.GetName(ordinal);
                        object data = reader.GetValue(ordinal);

                        row[fieldName] = data;
                    }
                    rows.Add(row);
                }
                reader.Close();
            }
            finally
            {
                if (closeConnection)
                {
                    this.sqlCommand.Connection.Close();
                }
            }
            
            return rows;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Render a query as a JavaScript object using the column formatters
        /// </summary>
        /// <returns></returns>
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
        public void Dispose()
        {
            if (this.sqlCommand != null)
            {
                this.sqlCommand.Dispose();
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

    }
}
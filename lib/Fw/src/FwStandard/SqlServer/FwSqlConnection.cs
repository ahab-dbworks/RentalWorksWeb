using System;
using System.Data.SqlClient;

namespace FwStandard.SqlServer
{
    public class FwSqlConnection : IDisposable
    {
        //---------------------------------------------------------------------------------------------
        private SqlConnection sqlConnection = null;
        private string server = string.Empty;
        private string database = string.Empty;
        private string databaseUser = string.Empty;
        private string databasePassword = string.Empty;
        //---------------------------------------------------------------------------------------------
        //public FwDatabases DatabaseConnection {get;private set;}
        //---------------------------------------------------------------------------------------------
        public FwSqlConnection(string connectionString)
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;
        }
        //---------------------------------------------------------------------------------------------
        public SqlConnection GetConnection()
        {
            return this.sqlConnection;
        }
        //---------------------------------------------------------------------------------------------
        public void Open()
        {
            this.sqlConnection.Open();
        }
        //---------------------------------------------------------------------------------------------
        public void Close()
        {
            this.sqlConnection.Close();
        }
        //---------------------------------------------------------------------------------------------
        public void Dispose()
        {
            if (this.sqlConnection != null)
            {
                this.sqlConnection.Dispose();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}

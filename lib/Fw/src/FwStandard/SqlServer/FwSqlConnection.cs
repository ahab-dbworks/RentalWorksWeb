using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        /// <summary>
        /// Create a new FwSqlConnection.
        /// </summary>
        /// <param name="connectionString">The Connection String.</param>
        /// <param name="multipleActiveResultsSets">Set this to true to enable multiple queries running in parallel on the same SqlConnection.  This has some additional overhead, so we only want to enable this when running multiple queries.</param>
        public FwSqlConnection(string connectionString, bool multipleActiveResultsSets)
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString.TrimEnd(new char[] { ';' }) + ";MultipleActiveResultSets=True;";
        }
        //---------------------------------------------------------------------------------------------
        public SqlConnection GetConnection()
        {
            return this.sqlConnection;
        }
        //---------------------------------------------------------------------------------------------
        public async Task OpenAsync()
        {
            await this.sqlConnection.OpenAsync();
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

using System;
using System.Data;
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
        private SqlTransaction activeTransaction;
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
            sqlConnection.ConnectionString = connectionString.TrimEnd(new char[] { ';' }) + ";MultipleActiveResultSets=" + (multipleActiveResultsSets ? "True" : "False") + ";";
        }
        //---------------------------------------------------------------------------------------------
        public SqlConnection GetConnection()
        {
            return this.sqlConnection;
        }
        //---------------------------------------------------------------------------------------------
        public bool IsOpen()
        {
            return (this.sqlConnection.State != ConnectionState.Closed);
        }
        //---------------------------------------------------------------------------------------------
        public SqlTransaction BeginTransaction()
        {
            activeTransaction = this.sqlConnection.BeginTransaction();
            FwSqlLogEntry sqlLogEntry = new FwSqlLogEntry();
            sqlLogEntry.WriteToConsole("BEGIN TRANSACTION");
            return activeTransaction;
        }
        //---------------------------------------------------------------------------------------------
        public SqlTransaction GetActiveTransaction()
        {
            return activeTransaction;
        }
        //---------------------------------------------------------------------------------------------
        public void CommitTransaction()
        {
            activeTransaction.Commit();
            FwSqlLogEntry sqlLogEntry = new FwSqlLogEntry();
            sqlLogEntry.WriteToConsole("COMMIT TRANSACTION");
        }
        //---------------------------------------------------------------------------------------------
        public void RollbackTransaction()
        {
            activeTransaction.Rollback();
            FwSqlLogEntry sqlLogEntry = new FwSqlLogEntry();
            sqlLogEntry.WriteToConsole("ROLLBACK TRANSACTION");
        }
        //---------------------------------------------------------------------------------------------
        public async Task OpenAsync()
        {
            if (this.sqlConnection.State != System.Data.ConnectionState.Open)
            {
                if (this.sqlConnection.ConnectionString.ToLower().Contains("user id=dbworks;"))
                {
                    string errorMsg = "\nCannot use the 'dbworks' SQL Server login.  Change the login in the connection string.\n";
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("#######################################   ERROR   ######################################");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(errorMsg);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("########################################################################################");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    throw new Exception(errorMsg);
                }
                await this.sqlConnection.OpenAsync();
            }
        }
        //---------------------------------------------------------------------------------------------
        public void Close()
        {
            if (this.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                this.sqlConnection.Close();
            }
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

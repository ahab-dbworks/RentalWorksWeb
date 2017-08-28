using System.Configuration;
using System.Data.SqlClient;
using Fw.Json.Utilities;
using System;
using Fw.Json.ValueTypes;

namespace Fw.Json.SqlServer
{
    public class FwSqlConnection : IDisposable
    {
        //---------------------------------------------------------------------------------------------
        private SqlConnection sqlConnection = null;
        private string server = string.Empty;
        private string database = string.Empty;
        private string databaseUser = string.Empty;
        private string databasePassword = string.Empty;
        private FwApplicationConfig_DatabaseConnection dbConnection;
        public string ConnectionString
        {
            get
            {
                return sqlConnection.ConnectionString;
            }

        }
        //---------------------------------------------------------------------------------------------
        public static FwSqlConnection RentalWorks   { get { return new FwSqlConnection(FwDatabases.RentalWorks);   } }
        public static FwSqlConnection RentalWorksDW { get { return new FwSqlConnection(FwDatabases.RentalWorksDW); } }
        public static FwSqlConnection MicrosoftCRM  { get { return new FwSqlConnection(FwDatabases.MicrosoftCRM);  } }
        public static FwSqlConnection GateWorks     { get { return new FwSqlConnection(FwDatabases.GateWorks);     } }
        public static FwSqlConnection TransWorks    { get { return new FwSqlConnection(FwDatabases.TransWorks);    } }
        public static FwSqlConnection MediaWorks    { get { return new FwSqlConnection(FwDatabases.MediaWorks);    } }
        public static FwSqlConnection AppConnection 
        { 
            get {
                if (FwSqlConnection.AppDatabase == FwDatabases.None) throw new ArgumentException("AppDatabase", "FwSqlConnection: Please set FwSqlConnection.AppDatabase during the Application Start event in Global.asax.cs");
                return new FwSqlConnection(FwSqlConnection.AppDatabase);
            } 
        }
        public static FwDatabases AppDatabase = FwDatabases.None;
        //---------------------------------------------------------------------------------------------
        public FwDatabases DatabaseConnection {get;private set;}
        //---------------------------------------------------------------------------------------------
        // This constructor is only if you are using Application.config
        public FwSqlConnection(FwDatabases databaseConnection)
        {
            DatabaseConnection = databaseConnection;
            if (!FwApplicationConfig.CurrentSite.DatabaseConnectionFinder.ContainsKey(databaseConnection))
            {
                throw new Exception("There is no DatabaseConnection configured for " + databaseConnection.ToString() + " in Application.config");
            }
            dbConnection = FwApplicationConfig.CurrentSite.DatabaseConnectionFinder[databaseConnection];
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = dbConnection.ConnectionString;
        }
        //---------------------------------------------------------------------------------------------
        public SqlConnection GetConnection()
        {
            return this.sqlConnection;
        }
        //---------------------------------------------------------------------------------------------
        public void Open()
        {
            FwFunc.WriteLog("Begin FwSqlConnection: Open()");
            this.sqlConnection.Open();
            FwFunc.WriteLog("End FwSqlConnection: Open()");
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

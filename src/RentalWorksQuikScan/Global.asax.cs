using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using FwStandard.Models;
using RentalWorksQuikScan.Source;
using System;

namespace RentalWorksQuikScan
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            FwFunc.WriteLog("Begin Application Start");
            AccountService.Current = new AccountService();
            string pathFwApplicationSchema = this.Server.MapPath("FwApplicationSchema.config");
            FwApplicationSchema.Load(pathFwApplicationSchema);
            FwSqlConnection.AppDatabase = FwDatabases.RentalWorks;
            FwReport.AddLicense();
            SqlServerConfig sqlServerConfig = new SqlServerConfig();
            sqlServerConfig.ConnectionString = FwSqlConnection.AppConnection.ConnectionString;
            FwFunc.WriteLog("End Application Start");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
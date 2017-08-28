using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using FwStandard.Options;
using FwStandard.Security;
using RentalWorksQuikScan.Source;
using RentalWorksWebLibrary;
using System;

namespace RentalWorksQuikScan
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            string pathFwApplicationSchema;

            FwFunc.WriteLog("Begin Application Start");
            AccountService.Current = new AccountService();
            pathFwApplicationSchema = this.Server.MapPath("FwApplicationSchema.config");
            FwApplicationSchema.Load(pathFwApplicationSchema);
            FwSqlConnection.AppDatabase = FwDatabases.RentalWorks;
            FwReport.AddLicense();
            SqlServerOptions sqlServerOptions = new SqlServerOptions();
            sqlServerOptions.ConnectionString = FwSqlConnection.AppConnection.ConnectionString;
            FwSecurityTree.Tree = new SecurityTree(sqlServerOptions, "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}");
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
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using FwStandard.Models;
using FwStandard.Security;
using System;
using TrakitWorksWeb.Source;
using WebLibrary.Security;

namespace TrakitWorksWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            FwFunc.WriteLog("Begin Application Start");
            AccountService.Current = new AccountService();
            //string pathFwApplicationSchema = this.Server.MapPath("FwApplicationSchema.config");
            //FwApplicationSchema.Load(pathFwApplicationSchema);
            FwSqlConnection.AppDatabase = FwDatabases.RentalWorks;
            FwReport.AddLicense();
            SqlServerConfig sqlServerConfig = new SqlServerConfig();
            sqlServerConfig.ConnectionString = FwSqlConnection.RentalWorks.ConnectionString;
            FwSecurityTree.Tree = new SecurityTree(sqlServerConfig, "{D901DE93-EC22-45A1-BB4A-DD282CAF59FB}");
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
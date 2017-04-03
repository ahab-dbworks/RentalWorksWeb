using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using RentalWorksWeb.Source;
using System;

namespace RentalWorksWeb
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
            FwApplicationTree.CurrentApplicationId = "{0A5F2584-D239-480F-8312-7C2B552A30BA}";
            FwApplicationTree.Tree = new SecurityTree();
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
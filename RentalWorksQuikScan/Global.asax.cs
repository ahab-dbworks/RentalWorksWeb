using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Fw.Json.Utilities;
using System.Configuration;
using Fw.Json.SqlServer;
using Fw.Json.ValueTypes;
using Fw.Json.Services;
using RentalWorksQuikScanLibrary;

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
            FwApplicationTree.CurrentApplicationId = "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}";
            FwApplicationTree.Tree = new RentalWorksWeb.SecurityTree();
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
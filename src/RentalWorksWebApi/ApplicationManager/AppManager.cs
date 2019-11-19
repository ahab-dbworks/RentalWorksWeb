using FwStandard.AppManager;
using FwStandard.Models;
using System;
using System.Collections.Generic;
using WebApi.Modules.Administrator.Group;

namespace WebApi.ApplicationManager
{
    public class AppManager : FwAppManager
    {
        //---------------------------------------------------------------------------------------------
        public AppManager(SqlServerConfig sqlServerOptions) : base(sqlServerOptions)
        {

        }
        //---------------------------------------------------------------------------------------------
        protected override string Unabbreviate(string value)
        {
            string result = value;
            switch(value)
            {
                case "Rw": result = "RentalWorks"; break;
                case "Tw": result = "TrakitWorks"; break;
                case "S": result = "Standard"; break;
                case "P": result = "Pro"; break;
                case "E": result = "Enterprise"; break;
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public override string GetDefaultEditions()
        {
            return "Rw|S|P|E,Tw|S|P|E";
        }
        //---------------------------------------------------------------------------------------------
        protected override List<Type> GetControllerTypes()
        {
            List<Type> appDataControllerTypes = this.LoadTypesBySubclass(this.GetType().Assembly, "WebApi", typeof(WebApi.Controllers.AppDataController));
            List<Type> appReportControllerTypes = this.LoadTypesBySubclass (this.GetType().Assembly, "WebApi", typeof(WebApi.Controllers.AppReportController));
            List<Type> controllerTypes = new List<Type>();
            controllerTypes.AddRange(appDataControllerTypes);
            controllerTypes.AddRange(appReportControllerTypes);
            controllerTypes.Add(typeof(GroupController));

            return controllerTypes;
        }
        //---------------------------------------------------------------------------------------------
        protected override List<Type> GetLogicTypes()
        {
            List<Type> logicTypes = this.LoadTypesBySubclass(this.GetType().Assembly, "WebApi", typeof(WebApi.Logic.AppBusinessLogic));
            return logicTypes;
        }
        //---------------------------------------------------------------------------------------------
        protected override List<Type> GetBranchTypes()
        {
            //List<Type> branchTypes1 = this.LoadTypesBySubclass(this.GetType().Assembly, "WebApi", typeof(FwStandard.Security.FwSecurityTreeBranch));
            //List<Type> branchTypes2 = this.LoadTypesBySubclass(this.GetType().Assembly, "WebLibrary", typeof(FwStandard.Security.FwSecurityTreeBranch));
            List<Type> branchTypes = new List<Type>();
            //branchTypes.AddRange(branchTypes1);
            //branchTypes.AddRange(branchTypes2);
            return branchTypes;
        }
        //---------------------------------------------------------------------------------------------
    }
}

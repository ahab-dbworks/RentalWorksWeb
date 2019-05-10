using FwStandard.Models;
using FwStandard.Security;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;

namespace WebApi.Modules.AccountServices
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "accountservices-v1")]
    public class AccountController : AppController
    {
        public AccountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {

        }
        //---------------------------------------------------------------------------------------------
        public class GetSessionResponse
        {
            public AppFunc.SessionLocation location { get; set; } = null;
            public AppFunc.SessionWarehouse warehouse { get; set; } = null;
            public AppFunc.SessionDepartment department { get; set; } = null;
            public AppFunc.SessionUser webUser { get; set; } = null;
            public FwSecurityTreeNode applicationtree { get; set; } = null;
            public dynamic applicationOptions { get; set; } = null;
            public string clientcode { get; set; } = string.Empty;
            public string serverVersion { get; set; } = string.Empty;
        }

        // GET api/v1/account/sessioninfo
        [HttpGet("session")]
        public async Task<GetSessionResponse> GetSession([FromQuery]string applicationId)
        {
            var response = new GetSessionResponse();
            response.webUser = await AppFunc.GetSessionUser(this.AppConfig, this.UserSession);

            // run the rest of the queries in parallel
            var taskSessionLocation = AppFunc.GetSessionLocation(this.AppConfig, response.webUser.locationid);
            var taskSessionWarehouse = AppFunc.GetSessionWarehouse(this.AppConfig, response.webUser.warehouseid);
            var taskSessionDepartment = AppFunc.GetSessionDepartment(this.AppConfig, response.webUser.departmentid);
            var taskClientCode = FwSqlData.GetClientCodeAsync(this.AppConfig.DatabaseSettings);
            var taskApplicationTree = FwSecurityTree.Tree.GetGroupsTreeAsync(applicationId, this.UserSession.GroupsId, true);
            var taskApplicationOptions = FwSqlData.GetApplicationOptionsAsync(this.AppConfig.DatabaseSettings);

            // wait for all the queries to finish
            Task.WaitAll(new Task[] { taskSessionLocation, taskSessionWarehouse, taskSessionDepartment, taskClientCode, taskApplicationTree, taskApplicationOptions });
            
            response.location = taskSessionLocation.Result;
            response.warehouse = taskSessionWarehouse.Result;
            response.department = taskSessionDepartment.Result;
            response.clientcode = taskClientCode.Result;
            response.applicationtree = taskApplicationTree.Result;
            response.applicationOptions = taskApplicationOptions.Result;

            // get the application version
            switch (applicationId)
            {
                // RentalWorksWeb
                case "{0A5F2584-D239-480F-8312-7C2B552A30BA}":
                    response.serverVersion = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "version-RentalWorksWeb.txt"));
                    break;
                // RentalWorks QuikScan
                case "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}":
                    response.serverVersion = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "version-QuikScan.txt"));
                    break;
                // TrakitWorksWeb
                case "{D901DE93-EC22-45A1-BB4A-DD282CAF59FB}":
                    response.serverVersion = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "version-TrakitWorksWeb.txt"));
                    break;
                // WebApi
                case "{94FBE349-104E-420C-81E9-1636EBAE2836}":
                    response.serverVersion = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "version.txt"));
                    break;
            }

            await Task.CompletedTask;
            return response;
        }
        //---------------------------------------------------------------------------------------------
        public class GetOfficeLocationResponse
        {
            public AppFunc.SessionLocation location { get; set; } = null;
            public AppFunc.SessionWarehouse warehouse { get; set; } = null;
            public AppFunc.SessionDepartment department { get; set; } = null;
        }
        
        // GET api/v1/account/locationinfo?locationid=value&warehouseid=value&departmentid=value
        [HttpGet("officelocation")]
        public async Task<GetOfficeLocationResponse> GetOfficeLocation([FromQuery]string locationid, [FromQuery]string warehouseid, [FromQuery]string departmentid)
        {
            // run all the queries in parallel
            var taskSessionLocation = AppFunc.GetSessionLocation(this.AppConfig, locationid);
            var taskSessionWarehouse = AppFunc.GetSessionWarehouse(this.AppConfig, warehouseid);
            var taskSessionDepartment = AppFunc.GetSessionDepartment(this.AppConfig, departmentid);

            // wait for all the queries to finish
            Task.WaitAll(new Task[] { taskSessionLocation, taskSessionWarehouse, taskSessionDepartment });

            var response = new GetOfficeLocationResponse();
            response.location = taskSessionLocation.Result;
            response.warehouse = taskSessionWarehouse.Result;
            response.department = taskSessionDepartment.Result;
            await Task.CompletedTask;
            return response;
        }
        //---------------------------------------------------------------------------------------------
    }
}

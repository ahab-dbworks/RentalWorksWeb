using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;

namespace WebApi.Modules.AccountServices.Account
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "accountservices-v1")]
    [FwController(Id: "R6UM7U78W2dB")]
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
            public AppFunc.SessionDeal deal { get; set; } = null;
            public FwAmSecurityTreeNode applicationtree { get; set; } = null;
            public dynamic applicationOptions { get; set; } = null;
            public string clientcode { get; set; } = string.Empty;
            public string serverVersion { get; set; } = string.Empty;
            public SystemSettingsResponse  systemSettings{ get; set; } = null;
        }
        //---------------------------------------------------------------------------------------------
        // GET api/v1/account/sessioninfo
        [HttpGet("session")]
        [FwControllerMethod(Id: "hC5MXcjWFqjb", ValidateSecurityGroup:false)]
        public async Task<ActionResult<GetSessionResponse>> GetSession([FromQuery]string applicationId)
        {
            AccountLogic account = FwBusinessLogic.CreateBusinessLogic<AccountLogic>(this.AppConfig, this.UserSession);
            var response = new GetSessionResponse();
            var waitList = new List<Task>();
            Task<AppFunc.SessionDeal> taskSessionDeal = null;

            response.webUser = await AppFunc.GetSessionUserAsync(this.AppConfig, this.UserSession);
            if (this.UserSession.UserType == "CONTACT")
            {
                taskSessionDeal = AppFunc.GetSessionDealAsync(this.AppConfig, response.webUser.contactid);
                waitList.Add(taskSessionDeal);
            }
            var taskSessionLocation = AppFunc.GetSessionLocationAsync(this.AppConfig, response.webUser.locationid);
            waitList.Add(taskSessionLocation);
            var taskSessionWarehouse = AppFunc.GetSessionWarehouseAsync(this.AppConfig, response.webUser.warehouseid);
            waitList.Add(taskSessionWarehouse);
            var taskSessionDepartment = AppFunc.GetSessionDepartmentAsync(this.AppConfig, response.webUser.departmentid);
            waitList.Add(taskSessionDepartment);
            var taskClientCode = FwSqlData.GetClientCodeAsync(this.AppConfig.DatabaseSettings);
            waitList.Add(taskClientCode);
            var taskApplicationTree = FwAppManager.Tree.GetGroupsTreeAsync(this.UserSession.GroupsId, true);
            waitList.Add(taskApplicationTree);

            dynamic taskApplicationOptions = null;
            if ((!string.IsNullOrEmpty(applicationId)) && (applicationId.Equals("{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}")))  // RentalWorks QuikScan
            {
                taskApplicationOptions = FwSqlData.GetApplicationOptionsAsync(this.AppConfig.DatabaseSettings);
                waitList.Add(taskApplicationOptions);
            }
            
            var taskSystemSettings = account.GetSystemSettingsAsync();
            waitList.Add(taskSystemSettings);

            // wait for all the queries to finish
            Task.WaitAll(waitList.ToArray());

            if (this.UserSession.UserType == "CONTACT")
            {
                if (taskSessionDeal != null)
                {
                    if (taskSessionDeal.Result == null)
                    {
                        return new ForbidResult();
                    }
                    response.deal = taskSessionDeal.Result;
                }
            }
            response.location = taskSessionLocation.Result;
            response.warehouse = taskSessionWarehouse.Result;
            response.department = taskSessionDepartment.Result;
            response.clientcode = taskClientCode.Result;
            response.applicationtree = taskApplicationTree.Result.RootNode;
            if ((!string.IsNullOrEmpty(applicationId)) && (applicationId.Equals("{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}")))  // RentalWorks QuikScan
            {
                response.applicationOptions = taskApplicationOptions.Result;
            }
            response.systemSettings = taskSystemSettings.Result;

            // get the application version
            bool returnedVersion = false;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                string pathRentalWorksWebVersion = System.IO.Path.Combine(Environment.CurrentDirectory, "version-RentalWorksWeb.txt");
                string pathTrakitWorksWebVersion = System.IO.Path.Combine(Environment.CurrentDirectory, "version-TrakitWorksWeb.txt");
                switch (applicationId)
                {
                    // RentalWorksWeb
                    case "{0A5F2584-D239-480F-8312-7C2B552A30BA}":
                        if (System.IO.File.Exists(pathRentalWorksWebVersion))
                        {
                            response.serverVersion = System.IO.File.ReadAllText(pathRentalWorksWebVersion);
                            returnedVersion = true;
                        }
                        break;
                    // RentalWorks QuikScan
                    case "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}":
                        if (this.AppConfig.DatabaseSettings.ConnectionString.ToLower().Contains("rentalworks") && (System.IO.File.Exists(pathRentalWorksWebVersion)))
                        {
                            response.serverVersion = System.IO.File.ReadAllText(pathRentalWorksWebVersion);
                            returnedVersion = true;
                        }
                        else if (this.AppConfig.DatabaseSettings.ConnectionString.ToLower().Contains("trakitworks") && (System.IO.File.Exists(pathTrakitWorksWebVersion)))
                        {
                            response.serverVersion = System.IO.File.ReadAllText(pathTrakitWorksWebVersion);
                            returnedVersion = true;
                        }
                        break;
                    // TrakitWorksWeb
                    case "{D901DE93-EC22-45A1-BB4A-DD282CAF59FB}":
                        if (System.IO.File.Exists(pathTrakitWorksWebVersion))
                        {
                            response.serverVersion = System.IO.File.ReadAllText(pathTrakitWorksWebVersion);
                            returnedVersion = true;
                        }
                        break;
                    // WebApi
                    case "{94FBE349-104E-420C-81E9-1636EBAE2836}":
                        response.serverVersion = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "version.txt"));
                        returnedVersion = true;
                        break;
                }
            }
            if (!returnedVersion)
            {
                string pathVersion = System.IO.Path.Combine(Environment.CurrentDirectory, "version.txt");
                response.serverVersion = System.IO.File.ReadAllText(pathVersion);
                returnedVersion = true;
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
        //---------------------------------------------------------------------------------------------
        // GET api/v1/account/locationinfo?locationid=value&warehouseid=value&departmentid=value
        [HttpGet("officelocation")]
        [FwControllerMethod(Id: "d22TgeY4ersd", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]  //justin hoffman 12/16/2019 added ValidateSecurityGroup=false as a temporary fix for #1473.  
        public async Task<ActionResult<GetOfficeLocationResponse>> GetOfficeLocation([FromQuery]string locationid, [FromQuery]string warehouseid, [FromQuery]string departmentid)
        {
            // run all the queries in parallel
            var taskSessionLocation = AppFunc.GetSessionLocationAsync(this.AppConfig, locationid);
            var taskSessionWarehouse = AppFunc.GetSessionWarehouseAsync(this.AppConfig, warehouseid);
            var taskSessionDepartment = AppFunc.GetSessionDepartmentAsync(this.AppConfig, departmentid);

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
        // POST api/v1/account/resetpassword
        [HttpPost("resetpassword")]
        [FwControllerMethod(Id: "CL8bnxKGRvMO", ValidateSecurityGroup:false)]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = new ResetPasswordResponse();
                var account  = FwBusinessLogic.CreateBusinessLogic<AccountLogic>(this.AppConfig, this.UserSession);

                response = await account.ValidatePassword(request);
                if (response.Status == 0)
                {
                    response = await account.ResetPassword(request);
                }
                else
                {
                    var jsonException        = new FwApiException();
                    jsonException.StatusCode = StatusCodes.Status400BadRequest;
                    jsonException.Message    = response.Message;
                    return StatusCode(jsonException.StatusCode, jsonException);
                }

                return response;
            }
            catch (Exception ex)
            {
                var jsonException        = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message    = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}

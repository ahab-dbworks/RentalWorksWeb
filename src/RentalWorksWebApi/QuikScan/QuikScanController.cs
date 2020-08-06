using FwCore.Mobile;
using FwStandard.AppManager;
using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.Modules.Mobile.QuikScan
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id: "L2TmlkfDPVoo")]
    public class QuikScanController : FwJsonServiceController
    {
        RentalWorksQuikScan.Modules.FillContainer fillContainer;
        RwService rwService;
        //---------------------------------------------------------------------------------------------
        public QuikScanController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {
            fillContainer = new RentalWorksQuikScan.Modules.FillContainer(appConfig.Value);
            rwService = new RwService(this.AppConfig);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [FwControllerMethod("x78Eg9qFDfUA", FwControllerActionTypes.ApiMethod, AllowAnonymous:false, ValidateSecurityGroup:false)]
        public async Task<ActionResult<JObject>> ProcessRequestAsync([FromQuery]string path, [FromBody]JObject jsonRequest)
        {
            await Task.CompletedTask;
            //return Ok(jsonRequest);
            return await base.ProcessRequestAsync(this.ControllerContext, path, jsonRequest);
        }
        //---------------------------------------------------------------------------------------------
        protected string GetRegexString(string path)
        {
            string regex;

            regex = @"^" + Regex.Escape(path) + "$";

            return regex;
        }
        //---------------------------------------------------------------------------------------------
        protected string GetRegexStringNoEndOfString(string path)
        {
            string regex;

            regex = @"^" + Regex.Escape(path);

            return regex;
        }

        protected override List<FwJsonRequestAction> GetRequestActions()
        {
            List<FwJsonRequestAction> actions = new List<FwJsonRequestAction>() {
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/services/")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await this.ServiceRoutingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/validation/")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await this.ServiceRoutingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/module/")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await this.ServiceRoutingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/grid/")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await this.ServiceRoutingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/reports/")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await this.ServiceRoutingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/getauthtoken")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.GetAuthTokenAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.WebUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/changepassword")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) 
                    {
                        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                        {
                            await AccountService.Current.WebUserChangePasswordAsync(conn, request, response, session);  
                        }
                    }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/resetpassword")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) 
                    {
                        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
{
                            await AccountService.Current.WebUserResetPasswordAsync(conn, request, response, session);  
}
                    }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/authpassword")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) 
                    {
                        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                        {
                            await AccountService.Current.AuthenticatePasswordAsync(conn, request, response, session);  
                        }
                    }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/addallqtyitemstocontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.AddAllQtyItemsToContainerAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/closecontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.CloseContainerAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/createcontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.CreateContainerAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/getcontaineritems")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.GetContainerItemsAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/getcontainerpendingitems")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.GetContainerPendingItems(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/hascheckinfillcontainerbutton")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.HasCheckinFillContainerButton(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/removeitemfromcontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.RemoveItemFromContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/selectcontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.SelectContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/setcontainerno")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.SetContainerNo(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/instantiatecontainer")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await fillContainer.InstantiateContainerAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/getbarcodefromrfid")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.GetBarcodeFromRfidAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/phycountitem")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.PhyCountItemAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/repairitem")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.RepairItemAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/addinventorywebimage")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.AddInventoryWebImageAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/selectrepairorder")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.SelectRepairOrderAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/repairstatusrfid")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.RepairStatusRFIDAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/qcstatusrfid")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.QCStatusRFIDAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/cancelcontract")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) {await rwService.CancelContractAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/checkinitem")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.CheckInItemAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/createnewincontractandsuspend")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.CreateNewInContractAndSuspendAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestpath) { return Regex.IsMatch(requestpath, GetRegexString("/order/createoutcontract")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.CreateOutContractAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestpath) { return Regex.IsMatch(requestpath, GetRegexString("/order/createincontract")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.CreateInContractAsync(request, response, session); }
                ),
                //new FwJsonRequestAction(
                //    roles: new string[]{RwUserRoles.RentalWorksUser},
                //    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getitemstatus")); },
                //    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetItemStatus(request, response, session); }
                //),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/selectsession")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.SelectSessionAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/pdastageitem")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.PdaStageItemAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/stageallqtyitems")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.StageAllQtyItemsAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/unstageitem")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.UnstageItemAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getrfidstatus")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.GetRFIDStatusAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/loadrfidpending")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.LoadRFIDPendingAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/loadrfidexceptions")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.LoadRFIDExceptionsAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/processrfidexception")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.ProcessRFIDExceptionAsync(request, response, session); }
                ),
                //new FwJsonRequestAction(
                //    roles: new string[]{RwUserRoles.RentalWorksUser},
                //    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/itemstatusrfid")); },
                //    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ItemStatusRFID(request, response, session); }
                //),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/rfidclearsession")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.RFIDClearSessionAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogsearch")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.TimeLogSearchAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogsubmit")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.TimeLogSubmitAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogviewentries")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.TimeLogViewEntriesAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getorderitemstosub")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.GetOrderItemsToSubAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getordercompletestosub")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.GetOrderCompletesToSubAsync(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/substituteatstaging")); },
                    onMatch: async delegate(dynamic request, dynamic response, dynamic session) { await rwService.SubstituteAtStagingAsync(request, response, session); }
                )
            };
            return actions;
        }
        //---------------------------------------------------------------------------------------------
        protected override dynamic GetSession(dynamic request, dynamic response)
        {
            //dynamic authTokenData;
            //bool hasAuthToken;
            //FormsAuthenticationTicket token;

            dynamic session = new ExpandoObject();
            session.security = new ExpandoObject();
            session.security.userRoles = new List<string>();
            session.security.userRoles.Add(RwUserRoles.Everyone);
            session.security.webUser = new ExpandoObject();
            session.security.webUser.webusersid = this.UserSession.WebUsersId;
            session.security.webUser.usersid = this.UserSession.UsersId;
            session.security.webUser.usertype = this.UserSession.UserType;
            //session.security.webUser.webadministrator = this.UserSession.IsWebAdministrator;
            if (session.security.webUser != null)
            {
                switch ((string)session.security.webUser.usertype)
                {
                    case "USER":
                        session.security.userRoles.Add(RwUserRoles.RentalWorksUser);
                        //if (session.security.webUser.webadministrator == "T")
                        //{
                        //    session.security.userRoles.Add(RwUserRoles.RentalWorksUserAdministrator);
                        //}
                        break;
                    case "CONTACT":
                        session.security.userRoles.Add(RwUserRoles.RentalWorksDealContact);
                        //if (session.security.webUser.webadministrator == "T")
                        //{
                        //    session.security.userRoles.Add(RwUserRoles.RentalWorksDealContactAdministrator);
                        //}
                        break;
                    case "CREW":
                        session.security.userRoles.Add(RwUserRoles.RentalWorksCrew);
                        break;
                }
            }

            //hasAuthToken = (FwValidate.IsPropertyDefined(request, "authToken"));
            //if (hasAuthToken)
            //{
            //    //token = AccountService.Current.GetAuthToken(request.authToken);
            //    //authTokenData = AccountService.Current.GetAuthTokenData(token.UserData);

            //    if (authTokenData != null)
            //    {
            //        if (authTokenData.siteName != FwApplicationConfig.CurrentSite.Name)
            //        {
            //            throw new Exception("Auth token is not valid at this site.");
            //        }
            //        session.security.webUser = authTokenData.webUser;
            //        if (session.security.webUser != null)
            //        {
            //            switch ((string)session.security.webUser.usertype)
            //            {
            //                case "USER":
            //                    session.security.userRoles.Add(RwUserRoles.RentalWorksUser);
            //                    if (session.security.webUser.webadministrator == "T")
            //                    {
            //                        session.security.userRoles.Add(RwUserRoles.RentalWorksUserAdministrator);
            //                    }
            //                    break;
            //                case "CONTACT":
            //                    session.security.userRoles.Add(RwUserRoles.RentalWorksDealContact);
            //                    if (session.security.webUser.webadministrator == "T")
            //                    {
            //                        session.security.userRoles.Add(RwUserRoles.RentalWorksDealContactAdministrator);
            //                    }
            //                    break;
            //                case "CREW":
            //                    session.security.userRoles.Add(RwUserRoles.RentalWorksCrew);
            //                    break;
            //            }
            //        }

            //    }
            //}
            return session;
        }
        //---------------------------------------------------------------------------------------------
        protected async Task ServiceRoutingAsync(dynamic request, dynamic response, dynamic session)
        {
            string componentType, name, method, path = string.Empty;
            string[] pathFragments;
            Type type;
            MethodInfo methodInfo;

            
            if (this.ControllerContext.HttpContext.Request.Query["path"].Count > 0)
            {
                path = this.ControllerContext.HttpContext.Request.Query["path"][0];
            }
            pathFragments = path.Split('/');
            componentType = pathFragments[1].ToLower();
            name = pathFragments[2];
            method = pathFragments[3];
            switch (componentType)
            {
                case "services":
                    type = typeof(QuikScanController).Assembly.GetType("RentalWorksQuikScan.Modules." + name, false);
                    object service = Activator.CreateInstance(type, this.AppConfig);
                    methodInfo = service.GetType().GetMethod(method);
                    FwJsonServiceMethodAttribute[] serviceMethodAttributes = (FwJsonServiceMethodAttribute[])methodInfo.GetCustomAttributes(typeof(FwJsonServiceMethodAttribute), false);
                    if ((serviceMethodAttributes.Length > 0) && methodInfo.IsPublic)
                    {
                        FwJsonServiceMethodAttribute serviceMethodAttribute = serviceMethodAttributes[0];
                        List<string> requiredparameters = new List<string>(serviceMethodAttribute.RequiredParameters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                        for (int i = 0; i < requiredparameters.Count; i++)
                        {
                            FwValidate.TestPropertyDefined(name + "." + method, request, requiredparameters[i]);
                        }
                        Task result = (Task)methodInfo.Invoke(service, new object[] { request, response, session });
                        await result;
                    }
                    else
                    {
                        throw new Exception("Unable to find web service: " + name + "." + method);
                    }
                    break;
                //case "module":
                //    type = typeof(QuikScanController).Assembly.GetType("RentalWorksQuikScan.Source.Modules." + name, false);
                //    if ((type != null) && (type.IsSubclassOf(typeof(FwModule))))
                //    {
                //        FwModule module = (FwModule)Activator.CreateInstance(type);
                //        module.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                //        if (module != null)
                //        {
                //            methodInfo = typeof(FwModule).GetMethod(method);
                //            if (methodInfo.IsPublic)
                //            {
                //                typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        type = typeof(FwValidation).Assembly.GetType("Fw.Json.Services.Modules.Fw" + name, false);
                //        if ((type != null) && (type.IsSubclassOf(typeof(FwModule))))
                //        {
                //            FwModule module = (FwModule)Activator.CreateInstance(type);
                //            module.Init("Fw.Json.Services", "Fw", typeof(FwModule).Assembly, request, response, session);
                //            if (module != null)
                //            {
                //                methodInfo = typeof(FwModule).GetMethod(method);
                //                if (methodInfo.IsPublic)
                //                {
                //                    typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("There is no web service setup for module: " + name);
                //        }
                //    }
                //    break;
                //case "grid":
                //    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Source.Grids." + name, false);
                //    if ((type != null) && (type.IsSubclassOf(typeof(FwGrid))))
                //    {
                //        FwGrid grid = (FwGrid)Activator.CreateInstance(type);
                //        grid.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                //        if (grid != null)
                //        {
                //            methodInfo = typeof(FwGrid).GetMethod(method);
                //            if (methodInfo.IsPublic)
                //            {
                //                typeof(FwModule).GetMethod(method).Invoke(grid, new object[0]);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        type = typeof(FwGrid).Assembly.GetType("Fw.Json.Services.Grids.Fw" + name, false);
                //        if ((type != null) && (type.IsSubclassOf(typeof(FwGrid))))
                //        {
                //            FwGrid grid = (FwGrid)Activator.CreateInstance(type);
                //            grid.Init("Fw.Json.Services", "Fw", typeof(FwGrid).Assembly, request, response, session);
                //            if (grid != null)
                //            {
                //                methodInfo = typeof(FwGrid).GetMethod(method);
                //                if (methodInfo.IsPublic)
                //                {
                //                    typeof(FwGrid).GetMethod(method).Invoke(grid, new object[0]);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("There is no web service setup for grid: " + name);
                //        }
                //    }
                //    break;
                //case "validation":
                //    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Source.Validations." + name, false);
                //    if ((type != null) && (type.IsSubclassOf(typeof(FwValidation))))
                //    {
                //        FwValidation validation = (FwValidation)Activator.CreateInstance(type);
                //        validation.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                //        if (validation != null)
                //        {
                //            methodInfo = typeof(FwValidation).GetMethod(method);
                //            if (methodInfo.IsPublic)
                //            {
                //                typeof(FwModule).GetMethod(method).Invoke(validation, new object[0]);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        type = typeof(FwValidation).Assembly.GetType("Fw.Json.Services.Validations.Fw" + name, false);
                //        if ((type != null) && (type.IsSubclassOf(typeof(FwValidation))))
                //        {
                //            FwValidation validation = (FwValidation)Activator.CreateInstance(type);
                //            validation.Init("Fw.Json.Services", "Fw", typeof(FwValidation).Assembly, request, response, session);
                //            if (validation != null)
                //            {
                //                methodInfo = typeof(FwValidation).GetMethod(method);
                //                if (methodInfo.IsPublic)
                //                {
                //                    typeof(FwModule).GetMethod(method).Invoke(validation, new object[0]);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("There is no web service setup for validation: " + name);
                //        }
                //    }
                //    break;
                //case "reports":
                //    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Reports.Rw" + name, false);
                //    if ((type != null) && (type.IsSubclassOf(typeof(FwReport))))
                //    {
                //        FwReport report = (FwReport)Activator.CreateInstance(type);
                //        report.Init(request, response, session);
                //        if (report != null)
                //        {
                //            methodInfo = typeof(FwReport).GetMethod(method);
                //            if (methodInfo.IsPublic)
                //            {
                //                typeof(FwModule).GetMethod(method).Invoke(report, new object[0]);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        type = typeof(FwReport).Assembly.GetType("Fw.Json.Services.Reports.Fw" + name, false);
                //        if ((type != null) && (type.IsSubclassOf(typeof(FwReport))))
                //        {
                //            FwReport report = (FwReport)Activator.CreateInstance(type);
                //            report.Init(request, response, session);
                //            if (report != null)
                //            {
                //                methodInfo = typeof(FwReport).GetMethod(method);
                //                if (methodInfo.IsPublic)
                //                {
                //                    typeof(FwModule).GetMethod(method).Invoke(report, new object[0]);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("There is no web service setup for report: " + name);
                //        }
                //    }
                //    break;
            }

        }
    }
}

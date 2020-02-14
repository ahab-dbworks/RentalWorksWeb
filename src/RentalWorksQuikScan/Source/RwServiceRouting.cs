using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using RentalWorksQuikScan.Modules;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace RentalWorksQuikScan.Source
{
    public class RwServiceRouting : Fw.Json.Services.FwJsonService
    {
        //---------------------------------------------------------------------------------------------
        protected string GetRegexString(string path, string applicationPath)
        {
            string regex;
            
            regex = @"^" + Regex.Escape(applicationPath + @"/services.ashx?path=" + path) + "$";

            return regex;
        }
        //---------------------------------------------------------------------------------------------
        protected string GetRegexStringNoEndOfString(string path, string applicationPath)
        {
            string regex;
            
            regex = @"^" + Regex.Escape(applicationPath + @"/services.ashx?path=" + path);

            return regex;
        }
        //---------------------------------------------------------------------------------------------
        protected override List<FwJsonRequestAction> GetRequestActions()
        {
            List<FwJsonRequestAction> actions = new List<FwJsonRequestAction>() {
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/services/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwServiceRouting.ServiceRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/validation/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwServiceRouting.ServiceRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/module/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwServiceRouting.ServiceRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/grid/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwServiceRouting.ServiceRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexStringNoEndOfString("/reports/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwServiceRouting.ServiceRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/getauthtoken", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetAuthToken(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.WebUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/changepassword", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { AccountService.Current.WebUserChangePassword(FwSqlConnection.RentalWorks, request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/resetpassword", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { AccountService.Current.WebUserResetPassword(FwSqlConnection.RentalWorks, request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/account/authpassword", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { AccountService.Current.AuthenticatePassword(FwSqlConnection.RentalWorks, request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/addallqtyitemstocontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.AddAllQtyItemsToContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/closecontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.CloseContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/createcontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.CreateContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/getcontaineritems", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.GetContainerItems(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/getcontainerpendingitems", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.GetContainerPendingItems(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/hascheckinfillcontainerbutton", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.HasCheckinFillContainerButton(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/removeitemfromcontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.RemoveItemFromContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/selectcontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.SelectContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/setcontainerno", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.SetContainerNo(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fillcontainer/instantiatecontainer", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FillContainer.InstantiateContainer(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/getbarcodefromrfid", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetBarcodeFromRfid(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/phycountitem", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.PhyCountItem(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/repairitem", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.RepairItem(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/addinventorywebimage", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.AddInventoryWebImage(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/selectrepairorder", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.SelectRepairOrder(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/repairstatusrfid", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.RepairStatusRFID(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/inventory/qcstatusrfid", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.QCStatusRFID(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/cancelcontract", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.CancelContract(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/checkinitem", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.CheckInItem(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/createnewincontractandsuspend", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.CreateNewInContractAndSuspend(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestpath, string applicationPath) { return Regex.IsMatch(requestpath, GetRegexString("/order/createoutcontract", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.CreateOutContract(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestpath, string applicationPath) { return Regex.IsMatch(requestpath, GetRegexString("/order/createincontract", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.CreateInContract(request, response, session); }
                ),
                //new FwJsonRequestAction(
                //    roles: new string[]{RwUserRoles.RentalWorksUser},
                //    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getitemstatus", applicationPath)); },
                //    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetItemStatus(request, response, session); }
                //),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/selectsession", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.SelectSession(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/pdastageitem", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.PdaStageItem(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/stageallqtyitems", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.StageAllQtyItems(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/unstageitem", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.UnstageItem(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getrfidstatus", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetRFIDStatus(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/loadrfidpending", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.LoadRFIDPending(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/loadrfidexceptions", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.LoadRFIDExceptions(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/processrfidexception", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ProcessRFIDException(request, response, session); }
                ),
                //new FwJsonRequestAction(
                //    roles: new string[]{RwUserRoles.RentalWorksUser},
                //    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/itemstatusrfid", applicationPath)); },
                //    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ItemStatusRFID(request, response, session); }
                //),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/rfidclearsession", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.RFIDClearSession(request, response, session); }
                ), 
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogsearch", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.TimeLogSearch(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogsubmit", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.TimeLogSubmit(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser, RwUserRoles.RentalWorksCrew},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/utility/timelogviewentries", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.TimeLogViewEntries(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getorderitemstosub", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetOrderItemsToSub(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/getordercompletestosub", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.GetOrderCompletesToSub(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/order/substituteatstaging", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.SubstituteAtStaging(request, response, session); }
                )
            };
            return actions;
        }
        //---------------------------------------------------------------------------------------------
        // Get an object to hold state information that can be passed around for the duration of the current request.
        protected override dynamic GetSession(dynamic request, dynamic response)
        {
            dynamic authTokenData, session;
            bool hasAuthToken;
            FormsAuthenticationTicket token;
            
            session = new ExpandoObject();
            session.security = new ExpandoObject();
            session.security.userRoles = new List<string>();
            session.security.userRoles.Add(RwUserRoles.Everyone);
            hasAuthToken = (FwValidate.IsPropertyDefined(request, "authToken"));
            if (hasAuthToken)
            {
                token = AccountService.Current.GetAuthToken(request.authToken);
                authTokenData = AccountService.Current.GetAuthTokenData(token.UserData);

                if (authTokenData != null)
                {
                    if(authTokenData.siteName != FwApplicationConfig.CurrentSite.Name)
                    {
                        throw new Exception("Auth token is not valid at this site.");
                    }
                    session.security.webUser = authTokenData.webUser;
                    if (session.security.webUser != null)
                    {
                        switch((string)session.security.webUser.usertype)
                        {
                            case "USER":
                                session.security.userRoles.Add(RwUserRoles.RentalWorksUser);
                                if (session.security.webUser.webadministrator == "T")
                                {
                                    session.security.userRoles.Add(RwUserRoles.RentalWorksUserAdministrator);
                                }
                                break;
                            case "CONTACT":
                                session.security.userRoles.Add(RwUserRoles.RentalWorksDealContact);
                                if (session.security.webUser.webadministrator == "T")
                                {
                                    session.security.userRoles.Add(RwUserRoles.RentalWorksDealContactAdministrator);
                                }
                                break;
                            case "CREW":
                                session.security.userRoles.Add(RwUserRoles.RentalWorksCrew);
                                break;
                        }
                    }
                    
                }
            }
            return session;
        }
        //---------------------------------------------------------------------------------------------
        public static void ServiceRouting(dynamic request, dynamic response, dynamic session)
        {
            string componentType, name, method, path;
            string[] pathFragments;
            Type type;
            MethodInfo methodInfo;

            path = HttpContext.Current.Request.QueryString["path"];
            pathFragments = path.Split('/');
            componentType = pathFragments[1].ToLower();
            name          = pathFragments[2];
            method        = pathFragments[3];
            switch(componentType)
            {
                case "services":
                    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Modules." + name, false);
                    object service = Activator.CreateInstance(type);
                    methodInfo = service.GetType().GetMethod(method);
                    FwJsonServiceMethodAttribute[] serviceMethodAttributes = (FwJsonServiceMethodAttribute[])methodInfo.GetCustomAttributes(typeof(FwJsonServiceMethodAttribute), false);
                    if ((serviceMethodAttributes.Length > 0) && methodInfo.IsPublic)
                    {
                        FwJsonServiceMethodAttribute serviceMethodAttribute = serviceMethodAttributes[0];
                        List<string> requiredparameters = new List<string>(serviceMethodAttribute.RequiredParameters.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries));
                        for (int i = 0; i < requiredparameters.Count; i++)
                        {
                            FwValidate.TestPropertyDefined(name + "." + method, request, requiredparameters[i]);
                        }
                        methodInfo.Invoke(service, new object[] {request, response, session});
                    }
                    else
                    {
                        throw new Exception("Unable to find web service: " + name + "." + method);
                    }
                    break;
                case "module":
                    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Source.Modules." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwModule))))
                    {
                        FwModule module = (FwModule)Activator.CreateInstance(type);
                        module.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                        if (module != null)
                        {
                            methodInfo = typeof(FwModule).GetMethod(method);
                            if (methodInfo.IsPublic)
                            {
                                typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
                            }
                        }
                    }
                    else
                    {
                        type = typeof(FwValidation).Assembly.GetType("Fw.Json.Services.Modules.Fw" + name, false);
                        if ((type != null) && (type.IsSubclassOf(typeof(FwModule))))
                        {
                            FwModule module = (FwModule)Activator.CreateInstance(type);
                            module.Init("Fw.Json.Services", "Fw", typeof(FwModule).Assembly, request, response, session);
                            if (module != null)
                            {
                                methodInfo = typeof(FwModule).GetMethod(method);
                                if (methodInfo.IsPublic)
                                {
                                    typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for module: " + name);
                        }
                    }
                    break;
                case "grid":
                    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Source.Grids." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwGrid))))
                    {
                        FwGrid grid = (FwGrid)Activator.CreateInstance(type);
                        grid.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                        if (grid != null)
                        {
                            methodInfo = typeof(FwGrid).GetMethod(method);
                            if (methodInfo.IsPublic)
                            {
                                typeof(FwModule).GetMethod(method).Invoke(grid, new object[0]);
                            }
                        }
                    }
                    else
                    {
                        type = typeof(FwGrid).Assembly.GetType("Fw.Json.Services.Grids.Fw" + name, false);
                        if ((type != null) && (type.IsSubclassOf(typeof(FwGrid))))
                        {
                            FwGrid grid = (FwGrid)Activator.CreateInstance(type);
                            grid.Init("Fw.Json.Services", "Fw", typeof(FwGrid).Assembly, request, response, session);
                            if (grid != null)
                            {
                                methodInfo = typeof(FwGrid).GetMethod(method);
                                if (methodInfo.IsPublic)
                                {
                                    typeof(FwGrid).GetMethod(method).Invoke(grid, new object[0]);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for grid: " + name);
                        }
                    }
                    break;
                case "validation":
                    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Source.Validations." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwValidation))))
                    {
                        FwValidation validation = (FwValidation)Activator.CreateInstance(type);
                        validation.Init("RentalWorksQuikScan", "Rw", typeof(RwServiceRouting).Assembly, request, response, session);
                        if (validation != null)
                        {
                            methodInfo = typeof(FwValidation).GetMethod(method);
                            if (methodInfo.IsPublic)
                            {
                                typeof(FwModule).GetMethod(method).Invoke(validation, new object[0]);
                            }
                        }
                    }
                    else
                    {
                        type = typeof(FwValidation).Assembly.GetType("Fw.Json.Services.Validations.Fw" + name, false);
                        if ((type != null) && (type.IsSubclassOf(typeof(FwValidation))))
                        {
                            FwValidation validation = (FwValidation)Activator.CreateInstance(type);
                            validation.Init("Fw.Json.Services", "Fw", typeof(FwValidation).Assembly, request, response, session);
                            if (validation != null)
                            {
                                methodInfo = typeof(FwValidation).GetMethod(method);
                                if (methodInfo.IsPublic)
                                {
                                    typeof(FwModule).GetMethod(method).Invoke(validation, new object[0]);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for validation: " + name);
                        }
                    }
                    break;
                case "reports":
                    type = typeof(RwServiceRouting).Assembly.GetType("RentalWorksQuikScan.Reports.Rw" + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwReport))))
                    {
                        FwReport report = (FwReport)Activator.CreateInstance(type);
                        report.Init(request, response, session);
                        if (report != null)
                        {
                            methodInfo = typeof(FwReport).GetMethod(method);
                            if (methodInfo.IsPublic)
                            {
                                typeof(FwModule).GetMethod(method).Invoke(report, new object[0]);
                            }
                        }
                    }
                    else
                    {
                        type = typeof(FwReport).Assembly.GetType("Fw.Json.Services.Reports.Fw" + name, false);
                        if ((type != null) && (type.IsSubclassOf(typeof(FwReport))))
                        {
                            FwReport report = (FwReport)Activator.CreateInstance(type);
                            report.Init(request, response, session);
                            if (report != null)
                            {
                                methodInfo = typeof(FwReport).GetMethod(method);
                                if (methodInfo.IsPublic)
                                {
                                    typeof(FwModule).GetMethod(method).Invoke(report, new object[0]);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for report: " + name);
                        }
                    }
                    break;
            }
            
        }
        //---------------------------------------------------------------------------------------------
        //protected override string[] GetUserRoles(dynamic request)
        //{
        //    List<string> userRoles;
        //    string webUsersId, usersId, contactId;
        //    dynamic userData;
        //    userRoles = new List<string>();
        //    userRoles.Add(UserRoles.Everyone);
        //    if (FwFunc.IsPropertyDefined(request, "authToken"))
        //    {
                
        //        usersId = FwSqlData.GetUsersId(webUsersId);
        //        contactId = 
        //        if (!string.IsNullOrEmpty(usersId))
        //        {
        //            userRoles.Add(UserRoles.RentalWorksUser);
                
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(webUsersId))
        //    {
                
        //    }
        //    return userRoles.ToArray();
        //}
        //---------------------------------------------------------------------------------------------
    }
}

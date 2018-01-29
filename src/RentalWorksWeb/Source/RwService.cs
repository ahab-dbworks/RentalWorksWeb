using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Web.Security;

namespace Web.Source
{
    public class RwService
    {
        //---------------------------------------------------------------------------------------------
        public static void GetAuthToken(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetAuthToken";
            bool hasWebUser;
            AccountService.Current.GetAuthToken(FwSqlConnection.RentalWorks, request, response, session);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");
            if (response.errNo == 0)
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, session.security, "webUser");
                FwValidate.TestPropertyDefined(METHOD_NAME, session.security.webUser, "usersid");
                FwValidate.TestPropertyDefined(METHOD_NAME, session.security.webUser, "contactid");
                hasWebUser = FwValidate.IsPropertyDefined(session.security, "webUser");
                if (hasWebUser)
                {
                    if (!string.IsNullOrEmpty(session.security.webUser.usersid))
                    {
                        session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                                       , usersId: session.security.webUser.usersid);
                    }
                    if (!string.IsNullOrEmpty(session.security.webUser.contactid))
                    {
                        session.contact = RwAppData.GetContact(conn:      FwSqlConnection.RentalWorks
                                                             , contactId: session.security.webUser.contactid);
                    }
                }
                GetSite(request, response, session);

                //MY 7/28/2016: Not allowing contacts to login into RentalWorks Web yet
                if (session.security.webUser.usertype == "CONTACT")
                {
                    ((IDictionary<string, Object>)response).Remove("webUser");
                    ((IDictionary<string, Object>)response).Remove("applicationtree");
                    ((IDictionary<string, Object>)response).Remove("clientcode");
                    ((IDictionary<string, Object>)response).Remove("applicationOptions");
                    ((IDictionary<string, Object>)response).Remove("authToken");
                    ((IDictionary<string, Object>)response).Remove("site");

                    response.errNo = 4;
                    response.errMsg = "Contacts do not currently have access to the RentalWorks Web application at this time.";
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void AuthPassword(dynamic request, dynamic response, dynamic session)
        {
            bool isValid = false;
            const string METHOD_NAME = "AuthPassword";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");
            isValid = AccountService.Current.AuthenticatePassword(FwSqlConnection.RentalWorks, request, response, session);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetSite(dynamic request, dynamic response, dynamic session)
        {
            FwApplicationConfig_Site site = FwApplicationConfig.CurrentSite;
            
            response.site      = new ExpandoObject();
            response.site.name = site.Name;
        }
        //---------------------------------------------------------------------------------------------
        public static void GetApplicationOptions(dynamic request, dynamic response, dynamic session)
        {
            dynamic appoptions;

            appoptions = FwSqlData.GetApplicationOptions(FwSqlConnection.RentalWorks);
            FwValidate.TestPropertyDefined("RwService.GetApplicationOptions", appoptions, "mixedcase");
            response.appoptions = new ExpandoObject();
            response.appoptions.mixedcase = appoptions.mixedcase;
        }
        //---------------------------------------------------------------------------------------------
        public static void UpdateLocation(dynamic request, dynamic response, dynamic session)
        {
            FormsAuthenticationTicket token;
            dynamic authTokenData;

            token              = AccountService.Current.GetAuthToken(request.authToken);
            authTokenData      = AccountService.Current.GetAuthTokenData(token.UserData);

            authTokenData.webUser.locationid  = FwCryptography.AjaxDecrypt(request.location);
            authTokenData.webUser.warehouseid = FwCryptography.AjaxDecrypt(request.warehouse);

            response.authToken         = AccountService.Current.GetAuthToken(token.Name, authTokenData);
            response.location          = RwAppData.GetLocationInfo(FwSqlConnection.RentalWorks, FwCryptography.AjaxDecrypt(request.location));
            response.warehouse         = RwAppData.GetWarehouseInfo(FwSqlConnection.RentalWorks, FwCryptography.AjaxDecrypt(request.warehouse));
            response.department        = RwAppData.GetDepartmentInfo(FwSqlConnection.RentalWorks, FwCryptography.AjaxDecrypt(request.department));
        }
        //---------------------------------------------------------------------------------------------
        public static void ModuleRouting(dynamic request, dynamic response, dynamic session)
        {
            string componentType, name, method, path;
            string[] pathFragments;
            Type type;

            path = HttpContext.Current.Request.QueryString["path"];
            pathFragments = path.Split('/');
            componentType = pathFragments[1];
            componentType = char.ToUpper(componentType[0]) + componentType.Substring(1);
            name          = pathFragments[2];
            name          = char.ToUpper(name[0]) + name.Substring(1);
            method        = pathFragments[3];
            method        = char.ToUpper(method[0]) + method.Substring(1);
            switch(componentType)
            {
                case "Module":
                    if ((session.security.webUser.usertype == "CONTACT") && (!new List<string>(){"Driver","Vehicle"}.Contains(name))) throw new Exception("Access denied.");
                    type = typeof(RwService).Assembly.GetType("Web.Source.Modules." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwModule))))
                    {
                        FwModule module = (FwModule)Activator.CreateInstance(type);
                        module.Init("Web.Source", "", typeof(RwService).Assembly, request, response, session);
                        if (module != null)
                        {
                            typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
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
                                typeof(FwModule).GetMethod(method).Invoke(module, new object[0]);
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for module: " + name);
                        }
                    }
                    break;
                case "Grid":
                    if ((session.security.webUser.usertype == "CONTACT") && (!new List<string>(){"DriverLicenseClass","DriverEndorsement","DriverRestriction","DriverDocument","VehicleDocument", "AppDocumentVersion"}.Contains(name))) throw new Exception("Access denied.");
                    type = typeof(RwService).Assembly.GetType("Web.Source.Grids." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwGrid))))
                    {
                        FwGrid grid = (FwGrid)Activator.CreateInstance(type);
                        grid.Init("Web.Source", "", typeof(RwService).Assembly, request, response, session);
                        if (grid != null)
                        {
                            typeof(FwGrid).GetMethod(method).Invoke(grid, new object[0]);
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
                                typeof(FwGrid).GetMethod(method).Invoke(grid, new object[0]);
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for grid: " + name);
                        }
                    }
                    break;
                case "Validation":
                    if ((session.security.webUser.usertype == "CONTACT") && (!new List<string>(){"VehicleDocumentType"}.Contains(name))) throw new Exception("Access denied.");
                    type = typeof(RwService).Assembly.GetType("Web.Source.Validations." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwValidation))))
                    {
                        FwValidation validation = (FwValidation)Activator.CreateInstance(type);
                        validation.Init("Web.Source", "", typeof(RwService).Assembly, request, response, session);
                        if (validation != null)
                        {
                            typeof(FwValidation).GetMethod(method).Invoke(validation, new object[0]);
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
                                typeof(FwValidation).GetMethod(method).Invoke(validation, new object[0]);
                            }
                        }
                        else
                        {
                            throw new Exception("There is no web service setup for validation: " + name);
                        }
                    }
                    break;
                case "Reports":
                    if (session.security.webUser.usertype == "CONTACT") throw new Exception("Access denied.");
                    type = typeof(RwService).Assembly.GetType("Web.Source.Reports." + name, false);
                    if ((type != null) && (type.IsSubclassOf(typeof(FwReport))))
                    {
                        FwReport report = (FwReport)Activator.CreateInstance(type);
                        report.Init(request, response, session);
                        if (report != null)
                        {
                            typeof(FwReport).GetMethod(method).Invoke(report, new object[0]);
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
                                typeof(FwReport).GetMethod(method).Invoke(report, new object[0]);
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
    }
}
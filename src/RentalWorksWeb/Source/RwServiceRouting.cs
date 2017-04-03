﻿using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace RentalWorksWeb.Source
{
    public class RwServiceRouting : Fw.Json.Services.FwJsonService
    {
        //---------------------------------------------------------------------------------------------
        protected string GetRegexString(string path, string applicationPath)
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
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/module/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ModuleRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/grid/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ModuleRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/validation/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ModuleRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.User},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/reports/", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.ModuleRouting(request, response, session); }
                ),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.Everyone},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/session/updatelocation", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { RwService.UpdateLocation(request, response, session); }
                ),
                //new FwJsonRequestAction(
                //    roles: new string[]{RwUserRoles.User},
                //    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fwservices/setcompanycontactstatus", applicationPath)); },
                //    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FwServices.SetCompanyContactStatus(FwSqlConnection.RentalWorks, request, response, session); }
                //),
                new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.User},
                    isMatch: delegate(string requestPath, string applicationPath) { return Regex.IsMatch(requestPath, GetRegexString("/fwscheduler/getholidayevents", applicationPath)); },
                    onMatch: delegate(dynamic request, dynamic response, dynamic session) { FwServices.GetHolidayEvents(FwSqlConnection.RentalWorks, request, response, session); }
                )
            };
            return actions;
        }
        //---------------------------------------------------------------------------------------------
        // Get an object to hold state information that can be passed around for the duration of the current request.
        protected override dynamic GetSession(dynamic request, dynamic response)
        {
            dynamic authTokenData, session;
            bool hasAuthToken, /*invalidClientIP,*/ tokenIsExpired;
            FormsAuthenticationTicket token;
            FwControl controlrec;
            
            session = new ExpandoObject();
            session.security = new ExpandoObject();
            session.security.userRoles = new List<string>();
            session.security.userRoles.Add(RwUserRoles.Everyone);
            hasAuthToken = (FwValidate.IsPropertyDefined(request, "authToken"));
            if (hasAuthToken)
            {
                token              = AccountService.Current.GetAuthToken(request.authToken);
                authTokenData      = AccountService.Current.GetAuthTokenData(token.UserData);
                controlrec         = FwSqlData.GetControl(FwSqlConnection.RentalWorks);
                session.controlrec = controlrec;
                //invalidClientIP    = ((!FwValidate.IsPropertyDefined(authTokenData, "clientIP")) || (authTokenData.clientIP != HttpContext.Current.Request.UserHostAddress));
                tokenIsExpired     = (controlrec.Settings.AutoLogoutUser && (controlrec.Settings.AutoLogoutMinutes > 0 ) && (token.IssueDate.AddMinutes(controlrec.Settings.AutoLogoutMinutes) < DateTime.Now));
                if (/*invalidClientIP || */tokenIsExpired) 
                {
                    throw new AuthTokenInvalidException();
                }
                response.authToken         = AccountService.Current.GetAuthToken(token.Name, authTokenData);
                response.autoLogoutMinutes = (controlrec.Settings.AutoLogoutUser ? controlrec.Settings.AutoLogoutMinutes : 0);
                token = AccountService.Current.GetAuthToken(response.authToken);
                if (authTokenData != null)
                {
                    if(authTokenData.siteName != FwApplicationConfig.CurrentSite.Name)
                    {
                        throw new AuthTokenInvalidException();
                    }
                    session.security.webUser = authTokenData.webUser;
                    if (session.security.webUser != null)
                    {
                        switch((string)session.security.webUser.usertype)
                        {
                            case "USER":
                                session.security.userRoles.Add(RwUserRoles.User);
                                if (session.security.webUser.webadministrator == "T")
                                {
                                    session.security.userRoles.Add(RwUserRoles.UserAdministrator);
                                }
                                break;
                            case "CONTACT":
                                session.security.userRoles.Add(RwUserRoles.Contact);
                                if (session.security.webUser.webadministrator == "T")
                                {
                                    session.security.userRoles.Add(RwUserRoles.ContactAdministrator);
                                }
                                break;
                        }
                    }
                    
                }
            }
            return session;
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
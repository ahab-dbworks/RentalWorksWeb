using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using System.Dynamic;
using System.Threading.Tasks;

namespace FwStandard.Mobile
{
    public abstract class FwAccountService
    {
        protected FwApplicationConfig ApplicationConfig;
        protected FwUserSession UserSession;
        //---------------------------------------------------------------------------------------------
        private FwAccountService() { } // block the default constructor to force the one below
        public FwAccountService(FwApplicationConfig applicationConfig, FwUserSession userSession)
        {
            this.ApplicationConfig = applicationConfig;
            this.UserSession = userSession;
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetAuthTokenAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session)
        {
            int errNo;
            string errMsg, webUsersId=null, usertype, usersid, contactid, webusersid, groupsid, webadministrator, /*sitename, version, authtoken,*/ fullname, name, clientcode;
            dynamic tokenData, webUsersView;
            FwWebUserSettings settings;
            
            errNo      = 0;
            errMsg     = string.Empty;
            if (errNo.Equals(0) && (FwValidate.IsPropertyDefined(request, "authToken")))
            {
                //FormsAuthenticationTicket ticket;
                //dynamic authTokenData;
                //ticket        = GetAuthToken(request.authToken);
                //authTokenData = GetAuthTokenData(ticket.UserData);
                //webUsersId    = authTokenData.webUser.webusersid;
                webUsersId = this.UserSession.WebUsersId;
            }
            else
            {
                if (errNo.Equals(0) && (!FwValidate.IsPropertyDefined(request, "email")))
                {
                    errNo  = 1;
                    errMsg = "request.email is not defined.";
                }
                if (errNo.Equals(0) && (!FwValidate.IsPropertyDefined(request, "password")))
                {
                    errNo  = 2;
                    errMsg = "request.password is not defined.";
                }
                if (errNo.Equals(0))
                {
                    string email, password;
                    email      = request.email;
                    password   = request.password;
                    FwSqlData.WebAuthenticateResult webAuthenticateResult = await FwSqlData.WebAuthenticateAsync(conn, this.ApplicationConfig.DatabaseSettings, email, password);
                    webUsersId = webAuthenticateResult.WebUsersId;
                    errNo = webAuthenticateResult.ErrNo;
                    errMsg = webAuthenticateResult.ErrMsg;
                }
            }
            if (!string.IsNullOrEmpty(webUsersId) && (errNo.Equals(0)))
            {
                webUsersView                        = await FwSqlData.GetWebUsersViewAsync(conn, this.ApplicationConfig.DatabaseSettings, webUsersId);
                session.security                    = new ExpandoObject();
                session.security.webUser            = new ExpandoObject();
                tokenData                           = new ExpandoObject();
                //tokenData.webUser                   = new ExpandoObject();
                response.webUser                    = new ExpandoObject();

                usertype                            = (FwValidate.IsPropertyDefined(webUsersView, "usertype"))  ? webUsersView.usertype  : string.Empty;
                session.security.webUser.usertype   = usertype;
                //tokenData.webUser.usertype          = usertype;
                response.webUser.usertype           = session.security.webUser.usertype;
                    
                usersid                             = (FwValidate.IsPropertyDefined(webUsersView, "usersid"))   ? webUsersView.usersid   : string.Empty;
                session.security.webUser.usersid    = usersid;
                //tokenData.webUser.usersid           = usersid;
                response.webUser.usersid            = usersid;  //justin 05/25/2018   //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44

                contactid                           = (FwValidate.IsPropertyDefined(webUsersView, "contactid")) ? webUsersView.contactid : string.Empty;
                session.security.webUser.contactid  = contactid;
                //tokenData.webUser.contactid         = contactid;

                //sitename                            = FwApplicationConfig.CurrentSite.Name;
                //tokenData.siteName                  = sitename;
                    
                webusersid                          = (FwValidate.IsPropertyDefined(webUsersView, "webusersid")) ? webUsersView.webusersid : string.Empty;
                session.security.webUser.webusersid = webusersid;
                //tokenData.webUser.webusersid        = webusersid;
                    
                groupsid                            = (FwValidate.IsPropertyDefined(webUsersView, "webusersid")) ? webUsersView.groupsid : string.Empty;
                //tokenData.webUser.groupsid          = groupsid;
                response.applicationtree            = this.GetGroupsTree(groupsid, true);
                    
                webadministrator                    = (FwValidate.IsPropertyDefined(webUsersView, "webadministrator")) ? webUsersView.webadministrator : string.Empty;
                //tokenData.webUser.webadministrator  = webadministrator;

                //version                             = FwVersion.Current.FullVersion;
                //tokenData.version                   = version;
                    
                fullname                            = webUsersView.fullname;
                response.webUser.fullname           = fullname;

                name                                = (FwValidate.IsPropertyDefined(webUsersView, "name")) ? webUsersView.name : string.Empty;
                session.security.webUser.name       = name;
                //tokenData.webUser.name              = name;
                response.webUser.name               = name;  //justin 05/06/2018

                clientcode                          = await FwSqlData.GetClientCodeAsync(this.ApplicationConfig.DatabaseSettings);
                session.security.clientcode         = clientcode;
                //tokenData.clientcode                = clientcode;
                response.clientcode                 = clientcode;

                settings                            = await FwSqlData.GetWebUserSettingsAsync(conn, this.ApplicationConfig.DatabaseSettings, webusersid);
                response.webUser.browsedefaultrows  = settings.Settings.BrowseDefaultRows;
                response.webUser.applicationtheme   = settings.Settings.ApplicationTheme;

                session.applicationOptions          = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
                response.applicationOptions         = new ExpandoObject();

                await LoadApplicationAuthenticationInformationAsync(conn, request, response, session, webUsersView);

                // save the tokenData in the authtoken
                //authtoken = GetAuthToken(webUsersView.email, tokenData);
                //response.authToken = authtoken;

                // mv 2020-07-05 Disabling the cookie, it's giving warnings in Chrome and I don't think this is used anywhere.
                //HttpCookie cookieAuthToken;
                //cookieAuthToken = new HttpCookie("authtoken", authtoken);
                //cookieAuthToken.HttpOnly = true;
                //HttpContext.Current.Response.SetCookie(cookieAuthToken);
            }
            else if (errNo.Equals(0))
            {
                errNo = 3;
                errMsg = "Invalid user and/or password.";
            }
            response.errNo  = errNo;
            response.errMsg = errMsg;
        }

        public abstract dynamic GetGroupsTree(string groupsid, bool removeHiddenNodes);
        //---------------------------------------------------------------------------------------------
        //public string GetAuthToken(string email, dynamic userData)
        //{
        //    string jsonUserData, authToken;
        //    JsonWriter jsonWriter;
        //    FormsAuthenticationTicket ticket;
        //    FwControl controlrec;
        //    DateTime issueDate, expiration;
            
        //    userData.clientIP = HttpContext.Current.Request.UserHostAddress;
        //    issueDate  =  DateTime.Now;
        //    if (FwSqlConnection.AppDatabase != FwDatabases.MicrosoftCRM)
        //    {
        //        controlrec = FwSqlData.GetControl(FwSqlConnection.AppConnection);
        //        expiration = (controlrec.Settings.AutoLogoutUser) ? issueDate.AddMinutes(controlrec.Settings.AutoLogoutMinutes) : DateTime.MaxValue;
        //    }
        //    else
        //    {
        //        expiration = DateTime.MaxValue;
        //    }
        //    jsonWriter = new JsonWriter();
        //    jsonUserData = jsonWriter.Write(userData);
        //    ticket = new FormsAuthenticationTicket(
        //        version:      1
        //      , name:         email
        //      , issueDate:    issueDate
        //      , expiration:   expiration
        //      , isPersistent: false
        //      , userData:     jsonUserData
        //    );
        //    authToken = FormsAuthentication.Encrypt(ticket);
            
        //    return authToken;
        //}
        //---------------------------------------------------------------------------------------------
        //public FormsAuthenticationTicket GetAuthToken(string encryptedTicket)
        //{
        //    FormsAuthenticationTicket result;
            
        //    try
        //    {
        //        result = FormsAuthentication.Decrypt(encryptedTicket);
        //    }
        //    catch
        //    {
        //        throw new Fw.Json.Services.FwJsonService.AuthTokenInvalidException();
        //    }

        //    return result;
        //}
        //---------------------------------------------------------------------------------------------
        //public dynamic GetAuthTokenData(string userData)
        //{
        //    dynamic result = JsonConvert.DeserializeObject(userData);
        //    return result;
        //}
        //---------------------------------------------------------------------------------------------
        public virtual async Task LoadApplicationAuthenticationInformationAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session, dynamic webUserData)
        {
            //Used by projects to load system specific data
            await Task.CompletedTask;
        }
        //---------------------------------------------------------------------------------------------
        //private bool HasAuthToken()
        //{

        //}
        ////---------------------------------------------------------------------------------------------
        //private bool IsAuthTokenValid()
        //{
        //    bool isValid = true;
        //    long fileTimeStamp;
        //    DateTime timeStamp;
        //    string decryptedToken, webUsersId;
        //    string[] fragments;
            
        //    try
        //    {
        //        decryptedToken = FwCryptography.AjaxDecrypt(token);
        //        fragments = decryptedToken.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
        //        fileTimeStamp = FwConvert.ToInt32(fragments[0]);
        //        timeStamp = DateTime.FromFileTimeUtc(fileTimeStamp);
        //        webUsersId = fragments[1];
        //        if (timeStamp < DateTime.UtcNow.AddMinutes(-20))
        //        {
        //            isValid = false;
        //        }
        //    }
        //    catch
        //    {
        //        isValid = false;
        //    }
        //    return isValid;
        //}
        //---------------------------------------------------------------------------------------------
        public async Task AuthenticatePasswordAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session)
        {
            bool isPasswordDefined, isEmailDefined;
            int errNo;
            string webUsersId, password, errMsg, email;
            errNo = 0;
            errMsg = string.Empty;

            webUsersId          = string.Empty;
            isPasswordDefined   = FwValidate.IsPropertyDefined(request, "password");
            isEmailDefined      = FwValidate.IsPropertyDefined(request, "email");
            if (isEmailDefined && isPasswordDefined)
            {
                email      = request.email;
                password   = request.password;
                FwSqlData.WebAuthenticateResult webAuthenticateResult = await FwSqlData.WebAuthenticateAsync(conn, this.ApplicationConfig.DatabaseSettings, email, password);
                webUsersId = webAuthenticateResult.WebUsersId;
                errNo = webAuthenticateResult.ErrNo;
                errMsg = webAuthenticateResult.ErrMsg;
            }
            response.errNo  = errNo;
            response.errMsg = errMsg;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<bool> WebUserChangePasswordAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session)
        {
            bool result, isPasswordValid, isPasswordDefined, isWebUsersIdDefined;
            int errNo;
            string webUsersId, password, errMsg;
            errNo = 0;
            errMsg = string.Empty;
            result = false;
            isPasswordValid = false;
            isPasswordDefined   = FwValidate.IsPropertyDefined(request, "passsword");
            isWebUsersIdDefined = FwValidate.IsPropertyDefined(request, "webUsersId");
            if (isWebUsersIdDefined && isPasswordDefined)
            {
                webUsersId = request.webUsersId;
                password = request.password;
                FwSqlData.WebValidatePasswordResult validatePasswordResult = await FwSqlData.WebValidatePasswordAsync(conn, this.ApplicationConfig.DatabaseSettings, password);
                isPasswordValid = validatePasswordResult.IsValid;
                if (isPasswordValid)
                {
                    await FwSqlData.WebUsersSetPasswordAsync(conn, this.ApplicationConfig.DatabaseSettings, webUsersId, password, "T");
                }
                else
                {
                    errNo = validatePasswordResult.ErrorNo;
                    errMsg = validatePasswordResult.ErrorMessage;
                }
            }
            response.errNo = errNo;
            response.errMsg = errMsg;
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task WebUserResetPasswordAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "WebUserResetPassword";
            string email;
            FwSqlData.WebUserResetPasswordResult webUserResetPasswordResult;
            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            email    = request.email;
            webUserResetPasswordResult = await FwSqlData.WebUserResetPassword2Async(conn, this.ApplicationConfig.DatabaseSettings, email);
            response.errNo  = webUserResetPasswordResult.ErrNo;
            response.errMsg = webUserResetPasswordResult.ErrMsg;
        }
        //---------------------------------------------------------------------------------------------
    }
}
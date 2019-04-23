using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Web.Security;

namespace TrakitWorksWeb.Source
{
    public class Service
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
                        session.user = AppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                                       , usersId: session.security.webUser.usersid);
                    }
                    if (!string.IsNullOrEmpty(session.security.webUser.contactid))
                    {
                        session.contact = AppData.GetContact(conn:      FwSqlConnection.RentalWorks
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
        public static void GetSite(dynamic request, dynamic response, dynamic session)
        {
            FwApplicationConfig_Site site = FwApplicationConfig.CurrentSite;
            
            response.site      = new ExpandoObject();
            response.site.name = site.Name;
        }
        //---------------------------------------------------------------------------------------------
    }
}
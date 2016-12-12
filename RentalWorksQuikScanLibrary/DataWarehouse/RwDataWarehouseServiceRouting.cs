using Fw.Json.Services;
using Fw.Json.Utilities;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    public class RwDataWarehouseServiceRouting : Fw.Json.Services.FwJsonService
    {
        //---------------------------------------------------------------------------------------------
        protected string GetRegexString(string path, string applicationPath)
        {
            string regex;
            
            regex = @"^" + Regex.Escape(applicationPath + @"/rwdatawarehousewservices.ashx?path=" + path);

            return regex;
        }
        //---------------------------------------------------------------------------------------------
        protected override List<FwJsonRequestAction> GetRequestActions()
        {
            List<FwJsonRequestAction> actions;
            
            actions = new List<FwJsonRequestAction>() {
                  new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getcompanydepartments", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetCompanyDepartments(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getlocations", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetLocations(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getcategories", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetCategories(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getdeals", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetDeals(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getdealtypes", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetDealTypes(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getcustomers", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetCustomers(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getactivitytypes", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetActivityTypes(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/getinventorydepartments", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetInventoryDepartments(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/parameters/geticodes", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseParameterService.GetICodes(request, response, session); 
                  })
                , new FwJsonRequestAction(
                    roles: new string[]{RwUserRoles.RentalWorksUser}
                  , isMatch: delegate(string requestPath, string applicationPath) { 
                      return Regex.IsMatch(requestPath, GetRegexString("/reports/customerrevenuebymonth", applicationPath)); 
                  }
                  , onMatch: delegate(dynamic request, dynamic response, dynamic session) { 
                      RwDataWarehouseReportService.GetCustomerRevenueByMonth(request, response, session); 
                  })
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
                token         = FwAccountService.GetAuthToken(request.authToken);
                authTokenData = FwAccountService.GetAuthTokenData(token.UserData);

                if (authTokenData != null)
                {
                    session.security.webUser = authTokenData.webUser;
                    if (session.security.webUser != null)
                    {
                        if (!string.IsNullOrEmpty(session.security.webUser.usersid))
                        {
                            session.security.userRoles.Add(RwUserRoles.RentalWorksUser);
                            if (session.security.webUser.webadministrator == "T")
                            {
                                session.security.userRoles.Add(RwUserRoles.RentalWorksUserAdministrator);
                            }
                        }
                        if (!string.IsNullOrEmpty(session.security.webUser.contactid))
                        {
                            session.security.userRoles.Add(RwUserRoles.RentalWorksDealContact);
                            if (session.security.webUser.webadministrator == "T")
                            {
                                session.security.userRoles.Add(RwUserRoles.RentalWorksDealContactAdministrator);
                            }
                        }
                    }
                    
                }
            }

            return session;
        }
        //---------------------------------------------------------------------------------------------
    }
}

using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;

namespace RentalWorksWeb.Source
{
    public class AccountService : FwAccountService
    {
        public static AccountService Current;
        public override void LoadApplicationAuthenticationInformation(FwSqlConnection conn, dynamic request, dynamic response, dynamic session, dynamic tokenData, dynamic webUserData)
        {
            tokenData.webUser.locationid  = (FwValidate.IsPropertyDefined(webUserData, "locationid"))  ? webUserData.locationid  : string.Empty;
            tokenData.webUser.warehouseid = (FwValidate.IsPropertyDefined(webUserData, "warehouseid")) ? webUserData.warehouseid : string.Empty;

            response.webUser.location  = RwAppData.GetLocationInfo(conn, webUserData.locationid);

            if (FwValidate.IsPropertyDefined(session.applicationOptions, "quickbooks")) response.applicationOptions.quickbooks = session.applicationOptions.quickbooks;
        }
    }
}

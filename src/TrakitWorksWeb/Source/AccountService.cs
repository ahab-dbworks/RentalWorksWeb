using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using FwStandard.Security;

namespace TrakitWorksWeb.Source
{
    public class AccountService : FwAccountService
    {
        public static AccountService Current;
        public override void LoadApplicationAuthenticationInformation(FwSqlConnection conn, dynamic request, dynamic response, dynamic session, dynamic tokenData, dynamic webUserData)
        {
            tokenData.webUser.locationid  = (FwValidate.IsPropertyDefined(webUserData, "locationid"))  ? webUserData.locationid  : string.Empty;
            tokenData.webUser.warehouseid = (FwValidate.IsPropertyDefined(webUserData, "warehouseid")) ? webUserData.warehouseid : string.Empty;
            tokenData.webUser.departmentid = (FwValidate.IsPropertyDefined(webUserData, "departmentid")) ? webUserData.departmentid : string.Empty;
            tokenData.webUser.webusersid = (FwValidate.IsPropertyDefined(webUserData, "webusersid")) ? webUserData.webusersid : string.Empty;

            response.webUser.location   = AppData.GetLocationInfo(conn, webUserData.locationid);
            response.webUser.warehouse  = AppData.GetWarehouseInfo(conn, webUserData.warehouseid);
            response.webUser.department = AppData.GetDepartmentInfo(conn, webUserData.departmentid);
            response.webUser.webusersid = AppData.GetUserInfo(conn, webUserData.webusersid);

            if (FwValidate.IsPropertyDefined(session.applicationOptions, "quickbooks")) response.applicationOptions.quickbooks = session.applicationOptions.quickbooks;
        }
        //----------------------------------------------------------------------------------------------------
        public override dynamic GetGroupsTree(string groupsid, bool removeHiddenNodes)
        {
            return FwSecurityTree.Tree.GetGroupsTreeAsync(groupsid, true);
        }
        //----------------------------------------------------------------------------------------------------
    }
}

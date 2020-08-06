using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;

namespace RentalWorksQuikScan.Modules
{
    public class TransferInMenu
    {
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SessionSearch(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = request.pageno;
                select.PageSize = request.pagesize;

                select.Add("select *");
                select.Add("from suspendview with (nolock)");
                select.Add("where contracttype = 'RECEIPT'");
                select.Add("  and ordertype    = 'T'");
                select.Add("  and warehouseid  = @warehouseid");
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    select.Add("  and sessionno = @sessionno");
                    select.AddParameter("@sessionno", request.searchvalue);
                }
                if (!string.IsNullOrEmpty(request.orderid))
                {
                    select.Add("  and orderid = @orderid");
                    select.AddParameter("@orderid", request.orderid);
                }
                select.Parse();
                select.AddOrderBy("contractdate desc, contracttime desc");
                select.AddParameter("@warehouseid", userLocation.warehouseId);
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "OrderSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            select.PageNo   = request.pageno;
            select.PageSize = request.pagesize;
            qry.AddColumn("orderdate",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("pickdate",    false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("shipdate",    false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("receivedate", false, FwJsonDataTableColumn.DataTypes.Date);
            select.Add("select *");
            select.Add("from  transferview with (nolock)");
            select.Add("where status       ='ACTIVE'");
            select.Add("  and warehouseid  = @warehouseid");
            if (!string.IsNullOrEmpty(request.searchvalue))
            {
                switch ((string)request.searchmode)
                {
                    case "ORDERNO":
                        select.Add("and orderno like @orderno");
                        select.AddParameter("@orderno", request.searchvalue + "%");
                        break;
                    case "DESCRIPTION":
                        select.Add("and orderdesc like @orderdesc");
                        select.AddParameter("@orderdesc",  "%" + request.searchvalue + "%");
                        break;
                    case "DEAL":
                        select.Add("and deal like @deal");
                        select.AddParameter("@deal",  "%" + request.searchvalue + "%");
                        break;
                }
            }
            select.Add("order by orderdate desc, orderno desc");
            select.AddParameter("@warehouseid", userLocation.warehouseId);

            response.searchresults = qry.QueryToFwJsonTable(select, true);
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public static void CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createincontract"))
            {
                sp.AddParameter("@orderid",      request.orderid);
                sp.AddParameter("@dealid",       request.dealid);
                sp.AddParameter("@departmentid", request.departmentid);
                sp.AddParameter("@usersid",      session.security.webUser.usersid);
                sp.AddParameter("@contractid",   SqlDbType.NVarChar, ParameterDirection.Output);
                sp.Execute();
                response.contractid = sp.GetParameter("@contractid").ToString().Trim();
            }
            response.sessionno = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "contract", "contractid", response.contractid, "sessionno");
            if (!string.IsNullOrEmpty(response.contractid))
            {
                using (FwSqlCommand cmd = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    cmd.Add("update contract");
                    cmd.Add("set forcedsuspend = 'G'");
                    cmd.Add("where contractid = @contractid");
                    cmd.AddParameter("@contractid", response.contractid);
                    cmd.Execute();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
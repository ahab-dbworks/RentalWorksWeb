using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;

namespace RentalWorksQuikScan.Modules
{
    public class CheckInMenu
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void DealSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "DealSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            select.PageNo   = request.pageno;
            select.PageSize = request.pagesize;
            select.Add("select *");
            select.Add("from   dbo.funcdeal(@locationid)");
            select.Add("where  dealinactive <> 'T'");
            if (!request.showalllocation)
            {
                select.Add("  and locationid = @locationid"); //MY + EG 2017-08-25: QS needs to support location filter on the user level.
            }
            if (!string.IsNullOrEmpty(request.searchvalue))
            {
                switch ((string)request.searchmode)
                {
                    case "DEALNO":
                        select.Add("and dealno like @dealno");
                        select.AddParameter("@dealno", request.searchvalue + "%");
                        break;
                    case "NAME":
                        select.Add("and deal like @deal");
                        select.AddParameter("@deal",  "%" + request.searchvalue + "%");
                        break;
                }
            }
            select.Add("order by deal");
            select.AddParameter("@locationid", userLocation.locationId);

            response.searchresults = qry.QueryToFwJsonTable(select, true);
        }
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
                select.Add("where contracttype = 'IN'");
                select.Add("  and ordertype    = 'O'");
                if (!request.showalllocation)
                {
                    select.Add("  and locationid   = @locationid");
                }
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
                select.AddOrderBy("statusdate desc");
                select.AddParameter("@locationid", userLocation.locationId);
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
            qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("statusdate",  false, FwJsonDataTableColumn.DataTypes.Date);
            select.Add("select o.orderid, o.orderdesc, o.status, o.orderno, o.orderdate, o.deal, o.estrentfrom, o.estrentto, o.statusdate, o.dealid, o.departmentid");
            select.Add("from  dbo.funcorder('O','F') o");
            select.Add("where o.ordertype = 'O'");
            if (!request.showalllocation)
            {
                select.Add("  and o.locationid = @locationid"); //MY + EG 2017-08-25: QS needs to support location filter on the user level.
            }
            select.Add("  and o.rentalsale <> 'T'");
            select.Add("  and o.status not in ('CANCELLED', 'SNAPSHOT')");
            select.Add("  and o.hasrentalout = 'T'");
            select.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
            if (!string.IsNullOrEmpty(request.searchvalue))
            {
                switch ((string)request.searchmode)
                {
                    case "ORDERNO":
                        select.Add("and orderno like @orderno");
                        select.AddParameter("@orderno", request.searchvalue + "%");
                        break;
                    case "DESCRIPTION":
                        //select.Add("and orderdesc like @orderdesc");
                        //select.AddParameter("@orderdesc",  "%" + request.searchvalue + "%");
                        {
                            string[] searchValues = request.searchvalue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < searchValues.Length; i++)
                            {
                                select.AddWhere($"orderdesc like @searchvalue{i}");
                                select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                            }
                        }
                        break;
                    case "DEAL":
                        select.Add("and deal like @deal");
                        select.AddParameter("@deal",  "%" + request.searchvalue + "%");
                        break;
                }
            }
            select.Add("order by orderdate desc, orderno desc");
            select.AddParameter("@locationid", userLocation.locationId);

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
        [FwJsonServiceMethod]
        public static void EnableShowAllLocations(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "ToggleShowAllLocations";
            bool result;

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select warehousecount = count(*)");
                qry.Add("  from warehouse with (nolock)");
                qry.Add(" where inactive <> 'T'");
                qry.Execute();
                result = qry.GetField("warehousecount").ToInt32() > 1;
            }
            response.enable = result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
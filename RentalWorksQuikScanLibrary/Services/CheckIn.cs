using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RentalWorksQuikScanLibrary.Services
{
    class CheckIn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetShowCreateContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select showcreatecontract = (case when exists (select *");
                qry.Add("                                               from ordertran with (nolock)");
                qry.Add("                                               where inreturncontractid = @contractid)");
                qry.Add("                                  then 'T'");
                qry.Add("                                  else 'F'");
                qry.Add("                             end)");
                qry.AddParameter("@contractid", request.contractid);
                qry.Execute();
                response.showcreatecontract = qry.GetField("showcreatecontract").ToBoolean();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetSerialInfo(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            response.serial = CheckIn.FuncSerialFrm(conn:         FwSqlConnection.RentalWorks,
                                                    orderid:      request.orderid,
                                                    warehouseid:  userLocation.warehouseId,
                                                    contractid:   request.contractid,
                                                    masterid:     request.masterid);

            response.serialitems = CheckIn.FuncSerialMeterIn2(conn:        FwSqlConnection.RentalWorks,
                                                              contractid:  request.contractid,
                                                              orderid:     request.orderid,
                                                              masterid:    request.masterid,
                                                              warehouseid: userLocation.warehouseId);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetSerialItems(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            response.serialitems = CheckIn.FuncSerialMeterIn2(conn:        FwSqlConnection.RentalWorks,
                                                              contractid:  request.contractid,
                                                              orderid:     request.orderid,
                                                              masterid:    request.masterid,
                                                              warehouseid: userLocation.warehouseId);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void SerialSessionIn(dynamic request, dynamic response, dynamic session)
        {
            dynamic userLocation;

            userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks,
                                                     usersId: session.security.webUser.usersid);

            CheckIn.InsertSerialSession(conn:         FwSqlConnection.RentalWorks,
                                        contractid:   request.contractid,
                                        orderid:      request.orderid,
                                        masteritemid: request.masteritemid,
                                        rentalitemid: request.rentalitemid,
                                        activitytype: "I",
                                        usersid:      session.security.webUser.usersid,
                                        meter:        request.meter,
                                        toggledelete: request.toggledelete);

            response.serial = CheckIn.FuncSerialFrm(conn:         FwSqlConnection.RentalWorks,
                                                    orderid:      request.orderid,
                                                    warehouseid:  userLocation.warehouseId,
                                                    contractid:   request.contractid,
                                                    masterid:     request.masterid);
        }
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
                        select.AddParameter("@deal", request.searchvalue + "%");
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

                select.Add("select v.contractid, v.status, v.statusdate, v.sessionno, v.dealno, v.deal, v.orderno, v.orderdesc, v.usersid, v.username, oc.orderid, v.dealid, v.departmentid");
                select.Add("from suspendview v with (nolock) join ordercontract oc with (nolock) on (v.contractid = oc.contractid)");
                select.Add("where v.contracttype = 'IN'");
                select.Add("  and v.ordertype    = 'O'");
                select.Add("  and v.locationid   = @locationid");
                select.AddParameter("@locationid", userLocation.locationId);
                if (!string.IsNullOrEmpty(request.orderid))
                {
                    select.Add("  and oc.orderid = @orderid");
                    select.AddParameter("@orderid", request.orderid);
                }
                select.Parse();
                select.AddOrderBy("statusdate desc");
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
            select.Add("  and o.locationid = @locationid");
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
                        select.Add("and orderdesc like @orderdesc");
                        select.AddParameter("@orderdesc", request.searchvalue + "%");
                        break;
                    case "DEAL":
                        select.Add("and deal like @deal");
                        select.AddParameter("@deal", request.searchvalue + "%");
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
        //----------------------------------------------------------------------------------------------------
        public static dynamic FuncSerialFrm(FwSqlConnection conn, string orderid, string warehouseid, string contractid, string masterid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialfrm(@orderid, @warehouseid, @contractid)");
            qry.Add(" where masterid = @masterid");
            qry.AddParameter("@orderid",      orderid);
            qry.AddParameter("@warehouseid",  warehouseid);
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = qry.QueryToDynamicObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic FuncSerialMeterIn2(FwSqlConnection conn, string contractid, string orderid, string masterid, string warehouseid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialmeterin2(@contractid, @orderid, @masterid, @warehouseid, '')");
            qry.AddParameter("@orderid",      orderid);
            qry.AddParameter("@warehouseid",  warehouseid);
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = qry.QueryToDynamicList();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static void InsertSerialSession(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string rentalitemid, string activitytype, string usersid, string meter, string toggledelete)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.Add("exec dbo.insertserialsession @contractid, @orderid, @masteritemid, @rentalitemid, @activitytype, @internalchar, @usersid, @meter, @toggledelete, @containeritemid, @containeroutcontractid");
            qry.AddParameter("@contractid",             contractid);
            qry.AddParameter("@orderid",                orderid);
            qry.AddParameter("@masteritemid",           masteritemid);
            qry.AddParameter("@rentalitemid",           rentalitemid);
            qry.AddParameter("@activitytype",           activitytype);
            qry.AddParameter("@internalchar",           "");
            qry.AddParameter("@usersid",                usersid);
            qry.AddParameter("@meter",                  meter);
            qry.AddParameter("@toggledelete",           toggledelete);
            qry.AddParameter("@containeritemid",        "");
            qry.AddParameter("@containeroutcontractid", "");
            qry.Execute();
        }
        //---------------------------------------------------------------------------------------------
    }
}

﻿using Fw.Json.Services;
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
    class Staging
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetSearchResults(dynamic request, dynamic response, dynamic session)
        {
            string searchmode = request.searchmode;
            var userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            string locationid = userLocation.locationId;
            string moduletype = request.moduletype;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;

                switch(moduletype)
                {
                    case RwConstants.ModuleTypes.Order:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                //qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, deal, estrentfrom, estrentto, statusdate");
                                select.Add("from  dbo.funcorder('O' ,'F') o");
                                select.Add("where o.ordertype = 'O'");
                                select.Add("  and o.locationid = @locationid");
                                select.Add("  and o.status not in ('NEW','CANCELLED','CLOSED','ORDERED','CANCELLED','CLOSED')");
                                select.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                //qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, deal, estrentfrom, estrentto, statusdate");
                                select.Add("from  dbo.funcorder('O' ,'F') o");
                                select.Add("where o.ordertype = 'O'");
                                select.Add("  and o.locationid = @locationid");
                                select.Add("  and o.status not in ('NEW','CANCELLED','CLOSED','ORDERED','CANCELLED','CLOSED')");
                                select.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                break;
                            case "deal":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentfrom", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("estrentto", false, FwJsonDataTableColumn.DataTypes.Date);
                                //qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, deal, estrentfrom, estrentto, statusdate");
                                select.Add("from  dbo.funcorder('O' ,'F') o");
                                select.Add("where o.ordertype = 'O'");
                                select.Add("  and o.locationid = @locationid");
                                select.Add("  and o.status not in ('NEW','CANCELLED','CLOSED','ORDERED','CANCELLED','CLOSED')");
                                select.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("deal like @searchvalue");
                                select.AddOrderBy("orderdate desc, orderno desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                select.Add("from suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'OUT'");
                                select.Add("  and v.ordertype    = 'O'");
                                select.Add("  and v.locationid   = @locationid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                select.AddOrderBy("statusdate desc, sessionno desc");
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                        }
                        break;
                    case RwConstants.ModuleTypes.Truck:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                select.Add("select orderid, orderdesc, status, orderno");
                                select.Add("from dbo.functruck() o");
                                select.Add("where o.locationid = @locationid");
                                select.Add("  and o.status not in ('INACTIVE')");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                select.Add("select orderid, orderdesc, status, orderno");
                                select.Add("from dbo.functruck() o");
                                select.Add("where o.locationid = @locationid");
                                select.Add("  and o.status not in ('INACTIVE')");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderno");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("contractdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select contractdate,contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, dealno, deal, usersid, username, contractid");
                                select.Add("from suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'OUT'");
                                select.Add("  and v.locationid   = @locationid");
                                select.Add("  and v.dealid       = ''");
                                select.Add("  and v.ordertype    = 'P'");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                //select.AddOrderBy("statusdate desc, sessionno desc");
                                select.AddOrderBy("contractdate desc, contracttime desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                        }
                        break;
                    case RwConstants.ModuleTypes.Transfer:
                        switch (searchmode)
                        {
                            case "orderno":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("shipdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("receivedate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                select.Add("from transferview tv with (nolock)");
                                select.Add("where status in ('ACTIVE','CONFIRMED')");
                                select.Add("  and fromwarehouseid = @warehouseid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddOrderBy("orderno desc");
                                select.AddWhere("orderno like @searchvalue");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@warehouseid", userLocation.warehouseId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "orderdesc":
                                select.PageNo = request.pageno;
                                select.PageSize = request.pagesize;
                                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("pickdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("shipdate", false, FwJsonDataTableColumn.DataTypes.Date);
                                qry.AddColumn("receivedate", false, FwJsonDataTableColumn.DataTypes.Date);
                                select.Add("select orderid, orderdesc, status, orderno, orderdate, fromwarehouse, towarehouse, department, origorderno, pickdate, shipdate, receivedate");
                                select.Add("from transferview tv with (nolock)");
                                select.Add("where status in ('ACTIVE','CONFIRMED')");
                                select.Add("  and fromwarehouseid = @warehouseid");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("orderdesc like @searchvalue");
                                select.AddOrderBy("orderno desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@warehouseid", userLocation.warehouseId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                                break;
                            case "sessionno":
                                select.Add("select contractdate, contracttime, orderid, orderdesc, orderno, status, statusdate, sessionno, usersid, username, contractid, warehouse");
                                select.Add("from  suspendview v with (nolock)");
                                select.Add("where v.contracttype = 'MANIFEST'");
                                select.Add("  and   v.locationid   = @locationid");
                                select.Add("  and   v.dealid       = ''");
                                select.Add("  and  v.ordertype = 'T'");
                                DepartmentFilter.SetDepartmentFilter(session.security.webUser.usersid, select);
                                select.Parse();
                                select.AddWhere("sessionno like @searchvalue");
                                select.AddOrderBy("contractdate desc, contracttime desc");
                                select.AddParameter("@locationid", userLocation.locationId);
                                select.AddParameter("@searchvalue", request.searchvalue + "%");

                                break;
                        }
                        break;
                }

                
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }

        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static bool IsSuspendedSessionsEnabled()
        {
            bool isenabled = false;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select isenabled = dbo.quikscanstagebysession()");
                qry.Execute();
                isenabled = qry.GetField("isenabled").ToBoolean();
            }
            return isenabled;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid")]
        public static void GetOrderSuspendedSessions(dynamic request, dynamic response, dynamic session)
        {
            string usersid = session.security.webUser.usersid;
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, usersid);
            string locationid = userLocation.locationId;
            string orderid = request.orderid;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;

                select.Add("select v.contractid, v.status, v.statusdate, v.sessionno, v.dealno, v.deal, v.orderno, v.orderdesc, v.usersid, v.username, oc.orderid");
                select.Add("from suspendview v with (nolock) join ordercontract oc with (nolock) on (v.contractid = oc.contractid)");
                select.Add("where v.contracttype = 'OUT'");
                select.Add("  and v.ordertype    = 'O'");
                select.Add("  and v.locationid   = @locationid");
                select.Add("  and oc.orderid     = @orderid");
                select.AddParameter("@locationid", locationid);
                select.AddParameter("@orderid", orderid);
                select.Parse();
                select.AddOrderBy("statusdate desc");
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public static void CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createoutcontract"))
            {
                sp.AddParameter("@orderid", request.orderid);
                sp.AddParameter("@usersid", session.security.webUser.usersid);
                sp.AddParameter("@notes", string.Empty);
                sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
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
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void GetPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetStagingPendingItems";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            session.userLocation = RwAppData.GetUserLocation(conn: FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "Your user account requires a warehouse to peform this action.", session.userLocation.warehouseId);
            response.getStagingPendingItems = RwAppData.GetStagingPendingItems(conn: FwSqlConnection.RentalWorks
                                                                             , orderId: request.orderid
                                                                             , warehouseId: session.userLocation.warehouseId
                                                                             , contractId: request.contractid);
        }
        //----------------------------------------------------------------------------------------------------
        public static FwJsonDataTable funcstaged(FwSqlConnection conn, string orderid, string warehouseid, bool summary)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.AddColumn("rectype", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("barcode", false);
            qry.AddColumn("quantity", false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("vendorid", false);
            qry.AddColumn("vendor", false);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("trackedby", false);
            qry.Add("select");
            qry.Add("  rectype,");
            qry.Add("  masteritemid,");
            qry.Add("  description,");
            qry.Add("  masterno,");
            qry.Add("  barcode,");
            qry.Add("  quantity,");
            qry.Add("  vendorid,");
            qry.Add("  vendor,");
            qry.Add("  itemclass,");
            qry.Add("  trackedby");
            qry.Add("from dbo.funcstaged(@orderid, @summary)");
            qry.Add("where warehouseid = @warehouseid");
            qry.Add("order by orderby");
            qry.AddParameter("@orderid", orderid);
            qry.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
            qry.AddParameter("@warehouseid", warehouseid);
            result = new ExpandoObject();
            result = qry.QueryToFwJsonTable();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static FwJsonDataTable funccheckedout(FwSqlConnection conn, string contractid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn);
            qry.AddColumn("rectype", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("barcode", false);
            qry.AddColumn("quantity", false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("vendorid", false);
            qry.AddColumn("vendor", false);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("trackedby", false);
            qry.Add("select");
            qry.Add("  rectype,");
            qry.Add("  masteritemid,");
            qry.Add("  description,");
            qry.Add("  masterno,");
            qry.Add("  barcode,");
            qry.Add("  quantity,");
            qry.Add("  vendorid,");
            qry.Add("  vendor,");
            qry.Add("  itemclass,");
            qry.Add("  trackedby");
            qry.Add("from dbo.funccheckedout(@contractid)");
            qry.Add("order by orderby");
            qry.AddParameter("@contractid", contractid);
            result = new ExpandoObject();
            result = qry.QueryToFwJsonTable();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void GetStagedItems(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetStagedItems";
            //FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            dynamic userLocation = RwAppData.GetUserLocation(conn: FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            string warehouseid = userLocation.warehouseId;
            //FwValidate.TestIsNullOrEmpty(METHOD_NAME, "Your user account requires a warehouse to peform this action.", session.userLocation.warehouseId);
            //FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");

            if (string.IsNullOrEmpty(request.contractid))
            {
                response.getStagingStagedItems = Staging.funcstaged(FwSqlConnection.RentalWorks, request.orderid, warehouseid, false);
            }
            else
            {
                response.getStagingStagedItems = Staging.funccheckedout(FwSqlConnection.RentalWorks, request.contractid);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "orderid,contractid")]
        public static void OrdertranExists(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                if (!(string.IsNullOrEmpty(request.contractid)))
                {
                    qry.Add("select ordertranexists = case when exists (select *");
                    qry.Add("                                             from ordertran with (nolock)");
                    qry.Add("                                             where outreceivecontractid = @contractid)");
                    qry.Add("                              then 'T'");
                    qry.Add("                              else 'F'");
                    qry.Add("                         end");
                    qry.AddParameter("@contractid", request.contractid);
                    qry.Execute();
                    response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                }
                else
                {
                    qry.Add("select ordertranexists = case when exists (select *");
                    qry.Add("                                             from ordertran ot with (nolock) join ordercontract oc on (ot.contractid = oc.contractid");
                    qry.Add("                                             where oc.orderid = @orderid)");
                    qry.Add("                              then 'T'");
                    qry.Add("                              else 'F'");
                    qry.Add("                         end");
                    qry.AddParameter("@orderid", request.orderid);
                    qry.Execute();
                    response.ordertranExists = qry.GetField("ordertranexists").ToBoolean();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public static void CancelContract(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.CancelContract(FwSqlConnection.RentalWorks, request.contractid, session.security.webUser.usersid, true);
        }
        //---------------------------------------------------------------------------------------------
    }
}

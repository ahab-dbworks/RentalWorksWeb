﻿using System;
using System.Dynamic;
using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;

namespace RentalWorksQuikScanLibrary.Services
{
    public class SelectPO
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
       public static void GetPurchaseOrders(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectPO.GetPurchaseOrders";
            FwSqlConnection conn;
            string usersid, moduletype;
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pageno");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pagesize");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduletype");
            conn                    = FwSqlConnection.RentalWorks;
            usersid                 = session.security.webUser.usersid;
            var userLocation  = RwAppData.GetUserLocation(conn, usersid);
            moduletype = request.moduletype;
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("PO No",  "orderno",    FwJsonDataTableColumn.DataTypes.Text, false);
                qry.AddColumn("Desc",   "orderdesc",  FwJsonDataTableColumn.DataTypes.Text, false);
                qry.AddColumn("Date",   "orderdate",  FwJsonDataTableColumn.DataTypes.Date, false);
                qry.AddColumn("Deal",   "deal",       FwJsonDataTableColumn.DataTypes.Text, false);
                qry.AddColumn("Status", "status",     FwJsonDataTableColumn.DataTypes.Text, false);
                qry.AddColumn("As Of",  "statusdate", FwJsonDataTableColumn.DataTypes.Date, false);
                switch (moduletype)
                {
                    case "SubReceive":
                        qry.Add("select top 1000 orderno, orderdesc, orderdate, deal, status, statusdate");
                        qry.Add("from poview o with (nolock)");
                        qry.Add("where o.status in ('NEW','OPEN')");
                        qry.Add("  and o.orderno > ''");
                        qry.Add("  and (");
                        qry.Add("       o.subrent     = 'T'");
                        qry.Add("    or o.subsale     = 'T'");
                        qry.Add("    or o.parts       = 'T'");
                        qry.Add("    or o.sales       = 'T'");
                        qry.Add("    or o.rental      = 'T'");
                        qry.Add("    or o.consignment = 'T'");
                        qry.Add("  )");
                        qry.Add("  and exists (select *");
                        qry.Add("              from  masteritem mi with (nolock)");
                        qry.Add("              where mi.orderid     = o.orderid");
                        qry.Add("              and   mi.warehouseid = @warehouseid");
                        qry.Add("             )");
                        qry.Add("order by orderdate desc, orderno desc");
                        qry.AddParameter("@locationid", userLocation.locationId);
                        qry.AddParameter("@warehouseid", userLocation.warehouseId);
                        break;
                    case "SubReturn":
                        qry.Add("select top 1000 orderno, orderdesc, orderdate, deal, status, statusdate");
                        qry.Add("from poview o with (nolock)");
                        qry.Add("where o.status in ('OPEN','RECEIVED','COMPLETE')");
                        //qry.Add("  and o.locationid = @locationid");
                        qry.Add("  and o.orderno > ''");
                        qry.Add("  and (");
                        qry.Add("         o.subrent     = 'T'");
                        qry.Add("      or o.subsale     = 'T'");
                        qry.Add("      or o.parts       = 'T'");
                        qry.Add("      or o.sales       = 'T'");
                        qry.Add("      or o.rental      = 'T'");
                        qry.Add("      or o.consignment = 'T'");
                        qry.Add("  )");
                        qry.Add("order by orderdate desc, orderno desc");
                        qry.AddParameter("@locationid", userLocation.locationId);
                        break;
                }
                response.datatable = qry.QueryToFwJsonTable(request.pageno, request.pagesize);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void WebSelectPO(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "WebSelectPO";
            string usersid, pono, poid;
            RwAppData.ModuleType moduleType;
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "poNo");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            moduleType = (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType);
            usersid    = session.security.webUser.usersid;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            var userLocation  = RwAppData.GetUserLocation(conn, usersid);
            string locationid = userLocation.locationId;

            pono       = request.poNo;
            response.webSelectPO = RwAppData.WebSelectPO(conn:       conn
                                                       , usersId:    usersid
                                                       , poNo:       pono
                                                       , moduleType: moduleType);
            if (response.webSelectPO.status == 0)
            {
                FwJsonDataTable dt = RwAppData.GetDealOrderByOrderNo(conn:    conn
                                                                   , orderNo: pono);
                poid = dt.Rows[0][dt.ColumnIndex["orderid"]].ToString().TrimEnd();
                if (dt.Rows.Count > 0)
                {
                    if (FwValidate.IsPropertyDefined(request, "sessionNo"))
                    {
                        response.contract = new ExpandoObject();
                        response.contract.contractId = request.contractId;
                        response.contract.sessionNo = request.sessionNo;
                    }
                    else
                    {
                        FwJsonDataTable dtSuspendedContracts = null;
                        switch (moduleType)
                        {
                            case RentalWorksQuikScanLibrary.RwAppData.ModuleType.SubReceive:
                                bool createreceivecontract = request.forcecreatecontract;
                                if (!createreceivecontract)
                                {
                                    using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                                    {
                                        qry.Add("select *");
                                        qry.Add("from suspendview v with (nolock)");
                                        qry.Add("where v.contracttype = 'RECEIVE'");
                                        qry.Add("  and v.locationid = @locationid");
                                        qry.Add("  and v.orderno = @orderno");
                                        qry.Add("order by statusdate desc");
                                        qry.AddParameter("@locationid", locationid);
                                        qry.AddParameter("@orderno", pono);
                                        dtSuspendedContracts = qry.QueryToFwJsonTable(true);
                                    }
                                    if (dtSuspendedContracts.Rows.Count > 0)
                                    {
                                        response.suspendedContracts = dtSuspendedContracts;
                                    }
                                    else
                                    {
                                        createreceivecontract = true;
                                    }
                                }
                                if (createreceivecontract)
                                {
                                    response.contract = RwAppData.CreateReceiveContract(conn:    FwSqlConnection.RentalWorks,
                                                                                        poid:    poid,
                                                                                        usersid: usersid);
                                }
                                break;
                            case RentalWorksQuikScanLibrary.RwAppData.ModuleType.SubReturn:
                                bool createreturncontract = request.forcecreatecontract;
                                if (!createreturncontract)
                                {
                                    using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                                    {
                                        qry.Add("select *");
                                        qry.Add("from suspendview v with (nolock)");
                                        qry.Add("where v.contracttype = 'RETURN'");
                                        qry.Add("  and v.locationid = @locationid");
                                        qry.Add("  and v.orderno = @orderno");
                                        qry.Add("order by statusdate desc");
                                        qry.AddParameter("@locationid", locationid);
                                        qry.AddParameter("@orderno", pono);
                                        dtSuspendedContracts = qry.QueryToFwJsonTable(true);
                                    }
                                    if (dtSuspendedContracts.Rows.Count > 0)
                                    {
                                        response.suspendedContracts = dtSuspendedContracts;
                                    }
                                    else
                                    {
                                        createreturncontract = true;
                                    }
                                }
                                if (createreturncontract)
                                {
                                    response.contract = RwAppData.CreateReturnContract(conn:    FwSqlConnection.RentalWorks, 
                                                                                       poid:    poid, 
                                                                                       usersid: usersid);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    throw new Exception(METHOD_NAME + ": Can't find PO.");
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}

﻿using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    class CheckIn
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
                select.Add("  and locationid   = @locationid");
                select.AddParameter("@locationid", userLocation.locationId);
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
                        select.AddParameter("@orderdesc",  "%" + request.searchvalue + "%");
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
        public static void CheckInItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItem";
            RwAppData.ModuleType moduleType;
            RwAppData.CheckInMode checkInMode;
            dynamic webGetItemStatus;
            FwJsonDataTable dtSuspendedInContracts;
            string usersid, orderid, code, dealid, departmentid, masteritemid, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, contractid;
            FwSqlConnection conn;
            decimal qty;
            bool checkinitem = true, disablemultiorder = false;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "checkInMode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignorId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "description");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "internalChar");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterItemId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "parentId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestIsNumeric(METHOD_NAME, "qty", request.qty);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "newOrderAction");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "aisle");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "shelf");
            
            usersid         = session.security.webUser.usersid;
            conn            = FwSqlConnection.RentalWorks;
            code            = request.code;
            masteritemid    = request.masterItemId;
            qty             = request.qty;
            neworderaction  = request.newOrderAction;
            containeritemid        = FwValidate.IsPropertyDefined(request, "containeritemid")        ? request.containeritemid        : string.Empty;
            containeroutcontractid = FwValidate.IsPropertyDefined(request, "containeroutcontractid") ? request.containeroutcontractid : string.Empty;
            aisle           = request.aisle;
            shelf           = request.shelf;
            parentid        = request.parentId;
            vendorid        = request.vendorId;
            contractid      = request.contractId;
            orderid         = request.orderId;
            dealid          = request.dealId;
            departmentid    = request.departmentId;
            moduleType      = (RwAppData.ModuleType) Enum.Parse(typeof(RwAppData.ModuleType),  request.moduleType);
            checkInMode     = (RwAppData.CheckInMode)Enum.Parse(typeof(RwAppData.CheckInMode), request.checkInMode);
            if (string.IsNullOrEmpty(contractid))
            {
                webGetItemStatus = RwAppData.WebGetItemStatus(conn, usersid, code);
                if ((!string.IsNullOrEmpty(webGetItemStatus.orderNo)) && (webGetItemStatus.rentalStatus == "OUT"))
                {
                    dtSuspendedInContracts = RwAppData.GetSuspendedInContracts(conn, moduleType, webGetItemStatus.orderid, usersid);
                    if (dtSuspendedInContracts.Rows.Count > 0)
                    {
                        response.suspendedInContracts = dtSuspendedInContracts;
                        response.webGetItemStatus     = webGetItemStatus;
                        checkinitem = false;
                    }
                }
            }
            if (checkinitem)
            {
                response.webCheckInItem = RwAppData.WebCheckInItem(conn, usersid, moduleType, checkInMode, code, masteritemid, qty, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, disablemultiorder, contractid, orderid, dealid, departmentid);
            }
            if (!string.IsNullOrEmpty(containeritemid) && (!string.IsNullOrEmpty(response.webCheckInItem.masterItemId)))
            {
                if (response.webCheckInItem.isICode) {
                    response.fillcontainer = RwAppData.FillContainer_GetContainerCheckInQuantity2(conn, contractid, containeritemid, dealid, departmentid, orderid, "O", "SINGLE_ORDER", "F", "F", response.webCheckInItem.warehouseId, response.webCheckInItem.masterItemId);
                } else {
                    response.fillcontainer = RwAppData.FillContainer_GetContainerCheckInBCData(conn, contractid, containeritemid, dealid, departmentid, orderid, "O", "SINGLE_ORDER", "F", "F", response.webCheckInItem.warehouseId, response.webCheckInItem.masterItemId);
                }
            }

            response.enablereconcile = EnableReconcile(request.contractId);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public void ReconcileNonBC(dynamic request, dynamic response, dynamic session)
        {
            string usersid = RwAppData.GetUsersId(session);
            string contractid = request.contractid;
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "reconcilenonbc"))
            {
                sp.AddParameter("@contractid", contractid);
                sp.AddParameter("@usersid", usersid);
                sp.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void RemoveExtraItem(dynamic request, dynamic response, dynamic session)
        {
            ChkInItemCancel(request.contractid, "", "", "", "", "", session.security.webUser.usersid, "", request.recorddata.ordertranid, request.recorddata.internalchar, 0);

            response.extraitems = GetExtraSessionedInItems(request.contractid, "");
        }
        //---------------------------------------------------------------------------------------------
        private static void ChkInItemCancel(string contractid, string masteritemid, string masterid, string vendorid, string consignorid, string warehouseid, string usersid,
                                              string description, int ordertranid, string internalchar, int qty)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "chkinitemcancel");
            sp.AddParameter("@contractid",   contractid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@masterid",     masterid);
            sp.AddParameter("@vendorid",     vendorid);
            sp.AddParameter("@consignorid",  consignorid);
            sp.AddParameter("@warehouseid",  warehouseid);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@description",  description);
            sp.AddParameter("@ordertranid",  ordertranid);
            sp.AddParameter("@internalchar", internalchar);
            sp.AddParameter("@qty",          qty);
            sp.Execute();
        }
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
        public static void RFIDScan(dynamic request, dynamic response, dynamic session)
        {
            string batchid;
            dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);

            batchid = RwAppData.LogRFIDTags(conn:      FwSqlConnection.RentalWorks,
                                            portal:    request.portal,
                                            sessionid: request.sessionid,
                                            tags:      request.tags,
                                            usersid:   session.security.webUser.usersid);

            RwAppData.ProcessScannedTags(conn:      FwSqlConnection.RentalWorks,
                                         portal:    request.portal,
                                         sessionid: request.sessionid,
                                         batchid:   batchid,
                                         usersid:   session.security.webUser.usersid,
                                         rfidmode:  "CHECKIN");

            response.tags = GetTags(sessionid: request.sessionid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   batchid);
            response.batchid = batchid;

            if ((request.aisle != "") && (request.shelf != ""))
            {
                foreach (var processeditem in response.processed)
                {
                    if (processeditem.barcode != "")
                    {
                        RwAppData.WebMoveBCLocation(conn:    FwSqlConnection.RentalWorks,
                                                    usersId: session.security.webUser.usersid,
                                                    barcode: processeditem.barcode,
                                                    aisle:   request.aisle,
                                                    shelf:   request.shelf);
                    }
                }
            }

            response.enablereconcile = EnableReconcile(request.sessionid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void LoadRFIDExceptions(dynamic request, dynamic response, dynamic session)
        {
            response.tags = GetTags(sessionid: request.sessionid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ProcessRFIDException(dynamic request, dynamic response, dynamic session)
        {
            dynamic itemstatus;
            switch ((string)request.method)
            {
                case "Clear":
                    RwAppData.ClearRFIDException(conn:      FwSqlConnection.RentalWorks,
                                                 sessionid: request.sessionid,
                                                 tag:       request.rfid);
                    break;
                case "AddOrderToSession":
                    itemstatus = RwAppData.WebGetItemStatus(conn: FwSqlConnection.RentalWorks,
                                                            usersId: session.security.webUser.usersid,
                                                            barcode: request.rfid);
                    response.process = RwAppData.CheckInBC(conn:                   FwSqlConnection.RentalWorks,
                                                           contractid:             request.sessionid,
                                                           orderid:                itemstatus.orderid,
                                                           masteritemid:           itemstatus.masteritemid,
                                                           ordertranid:            itemstatus.ordertranid,
                                                           internalchar:           itemstatus.internalchar,
                                                           vendorid:               itemstatus.vendorid,
                                                           usersid:                session.security.webUser.usersid,
                                                           exchange:               false,
                                                           location:               string.Empty,
                                                           spaceid:                string.Empty,
                                                           spacetypeid:            string.Empty,
                                                           facilitiestypeid:       string.Empty,
                                                           containeritemid:        string.Empty,
                                                           containeroutcontractid: string.Empty,
                                                           aisle:                  string.Empty,
                                                           shelf:                  string.Empty);

                    FwSqlCommand qry1 = new FwSqlCommand(FwSqlConnection.RentalWorks);
                    qry1.Add("update scannedtag");
                    qry1.Add("   set status = 'PROCESSED'");
                    qry1.Add(" where sessionid = @sessionid");
                    qry1.Add("   and tag = @rfid");
                    qry1.AddParameter("@sessionid", request.sessionid);
                    qry1.AddParameter("@rfid",      request.rfid);
                    qry1.Execute();
                    break;
                case "Swap":
                    itemstatus = RwAppData.WebGetItemStatus(conn:    FwSqlConnection.RentalWorks,
                                                            usersId: session.security.webUser.usersid,
                                                            barcode: request.rfid);
                    RwAppData.ModuleType  moduleType      = (RwAppData.ModuleType) Enum.Parse(typeof(RwAppData.ModuleType),  request.moduletype);
                    RwAppData.CheckInMode checkInMode     = (RwAppData.CheckInMode)Enum.Parse(typeof(RwAppData.CheckInMode), request.checkinmode);
                    dynamic webcheckinitem;
                    webcheckinitem = RwAppData.WebCheckInItem(conn:                   FwSqlConnection.RentalWorks,
                                                              usersId:                session.security.webUser.usersid,
                                                              moduleType:             moduleType,
                                                              checkInMode:            checkInMode,
                                                              code:                   request.rfid,
                                                              masterItemId:           itemstatus.masteritemid,
                                                              qty:                    1,
                                                              newOrderAction:         "S",
                                                              containeritemid:        string.Empty,
                                                              containeroutcontractid: string.Empty,
                                                              aisle:                  string.Empty,
                                                              shelf:                  string.Empty,
                                                              parentid:               string.Empty,
                                                              vendorId:               itemstatus.vendorid,
                                                              disablemultiorder:      false,
                                                              contractId:             request.sessionid,
                                                              orderId:                itemstatus.orderid,
                                                              dealId:                 itemstatus.dealid,
                                                              departmentId:           itemstatus.departmentid);

                    FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                    qry.Add("update scannedtag");
                    qry.Add("   set status = 'PROCESSED'");
                    qry.Add(" where sessionid = @sessionid");
                    qry.Add("   and tag = @rfid");
                    qry.AddParameter("@sessionid", request.sessionid);
                    qry.AddParameter("@rfid",      request.rfid);
                    qry.Execute();

                    response.process = new ExpandoObject();
                    response.process.status = webcheckinitem.status;
                    response.process.msg    = webcheckinitem.msg;
                    break;
            }
            response.tags = GetTags(sessionid: request.sessionid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   request.batchid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void RFIDClearSession(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.RFIDClearSession(conn:      FwSqlConnection.RentalWorks,
                                       sessionid: request.sessionid,
                                       usersid:   session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void RFIDCancelItem(dynamic request, dynamic response, dynamic session)
        {
            ChkInItemCancel(request.contractid, request.recorddata.masteritemid, request.recorddata.masterid, request.recorddata.vendorid, request.recorddata.consignorid, request.recorddata.warehouseid, request.recorddata.usersid,
                            request.recorddata.description, request.recorddata.ordertranid, request.recorddata.internalcharot, 1);

            response.tags = GetTags(sessionid: request.contractid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   request.batchid);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetTags(string sessionid, string usersid, string portal, string batchid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("from   dbo.funcscannedtag(@sessionid, @orderid, @usersid, @portal, @batchid, @rfidmode)");
            if (string.IsNullOrEmpty(batchid))
            {
                qry.Add(" where status = 'EXCEPTION'");
            }
            qry.AddParameter("@sessionid", sessionid);
            qry.AddParameter("@orderid",   "");
            qry.AddParameter("@usersid",   usersid);
            qry.AddParameter("@portal",    portal);
            qry.AddParameter("@batchid",   batchid);
            qry.AddParameter("@rfidmode",  "CHECKIN");
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void LoadOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            response.orders = GetOrderPriority(request.contractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void ToggleOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            dynamic orders;
            int includedorders = 0;
            orders = GetOrderPriority(request.contractid);
            for (int i = 0; i < orders.Count; i++)
            {
                if ((orders[i].chkininclude == "T") && (orders[i].orderid != request.recorddata.orderid))
                {
                    includedorders++;
                }
            }

            if (includedorders >= 1)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("update orderpriority");
                    qry.Add("   set chkininclude = @chkininclude");
                    qry.Add(" where contractid   = @contractid");
                    qry.Add("   and orderid      = @orderid");
                    qry.AddParameter("@contractid",   request.contractid);
                    qry.AddParameter("@orderid",      request.recorddata.orderid);
                    qry.AddParameter("@chkininclude", (request.recorddata.chkininclude == "T") ? "F" : "T");
                    qry.Execute();
                }
                response.status = 0;
            }
            else
            {
                response.status  = 1;
                response.message = "At least one order needs to be active.";
            }
            response.orders = GetOrderPriority(request.contractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void UpdateOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            int toindex = 0;
            if (request.priority == "add")
            {
                toindex = FwConvert.ToInt32(request.index) + 1;
            }
            else if (request.priority == "subtract")
            {
                toindex = FwConvert.ToInt32(request.index) - 1;
            }
            
            if (toindex > 0)
            {
                using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "moveorderpriority"))
                {
                    sp.AddParameter("@orderid",      request.recorddata.orderid);
                    sp.AddParameter("@contractid",   request.contractid);
                    sp.AddParameter("@toindex",      toindex.ToString());
                    sp.Execute();
                }
            }

            response.orders = GetOrderPriority(request.contractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="contractid")]
        public void Reconcile(dynamic request, dynamic response, dynamic session)
        {
            string usersid            = RwAppData.GetUsersId(session);
            string contractid         = request.contractid;
            string exchangecontractid = "";                             //TODO: 2017-05-31 MY: need to pass in exchangecontractid
            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "reconcilenonbc"))
            {
                sp.AddParameter("@contractid", contractid);
                sp.AddParameter("@usersid",    usersid);
                sp.Execute();
            }

            using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "reconcilebc"))
            {
                sp.AddParameter("@contractid",         contractid);
                sp.AddParameter("@usersid",            usersid);
                sp.AddParameter("@exchangecontractid", exchangecontractid);
                sp.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetOrderPriority(string contractid)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("orderdate", false, FwJsonDataTableColumn.DataTypes.Date);
                qry.Add("select *");
                qry.Add("  from checkinorderpriorityview with (nolock)");
                qry.Add(" where contractid = @contractid");
                qry.Add("order by priority");
                qry.AddParameter("@contractid", contractid);
                result = qry.QueryToDynamicList2();
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void LoadSessionInList(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "LoadSessionInList";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");

            response.items             = new ExpandoObject();
            response.items.sessionedin = GetSessionedInItems(request.contractid);
            response.items.extraitems  = GetExtraSessionedInItems(request.contractid, "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CheckInItemCancel(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItemCancel";

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "description");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "ordertranid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "internalchar");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");

            if (request.trackedby == "QUANTITY")
            {
                RwAppData.CheckInItemUnassign(conn:         FwSqlConnection.RentalWorks,
                                              contractid:   request.contractid,
                                              orderid:      request.orderid,
                                              vendorid:     request.vendorid,
                                              consignorid:  request.consignorid,
                                              masteritemid: request.masteritemid,
                                              masterid:     request.masterid,
                                              usersid:      session.security.webUser.usersid,
                                              aisle:        request.aisle,
                                              shelf:        request.shelf,
                                              qty:          request.qty);
            }

            RwAppData.CheckInItemCancel(conn:         FwSqlConnection.RentalWorks,
                                        contractid:   request.contractid,
                                        masteritemid: request.masteritemid,
                                        masterid:     request.masterid,
                                        vendorid:     request.vendorid,
                                        consignorid:  request.consignorid,
                                        usersid:      session.security.webUser.usersid,
                                        description:  request.description,
                                        ordertranid:  FwConvert.ToInt32(request.ordertranid),
                                        internalchar: request.internalchar,
                                        qty:          request.qty);

            response.items             = new ExpandoObject();
            response.items.sessionedin = GetSessionedInItems(request.contractid);
            response.items.extraitems  = GetExtraSessionedInItems(request.contractid, "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CheckInItemSendToRepair(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItemSendToRepair";

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            string repairid = RwAppData.CreateRepair(conn:         FwSqlConnection.RentalWorks,
                                                     contractid:   request.contractid,
                                                     orderid:      request.orderid,
                                                     masteritemid: request.masteritemid,
                                                     rentalitemid: request.rentalitemid,
                                                     qty:          request.qty,
                                                     usersid:      session.security.webUser.usersid);
            response.repairno = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "repair", "repairid", repairid, "repairno");
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetSessionedInItems(string contractid)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from   dbo.funccheckincontract(@contractid, @groupby)");
                qry.Add("order by orderby");
                qry.AddParameter("@contractid", contractid);
                qry.AddParameter("@groupby",    "DETAIL");
                result = qry.QueryToDynamicList2();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetExtraSessionedInItems(string contractid, string exchangecontractid)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from   dbo.funcitemstoswap(@contractid, @exchangecontractid)");
                qry.Add("order by masterno, inorderno, outorderno");
                qry.AddParameter("@contractid",         contractid);
                qry.AddParameter("@exchangecontractid", exchangecontractid);
                result = qry.QueryToDynamicList2();
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void LoadPendingList(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "LoadPendingList";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            response.pendingitems = GetPendingItems(request.contractId, "F");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CheckInAllQtyItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInAllQtyItems";
            dynamic pendinglist;
            string trackedby, subbyquantity, orderid, vendorid, masteritemid, masterid, parentid, description;
            decimal qtystillout;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks,
                                             usersId: session.security.webUser.usersid);
            if (session.user.qsallowapplyallqtyitems != "T")
            {
                throw new Exception("You do not have permission to Apply All Quantity Items");
            }
            pendinglist = GetPendingItems(request.contractId, "F");
            for(int i = 0; i < pendinglist.Count; i++)
            {
                trackedby     = pendinglist[i].trackedby;
                subbyquantity = FwConvert.ToString(pendinglist[i].subbyquantity);
                orderid       = pendinglist[i].orderid;
                vendorid      = pendinglist[i].vendorid;
                masteritemid  = pendinglist[i].masteritemid;
                masterid      = pendinglist[i].masterid;
                parentid      = pendinglist[i].parentid;
                description   = pendinglist[i].description;
                qtystillout   = FwConvert.ToDecimal(pendinglist[i].qtystillout);
                if ((trackedby.Equals("QUANTITY") || subbyquantity.Equals("T")) && (qtystillout > 0))
                {
                    RwAppData.CheckInQty(conn:                   FwSqlConnection.RentalWorks,
                                         contractId:             request.contractId,
                                         orderId:                orderid,
                                         usersId:                session.security.webUser.usersid,
                                         vendorId:               vendorid,
                                         consignorId:            string.Empty,
                                         masterItemId:           masteritemid,
                                         masterId:               masterid,
                                         parentId:               parentid,
                                         description:            description,
                                         orderTranId:            0,
                                         internalChar:           string.Empty,
                                         aisle:                  string.Empty,
                                         shelf:                  string.Empty,
                                         qty:                    qtystillout,
                                         containeritemid:        string.Empty,
                                         containeroutcontractid: string.Empty);
                }
            }
            response.pendingitems = GetPendingItems(request.contractId, "F");
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetPendingItems(string contractid, string showall)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("orderid",            false);
            qry.AddColumn("masterid",           false);
            qry.AddColumn("parentid",           false);
            qry.AddColumn("masteritemid",       false);
            qry.AddColumn("exceptionflg",       false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.AddColumn("somein",             false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.AddColumn("masterno",           false);
            qry.AddColumn("description",        false);
            qry.AddColumn("vendor",             false);
            qry.AddColumn("vendorid",           false);
            qry.AddColumn("qtyordered",         false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtystagedandout",    false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtyout",             false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtysub",             false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtysubstagedandout", false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtysubout",          false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtyin",              false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("qtystillout",        false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("missingflg",         false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.AddColumn("missingqty",         false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("trackedby",          false);
            qry.AddColumn("rectype",            false);
            qry.AddColumn("itemclass",          false);
            qry.AddColumn("itemorder",          false);
            qry.AddColumn("orderby",            false);
            qry.AddColumn("optioncolor",        false);
            qry.AddColumn("warehouseid",        false);
            qry.AddColumn("whcode",             false);
            qry.AddColumn("orderno",            false);
            qry.AddColumn("isbarcode",          false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.AddColumn("contractid",         false);
            qry.AddColumn("subbyquantity",      false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.AddColumn("ispackage",          false, FwJsonDataTableColumn.DataTypes.Boolean);
            qry.Add("select *,");
            qry.Add("  ispackage = dbo.ispackage(itemclass)");
            qry.Add("from dbo.funccheckinexception2(@contractid, @rectype, @showall)");
            qry.Add("where exceptionflg = 'T'");
            qry.Add("  and (dbo.ispackage(itemclass) = 'T' or qtystillout > 0)");
            qry.Add("order by orderno, itemorder, masterno");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@rectype",    "R");
            qry.AddParameter("@showall",    showall);
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ToggleReconcile(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "ToggleReconcile";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            response.enable = EnableReconcile(request.contractid);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic EnableReconcile(string contractid)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select dbo.funcenablereconcile(@contractid) as enable");
                qry.AddParameter("@contractid", contractid);
                qry.Execute();
                result = qry.GetField("enable").ToBoolean();
            }

            return result;
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
            result = qry.QueryToDynamicObject2();

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
            result = qry.QueryToDynamicList2();

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

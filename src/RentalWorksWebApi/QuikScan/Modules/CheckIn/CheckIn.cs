using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class CheckIn : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public CheckIn(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void LoadModuleProperties(dynamic request, dynamic response, dynamic session)
        {
            response.syscontrol = LoadSysControlValuesAsync();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CheckInItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItem";
            RwAppData.ModuleType moduleType;
            RwAppData.CheckInMode checkInMode;
            dynamic webGetItemStatus;
            FwJsonDataTable dtSuspendedInContracts;
            string usersid, orderid, code, dealid, departmentid, masteritemid, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, contractid, trackedby, spaceid, spacetypeid, facilitiestypeid;
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
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                usersid = session.security.webUser.usersid;
                code = request.code;
                masteritemid = request.masterItemId;
                qty = request.qty;
                neworderaction = request.newOrderAction;
                containeritemid = FwValidate.IsPropertyDefined(request, "containeritemid") ? request.containeritemid : string.Empty;
                containeroutcontractid = FwValidate.IsPropertyDefined(request, "containeroutcontractid") ? request.containeroutcontractid : string.Empty;
                aisle = request.aisle;
                shelf = request.shelf;
                parentid = request.parentId;
                vendorid = request.vendorId;
                contractid = request.contractId;
                orderid = request.orderId;
                dealid = request.dealId;
                departmentid = request.departmentId;
                trackedby = request.trackedby;
                spaceid = (request.locationdata != null) ? request.locationdata.spaceid : "";
                spacetypeid = (request.locationdata != null) ? request.locationdata.spacetypeid : "";
                facilitiestypeid = (request.locationdata != null) ? request.locationdata.facilitiestypeid : "";
                moduleType = (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType);
                checkInMode = (RwAppData.CheckInMode)Enum.Parse(typeof(RwAppData.CheckInMode), request.checkInMode);
                if (string.IsNullOrEmpty(contractid))
                {
                    webGetItemStatus = await this.AppData.WebGetItemStatusAsync(conn, usersid, code);
                    if ((!string.IsNullOrEmpty(webGetItemStatus.orderNo)) && (webGetItemStatus.rentalStatus == "OUT"))
                    {
                        dtSuspendedInContracts = await this.AppData.GetSuspendedInContractsAsync(conn, moduleType, webGetItemStatus.orderid, usersid);
                        if (dtSuspendedInContracts.Rows.Count > 0)
                        {
                            response.suspendedInContracts = dtSuspendedInContracts;
                            response.webGetItemStatus = webGetItemStatus;
                            checkinitem = false;
                        }
                    }
                }
                if (checkinitem)
                {
                    response.webCheckInItem = await this.AppData.WebCheckInItemAsync(conn, usersid, moduleType, checkInMode, code, masteritemid, qty, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, disablemultiorder, contractid, orderid, dealid, departmentid, trackedby, spaceid, spacetypeid, facilitiestypeid);
                }
                if (!string.IsNullOrEmpty(containeritemid) && (!string.IsNullOrEmpty(response.webCheckInItem.masterItemId)))
                {
                    if (response.webCheckInItem.isICode)
                    {
                        response.fillcontainer = await this.AppData.FillContainer_GetContainerCheckInQuantity2Async(conn, contractid, containeritemid, dealid, departmentid, orderid, "O", "SINGLE_ORDER", "F", "F", response.webCheckInItem.warehouseId, response.webCheckInItem.masterItemId);
                    }
                    else
                    {
                        response.fillcontainer = await this.AppData.FillContainer_GetContainerCheckInBCDataAsync(conn, contractid, containeritemid, dealid, departmentid, orderid, "O", "SINGLE_ORDER", "F", "F", response.webCheckInItem.warehouseId, response.webCheckInItem.masterItemId);
                    }
                }

                response.enablereconcile = await EnableReconcileAsync(request.contractId); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public async Task ReconcileNonBC(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string usersid = this.AppData.GetUsersId(session);
                string contractid = request.contractid;
                using (FwSqlCommand sp = new FwSqlCommand(conn, "reconcilenonbc", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@contractid", contractid);
                    sp.AddParameter("@usersid", usersid);
                    await sp.ExecuteNonQueryAsync();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RemoveExtraItem(dynamic request, dynamic response, dynamic session)
        {
            await ChkInItemCancelAsync(request.contractid, request.recorddata.inorderid, "", "", "", "", "", session.security.webUser.usersid, "", request.recorddata.ordertranid, request.recorddata.internalchar, 0);
        }
        //---------------------------------------------------------------------------------------------
        private async Task ChkInItemCancelAsync(string contractid, string orderid, string masteritemid, string masterid, string vendorid, string consignorid, string warehouseid,
            string usersid, string description, int ordertranid, string internalchar, int qty)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand sp;

                sp = new FwSqlCommand(conn, "chkinitemcancel", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@contractid", contractid);
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@masteritemid", masteritemid);
                sp.AddParameter("@masterid", masterid);
                sp.AddParameter("@vendorid", vendorid);
                sp.AddParameter("@consignorid", consignorid);
                sp.AddParameter("@warehouseid", warehouseid);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@description", description);
                sp.AddParameter("@ordertranid", ordertranid);
                sp.AddParameter("@internalchar", internalchar);
                sp.AddParameter("@qty", qty);
                await sp.ExecuteNonQueryAsync(); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetShowCreateContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select showcreatecontract = (case when exists (select *");
                    qry.Add("                                               from ordertran with (nolock)");
                    qry.Add("                                               where inreturncontractid = @contractid)");
                    qry.Add("                                  then 'T'");
                    qry.Add("                                  else 'F'");
                    qry.Add("                             end)");
                    qry.AddParameter("@contractid", request.contractid);
                    await qry.ExecuteNonQueryAsync();
                    response.showcreatecontract = qry.GetField("showcreatecontract").ToBoolean();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RFIDScan(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string batchid;
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);

                batchid = await this.AppData.LogRFIDTagsAsync(conn: conn,
                                                portal: request.portal,
                                                sessionid: request.sessionid,
                                                tags: request.tags,
                                                usersid: session.security.webUser.usersid,
                                                rfidmode: "CHECKIN");

                await this.AppData.ProcessScannedTagsAsync(conn: conn,
                                             portal: request.portal,
                                             sessionid: request.sessionid,
                                             batchid: batchid,
                                             usersid: session.security.webUser.usersid,
                                             rfidmode: "CHECKIN");

                response.tags = await GetTagsAsync(sessionid: request.sessionid,
                                        usersid: session.security.webUser.usersid,
                                        portal: request.portal,
                                        batchid: batchid);
                response.batchid = batchid;

                if ((request.aisle != "") && (request.shelf != ""))
                {
                    foreach (var processeditem in response.processed)
                    {
                        if (processeditem.barcode != "")
                        {
                            await this.AppData.WebMoveBCLocationAsync(conn: conn,
                                                        usersId: session.security.webUser.usersid,
                                                        barcode: processeditem.barcode,
                                                        aisle: request.aisle,
                                                        shelf: request.shelf);
                        }
                    }
                }

                response.enablereconcile = await EnableReconcileAsync(request.sessionid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadRFIDExceptions(dynamic request, dynamic response, dynamic session)
        {
            response.tags = await GetTagsAsync(sessionid: request.sessionid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ProcessRFIDException(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic itemstatus;
                switch ((string)request.method)
                {
                    case "Clear":
                        await this.AppData.ClearRFIDExceptionAsync(conn: conn,
                                                     sessionid: request.sessionid,
                                                     tag: request.rfid);
                        break;
                    case "AddOrderToSession":
                        itemstatus = await this.AppData.WebGetItemStatusAsync(conn: conn,
                                                                usersId: session.security.webUser.usersid,
                                                                barcode: request.rfid);
                        response.process = await this.AppData.CheckInBCAsync(conn: conn,
                                                               contractid: request.sessionid,
                                                               orderid: itemstatus.orderid,
                                                               masteritemid: itemstatus.masteritemid,
                                                               ordertranid: itemstatus.ordertranid,
                                                               internalchar: itemstatus.internalchar,
                                                               vendorid: itemstatus.vendorid,
                                                               usersid: session.security.webUser.usersid,
                                                               exchange: false,
                                                               location: string.Empty,
                                                               spaceid: string.Empty,
                                                               spacetypeid: string.Empty,
                                                               facilitiestypeid: string.Empty,
                                                               containeritemid: string.Empty,
                                                               containeroutcontractid: string.Empty,
                                                               aisle: string.Empty,
                                                               shelf: string.Empty);

                        FwSqlCommand qry1 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                        qry1.Add("update scannedtag");
                        qry1.Add("   set status = 'PROCESSED'");
                        qry1.Add(" where sessionid = @sessionid");
                        qry1.Add("   and tag = @rfid");
                        qry1.AddParameter("@sessionid", request.sessionid);
                        qry1.AddParameter("@rfid", request.rfid);
                        await qry1.ExecuteNonQueryAsync();
                        break;
                    case "Swap":
                        itemstatus = await this.AppData.WebGetItemStatusAsync(conn: conn,
                                                                usersId: session.security.webUser.usersid,
                                                                barcode: request.rfid);
                        RwAppData.ModuleType moduleType = (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduletype);
                        RwAppData.CheckInMode checkInMode = (RwAppData.CheckInMode)Enum.Parse(typeof(RwAppData.CheckInMode), request.checkinmode);
                        dynamic webcheckinitem;
                        webcheckinitem = await this.AppData.WebCheckInItemAsync(conn: conn,
                                                                  usersId: session.security.webUser.usersid,
                                                                  moduleType: moduleType,
                                                                  checkInMode: checkInMode,
                                                                  code: request.rfid,
                                                                  masterItemId: itemstatus.masteritemid,
                                                                  qty: 1,
                                                                  newOrderAction: "S",
                                                                  containeritemid: string.Empty,
                                                                  containeroutcontractid: string.Empty,
                                                                  aisle: string.Empty,
                                                                  shelf: string.Empty,
                                                                  parentid: string.Empty,
                                                                  vendorId: itemstatus.vendorid,
                                                                  disablemultiorder: false,
                                                                  contractId: request.sessionid,
                                                                  orderId: itemstatus.orderid,
                                                                  dealId: itemstatus.dealid,
                                                                  departmentId: itemstatus.departmentid,
                                                                  trackedby: itemstatus.trackedby,
                                                                  spaceid: "",
                                                                  spacetypeid: "",
                                                                  facilitiestypeid: "");

                        FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("update scannedtag");
                        qry.Add("   set status = 'PROCESSED'");
                        qry.Add(" where sessionid = @sessionid");
                        qry.Add("   and tag = @rfid");
                        qry.AddParameter("@sessionid", request.sessionid);
                        qry.AddParameter("@rfid", request.rfid);
                        await qry.ExecuteNonQueryAsync();

                        response.process = new ExpandoObject();
                        response.process.status = webcheckinitem.status;
                        response.process.msg = webcheckinitem.msg;
                        break;
                }
                response.tags = await GetTagsAsync(sessionid: request.sessionid,
                                        usersid: session.security.webUser.usersid,
                                        portal: request.portal,
                                        batchid: request.batchid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RFIDClearSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.RFIDClearSessionAsync(conn: conn,
                                                  sessionid: request.sessionid,
                                                  usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RFIDCancelItem(dynamic request, dynamic response, dynamic session)
        {
            await ChkInItemCancelAsync(request.contractid, request.recorddata.orderid, request.recorddata.masteritemid, request.recorddata.masterid, request.recorddata.vendorid, request.recorddata.consignorid, request.recorddata.warehouseid, request.recorddata.usersid,
                            request.recorddata.description, request.recorddata.ordertranid, request.recorddata.internalcharot, 1);

            response.tags = await GetTagsAsync(sessionid: request.contractid,
                                    usersid:   session.security.webUser.usersid,
                                    portal:    request.portal,
                                    batchid:   request.batchid);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetTagsAsync(string sessionid, string usersid, string portal, string batchid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("from   dbo.funcscannedtag(@sessionid, @orderid, @usersid, @portal, @batchid, @rfidmode)");
                if (string.IsNullOrEmpty(batchid))
                {
                    qry.Add(" where status = 'EXCEPTION'");
                }
                qry.AddParameter("@sessionid", sessionid);
                qry.AddParameter("@orderid", "");
                qry.AddParameter("@usersid", usersid);
                qry.AddParameter("@portal", portal);
                qry.AddParameter("@batchid", batchid);
                qry.AddParameter("@rfidmode", "CHECKIN");
                dynamic result = await qry.QueryToDynamicList2Async(); 
                return result;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            response.orders = await GetOrderPriorityAsync(request.contractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ToggleOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic orders;
                int includedorders = 0;
                orders = await GetOrderPriorityAsync(request.contractid);
                for (int i = 0; i < orders.Count; i++)
                {
                    if ((orders[i].chkininclude == "T") && (orders[i].orderid != request.recorddata.orderid))
                    {
                        includedorders++;
                    }
                }

                if (includedorders >= 1)
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("update orderpriority");
                        qry.Add("   set chkininclude = @chkininclude");
                        qry.Add(" where contractid   = @contractid");
                        qry.Add("   and orderid      = @orderid");
                        qry.AddParameter("@contractid", request.contractid);
                        qry.AddParameter("@orderid", request.recorddata.orderid);
                        qry.AddParameter("@chkininclude", (request.recorddata.chkininclude == "T") ? "F" : "T");
                        await qry.ExecuteAsync();
                    }
                    response.status = 0;
                }
                else
                {
                    response.status = 1;
                    response.message = "At least one order needs to be active.";
                }
                response.orders = await GetOrderPriorityAsync(request.contractid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task UpdateOrderPriority(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
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
                    using (FwSqlCommand sp = new FwSqlCommand(conn, "moveorderpriority", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        sp.AddParameter("@orderid", request.recorddata.orderid);
                        sp.AddParameter("@contractid", request.contractid);
                        sp.AddParameter("@toindex", toindex.ToString());
                        await sp.ExecuteNonQueryAsync();
                    }
                }

                response.orders = await GetOrderPriorityAsync(request.contractid);
            } 
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="contractid")]
        public async Task Reconcile(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string usersid = this.AppData.GetUsersId(session);
                string contractid = request.contractid;
                string exchangecontractid = "";                             //TODO: 2017-05-31 MY: need to pass in exchangecontractid
                using (FwSqlCommand sp = new FwSqlCommand(conn, "reconcilenonbc", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@contractid", contractid);
                    sp.AddParameter("@usersid", usersid);
                    await sp.ExecuteNonQueryAsync();
                }

                using (FwSqlCommand sp = new FwSqlCommand(conn, "reconcilebc", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@contractid", contractid);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@exchangecontractid", exchangecontractid);
                    await sp.ExecuteNonQueryAsync();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> GetOrderPriorityAsync(string contractid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                    qry.Add("select *");
                    qry.Add("  from checkinorderpriorityview with (nolock)");
                    qry.Add(" where contractid = @contractid");
                    qry.Add("order by priority");
                    qry.AddParameter("@contractid", contractid);
                    result = await qry.QueryToDynamicList2Async();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SessionInSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string searchMode = request.searchmode;
                string searchValue = request.searchvalue;
                int pageNo = 0;
                int pageSize = 0;
                if (FwValidate.IsPropertyDefined(request, "pageno"))
                {
                    pageNo = request.pageno;
                }
                if (FwValidate.IsPropertyDefined(request, "pagesize"))
                {
                    pageSize = request.pagesize;
                }
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                qry.AddColumn("sessionin", false, FwDataTypes.Decimal);
                qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
                select.Add("select *");
                select.Add("from dbo.funccheckincontract(@contractid, @groupby)");
                select.Parse();
                if (searchMode == "description" && searchValue != null && searchValue.Length > 0)
                {
                    string[] searchValues = searchValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        select.AddWhere($"description like @searchvalue{i}");
                        select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                    }
                }
                select.AddOrderBy("orderby");
                select.AddParameter("@contractid", request.contractid);
                select.AddParameter("@groupby", "DETAIL");

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                response.extraitems = await GetExtraSessionedInItemsAsync(request.contractid, "").Count; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CheckInItemCancel(dynamic request, dynamic response, dynamic session)
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

            //if (request.trackedby == "QUANTITY")  //Removed for 2018 per Emil
            //{
            //    this.AppData.CheckInItemUnassign(conn:         conn,
            //                                  contractid:   request.contractid,
            //                                  orderid:      request.orderid,
            //                                  masteritemid: request.masteritemid,
            //                                  masterid:     request.masterid,
            //                                  description:  request.description,
            //                                  vendorid:     request.vendorid,
            //                                  consignorid:  request.consignorid,
            //                                  usersid:      session.security.webUser.usersid,
            //                                  aisle:        request.aisle,
            //                                  shelf:        request.shelf,
            //                                  qty:          request.qty);
            //}

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.CheckInItemCancelAsync(conn: conn,
                                                    contractid: request.contractid,
                                                    orderid: request.orderid,
                                                    masteritemid: request.masteritemid,
                                                    masterid: request.masterid,
                                                    vendorid: request.vendorid,
                                                    consignorid: request.consignorid,
                                                    usersid: session.security.webUser.usersid,
                                                    description: request.description,
                                                    ordertranid: FwConvert.ToInt32(request.ordertranid),
                                                    internalchar: request.internalchar,
                                                    qty: request.qty); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CheckInItemSendToRepair(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItemSendToRepair";

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string repairid = await this.AppData.CreateRepairAsync(conn: conn,
                                                                 contractid: request.contractid,
                                                                 orderid: request.orderid,
                                                                 masteritemid: request.masteritemid,
                                                                 rentalitemid: request.rentalitemid,
                                                                 qty: request.qty,
                                                                 usersid: session.security.webUser.usersid);
                response.repairno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "repair", "repairid", repairid, "repairno"); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetSessionedInItemsAsync(string contractid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select *");
                    qry.Add("from   dbo.funccheckincontract(@contractid, @groupby)");
                    qry.Add("order by orderby");
                    qry.AddParameter("@contractid", contractid);
                    qry.AddParameter("@groupby", "DETAIL");
                    result = await qry.QueryToDynamicList2Async();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ExtraItemsSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;
                FwSqlSelect select = new FwSqlSelect();

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("from   dbo.funcitemstoswap(@contractid, @exchangecontractid)");
                select.Add("order by masterno, inorderno, outorderno");
                select.AddParameter("@contractid", request.contractid);
                select.AddParameter("@exchangecontractid", "");

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetExtraSessionedInItemsAsync(string contractid, string exchangecontractid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select *");
                    qry.Add("from   dbo.funcitemstoswap(@contractid, @exchangecontractid)");
                    qry.Add("order by masterno, inorderno, outorderno");
                    qry.AddParameter("@contractid", contractid);
                    qry.AddParameter("@exchangecontractid", exchangecontractid);
                    result = await qry.QueryToDynamicList2Async();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task PendingSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string searchMode = request.searchmode;
                string searchValue = request.searchvalue;
                int pageNo = 0;
                int pageSize = 0;
                if (FwValidate.IsPropertyDefined(request, "pageno"))
                {
                    pageNo = request.pageno;
                }
                if (FwValidate.IsPropertyDefined(request, "pagesize"))
                {
                    pageSize = request.pagesize;
                }
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = pageNo;
                select.PageSize = pageSize;
                qry.AddColumn("exceptionflg", false, FwDataTypes.Boolean);
                qry.AddColumn("somein", false, FwDataTypes.Boolean);
                qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
                qry.AddColumn("qtystagedandout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtyout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysub", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubstagedandout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtyin", false, FwDataTypes.Decimal);
                qry.AddColumn("qtystillout", false, FwDataTypes.Decimal);
                qry.AddColumn("missingflg", false, FwDataTypes.Boolean);
                qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
                qry.AddColumn("isbarcode", false, FwDataTypes.Boolean);
                qry.AddColumn("subbyquantity", false, FwDataTypes.Boolean);
                qry.AddColumn("ispackage", false, FwDataTypes.Boolean);
                select.Add("select *, ispackage = dbo.ispackage(itemclass)");
                select.Add("from dbo.funccheckinexception(@contractid, @rectype, @containeritemid, @showall)");
                select.Add("where exceptionflg = 'T'");
                select.Add("  and (dbo.ispackage(itemclass) = 'T' or qtystillout > 0)");
                select.Parse();
                if (searchMode == "description" && searchValue != null && searchValue.Length > 0)
                {
                    string[] searchValues = searchValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        select.AddWhere($"description like @searchvalue{i}");
                        select.AddParameter($"@searchvalue{i}", $"%{searchValue[i]}%");
                    }
                }
                select.AddOrderBy("orderno, itemorder, masterno");
                select.AddParameter("@contractid", request.contractid);
                select.AddParameter("@rectype", "R");
                select.AddParameter("@containeritemid", "");
                select.AddParameter("@showall", "F");

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CheckInAllQtyItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "CheckInAllQtyItems";
                dynamic pendinglist;
                string trackedby, subbyquantity, orderid, vendorid, masteritemid, masterid, parentid, description;
                decimal qtystillout;

                FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
                session.user = await this.AppData.GetUserAsync(conn: conn,
                                                 usersId: session.security.webUser.usersid);
                if (session.user.qsallowapplyallqtyitems != "T")
                {
                    throw new Exception("You do not have permission to Apply All Quantity Items");
                }
                pendinglist = GetPendingItems(request.contractId, "F");
                for (int i = 0; i < pendinglist.Count; i++)
                {
                    trackedby = pendinglist[i].trackedby;
                    subbyquantity = FwConvert.ToString(pendinglist[i].subbyquantity);
                    orderid = pendinglist[i].orderid;
                    vendorid = pendinglist[i].vendorid;
                    masteritemid = pendinglist[i].masteritemid;
                    masterid = pendinglist[i].masterid;
                    parentid = pendinglist[i].parentid;
                    description = pendinglist[i].description;
                    qtystillout = FwConvert.ToDecimal(pendinglist[i].qtystillout);
                    if ((trackedby.Equals("QUANTITY") || subbyquantity.Equals("T")) && (qtystillout > 0))
                    {
                        await this.AppData.CheckInQtyAsync(conn: conn,
                                             contractId: request.contractId,
                                             orderId: orderid,
                                             usersId: session.security.webUser.usersid,
                                             vendorId: vendorid,
                                             consignorId: string.Empty,
                                             masterItemId: masteritemid,
                                             masterId: masterid,
                                             parentId: parentid,
                                             description: description,
                                             orderTranId: 0,
                                             internalChar: string.Empty,
                                             aisle: string.Empty,
                                             shelf: string.Empty,
                                             qty: qtystillout,
                                             containeritemid: string.Empty,
                                             containeroutcontractid: string.Empty);
                    }
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetPendingItems(string contractid, string showall)
        {
            dynamic result;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("orderid", false);
                qry.AddColumn("masterid", false);
                qry.AddColumn("parentid", false);
                qry.AddColumn("masteritemid", false);
                qry.AddColumn("exceptionflg", false, FwDataTypes.Boolean);
                qry.AddColumn("somein", false, FwDataTypes.Boolean);
                qry.AddColumn("masterno", false);
                qry.AddColumn("description", false);
                qry.AddColumn("vendor", false);
                qry.AddColumn("vendorid", false);
                qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
                qry.AddColumn("qtystagedandout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtyout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysub", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubstagedandout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtyin", false, FwDataTypes.Decimal);
                qry.AddColumn("qtystillout", false, FwDataTypes.Decimal);
                qry.AddColumn("missingflg", false, FwDataTypes.Boolean);
                qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
                qry.AddColumn("trackedby", false);
                qry.AddColumn("rectype", false);
                qry.AddColumn("itemclass", false);
                qry.AddColumn("itemorder", false);
                qry.AddColumn("orderby", false);
                qry.AddColumn("optioncolor", false);
                qry.AddColumn("warehouseid", false);
                qry.AddColumn("whcode", false);
                qry.AddColumn("orderno", false);
                qry.AddColumn("isbarcode", false, FwDataTypes.Boolean);
                qry.AddColumn("contractid", false);
                qry.AddColumn("subbyquantity", false, FwDataTypes.Boolean);
                qry.AddColumn("ispackage", false, FwDataTypes.Boolean);
                qry.Add("select *,");
                qry.Add("       ispackage = dbo.ispackage(itemclass)");
                qry.Add("  from dbo.funccheckinexception(@contractid, @rectype, @containeritemid, @showall)");
                qry.Add(" where exceptionflg = 'T'");
                qry.Add("   and (dbo.ispackage(itemclass) = 'T' or qtystillout > 0)");
                qry.Add("order by orderno, itemorder, masterno");
                qry.AddParameter("@contractid", contractid);
                qry.AddParameter("@rectype", "R");
                qry.AddParameter("@containeritemid", "");
                qry.AddParameter("@showall", showall);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ToggleReconcile(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "ToggleReconcile";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            response.enable = await EnableReconcileAsync(request.contractid);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> EnableReconcileAsync(string contractid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select dbo.funcenablereconcile(@contractid) as enable");
                    qry.AddParameter("@contractid", contractid);
                    await qry.ExecuteAsync();
                    result = qry.GetField("enable").ToBoolean();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetSerialInfo(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                response.serial = await this.FuncSerialFrmAsync(conn: conn,
                                                        orderid: request.orderid,
                                                        warehouseid: userLocation.warehouseId,
                                                        contractid: request.contractid,
                                                        masterid: request.masterid);

                response.serialitems = await this.FuncSerialMeterIn2Async(conn: conn,
                                                                  contractid: request.contractid,
                                                                  orderid: request.orderid,
                                                                  masterid: request.masterid,
                                                                  warehouseid: userLocation.warehouseId); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetSerialItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                response.serialitems = await this.FuncSerialMeterIn2Async(conn: conn,
                                                                  contractid: request.contractid,
                                                                  orderid: request.orderid,
                                                                  masterid: request.masterid,
                                                                  warehouseid: userLocation.warehouseId); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SerialSessionIn(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                await this.InsertSerialSessionAsync(conn: conn,
                                            contractid: request.contractid,
                                            orderid: request.orderid,
                                            masteritemid: request.masteritemid,
                                            rentalitemid: request.rentalitemid,
                                            activitytype: "I",
                                            usersid: session.security.webUser.usersid,
                                            meter: FwConvert.ToDecimal(request.meter),
                                            toggledelete: request.toggledelete);

                response.serial = await this.FuncSerialFrmAsync(conn: conn,
                                                        orderid: request.orderid,
                                                        warehouseid: userLocation.warehouseId,
                                                        contractid: request.contractid,
                                                        masterid: request.masterid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SearchLocations(dynamic request, dynamic response, dynamic session)
        {
            response.locations = await SearchOrderLocationsAsync(orderid:     request.orderid,
                                                      searchvalue: request.searchvalue);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> SearchOrderLocationsAsync(string orderid, string searchvalue)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result = new ExpandoObject();
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                string includefacilitiestype = "F";
                dynamic applicationoptions = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
                string[] searchvalues = searchvalue.Split(' ');

                if (applicationoptions.facilities.enabled)
                {
                    dynamic controlresult = LoadSysControlValuesAsync();

                    if ((controlresult.itemsinrooms == "T") && (controlresult.facilitytypeincurrentlocation == "T"))
                    {
                        includefacilitiestype = "T";
                    }
                }

                qry.Add("select *");
                qry.Add("  from dbo.funcorderspacelocation(@orderid, @includefacilitiestype)");
                qry.Add(" where orderid = orderid");

                if (searchvalues.Length == 1)
                {
                    qry.Add("   and location like '%' + @searchvalue + '%'");
                    qry.AddParameter("@searchvalue", searchvalues[0]);
                }
                else
                {
                    qry.Add("   and (");
                    for (int i = 0; i < searchvalues.Length; i++)
                    {
                        if (i > 0)
                        {
                            qry.Add(" or ");
                        }
                        qry.Add("(location like '%' + @searchvalue" + i + " + '%')");
                        qry.AddParameter("@searchvalue" + i, searchvalues[i]);
                    }
                    qry.Add(")");
                }

                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@includefacilitiestype", includefacilitiestype);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> LoadSysControlValuesAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 itemsinrooms, facilitytypeincurrentlocation");
                qry.Add("  from syscontrol with (nolock)");
                qry.Add(" where controlid = '1'");
                dynamic result = await qry.QueryToDynamicObject2Async();

                return result; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FuncSerialFrmAsync(FwSqlConnection conn, string orderid, string warehouseid, string contractid, string masterid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialfrm(@orderid, @warehouseid, @contractid)");
            qry.Add(" where masterid = @masterid");
            qry.AddParameter("@orderid",      "");
            qry.AddParameter("@warehouseid",  "");
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = await qry.QueryToDynamicObject2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FuncSerialMeterIn2Async(FwSqlConnection conn, string contractid, string orderid, string masterid, string warehouseid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("  from dbo.funcserialmeterin2(@contractid, @orderid, @masterid, @warehouseid, '')");
            qry.AddParameter("@orderid",      "");
            qry.AddParameter("@warehouseid",  warehouseid);
            qry.AddParameter("@contractid",   contractid);
            qry.AddParameter("@masterid",     masterid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task InsertSerialSessionAsync(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string rentalitemid, string activitytype, string usersid, decimal meter, string toggledelete)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, "dbo.insertserialsession", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
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
            qry.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
            qry.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await qry.ExecuteAsync();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CreateContract(dynamic request, dynamic response, dynamic session)   //Only called directly from check-in when moduletype == transfer
        {
            const string METHOD_NAME = "CreateContract";
            string usersid, contracttype, contractid, orderid, responsiblepersonid;
            string printname = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "responsiblePersonId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "signatureImage");
            usersid             = session.security.webUser.usersid;
            contracttype        = request.contractType;
            contractid          = request.contractId;
            orderid             = request.orderId;
            responsiblepersonid = request.responsiblePersonId;
            if (FwValidate.IsPropertyDefined(request, "printname"))
            {
                printname = request.printname;
            }

            // Create the contract
            response.createcontract = await WebCreateContractAsync(usersid, contracttype, contractid, orderid, responsiblepersonid, printname);

            // insert the signature image
            //FwSqlData.InsertAppImage(conn, contractid, string.Empty, string.Empty, "CONTRACT_SIGNATURE", string.Empty, "JPG", request.signatureImage);

            //if ((FwValidate.IsPropertyDefined(request, "images")) && (request.images.Length > 0))
            //{
            //    byte[] image;
            //    for (int i = 0; i < request.images.Length; i++)
            //    {
            //        image = Convert.FromBase64String(request.images[i]);
            //        FwSqlData.InsertAppImage(conn, contractid, string.Empty, string.Empty, "CONTRACT_IMAGE", string.Empty, "JPG", image);
            //    }
            //}

            //if (contracttype == this.AppData.CONTRACT_TYPE_OUT)
            //{
            //    FwSqlCommand qry = new FwSqlCommand(conn);
            //    qry.Add("select orderno, orderdesc");
            //    qry.Add("  from dealorder with (nolock)");
            //    qry.Add(" where orderid = @orderid");
            //    qry.AddParameter("@orderid", orderid);
            //    qry.Execute();

            //    response.subject = qry.GetField("@orderno").ToString().TrimEnd() + " - " + qry.GetField("@orderdesc").ToString().TrimEnd() + " - Out Contract";
            //}
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebCreateContractAsync(string usersid, string contracttype, string contractid, string orderId, string responsiblePersonId, string printname)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand sp, qryUpdateDealOrderDetail, qryUpdateContract;

                FwSqlCommand qryInputByUser;
                string inputbyusersid, namefml;
                if (!string.IsNullOrEmpty(contractid))
                {
                    qryInputByUser = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryInputByUser.Add("select c.inputbyusersid, u.namefml");
                    qryInputByUser.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
                    qryInputByUser.Add("where contractid = @contractid");
                    qryInputByUser.AddParameter("@contractid", contractid);
                    await qryInputByUser.ExecuteAsync();
                    inputbyusersid = qryInputByUser.GetField("inputbyusersid").ToString().TrimEnd();
                    namefml = qryInputByUser.GetField("namefml").ToString().TrimEnd();
                    if (usersid != inputbyusersid)
                    {
                        throw new Exception("Only the session owner " + namefml + " can create a contract.");
                    }
                }

                sp = new FwSqlCommand(conn, "dbo.webcreatecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@contracttype", contracttype);
                sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.InputOutput, contractid);
                sp.AddParameter("@orderid", orderId);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@personprintname", printname);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteNonQueryAsync();
                result = new ExpandoObject();
                result.contractId = sp.GetParameter("@contractid").ToString().TrimEnd();
                result.status = sp.GetParameter("@status").ToInt32();
                result.msg = sp.GetParameter("@msg").ToString().TrimEnd();

                if ((contracttype == RwAppData.CONTRACT_TYPE_OUT) && (!string.IsNullOrEmpty(responsiblePersonId)))
                {
                    qryUpdateDealOrderDetail = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateDealOrderDetail.Add("update dealorderdetail");
                    qryUpdateDealOrderDetail.Add("set responsiblepersonid = @responsiblepersonid");
                    qryUpdateDealOrderDetail.Add("where orderid           = @orderid");
                    qryUpdateDealOrderDetail.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateDealOrderDetail.AddParameter("@orderid", orderId);
                    await qryUpdateDealOrderDetail.ExecuteNonQueryAsync();

                    qryUpdateContract = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateContract.Add("update contract");
                    qryUpdateContract.Add("   set responsiblepersonid = @responsiblepersonid,");
                    qryUpdateContract.Add(" where contractId          = @contractId");
                    qryUpdateContract.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateContract.AddParameter("@contractId", result.contractId);
                    await qryUpdateContract.ExecuteNonQueryAsync();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}

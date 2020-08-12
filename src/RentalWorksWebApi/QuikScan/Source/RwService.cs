using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Modules;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Source
{
    public class RwService
    {
        FwApplicationConfig ApplicationConfig;
        FwUserSession UserSession;
        RwAppData AppData;
        //---------------------------------------------------------------------------------------------
        public RwService(FwApplicationConfig applicationConfig, FwUserSession userSession)
        {
            this.ApplicationConfig = applicationConfig;
            this.UserSession = userSession;
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetAuthTokenAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "GetAuthToken";
                bool hasWebUser;
                AccountService accountService = new AccountService(this.ApplicationConfig, this.UserSession);
                await accountService.GetAuthTokenAsync(conn, request, response, session);
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");

                response.checkDatabaseVersionMessage = await FwSqlData.CheckDatabaseVersionAsync(conn: conn,
                                                                                      this.ApplicationConfig.DatabaseSettings,
                                                                                      requireddbversion: Version.RequiredDatabaseVersion,
                                                                                      requiredhotfixfilename: Version.RequiredHotfixFilename);
                response.minBrowserVersion = Version.MinIosAppVersion;

                if (response.errNo == 0)
                {
                    FwValidate.TestPropertyDefined(METHOD_NAME, session.security, "webUser");
                    FwValidate.TestPropertyDefined(METHOD_NAME, session.security.webUser, "usersid");
                    FwValidate.TestPropertyDefined(METHOD_NAME, session.security.webUser, "contactid");
                    hasWebUser = FwValidate.IsPropertyDefined(session.security, "webUser");
                    if (hasWebUser)
                    {
                        if ((session.security.webUser.usertype == "USER") && (!string.IsNullOrEmpty(session.security.webUser.usersid)))
                        {
                            session.user = await this.AppData.GetUserAsync(conn: conn
                                                           , usersId: session.security.webUser.usersid);
                            response.user = new ExpandoObject();
                            response.user.enablecreatecontract = session.user.enablecreatecontract;
                            response.user.qsallowapplyallqtyitems = session.user.qsallowapplyallqtyitems;
                            if (string.IsNullOrEmpty(session.user.warehouseid)) throw new Exception("A warehouse needs to be set on your user account before you can login to this system.");
                            response.barcodeskipprefixes = await this.AppData.GetBarcodeSkipPrefixesAsync(conn, session.user.warehouseid);
                        }
                        if ((session.security.webUser.usertype == "CONTACT") && (!string.IsNullOrEmpty(session.security.webUser.contactid)))
                        {
                            session.contact = new ExpandoObject();
                            session.contact = this.AppData.GetContactAsync(conn: conn
                                                                 , contactId: session.security.webUser.contactid);
                        }
                    }
                    //GetSite(request, response, session);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task AuthPasswordAsync(dynamic request, dynamic response, dynamic session)
        {
            bool isValid = false;
            const string METHOD_NAME = "AuthPassword";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                isValid = await AccountService.Current.AuthenticatePasswordAsync(conn, request, response, session); 
            }
        }
        //---------------------------------------------------------------------------------------------
        //public void GetSite(dynamic request, dynamic response, dynamic session)
        //{
        //    FwApplicationConfig_Site site = FwApplicationConfig.CurrentSite;
            
        //    response.site = new ExpandoObject();
        //    response.site.name = site.Name;
        //}
        //---------------------------------------------------------------------------------------------
        public async Task CheckInItemAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInItem";
            RwAppData.ModuleType moduleType;
            RwAppData.CheckInMode checkInMode;
            dynamic webGetItemStatus;
            FwJsonDataTable dtSuspendedInContracts;
            string usersid, orderid, code, dealid, departmentid, masteritemid, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, contractid;
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
                    response.webCheckInItem = await this.AppData.WebCheckInItemAsync(conn, usersid, moduleType, checkInMode, code, masteritemid, qty, neworderaction, containeritemid, containeroutcontractid, aisle, shelf, parentid, vendorid, disablemultiorder, contractid, orderid, dealid, departmentid, "", "", "", "");
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
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task CreateNewInContractAndSuspendAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateNewInContractAndSuspend";    
            string usersid, orderid, dealid, departmentid;
            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentid");
            orderid      = request.orderid;
            dealid       = request.dealid;
            departmentid = request.departmentid;
            usersid      = session.security.webUser.usersid;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.contract = await this.AppData.CreateNewInContractAndSuspendAsync(conn, orderid, dealid, departmentid, usersid);     
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task SelectRepairOrderAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectRepairOrder";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.webSelectRepairOrder = await this.AppData.WebRepairItemAsync(conn: conn
                                                                              , code: request.code
                                                                              , repairMode: RwAppData.RepairMode.Select
                                                                              , usersId: session.security.webUser.usersid
                                                                              , qty: 0); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task SelectSessionAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectSession";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "sessionNo");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.webSelectSession = await this.AppData.WebSelectSessionAsync(conn: conn
                                                                             , usersId: session.security.webUser.usersid
                                                                             , sessionNo: request.sessionNo
                                                                             , moduleType: (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType)); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task PdaStageItemAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "StageItem";
            string masteritemid = null;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "additemtoorder");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "addcompletetoorder");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "releasefromrepair");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "unstage");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "meter");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "location");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "addcontainertoorder");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "overridereservation");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "stageconsigned");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "transferrepair");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "removefromcontainer");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "ignoresuspendedin");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignoragreementid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.userLocation = await this.AppData.GetUserLocationAsync(conn: conn
                                                                       , usersId: session.security.webUser.usersid);
                string spaceid = string.Empty;
                string spacetypeid = string.Empty;
                string facilitiestypeid = string.Empty;
                if (FwValidate.IsPropertyDefined(request, "locationdata"))
                {
                    if (request.locationdata != null)
                    {
                        spaceid = request.locationdata.spaceid;
                        spacetypeid = request.locationdata.spacetypeid;
                        facilitiestypeid = request.locationdata.facilitiestypeid;
                    }
                }
                response.webStageItem = await this.AppData.PdaStageItemAsync(conn: conn,
                                                               orderid: request.orderid,
                                                               code: request.code,
                                                               masteritemid: request.masteritemid,
                                                               usersid: session.security.webUser.usersid,
                                                               qty: request.qty,
                                                               additemtoorder: request.additemtoorder,
                                                               addcompletetoorder: request.addcompletetoorder,
                                                               releasefromrepair: request.releasefromrepair,
                                                               unstage: request.unstage,
                                                               vendorid: request.vendorid,
                                                               meter: request.meter,
                                                               location: request.location,
                                                               spaceid: spaceid,
                                                               addcontainertoorder: request.addcontainertoorder,
                                                               overridereservation: request.overridereservation,
                                                               stageconsigned: request.stageconsigned,
                                                               transferrepair: request.transferrepair,
                                                               removefromcontainer: request.removefromcontainer,
                                                               contractid: request.contractid,
                                                               ignoresuspendedin: request.ignoresuspendedin,
                                                               consignorid: request.consignorid,
                                                               consignoragreementid: request.consignoragreementid,
                                                               spacetypeid: spacetypeid,
                                                               facilitiestypeid: facilitiestypeid);
                if (!string.IsNullOrEmpty(request.masteritemid))
                {
                    masteritemid = request.masteritemid;
                }
                else if (!string.IsNullOrEmpty(response.webStageItem.masterItemId))
                {
                    masteritemid = response.webStageItem.masterItemId;
                }
                if (!string.IsNullOrEmpty(masteritemid))
                {
                    session.getStagingPendingItem = await this.AppData.FuncCheckoutExceptionAsync(conn: conn,
                                                                                    orderId: request.orderid,
                                                                                    warehouseId: session.userLocation.warehouseId,
                                                                                    contractId: request.contractid,
                                                                                    masterItemId: masteritemid);
                    response.webStageItem.masterId = session.getStagingPendingItem.masterId;
                    response.webStageItem.masterItemId = session.getStagingPendingItem.masterItemId;
                    response.webStageItem.masterNo = session.getStagingPendingItem.masterNo;
                    response.webStageItem.description = session.getStagingPendingItem.description;
                    response.webStageItem.qtyOrdered = session.getStagingPendingItem.qtyOrdered;
                    response.webStageItem.qtySub = session.getStagingPendingItem.qtySub;
                    response.webStageItem.qtyStaged = session.getStagingPendingItem.qtyStagedAndOut;
                    response.webStageItem.qtyOut = session.getStagingPendingItem.qtyOut;
                    if (request.contractid == string.Empty)
                    {
                        response.webStageItem.qtyIn = session.getStagingPendingItem.qtyIn;
                    }
                    else
                    {
                        response.webStageItem.qtyIn = session.getStagingPendingItem.qtyStagedAndOut;
                    }
                    response.webStageItem.qtyRemaining = session.getStagingPendingItem.missingQty;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task UnstageItemAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "UnstageItem";
            string contractid, vendorid, masteritemid, usersid, orderid, warehouseid;
            decimal qty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestIsNumeric(METHOD_NAME, "qty", request.qty);

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                usersid = session.security.webUser.usersid;
                contractid = request.contractid;
                vendorid = request.vendorid;
                masteritemid = request.masteritemid;
                qty = request.qty;
                orderid = request.orderid;
                session.userLocation = await this.AppData.GetUserLocationAsync(conn, usersid);
                warehouseid = session.userLocation.warehouseId;
                response.unstageItem = await this.AppData.AdvancedMoveMasterItemIdAsync(conn, contractid, vendorid, masteritemid, qty, usersid, 4);
                Staging staging = new Staging(this.ApplicationConfig);
                response.getStagingStagedItems = await staging.funccheckedoutAsync(conn, contractid, string.Empty, string.Empty, 0, 0); 
            }
        }
        //---------------------------------------------------------------------------------------------
        //public async Task GetPhyItemInfo(dynamic request, dynamic response, dynamic session)
        //{
        //    const string METHOD_NAME = "GetPhyItemInfo";
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalId");
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
        //    response.webGetPhyItemInfo = RwAppData.WebGetPhyItemInfo(physicalId: request.physicalId
        //                                                           , code:       request.code);
        //}
        //---------------------------------------------------------------------------------------------
        public async Task PhyCountItemAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "PhyCountItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalId");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (FwValidate.IsPropertyDefined(request, "qty"))
                {
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalId");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalItemId");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalItemId");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterId");
                    //FwValidate.TestPropertyDefined(METHOD_NAME, request, "isICode");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "addReplace");
                    //FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
                    FwValidate.TestIsNumeric(METHOD_NAME, "qty", request.qty);
                    response.webPhyCountItem = await this.AppData.WebPhyCountItemAsync(conn: conn
                                                                       , physicalId: request.physicalId
                                                                       , physicalItemId: request.physicalItemId
                                                                       , rentalItemId: request.rentalItemId
                                                                       , masterId: request.masterId
                                                                       , isICode: true
                                                                       , usersId: session.security.webUser.usersid
                                                                       , addReplace: request.addReplace
                                                                       , qty: request.qty);
                }
                else
                {
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
                    response.webGetPhyItemInfo = await this.AppData.WebGetPhyItemInfoAsync(conn: conn
                                                                           , usersId: session.security.webUser.usersid
                                                                           , physicalId: request.physicalId
                                                                           , code: request.code);
                    if ((response.webGetPhyItemInfo.itemType == "BarCoded") && (response.webGetPhyItemInfo.status == 0))
                    {
                        response.webPhyCountItem = await this.AppData.WebPhyCountItemAsync(conn: conn
                                                                           , physicalId: request.physicalId
                                                                           , physicalItemId: response.webGetPhyItemInfo.physicalItemId
                                                                           , rentalItemId: response.webGetPhyItemInfo.rentalItemId
                                                                           , masterId: response.webGetPhyItemInfo.masterId
                                                                           , isICode: false
                                                                           , usersId: session.security.webUser.usersid
                                                                           , addReplace: "R"
                                                                           , qty: 1);
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task RepairItemAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RepairItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestIsNumeric(METHOD_NAME, "qty", request.qty);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "repairMode");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.webRepairItem = await this.AppData.WebRepairItemAsync(conn: conn,
                                                                         code: request.code,
                                                                         repairMode: (RwAppData.RepairMode)Enum.Parse(typeof(RwAppData.RepairMode), request.repairMode),
                                                                         usersId: session.security.webUser.usersid,
                                                                         qty: request.qty); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task CreateOutContractAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateOutContract";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.createOutContract = await this.AppData.CreateOutContractAsync(conn: conn
                                                                               , usersid: session.security.webUser.usersid
                                                                               , orderid: request.orderId
                                                                               , notes: ""); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task CreateInContractAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateInContract";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentId");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.createInContract = await this.AppData.CreateInContractAsync(conn: conn
                                                                             , orderid: request.orderId
                                                                             , dealid: request.dealId
                                                                             , departmentid: request.departmentId
                                                                             , usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task CancelContractAsync(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.ActivityType activityType;
            bool ordertranExists;
            const string METHOD_NAME = "CancelContract";

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dontCancelIfOrderTranExists");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "failSilentlyOnOwnershipErrors");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (request.dontCancelIfOrderTranExists)
                {
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "activityType");
                    activityType = (RwAppData.ActivityType)Enum.Parse(typeof(RwAppData.ActivityType), request.activityType);
                    ordertranExists = await this.AppData.OrdertranExistsAsync(conn: conn
                                                          , contractId: request.contractId
                                                          , activityType: activityType);
                    if (!ordertranExists)
                    {
                        await this.AppData.CancelContractAsync(conn: conn,
                                                 contractid: request.contractId,
                                                 usersid: session.security.webUser.usersid,
                                                 failSilentlyOnOwnershipErrors: request.failSilentlyOnOwnershipErrors);
                    }
                }
                else
                {
                    await this.AppData.CancelContractAsync(conn: conn,
                                             contractid: request.contractId,
                                             usersid: session.security.webUser.usersid,
                                             failSilentlyOnOwnershipErrors: request.failSilentlyOnOwnershipErrors);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task AddInventoryWebImageAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "AddInventoryWebImage";
            List<object> images;
            byte[] image;
            bool hasImages;
            string appimageid;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "uniqueid1");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                hasImages = FwValidate.IsPropertyDefined(request, "images");
                if (hasImages && (request.images.Length > 0))
                {
                    images = (List<object>)request.images;
                    response.appimageids = new string[images.Count];
                    for (int i = 0; i < images.Count; i++)
                    {
                        image = Convert.FromBase64String(images[i].ToString());
                        appimageid = await FwSqlData.InsertAppImageAsync(conn: conn
                            , dbConfig: this.ApplicationConfig.DatabaseSettings
                                                            , uniqueid1: request.uniqueid1
                                                            , uniqueid2: string.Empty
                                                            , uniqueid3: string.Empty
                                                            , description: string.Empty
                                                            , rectype: string.Empty
                                                            , extension: "JPG"
                                                            , image: image);
                        response.appimageids[i] = appimageid;
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task StageAllQtyItemsAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "StageAllQtyItems";
            FwJsonDataTable dtPendingList;
            int /*ordMasterNo,*/ ordMasterItemId, ordMissingQty, ordTrackedBy, ordConsignorId, ordConsignorAgreementId;
            string orderid, /*masterno,*/ masteritemid, trackedby, contractid, warehouseid, usersid, consignorid, consignoragreementid;
            decimal missingqty;
            dynamic userLocation;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.user = await this.AppData.GetUserAsync(conn: conn
                                                       , usersId: session.security.webUser.usersid);
                if (session.user.qsallowapplyallqtyitems != "T")
                {
                    throw new Exception("You do not have permission to Apply All Quantity Items");
                }
                orderid = request.orderid;
                contractid = request.contractid;
                usersid = session.security.webUser.usersid;
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn
                                                        , usersId: usersid);
                warehouseid = userLocation.warehouseId;
                dtPendingList = await this.AppData.GetStagingPendingItemsAsync(conn: conn
                                                               , orderId: orderid
                                                               , warehouseId: warehouseid
                                                               , contractId: string.Empty
                                                               , searchMode: string.Empty
                                                               , searchValue: string.Empty
                                                               , pageNo: 0
                                                               , pageSize: 0); //mv 08/10/2015 I think we don't want to pass the contract so it shows everything from the order
                                                                               //ordMasterNo            = dtPendingList.ColumnIndex["masterno"];
                ordMasterItemId = dtPendingList.ColumnIndex["masteritemid"];
                ordMissingQty = dtPendingList.ColumnIndex["missingqty"];
                ordTrackedBy = dtPendingList.ColumnIndex["trackedby"];
                ordConsignorId = dtPendingList.ColumnIndex["consignorid"];
                ordConsignorAgreementId = dtPendingList.ColumnIndex["consignoragreementid"];
                for (int i = 0; i < dtPendingList.Rows.Count; i++)
                {
                    // masterno             = dtPendingList.Rows[i][ordMasterNo].ToString();
                    masteritemid = dtPendingList.Rows[i][ordMasterItemId].ToString();
                    missingqty = FwConvert.ToDecimal(dtPendingList.Rows[i][ordMissingQty].ToString());
                    trackedby = dtPendingList.Rows[i][ordTrackedBy].ToString();
                    consignorid = dtPendingList.Rows[i][ordConsignorId].ToString();
                    consignoragreementid = dtPendingList.Rows[i][ordConsignorAgreementId].ToString();

                    if (trackedby.Equals("QUANTITY"))
                    {
                        await this.AppData.PdaStageItemAsync(conn: conn,
                                               orderid: orderid,
                                               code: string.Empty,
                                               masteritemid: masteritemid,
                                               usersid: usersid,
                                               qty: missingqty,
                                               additemtoorder: false,
                                               addcompletetoorder: false,
                                               releasefromrepair: false,
                                               unstage: false,
                                               vendorid: string.Empty,
                                               meter: 0,
                                               location: string.Empty,
                                               spaceid: string.Empty,
                                               addcontainertoorder: false,
                                               overridereservation: false,
                                               stageconsigned: false,
                                               transferrepair: false,
                                               removefromcontainer: false,
                                               contractid: contractid, // this will move item to contract, so only pass a contractid for containers
                                               ignoresuspendedin: false,
                                               consignorid: consignorid,
                                               consignoragreementid: consignoragreementid,
                                               spacetypeid: string.Empty,
                                               facilitiestypeid: string.Empty);
                    }
                }
                response.getStagingPendingItems = await this.AppData.GetStagingPendingItemsAsync(conn: conn,
                                                                                   orderId: orderid,
                                                                                   warehouseId: warehouseid,
                                                                                   contractId: string.Empty,
                                                                                   searchMode: string.Empty,
                                                                                   searchValue: string.Empty,
                                                                                   pageNo: 0,
                                                                                   pageSize: 0); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task LoadRFIDPendingAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (request.rfidmode == "STAGING")
                {
                    dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                    response.pending = await this.AppData.CheckoutExceptionRFIDAsync(conn: conn,
                                                                       orderid: request.sessionid,
                                                                       warehouseid: userLocation.warehouseId);
                }
                else if (request.rfidmode == "CHECKIN")
                {
                    response.pending = await this.AppData.CheckInExceptionRFIDAsync(conn: conn,
                                                                      sessionid: request.sessionid);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task LoadRFIDExceptionsAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.exceptions = await this.AppData.GetRFIDExceptionsAsync(conn: conn,
                                                                          sessionid: request.sessionid,
                                                                          portal: request.portal,
                                                                          usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetRFIDStatusAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.exceptions = await this.AppData.GetRFIDExceptionsAsync(conn: conn,
                                                                            sessionid: request.sessionid,
                                                                            portal: request.portal,
                                                                            usersid: session.security.webUser.usersid);
                if (request.rfidmode == "STAGING")
                {
                    dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                    response.pending = await this.AppData.CheckoutExceptionRFIDAsync(conn: conn,
                                                                       orderid: request.sessionid,
                                                                       warehouseid: userLocation.warehouseId);
                }
                else if (request.rfidmode == "CHECKIN")
                {
                    response.pending = await this.AppData.CheckInExceptionRFIDAsync(conn: conn,
                                                                      sessionid: request.sessionid);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task ProcessRFIDExceptionAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                switch ((string)request.method)
                {
                    case "Clear":
                        await this.AppData.ClearRFIDExceptionAsync(conn: conn,
                                                     sessionid: request.sessionid,
                                                     tag: request.rfid);
                        break;
                    case "AddItem":
                        response.process = await this.AppData.ProcessAddItemToOrderAsync(conn: conn,
                                                                           orderid: request.sessionid,
                                                                           tag: request.rfid,
                                                                           usersid: session.security.webUser.usersid);
                        break;
                    case "AddComplete":
                        response.process = await this.AppData.ProcessAddCompleteToOrderAsync(conn: conn,
                                                                               orderid: request.sessionid,
                                                                               tag: request.rfid,
                                                                               usersid: session.security.webUser.usersid,
                                                                               autostageacc: "F");
                        break;
                    case "OverrideConflict":
                        response.process = await this.AppData.ProcessOverrideAvailabilityConflictAsync(conn: conn,
                                                                                         orderid: request.sessionid,
                                                                                         tag: request.rfid,
                                                                                         usersid: session.security.webUser.usersid);
                        break;
                    case "TransferItemInRepair":
                        response.process = await this.AppData.ProcessTransferItemInRepairAsync(conn: conn,
                                                                                 orderid: request.sessionid,
                                                                                 tag: request.rfid,
                                                                                 usersid: session.security.webUser.usersid);
                        break;
                    case "ReleaseAndStage":
                        dynamic webstageitemresponse;
                        webstageitemresponse = await this.AppData.PdaStageItemAsync(conn: conn,
                                                                      orderid: request.sessionid,
                                                                      code: request.rfid,
                                                                      masteritemid: string.Empty,
                                                                      usersid: session.security.webUser.usersid,
                                                                      qty: 0,
                                                                      additemtoorder: false,
                                                                      addcompletetoorder: false,
                                                                      releasefromrepair: true,
                                                                      unstage: false,
                                                                      vendorid: string.Empty,
                                                                      meter: 0,
                                                                      location: string.Empty,
                                                                      spaceid: string.Empty,
                                                                      addcontainertoorder: false,
                                                                      overridereservation: false,
                                                                      stageconsigned: false,
                                                                      transferrepair: false,
                                                                      removefromcontainer: false,
                                                                      contractid: string.Empty,
                                                                      ignoresuspendedin: false,
                                                                      consignorid: string.Empty,
                                                                      consignoragreementid: string.Empty,
                                                                      spacetypeid: string.Empty,
                                                                      facilitiestypeid: string.Empty);
                        response.process = new ExpandoObject();
                        response.process.status = webstageitemresponse.status;
                        response.process.msg = webstageitemresponse.msg;
                        break;
                    case "AddOrderToSession":
                        dynamic itemstatus;
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
                                                                  trackedby: "",
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
                        await qry.ExecuteAsync();

                        response.process = new ExpandoObject();
                        response.process.status = webcheckinitem.status;
                        response.process.msg = webcheckinitem.msg;
                        break;
                }
                response.exceptions = await this.AppData.GetRFIDExceptionsAsync(conn: conn,
                                                                  sessionid: request.sessionid,
                                                                  portal: request.portal,
                                                                  usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        //public async Task ItemStatusRFID(dynamic request, dynamic response, dynamic session)
        //{
        //    response.items = await this.AppData.ItemStatusRFID(conn:      conn,
        //                                              tags:       request.tags);
        //}
        //---------------------------------------------------------------------------------------------
        public async Task RFIDClearSessionAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.RFIDClearSessionAsync(conn: conn,
                                                   sessionid: request.sessionid,
                                                   usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task RepairStatusRFIDAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.items = await this.AppData.RepairStatusRFIDAsync(conn: conn,
                                                                    tags: request.tags); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task QCStatusRFIDAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.items = await this.AppData.QCStatusRFIDAsync(conn: conn,
                                                                tags: request.tags);

                //if (request.isconditionsloaded == "F")
                //{
                //    response.rentalconditions = await this.AppData.GetRentalConditions(conn: conn);
                //} 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task TimeLogSearchAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "TimeLogSearch";
            string mode;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "value");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                mode = request.mode;

                switch (mode)
                {
                    case "ORDER":
                        response.records = await this.AppData.TimeLogSearchOrdersAsync(conn: conn,
                                                                         webusersid: session.security.webUser.webusersid,
                                                                         dealid: "",
                                                                         orderno: request.value);
                        break;
                    case "DEAL":
                        response.records = await this.AppData.TimeLogSearchDealsAsync(conn: conn,
                                                                        webusersid: session.security.webUser.webusersid,
                                                                        dealno: request.value);
                        break;
                    case "EVENT":
                        response.records = await this.AppData.TimeLogSearchEventsAsync(conn: conn,
                                                                         webusersid: session.security.webUser.webusersid,
                                                                         eventno: request.value);
                        break;
                    case "PROJECT":
                        response.records = await this.AppData.TimeLogSearchProjectsAsync(conn: conn,
                                                                           orderno: request.value);
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task TimeLogSubmitAsync(dynamic request, dynamic response, dynamic session)
        {
            string mode;
            mode = request.mode;

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if ((mode == "ORDER") || (mode == "DEAL") || (mode == "EVENT"))
                {
                    response.status = await this.AppData.TimeLogSubmitAsync(conn: conn,
                                                              webusersid: session.security.webUser.webusersid,
                                                              thedate: request.date,
                                                              orderid: request.orderid,
                                                              masteritemid: request.masteritemid,
                                                              starttime: FwConvert.ToShortTime24(FwConvert.ToDateTime(request.starttime)),
                                                              stoptime: FwConvert.ToShortTime24(FwConvert.ToDateTime(request.stoptime)),
                                                              break1starttime: (request.break1starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break1starttime)) : "",
                                                              break1stoptime: (request.break1stoptime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break1stoptime)) : "",
                                                              break2starttime: (request.break2starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break2starttime)) : "",
                                                              break2stoptime: (request.break2stoptime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break2stoptime)) : "",
                                                              break3starttime: (request.break3starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break3starttime)) : "",
                                                              break3stoptime: (request.break3stoptime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break3stoptime)) : "",
                                                              notes: request.notes,
                                                              inputbywebusersid: session.security.webUser.webusersid);
                }
                else if (mode == "PROJECT")
                {
                    if (!request.update)
                    {
                        response.status = await this.AppData.InsertUserTimeAsync(conn: conn,
                                                                   usersid: session.security.webUser.usersid,
                                                                   workdate: request.date,
                                                                   orderid: (FwValidate.IsPropertyDefined(request.timelogdata.selectedrecord, "orderid") ? request.timelogdata.selectedrecord.orderid : ""),
                                                                   description: request.description,
                                                                   workhours: request.hours);
                    }
                    else if (request.update)
                    {
                        response.status = await this.AppData.UpdateUserTimeAsync(conn: conn,
                                                                   usertimeid: request.recorddata.usertimeid,
                                                                   internalchar: request.recorddata.internalchar,
                                                                   usersid: session.security.webUser.usersid,
                                                                   workdate: request.date,
                                                                   orderid: request.recorddata.orderid,
                                                                   description: request.description,
                                                                   workhours: request.hours);
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task TimeLogViewEntriesAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "TimeLogDropDownData";
            string mode;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "selectedrecord");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                mode = request.mode;

                switch (mode)
                {
                    case "EVENT":
                    case "DEAL":
                    case "ORDER":
                        response.entries = await await this.AppData.GetCrewTimeEntriesAsync(conn: conn,
                                                                        webusersid: session.security.webUser.webusersid,
                                                                        orderid: ((mode == "ORDER") ? request.selectedrecord.orderid : ""),
                                                                        dealid: ((mode == "DEAL") ? request.selectedrecord.dealid : ""),
                                                                        eventid: ((mode == "EVENT") ? request.selectedrecord.eventid : ""));
                        break;
                    case "PROJECT":
                        response.entries = await await this.AppData.GetUserTimeEntriesAsync(conn: conn,
                                                                        usersid: session.security.webUser.usersid,
                                                                        orderid: (FwValidate.IsPropertyDefined(request.selectedrecord, "orderid") ? request.selectedrecord.orderid : ""));
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetOrderItemsToSubAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.itemstosub = await await this.AppData.GetOrderItemsToSubAsync(conn: conn,
                                                                           usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetOrderCompletesToSubAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.completestosub = await await this.AppData.GetOrderCompletesToSubAsync(conn: conn,
                                                                                   usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task SubstituteAtStagingAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.completestosub = await await this.AppData.SubstituteAtStagingAsync(conn: conn,
                                                                                orderid: request.orderid,
                                                                                reducemasteritemid: request.reducemasteritemid,
                                                                                replacewithmasterid: request.replacewithmasterid,
                                                                                usersid: session.security.webUser.usersid,
                                                                                qtytosubstitute: request.qtytosubstitute,
                                                                                substitutecomplete: request.substitutecomplete); 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetBarcodeFromRfidAsync(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.barcode = await await this.AppData.GetBarcodeFromRfidAsync(conn: conn,
                                                                        rfid: request.rfid); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
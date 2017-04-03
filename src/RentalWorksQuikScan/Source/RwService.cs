using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using RentalWorksQuikScan.Modules;
using System;
using System.Dynamic;

namespace RentalWorksQuikScan.Source
{
    public class RwService
    {
        //---------------------------------------------------------------------------------------------
        public static void GetAuthToken(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetAuthToken";
            bool hasWebUser;
            AccountService.Current.GetAuthToken(FwSqlConnection.RentalWorks, request, response, session);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");
            
            response.checkDatabaseVersionMessage = FwSqlData.CheckDatabaseVersion(conn:                   FwSqlConnection.RentalWorks, 
                                                                                  requireddbversion:      Version.RequiredDatabaseVersion, 
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
                        session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                                       , usersId: session.security.webUser.usersid);
                        response.user = new ExpandoObject();
                        response.user.enablecreatecontract    = session.user.enablecreatecontract;
                        response.user.qsallowapplyallqtyitems = session.user.qsallowapplyallqtyitems;
                        if (string.IsNullOrEmpty(session.user.warehouseid)) throw new Exception("A warehouse needs to be set on your user account before you can login to this system.");
                        response.barcodeskipprefixes = RwAppData.GetBarcodeSkipPrefixes(FwSqlConnection.RentalWorks, session.user.warehouseid);
                    }
                    if ((session.security.webUser.usertype == "CONTACT") && (!string.IsNullOrEmpty(session.security.webUser.contactid)))
                    {
                        session.contact = new ExpandoObject();
                        session.contact = RwAppData.GetContact(conn:      FwSqlConnection.RentalWorks
                                                             , contactId: session.security.webUser.contactid);
                    }
                }
                GetSite(request, response, session);
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void AuthPassword(dynamic request, dynamic response, dynamic session)
        {
            bool isValid = false;
            const string METHOD_NAME = "AuthPassword";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "email");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "password");
            isValid = AccountService.Current.AuthenticatePassword(FwSqlConnection.RentalWorks, request, response, session);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetSite(dynamic request, dynamic response, dynamic session)
        {
            FwApplicationConfig_Site site = FwApplicationConfig.CurrentSite;
            
            response.site = new ExpandoObject();
            response.site.name = site.Name;
        }
        //---------------------------------------------------------------------------------------------
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
        }
        //---------------------------------------------------------------------------------------------
        public static void CreateContract(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateContract";
            string usersid, contracttype, contractid, orderid, responsiblepersonid, uniqueid1, uniqueid2, uniqueid3, description, rectype, extension, base64image;
            FwSqlConnection conn;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "responsiblePersonId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "signatureImage");
            conn                = FwSqlConnection.RentalWorks;
            usersid             = session.security.webUser.usersid;
            contracttype        = request.contractType;
            contractid          = request.contractId;
            orderid             = request.orderId;
            responsiblepersonid = request.responsiblePersonId;
            
            // Create the contract
            response.webCreateContract = RwAppData.WebCreateContract(conn, usersid, contracttype, contractid, orderid, responsiblepersonid);
            
            uniqueid1   = response.webCreateContract.contractId;
            uniqueid2   = string.Empty;
            uniqueid3   = string.Empty;
            description = "CONTRACT_SIGNATURE";
            rectype     = string.Empty;
            extension   = "JPG";
            base64image = request.signatureImage;
            
            // insert the signature image
            FwSqlData.InsertAppImage(conn, uniqueid1, uniqueid2, uniqueid3, description, rectype, extension, base64image);
        }
        //---------------------------------------------------------------------------------------------
        public static void SelectDeal(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectDeal";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealNo");
            response.webSelectDeal = RwAppData.WebSelectDeal(conn:    FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid
                                                           , dealNo:  request.dealNo);
        }
        //---------------------------------------------------------------------------------------------
        public static void CreateNewInContractAndSuspend(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateNewInContractAndSuspend";    
            FwSqlConnection conn;
            string usersid, orderid, dealid, departmentid;
            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentid");
            orderid      = request.orderid;
            dealid       = request.dealid;
            departmentid = request.departmentid;
            conn         = FwSqlConnection.RentalWorks;
            usersid      = session.security.webUser.usersid;
            response.contract = RwAppData.CreateNewInContractAndSuspend(conn, orderid, dealid, departmentid, usersid);    
        }
        //---------------------------------------------------------------------------------------------
        public static void SelectPhyInv(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectPhyInv";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "phyNo");
            response.webSelectPhyInv = RwAppData.WebSelectPhyInv(conn:    FwSqlConnection.RentalWorks
                                                               , usersId: session.security.webUser.usersid
                                                               , phyNo:   request.phyNo);
        }
        //---------------------------------------------------------------------------------------------
        public static void SelectRepairOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectRepairOrder";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            response.webSelectRepairOrder = RwAppData.WebRepairItem(conn:       FwSqlConnection.RentalWorks
                                                                  , code:       request.code
                                                                  , repairMode: RwAppData.RepairMode.Select
                                                                  , usersId:    session.security.webUser.usersid
                                                                  , qty:        0);
        }
        //---------------------------------------------------------------------------------------------
        public static void SelectSession(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectSession";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "sessionNo");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            response.webSelectSession = RwAppData.WebSelectSession(conn:       FwSqlConnection.RentalWorks
                                                                 , usersId:    session.security.webUser.usersid
                                                                 , sessionNo:  request.sessionNo
                                                                 , moduleType: (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType));
        }
        //---------------------------------------------------------------------------------------------
        public static void PdaStageItem(dynamic request, dynamic response, dynamic session)
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
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "spaceid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "addcontainertoorder");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "overridereservation");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "stageconsigned");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "transferrepair");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "removefromcontainer");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "ignoresuspendedin");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "consignoragreementid");
            session.userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            response.webStageItem = RwAppData.PdaStageItem(conn:                 FwSqlConnection.RentalWorks,
                                                           orderid:              request.orderid,
                                                           code:                 request.code,
                                                           masteritemid:         request.masteritemid,
                                                           usersid:              session.security.webUser.usersid,
                                                           qty:                  request.qty,
                                                           additemtoorder:       request.additemtoorder,
                                                           addcompletetoorder:   request.addcompletetoorder,
                                                           releasefromrepair:    request.releasefromrepair,
                                                           unstage:              request.unstage,
                                                           vendorid:             request.vendorid, 
                                                           meter:                request.meter, 
                                                           location:             request.location, 
                                                           spaceid:              request.spaceid, 
                                                           addcontainertoorder:  request.addcontainertoorder, 
                                                           overridereservation:  request.overridereservation, 
                                                           stageconsigned:       request.stageconsigned, 
                                                           transferrepair:       request.transferrepair, 
                                                           removefromcontainer:  request.removefromcontainer, 
                                                           contractid:           request.contractid, 
                                                           ignoresuspendedin:    request.ignoresuspendedin, 
                                                           consignorid:          request.consignorid, 
                                                           consignoragreementid: request.consignoragreementid);
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
                session.getStagingPendingItem = RwAppData.FuncCheckoutException(conn:         FwSqlConnection.RentalWorks,
                                                                                orderId:      request.orderid,
                                                                                warehouseId:  session.userLocation.warehouseId,
                                                                                contractId:   request.contractid,
                                                                                masterItemId: masteritemid);
                response.webStageItem.masterId     = session.getStagingPendingItem.masterId;
                response.webStageItem.masterItemId = session.getStagingPendingItem.masterItemId;
                response.webStageItem.masterNo     = session.getStagingPendingItem.masterNo;
                response.webStageItem.description  = session.getStagingPendingItem.description;
                response.webStageItem.qtyOrdered   = session.getStagingPendingItem.qtyOrdered;
                response.webStageItem.qtySub       = session.getStagingPendingItem.qtySub;
                response.webStageItem.qtyStaged    = session.getStagingPendingItem.qtyStagedAndOut;
                response.webStageItem.qtyOut       = session.getStagingPendingItem.qtyOut;
                if (request.contractid == string.Empty)
                {
                    response.webStageItem.qtyIn        = session.getStagingPendingItem.qtyIn;
                }
                else
                {
                    response.webStageItem.qtyIn        = session.getStagingPendingItem.qtyStagedAndOut;
                }
                response.webStageItem.qtyRemaining = session.getStagingPendingItem.missingQty;
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void UnstageItem(dynamic request, dynamic response, dynamic session)
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

            usersid       = session.security.webUser.usersid;
            contractid    = request.contractid;
            vendorid      = request.vendorid;
            masteritemid  = request.masteritemid;
            qty           = request.qty;
            orderid       = request.orderid;
            session.userLocation   = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, usersid);
            warehouseid  = session.userLocation.warehouseId;
            response.unstageItem           = RwAppData.AdvancedMoveMasterItemId(FwSqlConnection.RentalWorks, contractid, vendorid, masteritemid, qty, usersid, 4);
            response.getStagingStagedItems = Staging.funccheckedout(FwSqlConnection.RentalWorks, contractid);
        }
        //---------------------------------------------------------------------------------------------
        //public static void GetPhyItemInfo(dynamic request, dynamic response, dynamic session)
        //{
        //    const string METHOD_NAME = "GetPhyItemInfo";
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalId");
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
        //    response.webGetPhyItemInfo = RwAppData.WebGetPhyItemInfo(physicalId: request.physicalId
        //                                                           , code:       request.code);
        //}
        //---------------------------------------------------------------------------------------------
        public static void PhyCountItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "PhyCountItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "physicalId");
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
                response.webPhyCountItem = RwAppData.WebPhyCountItem(conn:           FwSqlConnection.RentalWorks
                                                                   , physicalId:     request.physicalId
                                                                   , physicalItemId: request.physicalItemId
                                                                   , rentalItemId:   request.rentalItemId
                                                                   , masterId:       request.masterId
                                                                   , isICode:        true
                                                                   , usersId:        session.security.webUser.usersid
                                                                   , addReplace:     request.addReplace
                                                                   , qty:            request.qty);
            } 
            else 
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
                response.webGetPhyItemInfo = RwAppData.WebGetPhyItemInfo(conn:       FwSqlConnection.RentalWorks
                                                                       , usersId:    session.security.webUser.usersid
                                                                       , physicalId: request.physicalId
                                                                       , code:       request.code);
                if ((response.webGetPhyItemInfo.itemType == "BarCoded") && (response.webGetPhyItemInfo.status == 0))
                {
                    response.webPhyCountItem = RwAppData.WebPhyCountItem(conn:           FwSqlConnection.RentalWorks
                                                                       , physicalId:     request.physicalId
                                                                       , physicalItemId: response.webGetPhyItemInfo.physicalItemId
                                                                       , rentalItemId:   response.webGetPhyItemInfo.rentalItemId
                                                                       , masterId:       response.webGetPhyItemInfo.masterId
                                                                       , isICode:        false
                                                                       , usersId:        session.security.webUser.usersid
                                                                       , addReplace:     "R"
                                                                       , qty:            1);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void RepairItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RepairItem";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestIsNumeric(METHOD_NAME, "qty", request.qty);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "repairMode");
            response.webRepairItem = RwAppData.WebRepairItem(conn:       FwSqlConnection.RentalWorks
                                                           , code:       request.code
                                                           , repairMode: (RwAppData.RepairMode)Enum.Parse(typeof(RwAppData.RepairMode), request.repairMode)
                                                           , usersId:    session.security.webUser.usersid
                                                           , qty:        request.qty);
        }
        //---------------------------------------------------------------------------------------------
        public static void CompleteQCItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CompleteQCItem";
            string[] images;
            bool hasRentalItemId , hasRentalItemQCId, hasConditionId, hasNote, hasImages, autoreleaseqc, hasWebCompleteQCItem;
            string usersid, code, rentalitemid, rentalitemqcid, action, conditionid, note,
                uniqueid1, uniqueid2, uniqueid3, description, rectype, extension;
            FwSqlConnection conn;
            byte[] image;
            dynamic webCompleteQCItem;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");

            usersid       = session.security.webUser.usersid;
            conn          = FwSqlConnection.RentalWorks;
            code          = request.code;
            action        = request.action;
            autoreleaseqc = true; // MV 2015-07-07 it used to not autocomplete the QC when you scanned the barcode, which differed from the application and RWMobile.  Only leaving this option here incase someone asks for this back.  If this is still here after a year please remove the option.

            if (request.isconditionsloaded == "F")
            {
                response.rentalconditions = RwAppData.GetRentalConditions(conn: FwSqlConnection.RentalWorks);
            }

            if (action == "scanbarcode")
            {
                if (autoreleaseqc)
                {
                    response.webCompleteQCItem      = RwAppData.WebCompleteQCItem(conn, code, usersid);
                    rentalitemid                    = response.webCompleteQCItem.rentalitemid;
                    response.getRentalItemCondition = RwAppData.GetRentalItemCondition(conn, rentalitemid);
                }
                response.webGetItemStatus = RwAppData.WebGetItemStatus(conn, usersid, code);
            }
            else if (action == "completeqc")
            {
                hasWebCompleteQCItem = FwValidate.IsPropertyDefined(request, "webCompleteQCItem");
                if (hasWebCompleteQCItem)
                {
                    webCompleteQCItem = request.webCompleteQCItem;
                }
                else
                {
                    webCompleteQCItem = RwAppData.WebCompleteQCItem(conn, code, usersid);
                }
                rentalitemid      = webCompleteQCItem.rentalitemid;
                rentalitemqcid    = webCompleteQCItem.rentalitemqcid;
                conditionid       = request.conditionid;
                note              = request.note;
                hasRentalItemId   = FwValidate.IsPropertyDefined(webCompleteQCItem, "rentalitemid");
                hasRentalItemQCId = FwValidate.IsPropertyDefined(webCompleteQCItem, "rentalitemqcid");
                hasConditionId    = FwValidate.IsPropertyDefined(request, "conditionid") && !string.IsNullOrEmpty(request.conditionid);
                hasNote           = FwValidate.IsPropertyDefined(request, "note")        && !string.IsNullOrEmpty(request.note);
                hasImages         = FwValidate.IsPropertyDefined(request, "images");
                if (hasRentalItemId && hasRentalItemQCId && (hasConditionId || hasNote))
                {
                    RwAppData.UpdateRentalItemQC(conn, rentalitemid, rentalitemqcid, conditionid, note);
                }
                if (hasRentalItemId && hasRentalItemQCId && hasImages && (request.images.Length > 0))
                {
                    images = (string[])request.images;
                    uniqueid1         = rentalitemqcid;
                    uniqueid2         = string.Empty;
                    uniqueid3         = string.Empty;
                    description       = string.Empty;
                    rectype           = string.Empty;
                    extension         = "JPG";
                    for (int i = 0; i < images.Length; i++)
                    {
                        image = Convert.FromBase64String(images[i]);
                        FwSqlData.InsertAppImage(conn, uniqueid1, uniqueid2, uniqueid3, description, rectype, extension, image);
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void GetRentalItemCondition(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetRentalItemCondition";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            response.getRentalItemCondition = RwAppData.GetRentalItemCondition(conn:         FwSqlConnection.RentalWorks
                                                                             , rentalItemId: request.rentalitemid);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetRentalConditions(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetRentalConditions";
            response.getRentalConditions = RwAppData.GetRentalConditions(conn: FwSqlConnection.RentalWorks);
        }
        //---------------------------------------------------------------------------------------------
        public static void UpdateRentalItemQC(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "UpdateRentalItemQC";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemqcid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "conditionid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "note");
            response.updateRentalItemQC = RwAppData.UpdateRentalItemQC(conn:            FwSqlConnection.RentalWorks
                                                                     , rentalItemId:    request.rentalitemid
                                                                     , rentalItemQCId:  request.rentalitemqcid
                                                                     , conditionId:     request.conditionid
                                                                     , note:            request.note);
        }
        
        //---------------------------------------------------------------------------------------------
        public static void CreateOutContract(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateOutContract";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            response.createOutContract = RwAppData.CreateOutContract(conn:    FwSqlConnection.RentalWorks
                                                                   , usersid: session.security.webUser.usersid
                                                                   , orderid: request.orderId
                                                                   , notes:   "");
        }
        //---------------------------------------------------------------------------------------------
        public static void CreateInContract(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateInContract";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentId");
            response.createInContract = RwAppData.CreateInContract(conn:         FwSqlConnection.RentalWorks
                                                                 , orderid:      request.orderId
                                                                 , dealid:       request.dealId
                                                                 , departmentid: request.departmentId
                                                                 , usersid:      session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetCheckInPendingList(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetCheckInPendingList";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            response.getCheckInPendingList = RwAppData.GetCheckInPendingList(conn:       FwSqlConnection.RentalWorks
                                                                           , contractid: request.contractId);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetCheckInSessionInList(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetCheckInSessionInList";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            response.getCheckInSessionInList = RwAppData.GetCheckInSessionInList(conn:       FwSqlConnection.RentalWorks
                                                                               , contractid: request.contractId);
        }
        //---------------------------------------------------------------------------------------------
        public static void CancelContract(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.ActivityType activityType;
            bool ordertranExists;
            const string METHOD_NAME = "CancelContract";

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dontCancelIfOrderTranExists");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "failSilentlyOnOwnershipErrors");
            if (request.dontCancelIfOrderTranExists)
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "activityType");
                activityType = (RwAppData.ActivityType)Enum.Parse(typeof(RwAppData.ActivityType), request.activityType);
                ordertranExists = RwAppData.OrdertranExists(conn:         FwSqlConnection.RentalWorks
                                                      , contractId:   request.contractId
                                                      , activityType: activityType);
                if (!ordertranExists)
                {
                    RwAppData.CancelContract(conn:       FwSqlConnection.RentalWorks,
                                             contractid: request.contractId,
                                             usersid:    session.security.webUser.usersid,
                                             failSilentlyOnOwnershipErrors: request.failSilentlyOnOwnershipErrors);
                }
            }
            else
            {
                RwAppData.CancelContract(conn:       FwSqlConnection.RentalWorks,
                                         contractid: request.contractId,
                                         usersid:    session.security.webUser.usersid,
                                         failSilentlyOnOwnershipErrors: request.failSilentlyOnOwnershipErrors);
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void AddInventoryWebImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "AddInventoryWebImage";
            string[] images;
            byte[] image;
            bool hasImages;
            string appimageid;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "uniqueid1");
            hasImages = FwValidate.IsPropertyDefined(request, "images");
            if (hasImages && (request.images.Length > 0))
            {
                images = (string[])request.images;
                response.appimageids = new string[images.Length];
                for (int i = 0; i < images.Length; i++)
                {
                    image = Convert.FromBase64String(images[i]);
                    appimageid = FwSqlData.InsertAppImage(conn:        FwSqlConnection.RentalWorks
                                                        , uniqueid1:   request.uniqueid1
                                                        , uniqueid2:   string.Empty
                                                        , uniqueid3:   string.Empty
                                                        , description: string.Empty
                                                        , rectype:     string.Empty
                                                        , extension:   "JPG"
                                                        , image:       image);
                    response.appimageids[i] = appimageid;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void CheckInAllQtyItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CheckInAllQtyItems";
            FwJsonDataTable dtPendingList;
            int ordTrackedBy, ordSubByQuantity, ordOrderId, ordVendorId, ordMasterItemId, ordMasterId, ordParentId, ordDescription, ordQtyStillOut;
            string trackedby, subbyquantity, orderid, vendorid, masteritemid, masterid, parentid, description;
            decimal qtystillout;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                           , usersId: session.security.webUser.usersid);
            if (session.user.qsallowapplyallqtyitems != "T")
            {
                throw new Exception("You do not have permission to Apply All Quantity Items");
            }
            dtPendingList = RwAppData.GetCheckInPendingList(conn:       FwSqlConnection.RentalWorks
                                                          , contractid: request.contractId);
            ordTrackedBy     = dtPendingList.ColumnIndex["trackedby"];
            ordSubByQuantity = dtPendingList.ColumnIndex["subbyquantity"];
            ordOrderId       = dtPendingList.ColumnIndex["orderid"];
            ordVendorId      = dtPendingList.ColumnIndex["vendorid"];
            ordMasterItemId  = dtPendingList.ColumnIndex["masteritemid"];
            ordMasterId      = dtPendingList.ColumnIndex["masterid"];
            ordParentId      = dtPendingList.ColumnIndex["parentid"];
            ordDescription   = dtPendingList.ColumnIndex["description"];
            ordQtyStillOut   = dtPendingList.ColumnIndex["qtystillout"];
            for(int i = 0; i < dtPendingList.Rows.Count; i++)
            {
                trackedby     = dtPendingList.Rows[i][ordTrackedBy].ToString();
                subbyquantity = dtPendingList.Rows[i][ordSubByQuantity].ToString();
                orderid       = dtPendingList.Rows[i][ordOrderId].ToString();
                vendorid      = dtPendingList.Rows[i][ordVendorId].ToString();
                masteritemid  = dtPendingList.Rows[i][ordMasterItemId].ToString();
                masterid      = dtPendingList.Rows[i][ordMasterId].ToString();
                parentid      = dtPendingList.Rows[i][ordParentId].ToString();
                description   = dtPendingList.Rows[i][ordDescription].ToString();
                qtystillout   = FwConvert.ToDecimal(dtPendingList.Rows[i][ordQtyStillOut]);
                if ((trackedby.Equals("QUANTITY") || subbyquantity.Equals("True")) && (qtystillout > 0))
                {
                    RwAppData.CheckInQty(conn:         FwSqlConnection.RentalWorks
                                       , contractId:   request.contractId
                                       , orderId:      orderid
                                       , usersId:      session.security.webUser.usersid
                                       , vendorId:     vendorid
                                       , consignorId:  string.Empty
                                       , masterItemId: masteritemid
                                       , masterId:     masterid
                                       , parentId:     parentid
                                       , description:  description
                                       , orderTranId:  0
                                       , internalChar: string.Empty
                                       , aisle:        string.Empty
                                       , shelf:        string.Empty
                                       , qty:          qtystillout
                                       , containeritemid:        string.Empty
                                       , containeroutcontractid: string.Empty);
                }
            }
            response.getCheckInPendingList = RwAppData.GetCheckInPendingList(conn:       FwSqlConnection.RentalWorks
                                                                           , contractid: request.contractId);
        }
        //---------------------------------------------------------------------------------------------
        public static void StageAllQtyItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "StageAllQtyItems";
            FwJsonDataTable dtPendingList;
            int /*ordMasterNo,*/ ordMasterItemId, ordMissingQty, ordTrackedBy;
            string orderid, /*masterno,*/ masteritemid, trackedby, contractid, warehouseid, usersid;
            decimal missingqty;
            dynamic userLocation;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                           , usersId: session.security.webUser.usersid);
            if (session.user.qsallowapplyallqtyitems != "T")
            {
                throw new Exception("You do not have permission to Apply All Quantity Items");
            }
            orderid       = request.orderid;
            contractid    = request.contractid;
            usersid       = session.security.webUser.usersid;
            userLocation  = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                    , usersId: usersid);
            warehouseid   = userLocation.warehouseId;
            dtPendingList = RwAppData.GetStagingPendingItems(conn:        FwSqlConnection.RentalWorks
                                                           , orderId:     orderid
                                                           , warehouseId: warehouseid
                                                           , contractId:  string.Empty); //mv 08/10/2015 I think we don't want to pass the contract so it shows everything from the order
            //ordMasterNo    = dtPendingList.ColumnIndex["masterno"];
            ordMasterItemId = dtPendingList.ColumnIndex["masteritemid"];
            ordMissingQty   = dtPendingList.ColumnIndex["missingqty"];
            ordTrackedBy    = dtPendingList.ColumnIndex["trackedby"];
            for(int i = 0; i < dtPendingList.Rows.Count; i++)
            {
               // masterno     = dtPendingList.Rows[i][ordMasterNo].ToString();
                masteritemid = dtPendingList.Rows[i][ordMasterItemId].ToString();
                missingqty   = FwConvert.ToDecimal(dtPendingList.Rows[i][ordMissingQty].ToString());
                trackedby    = dtPendingList.Rows[i][ordTrackedBy].ToString();
                if (trackedby.Equals("QUANTITY"))
                {
                    RwAppData.PdaStageItem(conn:                 FwSqlConnection.RentalWorks,
                                           orderid:              orderid,
                                           code:                 string.Empty,
                                           masteritemid:         masteritemid,
                                           usersid:              usersid,
                                           qty:                  missingqty,
                                           additemtoorder:       false,
                                           addcompletetoorder:   false,
                                           releasefromrepair:    false,
                                           unstage:              false,
                                           vendorid:             string.Empty, 
                                           meter:                0, 
                                           location:             string.Empty, 
                                           spaceid:              string.Empty, 
                                           addcontainertoorder:  false, 
                                           overridereservation:  false, 
                                           stageconsigned:       false, 
                                           transferrepair:       false, 
                                           removefromcontainer:  false, 
                                           contractid:           contractid, // this will move item to contract, so only pass a contractid for containers
                                           ignoresuspendedin:    false, 
                                           consignorid:          string.Empty, 
                                           consignoragreementid: string.Empty);
                }
            }
            response.getStagingPendingItems = RwAppData.GetStagingPendingItems(conn:        FwSqlConnection.RentalWorks,
                                                                               orderId:     orderid,
                                                                               warehouseId: warehouseid,
                                                                               contractId:  string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetQuoteItems(dynamic request, dynamic response, dynamic session)
        {
            response.getQuoteItems = RwAppData.QSMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                            orderid:      request.orderid            ,
                                                            masteritemid: string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        public static void AddItem(dynamic request, dynamic response, dynamic session)
        {
            response.insert = RwAppData.QSInsertMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                           orderid:      request.orderid            ,
                                                           barcode:      request.masterno           ,
                                                           qtyordered:   request.qty                ,
                                                           webusersid:   session.security.webUser.webusersid,
                                                           masteritemid: "");

            response.getItemInfo = RwAppData.QSMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                          orderid:      request.orderid,
                                                          masteritemid: response.insert.masteritemid);

        }
        //---------------------------------------------------------------------------------------------
        public static void UpdateItemQty(dynamic request, dynamic response, dynamic session)
        {
            response.update = RwAppData.QSInsertMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                           orderid:      request.orderid            ,
                                                           barcode:      request.masterno           ,
                                                           qtyordered:   request.qty                ,
                                                           webusersid:   session.security.webUser.webusersid,
                                                           masteritemid: request.masteritemid);

            response.getItemInfo = RwAppData.QSMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                          orderid:      request.orderid,
                                                          masteritemid: response.update.masteritemid);
        }
        //---------------------------------------------------------------------------------------------
        public static void DeleteItem(dynamic request, dynamic response, dynamic session)
        {
            response.deleteitem = RwAppData.QSDeleteMasterItem(conn:         FwSqlConnection.RentalWorks,
                                                               orderid:      request.orderid,
                                                               masteritemid: request.masteritemid,
                                                               rentalitemid: request.rentalitemid,
                                                               webusersid:   session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void QSSubmitQuote(dynamic request, dynamic response, dynamic session)
        {
            response.submit = RwAppData.QSSubmitQuote(conn:       FwSqlConnection.RentalWorks,
                                                      orderid:    request.orderid,
                                                      webusersid: session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void QSCancelQuote(dynamic request, dynamic response, dynamic session)
        {
            response.cancel = RwAppData.QSCancelQuote(conn:       FwSqlConnection.RentalWorks,
                                                      orderid:    request.orderid,
                                                      webusersid: session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
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
                                         rfidmode:  request.rfidmode);

            response.processed = RwAppData.ScannedTag(conn:      FwSqlConnection.RentalWorks,
                                                      sessionid: request.sessionid,
                                                      usersid:   session.security.webUser.usersid,
                                                      portal:    request.portal,
                                                      batchid:   batchid);


            response.exceptions = RwAppData.GetRFIDExceptions(conn:      FwSqlConnection.RentalWorks,
                                                                sessionid: request.sessionid,
                                                                portal:    request.portal,
                                                                usersid:   session.security.webUser.usersid);

            if (request.rfidmode == "STAGING")
            {

                response.pending = RwAppData.CheckoutExceptionRFID(conn:        FwSqlConnection.RentalWorks,
                                                                   orderid:     request.sessionid,
                                                                   warehouseid: userLocation.warehouseId);
            }
            else if (request.rfidmode == "CHECKIN")
            {
                response.pending = RwAppData.CheckInExceptionRFID(conn:      FwSqlConnection.RentalWorks,
                                                                  sessionid: request.sessionid);

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
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void LoadRFIDPending(dynamic request, dynamic response, dynamic session)
        {
            if (request.rfidmode == "STAGING")
            {
                dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
                response.pending = RwAppData.CheckoutExceptionRFID(conn:        FwSqlConnection.RentalWorks,
                                                                   orderid:     request.sessionid,
                                                                   warehouseid: userLocation.warehouseId);
            }
            else if (request.rfidmode == "CHECKIN")
            {
                response.pending = RwAppData.CheckInExceptionRFID(conn:      FwSqlConnection.RentalWorks,
                                                                  sessionid: request.sessionid);
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void LoadRFIDExceptions(dynamic request, dynamic response, dynamic session)
        {
            response.exceptions = RwAppData.GetRFIDExceptions(conn:      FwSqlConnection.RentalWorks,
                                                              sessionid: request.sessionid,
                                                              portal:    request.portal,
                                                              usersid:   session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetRFIDStatus(dynamic request, dynamic response, dynamic session)
        {
            response.exceptions = RwAppData.GetRFIDExceptions(conn:      FwSqlConnection.RentalWorks,
                                                                sessionid: request.sessionid,
                                                                portal:    request.portal,
                                                                usersid:   session.security.webUser.usersid);
            if (request.rfidmode == "STAGING")
            {
                dynamic userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
                response.pending = RwAppData.CheckoutExceptionRFID(conn:        FwSqlConnection.RentalWorks,
                                                                   orderid:     request.sessionid,
                                                                   warehouseid: userLocation.warehouseId);
            }
            else if (request.rfidmode == "CHECKIN")
            {
                response.pending = RwAppData.CheckInExceptionRFID(conn:      FwSqlConnection.RentalWorks,
                                                                  sessionid: request.sessionid);
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void ProcessRFIDException(dynamic request, dynamic response, dynamic session)
        {
            switch ((string)request.method)
            {
                case "Clear":
                    RwAppData.ClearRFIDException(conn:      FwSqlConnection.RentalWorks,
                                                 sessionid: request.sessionid,
                                                 tag:       request.rfid);
                    break;
                case "AddItem":
                    response.process = RwAppData.ProcessAddItemToOrder(conn:    FwSqlConnection.RentalWorks,
                                                                       orderid: request.sessionid,
                                                                       tag:     request.rfid,
                                                                       usersid: session.security.webUser.usersid);
                    break;
                case "AddComplete":
                    response.process = RwAppData.ProcessAddCompleteToOrder(conn:         FwSqlConnection.RentalWorks,
                                                                           orderid:      request.sessionid,
                                                                           tag:          request.rfid,
                                                                           usersid:      session.security.webUser.usersid,
                                                                           autostageacc: "F");
                    break;
                case "OverrideConflict":
                    response.process = RwAppData.ProcessOverrideAvailabilityConflict(conn:    FwSqlConnection.RentalWorks,
                                                                                     orderid: request.sessionid,
                                                                                     tag:     request.rfid,
                                                                                     usersid: session.security.webUser.usersid);
                    break;
                case "TransferItemInRepair":
                    response.process = RwAppData.ProcessTransferItemInRepair(conn: FwSqlConnection.RentalWorks,
                                                                             orderid: request.sessionid,
                                                                             tag:     request.rfid,
                                                                             usersid: session.security.webUser.usersid);
                    break;
                case "ReleaseAndStage":
                    dynamic webstageitemresponse;
                    webstageitemresponse = RwAppData.PdaStageItem(conn:                 FwSqlConnection.RentalWorks,
                                                                  orderid:              request.sessionid,
                                                                  code:                 request.rfid,
                                                                  masteritemid:         string.Empty,
                                                                  usersid:              session.security.webUser.usersid,
                                                                  qty:                  0,
                                                                  additemtoorder:       false,
                                                                  addcompletetoorder:   false,
                                                                  releasefromrepair:    true,
                                                                  unstage:              false,
                                                                  vendorid:             string.Empty, 
                                                                  meter:                0, 
                                                                  location:             string.Empty, 
                                                                  spaceid:              string.Empty, 
                                                                  addcontainertoorder:  false, 
                                                                  overridereservation:  false, 
                                                                  stageconsigned:       false, 
                                                                  transferrepair:       false, 
                                                                  removefromcontainer:  false, 
                                                                  contractid:           string.Empty,
                                                                  ignoresuspendedin:    false, 
                                                                  consignorid:          string.Empty, 
                                                                  consignoragreementid: string.Empty);
                    response.process = new ExpandoObject();
                    response.process.status = webstageitemresponse.status;
                    response.process.msg    = webstageitemresponse.msg;
                    break;
                case "AddOrderToSession":
                    dynamic itemstatus;
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
                    break;
            }
            response.exceptions = RwAppData.GetRFIDExceptions(conn:      FwSqlConnection.RentalWorks,
                                                              sessionid: request.sessionid,
                                                              portal:    request.portal,
                                                              usersid:   session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        //public static void ItemStatusRFID(dynamic request, dynamic response, dynamic session)
        //{
        //    response.items = RwAppData.ItemStatusRFID(conn:      FwSqlConnection.RentalWorks,
        //                                              tags:       request.tags);
        //}
        //---------------------------------------------------------------------------------------------
        public static void RFIDClearSession(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.RFIDClearSession(conn:      FwSqlConnection.RentalWorks,
                                       sessionid: request.sessionid,
                                       usersid:   session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void RepairStatusRFID(dynamic request, dynamic response, dynamic session)
        {
            response.items = RwAppData.RepairStatusRFID(conn: FwSqlConnection.RentalWorks,
                                                        tags: request.tags);
        }
        //---------------------------------------------------------------------------------------------
        public static void QCStatusRFID(dynamic request, dynamic response, dynamic session)
        {
            response.items = RwAppData.QCStatusRFID(conn: FwSqlConnection.RentalWorks,
                                                    tags: request.tags);

            if (request.isconditionsloaded == "F")
            {
                response.rentalconditions = RwAppData.GetRentalConditions(conn: FwSqlConnection.RentalWorks);
            }
        }
        //---------------------------------------------------------------------------------------------
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
        }
        //---------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------
        public static void TimeLogSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "TimeLogSearch";
            string mode;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "value");
            mode = request.mode;

            switch (mode)
            {
                case "ORDER":
                    response.records = RwAppData.TimeLogSearchOrders(conn:       FwSqlConnection.RentalWorks,
                                                                     webusersid: session.security.webUser.webusersid,
                                                                     dealid:     "",
                                                                     orderno:    request.value);
                    break;
                case "DEAL":
                    response.records = RwAppData.TimeLogSearchDeals(conn:       FwSqlConnection.RentalWorks,
                                                                    webusersid: session.security.webUser.webusersid,
                                                                    dealno:     request.value);
                    break;
                case "EVENT":
                    response.records = RwAppData.TimeLogSearchEvents(conn:       FwSqlConnection.RentalWorks,
                                                                     webusersid: session.security.webUser.webusersid,
                                                                     eventno:    request.value);
                    break;
                case "PROJECT":
                    response.records = RwAppData.TimeLogSearchProjects(conn:      FwSqlConnection.RentalWorks,
                                                                       orderno:   request.value);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void TimeLogSubmit(dynamic request, dynamic response, dynamic session)
        {
            string mode;
            mode = request.mode;

            if ((mode == "ORDER") || (mode == "DEAL") || (mode == "EVENT"))
            {
                response.status = RwAppData.TimeLogSubmit(conn:              FwSqlConnection.RentalWorks,
                                                          webusersid:        session.security.webUser.webusersid,
                                                          thedate:           request.date,
                                                          orderid:           request.orderid,
                                                          masteritemid:      request.masteritemid,
                                                          starttime:         FwConvert.ToShortTime24(FwConvert.ToDateTime(request.starttime)),
                                                          stoptime:          FwConvert.ToShortTime24(FwConvert.ToDateTime(request.stoptime)),
                                                          break1starttime:   (request.break1starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break1starttime)) : "",
                                                          break1stoptime:    (request.break1stoptime  != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break1stoptime))  : "",
                                                          break2starttime:   (request.break2starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break2starttime)) : "",
                                                          break2stoptime:    (request.break2stoptime  != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break2stoptime))  : "",
                                                          break3starttime:   (request.break3starttime != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break3starttime)) : "",
                                                          break3stoptime:    (request.break3stoptime  != "") ? FwConvert.ToShortTime24(FwConvert.ToDateTime(request.break3stoptime))  : "",
                                                          notes:             request.notes,
                                                          inputbywebusersid: session.security.webUser.webusersid);
            }
            else if (mode == "PROJECT")
            {
                if (!request.update) {
                    response.status = RwAppData.InsertUserTime(conn:        FwSqlConnection.RentalWorks,
                                                               usersid:     session.security.webUser.usersid,
                                                               workdate:    request.date,
                                                               orderid:     request.timelogdata.selectedrecord.orderid,
                                                               description: request.description,
                                                               workhours:   request.hours);
                }
                else if (request.update)
                {
                    response.status = RwAppData.UpdateUserTime(conn:         FwSqlConnection.RentalWorks,
                                                               usertimeid:   request.recorddata.usertimeid,
                                                               internalchar: request.recorddata.internalchar,
                                                               usersid:      session.security.webUser.usersid,
                                                               workdate:     request.date,
                                                               orderid:      request.recorddata.orderid,
                                                               description:  request.description,
                                                               workhours:    request.hours);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void TimeLogViewEntries(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "TimeLogDropDownData";
            string mode;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "selectedrecord");
            mode = request.mode;

            switch (mode)
            {
                case "EVENT":
                case "DEAL":
                case "ORDER":
                    response.entries = RwAppData.GetCrewTimeEntries(conn:       FwSqlConnection.RentalWorks,
                                                                    webusersid: session.security.webUser.webusersid,
                                                                    orderid:    ((mode == "ORDER") ? request.selectedrecord.orderid : ""),
                                                                    dealid:     ((mode == "DEAL")  ? request.selectedrecord.dealid   : ""),
                                                                    eventid:    ((mode == "EVENT") ? request.selectedrecord.eventid  : ""));
                    break;
                case "PROJECT":
                    response.entries = RwAppData.GetUserTimeEntries(conn:    FwSqlConnection.RentalWorks,
                                                                    usersid: session.security.webUser.usersid,
                                                                    orderid: (FwValidate.IsPropertyDefined(request.selectedrecord, "orderid") ? request.selectedrecord.orderid : ""));
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        public static void GetOrderItemsToSub(dynamic request, dynamic response, dynamic session)
        {
            response.itemstosub = RwAppData.GetOrderItemsToSub(conn:    FwSqlConnection.RentalWorks,
                                                               usersid: session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void GetOrderCompletesToSub(dynamic request, dynamic response, dynamic session)
        {
            response.completestosub = RwAppData.GetOrderCompletesToSub(conn:    FwSqlConnection.RentalWorks,
                                                                       usersid: session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        public static void SubstituteAtStaging(dynamic request, dynamic response, dynamic session)
        {
            response.completestosub = RwAppData.SubstituteAtStaging(conn:                FwSqlConnection.RentalWorks,
                                                                    orderid:             request.orderid,
                                                                    reducemasteritemid:  request.reducemasteritemid,
                                                                    replacewithmasterid: request.replacewithmasterid,
                                                                    usersid:             session.security.webUser.usersid,
                                                                    qtytosubstitute:     request.qtytosubstitute,
                                                                    substitutecomplete:  request.substitutecomplete);
        }
        //---------------------------------------------------------------------------------------------
    }
}
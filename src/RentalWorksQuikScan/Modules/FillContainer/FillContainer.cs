using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.Services;
using System.Dynamic;
using Fw.Json.Services.Common;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules
{
    class FillContainer
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CreateContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.CreateContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
            RwAppData.FillContainer_CreateContainer(conn:             FwSqlConnection.RentalWorks, 
                                                    containeritemid:  request.containeritemid,
                                                    loggedin_usersid: session.security.webUser.usersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetContainerItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetContainerItems";
            switch((string)request.mode)
            {
                case "fillcontainer":
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                    response.containeritems = RwAppData.GetContainerItemsFillContainer(conn:            FwSqlConnection.RentalWorks, 
                                                                                       containeritemid: request.containeritemid);
                    break;
                case "checkin":
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                    response.containeritems = RwAppData.GetContainerItemsCheckIn(conn:            FwSqlConnection.RentalWorks, 
                                                                                 contractid:      request.contractid,
                                                                                 containeritemid: request.containeritemid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetContainerPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetContainerPendingItems";
            session.user = RwAppData.GetUser(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
            switch((string)request.mode)
            {
                case "fillcontainer":
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsFillContainer(conn:            FwSqlConnection.RentalWorks,
                                                                                                          containeritemid: request.containeritemid);
                    break;
                case "checkin":
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealid");
                    FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentid");
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsCheckIn(conn: FwSqlConnection.RentalWorks,
                                                                                                    contractid: request.contractid,
                                                                                                    containeritemid: request.containeritemid,
                                                                                                    dealid: request.dealid,
                                                                                                    departmentid: request.departmentid,
                                                                                                    orderid: request.orderid,
                                                                                                    ordertype: "O",
                                                                                                    tab: "SINGLE_ORDER",
                                                                                                    calculatecounted: "F",
                                                                                                    groupitems: "F",
                                                                                                    warehouseid: "");
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SelectContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.SelectContainer";
            string mode, warehouseid, barcode, usersid;
            dynamic container=null, checkingetiteminfo;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            response.serviceerrormessage = string.Empty;
            usersid      = session.security.webUser.usersid;
            session.user = RwAppData.GetUser(FwSqlConnection.RentalWorks, usersid);
            mode         = request.mode;
            warehouseid  = session.user.warehouseid;
            barcode      = (request.barcode as string).ToUpper();
            if (mode == "fillcontainer")
            {
                response.selectcontainer = RwAppData.FillContainer_SelectContainerForFillContainer(FwSqlConnection.RentalWorks, usersid, warehouseid, barcode, true, string.Empty, null, string.Empty);
            }
            else if (mode == "checkin")
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "checkin");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "moduleType");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "checkInMode");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "code");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "neworderaction");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "aisle");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "shelf");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "vendorid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "contractid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "orderid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "dealid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request.checkin, "departmentid");
                // check if barcode is scannable item on container
                if (!RwAppData.FillContainer_IsScannableItemOfAContainer(FwSqlConnection.RentalWorks, request.checkin.code))
                {
                    response.serviceerrormessage = "Bar Code: " + request.checkin.code + " is not the scannable item of a container.";
                }
                if (string.IsNullOrEmpty(response.serviceerrormessage))
                {
                    container = RwAppData.FillContainer_FuncGetContainer(conn:        FwSqlConnection.RentalWorks,
                                                           barcode:     request.checkin.code,
                                                           warehouseid: warehouseid);
                    if (container == null)
                    {
                        response.serviceerrormessage = "Container not found.";
                    }
                }
                if (string.IsNullOrEmpty(response.serviceerrormessage))
                {
                    checkingetiteminfo = RwAppData.CheckInGetItemInfo(conn:            FwSqlConnection.RentalWorks,
                                                                      barcode:         request.checkin.code,
                                                                      incontractid:    request.checkin.contractid,
                                                                      usersid:         usersid,
                                                                      bctype:          "O",
                                                                      containeritemid: container.containeritemid,
                                                                      orderid:         request.checkin.orderid,
                                                                      dealid:          request.checkin.dealid,
                                                                      departmentid:    request.checkin.departmentid,
                                                                      masteritemid:    "",
                                                                      rentalitemid:    "");
                    if ((checkingetiteminfo.status == 0)
                        || (checkingetiteminfo.status == 1002) // Already in this Session
                        || (checkingetiteminfo.status == 1011) // primary item is already in the container
                       )
                    {
                        string containerid = string.Empty;
                        string containerdesc = string.Empty;
                        if (FwValidate.IsPropertyDefined(request.checkin, "containerdesc"))
                        {
                            containerid   = FwCryptography.AjaxDecrypt(request.checkin.containerid);
                            containerdesc = request.checkin.containerdesc;
                        }
                        response.selectcontainer = RwAppData.FillContainer_SelectContainerForCheckIn(FwSqlConnection.RentalWorks, usersid, warehouseid, barcode, true, request.checkin.contractid, checkingetiteminfo, containerid, containerdesc, request.checkin.orderid, request.checkin.dealid, request.checkin.departmentid);
                    }
                    else
                    {
                        response.serviceerrormessage = checkingetiteminfo.msg;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void InstantiateContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.InstantiateContainer";
            string mode, barcode, containerid, rentalitemid, usersid, warehouseid;
            bool autostageacc, fromcheckin;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containerid");

            mode         = request.mode;
            barcode      = request.barcode;
            containerid  = FwCryptography.AjaxDecrypt(request.containerid);
            rentalitemid = FwSqlCommand.GetData(FwSqlConnection.RentalWorks, "rentalitem", "barcode", barcode, "rentalitemid").ToString().TrimEnd();
            autostageacc = true;
            usersid      = session.security.webUser.usersid;
            session.user = RwAppData.GetUser(FwSqlConnection.RentalWorks, usersid);
            warehouseid  = session.user.warehouseid;
            fromcheckin  = (request.mode == "checkin");
            response.serviceerrormessage = string.Empty;
            response.instantiatecontainer = RwAppData.InstantiateContainer(FwSqlConnection.RentalWorks, containerid, rentalitemid, autostageacc, usersid, fromcheckin);
            if (request.mode == "fillcontainer")
            {
                response.selectcontainer      = RwAppData.FillContainer_SelectContainerForFillContainer(FwSqlConnection.RentalWorks, usersid, warehouseid, barcode, true, string.Empty, null, string.Empty);
            }
            else if (request.mode == "checkin")
            {
                response.selectcontainer      = RwAppData.FillContainer_SelectContainerForCheckIn(FwSqlConnection.RentalWorks, usersid, warehouseid, barcode, false, string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void AddAllQtyItemsToContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.AddAllQtyItemsToContainer";
            FwJsonDataTable dtPendingList;
            string masterno, masteritemid, masterid, trackedby, contractid, warehouseid, usersid, parentid;
            decimal missingqty;
            dynamic userLocation;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            if (request.mode == "fillcontainer")
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
            }
            else if (request.mode == "checkin")
            {
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeroutcontractid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentid");
            }
            session.user = RwAppData.GetUser(conn:    FwSqlConnection.RentalWorks
                                            , usersId: session.security.webUser.usersid);
            if (session.user.qsallowapplyallqtyitems != "T")
            {
                throw new Exception("You do not have permission to Add All Quantity Items");
            }
            //orderid       = request.orderid;
            contractid    = request.contractid;
            usersid       = session.security.webUser.usersid;
            userLocation  = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                    , usersId: usersid);
            warehouseid   = userLocation.warehouseId;
            switch((string)request.mode)
            {
                case "fillcontainer":
                    dtPendingList = RwAppData.FillContainer_GetContainerPendingItemsFillContainer(conn:            FwSqlConnection.RentalWorks,
                                                                                                  containeritemid: request.containeritemid);
                    break;
                case "checkin":
                    dtPendingList = RwAppData.FillContainer_GetContainerPendingItemsCheckIn(conn:             FwSqlConnection.RentalWorks,
                                                                                            contractid:       contractid,
                                                                                            containeritemid:  request.containeritemid,
                                                                                            dealid:           request.dealid,
                                                                                            departmentid:     request.departmentid,
                                                                                            orderid:          "", //orderid,
                                                                                            ordertype:        "O",
                                                                                            tab:              "SINGLE_ORDER",
                                                                                            calculatecounted: "F",
                                                                                            groupitems:       "F",
                                                                                            warehouseid:      ""); //warehouseid);
                    break;
                default: throw new Exception("mode is invalid");
            }
            for(int rowno = 0; rowno < dtPendingList.Rows.Count; rowno++)
            {
                trackedby    = dtPendingList.GetValue(rowno, "trackedby").ToString();
                if (trackedby.Equals("QUANTITY"))
                {
                    if (request.mode == "fillcontainer")
                    {
                        masterno     = dtPendingList.GetValue(rowno, "masterno").ToString();
                        masteritemid = dtPendingList.GetValue(rowno, "masteritemid").ToString();
                        missingqty   = dtPendingList.GetValue(rowno, "missingqty").ToDecimal();
                        dynamic pdastageitem = RwAppData.PdaStageItem(conn:                 FwSqlConnection.RentalWorks,
                                                                      orderid:              request.containeritemid,
                                                                      code:                 masterno,
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
                                                                      contractid:           request.containeroutcontractid, 
                                                                      ignoresuspendedin:    false, 
                                                                      consignorid:          string.Empty, 
                                                                      consignoragreementid: string.Empty,
                                                                      spacetypeid:          string.Empty,
                                                                      facilitiestypeid:     string.Empty);
                    }
                    else if (request.mode == "checkin")
                    {
                        masterno     = dtPendingList.GetValue(rowno, "masterno").ToString();
                        masteritemid = dtPendingList.GetValue(rowno, "masteritemid").ToString();
                        masterid     = dtPendingList.GetValue(rowno, "masterid").ToString();
                        missingqty   = dtPendingList.GetValue(rowno, "missingqty").ToDecimal();
                        parentid     = dtPendingList.GetValue(rowno, "parentid").ToString();
                        dynamic webcheckinitem = RwAppData.WebCheckInItem(conn:                   FwSqlConnection.RentalWorks,
                                                                          usersId:                usersid,
                                                                          moduleType:             RwAppData.ModuleType.Order,
                                                                          checkInMode:            RwAppData.CheckInMode.SingleOrder,
                                                                          code:                   masterno,
                                                                          masterItemId:           masteritemid,
                                                                          qty:                    missingqty,
                                                                          newOrderAction:         "",
                                                                          containeritemid:        request.containeritemid,
                                                                          containeroutcontractid: request.containeroutcontractid,
                                                                          aisle:                  string.Empty,
                                                                          shelf:                  string.Empty,
                                                                          parentid:               parentid,
                                                                          vendorId:               string.Empty,
                                                                          disablemultiorder:      false,
                                                                          contractId:             contractid,
                                                                          orderId:                dtPendingList.GetValue(rowno, "orderid").ToString(),
                                                                          dealId:                 request.dealid,
                                                                          departmentId:           request.departmentid,
                                                                          trackedby:              "",
                                                                          spaceid:                "",
                                                                          spacetypeid:            "",
                                                                          facilitiestypeid:       "");
                    }
                }
            }
            switch((string)request.mode)
            {
                case "fillcontainer":
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsFillContainer(conn:            FwSqlConnection.RentalWorks,
                                                                                                          containeritemid: request.containeritemid);
                    break;
                case "checkin":
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsCheckIn(conn:             FwSqlConnection.RentalWorks,
                                                                                                    contractid:       contractid,
                                                                                                    containeritemid:  request.containeritemid,
                                                                                                    dealid:           request.dealid,
                                                                                                    departmentid:     request.departmentid,
                                                                                                    orderid:          "", //request.orderid,
                                                                                                    ordertype:        "O",
                                                                                                    tab:              "SINGLE_ORDER",
                                                                                                    calculatecounted: "F",
                                                                                                    groupitems:       "F",
                                                                                                    warehouseid:      ""); //session.user.warehouseid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SetContainerNo(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.SetContainerNo";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containerno");
            response.containerno = RwAppData.FillContainer_SetContainerNo(conn:         FwSqlConnection.RentalWorks,
                                                            rentalitemid: request.rentalitemid,
                                                            containerno:  request.containerno);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void RemoveItemFromContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.RemoveItemFromContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
            response.advancedmovemasteritemid = RwAppData.AdvancedMoveMasterItemId(conn:         FwSqlConnection.RentalWorks,
                                                                                   contractid:   request.contractid,
                                                                                   vendorid:     request.vendorid,
                                                                                   masteritemid: request.masteritemid,
                                                                                   qty:          request.qty,
                                                                                   usersid:      session.security.webUser.usersid,
                                                                                   movemode:     4);
            switch((string)request.mode)
            {
                case "fillcontainer":
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsFillContainer(conn:            FwSqlConnection.RentalWorks,
                                                                                                          containeritemid: request.containeritemid);
                    break;
                case "checkin":
                    response.pendingitems = RwAppData.FillContainer_GetContainerPendingItemsCheckIn(conn:             FwSqlConnection.RentalWorks,
                                                                                                    contractid:       request.contractid,
                                                                                                    containeritemid:  request.containeritemid,
                                                                                                    dealid:           request.dealid,
                                                                                                    departmentid:     request.departmentid,
                                                                                                    orderid:          "", //request.orderid,
                                                                                                    ordertype:        "O",
                                                                                                    tab:              "SINGLE_ORDER",
                                                                                                    calculatecounted: "F",
                                                                                                    groupitems:       "F",
                                                                                                    warehouseid:      ""); //session.user.warehouseid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CloseContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.CloseContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            if (RwAppData.ContractIsEmpty(conn:       FwSqlConnection.RentalWorks,
                                          contractid: request.contractid))
            {
                RwAppData.CancelContract(conn:       FwSqlConnection.RentalWorks,
                                         contractid: request.contractid,
                                         usersid:    session.security.webUser.usersid,
                                         failSilentlyOnOwnershipErrors: false);
            }
            else
            {
                using (FwSqlCommand sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "checkinassignsuspendedcontainers"))
                {
                    sp.AddParameter("@contractid", request.contractid);
                    sp.AddParameter("@usersid", session.security.webUser.usersid);
                    sp.ExecuteNonQuery();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void HasCheckinFillContainerButton(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.HasCheckinFillContainerButton";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            response.hasCheckinFillContainerButton = RwAppData.HasCheckinFillContainerButton(conn:       FwSqlConnection.RentalWorks,
                                                                                             contractid: request.contractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetDefaultContainerDescCheckIn(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetDefaultContainerDescCheckIn";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            string usersid       = session.security.webUser.usersid;
            dynamic userLocation  = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                    , usersId: usersid);
            string warehouseid   = userLocation.warehouseId;;

            response.defaultcontainerdesc = RwAppData.FillContainer_GetDefaultContainerDescCheckIn(conn:        FwSqlConnection.RentalWorks,
                                                                                                   barcode:     request.barcode,
                                                                                                   warehouseid: warehouseid);
        }
        //---------------------------------------------------------------------------------------------
    }
}
 
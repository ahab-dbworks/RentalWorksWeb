using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class FillContainer : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public FillContainer(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CreateContainerAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.CreateContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.FillContainer_CreateContainerAsync(conn: conn,
                                                                containeritemid: request.containeritemid,
                                                                loggedin_usersid: session.security.webUser.usersid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetContainerItemsAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetContainerItems";
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                switch ((string)request.mode)
                {
                    case "fillcontainer":
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                        response.containeritems = await this.AppData.GetContainerItemsFillContainerAsync(conn: conn,
                                                                                           containeritemid: request.containeritemid);
                        break;
                    case "checkin":
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                        response.containeritems = await this.AppData.GetContainerItemsCheckInAsync(conn: conn,
                                                                                     contractid: request.contractid,
                                                                                     containeritemid: request.containeritemid);
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetContainerPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetContainerPendingItems";
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.user = await this.AppData.GetUserAsync(conn, session.security.webUser.usersid);
                switch ((string)request.mode)
                {
                    case "fillcontainer":
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsFillContainerAsync(conn: conn,
                                                                                                              containeritemid: request.containeritemid);
                        break;
                    case "checkin":
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealid");
                        FwValidate.TestPropertyDefined(METHOD_NAME, request, "departmentid");
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsCheckInAsync(conn: conn,
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
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SelectContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.SelectContainer";
            string mode, warehouseid, barcode, usersid;
            dynamic container=null, checkingetiteminfo;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.serviceerrormessage = string.Empty;
                usersid = session.security.webUser.usersid;
                session.user = await this.AppData.GetUserAsync(conn, usersid);
                mode = request.mode;
                warehouseid = session.user.warehouseid;
                barcode = (request.barcode as string).ToUpper();
                if (mode == "fillcontainer")
                {
                    response.selectcontainer = await this.AppData.FillContainer_SelectContainerForFillContainerAsync(conn, usersid, warehouseid, barcode, true, string.Empty, null, string.Empty);
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
                    if (!await this.AppData.FillContainer_IsScannableItemOfAContainerAsync(conn, request.checkin.code))
                    {
                        response.serviceerrormessage = "Bar Code: " + request.checkin.code + " is not the scannable item of a container.";
                    }
                    if (string.IsNullOrEmpty(response.serviceerrormessage))
                    {
                        container = await this.AppData.FillContainer_FuncGetContainerAsync(conn: conn,
                                                               barcode: request.checkin.code,
                                                               warehouseid: warehouseid);
                        if (container == null)
                        {
                            response.serviceerrormessage = "Container not found.";
                        }
                    }
                    if (string.IsNullOrEmpty(response.serviceerrormessage))
                    {
                        checkingetiteminfo = await this.AppData.CheckInGetItemInfoAsync(conn: conn,
                                                                          barcode: request.checkin.code,
                                                                          incontractid: request.checkin.contractid,
                                                                          usersid: usersid,
                                                                          bctype: "O",
                                                                          containeritemid: container.containeritemid,
                                                                          orderid: request.checkin.orderid,
                                                                          dealid: request.checkin.dealid,
                                                                          departmentid: request.checkin.departmentid,
                                                                          masteritemid: "",
                                                                          rentalitemid: "");
                        if ((checkingetiteminfo.status == 0)
                            || (checkingetiteminfo.status == 1002) // Already in this Session
                            || (checkingetiteminfo.status == 1011) // primary item is already in the container
                           )
                        {
                            string containerid = string.Empty;
                            string containerdesc = string.Empty;
                            if (FwValidate.IsPropertyDefined(request.checkin, "containerdesc"))
                            {
                                containerid = FwCryptography.AjaxDecrypt(request.checkin.containerid);
                                containerdesc = request.checkin.containerdesc;
                            }
                            response.selectcontainer = await this.AppData.FillContainer_SelectContainerForCheckInAsync(conn, usersid, warehouseid, barcode, true, request.checkin.contractid, checkingetiteminfo, containerid, containerdesc, request.checkin.orderid, request.checkin.dealid, request.checkin.departmentid);
                        }
                        else
                        {
                            response.serviceerrormessage = checkingetiteminfo.msg;
                        }
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task InstantiateContainerAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.InstantiateContainer";
            string mode, barcode, containerid, rentalitemid, usersid, warehouseid;
            bool autostageacc, fromcheckin;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "mode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containerid");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                mode = request.mode;
                barcode = request.barcode;
                containerid = FwCryptography.AjaxDecrypt(request.containerid);
                rentalitemid = (await FwSqlCommand.GetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "rentalitem", "barcode", barcode, "rentalitemid")).ToString().TrimEnd();
                autostageacc = true;
                usersid = session.security.webUser.usersid;
                session.user = await this.AppData.GetUserAsync(conn, usersid);
                warehouseid = session.user.warehouseid;
                fromcheckin = (request.mode == "checkin");
                response.serviceerrormessage = string.Empty;
                response.instantiatecontainer = await this.AppData.InstantiateContainerAsync(conn, containerid, rentalitemid, autostageacc, usersid, fromcheckin);
                if (request.mode == "fillcontainer")
                {
                    response.selectcontainer = await this.AppData.FillContainer_SelectContainerForFillContainerAsync(conn, usersid, warehouseid, barcode, true, string.Empty, null, string.Empty);
                }
                else if (request.mode == "checkin")
                {
                    response.selectcontainer = await this.AppData.FillContainer_SelectContainerForCheckInAsync(conn, usersid, warehouseid, barcode, false, string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task AddAllQtyItemsToContainerAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.AddAllQtyItemsToContainer";
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
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
                session.user = await this.AppData.GetUserAsync(conn: conn
                                                , usersId: session.security.webUser.usersid);
                if (session.user.qsallowapplyallqtyitems != "T")
                {
                    throw new Exception("You do not have permission to Add All Quantity Items");
                }
                //orderid       = request.orderid;
                contractid = request.contractid;
                usersid = session.security.webUser.usersid;
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn
                                                        , usersId: usersid);
                warehouseid = userLocation.warehouseId;
                switch ((string)request.mode)
                {
                    case "fillcontainer":
                        dtPendingList = await this.AppData.FillContainer_GetContainerPendingItemsFillContainerAsync(conn: conn,
                                                                                                      containeritemid: request.containeritemid);
                        break;
                    case "checkin":
                        dtPendingList = await this.AppData.FillContainer_GetContainerPendingItemsCheckInAsync(conn: conn,
                                                                                                contractid: contractid,
                                                                                                containeritemid: request.containeritemid,
                                                                                                dealid: request.dealid,
                                                                                                departmentid: request.departmentid,
                                                                                                orderid: "", //orderid,
                                                                                                ordertype: "O",
                                                                                                tab: "SINGLE_ORDER",
                                                                                                calculatecounted: "F",
                                                                                                groupitems: "F",
                                                                                                warehouseid: ""); //warehouseid);
                        break;
                    default: throw new Exception("mode is invalid");
                }
                for (int rowno = 0; rowno < dtPendingList.Rows.Count; rowno++)
                {
                    trackedby = dtPendingList.GetValue(rowno, "trackedby").ToString();
                    if (trackedby.Equals("QUANTITY"))
                    {
                        if (request.mode == "fillcontainer")
                        {
                            masterno = dtPendingList.GetValue(rowno, "masterno").ToString();
                            masteritemid = dtPendingList.GetValue(rowno, "masteritemid").ToString();
                            missingqty = dtPendingList.GetValue(rowno, "missingqty").ToDecimal();
                            dynamic pdastageitem = await this.AppData.PdaStageItemAsync(conn: conn,
                                                                          orderid: request.containeritemid,
                                                                          code: masterno,
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
                                                                          contractid: request.containeroutcontractid,
                                                                          ignoresuspendedin: false,
                                                                          consignorid: string.Empty,
                                                                          consignoragreementid: string.Empty,
                                                                          spacetypeid: string.Empty,
                                                                          facilitiestypeid: string.Empty);
                        }
                        else if (request.mode == "checkin")
                        {
                            masterno = dtPendingList.GetValue(rowno, "masterno").ToString();
                            masteritemid = dtPendingList.GetValue(rowno, "masteritemid").ToString();
                            masterid = dtPendingList.GetValue(rowno, "masterid").ToString();
                            missingqty = dtPendingList.GetValue(rowno, "missingqty").ToDecimal();
                            parentid = dtPendingList.GetValue(rowno, "parentid").ToString();
                            dynamic webcheckinitem = await this.AppData.WebCheckInItemAsync(conn: conn,
                                                                              usersId: usersid,
                                                                              moduleType: RwAppData.ModuleType.Order,
                                                                              checkInMode: RwAppData.CheckInMode.SingleOrder,
                                                                              code: masterno,
                                                                              masterItemId: masteritemid,
                                                                              qty: missingqty,
                                                                              newOrderAction: "",
                                                                              containeritemid: request.containeritemid,
                                                                              containeroutcontractid: request.containeroutcontractid,
                                                                              aisle: string.Empty,
                                                                              shelf: string.Empty,
                                                                              parentid: parentid,
                                                                              vendorId: string.Empty,
                                                                              disablemultiorder: false,
                                                                              contractId: contractid,
                                                                              orderId: dtPendingList.GetValue(rowno, "orderid").ToString(),
                                                                              dealId: request.dealid,
                                                                              departmentId: request.departmentid,
                                                                              trackedby: "",
                                                                              spaceid: "",
                                                                              spacetypeid: "",
                                                                              facilitiestypeid: "");
                        }
                    }
                }
                switch ((string)request.mode)
                {
                    case "fillcontainer":
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsFillContainerAsync(conn: conn,
                                                                                                              containeritemid: request.containeritemid);
                        break;
                    case "checkin":
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsCheckInAsync(conn: conn,
                                                                                                        contractid: contractid,
                                                                                                        containeritemid: request.containeritemid,
                                                                                                        dealid: request.dealid,
                                                                                                        departmentid: request.departmentid,
                                                                                                        orderid: "", //request.orderid,
                                                                                                        ordertype: "O",
                                                                                                        tab: "SINGLE_ORDER",
                                                                                                        calculatecounted: "F",
                                                                                                        groupitems: "F",
                                                                                                        warehouseid: ""); //session.user.warehouseid);
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SetContainerNo(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.SetContainerNo";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "rentalitemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containerno");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.containerno = await this.AppData.FillContainer_SetContainerNoAsync(conn: conn,
                                                                        rentalitemid: request.rentalitemid,
                                                                        containerno: request.containerno); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RemoveItemFromContainer(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.RemoveItemFromContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "vendorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "masteritemid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "containeritemid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.advancedmovemasteritemid = await this.AppData.AdvancedMoveMasterItemIdAsync(conn: conn,
                                                                                               contractid: request.contractid,
                                                                                               vendorid: request.vendorid,
                                                                                               masteritemid: request.masteritemid,
                                                                                               qty: request.qty,
                                                                                               usersid: session.security.webUser.usersid,
                                                                                               movemode: 4);
                switch ((string)request.mode)
                {
                    case "fillcontainer":
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsFillContainerAsync(conn: conn,
                                                                                                              containeritemid: request.containeritemid);
                        break;
                    case "checkin":
                        response.pendingitems = await this.AppData.FillContainer_GetContainerPendingItemsCheckInAsync(conn: conn,
                                                                                                        contractid: request.contractid,
                                                                                                        containeritemid: request.containeritemid,
                                                                                                        dealid: request.dealid,
                                                                                                        departmentid: request.departmentid,
                                                                                                        orderid: "", //request.orderid,
                                                                                                        ordertype: "O",
                                                                                                        tab: "SINGLE_ORDER",
                                                                                                        calculatecounted: "F",
                                                                                                        groupitems: "F",
                                                                                                        warehouseid: ""); //session.user.warehouseid);
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CloseContainerAsync(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.CloseContainer";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (await this.AppData.ContractIsEmptyAsync(conn: conn,
                                                      contractid: request.contractid))
                {
                    await this.AppData.CancelContractAsync(conn: conn,
                                             contractid: request.contractid,
                                             usersid: session.security.webUser.usersid,
                                             failSilentlyOnOwnershipErrors: false);
                }
                else
                {
                    using (FwSqlCommand sp = new FwSqlCommand(conn, "checkinassignsuspendedcontainers", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        sp.AddParameter("@contractid", request.contractid);
                        sp.AddParameter("@usersid", session.security.webUser.usersid);
                        await sp.ExecuteNonQueryAsync();
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task HasCheckinFillContainerButton(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.HasCheckinFillContainerButton";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractid");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.hasCheckinFillContainerButton = await this.AppData.HasCheckinFillContainerButtonAsync(conn: conn,
                                                                                                         contractid: request.contractid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetDefaultContainerDescCheckIn(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "FillContainer.GetDefaultContainerDescCheckIn";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string usersid = session.security.webUser.usersid;
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn: conn
                                                        , usersId: usersid);
                string warehouseid = userLocation.warehouseId; ;

                response.defaultcontainerdesc = await this.AppData.FillContainer_GetDefaultContainerDescCheckInAsync(conn: conn,
                                                                                                       barcode: request.barcode,
                                                                                                       warehouseid: warehouseid); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
 
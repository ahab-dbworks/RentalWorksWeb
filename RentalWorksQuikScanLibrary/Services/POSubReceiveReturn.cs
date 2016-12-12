﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;

namespace RentalWorksQuikScanLibrary.Services
{
    public class POSubReceiveReturn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetPOSubReceiveReturnPendingList(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetPOSubReceiveReturnPendingList";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "poId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            session.userLocation = RwAppData.GetUserLocation(conn:    FwSqlConnection.RentalWorks
                                                           , usersId: session.security.webUser.usersid);
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "User warehouse is required to peform this action.", session.userLocation.warehouseId);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "showAll");
            response.poSubReceiveReturnPendingList = RwAppData.GetPOSubReceiveReturnPendingList(conn:        FwSqlConnection.RentalWorks
                                                                                              , moduleType:  request.moduleType
                                                                                              , poId:        request.poId
                                                                                              , warehouseId: session.userLocation.warehouseId
                                                                                              , contractId:  request.contractId
                                                                                              , showAll:     request.showAll);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void WebSubReceiveReturnItem(dynamic request, dynamic response, dynamic session)
        {
            string poId, code, barcode, usersId, contractId, masterItemId;
            decimal qty;
            string moduleType;
            bool assignBC;
            const string METHOD_NAME = "WebSubReceiveReturnItem";
            dynamic poItem;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "poId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            poId       = request.poId;
            code       = request.code;
            barcode    = request.barcode;
            usersId    = session.security.webUser.usersid;
            qty        = FwConvert.ToDecimal(request.qty);
            moduleType = request.moduleType;
            assignBC   = (barcode.Length > 0);
            contractId = request.contractId;
            if ((FwValidate.IsPropertyDefined(request, "masterItemId")) && (!string.IsNullOrEmpty(request.masterItemId)))
            {
                masterItemId = request.masterItemId;
                switch (moduleType)
                {
                    case RwConstants.ModuleTypes.SubReceive:
                        if (string.IsNullOrEmpty(contractId))
                        {
                            response.contract = RwAppData.CreateReceiveContract(conn, poId, usersId);
                            contractId = response.contract.contractId;
                            if (string.IsNullOrEmpty(contractId)) throw new Exception(METHOD_NAME + ": Unable to create receive contract.");
                        }
                        using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.receiveitem"))
                        {
                            sp.AddParameter("@contractid",   contractId);
                            sp.AddParameter("@orderid",      poId);
                            sp.AddParameter("@masteritemid", masterItemId);
                            sp.AddParameter("@usersid",      usersId);
                            sp.AddParameter("@barcode",      barcode);
                            sp.AddParameter("@qty",          qty);
                            sp.AddParameter("@status", SqlDbType.Int,     ParameterDirection.Output);
                            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
                            sp.Execute();
                            session.receiveItem = new ExpandoObject();
                            session.receiveItem.status = sp.GetParameter("@status").ToInt32();
                            session.receiveItem.msg    = sp.GetParameter("@msg").ToString();
                        }
                        //session.receiveItem = RwAppData.ReceiveItem(FwSqlConnection.RentalWorks, contractId, poId, masterItemId, usersId, barcode, qty); 
                        break;
                    case RwConstants.ModuleTypes.SubReturn: 
                        if (string.IsNullOrEmpty(contractId))
                        {
                            response.contract = RwAppData.CreateReturnContract(conn:    FwSqlConnection.RentalWorks
                                                                             , poid:    poId
                                                                             , usersid: usersId);
                            contractId = response.contract.contractId;
                            if (string.IsNullOrEmpty(contractId))
                            {
                                throw new Exception(METHOD_NAME + ": Unable to create return contract.");
                            }
                        }
                        session.returnItem = RwAppData.ReturnItem(conn:         FwSqlConnection.RentalWorks
                                                                , contractId:   contractId
                                                                , orderId:      poId
                                                                , masterItemId: masterItemId
                                                                , usersId:      usersId
                                                                , barcode:      barcode
                                                                , qty:          qty); 
                        break;
                }
                poItem = RwAppData.GetPoItem(conn:         FwSqlConnection.RentalWorks
                                           , poId:         poId
                                           , masterItemId: masterItemId
                                           , contractId:   contractId);
                if (poItem != null)
                {
                    response.webSubReceiveReturnItem = new ExpandoObject();
                    response.webSubReceiveReturnItem.poItemId     = masterItemId;
                    response.webSubReceiveReturnItem.contractId   = contractId;
                    response.webSubReceiveReturnItem.description  = poItem.description;
                    response.webSubReceiveReturnItem.qtyOrdered   = poItem.netqtyordered;
                    response.webSubReceiveReturnItem.qtySession   = poItem.qtysession;
                    response.webSubReceiveReturnItem.qtyReceived  = poItem.qtyreceived;
                    response.webSubReceiveReturnItem.qtyReturned  = poItem.qtyreturned;
                    response.webSubReceiveReturnItem.qtyRemaining = poItem.qtyremaining;
                    response.webSubReceiveReturnItem.genericMsg   = "";
                    response.webSubReceiveReturnItem.status       = 0;
                    response.webSubReceiveReturnItem.msg          = "";
                    response.webSubReceiveReturnItem.isBarCode    = (poItem.trackedby == "BARCODE");
                }
                else
                {
                    response.webSubReceiveReturnItem = new ExpandoObject();
                    response.webSubReceiveReturnItem.poItemId     = masterItemId;
                    response.webSubReceiveReturnItem.contractId   = contractId;
                    response.webSubReceiveReturnItem.description  = "";
                    response.webSubReceiveReturnItem.qtyOrdered   = 0;
                    response.webSubReceiveReturnItem.qtySession   = 0;
                    response.webSubReceiveReturnItem.qtyReceived  = 0;
                    response.webSubReceiveReturnItem.qtyReturned  = 0;
                    response.webSubReceiveReturnItem.qtyRemaining = 0;
                    response.webSubReceiveReturnItem.genericMsg   = "Error";
                    response.webSubReceiveReturnItem.status       = 0;
                    response.webSubReceiveReturnItem.msg          = "PO Item not found.";
                    response.webSubReceiveReturnItem.isBarCode    = false;
                }
            }
            else
            {
                response.webSubReceiveReturnItem = RwAppData.WebSubReceiveReturnItem(FwSqlConnection.RentalWorks, poId, code, barcode, usersId, qty, moduleType, assignBC, contractId);
                if (response.webSubReceiveReturnItem.status == 0)
                {
                    // The function above return the wrong results so this is a work around below:
                    masterItemId = response.webSubReceiveReturnItem.poItemId;
                    poItem = RwAppData.GetPoItem(FwSqlConnection.RentalWorks, poId, masterItemId, contractId);
                    //response.webSubReceiveReturnItem.poItemId     = masterItemId;
                    //response.webSubReceiveReturnItem.contractId   = contractId;
                    //response.webSubReceiveReturnItem.description  = poItem.description;
                    response.webSubReceiveReturnItem.qtyOrdered   = poItem.netqtyordered;
                    response.webSubReceiveReturnItem.qtySession   = poItem.qtysession;
                    response.webSubReceiveReturnItem.qtyReceived  = poItem.qtyreceived;
                    response.webSubReceiveReturnItem.qtyReturned  = poItem.qtyreturned;
                    response.webSubReceiveReturnItem.qtyRemaining = poItem.qtyremaining;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}

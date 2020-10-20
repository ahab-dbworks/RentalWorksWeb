﻿using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.PurchaseOrderReceiveItem;
using WebApi.Modules.HomeControls.PurchaseOrderReturnItem;
using WebApi;
using System.ComponentModel.DataAnnotations;
using WebApi.Modules.Warehouse.Contract;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using System.Reflection;
using FwStandard.AppManager;

namespace WebApi.Modules.Agent.PurchaseOrder
{

    public class ReceiveContractRequest
    {
        public string PurchaseOrderId;
        public string WarehouseId;
    }

    public class ReceiveContractResponse
    {
        public string ContractId;
    }

    public class CompleteReceiveContractRequest
    {
        public bool? CreateOutContracts;
    }

    public class ReceiveItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        [Required]
        public string PurchaseOrderItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class ReceiveItemResponse : TSpStatusResponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
        public double QuantityNeedBarCode;
        public string QuantityColor;
    }


    public class SelectAllNoneReceiveItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        [Required]
        public string WarehouseId { get; set; }
    }


    public class SelectAllNoneReceiveItemResponse : TSpStatusResponse
    {
    }

    public class ReturnContractRequest
    {
        public string PurchaseOrderId;
    }

    public class ReturnContractResponse
    {
        public string ContractId;
    }

    public class ReturnItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        //[Required]
        public string PurchaseOrderItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string BarCode { get; set; }
    }


    public class ReturnItemResponse : TSpStatusResponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
        public double QuantityReturned;
    }

    public class SelectAllNoneReturnItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        [Required]
        public string WarehouseId { get; set; }
    }


    public class SelectAllNoneReturnItemResponse : TSpStatusResponse
    {
    }



    public class PurchaseOrderReceiveBarCodeAddItemsRequest
    {
        public string PurchaseOrderId;
        public string ContractId;
        public string WarehouseId;
    }

    public class PurchaseOrderReceiveBarCodeAddItemsResponse : TSpStatusResponse
    {
        public int ItemsAdded;
    }

    public class PurchaseOrderReceiveAssignBarCodesRequest
    {
        public string PurchaseOrderId;
        public string ContractId;
        public string WarehouseId;
    }

    public class PurchaseOrderReceiveAssignBarCodesResponse : TSpStatusResponse
    {
    }

    public class PurchaseOrderToggleCloseResponse : TSpStatusResponse
    {
    }


    public class NextVendorInvoiceDefaultDatesResponse : TSpStatusResponse
    {
        public DateTime? BillingStartDate { get; set; }
        public DateTime? BillingEndDate { get; set; }
    }


    public class CopyPurchaseOrderRequest
    {
        public string PurchaseOrderId { get; set; }
        //public string LocationId { get; set; }
        //public string WarehouseId { get; set; }
    }
    public enum PurchaseOrderCopyMode
    {
        PurchaseOrder,
        NewVersion,
        Copy
    }
    public class CopyPurchaseOrderResponse : TSpStatusResponse
    {
        public PurchaseOrderLogic PurchaseOrder { get; set; }
    }


    public static class PurchaseOrderFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> AfterSavePurchaseOrder(FwApplicationConfig appConfig, FwUserSession userSession, string id, FwSqlConnection conn = null)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            using (FwSqlCommand qry = new FwSqlCommand(conn, "aftersavepoweb", appConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, id);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReceiveContractResponse> CreateReceiveContract(FwApplicationConfig appConfig, FwUserSession userSession, ReceiveContractRequest request)
        {
            ReceiveContractResponse response = new ReceiveContractResponse();

            PurchaseOrderLogic l = new PurchaseOrderLogic();
            l.SetDependencies(appConfig, userSession);
            l.PurchaseOrderId = request.PurchaseOrderId;
            if (await l.LoadAsync<PurchaseOrderLogic>())
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "createreceivecontract", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, request.PurchaseOrderId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.ContractId = qry.GetParameter("@contractid").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<ReturnContractResponse> CreateReturnContract(FwApplicationConfig appConfig, FwUserSession userSession, ReturnContractRequest request)
        {
            ReturnContractResponse response = new ReturnContractResponse();

            PurchaseOrderLogic l = new PurchaseOrderLogic();
            l.SetDependencies(appConfig, userSession);
            l.PurchaseOrderId = request.PurchaseOrderId;
            if (await l.LoadAsync<PurchaseOrderLogic>())
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "createreturncontract", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, request.PurchaseOrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.ContractId = qry.GetParameter("@contractid").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<ReceiveItemResponse> ReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity)
        {
            ReceiveItemResponse response = new ReceiveItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "receiveitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyneedbarcode", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtycolor", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityNeedBarCode = qry.GetParameter("@qtyneedbarcode").ToDouble();
                if (!qry.GetParameter("@qtycolor").IsDbNull())
                {
                    response.QuantityColor = FwConvert.OleColorToHtmlColor(qry.GetParameter("@qtycolor").ToInt32());
                }
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReceiveItemResponse> SelectAllNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId, bool selectAll)
        {
            SelectAllNoneReceiveItemResponse response = new SelectAllNoneReceiveItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreceivefromvendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectAllReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, warehouseId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, warehouseId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReturnItemResponse> ReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity, string barCode = "")
        {
            ReturnItemResponse response = new ReturnItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "returnitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, barCode);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreturned", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityReturned = qry.GetParameter("@qtyreturned").ToDouble();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReturnItemResponse> SelectAllNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId, bool selectAll)
        {
            SelectAllNoneReturnItemResponse response = new SelectAllNoneReturnItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreturntovendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectAllReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, warehouseId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string warehouseId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, warehouseId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<List<string>> CreateOutContractsFromReceive(FwApplicationConfig appConfig, FwUserSession userSession, string receiveContractId)
        {
            List<string> contractIds = new List<string>();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string[] outContractIds = new string[0];
                FwSqlCommand qry = new FwSqlCommand(conn, "createoutcontractsfromreceive", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@receivecontractid", SqlDbType.NVarChar, ParameterDirection.Input, receiveContractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@outcontractids", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                outContractIds = qry.GetParameter("@outcontractids").ToString().Split(',');
                foreach (string outContractId in outContractIds)
                {
                    contractIds.Add(outContractId);
                }
            }
            return contractIds;
        }
        //-------------------------------------------------------------------------------------------------------            
        public static async Task<NextVendorInvoiceDefaultDatesResponse> GetNextVendorInvoiceDefaultDates(FwApplicationConfig appConfig, FwUserSession userSession, string purchaseOrderId)
        {
            NextVendorInvoiceDefaultDatesResponse response = new NextVendorInvoiceDefaultDatesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "getnextvendorinvoicedefaultdates", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@billingstart", SqlDbType.Date, ParameterDirection.Output);
                qry.AddParameter("@billingend", SqlDbType.Date, ParameterDirection.Output);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.BillingStartDate = qry.GetParameter("@billingstart").ToDateTime();
                response.BillingEndDate = qry.GetParameter("@billingend").ToDateTime();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PurchaseOrderToggleCloseResponse> ToggleClose(FwApplicationConfig appConfig, FwUserSession userSession, string purchaseOrderId)
        {
            PurchaseOrderToggleCloseResponse response = new PurchaseOrderToggleCloseResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "toggleclosepoweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<CopyPurchaseOrderResponse> CopyPurchaseOrder(FwApplicationConfig appConfig, FwUserSession userSession, PurchaseOrderLogic from, CopyPurchaseOrderRequest request)
        {
            CopyPurchaseOrderResponse response = new CopyPurchaseOrderResponse();

            // refer to OrderFunc.CopyQuoteOrder to see how we are using C# reflection to load the properties of the "from" Purchase Order and creating a new "to" Purchase Order

            return response;
        }
        //-------------------------------------------------------------------------------------------------------  
        //-------------------------------------------------------------------------------------------------------
        private static async Task<PurchaseOrderLogic> CopyPurchaseOrder(FwApplicationConfig appConfig, FwUserSession userSession, PurchaseOrderLogic from, string toType, PurchaseOrderCopyMode copyMode, string newLocationId = "", string newWarehouseId = "")
        {
            PurchaseOrderLogic to = null;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                conn.BeginTransaction();

                if (string.IsNullOrEmpty(toType))
                {
                    toType = from.Type;
                }

                    to = new PurchaseOrderLogic();

                to.SetDependencies(appConfig, userSession);

                if (string.IsNullOrEmpty(newLocationId))
                {
                    newLocationId = from.OfficeLocationId;
                }
                if (string.IsNullOrEmpty(newWarehouseId))
                {
                    newWarehouseId = from.WarehouseId;
                }

                OfficeLocationLogic location = new OfficeLocationLogic();
                location.SetDependencies(appConfig, userSession);
                location.LocationId = newLocationId;
                await location.LoadAsync<PurchaseOrderLogic>();

                string fromId = from.GetPrimaryKeys()[0].ToString();

                //use reflection to copy all peroperties from Quote to Order
                PropertyInfo[] fromProperties = from.GetType().GetProperties();
                PropertyInfo[] toProperties = to.GetType().GetProperties();
                foreach (PropertyInfo fromProperty in fromProperties)
                {
                    if (fromProperty.IsDefined(typeof(FwLogicPropertyAttribute)))
                    {
                        foreach (Attribute attribute in fromProperty.GetCustomAttributes())
                        {
                            if (attribute.GetType() == typeof(FwLogicPropertyAttribute))
                            {
                                foreach (PropertyInfo toProperty in toProperties)
                                {
                                    if (toProperty.Name.Equals(fromProperty.Name))
                                    {
                                        toProperty.SetValue(to, fromProperty.GetValue(from));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //manually set these fields after the reflection copy
                to.Type = toType;
                to.SetPrimaryKeys(new string[] { "" });
                to.OutDeliveryId = "";
                to.InDeliveryId = "";
                to.BillToAddressId = "";
                to.TaxId = "";
                to.OfficeLocationId = newLocationId;
                to.WarehouseId = newWarehouseId;

                if (copyMode.Equals(PurchaseOrderCopyMode.PurchaseOrder))
                {
                    ((PurchaseOrderLogic)to).OrderNumber = ((QuoteLogic)from).QuoteNumber;
                    if (!location.UseSameNumberForQuoteAndOrder.GetValueOrDefault(false))
                    {
                        ((PurchaseOrderLogic)to).OrderNumber = await AppFunc.GetNextModuleCounterAsync(appConfig, userSession, RwConstants.MODULE_ORDER, newLocationId, conn);
                    }
                    ((PurchaseOrderLogic)to).SourceQuoteId = fromId;
                }
                else if (copyMode.Equals(PurchaseOrderCopyMode.NewVersion))
                {
                    ((PurchaseOrderLogic)to).QuoteNumber = ((PurchaseOrderLogic)from).QuoteNumber;

                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select newversionno = (max(o.versionno) + 1)");
                    qry.Add(" from  dealorder o with (nolock)");
                    qry.Add(" where o.orderno   = @quoteno");
                    qry.Add(" and   o.ordertype = @ordertype");
                    qry.AddParameter("@quoteno", ((PurchaseOrderLogic)from).QuoteNumber);
                    qry.AddParameter("@ordertype", RwConstants.ORDER_TYPE_QUOTE);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                    if (dt.TotalRows > 0)
                    {
                        ((PurchaseOrderLogic)to).VersionNumber = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("newversionno")].ToString());
                    }
                }


                //save the new 
                await to.SaveAsync(original: null, conn: conn);

                string toId = to.GetPrimaryKeys()[0].ToString();

                // copy all items
                BrowseRequest itemBrowseRequest = new BrowseRequest();
                itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                itemBrowseRequest.uniqueids.Add("OrderId", fromId);
                itemBrowseRequest.uniqueids.Add("NoAvailabilityCheck", true);

                PurchaseOrderLogic itemSelector = new PurchaseOrderLogic();
                itemSelector.SetDependencies(appConfig, userSession);
                List<PurchaseOrderLogic> items = await itemSelector.SelectAsync<PurchaseOrderLogic>(itemBrowseRequest, conn);

                // dictionary of ID's to map old OrderItemId value to new OrderItemId for parents
                Dictionary<string, string> ids = new Dictionary<string, string>();

                foreach (OrderItemLogic i in items)
                {
                    string oldId = i.OrderItemId;
                    string newId = "";
                    i.SetDependencies(appConfig, userSession);
                    i.OrderId = toId;
                    i.OrderItemId = "";
                    if (!string.IsNullOrEmpty(i.ParentId))
                    {
                        if (!(i.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || i.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || i.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER)))
                        {
                            string newParentId = "";
                            ids.TryGetValue(i.ParentId, out newParentId);
                            i.ParentId = newParentId;
                        }
                    }

                    if (!string.IsNullOrEmpty(i.NestedOrderItemId))
                    {
                        string newGrandParentId = "";
                        ids.TryGetValue(i.NestedOrderItemId, out newGrandParentId);
                        i.NestedOrderItemId = newGrandParentId;
                    }

                    i.copying = true; // don't perform the typical checking in BeforeSaves
                    await i.SaveAsync(conn: conn);
                    newId = i.OrderItemId;
                    ids.Add(oldId, newId);
                }


                // copy all Notes
                BrowseRequest noteBrowseRequest = new BrowseRequest();
                noteBrowseRequest.uniqueids = new Dictionary<string, object>();
                noteBrowseRequest.uniqueids.Add("OrderId", fromId);

                OrderNoteLogic noteSelector = new OrderNoteLogic();
                noteSelector.SetDependencies(appConfig, userSession);
                List<OrderNoteLogic> notes = await noteSelector.SelectAsync<OrderNoteLogic>(noteBrowseRequest, conn);

                foreach (OrderNoteLogic n in notes)
                {
                    n.SetDependencies(appConfig, userSession);
                    n.OrderId = toId;
                    n.OrderNoteId = "";
                    await n.SaveAsync(conn: conn);
                }

                //copy contacts
                BrowseRequest contactBrowseRequest = new BrowseRequest();
                contactBrowseRequest.uniqueids = new Dictionary<string, object>();
                contactBrowseRequest.uniqueids.Add("OrderId", fromId);

                OrderContactLogic contactSelector = new OrderContactLogic();
                contactSelector.SetDependencies(appConfig, userSession);
                List<OrderContactLogic> contacts = await contactSelector.SelectAsync<OrderContactLogic>(contactBrowseRequest, conn);

                foreach (OrderContactLogic n in contacts)
                {
                    if (n.ContactOnOrder.GetValueOrDefault(false))  // only create the record on the New Order if assigned on Orig Order
                    {
                        BrowseRequest contactCheckBrowseRequest = new BrowseRequest();
                        contactCheckBrowseRequest.uniqueids = new Dictionary<string, object>();
                        contactCheckBrowseRequest.uniqueids.Add("OrderId", toId);

                        OrderContactLogic contactCheckSelector = new OrderContactLogic();
                        contactCheckSelector.SetDependencies(appConfig, userSession);
                        List<OrderContactLogic> contactCheck = await contactCheckSelector.SelectAsync<OrderContactLogic>(contactCheckBrowseRequest, conn);

                        bool contactExists = (contactCheck.Count > 0);

                        if (!contactExists)  // only create the record on the New Order if not already on the new Order
                        {
                            n.SetDependencies(appConfig, userSession);
                            n.OrderId = toId;
                            n.OrderContactId = null;
                            await n.SaveAsync(conn: conn);
                        }
                    }
                }

                //copy multi po's/


                // copy all documents
                BrowseRequest documentBrowseRequest = new BrowseRequest();
                documentBrowseRequest.uniqueids = new Dictionary<string, object>();
                documentBrowseRequest.uniqueids.Add("UniqueId1", fromId);

                AppDocumentLogic documentSelector = new AppDocumentLogic();
                documentSelector.SetDependencies(appConfig, userSession);
                List<AppDocumentLogic> documents = await documentSelector.SelectAsync<AppDocumentLogic>(documentBrowseRequest, conn);

                foreach (AppDocumentLogic n in documents)
                {

                    if (copyMode.Equals(QuoteOrderCopyMode.QuoteToOrder))
                    {
                        OrderDocumentLogic newDoc = n.MakeCopy<OrderDocumentLogic>();
                        newDoc.SetDependencies(appConfig, userSession);
                        newDoc.OrderId = toId;
                        newDoc.DocumentId = "";
                        await newDoc.SaveAsync(conn: conn);
                    }
                    else if (copyMode.Equals(QuoteOrderCopyMode.NewVersion))
                    {
                        QuoteDocumentLogic newDoc = n.MakeCopy<QuoteDocumentLogic>();
                        newDoc.SetDependencies(appConfig, userSession);
                        newDoc.QuoteId = toId;
                        newDoc.DocumentId = "";
                        await newDoc.SaveAsync(conn: conn);
                    }
                    else if (copyMode.Equals(QuoteOrderCopyMode.Copy))
                    {
                        if (from is OrderLogic)
                        {
                            OrderDocumentLogic newDoc = n.MakeCopy<OrderDocumentLogic>();
                            newDoc.SetDependencies(appConfig, userSession);
                            newDoc.OrderId = toId;
                            newDoc.DocumentId = "";
                            await newDoc.SaveAsync(conn: conn);
                        }
                        else if (from is QuoteLogic)
                        {
                            QuoteDocumentLogic newDoc = n.MakeCopy<QuoteDocumentLogic>();
                            newDoc.SetDependencies(appConfig, userSession);
                            newDoc.QuoteId = toId;
                            newDoc.DocumentId = "";
                            await newDoc.SaveAsync(conn: conn);
                        }
                    }
                }


                if (copyMode.Equals(QuoteOrderCopyMode.QuoteToOrder))
                {
                    //set the original Quote to Ordered, update pointer to new OrderId
                    QuoteLogic q2 = new QuoteLogic();
                    q2.SetDependencies(appConfig, userSession);
                    q2.QuoteId = fromId;
                    q2.Status = RwConstants.QUOTE_STATUS_ORDERED;
                    q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                    q2.ConvertedToOrderId = toId;
                    await q2.SaveAsync(original: from, conn: conn);
                }
                else if (copyMode.Equals(QuoteOrderCopyMode.NewVersion))
                {
                    //set the original Quote to Closed
                    QuoteLogic q2 = new QuoteLogic();
                    q2.SetDependencies(appConfig, userSession);
                    q2.QuoteId = fromId;
                    q2.Status = RwConstants.QUOTE_STATUS_CLOSED;
                    q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                    await q2.SaveAsync(original: from, conn: conn);
                }

                conn.CommitTransaction();

            }

            return to;
        }
    }
}

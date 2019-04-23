using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.OrderItem;
using WebApi.Modules.Home.SubPurchaseOrderItem;
using WebLibrary;

namespace WebApi.Modules.Home.Order
{

    public class OrderDatesAndTimesChange
    {
        public string OrderId { get; set; }
        public string OldPickDate { get; set; }
        public string NewPickDate { get; set; }
        public string OldPickTime { get; set; }
        public string NewPickTime { get; set; }
        public string OldEstimatedStartDate { get; set; }
        public string NewEstimatedStartDate { get; set; }
        public string OldEstimatedStartTime { get; set; }
        public string NewEstimatedStartTime { get; set; }
        public string OldEstimatedStopDate { get; set; }
        public string NewEstimatedStopDate { get; set; }
        public string OldEstimatedStopTime { get; set; }
        public string NewEstimatedStopTime { get; set; }
    }

    public class CreatePoWorksheetSessionRequest
    {
        public string OrderId;
        public string RecType;
        public string VendorId;
        public string ContactId;
        public string RateType;
        public string BillingCycleId;
        public DateTime? RequiredDate;
        public string RequiredTime;
        public DateTime? FromDate;
        public DateTime? ToDate;
        public string DeliveryId;
        public bool? AdjustContractDates;
    }

    public class CreatePoWorksheetSessionResponse : TSpStatusReponse
    {
        public string SessionId;
    }

    public class ModifyPoWorksheetSessionRequest
    {
        public string OrderId;
        public string PurchaseOrderId;
        public string RecType;
    }

    public class ModifyPoWorksheetSessionResponse : TSpStatusReponse
    {
        public string SessionId;
        public string VendorId;
        public string Vendor;
        public string ContactId;
        public string Contact;
        public string RateType;
        public string BillingCycleId;
        public string BillingCycle;
        public DateTime? RequiredDate;
        public string RequiredTime;
        public DateTime? FromDate;
        public DateTime? ToDate;
        public string DeliveryId;
        public bool? AdjustContractDates;
    }


    public class CompletePoWorksheetSessionRequest
    {
        public string SessionId;
    }

    public class CompletePoWorksheetSessionResponse : TSpStatusReponse
    {
        public string PurchaseOrderId;
    }

    public class PoWorksheetSessionTotalsResponse : TSpStatusReponse
    {
        public double? GrossTotal;
        public double? Discount;
        public double? SubTotal;
        public double? Tax;
        public double? Total;
    }

    public class CopyTemplateRequest
    {
        public List<string> TemplateIds { get; set; } = new List<string>();
        public string OrderId { get; set; }
        public string RecType { get; set; }
    }

    public class CopyTemplateResponse : TSpStatusReponse
    {
    }


    public class CopyOrderItemsRequest
    {
        public string OrderId { get; set; }
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }

    public class CopyOrderItemsResponse : TSpStatusReponse
    {
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }


    public static class OrderFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> InsertPackage(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            string newOrderItemId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "insertpackage", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@packageid", SqlDbType.NVarChar, ParameterDirection.Input, oi.InventoryId);
                qry.AddParameter("@nestedmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, oi.WarehouseId);
                qry.AddParameter("@catalogid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, oi.RecType);
                qry.AddParameter("@qty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@forcenewrecord", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                qry.AddParameter("@primarymasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                newOrderItemId = qry.GetParameter("@primarymasteritemid").ToString();
            }
            return newOrderItemId;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<bool> UpdatePackageQuantities(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepackageqtys", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderItemId);
                qry.AddParameter("@newqty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@rowsummarized", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }

        //-------------------------------------------------------------------------------------------------------

        public static async Task<bool> UpdateOrderItemDatesAndTimes(FwApplicationConfig appConfig, FwUserSession userSession, OrderDatesAndTimesChange change, FwSqlConnection conn = null)
        {
            bool success = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "updatemasteritemdatesandtimes", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, change.OrderId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);

            //pick
            qry.AddParameter("@olddatepick", SqlDbType.Date, ParameterDirection.Input, change.OldPickDate);
            qry.AddParameter("@newdatepick", SqlDbType.Date, ParameterDirection.Input, change.NewPickDate);
            qry.AddParameter("@oldtimepick", SqlDbType.NVarChar, ParameterDirection.Input, change.OldPickTime);
            qry.AddParameter("@newtimepick", SqlDbType.NVarChar, ParameterDirection.Input, change.NewPickTime);

            //from
            qry.AddParameter("@olddatefrom", SqlDbType.Date, ParameterDirection.Input, change.OldEstimatedStartDate);
            qry.AddParameter("@newdatefrom", SqlDbType.Date, ParameterDirection.Input, change.NewEstimatedStartDate);
            qry.AddParameter("@oldtimefrom", SqlDbType.NVarChar, ParameterDirection.Input, change.OldEstimatedStartTime);
            qry.AddParameter("@newtimefrom", SqlDbType.NVarChar, ParameterDirection.Input, change.NewEstimatedStartTime);

            //to
            qry.AddParameter("@olddateto", SqlDbType.Date, ParameterDirection.Input, change.OldEstimatedStopDate);
            qry.AddParameter("@newdateto", SqlDbType.Date, ParameterDirection.Input, change.NewEstimatedStopDate);
            qry.AddParameter("@oldtimeto", SqlDbType.NVarChar, ParameterDirection.Input, change.OldEstimatedStopTime);
            qry.AddParameter("@newtimeto", SqlDbType.NVarChar, ParameterDirection.Input, change.NewEstimatedStopTime);
            await qry.ExecuteNonQueryAsync();
            success = true;
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreatePoWorksheetSessionResponse> StartCreatePoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, CreatePoWorksheetSessionRequest request)
        {
            CreatePoWorksheetSessionResponse response = new CreatePoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startcreatepoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId);
                qry.AddParameter("@ratetype", SqlDbType.NVarChar, ParameterDirection.Input, request.RateType);
                qry.AddParameter("@billperiodid", SqlDbType.NVarChar, ParameterDirection.Input, request.BillingCycleId);
                if (request.RequiredDate != null)
                {
                    qry.AddParameter("@requireddate", SqlDbType.Date, ParameterDirection.Input, request.RequiredDate);
                }
                qry.AddParameter("@requiredtime", SqlDbType.NVarChar, ParameterDirection.Input, request.RequiredTime);
                qry.AddParameter("@rentfromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                if (request.ToDate != null)
                {
                    qry.AddParameter("@renttodate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                }
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@deliveryid", SqlDbType.NVarChar, ParameterDirection.Input, request.DeliveryId);
                qry.AddParameter("@adjustcontractdate", SqlDbType.NVarChar, ParameterDirection.Input, (request.AdjustContractDates.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@contactid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContactId);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.SessionId = qry.GetParameter("@sessionid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ModifyPoWorksheetSessionResponse> StartModifyPoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, ModifyPoWorksheetSessionRequest request)
        {
            ModifyPoWorksheetSessionResponse response = new ModifyPoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startmodifypoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, request.PurchaseOrderId);
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@vendor", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@ratetype", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@billperiodid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@billperiod", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@requireddate", SqlDbType.Date, ParameterDirection.Output);
                qry.AddParameter("@requiredtime", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@rentfromdate", SqlDbType.Date, ParameterDirection.Output);
                qry.AddParameter("@renttodate", SqlDbType.Date, ParameterDirection.Output);
                qry.AddParameter("@deliveryid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@adjustcontractdate", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@contactid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@contact", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.SessionId = qry.GetParameter("@sessionid").ToString();
                response.VendorId = qry.GetParameter("@vendorid").ToString();
                response.Vendor = qry.GetParameter("@vendor").ToString();
                response.RateType = qry.GetParameter("@ratetype").ToString();
                response.BillingCycleId = qry.GetParameter("@billperiodid").ToString();
                response.BillingCycle = qry.GetParameter("@billperiod").ToString();
                response.RequiredDate = qry.GetParameter("@requireddate").ToDateTime();
                response.RequiredTime = qry.GetParameter("@requiredtime").ToString();
                response.FromDate = qry.GetParameter("@rentfromdate").ToDateTime();
                response.ToDate = qry.GetParameter("@renttodate").ToDateTime();
                response.DeliveryId = qry.GetParameter("@deliveryid").ToString();
                response.AdjustContractDates = FwConvert.ToBoolean(qry.GetParameter("@adjustcontractdate").ToString());
                response.ContactId = qry.GetParameter("@contactid").ToString();
                response.Contact = qry.GetParameter("@contact").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNonePoWorksheetItemResponse> SelectAllNonePoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNonePoWorksheetItemResponse response = new SelectAllNonePoWorksheetItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallpoworksheetitems", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
        public static async Task<SelectAllNonePoWorksheetItemResponse> SelectAllPoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNonePoWorksheetItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNonePoWorksheetItemResponse> SelectNonePoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNonePoWorksheetItem(appConfig, userSession, sessionId, false);
        }
        //-------------------------------------------------------------------------------------------------------



        public static async Task<CompletePoWorksheetSessionResponse> CompletePoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            CompletePoWorksheetSessionResponse response = new CompletePoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "completepoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.PurchaseOrderId = qry.GetParameter("@poid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PoWorksheetSessionTotalsResponse> GetPoWorksheetSessionTotals(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            PoWorksheetSessionTotalsResponse response = new PoWorksheetSessionTotalsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "getpoworksheetitemtotals", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@grosstotal", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@discount", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@subtotal", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@tax", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@total", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.GrossTotal = qry.GetParameter("@grosstotal").ToDouble();
                response.Discount = qry.GetParameter("@discount").ToDouble();
                response.SubTotal = qry.GetParameter("@subtotal").ToDouble();
                response.Tax = qry.GetParameter("@tax").ToDouble();
                response.Total = qry.GetParameter("@total").ToDouble();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CopyTemplateResponse> CopyTemplateAsync(FwApplicationConfig appConfig, FwUserSession userSession, CopyTemplateRequest request)
        {
            CopyTemplateResponse response = new CopyTemplateResponse();
            List<string> templateIds = request.TemplateIds;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                for (int i = 0; i <= templateIds.Count - 1; i++)
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, "copymasteritems", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.AddParameter("@fromorderid", SqlDbType.NVarChar, ParameterDirection.Input, templateIds[i]);
                        qry.AddParameter("@frominvoiceid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        qry.AddParameter("@toorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                        qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                        qry.AddParameter("@combinesubs", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                        await qry.ExecuteNonQueryAsync();
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CopyOrderItemsResponse> CopyOrderItems (FwApplicationConfig appConfig, FwUserSession userSession, CopyOrderItemsRequest request)
        {
            CopyOrderItemsResponse response = new CopyOrderItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                foreach(string OrderItemId in request.OrderItemIds)
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, "copymasteritem", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                        qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, OrderItemId);
                        //qry.AddParameter("@poorderid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        //qry.AddParameter("@pomasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                        //qry.AddParameter("@updateitemorder", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                        //qry.AddParameter("@newparentid", SqlDbType.NVarChar, ParameterDirection.Input, "");  // if not specified, copy from source
                        //qry.AddParameter("@copyfrommasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, "");  //used for forcing itemorder
                        //qry.AddParameter("@issubs", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                        //qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
                        qry.AddParameter("@newmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                        await qry.ExecuteNonQueryAsync();
                        response.OrderItemIds.Add(qry.GetParameter("@newmasteritemid").ToString());
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

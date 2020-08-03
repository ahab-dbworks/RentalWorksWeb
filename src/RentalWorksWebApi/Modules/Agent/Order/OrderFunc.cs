using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.OrderItem;
using WebApi.Modules.HomeControls.SubPurchaseOrderItem;
using WebApi;
using WebApi.Modules.Agent.Quote;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FwStandard.AppManager;
using WebApi.Modules.HomeControls.OrderNote;
using WebApi.Modules.HomeControls.OrderContact;
using WebApi.Modules.HomeControls.InventoryAvailability;
using FwStandard.Grids.AppDocument;

namespace WebApi.Modules.Agent.Order
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
        public string CurrencyId;
        public string BillingCycleId;
        public DateTime? RequiredDate;
        public string RequiredTime;
        public DateTime? FromDate;
        public DateTime? ToDate;
        public string DeliveryId;
        public bool? AdjustContractDates;
    }

    public class CreatePoWorksheetSessionResponse : TSpStatusResponse
    {
        public string SessionId;
    }

    public class ModifyPoWorksheetSessionRequest
    {
        public string OrderId;
        public string PurchaseOrderId;
        public string RecType;
    }

    public class ModifyPoWorksheetSessionResponse : TSpStatusResponse
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

    public class UpdatePoWorksheetSessionRequest
    {
        public string RecType;
        public string VendorId;
        public string ContactId;
        public string RateType;
        public string CurrencyId;
        public string BillingCycleId;
        public DateTime? RequiredDate;
        public string RequiredTime;
        public DateTime? FromDate;
        public DateTime? ToDate;
        public string DeliveryId;
        public bool? AdjustContractDates;
    }
    public class UpdatePoWorksheetSessionResponse : TSpStatusResponse
    {
    }

    public class CompletePoWorksheetSessionRequest
    {
        public string SessionId;
    }

    public class CompletePoWorksheetSessionResponse : TSpStatusResponse
    {
        public string PurchaseOrderId;
    }

    public class PoWorksheetSessionTotalsResponse : TSpStatusResponse
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

    public class CopyTemplateResponse : TSpStatusResponse
    {
    }


    public class CopyOrderItemsRequest
    {
        public string OrderId { get; set; }
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }

    public class CopyOrderItemsResponse : TSpStatusResponse
    {
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }


    public class OrderDateAndTime
    {
        public string OrderTypeDateTypeId { get; set; }
        //public string ActivityType { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public bool? IsProductionActivity { get; set; }
        public bool? IsMilestone { get; set; }
    }

    public class ApplyOrderDatesAndTimesRequest
    {
        public string OrderId;
        public List<OrderDateAndTime> DatesAndTimes = new List<OrderDateAndTime>();
    }

    public class ApplyOrderDatesAndTimesResponse : TSpStatusResponse
    {
    }

    public class UpdateOrderItemRatesRequest
    {
        public string OrderId { get; set; }
        public string RateType { get; set; }
    }

    public class ChangeOrderOfficeLocationRequest
    {
        public string OfficeLocationId { get; set; }
        public string WarehouseId { get; set; }
    }
    public class ChangeOrderOfficeLocationResponse : TSpStatusResponse
    {
        public OrderBaseLogic quoteOrOrder { get; set; }
    }
    public class ChangeOrderStatusRequest
    {
        public string OrderId { get; set; }
        public string NewStatus { get; set; }
    }
    public class ChangeOrderStatusResponse : TSpStatusResponse
    {
    }

    public class OrderMessagesRequest
    {
        public string OrderId { get; set; }
    }
    public class OrderMessage
    {
        public string Message { get; set; }
        public bool PreventCheckOut { get; set; }
    }
    public class OrderMessagesResponse : TSpStatusResponse
    {
        public List<OrderMessage> Messages = new List<OrderMessage>();
    }


    public enum QuoteOrderCopyMode
    {
        QuoteToOrder,
        NewVersion,
        Copy
    }

    public class QuoteToOrderRequest
    {
        [Required]
        public string QuoteId { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string WarehouseId { get; set; }
    }
    public class QuoteToOrderResponse : TSpStatusResponse
    {
        public OrderLogic Order { get; set; }
    }

    public class QuoteNewVersionResponse : TSpStatusResponse
    {
        public QuoteLogic NewVersion { get; set; }
    }
    public class ReserveUnreserveQuoteResponse : TSpStatusResponse
    {
        public QuoteLogic Quote { get; set; } = null;
    }
    public class CancelUncancelQuoteResponse : TSpStatusResponse
    {
        public QuoteLogic Quote { get; set; } = null;
    }

    public class GetCustomRatesRequest
    {
        public string OrderId { get; set; }
        public string InventoryId { get; set; }
        public string RecType { get; set; }
    }

    public class CustomRates
    {
        public decimal? DailyRate { get; set; }
        public decimal? WeeklyRate { get; set; }
        public decimal? Week2Rate { get; set; }
        public decimal? Week3Rate { get; set; }
        public decimal? Week4Rate { get; set; }
        public decimal? Week5Rate { get; set; }
        public decimal? MonthlyRate { get; set; }
    }
    public class GetCustomRatesResponse : TSpStatusResponse
    {
        public bool? HasDiscount { get; set; }
        public bool? ApplyDiscountToCustomRate { get; set; }
        public CustomRates CustomRates { get; set; }
        public decimal? DaysPerWeek { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? MarkupPercent { get; set; }
        public decimal? MarginPercent { get; set; }
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
                qry.AddParameter("@rowsummarized", SqlDbType.NVarChar, ParameterDirection.Input, oi.RowsRolledUp);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> UpdatePackageSubQuantities(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepackageqtyssub", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderItemId);
                qry.AddParameter("@qtyordered", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@newsubqty", SqlDbType.NVarChar, ParameterDirection.Input, oi.SubQuantity);
                qry.AddParameter("@rowsummarized", SqlDbType.NVarChar, ParameterDirection.Input, oi.RowsRolledUp);
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
            if (!string.IsNullOrEmpty(change.OldPickDate)) { qry.AddParameter("@olddatepick", SqlDbType.Date, ParameterDirection.Input, change.OldPickDate); }
            if (!string.IsNullOrEmpty(change.NewPickDate)) { qry.AddParameter("@newdatepick", SqlDbType.Date, ParameterDirection.Input, change.NewPickDate); }
            qry.AddParameter("@oldtimepick", SqlDbType.NVarChar, ParameterDirection.Input, change.OldPickTime);
            qry.AddParameter("@newtimepick", SqlDbType.NVarChar, ParameterDirection.Input, change.NewPickTime);

            //from
            if (!string.IsNullOrEmpty(change.OldEstimatedStartDate)) { qry.AddParameter("@olddatefrom", SqlDbType.Date, ParameterDirection.Input, change.OldEstimatedStartDate); }
            if (!string.IsNullOrEmpty(change.NewEstimatedStartDate)) { qry.AddParameter("@newdatefrom", SqlDbType.Date, ParameterDirection.Input, change.NewEstimatedStartDate); }
            qry.AddParameter("@oldtimefrom", SqlDbType.NVarChar, ParameterDirection.Input, change.OldEstimatedStartTime);
            qry.AddParameter("@newtimefrom", SqlDbType.NVarChar, ParameterDirection.Input, change.NewEstimatedStartTime);

            //to
            if (!string.IsNullOrEmpty(change.OldEstimatedStopDate)) { qry.AddParameter("@olddateto", SqlDbType.Date, ParameterDirection.Input, change.OldEstimatedStopDate); }
            if (!string.IsNullOrEmpty(change.NewEstimatedStopDate)) { qry.AddParameter("@newdateto", SqlDbType.Date, ParameterDirection.Input, change.NewEstimatedStopDate); }
            qry.AddParameter("@oldtimeto", SqlDbType.NVarChar, ParameterDirection.Input, change.OldEstimatedStopTime);
            qry.AddParameter("@newtimeto", SqlDbType.NVarChar, ParameterDirection.Input, change.NewEstimatedStopTime);
            await qry.ExecuteNonQueryAsync();
            success = true;
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> UpdateOrderItemExtendedAllASync(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, FwSqlConnection conn = null)
        {
            bool saved = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "updatemasteritemextendedall", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
            await qry.ExecuteNonQueryAsync();
            saved = true;
            return saved;
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
                qry.AddParameter("@currencyid", SqlDbType.NVarChar, ParameterDirection.Input, request.CurrencyId);
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
        public static async Task<UpdatePoWorksheetSessionResponse> UpdatePoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, UpdatePoWorksheetSessionRequest request)
        {
            UpdatePoWorksheetSessionResponse response = new UpdatePoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId);
                qry.AddParameter("@contactid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContactId);
                qry.AddParameter("@ratetype", SqlDbType.NVarChar, ParameterDirection.Input, request.RateType);
                qry.AddParameter("@currencyid", SqlDbType.NVarChar, ParameterDirection.Input, request.CurrencyId);
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
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
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
        public static async Task<CopyOrderItemsResponse> CopyOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, CopyOrderItemsRequest request)
        {
            CopyOrderItemsResponse response = new CopyOrderItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                foreach (string OrderItemId in request.OrderItemIds)
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
        public static async Task<GetCustomRatesResponse> GetCustomRates(FwApplicationConfig appConfig, FwUserSession userSession, GetCustomRatesRequest request)
        {
            GetCustomRatesResponse response = new GetCustomRatesResponse();
            CustomRates rates = new CustomRates();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add(" from dbo.webgetcustomrates(@orderid, @masterid, @rectype)");
                qry.AddParameter("@orderid", request.OrderId);
                qry.AddParameter("@masterid", request.InventoryId);
                qry.AddParameter("@rectype", request.RecType);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                if (dt.TotalRows > 0)
                {
                    rates.DailyRate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("dailyrate")].ToString());
                    rates.WeeklyRate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("weeklyrate")].ToString());
                    rates.Week2Rate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("week2rate")].ToString());
                    rates.Week3Rate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("week3rate")].ToString());
                    rates.Week4Rate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("week4rate")].ToString());
                    rates.Week5Rate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("week5rate")].ToString());
                    rates.MonthlyRate = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("monthlyrate")].ToString());

                    response.DaysPerWeek = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("daysinwk")].ToString());
                    response.HasDiscount = FwConvert.ToBoolean(dt.Rows[0][dt.GetColumnNo("hasdiscount")].ToString());
                    response.ApplyDiscountToCustomRate = FwConvert.ToBoolean(dt.Rows[0][dt.GetColumnNo("applydiscounttocustomrate")].ToString());
                    response.DiscountPercent = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("discountpct")].ToString());
                    response.MarkupPercent = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("markuppct")].ToString());
                    response.MarginPercent = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("marginpct")].ToString());
                    response.success = true;
                    response.CustomRates = rates;
                }
                else
                {
                    response.success = false;
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ApplyOrderDatesAndTimesResponse> ApplyOrderDatesAndTimes(FwApplicationConfig appConfig, FwUserSession userSession, ApplyOrderDatesAndTimesRequest request, FwSqlConnection conn = null)
        {
            ApplyOrderDatesAndTimesResponse response = new ApplyOrderDatesAndTimesResponse();
            response.success = true;  // initialize to true
            string session1Id = AppFunc.GetNextIdAsync(appConfig, conn).Result;
            string session2Id = AppFunc.GetNextIdAsync(appConfig, conn).Result;

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            if (response.success)
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "snapshotorderdatesandtimesweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@session1id", SqlDbType.NVarChar, ParameterDirection.Input, session1Id);
                qry.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }

            if (response.success) // continue if no errors occurred above
            {
                foreach (OrderDateAndTime dt in request.DatesAndTimes)
                {
                    if (response.success) // continue while no errors have occurred
                    {
                        FwSqlCommand qryDt = new FwSqlCommand(conn, "saveorderdateandtimeweb", appConfig.DatabaseSettings.QueryTimeout);
                        qryDt.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                        qryDt.AddParameter("@ordertypedatetypeid", SqlDbType.NVarChar, ParameterDirection.Input, dt.OrderTypeDateTypeId);
                        qryDt.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
                        qryDt.AddParameter("@date", SqlDbType.Date, ParameterDirection.Input, dt.Date);
                        qryDt.AddParameter("@time", SqlDbType.NVarChar, ParameterDirection.Input, dt.Time);
                        qryDt.AddParameter("@productionactivity", SqlDbType.NVarChar, ParameterDirection.Input, dt.IsProductionActivity);
                        qryDt.AddParameter("@milestone", SqlDbType.NVarChar, ParameterDirection.Input, dt.IsMilestone);
                        qryDt.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                        qryDt.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                        await qryDt.ExecuteNonQueryAsync();
                        response.status = qryDt.GetParameter("@status").ToInt32();
                        response.success = (response.status == 0);
                        response.msg = qryDt.GetParameter("@msg").ToString();
                    }
                }
            }

            if (response.success) // continue if no errors occurred above
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "applyorderdatesandtimesweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@session1id", SqlDbType.NVarChar, ParameterDirection.Input, session1Id);
                qry.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
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
        public static async Task<bool> UpdateOrderItemRates(FwApplicationConfig appConfig, FwUserSession userSession, UpdateOrderItemRatesRequest request, FwSqlConnection conn = null)
        {
            bool saved = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            using (FwSqlCommand qry = new FwSqlCommand(conn, "updateorderrates", appConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@ratetype", SqlDbType.NVarChar, ParameterDirection.Input, request.RateType);
                await qry.ExecuteNonQueryAsync();
                saved = true;
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TSpStatusResponse> AfterSaveQuoteOrder(FwApplicationConfig appConfig, FwUserSession userSession, string id, FwSqlConnection conn = null)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            using (FwSqlCommand qry = new FwSqlCommand(conn, "aftersavequoteorderweb", appConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, id);
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
        public static async Task<ChangeOrderStatusResponse> ChangeStatus(FwApplicationConfig appConfig, FwUserSession userSession, ChangeOrderStatusRequest request, FwSqlConnection conn = null)
        {
            ChangeOrderStatusResponse response = new ChangeOrderStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "changeorderstatus", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
            qry.AddParameter("@newstatus", SqlDbType.NVarChar, ParameterDirection.Input, request.NewStatus);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            await qry.ExecuteNonQueryAsync();
            response.success = true;

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<OrderMessagesResponse> GetOrderMessages(FwApplicationConfig appConfig, FwUserSession userSession, OrderMessagesRequest request)
        {
            OrderMessagesResponse response = new OrderMessagesResponse();

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getordermessagesweb", appConfig.DatabaseSettings.QueryTimeout))
                {
                    response.success = true;
                    FwJsonDataTable dt = null;
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddColumn("message", "Message", FwDataTypes.Text, true, false, false);
                    qry.AddColumn("preventcheckout", "PreventCheckOut", FwDataTypes.Text, true, false, false);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                    foreach (List<object> row in dt.Rows)
                    {
                        OrderMessage m = new OrderMessage();
                        m.Message = row[0].ToString();
                        m.PreventCheckOut = FwConvert.ToBoolean(row[1].ToString());
                        response.Messages.Add(m);
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<OrderBaseLogic> CopyQuoteOrder(FwApplicationConfig appConfig, FwUserSession userSession, OrderBaseLogic from, string toType, QuoteOrderCopyMode copyMode, string newLocationId = "", string newWarehouseId = "")
        {
            OrderBaseLogic to = null;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                conn.BeginTransaction();

                if (string.IsNullOrEmpty(toType))
                {
                    toType = from.Type;
                }
                if (toType.Equals(RwConstants.ORDER_TYPE_QUOTE))
                {
                    to = new QuoteLogic();
                }
                else //if (toType.Equals(RwConstants.ORDER_TYPE_ORDER))
                {
                    to = new OrderLogic();
                }
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
                await location.LoadAsync<OrderLogic>();

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

                if (copyMode.Equals(QuoteOrderCopyMode.QuoteToOrder))
                {
                    ((OrderLogic)to).OrderNumber = ((QuoteLogic)from).QuoteNumber;
                    if (!location.UseSameNumberForQuoteAndOrder.GetValueOrDefault(false))
                    {
                        ((OrderLogic)to).OrderNumber = await AppFunc.GetNextModuleCounterAsync(appConfig, userSession, RwConstants.MODULE_ORDER, newLocationId, conn);
                    }
                }
                else if (copyMode.Equals(QuoteOrderCopyMode.NewVersion))
                {
                    ((QuoteLogic)to).QuoteNumber = ((QuoteLogic)from).QuoteNumber;

                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select newversionno = (max(o.versionno) + 1)");
                    qry.Add(" from  dealorder o with (nolock)");
                    qry.Add(" where o.orderno   = @quoteno");
                    qry.Add(" and   o.ordertype = @ordertype");
                    qry.AddParameter("@quoteno", ((QuoteLogic)from).QuoteNumber);
                    qry.AddParameter("@ordertype", RwConstants.ORDER_TYPE_QUOTE);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                    if (dt.TotalRows > 0)
                    {
                        ((QuoteLogic)to).VersionNumber = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("newversionno")].ToString());
                    }
                }


                //save the new 
                await to.SaveAsync(original: null, conn: conn);

                // copy all items
                BrowseRequest itemBrowseRequest = new BrowseRequest();
                itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                itemBrowseRequest.uniqueids.Add("OrderId", from.GetPrimaryKeys()[0]);
                itemBrowseRequest.uniqueids.Add("NoAvailabilityCheck", true);

                OrderItemLogic itemSelector = new OrderItemLogic();
                itemSelector.SetDependencies(appConfig, userSession);
                List<OrderItemLogic> items = await itemSelector.SelectAsync<OrderItemLogic>(itemBrowseRequest, conn);

                // dictionary of ID's to map old OrderItemId value to new OrderItemId for parents
                Dictionary<string, string> ids = new Dictionary<string, string>();

                foreach (OrderItemLogic i in items)
                {
                    string oldId = i.OrderItemId;
                    string newId = "";
                    i.SetDependencies(appConfig, userSession);
                    i.OrderId = to.GetPrimaryKeys()[0].ToString();
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
                noteBrowseRequest.uniqueids.Add("OrderId", from.GetPrimaryKeys()[0]);

                OrderNoteLogic noteSelector = new OrderNoteLogic();
                noteSelector.SetDependencies(appConfig, userSession);
                List<OrderNoteLogic> notes = await noteSelector.SelectAsync<OrderNoteLogic>(noteBrowseRequest, conn);

                foreach (OrderNoteLogic n in notes)
                {
                    n.SetDependencies(appConfig, userSession);
                    n.OrderId = to.GetPrimaryKeys()[0].ToString();
                    n.OrderNoteId = "";
                    await n.SaveAsync(conn: conn);
                }

                //copy contacts
                BrowseRequest contactBrowseRequest = new BrowseRequest();
                contactBrowseRequest.uniqueids = new Dictionary<string, object>();
                contactBrowseRequest.uniqueids.Add("OrderId", from.GetPrimaryKeys()[0]);

                OrderContactLogic contactSelector = new OrderContactLogic();
                contactSelector.SetDependencies(appConfig, userSession);
                List<OrderContactLogic> contacts = await contactSelector.SelectAsync<OrderContactLogic>(contactBrowseRequest, conn);

                foreach (OrderContactLogic n in contacts)
                {
                    if (n.ContactOnOrder.GetValueOrDefault(false))  // only create the record on the New Order if assigned on Orig Order
                    {
                        n.SetDependencies(appConfig, userSession);
                        n.OrderId = to.GetPrimaryKeys()[0].ToString();
                        n.OrderContactId = null;
                        await n.SaveAsync(conn: conn);
                    }
                }

                //copy multi po's/


                // copy all documents
                BrowseRequest documentBrowseRequest = new BrowseRequest();
                documentBrowseRequest.uniqueids = new Dictionary<string, object>();
                documentBrowseRequest.uniqueids.Add("UniqueId1", from.GetPrimaryKeys()[0]);

                AppDocumentLogic documentSelector = new AppDocumentLogic();
                documentSelector.SetDependencies(appConfig, userSession);
                List<AppDocumentLogic> documents = await documentSelector.SelectAsync<AppDocumentLogic>(documentBrowseRequest, conn);

                foreach (AppDocumentLogic n in documents)
                {

                    if (copyMode.Equals(QuoteOrderCopyMode.QuoteToOrder))
                    {
                        OrderDocumentLogic newDoc = n.MakeCopy<OrderDocumentLogic>();
                        newDoc.SetDependencies(appConfig, userSession);
                        newDoc.OrderId = to.GetPrimaryKeys()[0].ToString();
                        newDoc.DocumentId = "";
                        await newDoc.SaveAsync(conn: conn);
                    }
                    else if (copyMode.Equals(QuoteOrderCopyMode.NewVersion))
                    {
                        QuoteDocumentLogic newDoc = n.MakeCopy<QuoteDocumentLogic>();
                        newDoc.SetDependencies(appConfig, userSession);
                        newDoc.QuoteId = to.GetPrimaryKeys()[0].ToString();
                        newDoc.DocumentId = "";
                        await newDoc.SaveAsync(conn: conn);
                    }
                    else if (copyMode.Equals(QuoteOrderCopyMode.Copy))
                    {
                        if (from is OrderLogic)
                        {
                            OrderDocumentLogic newDoc = n.MakeCopy<OrderDocumentLogic>();
                            newDoc.SetDependencies(appConfig, userSession);
                            newDoc.OrderId = to.GetPrimaryKeys()[0].ToString();
                            newDoc.DocumentId = "";
                            await newDoc.SaveAsync(conn: conn);
                        }
                        else if (from is QuoteLogic)
                        {
                            QuoteDocumentLogic newDoc = n.MakeCopy<QuoteDocumentLogic>();
                            newDoc.SetDependencies(appConfig, userSession);
                            newDoc.QuoteId = to.GetPrimaryKeys()[0].ToString();
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
                    q2.QuoteId = from.GetPrimaryKeys()[0].ToString();
                    q2.Status = RwConstants.QUOTE_STATUS_ORDERED;
                    q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                    q2.RelatedQuoteOrderId = to.GetPrimaryKeys()[0].ToString();
                    await q2.SaveAsync(original: from, conn: conn);
                }
                else if (copyMode.Equals(QuoteOrderCopyMode.NewVersion))
                {
                    //set the original Quote to Closed
                    QuoteLogic q2 = new QuoteLogic();
                    q2.SetDependencies(appConfig, userSession);
                    q2.QuoteId = from.GetPrimaryKeys()[0].ToString();
                    q2.Status = RwConstants.QUOTE_STATUS_CLOSED;
                    q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                    await q2.SaveAsync(original: from, conn: conn);
                }

                conn.CommitTransaction();

            }

            return to;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<QuoteToOrderResponse> QuoteToOrder(FwApplicationConfig appConfig, FwUserSession userSession, QuoteLogic quote, QuoteToOrderRequest request)
        {
            QuoteToOrderResponse response = new QuoteToOrderResponse();
            if (string.IsNullOrEmpty(response.msg))
            {
                if ((!quote.Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) || (!(quote.Status.Equals(RwConstants.QUOTE_STATUS_ACTIVE) || quote.Status.Equals(RwConstants.QUOTE_STATUS_RESERVED))))
                {
                    response.msg = "Only ACTIVE or RESERVED Quotes can be converted to Orders.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                if (string.IsNullOrEmpty(quote.DealId))
                {
                    response.msg = "Deal is required before converting a Quote into an Order.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                if ((!request.LocationId.Equals(quote.OfficeLocationId)) || (!request.WarehouseId.Equals(quote.WarehouseId)))
                {
                    response.msg = "Cannot create an Order from a Quote associated to a different Office/Warehouse.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                OrderLogic order = (OrderLogic)(await CopyQuoteOrder(appConfig, userSession, quote, RwConstants.ORDER_TYPE_ORDER, QuoteOrderCopyMode.QuoteToOrder, request.LocationId, request.WarehouseId));
                response.Order = order;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<QuoteNewVersionResponse> QuoteNewVersion(FwApplicationConfig appConfig, FwUserSession userSession, QuoteLogic quote)
        {
            QuoteNewVersionResponse response = new QuoteNewVersionResponse();

            if (string.IsNullOrEmpty(response.msg))
            {
                if (!quote.Type.Equals(RwConstants.ORDER_TYPE_QUOTE))
                {
                    response.msg = "Only Quotes can have new versions created.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                if (quote.Status.Equals(RwConstants.QUOTE_STATUS_ORDERED))
                {
                    response.msg = "Cannot create new versions of " + RwConstants.QUOTE_STATUS_ORDERED + " Quotes.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                QuoteLogic newVersion = (QuoteLogic)(await CopyQuoteOrder(appConfig, userSession, quote, RwConstants.ORDER_TYPE_QUOTE, QuoteOrderCopyMode.NewVersion));
                response.NewVersion = newVersion;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> QuoteOrderAvailabilityRequestRecalc(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            bool success = false;
            // request availability recalculation on all rental and sale items
            BrowseRequest itemBrowseRequest = new BrowseRequest();
            itemBrowseRequest.uniqueids = new Dictionary<string, object>();
            itemBrowseRequest.uniqueids.Add("OrderId", orderId);
            itemBrowseRequest.uniqueids.Add("NoAvailabilityCheck", true);
            itemBrowseRequest.uniqueids.Add("RecType", RwConstants.RECTYPE_RENTAL + "," + RwConstants.RECTYPE_SALE);

            OrderItemLogic itemSelector = new OrderItemLogic();
            itemSelector.SetDependencies(appConfig, userSession);
            List<OrderItemLogic> items = await itemSelector.SelectAsync<OrderItemLogic>(itemBrowseRequest);

            foreach (OrderItemLogic i in items)
            {
                if ((!string.IsNullOrEmpty(i.InventoryId)) && (!string.IsNullOrEmpty(i.WarehouseId)) && (i.QuantityOrdered != 0))
                {
                    InventoryAvailabilityFunc.RequestRecalc(i.InventoryId, i.WarehouseId, i.InventoryClass);
                }
            }
            success = true;

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReserveUnreserveQuoteResponse> ReserveQuote(FwApplicationConfig appConfig, FwUserSession userSession, QuoteLogic quote)
        {
            ReserveUnreserveQuoteResponse response = new ReserveUnreserveQuoteResponse();

            if (string.IsNullOrEmpty(response.msg))
            {
                if ((!quote.Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) || (!(quote.Status.Equals(RwConstants.QUOTE_STATUS_ACTIVE) || quote.Status.Equals(RwConstants.QUOTE_STATUS_RESERVED))))
                {
                    response.msg = "Only ACTIVE or RESERVED Quotes can be reserved/unreserved.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                if (string.IsNullOrEmpty(quote.DealId))
                {
                    response.msg = "Deal is required before reserving a Quote.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                //update the quote status
                QuoteLogic q2 = new QuoteLogic();
                q2.SetDependencies(appConfig, userSession);
                q2.QuoteId = quote.QuoteId;
                q2.Status = (quote.Status.Equals(RwConstants.QUOTE_STATUS_RESERVED) ? RwConstants.QUOTE_STATUS_ACTIVE : RwConstants.QUOTE_STATUS_RESERVED);
                q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                await q2.SaveAsync(original: quote);
                await QuoteOrderAvailabilityRequestRecalc(appConfig, userSession, quote.QuoteId);
                response.Quote = q2;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CancelUncancelQuoteResponse> CancelQuote(FwApplicationConfig appConfig, FwUserSession userSession, QuoteLogic quote)
        {
            CancelUncancelQuoteResponse response = new CancelUncancelQuoteResponse();

            if (string.IsNullOrEmpty(response.msg))
            {
                if ((!quote.Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) || (!(quote.Status.Equals(RwConstants.QUOTE_STATUS_NEW) || quote.Status.Equals(RwConstants.QUOTE_STATUS_PROSPECT) || quote.Status.Equals(RwConstants.QUOTE_STATUS_ACTIVE) || quote.Status.Equals(RwConstants.QUOTE_STATUS_RESERVED))))
                {
                    response.msg = "Only NEW, PROSPECT, ACTIVE, or RESERVED Quotes can be cancelled.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                //update the quote status
                QuoteLogic q2 = new QuoteLogic();
                q2.SetDependencies(appConfig, userSession);
                q2.QuoteId = quote.QuoteId;
                q2.Status = RwConstants.QUOTE_STATUS_CANCELLED;
                q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                await q2.SaveAsync(original: quote);
                await QuoteOrderAvailabilityRequestRecalc(appConfig, userSession, quote.QuoteId);
                response.Quote = q2;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<CancelUncancelQuoteResponse> UncancelQuote(FwApplicationConfig appConfig, FwUserSession userSession, QuoteLogic quote)
        {
            CancelUncancelQuoteResponse response = new CancelUncancelQuoteResponse();

            if (string.IsNullOrEmpty(response.msg))
            {
                if ((!quote.Type.Equals(RwConstants.ORDER_TYPE_QUOTE)) || (!quote.Status.Equals(RwConstants.QUOTE_STATUS_CANCELLED)))
                {
                    response.msg = "Only CANCELLED Quotes can be uncancelled.";
                }
            }

            if (string.IsNullOrEmpty(response.msg))
            {
                //update the quote status
                QuoteLogic q2 = new QuoteLogic();
                q2.SetDependencies(appConfig, userSession);
                q2.QuoteId = quote.QuoteId;
                q2.Status = ((string.IsNullOrEmpty(quote.DealId)) ? RwConstants.QUOTE_STATUS_PROSPECT : RwConstants.QUOTE_STATUS_ACTIVE);
                q2.StatusDate = FwConvert.ToUSShortDate(DateTime.Today);
                await q2.SaveAsync(original: quote);
                await QuoteOrderAvailabilityRequestRecalc(appConfig, userSession, quote.QuoteId);
                response.Quote = q2;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TSpStatusResponse> ReapplyManualSort(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, FwSqlConnection conn = null)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "reapplymanualsort", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.status = qry.GetParameter("@status").ToInt32();
            response.success = (response.status == 0);
            response.msg = qry.GetParameter("@msg").ToString();
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> OrderHasItems(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, FwSqlConnection conn = null)
        {
            bool response = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select hasitems = (case when exists (select * from masteritem where orderid = @orderid) then 'T' else 'F' end)");
            qry.AddParameter("@orderid", orderId);
            FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

            foreach (List<object> row in dt.Rows)
            {
                string hasItems = row[dt.GetColumnNo("hasitems")].ToString();
                response = FwConvert.ToBoolean(hasItems);
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> OrderHasRecurring(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, FwSqlConnection conn = null)
        {
            bool response = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select hasrecurring = dbo.funcorderhasrecurring(@orderid)");
            qry.AddParameter("@orderid", orderId);
            FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

            foreach (List<object> row in dt.Rows)
            {
                string hasRecurring = row[dt.GetColumnNo("hasrecurring")].ToString();
                response = FwConvert.ToBoolean(hasRecurring);
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

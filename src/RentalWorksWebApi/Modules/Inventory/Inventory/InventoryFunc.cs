using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi.Modules.HomeControls.InventoryPackageInventory;
using WebApi.Modules.HomeControls.InventoryWarehouse;
using WebApi.Modules.Utilities.RateUpdateBatch;
using WebApi.Modules.Utilities.RateUpdateBatchItem;
using WebApi.Modules.Utilities.RateUpdateItem;

namespace WebApi.Modules.Inventory.Inventory
{

    public class UpdateInventoryQuantityRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public string TransactionType { get; set; }
        public string OrderType { get; set; }
        public decimal QuantityChange { get; set; }
        public bool UpdateCost { get; set; }
        public decimal? CostPerItem { get; set; }
        public decimal? ForceCost { get; set; }
        public string UniqueId1 { get; set; }
        public string UniqueId2 { get; set; }
        public string UniqueId3 { get; set; }
        public int? UniqueId4 { get; set; }
        public bool LogOnly { get; set; }
    }
    public class UpdateInventoryQuantityResponse : TSpStatusResponse { }

    public class ChangeInventoryTrackedByRequest
    {
        public string InventoryId { get; set; }
        public string OldTrackedBy { get; set; }
        public string NewTrackedBy { get; set; }
    }

    public class ChangeInventoryTrackedByResponse : TSpStatusResponse
    {
        public int BarCodesCreated { get; set; }
    }

    public class ChangeICodeRequest
    {
        public string ItemId { get; set; }
        public string InventoryId { get; set; }
        //public string WarehouseId { get; set; }
        //public string Notes { get; set; }
    }

    public class ChangeICodeResponse : TSpStatusResponse
    {
        //public string InventoryId { get; set; }
    }

    public class RetireInventoryRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string ItemId { get; set; }
        public string RetiredReasonId { get; set; }
        public string Notes { get; set; }
        public decimal? Quantity { get; set; }
    }

    public class RetireInventoryResponse : TSpStatusResponse
    {
        public string RetiredId { get; set; }
    }

    public class UnretireInventoryRequest
    {
        public string RetiredId { get; set; }
        public string ItemId { get; set; }
        public string UnretiredReasonId { get; set; }
        public string Notes { get; set; }
        public decimal? Quantity { get; set; }
    }

    public class UnretireInventoryResponse : TSpStatusResponse
    {
        public string UnretiredId { get; set; }
    }

    public class RentalInventoryQcRequiredAllWarehousesRequest
    {
        public string InventoryId { get; set; }
        public bool QcRequired { get; set; }
    }

    public class RentalInventoryQcRequiredAllWarehousesResponse : TSpStatusResponse
    {
    }

    public class InventoryWarehouseSpecificPackageRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public bool IsWarehouseSpecific { get; set; }
    }

    public class InventoryWarehouseSpecificPackageResponse : TSpStatusResponse
    {
    }


    public class ApplyPendingRateUpdateModificationsRequest
    {
        public string RateUpdateBatchName { get; set; }
    }
    public class ApplyPendingRateUpdateModificationsResponse : TSpStatusResponse
    {
        public RateUpdateBatchLogic RateUpdateBatch { get; set; }
    }

    public static class InventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static string GetRateUpdatePendingModificationsWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append("  (newhourlyrate        <> 0) or ");
            sb.Append("  (newhourlycost        <> 0) or ");
            sb.Append("  (newdailyrate         <> 0) or ");
            sb.Append("  (newdailycost         <> 0) or ");
            sb.Append("  (newweeklyrate        <> 0) or ");
            sb.Append("  (newweeklycost        <> 0) or ");
            sb.Append("  (newweek2rate         <> 0) or ");
            sb.Append("  (newweek3rate         <> 0) or ");
            sb.Append("  (newweek4rate         <> 0) or ");
            sb.Append("  (newweek5rate         <> 0) or ");
            sb.Append("  (newmonthlyrate       <> 0) or ");
            sb.Append("  (newmonthlycost       <> 0) or ");
            sb.Append("  (newmanifestvalue     <> 0) or ");
            sb.Append("  (newreplacementcost   <> 0) or ");
            sb.Append("  (newretail            <> 0) or ");
            sb.Append("  (newprice             <> 0) or ");
            sb.Append("  (newcost              <> 0) or ");
            sb.Append("  (newdefaultcost       <> 0) or ");
            sb.Append("  (newmaxdiscount       <> 0) or ");
            sb.Append("  (newmindw             <> 0)    ");
            sb.Append(")");
            return sb.ToString();
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateInventoryQuantityResponse> UpdateInventoryQuantity(FwApplicationConfig appConfig, FwUserSession userSession, UpdateInventoryQuantityRequest request, FwSqlConnection conn = null)
        {
            UpdateInventoryQuantityResponse response = new UpdateInventoryQuantityResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, "updatemasterwhqty", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
            qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
            qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
            qry.AddParameter("@trantype", SqlDbType.NVarChar, ParameterDirection.Input, request.TransactionType);
            qry.AddParameter("@ordertype", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderType);
            qry.AddParameter("@qtychange", SqlDbType.Decimal, ParameterDirection.Input, request.QuantityChange);
            qry.AddParameter("@updatecost", SqlDbType.NVarChar, ParameterDirection.Input, request.UpdateCost);
            qry.AddParameter("@costperitem", SqlDbType.Decimal, ParameterDirection.Input, request.CostPerItem);
            qry.AddParameter("@forcecost", SqlDbType.NVarChar, ParameterDirection.Input, request.ForceCost);
            qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId1);
            qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId2);
            qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId3);
            qry.AddParameter("@uniqueid4", SqlDbType.Int, ParameterDirection.Input, request.UniqueId4);
            qry.AddParameter("@logonly", SqlDbType.NVarChar, ParameterDirection.Input, request.LogOnly);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            //response.success = (qry.GetParameter("@status").ToInt32() == 0);
            //response.msg = qry.GetParameter("@msg").ToString();
            response.success = true;

            string classification = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "class").Result;
            InventoryAvailabilityFunc.RequestRecalc(request.InventoryId, request.WarehouseId, classification);

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<ChangeInventoryTrackedByResponse> ChangeInventoryTrackedBy(FwApplicationConfig appConfig, FwUserSession userSession, ChangeInventoryTrackedByRequest request, FwSqlConnection conn = null)
        {
            ChangeInventoryTrackedByResponse response = new ChangeInventoryTrackedByResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "changemastertrackedby", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@oldtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.OldTrackedBy);
            qry.AddParameter("@newtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.NewTrackedBy);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@success", SqlDbType.NVarChar, ParameterDirection.Output);
            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            qry.AddParameter("@barcodescreated", SqlDbType.Int, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.success = FwConvert.ToBoolean(qry.GetParameter("@success").ToString());
            response.msg = qry.GetParameter("@msg").ToString();
            response.BarCodesCreated = FwConvert.ToInt32(qry.GetParameter("@barcodescreated").ToString());
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RetireInventoryResponse> RetireInventory(FwApplicationConfig appConfig, FwUserSession userSession, RetireInventoryRequest request, FwSqlConnection conn = null)
        {
            RetireInventoryResponse response = new RetireInventoryResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "retireitems", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
            qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
            //qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
            qry.AddParameter("@retiredreasonid", SqlDbType.NVarChar, ParameterDirection.Input, request.RetiredReasonId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@notes", SqlDbType.NVarChar, ParameterDirection.Input, request.Notes);
            qry.AddParameter("@qty", SqlDbType.Decimal, ParameterDirection.Input, request.Quantity);
            //qry.AddParameter("@outonly", SqlDbType.NVarChar, ParameterDirection.Input, request.OutOnly);
            //qry.AddParameter("@outorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@outmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxxx);
            //qry.AddParameter("@outcontractid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
            //qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
            //qry.AddParameter("@billedtoorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@billedtomasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@lostorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@physicalitemid", SqlDbType.Int, ParameterDirection.Input, request.xxxx);
            qry.AddParameter("@retiredid", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.RetiredId = qry.GetParameter("@retiredid").ToString();
            response.success = !string.IsNullOrEmpty(response.RetiredId);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UnretireInventoryResponse> UnretireInventory(FwApplicationConfig appConfig, FwUserSession userSession, UnretireInventoryRequest request, FwSqlConnection conn = null)
        {
            UnretireInventoryResponse response = new UnretireInventoryResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "unretireitems", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@retiredid", SqlDbType.NVarChar, ParameterDirection.Input, request.RetiredId);
            qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
            qry.AddParameter("@unretiredreasonid", SqlDbType.NVarChar, ParameterDirection.Input, request.UnretiredReasonId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@notes", SqlDbType.NVarChar, ParameterDirection.Input, request.Notes);
            qry.AddParameter("@qty", SqlDbType.Decimal, ParameterDirection.Input, request.Quantity);
            //qry.AddParameter("@outonly", SqlDbType.NVarChar, ParameterDirection.Input, request.OutOnly);
            //qry.AddParameter("@outorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@outcontractid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            qry.AddParameter("@unretiredid", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.UnretiredId = qry.GetParameter("@unretiredid").ToString();
            response.success = !string.IsNullOrEmpty(response.UnretiredId);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ChangeICodeResponse> ChangeICode(FwApplicationConfig appConfig, FwUserSession userSession, ChangeICodeRequest request, FwSqlConnection conn = null)
        {
            ChangeICodeResponse response = new ChangeICodeResponse();

            if (string.IsNullOrEmpty(request.ItemId))
            {
                response.msg = "No Bar Code or Serial Number provided.";
            }
            else if (string.IsNullOrEmpty(request.InventoryId))
            {
                response.msg = "No \"Change to I-Code\" provided.";
            }
            else
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                }
                FwSqlCommand qry = new FwSqlCommand(conn, "changeicodeweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
                qry.AddParameter("@newmasterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
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
        public static async Task<RentalInventoryQcRequiredAllWarehousesResponse> SetQcRequiredAllWarehouses(FwApplicationConfig appConfig, FwUserSession userSession, RentalInventoryQcRequiredAllWarehousesRequest request)
        {
            RentalInventoryQcRequiredAllWarehousesResponse response = new RentalInventoryQcRequiredAllWarehousesResponse();

            BrowseRequest warehouseBrowseRequest = new BrowseRequest();
            warehouseBrowseRequest.uniqueids = new Dictionary<string, object>();
            warehouseBrowseRequest.uniqueids.Add("InventoryId", request.InventoryId);

            InventoryWarehouseLogic warehouseSelector = new InventoryWarehouseLogic();
            warehouseSelector.SetDependencies(appConfig, userSession);
            List<InventoryWarehouseLogic> inventoryWarehouses = await warehouseSelector.SelectAsync<InventoryWarehouseLogic>(warehouseBrowseRequest);

            foreach (InventoryWarehouseLogic iw in inventoryWarehouses)
            {
                InventoryWarehouseLogic iw2 = new InventoryWarehouseLogic();
                iw2.SetDependencies(appConfig, userSession);
                iw2.InventoryId = iw.InventoryId;
                iw2.WarehouseId = iw.WarehouseId;
                iw2.QcRequired = request.QcRequired;
                await iw2.SaveAsync(original: iw);
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<InventoryWarehouseSpecificPackageResponse> SetWarehouseSpecificPackage(FwApplicationConfig appConfig, FwUserSession userSession, InventoryWarehouseSpecificPackageRequest request)
        {
            InventoryWarehouseSpecificPackageResponse response = new InventoryWarehouseSpecificPackageResponse();

            InventoryWarehouseLogic lCurr = new InventoryWarehouseLogic();
            lCurr.SetDependencies(appConfig, userSession);
            lCurr.InventoryId = request.InventoryId;
            lCurr.WarehouseId = request.WarehouseId;
            if (await lCurr.LoadAsync<InventoryWarehouseLogic>())
            {
                if (!lCurr.IsWarehouseSpecific.Equals(request.IsWarehouseSpecific))
                {
                    if (request.IsWarehouseSpecific)
                    {
                        //copy all items
                        BrowseRequest itemBrowseRequest = new BrowseRequest();
                        itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                        itemBrowseRequest.uniqueids.Add("PackageId", request.InventoryId);
                        itemBrowseRequest.uniqueids.Add("WarehouseId", "");  // copy the default warehouse definition
                        itemBrowseRequest.orderby = "OrderBy";

                        InventoryPackageInventoryLogic itemSelector = new InventoryPackageInventoryLogic();
                        itemSelector.SetDependencies(appConfig, userSession);
                        List<InventoryPackageInventoryLogic> items = await itemSelector.SelectAsync<InventoryPackageInventoryLogic>(itemBrowseRequest);

                        foreach (InventoryPackageInventoryLogic i in items)
                        {
                            i.SetDependencies(appConfig, userSession);
                            i.InventoryPackageInventoryId = ""; // creating a new
                            i.WarehouseId = request.WarehouseId;
                            await i.SaveAsync();
                        }
                    }
                    else
                    {
                        // delete all items
                        BrowseRequest itemBrowseRequest = new BrowseRequest();
                        itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                        itemBrowseRequest.uniqueids.Add("PackageId", request.InventoryId);
                        itemBrowseRequest.uniqueids.Add("WarehouseId", request.WarehouseId);
                        itemBrowseRequest.orderby = "OrderBy";

                        InventoryPackageInventoryLogic itemSelector = new InventoryPackageInventoryLogic();
                        itemSelector.SetDependencies(appConfig, userSession);
                        List<InventoryPackageInventoryLogic> items = await itemSelector.SelectAsync<InventoryPackageInventoryLogic>(itemBrowseRequest);

                        foreach (InventoryPackageInventoryLogic i in items)
                        {
                            i.SetDependencies(appConfig, userSession);
                            i.deletingWarehouseSpecific = true;
                            await i.DeleteAsync();
                        }
                    }

                    InventoryWarehouseLogic lNew = lCurr.MakeCopy<InventoryWarehouseLogic>();
                    lNew.SetDependencies(appConfig, userSession);
                    lNew.IsWarehouseSpecific = request.IsWarehouseSpecific;
                    await lNew.SaveAsync(original: lCurr);

                }
                response.success = true;
            }
            else
            {
                //return NotFound();
                response.success = false;
                response.msg = "Invalid Inventory Warehouse object.";
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ApplyPendingRateUpdateModificationsResponse> ApplyPendingModificationsAsync(FwApplicationConfig appConfig, FwUserSession userSession, ApplyPendingRateUpdateModificationsRequest request)
        {
            ApplyPendingRateUpdateModificationsResponse response = new ApplyPendingRateUpdateModificationsResponse();

            if (string.IsNullOrEmpty(request.RateUpdateBatchName))
            {
                response.msg = "Rate Update Batch Name is required.";
            }
            else
            {
                // get all of the pending modifications
                BrowseRequest itemBrowseRequest = new BrowseRequest();
                itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                itemBrowseRequest.uniqueids.Add("ShowPendingModifications", true);

                RateUpdateItemLogic itemSelector = new RateUpdateItemLogic();
                itemSelector.SetDependencies(appConfig, userSession);
                List<RateUpdateItemLogic> items = await itemSelector.SelectAsync<RateUpdateItemLogic>(itemBrowseRequest);

                if (items.Count > 0)
                {
                    FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                    try
                    {
                        await conn.OpenAsync();
                        conn.BeginTransaction();

                        RateUpdateBatchLogic batch = new RateUpdateBatchLogic();
                        batch.SetDependencies(appConfig, userSession);
                        batch.RateUpdateBatch = request.RateUpdateBatchName;
                        batch.UsersId = userSession.UsersId;
                        batch.Applied = DateTime.Now;
                        await batch.SaveAsync(conn: conn);

                        string sessionId = userSession.UsersId;
                        await FwSqlData.StartMeter(conn, appConfig.DatabaseSettings, sessionId, "Applying Rate Modifications", items.Count);

                        foreach (RateUpdateItemLogic i in items)
                        {
                            // log this change in the batch
                            RateUpdateBatchItemLogic batchItem = new RateUpdateBatchItemLogic();
                            batchItem.SetDependencies(appConfig, userSession);
                            batchItem.RateUpdateBatchId = batch.RateUpdateBatchId;
                            batchItem.InventoryId = i.InventoryId;
                            batchItem.WarehouseId = i.WarehouseId;
                            batchItem.OldHourlyCost = i.HourlyCost;
                            batchItem.NewHourlyCost = i.NewHourlyCost;
                            batchItem.OldHourlyRate = i.HourlyRate;
                            batchItem.NewHourlyRate = i.NewHourlyRate;
                            batchItem.OldDailyCost = i.DailyCost;
                            batchItem.NewDailyCost = i.NewDailyCost;
                            batchItem.OldDailyRate = i.DailyRate;
                            batchItem.NewDailyRate = i.NewDailyRate;
                            batchItem.OldWeeklyCost = i.WeeklyCost;
                            batchItem.NewWeeklyCost = i.NewWeeklyCost;
                            batchItem.OldWeeklyRate = i.WeeklyRate;
                            batchItem.NewWeeklyRate = i.NewWeeklyRate;
                            batchItem.OldWeek2Rate = i.Week2Rate;
                            batchItem.NewWeek2Rate = i.NewWeek2Rate;
                            batchItem.OldWeek3Rate = i.Week3Rate;
                            batchItem.NewWeek3Rate = i.NewWeek3Rate;
                            batchItem.OldWeek4Rate = i.Week4Rate;
                            batchItem.NewWeek4Rate = i.NewWeek4Rate;
                            batchItem.OldMonthlyCost = i.MonthlyCost;
                            batchItem.NewMonthlyCost = i.NewMonthlyCost;
                            batchItem.OldMonthlyRate = i.MonthlyRate;
                            batchItem.NewMonthlyRate = i.NewMonthlyRate;
                            batchItem.OldCost = i.Cost;
                            batchItem.NewCost = i.NewCost;
                            batchItem.OldDefaultCost = i.DefaultCost;
                            batchItem.NewDefaultCost = i.NewDefaultCost;
                            batchItem.OldPrice = i.Price;
                            batchItem.NewPrice = i.NewPrice;
                            batchItem.OldRetail = i.Retail;
                            batchItem.NewRetail = i.NewRetail;
                            batchItem.OldMinDaysPerWeek = i.MinDaysPerWeek;
                            batchItem.NewMinDaysPerWeek = i.NewMinDaysPerWeek;
                            batchItem.OldMaxDiscount = i.MaxDiscount;
                            batchItem.NewMaxDiscount = i.NewMaxDiscount;
                            batchItem.OldUnitValue = i.UnitValue;
                            batchItem.NewUnitValue = i.NewUnitValue;
                            batchItem.OldReplacementCost = i.ReplacementCost;
                            batchItem.NewReplacementCost = i.NewReplacementCost;
                            await batchItem.SaveAsync(conn: conn);

                            InventoryWarehouseLogic iwOrig = new InventoryWarehouseLogic();
                            iwOrig.SetDependencies(appConfig, userSession);
                            iwOrig.InventoryId = i.InventoryId;
                            iwOrig.WarehouseId = i.WarehouseId;
                            await iwOrig.LoadAsync<InventoryWarehouseLogic>(conn);

                            InventoryWarehouseLogic iwNew = new InventoryWarehouseLogic();
                            iwNew.SetDependencies(appConfig, userSession);
                            iwNew.InventoryId = i.InventoryId;
                            iwNew.WarehouseId = i.WarehouseId;
                            iwNew.HourlyCost = i.NewHourlyCost;
                            iwNew.HourlyRate = i.NewHourlyRate;
                            iwNew.DailyCost = i.NewDailyCost;
                            iwNew.DailyRate = i.NewDailyRate;
                            iwNew.WeeklyCost = i.NewWeeklyCost;
                            iwNew.WeeklyRate = i.NewWeeklyRate;
                            iwNew.Week2Rate = i.NewWeek2Rate;
                            iwNew.Week3Rate = i.NewWeek3Rate;
                            iwNew.Week4Rate = i.NewWeek4Rate;
                            iwNew.MonthlyCost = i.NewMonthlyCost;
                            iwNew.MonthlyRate = i.NewMonthlyRate;
                            iwNew.AverageCost = i.NewCost;
                            iwNew.DefaultCost = i.NewDefaultCost;
                            iwNew.Price = i.NewPrice;
                            iwNew.Retail = i.NewRetail;
                            //iwNew.MinDaysPerWeek = i.NewMinDaysPerWeek;
                            iwNew.MaximumDiscount = i.NewMaxDiscount;
                            iwNew.UnitValue = i.NewUnitValue;
                            iwNew.ReplacementCost = i.NewReplacementCost;
                            await iwNew.SaveAsync(original: iwOrig, conn: conn);

                            // clear the pending modification
                            RateUpdateItemLogic item = new RateUpdateItemLogic();
                            item.SetDependencies(appConfig, userSession);
                            item.InventoryId = i.InventoryId;
                            item.WarehouseId = i.WarehouseId;
                            item.NewHourlyCost = 0;
                            item.NewHourlyRate = 0;
                            item.NewDailyCost = 0;
                            item.NewDailyRate = 0;
                            item.NewWeeklyCost = 0;
                            item.NewWeeklyRate = 0;
                            item.NewWeek2Rate = 0;
                            item.NewWeek3Rate = 0;
                            item.NewWeek4Rate = 0;
                            item.NewMonthlyCost = 0;
                            item.NewMonthlyRate = 0;
                            item.NewCost = 0;
                            item.NewDefaultCost = 0;
                            item.NewPrice = 0;
                            item.NewRetail = 0;
                            item.NewMinDaysPerWeek = 0;
                            item.NewMaxDiscount = 0;
                            item.NewUnitValue = 0;
                            item.NewReplacementCost = 0;
                            await item.SaveAsync(original: i, conn: conn);

                            await FwSqlData.StepMeter(conn, appConfig.DatabaseSettings, sessionId);
                        }

                        await FwSqlData.FinishMeter(conn, appConfig.DatabaseSettings, sessionId);
                        response.success = true;
                        response.RateUpdateBatch = batch;
                    }
                    finally
                    {
                        if (response.success)
                        {
                            conn.CommitTransaction();
                        }
                        else
                        {
                            conn.RollbackTransaction();
                        }
                        conn.Close();
                    }
                }
                else
                {
                    response.msg = "There are no pending modifications to apply.";
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------



    }
}

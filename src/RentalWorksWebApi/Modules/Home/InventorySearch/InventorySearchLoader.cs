using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System;
using WebApi.Modules.Home.InventoryAvailability;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.InventorySearch
{
    [FwSqlTable("inventoryview")]
    public class InventorySearchLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastercolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ClassificationColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyavailable", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availisstale", modeltype: FwDataTypes.Boolean)]
        public bool? QuantityAvailableIsStale { get; set; } = true;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availabilitystate", modeltype: FwDataTypes.Text)]
        public string AvailabilityState { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflictdate", modeltype: FwDataTypes.Date)]
        public string ConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyqcrequired", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityQcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "appimageid", modeltype: FwDataTypes.Text)]
        public string ImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thumbnail", modeltype: FwDataTypes.JpgDataUrl)]
        public string Thumbnail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imageheight", modeltype: FwDataTypes.Integer)]
        public int? ImageHeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imagewidth", modeltype: FwDataTypes.Integer)]
        public int? ImageWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isoption", modeltype: FwDataTypes.Boolean)]
        public bool? IsOption { get; set; } = true;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultqty", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultQuantity { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        private async Task<FwJsonDataTable> AddAvailabilityData(FwJsonDataTable dt, bool showAvailability, DateTime? fromDate, DateTime? toDate, string sessionId/*, bool refreshAvailability*/)
        {
            FwJsonDataTable dtOut = dt;
            if (showAvailability)
            {
                DateTime fromDateTime = DateTime.MinValue;
                DateTime toDateTime = DateTime.MinValue;

                if ((fromDate != null) && (fromDate > DateTime.MinValue))
                {
                    fromDateTime = fromDate.GetValueOrDefault(DateTime.MinValue);
                }
                if ((toDate != null) && (toDate > DateTime.MinValue))
                {
                    toDateTime = toDate.GetValueOrDefault(DateTime.MinValue);
                }

                if ((fromDateTime != null) && (toDateTime != null))
                {
                    if (dtOut.Rows.Count > 0)
                    {
                        TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                        foreach (List<object> row in dtOut.Rows)
                        {
                            string inventoryId = row[dtOut.GetColumnNo("InventoryId")].ToString();
                            string warehouseId = row[dtOut.GetColumnNo("WarehouseId")].ToString();
                            availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDateTime, toDateTime));
                        }

                        TAvailabilityCache availCache = await InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems/*, refreshAvailability*/, refreshIfNeeded: true, forceRefresh: false);

                        foreach (List<object> row in dtOut.Rows)
                        {
                            string inventoryId = row[dtOut.GetColumnNo("InventoryId")].ToString();
                            string warehouseId = row[dtOut.GetColumnNo("WarehouseId")].ToString();
                            decimal qty = FwConvert.ToDecimal(row[dtOut.GetColumnNo("Quantity")]);

                            decimal qtyAvailable = 0;
                            bool isStale = true;
                            DateTime? conflictDate = null;
                            string availabilityState = RwConstants.AVAILABILITY_STATE_STALE;

                            TInventoryWarehouseAvailability availData = null;
                            if (availCache.TryGetValue(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId), out availData))
                            {
                                TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(fromDateTime, toDateTime, qty);
                                qtyAvailable = minAvail.MinimumAvailable.Total;
                                conflictDate = minAvail.FirstConfict;
                                isStale = minAvail.IsStale;
                                availabilityState = minAvail.AvailabilityState;
                            }

                            row[dtOut.GetColumnNo("QuantityAvailable")] = qtyAvailable;
                            row[dtOut.GetColumnNo("ConflictDate")] = conflictDate;
                            row[dtOut.GetColumnNo("QuantityAvailableIsStale")] = isStale;
                            row[dtOut.GetColumnNo("AvailabilityState")] = availabilityState;

                        }
                    }
                }
            }
            return dtOut;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> SearchAsync(InventorySearchRequest request)
        {
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventorysearch", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@availfor", SqlDbType.NVarChar, ParameterDirection.Input, request.AvailableFor);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.NVarChar, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.NVarChar, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@classification", SqlDbType.NVarChar, ParameterDirection.Input, request.Classification);
                    qry.AddParameter("@searchtext", SqlDbType.NVarChar, ParameterDirection.Input, request.SearchText);
                    if ((request.FromDate != null) && (request.FromDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, request.FromDate);
                    }
                    if ((request.ToDate != null) && (request.ToDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, request.ToDate);
                    }
                    qry.AddParameter("@showimages", SqlDbType.NVarChar, ParameterDirection.Input, (request.ShowImages.GetValueOrDefault(false) ? "T" : "F"));
                    qry.AddParameter("@hidezeroqty", SqlDbType.NVarChar, ParameterDirection.Input, (request.HideInventoryWithZeroQuantity.GetValueOrDefault(true) ? "T" : "F"));
                    qry.AddParameter("@sortby", SqlDbType.NVarChar, ParameterDirection.Input, request.SortBy);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            
            //dt = await AddAvailabilityData(dt, request.ShowAvailability.GetValueOrDefault(false), request.FromDate, request.ToDate, request.SessionId, request.RefreshAvailability.GetValueOrDefault(false));
            dt = await AddAvailabilityData(dt, request.ShowAvailability.GetValueOrDefault(false), request.FromDate, request.ToDate, request.SessionId/*, true*/);  //jh 08/23/2019 experimental

            return dt;
        }
        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> SearchAccessoriesAsync(InventorySearchAccessoriesRequest request)
        {
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventorysearchaccessories", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@parentid", SqlDbType.NVarChar, ParameterDirection.Input, request.ParentId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    if ((request.FromDate != null) && (request.FromDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, request.FromDate);
                    }
                    if ((request.ToDate != null) && (request.ToDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, request.ToDate);
                    }
                    qry.AddParameter("@showimages", SqlDbType.NVarChar, ParameterDirection.Input, (request.ShowImages.GetValueOrDefault(false) ? "T" : "F"));
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            //dt = await AddAvailabilityData(dt, request.ShowAvailability.GetValueOrDefault(false), request.FromDate, request.ToDate, request.SessionId, request.RefreshAvailability.GetValueOrDefault(false));
            dt = await AddAvailabilityData(dt, request.ShowAvailability.GetValueOrDefault(false), request.FromDate, request.ToDate, request.SessionId/*, true*/);  //jh 08/23/2019 experimental

            return dt;
        }
        //------------------------------------------------------------------------------------


    }
}
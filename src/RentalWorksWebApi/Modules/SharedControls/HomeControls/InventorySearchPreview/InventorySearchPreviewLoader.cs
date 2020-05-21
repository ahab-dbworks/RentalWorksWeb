using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System;
using static WebApi.Modules.HomeControls.InventorySearchPreview.InventorySearchPreviewController;
using WebApi.Modules.HomeControls.InventoryAvailability;
using System.Collections.Generic;
using WebApi;
using WebApi.Logic;
using FwStandard.Data;
using FwStandard.Models;

namespace WebApi.Modules.HomeControls.InventorySearchPreview
{
    [FwSqlTable("dbo.funcinventorysearchpreview(@sessionid)")]
    public class InventorySearchPreviewLoader : AppDataLoadRecord
    {
        private bool _showAvailability = false;
        private DateTime? _availFromDate = null;
        private DateTime? _availToDate = null;

        //------------------------------------------------------------------------------------ 
        public InventorySearchPreviewLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string Id { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "grandparentid", modeltype: FwDataTypes.Text)]
        public string GrandParentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string AvailableForDisplay
        {
            get { return getRecTypeDisplay(AvailFor); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string AvailableForColor
        {
            get { return determineRecTypeColor(AvailFor); }
            set { }
        }
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
        [FwSqlDataField(column: "availcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityAvailableColor { get; set; }
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
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
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
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        private string getRecTypeDisplay(string recType)
        {
            return AppFunc.GetInventoryRecTypeDisplay(recType);
        }
        //------------------------------------------------------------------------------------ 
        private string determineRecTypeColor(string recType)
        {
            return AppFunc.GetInventoryRecTypeColor(recType);
        }
        //------------------------------------------------------------------------------------    
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string sessionId = GetUniqueIdAsString("SessionId", request);
            _showAvailability = GetUniqueIdAsBoolean("ShowAvailability", request).GetValueOrDefault(false);
            _availFromDate = GetUniqueIdAsDate("FromDate", request);
            _availToDate = GetUniqueIdAsDate("ToDate", request);

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddParameter("@sessionid", sessionId);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (_showAvailability)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DateTime fromDateTime = DateTime.MinValue;
                        DateTime toDateTime = DateTime.MinValue;

                        if ((_availFromDate != null) && (_availFromDate > DateTime.MinValue))
                        {
                            fromDateTime = _availFromDate.GetValueOrDefault(DateTime.MinValue);
                        }
                        if ((_availToDate != null) && (_availToDate > DateTime.MinValue))
                        {
                            toDateTime = _availToDate.GetValueOrDefault(DateTime.MinValue);
                        }

                        if ((fromDateTime != null) && (toDateTime != null))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                                foreach (List<object> row in dt.Rows)
                                {
                                    string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                                    string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDateTime, toDateTime));
                                }

                                TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems, refreshIfNeeded: true, forceRefresh: false).Result;

                                foreach (List<object> row in dt.Rows)
                                {
                                    string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                                    string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                                    float qty = FwConvert.ToFloat(row[dt.GetColumnNo("Quantity")]);

                                    float qtyAvailable = 0;
                                    bool isStale = true;
                                    DateTime? conflictDate = null;
                                    string availColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEEDRECALC);
                                    string availabilityState = RwConstants.AVAILABILITY_STATE_STALE;


                                    TInventoryWarehouseAvailability availData = null;
                                    if (availCache.TryGetValue(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId), out availData))
                                    {
                                        TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(fromDateTime, toDateTime, qty);

                                        qtyAvailable = minAvail.MinimumAvailable.OwnedAndConsigned;
                                        conflictDate = minAvail.FirstConfict;
                                        isStale = minAvail.IsStale;
                                        availColor = minAvail.Color;
                                        availabilityState = minAvail.AvailabilityState;
                                    }

                                    row[dt.GetColumnNo("QuantityAvailable")] = qtyAvailable;
                                    if (conflictDate != null)
                                    {
                                        row[dt.GetColumnNo("ConflictDate")] = FwConvert.ToUSShortDate(conflictDate.GetValueOrDefault(DateTime.MinValue));
                                    }

                                    row[dt.GetColumnNo("QuantityAvailableColor")] = availColor;
                                    row[dt.GetColumnNo("QuantityAvailableIsStale")] = isStale;
                                    row[dt.GetColumnNo("AvailabilityState")] = availabilityState;
                                }
                            }
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("AvailableForDisplay")] = getRecTypeDisplay(row[dt.GetColumnNo("AvailFor")].ToString());
                        row[dt.GetColumnNo("AvailableForColor")] = determineRecTypeColor(row[dt.GetColumnNo("AvailFor")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
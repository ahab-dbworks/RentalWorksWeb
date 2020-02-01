using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.WarehouseSettings.Warehouse
{
    [FwSqlTable("warehouse")]
    public class WarehouseRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, required: true)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptbarcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public string AssignBarCodesBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesmarkuppct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        public decimal? SalesMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsmarkuppct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        public decimal? PartsMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupsales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? MarkupSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupparts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? MarkupParts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescostincfreight", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeFreightInSalesCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partscostincfreight", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeFreightInPartsCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorbarcode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ReceiveVendorBarCodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "useneginv", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdeliverytype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangerepair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ExchangedItemsRepairByDefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accstagingopt", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string StagingCompleteComponents { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chkinnonbcsort", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public string CheckInSortBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "truckschedulemethod", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string DefaultPackageTruckScheduleMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagenonbcpackages", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? StageQuantityAccessories { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "promptforcheckoutexceptions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PromptForCheckOutExceptions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "promptforcheckinexceptions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PromptForCheckInExceptions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcheckedinholding", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? StagingShowCheckedInHoldingItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeliverytype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string PoDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcachedays", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? AvailabilityCacheDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availpreserveconflicts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityPreserveConflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrepairdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? DefaultRepairDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qldeliverytype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string QuikLocateDefaultDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairagentfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string RepairBillableOrderAgentFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprintin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ReturnListPrintInQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprintout", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ReturnListPrintOutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availuseonpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityUseOnPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "regionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RegionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor, sqltype: "int")]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availlatedays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityLateDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseBarCodeLabelDesigner { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryappreportdesignerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryLabelDesignId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemappreportdesignerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemLabelDesignId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwexcludefromroa", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DataWarehouseExcludeFromROA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availlatehours", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityLateHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availstarthour", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityStartHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availstophour", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityStopHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includetaxinassetvalue", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeTaxInAssetValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangeavailpct", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ProductionExchangeAvailabilityPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangeenabled", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ProductionexchangeEnabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangewhcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 4)]
        public string ProductionExchangeWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4ratepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? Week4RatePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scanvendorbarcodereturn", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RequireScanVendorBarCodeOnReturn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internaldealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InternalDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InternalVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onpoafterapproved", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CalculateOnPoAfterApproved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultreturntowhtoyes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? TransferDefaultReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalculateinbackground", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityCalculateInBackground { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cannottransfer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikLocateCannotTransfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qldefaultrequireddate", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string QuikLocateDefaultRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qlrequireddaysbefore", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? QuikLocateRequiredDaysBefore { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrentalrates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CalculateDefaultRentalRates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldailyratepctofrepl", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 4)]
        public decimal? RentalDailyRatePercentOfReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalweeklyratemultofdaily", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 4)]
        public decimal? RentalWeeklyRateMultipleOfDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableaisleshelfcheckin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CheckInEnableScanningToAisleShelf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalfabarcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalFixedAssetBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "updatemanifestvalue", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AutoUpdateUnitValueOnReceivingHigherCostItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupreplacementcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? MarkupReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostmarkuppct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? ReplacementCostMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availhourlydays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityHourlyDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrestockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? SalesRestockPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availexcludeconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityExcludeConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availreserveconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityRequireConsignedReserved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availenableqcdelay", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityEnableQcDelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeweekend", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityQcDelayExcludeWeekend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeholiday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityQcDelayExcludeHoliday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayindefinite", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityQcDelayIndefinite { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addtoordermute", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DefaultMuteItemsAddedToOrderAtStaging { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
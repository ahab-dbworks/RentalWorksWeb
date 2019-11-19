using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.WarehouseSettings.Warehouse
{
    [FwSqlTable("warehouseview")]
    public class WarehouseLoader : WarehouseBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxcountry", modeltype: FwDataTypes.Text, required: true)]
        public string TaxCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxrule", modeltype: FwDataTypes.Text)]
        public string TaxRule { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxrental", modeltype: FwDataTypes.Boolean)]
        public bool? RentalExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxsales", modeltype: FwDataTypes.Boolean)]
        public bool? SalesExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxlabor", modeltype: FwDataTypes.Boolean)]
        public bool? LaborExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printfa", modeltype: FwDataTypes.Boolean)]
        public bool? PrintFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptbarcode", modeltype: FwDataTypes.Text)]
        public string AssignBarCodesBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesmarkuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsmarkuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupsales", modeltype: FwDataTypes.Boolean)]
        public bool? MarkupSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupparts", modeltype: FwDataTypes.Boolean)]
        public bool? MarkupParts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescostincfreight", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeFreightInSalesCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partscostincfreight", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeFreightInPartsCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveVendorBarCodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "useneginv", modeltype: FwDataTypes.Boolean)]
        public bool? AllowNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdeliverytype", modeltype: FwDataTypes.Text)]
        public string DefaultDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangerepair", modeltype: FwDataTypes.Boolean)]
        public bool? ExchangedItemsRepairByDefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accstagingopt", modeltype: FwDataTypes.Text)]
        public string StagingCompleteComponents { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chkinnonbcsort", modeltype: FwDataTypes.Text)]
        public string CheckInSortBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "truckschedulemethod", modeltype: FwDataTypes.Text)]
        public string DefaultPackageTruckScheduleMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagenonbcpackages", modeltype: FwDataTypes.Boolean)]
        public bool? StageQuantityAccessories { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "promptforcheckoutexceptions", modeltype: FwDataTypes.Boolean)]
        public bool? PromptForCheckOutExceptions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "promptforcheckinexceptions", modeltype: FwDataTypes.Boolean)]
        public bool? PromptForCheckInExceptions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcheckedinholding", modeltype: FwDataTypes.Boolean)]
        public bool? StagingShowCheckedInHoldingItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeliverytype", modeltype: FwDataTypes.Text)]
        public string PoDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcachedays", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityCacheDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availpreserveconflicts", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityPreserveConflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrepairdays", modeltype: FwDataTypes.Integer)]
        public int? DefaultRepairDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qldeliverytype", modeltype: FwDataTypes.Text)]
        public string QuikLocateDefaultDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairagentfrom", modeltype: FwDataTypes.Text)]
        public string RepairBillableOrderAgentFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprintin", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnListPrintInQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprintout", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnListPrintOutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availuseonpo", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityUseOnPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "regionid", modeltype: FwDataTypes.Text)]
        public string RegionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "region", modeltype: FwDataTypes.Text)]
        public string Region { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availlatedays", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityLateDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean)]
        public bool? UseBarCodeLabelDesigner { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryappreportdesignerid", modeltype: FwDataTypes.Text)]
        public string InventoryLabelDesignId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryappreportdesigner", modeltype: FwDataTypes.Text)]
        public string InventoryLabelDesign { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemappreportdesignerid", modeltype: FwDataTypes.Text)]
        public string ItemLabelDesignId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemappreportdesigner", modeltype: FwDataTypes.Text)]
        public string ItemLabelDesign { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwexcludefromroa", modeltype: FwDataTypes.Boolean)]
        public bool? DataWarehouseExcludeFromROA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availlatehours", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityLateHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availstarthour", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityStartHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availstophour", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityStopHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includetaxinassetvalue", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeTaxInAssetValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangeavailpct", modeltype: FwDataTypes.Integer)]
        public int? ProductionExchangeAvailabilityPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangeenabled", modeltype: FwDataTypes.Boolean)]
        public bool? ProductionexchangeEnabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionexchangewhcode", modeltype: FwDataTypes.Text)]
        public string ProductionExchangeWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4ratepct", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4RatePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scanvendorbarcodereturn", modeltype: FwDataTypes.Boolean)]
        public bool? RequireScanVendorBarCodeOnReturn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internaldealid", modeltype: FwDataTypes.Text)]
        public string InternalDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internaldeal", modeltype: FwDataTypes.Text)]
        public string InternalDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalvendorid", modeltype: FwDataTypes.Text)]
        public string InternalVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalvendor", modeltype: FwDataTypes.Text)]
        public string InternalVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onpoafterapproved", modeltype: FwDataTypes.Boolean)]
        public bool? CalculateOnPoAfterApproved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultreturntowhtoyes", modeltype: FwDataTypes.Boolean)]
        public bool? TransferDefaultReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalculateinbackground", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityCalculateInBackground { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cannottransfer", modeltype: FwDataTypes.Boolean)]
        public bool? QuikLocateCannotTransfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qldefaultrequireddate", modeltype: FwDataTypes.Text)]
        public string QuikLocateDefaultRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qlrequireddaysbefore", modeltype: FwDataTypes.Integer)]
        public int? QuikLocateRequiredDaysBefore { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrentalrates", modeltype: FwDataTypes.Boolean)]
        public bool? CalculateDefaultRentalRates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldailyratepctofrepl", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDailyRatePercentOfReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalweeklyratemultofdaily", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalWeeklyRateMultipleOfDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableaisleshelfcheckin", modeltype: FwDataTypes.Boolean)]
        public bool? CheckInEnableScanningToAisleShelf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderangeid", modeltype: FwDataTypes.Text)]
        public string RentalBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderange", modeltype: FwDataTypes.Text)]
        public string RentalBarCodeRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalfabarcoderangeid", modeltype: FwDataTypes.Text)]
        public string RentalFixedAssetBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalfabarcoderange", modeltype: FwDataTypes.Text)]
        public string RentalFixedAssetBarCodeRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderangeid", modeltype: FwDataTypes.Text)]
        public string SalesBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderange", modeltype: FwDataTypes.Text)]
        public string SalesBarCodeRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "updatemanifestvalue", modeltype: FwDataTypes.Boolean)]
        public bool? AutoUpdateUnitValueOnReceivingHigherCostItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupreplacementcost", modeltype: FwDataTypes.Boolean)]
        public bool? MarkupReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostmarkuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availhourlydays", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityHourlyDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrestockpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesRestockPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availexcludeconsigned", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityExcludeConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availreserveconsigned", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityRequireConsignedReserved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availenableqcdelay", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityEnableQcDelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeweekend", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityQcDelayExcludeWeekend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayexcludeholiday", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityQcDelayExcludeHoliday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelayindefinite", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityQcDelayIndefinite { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
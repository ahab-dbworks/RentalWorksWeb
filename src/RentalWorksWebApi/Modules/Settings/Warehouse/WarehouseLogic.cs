using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.Warehouse
{
    public class WarehouseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseRecord warehouse = new WarehouseRecord();
        WarehouseLoader warehouseLoader = new WarehouseLoader();
        public WarehouseLogic()
        {
            dataRecords.Add(warehouse);
            dataLoader = warehouseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return warehouse.WarehouseId; } set { warehouse.WarehouseId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Warehouse { get { return warehouse.Warehouse; } set { warehouse.Warehouse = value; } }
        public string WarehouseCode { get { return warehouse.WarehouseCode; } set { warehouse.WarehouseCode = value; } }
        public string TaxOptionId { get { return warehouse.TaxOptionId; } set { warehouse.TaxOptionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxCountry { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxRule { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? RentalExempt { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SalesExempt { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? LaborExempt { get; set; }
        public string Attention { get { return warehouse.Attention; } set { warehouse.Attention = value; } }
        public string Address1 { get { return warehouse.Address1; } set { warehouse.Address1 = value; } }
        public string Address2 { get { return warehouse.Address2; } set { warehouse.Address2 = value; } }
        public string City { get { return warehouse.City; } set { warehouse.City = value; } }
        public string Zip { get { return warehouse.Zip; } set { warehouse.Zip = value; } }
        public string State { get { return warehouse.State; } set { warehouse.State = value; } }
        public string CountryId { get { return warehouse.CountryId; } set { warehouse.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string Phone { get { return warehouse.Phone; } set { warehouse.Phone = value; } }
        public string Fax { get { return warehouse.Fax; } set { warehouse.Fax = value; } }
        public string AssignBarCodesBy { get { return warehouse.AssignBarCodesBy; } set { warehouse.AssignBarCodesBy = value; } }
        public decimal? SalesMarkupPercent { get { return warehouse.SalesMarkupPercent; } set { warehouse.SalesMarkupPercent = value; } }
        public decimal? PartsMarkupPercent { get { return warehouse.PartsMarkupPercent; } set { warehouse.PartsMarkupPercent = value; } }
        public bool? MarkupSales { get { return warehouse.MarkupSales; } set { warehouse.MarkupSales = value; } }
        public bool? MarkupParts { get { return warehouse.MarkupParts; } set { warehouse.MarkupParts = value; } }
        public bool? IncludeFreightInSalesCost { get { return warehouse.IncludeFreightInSalesCost; } set { warehouse.IncludeFreightInSalesCost = value; } }
        public bool? IncludeFreightInPartsCost { get { return warehouse.IncludeFreightInPartsCost; } set { warehouse.IncludeFreightInPartsCost = value; } }
        public bool? ReceiveVendorBarCodes { get { return warehouse.ReceiveVendorBarCodes; } set { warehouse.ReceiveVendorBarCodes = value; } }
        public bool? AllowNegativeInventory { get { return warehouse.AllowNegativeInventory; } set { warehouse.AllowNegativeInventory = value; } }
        public string DefaultDeliveryType { get { return warehouse.DefaultDeliveryType; } set { warehouse.DefaultDeliveryType = value; } }
        public bool? ExchangedItemsRepairByDefault { get { return warehouse.ExchangedItemsRepairByDefault; } set { warehouse.ExchangedItemsRepairByDefault = value; } }
        public string StagingCompleteComponents { get { return warehouse.StagingCompleteComponents; } set { warehouse.StagingCompleteComponents = value; } }
        public bool? CheckInSortBy { get { return warehouse.CheckInSortBy; } set { warehouse.CheckInSortBy = value; } }
        public string DefaultPackageTruckScheduleMethod { get { return warehouse.DefaultPackageTruckScheduleMethod; } set { warehouse.DefaultPackageTruckScheduleMethod = value; } }
        public bool? StageQuantityAccessories { get { return warehouse.StageQuantityAccessories; } set { warehouse.StageQuantityAccessories = value; } }
        public bool? PromptForCheckOutExceptions { get { return warehouse.PromptForCheckOutExceptions; } set { warehouse.PromptForCheckOutExceptions = value; } }
        public bool? PromptForCheckInExceptions { get { return warehouse.PromptForCheckInExceptions; } set { warehouse.PromptForCheckInExceptions = value; } }
        public bool? StagingShowCheckedInHoldingItems { get { return warehouse.StagingShowCheckedInHoldingItems; } set { warehouse.StagingShowCheckedInHoldingItems = value; } }
        public string PoDeliveryType { get { return warehouse.PoDeliveryType; } set { warehouse.PoDeliveryType = value; } }
        public int? AvailabilityCacheDays { get { return warehouse.AvailabilityCacheDays; } set { warehouse.AvailabilityCacheDays = value; } }
        public bool? AvailabilityPreserveConflicts { get { return warehouse.AvailabilityPreserveConflicts; } set { warehouse.AvailabilityPreserveConflicts = value; } }
        public int? DefaultRepairDays { get { return warehouse.DefaultRepairDays; } set { warehouse.DefaultRepairDays = value; } }
        public string QuikLocateDefaultDeliveryType { get { return warehouse.QuikLocateDefaultDeliveryType; } set { warehouse.QuikLocateDefaultDeliveryType = value; } }
        public string RepairBillableOrderAgentFrom { get { return warehouse.RepairBillableOrderAgentFrom; } set { warehouse.RepairBillableOrderAgentFrom = value; } }
        public bool? ReturnListPrintInQuantity { get { return warehouse.ReturnListPrintInQuantity; } set { warehouse.ReturnListPrintInQuantity = value; } }
        public bool? ReturnListPrintOutQuantity { get { return warehouse.ReturnListPrintOutQuantity; } set { warehouse.ReturnListPrintOutQuantity = value; } }
        public bool? AvailabilityUseOnPO { get { return warehouse.AvailabilityUseOnPO; } set { warehouse.AvailabilityUseOnPO = value; } }
        public string RegionId { get { return warehouse.RegionId; } set { warehouse.RegionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Region { get; set; }
        public string DataWarehouseBackgroundColor { get { return warehouse.DataWarehouseBackgroundColor; } set { warehouse.DataWarehouseBackgroundColor = value; } }
        public int? AvailabilityLateDays { get { return warehouse.AvailabilityLateDays; } set { warehouse.AvailabilityLateDays = value; } }
        public bool? UseBarCodeLabelDesigner { get { return warehouse.UseBarCodeLabelDesigner; } set { warehouse.UseBarCodeLabelDesigner = value; } }
        public string InventoryLabelDesignId { get { return warehouse.InventoryLabelDesignId; } set { warehouse.InventoryLabelDesignId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryLabelDesign { get; set; }
        public string ItemLabelDesignId { get { return warehouse.ItemLabelDesignId; } set { warehouse.ItemLabelDesignId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemLabelDesign { get; set; }
        public bool? DataWarehouseExcludeFromROA { get { return warehouse.DataWarehouseExcludeFromROA; } set { warehouse.DataWarehouseExcludeFromROA = value; } }
        public int? AvailabilityLateHours { get { return warehouse.AvailabilityLateHours; } set { warehouse.AvailabilityLateHours = value; } }
        public int? AvailabilityStartHour { get { return warehouse.AvailabilityStartHour; } set { warehouse.AvailabilityStartHour = value; } }
        public int? AvailabilityStopHour { get { return warehouse.AvailabilityStopHour; } set { warehouse.AvailabilityStopHour = value; } }
        public bool? IncludeTaxInAssetValue { get { return warehouse.IncludeTaxInAssetValue; } set { warehouse.IncludeTaxInAssetValue = value; } }
        public int? ProductionExchangeAvailabilityPercent { get { return warehouse.ProductionExchangeAvailabilityPercent; } set { warehouse.ProductionExchangeAvailabilityPercent = value; } }
        public bool? ProductionexchangeEnabled { get { return warehouse.ProductionexchangeEnabled; } set { warehouse.ProductionexchangeEnabled = value; } }
        public string ProductionExchangeWarehouseCode { get { return warehouse.ProductionExchangeWarehouseCode; } set { warehouse.ProductionExchangeWarehouseCode = value; } }
        public decimal? Week4RatePercent { get { return warehouse.Week4RatePercent; } set { warehouse.Week4RatePercent = value; } }
        public string GlSuffix { get { return warehouse.GlSuffix; } set { warehouse.GlSuffix = value; } }
        public bool? RequireScanVendorBarCodeOnReturn { get { return warehouse.RequireScanVendorBarCodeOnReturn; } set { warehouse.RequireScanVendorBarCodeOnReturn = value; } }
        public string GlPrefix { get { return warehouse.GlPrefix; } set { warehouse.GlPrefix = value; } }
        public string InternalDealId { get { return warehouse.InternalDealId; } set { warehouse.InternalDealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InternalDeal { get; set; }
        public string InternalVendorId { get { return warehouse.InternalVendorId; } set { warehouse.InternalVendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InternalVendor { get; set; }
        public bool? CalculateOnPoAfterApproved { get { return warehouse.CalculateOnPoAfterApproved; } set { warehouse.CalculateOnPoAfterApproved = value; } }
        public bool? TransferDefaultReturnToWarehouse { get { return warehouse.TransferDefaultReturnToWarehouse; } set { warehouse.TransferDefaultReturnToWarehouse = value; } }
        public bool? AvailabilityCalculateInBackground { get { return warehouse.AvailabilityCalculateInBackground; } set { warehouse.AvailabilityCalculateInBackground = value; } }
        public bool? QuikLocateCannotTransfer { get { return warehouse.QuikLocateCannotTransfer; } set { warehouse.QuikLocateCannotTransfer = value; } }
        public bool? QuikLocateDefaultRequiredDate { get { return warehouse.QuikLocateDefaultRequiredDate; } set { warehouse.QuikLocateDefaultRequiredDate = value; } }
        public int? QuikLocateRequiredDaysBefore { get { return warehouse.QuikLocateRequiredDaysBefore; } set { warehouse.QuikLocateRequiredDaysBefore = value; } }
        public bool? CalculateDefaultRentalRates { get { return warehouse.CalculateDefaultRentalRates; } set { warehouse.CalculateDefaultRentalRates = value; } }
        public decimal? RentalDailyRatePercentOfReplacementCost { get { return warehouse.RentalDailyRatePercentOfReplacementCost; } set { warehouse.RentalDailyRatePercentOfReplacementCost = value; } }
        public decimal? RentalWeeklyRateMultipleOfDailyRate { get { return warehouse.RentalWeeklyRateMultipleOfDailyRate; } set { warehouse.RentalWeeklyRateMultipleOfDailyRate = value; } }
        public bool? CheckInEnableScanningToAisleShelf { get { return warehouse.CheckInEnableScanningToAisleShelf; } set { warehouse.CheckInEnableScanningToAisleShelf = value; } }
        public string CurrencyId { get { return warehouse.CurrencyId; } set { warehouse.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Currency { get; set; }
        public string RentalBarCodeRangeId { get { return warehouse.RentalBarCodeRangeId; } set { warehouse.RentalBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalBarCodeRange { get; set; }
        public string RentalFixedAssetBarCodeRangeId { get { return warehouse.RentalFixedAssetBarCodeRangeId; } set { warehouse.RentalFixedAssetBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalFixedAssetBarCodeRange { get; set; }
        public string SalesBarCodeRangeId { get { return warehouse.SalesBarCodeRangeId; } set { warehouse.SalesBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesBarCodeRange { get; set; }
        public bool? AutoUpdateUnitValueOnReceivingHigherCostItem { get { return warehouse.AutoUpdateUnitValueOnReceivingHigherCostItem; } set { warehouse.AutoUpdateUnitValueOnReceivingHigherCostItem = value; } }
        public bool? MarkupReplacementCost { get { return warehouse.MarkupReplacementCost; } set { warehouse.MarkupReplacementCost = value; } }
        public decimal? ReplacementCostMarkupPercent { get { return warehouse.ReplacementCostMarkupPercent; } set { warehouse.ReplacementCostMarkupPercent = value; } }
        public int? AvailabilityHourlyDays { get { return warehouse.AvailabilityHourlyDays; } set { warehouse.AvailabilityHourlyDays = value; } }
        public decimal? SalesRestockPercent { get { return warehouse.SalesRestockPercent; } set { warehouse.SalesRestockPercent = value; } }
        public bool? AvailabilityExcludeConsigned { get { return warehouse.AvailabilityExcludeConsigned; } set { warehouse.AvailabilityExcludeConsigned = value; } }
        public bool? AvailabilityRequireConsignedReserved { get { return warehouse.AvailabilityRequireConsignedReserved; } set { warehouse.AvailabilityRequireConsignedReserved = value; } }
        public bool? AvailabilityEnableQcDelay { get { return warehouse.AvailabilityEnableQcDelay; } set { warehouse.AvailabilityEnableQcDelay = value; } }
        public bool? AvailabilityQcDelayExcludeWeekend { get { return warehouse.AvailabilityQcDelayExcludeWeekend; } set { warehouse.AvailabilityQcDelayExcludeWeekend = value; } }
        public bool? AvailabilityQcDelayExcludeHoliday { get { return warehouse.AvailabilityQcDelayExcludeHoliday; } set { warehouse.AvailabilityQcDelayExcludeHoliday = value; } }
        public bool? AvailabilityQcDelayIndefinite { get { return warehouse.AvailabilityQcDelayIndefinite; } set { warehouse.AvailabilityQcDelayIndefinite = value; } }
        public bool? Inactive { get { return warehouse.Inactive; } set { warehouse.Inactive = value; } }
        public string DateStamp { get { return warehouse.DateStamp; } set { warehouse.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
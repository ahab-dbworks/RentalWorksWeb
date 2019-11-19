using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseSettings.Warehouse
{
    [FwLogic(Id:"KXduciqmVEuD4")]
    public class WarehouseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseRecord warehouse = new WarehouseRecord();
        WarehouseLoader warehouseLoader = new WarehouseLoader();
        WarehouseBrowseLoader warehouseBrowseLoader = new WarehouseBrowseLoader();
        public WarehouseLogic()
        {
            dataRecords.Add(warehouse);
            dataLoader = warehouseLoader;
            browseLoader = warehouseBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"7CDZLCVBDZ2aX", IsPrimaryKey:true)]
        public string WarehouseId { get { return warehouse.WarehouseId; } set { warehouse.WarehouseId = value; } }

        [FwLogicProperty(Id:"7CDZLCVBDZ2aX", IsRecordTitle:true)]
        public string Warehouse { get { return warehouse.Warehouse; } set { warehouse.Warehouse = value; } }

        [FwLogicProperty(Id:"Y2hmK3Nhxf1")]
        public string WarehouseCode { get { return warehouse.WarehouseCode; } set { warehouse.WarehouseCode = value; } }

        [FwLogicProperty(Id:"Qv2MdWcUR5A")]
        public string TaxOptionId { get { return warehouse.TaxOptionId; } set { warehouse.TaxOptionId = value; } }

        [FwLogicProperty(Id:"qxxVhQ7e8qZku", IsReadOnly:true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id:"jXC2vTYi6Jvik", IsReadOnly:true)]
        public string TaxCountry { get; set; }

        [FwLogicProperty(Id:"n6dzSl1rbiNey", IsReadOnly:true)]
        public string TaxRule { get; set; }

        [FwLogicProperty(Id:"o0ztgmVdxP22z", IsReadOnly:true)]
        public decimal? RentalTaxRate1 { get; set; }

        [FwLogicProperty(Id:"j7Q7TY3mSU7e4", IsReadOnly:true)]
        public decimal? RentalTaxRate2 { get; set; }

        [FwLogicProperty(Id:"R5J1jFxXwwtk2", IsReadOnly:true)]
        public bool? RentalExempt { get; set; }

        [FwLogicProperty(Id:"L3yGBNy4Qx83B", IsReadOnly:true)]
        public decimal? SalesTaxRate1 { get; set; }

        [FwLogicProperty(Id:"vu7INJ4LL1wCk", IsReadOnly:true)]
        public decimal? SalesTaxRate2 { get; set; }

        [FwLogicProperty(Id:"TsWkuOaqpKoM2", IsReadOnly:true)]
        public bool? SalesExempt { get; set; }

        [FwLogicProperty(Id:"QqUhPggywvHn1", IsReadOnly:true)]
        public decimal? LaborTaxRate1 { get; set; }

        [FwLogicProperty(Id:"Z8hkbhNnFr6rn", IsReadOnly:true)]
        public decimal? LaborTaxRate2 { get; set; }

        [FwLogicProperty(Id:"kdSsmrFvmzGnf", IsReadOnly:true)]
        public bool? LaborExempt { get; set; }

        [FwLogicProperty(Id:"nZTubKo9R3U")]
        public string Attention { get { return warehouse.Attention; } set { warehouse.Attention = value; } }

        [FwLogicProperty(Id:"3njivtgI97M")]
        public string Address1 { get { return warehouse.Address1; } set { warehouse.Address1 = value; } }

        [FwLogicProperty(Id:"CqwJbaN4WZC")]
        public string Address2 { get { return warehouse.Address2; } set { warehouse.Address2 = value; } }

        [FwLogicProperty(Id:"qiVz6VOqioi")]
        public string City { get { return warehouse.City; } set { warehouse.City = value; } }

        [FwLogicProperty(Id:"mAm5yHIOLo2")]
        public string Zip { get { return warehouse.Zip; } set { warehouse.Zip = value; } }

        [FwLogicProperty(Id:"HbK3dXiqv5K")]
        public string State { get { return warehouse.State; } set { warehouse.State = value; } }

        [FwLogicProperty(Id:"RECRz3z4LNe")]
        public string CountryId { get { return warehouse.CountryId; } set { warehouse.CountryId = value; } }

        [FwLogicProperty(Id:"jXC2vTYi6Jvik", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"eHxOBSiEd96")]
        public string Phone { get { return warehouse.Phone; } set { warehouse.Phone = value; } }

        [FwLogicProperty(Id:"1YC6gt9B2PV")]
        public string Fax { get { return warehouse.Fax; } set { warehouse.Fax = value; } }

        [FwLogicProperty(Id:"YZrliYQQ6NE")]
        public string AssignBarCodesBy { get { return warehouse.AssignBarCodesBy; } set { warehouse.AssignBarCodesBy = value; } }

        [FwLogicProperty(Id:"XE1NnW1r3q6")]
        public decimal? SalesMarkupPercent { get { return warehouse.SalesMarkupPercent; } set { warehouse.SalesMarkupPercent = value; } }

        [FwLogicProperty(Id:"mcWDlBfN10x")]
        public decimal? PartsMarkupPercent { get { return warehouse.PartsMarkupPercent; } set { warehouse.PartsMarkupPercent = value; } }

        [FwLogicProperty(Id:"lUiHqYquFwk")]
        public bool? MarkupSales { get { return warehouse.MarkupSales; } set { warehouse.MarkupSales = value; } }

        [FwLogicProperty(Id:"k60nHdmSrEz")]
        public bool? MarkupParts { get { return warehouse.MarkupParts; } set { warehouse.MarkupParts = value; } }

        [FwLogicProperty(Id:"9EWSyGphygt")]
        public bool? IncludeFreightInSalesCost { get { return warehouse.IncludeFreightInSalesCost; } set { warehouse.IncludeFreightInSalesCost = value; } }

        [FwLogicProperty(Id:"ZdaquTseafB")]
        public bool? IncludeFreightInPartsCost { get { return warehouse.IncludeFreightInPartsCost; } set { warehouse.IncludeFreightInPartsCost = value; } }

        [FwLogicProperty(Id:"KNrqilUUrmE")]
        public bool? ReceiveVendorBarCodes { get { return warehouse.ReceiveVendorBarCodes; } set { warehouse.ReceiveVendorBarCodes = value; } }

        [FwLogicProperty(Id:"9TZQ5oGfiCb")]
        public bool? AllowNegativeInventory { get { return warehouse.AllowNegativeInventory; } set { warehouse.AllowNegativeInventory = value; } }

        [FwLogicProperty(Id:"9T8EyseuBOB")]
        public string DefaultDeliveryType { get { return warehouse.DefaultDeliveryType; } set { warehouse.DefaultDeliveryType = value; } }

        [FwLogicProperty(Id:"ffqjbtpmZrR")]
        public bool? ExchangedItemsRepairByDefault { get { return warehouse.ExchangedItemsRepairByDefault; } set { warehouse.ExchangedItemsRepairByDefault = value; } }

        [FwLogicProperty(Id:"ZcQ5hHDfWu3")]
        public string StagingCompleteComponents { get { return warehouse.StagingCompleteComponents; } set { warehouse.StagingCompleteComponents = value; } }

        [FwLogicProperty(Id:"LiAq30tbodD")]
        public string CheckInSortBy { get { return warehouse.CheckInSortBy; } set { warehouse.CheckInSortBy = value; } }

        [FwLogicProperty(Id:"Wz8Fe780xJp")]
        public string DefaultPackageTruckScheduleMethod { get { return warehouse.DefaultPackageTruckScheduleMethod; } set { warehouse.DefaultPackageTruckScheduleMethod = value; } }

        [FwLogicProperty(Id:"UtV8oiA9OQI")]
        public bool? StageQuantityAccessories { get { return warehouse.StageQuantityAccessories; } set { warehouse.StageQuantityAccessories = value; } }

        [FwLogicProperty(Id:"jBqqeDrWINv")]
        public bool? PromptForCheckOutExceptions { get { return warehouse.PromptForCheckOutExceptions; } set { warehouse.PromptForCheckOutExceptions = value; } }

        [FwLogicProperty(Id:"vwHgw9nvJPE")]
        public bool? PromptForCheckInExceptions { get { return warehouse.PromptForCheckInExceptions; } set { warehouse.PromptForCheckInExceptions = value; } }

        [FwLogicProperty(Id:"l99zLsytDqW")]
        public bool? StagingShowCheckedInHoldingItems { get { return warehouse.StagingShowCheckedInHoldingItems; } set { warehouse.StagingShowCheckedInHoldingItems = value; } }

        [FwLogicProperty(Id:"ZHkDHoXuxY7")]
        public string PoDeliveryType { get { return warehouse.PoDeliveryType; } set { warehouse.PoDeliveryType = value; } }

        [FwLogicProperty(Id:"7L6nBD4UtWp")]
        public int? AvailabilityCacheDays { get { return warehouse.AvailabilityCacheDays; } set { warehouse.AvailabilityCacheDays = value; } }

        [FwLogicProperty(Id:"ouLr8RKZMPC")]
        public bool? AvailabilityPreserveConflicts { get { return warehouse.AvailabilityPreserveConflicts; } set { warehouse.AvailabilityPreserveConflicts = value; } }

        [FwLogicProperty(Id:"xkTEcplbHuO")]
        public int? DefaultRepairDays { get { return warehouse.DefaultRepairDays; } set { warehouse.DefaultRepairDays = value; } }

        [FwLogicProperty(Id:"Oms9O3Mgn5Y")]
        public string QuikLocateDefaultDeliveryType { get { return warehouse.QuikLocateDefaultDeliveryType; } set { warehouse.QuikLocateDefaultDeliveryType = value; } }

        [FwLogicProperty(Id:"ROWG2kmvtu8")]
        public string RepairBillableOrderAgentFrom { get { return warehouse.RepairBillableOrderAgentFrom; } set { warehouse.RepairBillableOrderAgentFrom = value; } }

        [FwLogicProperty(Id:"cEQL9Cb0Hrx")]
        public bool? ReturnListPrintInQuantity { get { return warehouse.ReturnListPrintInQuantity; } set { warehouse.ReturnListPrintInQuantity = value; } }

        [FwLogicProperty(Id:"ouKwYVZfaLi")]
        public bool? ReturnListPrintOutQuantity { get { return warehouse.ReturnListPrintOutQuantity; } set { warehouse.ReturnListPrintOutQuantity = value; } }

        [FwLogicProperty(Id:"sbH2u4wOhvj")]
        public bool? AvailabilityUseOnPO { get { return warehouse.AvailabilityUseOnPO; } set { warehouse.AvailabilityUseOnPO = value; } }

        [FwLogicProperty(Id:"f48ygRcGEbi")]
        public string RegionId { get { return warehouse.RegionId; } set { warehouse.RegionId = value; } }

        [FwLogicProperty(Id:"lGxRDqfZMuUpd", IsReadOnly:true)]
        public string Region { get; set; }

        [FwLogicProperty(Id:"k9ZDrSZcnzm")]
        public string Color { get { return warehouse.Color; } set { warehouse.Color = value; } }

        [FwLogicProperty(Id:"dYrcTbsen7a")]
        public int? AvailabilityLateDays { get { return warehouse.AvailabilityLateDays; } set { warehouse.AvailabilityLateDays = value; } }

        [FwLogicProperty(Id:"ShfYX4NJTT1")]
        public bool? UseBarCodeLabelDesigner { get { return warehouse.UseBarCodeLabelDesigner; } set { warehouse.UseBarCodeLabelDesigner = value; } }

        [FwLogicProperty(Id:"qtDqGxxCq83")]
        public string InventoryLabelDesignId { get { return warehouse.InventoryLabelDesignId; } set { warehouse.InventoryLabelDesignId = value; } }

        [FwLogicProperty(Id:"OOfCp3I6sqpw5", IsReadOnly:true)]
        public string InventoryLabelDesign { get; set; }

        [FwLogicProperty(Id:"PE8GhebVdE6")]
        public string ItemLabelDesignId { get { return warehouse.ItemLabelDesignId; } set { warehouse.ItemLabelDesignId = value; } }

        [FwLogicProperty(Id:"tdEMii1EL6ZFL", IsReadOnly:true)]
        public string ItemLabelDesign { get; set; }

        [FwLogicProperty(Id:"38g4dgzU3R6")]
        public bool? DataWarehouseExcludeFromROA { get { return warehouse.DataWarehouseExcludeFromROA; } set { warehouse.DataWarehouseExcludeFromROA = value; } }

        [FwLogicProperty(Id:"nWTQjDXxMbh")]
        public int? AvailabilityLateHours { get { return warehouse.AvailabilityLateHours; } set { warehouse.AvailabilityLateHours = value; } }

        [FwLogicProperty(Id:"FHNIzE1vfrb")]
        public int? AvailabilityStartHour { get { return warehouse.AvailabilityStartHour; } set { warehouse.AvailabilityStartHour = value; } }

        [FwLogicProperty(Id:"2Or9rJivRoy")]
        public int? AvailabilityStopHour { get { return warehouse.AvailabilityStopHour; } set { warehouse.AvailabilityStopHour = value; } }

        [FwLogicProperty(Id:"huimcIQsz47")]
        public bool? IncludeTaxInAssetValue { get { return warehouse.IncludeTaxInAssetValue; } set { warehouse.IncludeTaxInAssetValue = value; } }

        [FwLogicProperty(Id:"YrT7IfM0BDK")]
        public int? ProductionExchangeAvailabilityPercent { get { return warehouse.ProductionExchangeAvailabilityPercent; } set { warehouse.ProductionExchangeAvailabilityPercent = value; } }

        [FwLogicProperty(Id:"ON2cOTschjO")]
        public bool? ProductionexchangeEnabled { get { return warehouse.ProductionexchangeEnabled; } set { warehouse.ProductionexchangeEnabled = value; } }

        [FwLogicProperty(Id:"vAeLe93YiDQ")]
        public string ProductionExchangeWarehouseCode { get { return warehouse.ProductionExchangeWarehouseCode; } set { warehouse.ProductionExchangeWarehouseCode = value; } }

        [FwLogicProperty(Id:"2ZJzvF4TdqE")]
        public decimal? Week4RatePercent { get { return warehouse.Week4RatePercent; } set { warehouse.Week4RatePercent = value; } }

        [FwLogicProperty(Id:"5spQFjMifl1")]
        public string GlSuffix { get { return warehouse.GlSuffix; } set { warehouse.GlSuffix = value; } }

        [FwLogicProperty(Id:"cDdBSSVRbsE")]
        public bool? RequireScanVendorBarCodeOnReturn { get { return warehouse.RequireScanVendorBarCodeOnReturn; } set { warehouse.RequireScanVendorBarCodeOnReturn = value; } }

        [FwLogicProperty(Id:"2AuWVwNo1oG")]
        public string GlPrefix { get { return warehouse.GlPrefix; } set { warehouse.GlPrefix = value; } }

        [FwLogicProperty(Id:"ezgxG2zyVMT")]
        public string InternalDealId { get { return warehouse.InternalDealId; } set { warehouse.InternalDealId = value; } }

        [FwLogicProperty(Id:"yzhkxXCrJsBvL", IsReadOnly:true)]
        public string InternalDeal { get; set; }

        [FwLogicProperty(Id:"rihHzpZ7tAS")]
        public string InternalVendorId { get { return warehouse.InternalVendorId; } set { warehouse.InternalVendorId = value; } }

        [FwLogicProperty(Id:"9f3FrCYeXYyOe", IsReadOnly:true)]
        public string InternalVendor { get; set; }

        [FwLogicProperty(Id:"sV86G7i68bE")]
        public bool? CalculateOnPoAfterApproved { get { return warehouse.CalculateOnPoAfterApproved; } set { warehouse.CalculateOnPoAfterApproved = value; } }

        [FwLogicProperty(Id:"Kli7qlJlzkb")]
        public bool? TransferDefaultReturnToWarehouse { get { return warehouse.TransferDefaultReturnToWarehouse; } set { warehouse.TransferDefaultReturnToWarehouse = value; } }

        [FwLogicProperty(Id:"cY66QmGS4rA")]
        public bool? AvailabilityCalculateInBackground { get { return warehouse.AvailabilityCalculateInBackground; } set { warehouse.AvailabilityCalculateInBackground = value; } }

        [FwLogicProperty(Id:"noJucNeBUYU")]
        public bool? QuikLocateCannotTransfer { get { return warehouse.QuikLocateCannotTransfer; } set { warehouse.QuikLocateCannotTransfer = value; } }

        [FwLogicProperty(Id:"rhxuEPvziPF")]
        public string QuikLocateDefaultRequiredDate { get { return warehouse.QuikLocateDefaultRequiredDate; } set { warehouse.QuikLocateDefaultRequiredDate = value; } }

        [FwLogicProperty(Id:"buGMfhPak1s")]
        public int? QuikLocateRequiredDaysBefore { get { return warehouse.QuikLocateRequiredDaysBefore; } set { warehouse.QuikLocateRequiredDaysBefore = value; } }

        [FwLogicProperty(Id:"DJtj9sAecCw")]
        public bool? CalculateDefaultRentalRates { get { return warehouse.CalculateDefaultRentalRates; } set { warehouse.CalculateDefaultRentalRates = value; } }

        [FwLogicProperty(Id:"CRzI8mSDvfW")]
        public decimal? RentalDailyRatePercentOfReplacementCost { get { return warehouse.RentalDailyRatePercentOfReplacementCost; } set { warehouse.RentalDailyRatePercentOfReplacementCost = value; } }

        [FwLogicProperty(Id:"tEhKoidb9gv")]
        public decimal? RentalWeeklyRateMultipleOfDailyRate { get { return warehouse.RentalWeeklyRateMultipleOfDailyRate; } set { warehouse.RentalWeeklyRateMultipleOfDailyRate = value; } }

        [FwLogicProperty(Id:"8xEkrxoYFoJ")]
        public bool? CheckInEnableScanningToAisleShelf { get { return warehouse.CheckInEnableScanningToAisleShelf; } set { warehouse.CheckInEnableScanningToAisleShelf = value; } }

        [FwLogicProperty(Id:"SIv3DMgLqgV")]
        public string CurrencyId { get { return warehouse.CurrencyId; } set { warehouse.CurrencyId = value; } }

        [FwLogicProperty(Id:"ejPJZG4OJd8fI", IsReadOnly:true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id:"ejPJZG4OJd8fI", IsReadOnly:true)]
        public string Currency { get; set; }

        [FwLogicProperty(Id:"yRWnfvOVl0k")]
        public string RentalBarCodeRangeId { get { return warehouse.RentalBarCodeRangeId; } set { warehouse.RentalBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"0f3Lzff2JcaU6", IsReadOnly:true)]
        public string RentalBarCodeRange { get; set; }

        [FwLogicProperty(Id:"3KJ545lHFEI")]
        public string RentalFixedAssetBarCodeRangeId { get { return warehouse.RentalFixedAssetBarCodeRangeId; } set { warehouse.RentalFixedAssetBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"OCci7usHoUxvo", IsReadOnly:true)]
        public string RentalFixedAssetBarCodeRange { get; set; }

        [FwLogicProperty(Id:"GG9IouF7SLM")]
        public string SalesBarCodeRangeId { get { return warehouse.SalesBarCodeRangeId; } set { warehouse.SalesBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"2PjHMX13W2qiU", IsReadOnly:true)]
        public string SalesBarCodeRange { get; set; }

        [FwLogicProperty(Id:"MYzTMzcYqZ5")]
        public bool? AutoUpdateUnitValueOnReceivingHigherCostItem { get { return warehouse.AutoUpdateUnitValueOnReceivingHigherCostItem; } set { warehouse.AutoUpdateUnitValueOnReceivingHigherCostItem = value; } }

        [FwLogicProperty(Id:"tgFyfN3InFc")]
        public bool? MarkupReplacementCost { get { return warehouse.MarkupReplacementCost; } set { warehouse.MarkupReplacementCost = value; } }

        [FwLogicProperty(Id:"0rTAskq2P0Q")]
        public decimal? ReplacementCostMarkupPercent { get { return warehouse.ReplacementCostMarkupPercent; } set { warehouse.ReplacementCostMarkupPercent = value; } }

        [FwLogicProperty(Id:"ykJWaPicnNW")]
        public int? AvailabilityHourlyDays { get { return warehouse.AvailabilityHourlyDays; } set { warehouse.AvailabilityHourlyDays = value; } }

        [FwLogicProperty(Id:"dczCaQpp0YP")]
        public decimal? SalesRestockPercent { get { return warehouse.SalesRestockPercent; } set { warehouse.SalesRestockPercent = value; } }

        [FwLogicProperty(Id:"aepmv5Fw7IR")]
        public bool? AvailabilityExcludeConsigned { get { return warehouse.AvailabilityExcludeConsigned; } set { warehouse.AvailabilityExcludeConsigned = value; } }

        [FwLogicProperty(Id:"lJAvCgegjqC")]
        public bool? AvailabilityRequireConsignedReserved { get { return warehouse.AvailabilityRequireConsignedReserved; } set { warehouse.AvailabilityRequireConsignedReserved = value; } }

        [FwLogicProperty(Id:"eDjQnoLo7Dz")]
        public bool? AvailabilityEnableQcDelay { get { return warehouse.AvailabilityEnableQcDelay; } set { warehouse.AvailabilityEnableQcDelay = value; } }

        [FwLogicProperty(Id:"ilq4MXK2FAo")]
        public bool? AvailabilityQcDelayExcludeWeekend { get { return warehouse.AvailabilityQcDelayExcludeWeekend; } set { warehouse.AvailabilityQcDelayExcludeWeekend = value; } }

        [FwLogicProperty(Id:"k8Brh18R6sX")]
        public bool? AvailabilityQcDelayExcludeHoliday { get { return warehouse.AvailabilityQcDelayExcludeHoliday; } set { warehouse.AvailabilityQcDelayExcludeHoliday = value; } }

        [FwLogicProperty(Id:"Jw9SZqhZeiq")]
        public bool? AvailabilityQcDelayIndefinite { get { return warehouse.AvailabilityQcDelayIndefinite; } set { warehouse.AvailabilityQcDelayIndefinite = value; } }

        [FwLogicProperty(Id:"gmUbESjjpR5")]
        public bool? Inactive { get { return warehouse.Inactive; } set { warehouse.Inactive = value; } }

        [FwLogicProperty(Id:"gErPZwPIJoi")]
        public string DateStamp { get { return warehouse.DateStamp; } set { warehouse.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

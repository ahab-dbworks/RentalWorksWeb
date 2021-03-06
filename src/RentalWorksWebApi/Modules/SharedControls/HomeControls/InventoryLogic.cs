using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.HomeControls.ItemDimension;
using WebApi.Modules.HomeControls.Master;
//using WebApi.Logic;
//using static FwStandard.Data.FwDataReadWriteRecord;
using System.Reflection;
//using WebApi;
//using FwStandard.SqlServer;
using WebApi.Logic;
using WebApi.Modules.Inventory.Inventory;
using WebApi.Modules.Settings.SystemSettings.InventorySettings;

namespace WebApi.Modules.HomeControls.Inventory
{
    public abstract class InventoryLogic : MasterLogic
    {

        //------------------------------------------------------------------------------------ 
        protected ItemDimensionRecord primaryDimension = new ItemDimensionRecord();
        protected ItemDimensionRecord secondaryDimension = new ItemDimensionRecord();
        //InventoryBrowseLoader inventoryBrowseLoader = new InventoryBrowseLoader();
        private bool _changingTrackedBy = false;

        public InventoryLogic() : base()
        {
            dataRecords.Add(primaryDimension);
            dataRecords.Add(secondaryDimension);
            //browseLoader = inventoryBrowseLoader;
            BeforeValidate += OnBeforeValidateInventory;
            BeforeSave += OnBeforeSave;
            UseTransactionToSave = true;
        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "FJxf8XLF4xY4", IsPrimaryKey: true)]
        public string InventoryId { get { return master.MasterId; } set { master.MasterId = value; } }

        [FwLogicProperty(Id: "OOIFYzSWEP1r", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id: "cRs5eaI7zyAK", IsReadOnly: true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id: "sXkMzkhWcSYH")]
        public string AvailableFrom { get { return master.AvailableFrom; } set { master.AvailableFrom = value; } }

        [FwLogicProperty(Id: "OPCHq5JjF8V7")]
        public string TrackedBy { get { return master.TrackedBy; } set { master.TrackedBy = value; } }

        [FwLogicProperty(Id: "VXivpyosX92Kc", IsNotAudited: true)]
        public string ConfirmTrackedBy { get; set; }

        [FwLogicProperty(Id: "tKUIWhrXo9ht")]
        public string Rank { get { return master.Rank; } set { master.Rank = value; } }

        [FwLogicProperty(Id: "D80RmiRi3Aff")]
        public string ManufacturerPartNumber { get { return master.ManufacturerPartNumber; } set { master.ManufacturerPartNumber = value; } }

        [FwLogicProperty(Id: "rF1PPmnLaJ0oG")]
        public string ManufacturerId { get { return master.ManufacturerId; } set { master.ManufacturerId = value; } }

        [FwLogicProperty(Id: "DZne9En4ddJTE", IsReadOnly: true)]
        public string Manufacturer { get; set; }


        [FwLogicProperty(Id: "rUjj9v4DlIo1x")]
        public string ManufacturerUrl { get { return master.ManufacturerUrl; } set { master.ManufacturerUrl = value; } }
        [FwLogicProperty(Id: "uIvXyX1UGsruy")]
        public bool? ExcludeImageFromQuoteOrderPrint { get { return master.ExcludeImageFromQuoteOrderPrint; } set { master.ExcludeImageFromQuoteOrderPrint = value; } }


        [FwLogicProperty(Id: "TIH65zNLyOKl")]
        public bool? NoAvailabilityCheck { get { return master.NoAvailabilityCheck; } set { master.NoAvailabilityCheck = value; } }

        [FwLogicProperty(Id: "G9acxT3RALqK")]
        public bool? AvailabilityManuallyResolveConflicts { get { return master.AvailabilityManuallyResolveConflicts; } set { master.AvailabilityManuallyResolveConflicts = value; } }

        [FwLogicProperty(Id: "tYL46AbH2f9p")]
        public bool? SendAvailabilityAlert { get { return master.SendAvailabilityAlert; } set { master.SendAvailabilityAlert = value; } }

        [FwLogicProperty(Id: "EKdqbcStd5O4")]
        public string PrimaryDimensionUniqueId { get { return master.PrimaryDimensionId; } set { master.PrimaryDimensionId = value; primaryDimension.UniqueId = value; } }

        [FwLogicProperty(Id: "jzvNoUou6afi")]
        public string PrimaryDimensionDescription { get { return primaryDimension.Description; } set { primaryDimension.Description = value; } }

        [FwLogicProperty(Id: "82X6PPtS55KA")]
        public int? PrimaryDimensionShipWeightLbs { get { return primaryDimension.ShipWeightLbs; } set { primaryDimension.ShipWeightLbs = value; } }

        [FwLogicProperty(Id: "Ewe4hx4aIWY4")]
        public int? PrimaryDimensionShipWeightOz { get { return primaryDimension.ShipWeightOz; } set { primaryDimension.ShipWeightOz = value; } }

        [FwLogicProperty(Id: "g0uIrxqVQ5Ms")]
        public int? PrimaryDimensionWeightInCaseLbs { get { return primaryDimension.WeightInCaseLbs; } set { primaryDimension.WeightInCaseLbs = value; } }

        [FwLogicProperty(Id: "VQQeYVnCSuhi")]
        public int? PrimaryDimensionWeightInCaseOz { get { return primaryDimension.WeightInCaseOz; } set { primaryDimension.WeightInCaseOz = value; } }

        [FwLogicProperty(Id: "wMqXnmQqqspt")]
        public int? PrimaryDimensionWidthFt { get { return primaryDimension.WidthFt; } set { primaryDimension.WidthFt = value; } }

        [FwLogicProperty(Id: "cYcUObw6XvOU")]
        public int? PrimaryDimensionWidthIn { get { return primaryDimension.WidthIn; } set { primaryDimension.WidthIn = value; } }

        [FwLogicProperty(Id: "aYu9xkUlXJ3D")]
        public int? PrimaryDimensionHeightFt { get { return primaryDimension.HeightFt; } set { primaryDimension.HeightFt = value; } }

        [FwLogicProperty(Id: "JldcSXiEPcPm")]
        public int? PrimaryDimensionHeightIn { get { return primaryDimension.HeightIn; } set { primaryDimension.HeightIn = value; } }

        [FwLogicProperty(Id: "kYj3Z6UpJdI5")]
        public int? PrimaryDimensionLengthFt { get { return primaryDimension.LengthFt; } set { primaryDimension.LengthFt = value; } }

        [FwLogicProperty(Id: "L8xpW417h6US")]
        public int? PrimaryDimensionLengthIn { get { return primaryDimension.LengthIn; } set { primaryDimension.LengthIn = value; } }

        [FwLogicProperty(Id: "T0U5193nHv0N")]
        public int? PrimaryDimensionShipWeightKg { get { return primaryDimension.ShipWeightKg; } set { primaryDimension.ShipWeightKg = value; } }

        [FwLogicProperty(Id: "yaXGQys6fxo7")]
        public int? PrimaryDimensionShipWeightG { get { return primaryDimension.ShipWeightG; } set { primaryDimension.ShipWeightG = value; } }

        [FwLogicProperty(Id: "NRcVKCHoEQuP")]
        public int? PrimaryDimensionWeightInCaseKg { get { return primaryDimension.WeightInCaseKg; } set { primaryDimension.WeightInCaseKg = value; } }

        [FwLogicProperty(Id: "FpanjgnqOirY")]
        public int? PrimaryDimensionWeightInCaseG { get { return primaryDimension.WeightInCaseG; } set { primaryDimension.WeightInCaseG = value; } }

        [FwLogicProperty(Id: "XVQp17ohXnW1")]
        public int? PrimaryDimensionWidthM { get { return primaryDimension.WidthM; } set { primaryDimension.WidthM = value; } }

        [FwLogicProperty(Id: "ld3cOUyqWrtr")]
        public int? PrimaryDimensionWidthCm { get { return primaryDimension.WidthCm; } set { primaryDimension.WidthCm = value; } }

        [FwLogicProperty(Id: "Iygu4RNfYNSw")]
        public int? PrimaryDimensionHeightM { get { return primaryDimension.HeightM; } set { primaryDimension.HeightM = value; } }

        [FwLogicProperty(Id: "zhs8kFuGC2jC")]
        public int? PrimaryDimensionHeightCm { get { return primaryDimension.HeightCm; } set { primaryDimension.HeightCm = value; } }

        [FwLogicProperty(Id: "ULNh25RzdYLE")]
        public int? PrimaryDimensionLengthM { get { return primaryDimension.LengthM; } set { primaryDimension.LengthM = value; } }

        [FwLogicProperty(Id: "DguNxMhXZ9FN")]
        public int? PrimaryDimensionLengthCm { get { return primaryDimension.LengthCm; } set { primaryDimension.LengthCm = value; } }

        [FwLogicProperty(Id: "MFD2qqVS7TwY")]
        public string SecondaryDimensionUniqueId { get { return master.SecondayDimensionId; } set { master.SecondayDimensionId = value; secondaryDimension.UniqueId = value; } }

        [FwLogicProperty(Id: "DYRE3PbGDDww")]
        public string SecondaryDimensionDescription { get { return secondaryDimension.Description; } set { secondaryDimension.Description = value; } }

        [FwLogicProperty(Id: "JhPAYP89Gflj")]
        public int? SecondaryDimensionShipWeightLbs { get { return secondaryDimension.ShipWeightLbs; } set { secondaryDimension.ShipWeightLbs = value; } }

        [FwLogicProperty(Id: "P3JmYkWYzoH0")]
        public int? SecondaryDimensionShipWeightOz { get { return secondaryDimension.ShipWeightOz; } set { secondaryDimension.ShipWeightOz = value; } }

        [FwLogicProperty(Id: "ytAgrD5YLlmT")]
        public int? SecondaryDimensionWeightInCaseLbs { get { return secondaryDimension.WeightInCaseLbs; } set { secondaryDimension.WeightInCaseLbs = value; } }

        [FwLogicProperty(Id: "IO29Fm6QnqnE")]
        public int? SecondaryDimensionWeightInCaseOz { get { return secondaryDimension.WeightInCaseOz; } set { secondaryDimension.WeightInCaseOz = value; } }

        [FwLogicProperty(Id: "94rwrIsWjaKb")]
        public int? SecondaryDimensionWidthFt { get { return secondaryDimension.WidthFt; } set { secondaryDimension.WidthFt = value; } }

        [FwLogicProperty(Id: "ETrg4SDPyuqJ")]
        public int? SecondaryDimensionWidthIn { get { return secondaryDimension.WidthIn; } set { secondaryDimension.WidthIn = value; } }

        [FwLogicProperty(Id: "imlsEVenuL67")]
        public int? SecondaryDimensionHeightFt { get { return secondaryDimension.HeightFt; } set { secondaryDimension.HeightFt = value; } }

        [FwLogicProperty(Id: "3T0M1KZ7MqSV")]
        public int? SecondaryDimensionHeightIn { get { return secondaryDimension.HeightIn; } set { secondaryDimension.HeightIn = value; } }

        [FwLogicProperty(Id: "iIb0uu3iCI1w")]
        public int? SecondaryDimensionLengthFt { get { return secondaryDimension.LengthFt; } set { secondaryDimension.LengthFt = value; } }

        [FwLogicProperty(Id: "WAciXr9F5tfj")]
        public int? SecondaryDimensionLengthIn { get { return secondaryDimension.LengthIn; } set { secondaryDimension.LengthIn = value; } }

        [FwLogicProperty(Id: "yguq1eDUy8ZZ")]
        public int? SecondaryDimensionShipWeightKg { get { return secondaryDimension.ShipWeightKg; } set { secondaryDimension.ShipWeightKg = value; } }

        [FwLogicProperty(Id: "vENG6LiV0pTW")]
        public int? SecondaryDimensionShipWeightG { get { return secondaryDimension.ShipWeightG; } set { secondaryDimension.ShipWeightG = value; } }

        [FwLogicProperty(Id: "YOxr62vrymKr")]
        public int? SecondaryDimensionWeightInCaseKg { get { return secondaryDimension.WeightInCaseKg; } set { secondaryDimension.WeightInCaseKg = value; } }

        [FwLogicProperty(Id: "Jdnyqr15Stzn")]
        public int? SecondaryDimensionWeightInCaseG { get { return secondaryDimension.WeightInCaseG; } set { secondaryDimension.WeightInCaseG = value; } }

        [FwLogicProperty(Id: "UzwOIkKb9w2X")]
        public int? SecondaryDimensionWidthM { get { return secondaryDimension.WidthM; } set { secondaryDimension.WidthM = value; } }

        [FwLogicProperty(Id: "E3IVbHPMg5pe")]
        public int? SecondaryDimensionWidthCm { get { return secondaryDimension.WidthCm; } set { secondaryDimension.WidthCm = value; } }

        [FwLogicProperty(Id: "nLQNDvb6r8zL")]
        public int? SecondaryDimensionHeightM { get { return secondaryDimension.HeightM; } set { secondaryDimension.HeightM = value; } }

        [FwLogicProperty(Id: "zlYDuQ9txSw5")]
        public int? SecondaryDimensionHeightCm { get { return secondaryDimension.HeightCm; } set { secondaryDimension.HeightCm = value; } }

        [FwLogicProperty(Id: "48HlNPOsjnho")]
        public int? SecondaryDimensionLengthM { get { return secondaryDimension.LengthM; } set { secondaryDimension.LengthM = value; } }

        [FwLogicProperty(Id: "W04Q46o9zVu1")]
        public int? SecondaryDimensionLengthCm { get { return secondaryDimension.LengthCm; } set { secondaryDimension.LengthCm = value; } }


        [FwLogicProperty(Id: "y0jNMsZbwVTq7")]
        public string CountryOfOriginId { get { return master.CountryOfOriginId; } set { master.CountryOfOriginId = value; } }

        [FwLogicProperty(Id: "FJH6W3qGF4Tk0", IsReadOnly: true)]
        public string CountryOfOrigin { get; set; }


        [FwLogicProperty(Id: "v7dGxdNTY9RY")]
        public bool? DisplayInSummaryModeWhenRateIsZero { get { return master.DisplayInSummaryModeWhenRateIsZero; } set { master.DisplayInSummaryModeWhenRateIsZero = value; } }

        [FwLogicProperty(Id: "aUhWeds5fZOm")]
        public bool? QcRequired { get { return master.QcRequired; } set { master.QcRequired = value; } }

        [FwLogicProperty(Id: "Tklr4TyFHKB6")]
        public string QcTime { get { return master.QcTime; } set { master.QcTime = value; } }

        [FwLogicProperty(Id: "GabrR2xEgNT8")]
        public bool? CopyAttributesAsNote { get { return master.CopyAttributesAsNote; } set { master.CopyAttributesAsNote = value; } }

        [FwLogicProperty(Id: "Y3t3X6hEpcHl")]
        public bool? TrackAssetUsage { get { return master.TrackAssetUsage; } set { master.TrackAssetUsage = value; } }

        [FwLogicProperty(Id: "SzciRzXcUNwk")]
        public bool? TrackLampUsage { get { return master.TrackLampUsage; } set { master.TrackLampUsage = value; } }

        [FwLogicProperty(Id: "fvhW9vKzOo7T")]
        public bool? TrackStrikes { get { return master.TrackStrikes; } set { master.TrackStrikes = value; } }

        [FwLogicProperty(Id: "4iegPUH9jnIo")]
        public bool? TrackCandles { get { return master.TrackCandles; } set { master.TrackCandles = value; } }

        [FwLogicProperty(Id: "1jpWb6QSlRRY")]
        public int? LampCount { get { return master.LampCount; } set { master.LampCount = value; } }

        [FwLogicProperty(Id: "5vDKoMbSKC1H")]
        public int? MinimumFootCandles { get { return master.MinimumFootCandles; } set { master.MinimumFootCandles = value; } }

        [FwLogicProperty(Id: "tQ5mma2iUBkf")]
        public bool? TrackSoftware { get { return master.TrackSoftware; } set { master.TrackSoftware = value; } }

        [FwLogicProperty(Id: "Qq62IbJk9iOj")]
        public string SoftwareVersion { get { return master.SoftwareVersion; } set { master.SoftwareVersion = value; } }

        [FwLogicProperty(Id: "xDiTjPrOPSnu")]
        public string SoftwareEffectiveDate { get { return master.SoftwareEffectiveDate; } set { master.SoftwareEffectiveDate = value; } }

        [FwLogicProperty(Id: "hYjnM5FeLLg5")]
        public bool? WarehouseSpecificPackage { get { return master.WarehouseSpecificPackage; } set { master.WarehouseSpecificPackage = value; } }

        [FwLogicProperty(Id: "zRuOXwkm25MV")]
        public string CompletePackagePrice { get { return master.PackagePrice; } set { master.PackagePrice = value; } }

        [FwLogicProperty(Id: "BmlVKXeDU54G")]
        public string KitPackagePrice { get { return master.PackagePrice; } set { master.PackagePrice = value; } }

        [FwLogicProperty(Id: "EGXULbx61APZ")]
        public bool? SeparatePackageOnQuoteOrder { get { return master.SeparatePackageOnQuoteOrder; } set { master.SeparatePackageOnQuoteOrder = value; } }

        [FwLogicProperty(Id: "FX897egQHBSX", IsReadOnly: true)]
        public string ContainerId { get { return master.ContainerId; } set { master.ContainerId = value; } }

        [FwLogicProperty(Id: "8YZvXGKmIVlC", IsReadOnly: true)]
        public string ContainerScannableInventoryId { get; set; }

        [FwLogicProperty(Id: "j8QOFMOkCj2R", IsReadOnly: true)]
        public string ContainerScannableICode { get; set; }

        [FwLogicProperty(Id: "j0cF1bTHBi4v", IsReadOnly: true)]
        public string ContainerScannableDescription { get; set; }

        [FwLogicProperty(Id: "Du3IT3W0rHeP")]
        public bool? AutomaticallyRebuildContainerAtCheckIn { get { return master.AutomaticallyRebuildContainerAtCheckIn; } set { master.AutomaticallyRebuildContainerAtCheckIn = value; } }

        [FwLogicProperty(Id: "JPnLYv0cXKk9")]
        public bool? AutomaticallyRebuildContainerAtTransferIn { get { return master.AutomaticallyRebuildContainerAtTransferIn; } set { master.AutomaticallyRebuildContainerAtTransferIn = value; } }

        [FwLogicProperty(Id: "hWlZzHfY5tEm")]
        public string ContainerStagingRule { get { return master.ContainerStagingRule; } set { master.ContainerStagingRule = value; } }

        [FwLogicProperty(Id: "eR5ogBSBSPyC")]
        public bool? ExcludeContainedItemsFromAvailability { get { return master.ExcludeContainedItemsFromAvailability; } set { master.ExcludeContainedItemsFromAvailability = value; } }

        [FwLogicProperty(Id: "wrXbUfOmR0tQ")]
        public bool? UseContainerNumber { get { return master.UseContainerNumber; } set { master.UseContainerNumber = value; } }

        [FwLogicProperty(Id: "T2VjUSCN86k1")]
        public string ContainerPackingListBehavior { get { return master.ContainerPackingListBehavior; } set { master.ContainerPackingListBehavior = value; } }

        [FwLogicProperty(Id: "f3VLTXhIygCC", IsReadOnly: true)]
        public bool? InventoryTypeIsWardrobe { get; set; }

        [FwLogicProperty(Id: "hFc5cCuZ4RYyt", IsReadOnly: true)]
        public bool? InventoryTypeIsSets { get; set; }

        [FwLogicProperty(Id: "HyXBs7SSrseH")]
        public string PatternId { get { return master.PatternId; } set { master.PatternId = value; } }

        [FwLogicProperty(Id: "tFLEqLEGPSXs", IsReadOnly: true)]
        public string Pattern { get; set; }

        [FwLogicProperty(Id: "oPCV4hnsur2M")]
        public string PeriodId { get { return master.PeriodId; } set { master.PeriodId = value; } }

        [FwLogicProperty(Id: "tfrGi0yL98LK", IsReadOnly: true)]
        public string Period { get; set; }

        [FwLogicProperty(Id: "sF8yElgh9fy1")]
        public string MaterialId { get { return master.MaterialId; } set { master.MaterialId = value; } }

        [FwLogicProperty(Id: "njMRimQaRi7M", IsReadOnly: true)]
        public string Material { get; set; }

        [FwLogicProperty(Id: "nlB4AsL8o1ZX")]
        public string GenderId { get { return master.GenderId; } set { master.GenderId = value; } }

        [FwLogicProperty(Id: "QVK6SKPOorE0", IsReadOnly: true)]
        public string Gender { get; set; }

        [FwLogicProperty(Id: "8spZ6P8dphVZ")]
        public string LabelId { get { return master.LabelId; } set { master.LabelId = value; } }

        [FwLogicProperty(Id: "6js2iAdyCwWt", IsReadOnly: true)]
        public string Label { get; set; }

        [FwLogicProperty(Id: "6lgCnIgzW3Wf")]
        public string WardrobeSize { get { return master.WardrobeSize; } set { master.WardrobeSize = value; } }

        [FwLogicProperty(Id: "DMA1xax7ipfq")]
        public int? WardrobePieceCount { get { return master.WardrobePieceCount; } set { master.WardrobePieceCount = value; } }

        [FwLogicProperty(Id: "l7n3eutc0G6d")]
        public bool? Dyed { get { return master.Dyed; } set { master.Dyed = value; } }

        [FwLogicProperty(Id: "qsbYbp5SYwqB")]
        public string WardrobeSourceId { get { return master.WardrobeSourceId; } set { master.WardrobeSourceId = value; } }

        [FwLogicProperty(Id: "Eo0qr8iSoje5", IsReadOnly: true)]
        public string WardrobeSource { get; set; }

        [FwLogicProperty(Id: "yPQEEf3Bf6Rg")]
        public string WardrobeCareId { get { return master.WardrobeCareId; } set { master.WardrobeCareId = value; } }

        [FwLogicProperty(Id: "GXwi8L5jaAva", IsReadOnly: true)]
        public string WardrobeCare { get; set; }

        [FwLogicProperty(Id: "ozgoKSfszD2Y")]
        public decimal? CleaningFeeAmount { get { return master.CleaningFeeAmount; } set { master.CleaningFeeAmount = value; } }

        [FwLogicProperty(Id: "kxITozZHTyV5", IsReadOnly: true)]
        public string WardrobeDetailedDescription { get; set; }

        [FwLogicProperty(Id: "Ww8li3RGVPFwg", IsReadOnly: true)]
        public string WebDetailedDescription { get; set; }


        [FwLogicProperty(Id: "hRwL5g8NMuvBz")]
        public bool? OverrideSystemDefaultRevenueAllocationBehavior { get { return master.OverrideSystemDefaultRevenueAllocationBehavior; } set { master.OverrideSystemDefaultRevenueAllocationBehavior = value; } }
        [FwLogicProperty(Id: "U4dFHJLmmq6VR")]
        public bool? AllocateRevenueForAccessories { get { return master.AllocateRevenueForAccessories; } set { master.AllocateRevenueForAccessories = value; } }
        [FwLogicProperty(Id: "fkS1wHPFYZBpc")]
        public string PackageRevenueCalculationFormula { get { return master.PackageRevenueCalculationFormula; } set { master.PackageRevenueCalculationFormula = value; } }


        [FwLogicProperty(Id: "kYIqdwYcTpEXx")]
        public bool? IsHazardousMaterial { get { return master.IsHazardousMaterial; } set { master.IsHazardousMaterial = value; } }


        [FwLogicProperty(Id: "YMbiQMc91vTlT", IsReadOnly: true)]
        public string DescriptionWithAkas { get; set; }


        [FwLogicProperty(Id: "dONXERvrzrsk")]
        public string CostCalculation { get { return master.CostCalculation; } set { master.CostCalculation = value; } }



        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Wx00gon6g1PRz", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "opnMUgKpYMzex", IsReadOnly: true)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "J5c7Y3Wgwxk6U", IsReadOnly: true)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "9oDsRRThCPEmw", IsReadOnly: true)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 



        protected abstract void SetDefaultAvailFor();
        //------------------------------------------------------------------------------------ 
        public void OnBeforeValidateInventory(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                SetDefaultAvailFor();
                if (CostCalculation == null)
                {
                    InventorySettingsLogic defaults = new InventorySettingsLogic();
                    defaults.SetDependencies(AppConfig, UserSession);
                    defaults.InventorySettingsId = RwConstants.CONTROL_ID;
                    if (defaults.LoadAsync<InventorySettingsLogic>().Result)
                    {
                        if (AvailFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT))
                        {
                            CostCalculation = defaults.DefaultRentalQuantityInventoryCostCalculation;
                        }
                        else if (AvailFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_SALE))
                        {
                            CostCalculation = defaults.DefaultSalesQuantityInventoryCostCalculation;
                        }
                        else if (AvailFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_PARTS))
                        {
                            CostCalculation = defaults.DefaultPartsQuantityInventoryCostCalculation;
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.ContainerStagingRule));
                string[] acceptableValues = { RwConstants.CONTAINER_STAGING_ADD_ITEMS_RULE_AUTOMATICALLY_ADD, RwConstants.CONTAINER_STAGING_ADD_ITEMS_RULE_WARN_BUT_ADD, RwConstants.CONTAINER_STAGING_ADD_ITEMS_RULE_DO_NOT_WARN_DO_NOT_ADD, RwConstants.CONTAINER_STAGING_ADD_ITEMS_RULE_DO_NOT_STAGE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.ContainerPackingListBehavior));
                string[] acceptableValues = { RwConstants.CONTAINER_PACKING_LIST_BEHAVIOR_AUTOMATICALLY_PRINT, RwConstants.CONTAINER_PACKING_LIST_BEHAVIOR_PROMPT_TO_PRINT, RwConstants.CONTAINER_PACKING_LIST_BEHAVIOR_DO_NOTHING };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            //Rank is required for Inventory, but we cannot require it on master.rank becasue the table is used for Misc/Labor as well
            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    if (string.IsNullOrEmpty(Rank))
                    {
                        isValid = false;
                        validateMsg = "Rank is required.";
                    }
                }

                if (isValid)
                {
                    PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.Rank));
                    string[] acceptableValues = { RwConstants.INVENTORY_RANK_A, RwConstants.INVENTORY_RANK_B, RwConstants.INVENTORY_RANK_C, RwConstants.INVENTORY_RANK_D, RwConstants.INVENTORY_RANK_E, RwConstants.INVENTORY_RANK_F, RwConstants.INVENTORY_RANK_G };
                    isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
                }
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.PackageRevenueCalculationFormula));
                string[] acceptableValues = { RwConstants.INVENTORY_PACKAGE_REVENUE_CALCULATION_FORMULA_USE_REPLACEMENT_COST, RwConstants.INVENTORY_PACKAGE_REVENUE_CALCULATION_FORMULA_USE_UNIT_VALUE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.CompletePackagePrice));
                string[] acceptableValues = { "", RwConstants.INVENTORY_PACKAGE_PRICE_COMPLETEKIT_PRICE, RwConstants.INVENTORY_PACKAGE_PRICE_ITEM_PRICE, RwConstants.INVENTORY_PACKAGE_PRICE_SPECIAL_ITEM_PRICE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.KitPackagePrice));
                string[] acceptableValues = { "", RwConstants.INVENTORY_PACKAGE_PRICE_COMPLETEKIT_PRICE, RwConstants.INVENTORY_PACKAGE_PRICE_ITEM_PRICE, RwConstants.INVENTORY_PACKAGE_PRICE_SPECIAL_ITEM_PRICE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InventoryLogic).GetProperty(nameof(InventoryLogic.CostCalculation));
                string[] acceptableValues = { RwConstants.COST_CALCULATION_FIFO, RwConstants.COST_CALCULATION_LIFO, RwConstants.COST_CALCULATION_AVERAGE, RwConstants.COST_CALCULATION_UNIT_VALUE };  // unit value will be removed
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg, (saveMode.Equals(TDataRecordSaveMode.smUpdate)));
            }

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    InventoryLogic orig = (InventoryLogic)original;

                    if (CostCalculation != null)
                    {
                        if (!CostCalculation.Equals(orig.CostCalculation))
                        {
                            bool retireExists = AppFunc.DataExistsAsync(AppConfig, "retired", new string[] { "masterid" }, new string[] { InventoryId }).Result;
                            bool transferExists = AppFunc.DataExistsAsync(AppConfig, "ordertranview", new string[] { "masterid", "istransfer" }, new string[] { InventoryId, "T" }).Result;
                            if (isValid)
                            {
                                if (retireExists)
                                {
                                    isValid = false;
                                    validateMsg = "Cannot change the Cost Calculation once Inventory has been sold or retired.";
                                }
                            }
                            if (isValid)
                            {
                                if (transferExists)
                                {
                                    isValid = false;
                                    validateMsg = "Cannot change the Cost Calculation once Inventory has been transferred.";
                                }
                            }
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                AvailableFrom = RwConstants.INVENTORY_AVAILABLE_FROM_WAREHOUSE;  //#jhtodo - need to? add a radio group for this on the form, default to Warehouse on new

                if (string.IsNullOrEmpty(TrackedBy))
                {
                    TrackedBy = RwConstants.INVENTORY_TRACKED_BY_QUANTITY;
                }
            }

            InventoryLogic orig = null;
            string trackedBy = TrackedBy;
            string classification = Classification;
            if (e.Original != null)
            {
                orig = (InventoryLogic)e.Original;
                trackedBy = trackedBy ?? orig.TrackedBy;
                classification = classification ?? orig.Classification;
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (orig != null)
                {
                    PrimaryDimensionUniqueId = orig.PrimaryDimensionUniqueId;
                    SecondaryDimensionUniqueId = orig.SecondaryDimensionUniqueId;
                }
            }


            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if ((orig != null) && (!string.IsNullOrEmpty(TrackedBy)) && (!TrackedBy.Equals(orig.TrackedBy)))
                {
                    if ((!string.IsNullOrEmpty(ConfirmTrackedBy)) && (TrackedBy.Equals(ConfirmTrackedBy)))
                    {
                        _changingTrackedBy = true;
                    }
                    else
                    {
                        _changingTrackedBy = false;
                        TrackedBy = orig.TrackedBy;
                    }
                }
            }


            if (string.IsNullOrEmpty(trackedBy))
            {
                if (classification.Equals(RwConstants.ITEMCLASS_COMPLETE))
                {
                    string primaryInventoryId = AppFunc.GetStringDataAsync(AppConfig, "packageitem", new string[] { "packageid", "primaryflg" }, new string[] { InventoryId, "T" }, new string[] { "masterid" }).Result[0];
                    trackedBy = AppFunc.GetStringDataAsync(AppConfig, "master", "masterid", primaryInventoryId, "trackedby").Result;
                    if (!string.IsNullOrEmpty(trackedBy))
                    {
                        TrackedBy = trackedBy;
                    }
                }
                else if (classification.Equals(RwConstants.ITEMCLASS_CONTAINER))
                {
                    TrackedBy = RwConstants.INVENTORY_TRACKED_BY_BAR_CODE;  // hard-coded for now.  Maybe someday we will support serial
                }
            }
        }
        //------------------------------------------------------------------------------------
        public override void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            base.OnAfterSave(sender, e);

            InventoryLogic orig = null;

            if (e.Original != null)
            {
                orig = (InventoryLogic)e.Original;
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a new Inventory.  PrimaryDimensionUniqueId and SecondaryDimensionUniqueId were not known at time of insert.  Need to re-update the data with the known ID's
                master.PrimaryDimensionId = primaryDimension.UniqueId;
                master.SecondayDimensionId = secondaryDimension.UniqueId;
                int i = master.SaveAsync(null, e.SqlConnection).Result;
            }
            else if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                // this is a modified Inventory.  Check to see if PrimaryDimensionUniqueId and SecondaryDimensionUniqueId were valid before save.  If not, need to re-update the data with the known ID's
                if (string.IsNullOrEmpty(master.PrimaryDimensionId) || string.IsNullOrEmpty(master.SecondayDimensionId))
                {
                    master.PrimaryDimensionId = primaryDimension.UniqueId;
                    master.SecondayDimensionId = secondaryDimension.UniqueId;
                    int i = master.SaveAsync(null, e.SqlConnection).Result;
                }

                if (_changingTrackedBy)
                {
                    ChangeInventoryTrackedByRequest request = new ChangeInventoryTrackedByRequest();
                    request.InventoryId = InventoryId;
                    request.OldTrackedBy = orig.TrackedBy;
                    request.NewTrackedBy = TrackedBy;
                    ChangeInventoryTrackedByResponse response = InventoryFunc.ChangeInventoryTrackedBy(AppConfig, UserSession, request, e.SqlConnection).Result;
                    if (!response.success)
                    {
                        throw new System.Exception(response.msg);
                        //bool b = AppFunc.UpdateDataAsync(AppConfig, "master", new string[] { "masterid" }, new string[] { InventoryId }, new string[] { "trackedby" }, new string[] { orig.TrackedBy }).Result;
                    }
                }
            }

            bool doSaveWardrobeDescription = false;
            bool doSaveWebDescription = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveWardrobeDescription = true;
                doSaveWebDescription = true;
            }
            else
            {
                if (orig != null)
                {
                    doSaveWardrobeDescription = (!orig.WardrobeDetailedDescription.Equals(WardrobeDetailedDescription));
                    doSaveWebDescription = (!orig.WebDetailedDescription.Equals(WebDetailedDescription));
                }
            }
            if (doSaveWardrobeDescription)
            {
                bool saved = master.SaveWardrobeDetailedDescription(WardrobeDetailedDescription, e.SqlConnection).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
            if (doSaveWebDescription)
            {
                bool saved = master.SaveWebDetailedDescription(WebDetailedDescription, e.SqlConnection).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}

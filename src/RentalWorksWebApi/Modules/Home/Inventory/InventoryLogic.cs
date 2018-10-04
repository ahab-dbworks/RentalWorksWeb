using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Home.ItemDimension;
using WebApi.Modules.Home.Master;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using System.Reflection;
using WebLibrary;

namespace WebApi.Modules.Home.Inventory
{
    public abstract class InventoryLogic : MasterLogic
    {

        //------------------------------------------------------------------------------------ 
        protected ItemDimensionRecord primaryDimension = new ItemDimensionRecord();
        protected ItemDimensionRecord secondaryDimension = new ItemDimensionRecord();
        InventoryBrowseLoader inventoryBrowseLoader = new InventoryBrowseLoader();

        public InventoryLogic() : base()
        {
            dataRecords.Add(primaryDimension);
            dataRecords.Add(secondaryDimension);
            browseLoader = inventoryBrowseLoader;
            master.AfterSave += OnAfterSaveMaster;
        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public string AvailableFrom { get { return master.AvailableFrom; } set { master.AvailableFrom = value; } }
        public string TrackedBy { get { return master.TrackedBy; } set { master.TrackedBy = value; } }
        public string Rank { get { return master.Rank; } set { master.Rank = value; } }
        public bool? NoAvailabilityCheck { get { return master.NoAvailabilityCheck; } set { master.NoAvailabilityCheck = value; } }
        public bool? AvailabilityManuallyResolveConflicts { get { return master.AvailabilityManuallyResolveConflicts; } set { master.AvailabilityManuallyResolveConflicts = value; } }
        public bool? SendAvailabilityAlert { get { return master.SendAvailabilityAlert; } set { master.SendAvailabilityAlert = value; } }
        public string PrimaryDimensionUniqueId { get { return primaryDimension.UniqueId; } set { primaryDimension.UniqueId = value; } }
        public string PrimaryDimensionDescription { get { return primaryDimension.Description; } set { primaryDimension.Description = value; } }
        public int? PrimaryDimensionShipWeightLbs { get { return primaryDimension.ShipWeightLbs; } set { primaryDimension.ShipWeightLbs = value; } }
        public int? PrimaryDimensionShipWeightOz { get { return primaryDimension.ShipWeightOz; } set { primaryDimension.ShipWeightOz = value; } }
        public int? PrimaryDimensionWeightInCaseLbs { get { return primaryDimension.WeightInCaseLbs; } set { primaryDimension.WeightInCaseLbs = value; } }
        public int? PrimaryDimensionWeightInCaseOz { get { return primaryDimension.WeightInCaseOz; } set { primaryDimension.WeightInCaseOz = value; } }
        public int? PrimaryDimensionWidthFt { get { return primaryDimension.WidthFt; } set { primaryDimension.WidthFt = value; } }
        public int? PrimaryDimensionWidthIn { get { return primaryDimension.WidthIn; } set { primaryDimension.WidthIn = value; } }
        public int? PrimaryDimensionHeightFt { get { return primaryDimension.HeightFt; } set { primaryDimension.HeightFt = value; } }
        public int? PrimaryDimensionHeightIn { get { return primaryDimension.HeightIn; } set { primaryDimension.HeightIn = value; } }
        public int? PrimaryDimensionLengthFt { get { return primaryDimension.LengthFt; } set { primaryDimension.LengthFt = value; } }
        public int? PrimaryDimensionLengthIn { get { return primaryDimension.LengthIn; } set { primaryDimension.LengthIn = value; } }
        public int? PrimaryDimensionShipWeightKg { get { return primaryDimension.ShipWeightKg; } set { primaryDimension.ShipWeightKg = value; } }
        public int? PrimaryDimensionShipWeightG { get { return primaryDimension.ShipWeightG; } set { primaryDimension.ShipWeightG = value; } }
        public int? PrimaryDimensionWeightInCaseKg { get { return primaryDimension.WeightInCaseKg; } set { primaryDimension.WeightInCaseKg = value; } }
        public int? PrimaryDimensionWeightInCaseG { get { return primaryDimension.WeightInCaseG; } set { primaryDimension.WeightInCaseG = value; } }
        public int? PrimaryDimensionWidthM { get { return primaryDimension.WidthM; } set { primaryDimension.WidthM = value; } }
        public int? PrimaryDimensionWidthCm { get { return primaryDimension.WidthCm; } set { primaryDimension.WidthCm = value; } }
        public int? PrimaryDimensionHeightM { get { return primaryDimension.HeightM; } set { primaryDimension.HeightM = value; } }
        public int? PrimaryDimensionHeightCm { get { return primaryDimension.HeightCm; } set { primaryDimension.HeightCm = value; } }
        public int? PrimaryDimensionLengthM { get { return primaryDimension.LengthM; } set { primaryDimension.LengthM = value; } }
        public int? PrimaryDimensionLengthCm { get { return primaryDimension.LengthCm; } set { primaryDimension.LengthCm = value; } }
        public string SecondaryDimensionUniqueId { get { return secondaryDimension.UniqueId; } set { secondaryDimension.UniqueId = value; } }
        public string SecondaryDimensionDescription { get { return secondaryDimension.Description; } set { secondaryDimension.Description = value; } }
        public int? SecondaryDimensionShipWeightLbs { get { return secondaryDimension.ShipWeightLbs; } set { secondaryDimension.ShipWeightLbs = value; } }
        public int? SecondaryDimensionShipWeightOz { get { return secondaryDimension.ShipWeightOz; } set { secondaryDimension.ShipWeightOz = value; } }
        public int? SecondaryDimensionWeightInCaseLbs { get { return secondaryDimension.WeightInCaseLbs; } set { secondaryDimension.WeightInCaseLbs = value; } }
        public int? SecondaryDimensionWeightInCaseOz { get { return secondaryDimension.WeightInCaseOz; } set { secondaryDimension.WeightInCaseOz = value; } }
        public int? SecondaryDimensionWidthFt { get { return secondaryDimension.WidthFt; } set { secondaryDimension.WidthFt = value; } }
        public int? SecondaryDimensionWidthIn { get { return secondaryDimension.WidthIn; } set { secondaryDimension.WidthIn = value; } }
        public int? SecondaryDimensionHeightFt { get { return secondaryDimension.HeightFt; } set { secondaryDimension.HeightFt = value; } }
        public int? SecondaryDimensionHeightIn { get { return secondaryDimension.HeightIn; } set { secondaryDimension.HeightIn = value; } }
        public int? SecondaryDimensionLengthFt { get { return secondaryDimension.LengthFt; } set { secondaryDimension.LengthFt = value; } }
        public int? SecondaryDimensionLengthIn { get { return secondaryDimension.LengthIn; } set { secondaryDimension.LengthIn = value; } }
        public int? SecondaryDimensionShipWeightKg { get { return secondaryDimension.ShipWeightKg; } set { secondaryDimension.ShipWeightKg = value; } }
        public int? SecondaryDimensionShipWeightG { get { return secondaryDimension.ShipWeightG; } set { secondaryDimension.ShipWeightG = value; } }
        public int? SecondaryDimensionWeightInCaseKg { get { return secondaryDimension.WeightInCaseKg; } set { secondaryDimension.WeightInCaseKg = value; } }
        public int? SecondaryDimensionWeightInCaseG { get { return secondaryDimension.WeightInCaseG; } set { secondaryDimension.WeightInCaseG = value; } }
        public int? SecondaryDimensionWidthM { get { return secondaryDimension.WidthM; } set { secondaryDimension.WidthM = value; } }
        public int? SecondaryDimensionWidthCm { get { return secondaryDimension.WidthCm; } set { secondaryDimension.WidthCm = value; } }
        public int? SecondaryDimensionHeightM { get { return secondaryDimension.HeightM; } set { secondaryDimension.HeightM = value; } }
        public int? SecondaryDimensionHeightCm { get { return secondaryDimension.HeightCm; } set { secondaryDimension.HeightCm = value; } }
        public int? SecondaryDimensionLengthM { get { return secondaryDimension.LengthM; } set { secondaryDimension.LengthM = value; } }
        public int? SecondaryDimensionLengthCm { get { return secondaryDimension.LengthCm; } set { secondaryDimension.LengthCm = value; } }
        public bool? DisplayInSummaryModeWhenRateIsZero { get { return master.DisplayInSummaryModeWhenRateIsZero; } set { master.DisplayInSummaryModeWhenRateIsZero = value; } }
        public bool? QcRequired { get { return master.QcRequired; } set { master.QcRequired = value; } }
        public string QcTime { get { return master.QcTime; } set { master.QcTime = value; } }
        public bool? CopyAttributesAsNote { get { return master.CopyAttributesAsNote; } set { master.CopyAttributesAsNote = value; } }
        public bool? TrackAssetUsage { get { return master.TrackAssetUsage; } set { master.TrackAssetUsage = value; } }
        public bool? TrackLampUsage { get { return master.TrackLampUsage; } set { master.TrackLampUsage = value; } }
        public bool? TrackStrikes { get { return master.TrackStrikes; } set { master.TrackStrikes = value; } }
        public bool? TrackCandles { get { return master.TrackCandles; } set { master.TrackCandles = value; } }
        public int? LampCount { get { return master.LampCount; } set { master.LampCount = value; } }
        public int? MinimumFootCandles { get { return master.MinimumFootCandles; } set { master.MinimumFootCandles = value; } }
        public bool? TrackSoftware { get { return master.TrackSoftware; } set { master.TrackSoftware = value; } }
        public string SoftwareVersion { get { return master.SoftwareVersion; } set { master.SoftwareVersion = value; } }
        public string SoftwareEffectiveDate { get { return master.SoftwareEffectiveDate; } set { master.SoftwareEffectiveDate = value; } }
        public bool? WarehouseSpecificPackage { get { return master.WarehouseSpecificPackage; } set { master.WarehouseSpecificPackage = value; } }
        public string PackagePrice { get { return master.PackagePrice; } set { master.PackagePrice = value; } }
        public bool? SeparatePackageOnQuoteOrder { get { return master.SeparatePackageOnQuoteOrder; } set { master.SeparatePackageOnQuoteOrder = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerScannableInventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerScannableICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerScannableDescription { get; set; }
        public bool? AutomaticallyRebuildContainerAtCheckIn { get { return master.AutomaticallyRebuildContainerAtCheckIn; } set { master.AutomaticallyRebuildContainerAtCheckIn = value; } }
        public bool? AutomaticallyRebuildContainerAtTransferIn { get { return master.AutomaticallyRebuildContainerAtTransferIn; } set { master.AutomaticallyRebuildContainerAtTransferIn = value; } }
        public string ContainerStagingRule { get { return master.ContainerStagingRule; } set { master.ContainerStagingRule = value; } }
        public bool? ExcludeContainedItemsFromAvailability { get { return master.ExcludeContainedItemsFromAvailability; } set { master.ExcludeContainedItemsFromAvailability = value; } }
        public bool? UseContainerNumber { get { return master.UseContainerNumber; } set { master.UseContainerNumber = value; } }
        public string ContainerPackingListBehavior { get { return master.ContainerPackingListBehavior; } set { master.ContainerPackingListBehavior = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? InventoryTypeIsWardrobe { get; set; }
        public string PatternId { get { return master.PatternId; } set { master.PatternId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pattern { get; set; }
        public string PeriodId { get { return master.PeriodId; } set { master.PeriodId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Period { get; set; }
        public string MaterialId { get { return master.MaterialId; } set { master.MaterialId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Material { get; set; }
        public string GenderId { get { return master.GenderId; } set { master.GenderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Gender { get; set; }
        public string LabelId { get { return master.LabelId; } set { master.LabelId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Label { get; set; }
        public string WardrobeSize { get { return master.WardrobeSize; } set { master.WardrobeSize = value; } }
        public int? WardrobePieceCount { get { return master.WardrobePieceCount; } set { master.WardrobePieceCount = value; } }
        public bool? Dyed { get { return master.Dyed; } set { master.Dyed = value; } }
        public string WardrobeSourceId { get { return master.WardrobeSourceId; } set { master.WardrobeSourceId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WardrobeSource { get; set; }
        public string WardrobeCareId { get { return master.WardrobeCareId; } set { master.WardrobeCareId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WardrobeCare { get; set; }
        public decimal? CleaningFeeAmount { get { return master.CleaningFeeAmount; } set { master.CleaningFeeAmount = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WardrobeDetailedDescription { get; set; }
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

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public override void OnAfterSaveMaster(object sender, AfterSaveDataRecordEventArgs e)
        {
            base.OnAfterSaveMaster(sender, e);
            bool saved = master.SaveWardrobeDetailedDescription(WardrobeDetailedDescription).Result;
        }
        //------------------------------------------------------------------------------------
    }
}
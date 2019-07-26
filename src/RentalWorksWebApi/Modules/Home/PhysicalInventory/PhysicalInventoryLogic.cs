using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebLibrary;
using System.Reflection;

namespace WebApi.Modules.Home.PhysicalInventory
{
    [FwLogic(Id: "ksaooIMphDnPC")]
    public class PhysicalInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PhysicalInventoryRecord physicalInventory = new PhysicalInventoryRecord();
        PhysicalInventoryLoader physicalInventoryLoader = new PhysicalInventoryLoader();
        public PhysicalInventoryLogic()
        {
            dataRecords.Add(physicalInventory);
            dataLoader = physicalInventoryLoader;

            ForceSave = true;
            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "juEFWkWknNTw", IsPrimaryKey: true)]
        public string PhysicalInventoryId { get { return physicalInventory.PhysicalInventoryId; } set { physicalInventory.PhysicalInventoryId = value; } }

        [FwLogicProperty(Id: "2YxKqobhlr5fP", DisableDirectModify: true)]
        public string OfficeLocationId { get { return physicalInventory.OfficeLocationId; } set { physicalInventory.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "0rWWUwRr6xc0J", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "ooJLDRMRIv1Vp", IsReadOnly: true)]
        public string OfficeLocationCode { get; set; }

        [FwLogicProperty(Id: "h8F8IImb3HiiE")]
        public string DealId { get { return physicalInventory.DealId; } set { physicalInventory.DealId = value; } }

        //[FwLogicProperty(Id: "QghXj4hxxIZ8t")]
        //public string Location { get { return physicalInventory.Location; } set { physicalInventory.Location = value; } }

        [FwLogicProperty(Id: "tk5Jz53Rkp5r")]
        public string ScheduleDate { get { return physicalInventory.ScheduleDate; } set { physicalInventory.ScheduleDate = value; } }

        [FwLogicProperty(Id: "gg7XH6Gb13i0")]
        public string PreScanDateTime { get { return physicalInventory.PreScanDateTime; } set { physicalInventory.PreScanDateTime = value; } }

        [FwLogicProperty(Id: "d1WfVZeKWEEf")]
        public string InitiateDateTime { get { return physicalInventory.InitiateDateTime; } set { physicalInventory.InitiateDateTime = value; } }

        [FwLogicProperty(Id: "LW2QUasiJNXg", IsRecordTitle: true, DisableDirectModify: true)]
        public string PhysicalInventoryNumber { get { return physicalInventory.PhysicalInventoryNumber; } set { physicalInventory.PhysicalInventoryNumber = value; } }

        [FwLogicProperty(Id: "NTUyUteyTQgK", IsRecordTitle: true)]
        public string Description { get { return physicalInventory.Description; } set { physicalInventory.Description = value; } }

        [FwLogicProperty(Id: "0DGgfyvfreYh")]
        public string CountType { get { return physicalInventory.CountType; } set { physicalInventory.CountType = value; } }

        [FwLogicProperty(Id: "73bcormAqTKZ9", DisableDirectModify: true)]
        public string WarehouseId { get { return physicalInventory.WarehouseId; } set { physicalInventory.WarehouseId = value; } }

        [FwLogicProperty(Id: "xuz09smeYVle1", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "IWZ29BDqBEnr", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "kzlTDZVUS94Zy")]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id: "Sj332iAKKy3G", IsReadOnly: true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id: "YW2LNiYggMS00")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id: "8cOJaru1YaaGz", IsReadOnly: true)]
        public string Category { get; set; }

        [FwLogicProperty(Id: "lVJKJ3JDhKm6u")]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id: "UrBE4XWbij56V", IsReadOnly: true)]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id: "6wZiXgLxSvdt")]
        public string RecType { get { return physicalInventory.RecType; } set { physicalInventory.RecType = value; } }

        [FwLogicProperty(Id: "rOG27fDtylG1s", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id: "Zct9X98I52t4")]
        public string Rank { get { return physicalInventory.Rank; } set { physicalInventory.Rank = value; } }

        [FwLogicProperty(Id: "Hyey2DHxjsAx")]
        public bool? CycleRankA { get { return physicalInventory.CycleRankA; } set { physicalInventory.CycleRankA = value; } }

        [FwLogicProperty(Id: "cQ898yrKxQ7o")]
        public bool? CycleRankB { get { return physicalInventory.CycleRankB; } set { physicalInventory.CycleRankB = value; } }

        [FwLogicProperty(Id: "Ir0YPgUQWA3O")]
        public bool? CycleRankC { get { return physicalInventory.CycleRankC; } set { physicalInventory.CycleRankC = value; } }

        [FwLogicProperty(Id: "MiOumgs4WW2f")]
        public bool? CycleRankD { get { return physicalInventory.CycleRankD; } set { physicalInventory.CycleRankD = value; } }

        [FwLogicProperty(Id: "FFCOx1yaRRIR")]
        public bool? CycleRankE { get { return physicalInventory.CycleRankE; } set { physicalInventory.CycleRankE = value; } }

        [FwLogicProperty(Id: "tyFvhpFKREUq")]
        public bool? CycleRankF { get { return physicalInventory.CycleRankF; } set { physicalInventory.CycleRankF = value; } }

        [FwLogicProperty(Id: "TOEQxxw6RKfj")]
        public bool? CycleRankG { get { return physicalInventory.CycleRankG; } set { physicalInventory.CycleRankG = value; } }

        [FwLogicProperty(Id: "4DFqzVgUXAsut", IsReadOnly: true)]
        public decimal? AFromValue { get; set; }

        [FwLogicProperty(Id: "lH38nweWxGLIm", IsReadOnly: true)]
        public decimal? AToValue { get; set; }

        [FwLogicProperty(Id: "EYW1c3Z9TdNfz", IsReadOnly: true)]
        public decimal? BFromValue { get; set; }

        [FwLogicProperty(Id: "04omWZSfONYB3", IsReadOnly: true)]
        public decimal? BToValue { get; set; }

        [FwLogicProperty(Id: "ThDrqz24Ly5Ha", IsReadOnly: true)]
        public decimal? CFromValue { get; set; }

        [FwLogicProperty(Id: "CLrb7USccEbSo", IsReadOnly: true)]
        public decimal? CToValue { get; set; }

        [FwLogicProperty(Id: "r68DXkBTYE8qG", IsReadOnly: true)]
        public decimal? DFromValue { get; set; }

        [FwLogicProperty(Id: "Z7iRrqHOi0kVQ", IsReadOnly: true)]
        public decimal? DToValue { get; set; }

        [FwLogicProperty(Id: "TqT19qABh9FKq", IsReadOnly: true)]
        public decimal? EFromValue { get; set; }

        [FwLogicProperty(Id: "eFhW97PicabyK", IsReadOnly: true)]
        public decimal? EToValue { get; set; }

        [FwLogicProperty(Id: "nrlILWAs8fP18", IsReadOnly: true)]
        public decimal? FFromValue { get; set; }

        [FwLogicProperty(Id: "zEWSxPxEOCbCv", IsReadOnly: true)]
        public decimal? FToValue { get; set; }

        [FwLogicProperty(Id: "neIz4qk8twbHI", IsReadOnly: true)]
        public decimal? GFromValue { get; set; }

        [FwLogicProperty(Id: "YwhQ1VsfagoGf", IsReadOnly: true)]
        public decimal? GToValue { get; set; }

        [FwLogicProperty(Id: "aXLVye28qjjK")]
        public int? StepPreScan { get { return physicalInventory.StepPreScan; } set { physicalInventory.StepPreScan = value; } }

        [FwLogicProperty(Id: "HLcu7SaeFDRsB", IsReadOnly: true)]
        public bool? AllowStepPreScan { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPreScan == 0) && (StepInitiate == 0)); } }

        [FwLogicProperty(Id: "cGq2QA2cfnmi")]
        public int? StepInitiate { get { return physicalInventory.StepInitiate; } set { physicalInventory.StepInitiate = value; } }

        [FwLogicProperty(Id: "zDO6KDqQsK0X6", IsReadOnly: true)]
        public bool? AllowStepInitiate { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepInitiate == 0)); } }

        [FwLogicProperty(Id: "ktRUSnhiROBH")]
        public int? StepPrintCountSheets { get { return physicalInventory.StepPrintCountSheets; } set { physicalInventory.StepPrintCountSheets = value; } }

        [FwLogicProperty(Id: "rMDZAZbsZfxXj", IsReadOnly: true)]
        public bool? AllowStepPrintCountSheets { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && ((StepPreScan > 0) || (StepInitiate > 0))); } }

        [FwLogicProperty(Id: "WoF5hrbrRySc")]
        public int? StepCount { get { return physicalInventory.StepCount; } set { physicalInventory.StepCount = value; } }

        [FwLogicProperty(Id: "2e6sK7AoshIyH", IsReadOnly: true)]
        public bool? AllowStepCount { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "KmC2DIMlmBgR")]
        public int? StepPrintExceptionReport { get { return physicalInventory.StepPrintExceptionReport; } set { physicalInventory.StepPrintExceptionReport = value; } }

        [FwLogicProperty(Id: "fbB4fzwxvz34z", IsReadOnly: true)]
        public bool? AllowStepPrintExceptionReport { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "zIiu12j2THefs")]
        public int? StepPrintDiscrepancyReport { get { return physicalInventory.StepPrintDiscrepancyReport; } set { physicalInventory.StepPrintDiscrepancyReport = value; } }

        [FwLogicProperty(Id: "jbZZ438Obk2Jy", IsReadOnly: true)]
        public bool? AllowStepPrintDiscrepancyReport { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "SIel2BpMN3MR")]
        public int? StepRecount { get { return physicalInventory.StepRecount; } set { physicalInventory.StepRecount = value; } }

        [FwLogicProperty(Id: "GUiNP5pWeznhR", IsReadOnly: true)]
        public bool? AllowStepRecount { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "oANRKbgDltZr")]
        public int? StepPrintRecountAnalysisReport { get { return physicalInventory.StepPrintRecountAnalysisReport; } set { physicalInventory.StepPrintRecountAnalysisReport = value; } }

        [FwLogicProperty(Id: "K8mDqIiXIiUEz", IsReadOnly: true)]
        public bool? AllowStepPrintRecountAnalysisReport { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "d82DJYvSNPkms")]
        public int? StepPrintReconciliationReport { get { return physicalInventory.StepPrintReconciliationReport; } set { physicalInventory.StepPrintReconciliationReport = value; } }

        [FwLogicProperty(Id: "Dn3Upb4qsvZEn", IsReadOnly: true)]
        public bool? AllowStepPrintReconciliationReport { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "Or5OdD4LxmFY2")]
        public int? StepApproveCounts { get { return physicalInventory.StepApproveCounts; } set { physicalInventory.StepApproveCounts = value; } }

        [FwLogicProperty(Id: "GB9t9zXsITTB2", IsReadOnly: true)]
        public bool? AllowStepApproveCounts { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepCount > 0)); } }

        [FwLogicProperty(Id: "pkqjHbztU2NT")]
        public int? StepClose { get { return physicalInventory.StepClose; } set { physicalInventory.StepClose = value; } }

        [FwLogicProperty(Id: "kwNDs1BTYfkQ3", IsReadOnly: true)]
        public bool? AllowStepClose { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepApproveCounts > 0) && (StepClose == 0) && (StepCloseWithoutAdjustments == 0)); } }

        [FwLogicProperty(Id: "cHdtMkYhr7DqA")]
        public int? StepCloseWithoutAdjustments { get { return physicalInventory.StepCloseWithoutAdjustments; } set { physicalInventory.StepCloseWithoutAdjustments = value; } }

        [FwLogicProperty(Id: "YfMmg9cenmqW9", IsReadOnly: true)]
        public bool? AllowStepCloseWithoutAdjustments { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepApproveCounts > 0) && (StepClose == 0) && (StepCloseWithoutAdjustments == 0)); } }

        [FwLogicProperty(Id: "DxljnDcuVxst")]
        public int? StepPrintResults { get { return physicalInventory.StepPrintResults; } set { physicalInventory.StepPrintResults = value; } }

        [FwLogicProperty(Id: "15lWMkmsIOEFB", IsReadOnly: true)]
        public bool? AllowStepPrintResults { get { return ((!PhysicalInventoryId.Equals(string.Empty)) && (StepPrintCountSheets > 0)); } }

        [FwLogicProperty(Id: "608KDO2tu1oX", DisableDirectModify: true)]
        public string Status { get { return physicalInventory.Status; } set { physicalInventory.Status = value; } }

        [FwLogicProperty(Id: "AZMvRUPqBrnP")]
        public string CycleLastCounted { get { return physicalInventory.CycleLastCounted; } set { physicalInventory.CycleLastCounted = value; } }

        [FwLogicProperty(Id: "DJZYjil20emQ9")]
        public string CycleTrackedBy { get { return physicalInventory.CycleTrackedBy; } set { physicalInventory.CycleTrackedBy = value; } }

        [FwLogicProperty(Id: "i4FLGjoo9YJN2")]
        public string CycleAisle { get { return physicalInventory.CycleAisle; } set { physicalInventory.CycleAisle = value; } }

        [FwLogicProperty(Id: "C3ZDbY7l9MSIV")]
        public string CycleShelf { get { return physicalInventory.CycleShelf; } set { physicalInventory.CycleShelf = value; } }

        [FwLogicProperty(Id: "jINyFo9sv3ztT")]
        public bool? CycleOnlyIncludeInventoryWithNonZeroQuantity { get { return physicalInventory.CycleOnlyIncludeInventoryWithNonZeroQuantity; } set { physicalInventory.CycleOnlyIncludeInventoryWithNonZeroQuantity = value; } }

        [FwLogicProperty(Id: "AMilgeds1O8aV")]
        public bool? ApprovedPurchaseCost { get { return physicalInventory.ApprovedPurchaseCost; } set { physicalInventory.ApprovedPurchaseCost = value; } }

        [FwLogicProperty(Id: "f8rrQTTi2j1Me")]
        public bool? CountInventoryThatIsOut { get { return physicalInventory.CountInventoryThatIsOut; } set { physicalInventory.CountInventoryThatIsOut = value; } }

        [FwLogicProperty(Id: "5nUZedvoqDhM")]
        public bool? FacilitiesInventory { get { return physicalInventory.FacilitiesInventory; } set { physicalInventory.FacilitiesInventory = value; } }

        [FwLogicProperty(Id: "aGmUOxnbGF1l")]
        public bool? PresInitializeAutomaticallyCountInventoryThatIsOut { get { return physicalInventory.PresInitializeAutomaticallyCountInventoryThatIsOut; } set { physicalInventory.PresInitializeAutomaticallyCountInventoryThatIsOut = value; } }

        [FwLogicProperty(Id: "cqhjA7VhJSML")]
        public bool? CycleIncludeOwned { get { return physicalInventory.CycleIncludeOwned; } set { physicalInventory.CycleIncludeOwned = value; } }

        [FwLogicProperty(Id: "uULGPnYtepvi")]
        public bool? CycleIncludeConsigned { get { return physicalInventory.CycleIncludeConsigned; } set { physicalInventory.CycleIncludeConsigned = value; } }

        [FwLogicProperty(Id: "epV6zuqFZEMk")]
        public bool? Selectitemsby_not_used { get { return physicalInventory.Selectitemsby_not_used; } set { physicalInventory.Selectitemsby_not_used = value; } }

        [FwLogicProperty(Id: "mPi2foG9bRmVx")]
        public bool? ExcludeInventoryWithNoAvailabilityCheck { get { return physicalInventory.ExcludeInventoryWithNoAvailabilityCheck; } set { physicalInventory.ExcludeInventoryWithNoAvailabilityCheck = value; } }

        [FwLogicProperty(Id: "193JsswiU37R")]
        public string ApprovedByUserId { get { return physicalInventory.ApprovedByUserId; } set { physicalInventory.ApprovedByUserId = value; } }

        [FwLogicProperty(Id: "GcgW7k8Rd6qLF", IsReadOnly: true)]
        public string ApprovedByUser { get; set; }

        [FwLogicProperty(Id: "fmDW2iNJGcsDj")]
        public string Approvedate { get { return physicalInventory.Approvedate; } set { physicalInventory.Approvedate = value; } }

        [FwLogicProperty(Id: "PmBn1dmi6vnt")]
        public string InputByUserId { get { return physicalInventory.InputByUserId; } set { physicalInventory.InputByUserId = value; } }

        [FwLogicProperty(Id: "yjlABczXRZof")]
        public string InputDate { get { return physicalInventory.InputDate; } set { physicalInventory.InputDate = value; } }

        [FwLogicProperty(Id: "Ek4D56YDdTBo", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "LXFWtJRHZSvmB")]
        public string DateStamp { get { return physicalInventory.DateStamp; } set { physicalInventory.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected bool ValidateNotChangedAFterInitiated(PropertyInfo property, PhysicalInventoryLogic orig, ref string validateMsg)
        {
            bool isValid = true;
            object newValue = property.GetValue(this);
            object oldValue = property.GetValue(orig);
            if ((newValue != null) && (!newValue.Equals(oldValue)))
            {
                isValid = false;
                validateMsg = property.Name + " cannot be changed after Physical Inventory has started Pre-Scan or has been Initiated.";
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            //override this method on a derived class to implement custom validation logic 
            bool isValid = true;
            if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                PhysicalInventoryLogic orig = (PhysicalInventoryLogic)original;
                int? stepPreScan = StepPreScan ?? orig.StepPreScan;
                int? stepInitiate = StepInitiate ?? orig.StepInitiate;

                if ((stepPreScan.GetValueOrDefault(0) > 0) || (stepInitiate.GetValueOrDefault(0) > 0))
                {
                    isValid = ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.RecType)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CountType)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.InventoryTypeId)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleTrackedBy)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleAisle)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleShelf)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleLastCounted)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleIncludeOwned)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleIncludeConsigned)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleOnlyIncludeInventoryWithNonZeroQuantity)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.ExcludeInventoryWithNoAvailabilityCheck)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankA)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankB)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankC)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankD)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankE)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankF)), orig, ref validateMsg) &&
                              ValidateNotChangedAFterInitiated(this.GetType().GetProperty(nameof(PhysicalInventoryLogic.CycleRankG)), orig, ref validateMsg) &&
                              true; // placeholder

                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.PHYSICAL_INVENTORY_STATUS_NEW;
                PhysicalInventoryNumber = AppFunc.GetNextModuleCounterAsync(AppConfig, UserSession, RwConstants.MODULE_PHYSICAL_INVENTORY, OfficeLocationId, e.SqlConnection).Result;
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveInventoryType = false;
            bool doSaveCategoryAndSubCategory = false;
            string inventoryTypeId = string.Empty;
            string categoryId = string.Empty;
            string subCategoryId = string.Empty;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveInventoryType = true;
                doSaveCategoryAndSubCategory = true;
                inventoryTypeId = InventoryTypeId;
                categoryId = CategoryId;
                subCategoryId = SubCategoryId;
            }
            else if (e.Original != null)
            {
                PhysicalInventoryLogic orig = (PhysicalInventoryLogic)e.Original;
                doSaveInventoryType = ((InventoryTypeId != null) && (!orig.InventoryTypeId.Equals(InventoryTypeId)));
                doSaveCategoryAndSubCategory = (((CategoryId != null) && (!orig.CategoryId.Equals(CategoryId))) || ((SubCategoryId != null) && (!orig.SubCategoryId.Equals(SubCategoryId))));

                inventoryTypeId = InventoryTypeId ?? orig.InventoryTypeId;
                categoryId = CategoryId ?? orig.CategoryId;
                subCategoryId = SubCategoryId ?? orig.SubCategoryId;
            }
            if (doSaveInventoryType)
            {
                bool saved = physicalInventory.SaveInventoryType(inventoryTypeId, e.SqlConnection).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
            if (doSaveCategoryAndSubCategory)
            {
                bool saved = physicalInventory.SaveCategoryAndSubCategory(categoryId, subCategoryId, e.SqlConnection).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

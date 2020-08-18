using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.HomeControls.Depreciation
{
    [FwLogic(Id: "wkgKJs3KscaCf")]
    public class DepreciationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DepreciationRecord depreciation = new DepreciationRecord();
        DepreciationLoader depreciationLoader = new DepreciationLoader();
        public DepreciationLogic()
        {
            dataRecords.Add(depreciation);
            dataLoader = depreciationLoader;

            BeforeSave += OnBeforeSave;
            BeforeDelete += OnBeforeDelete;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Wlx7DuPOLHgwz", IsPrimaryKey: true)]
        public int? DepreciationId { get { return depreciation.DepreciationId; } set { depreciation.DepreciationId = value; } }
        [FwLogicProperty(Id: "wLyI8zsZOCc4l")]
        public string DepreciationDate { get { return depreciation.DepreciationDate; } set { depreciation.DepreciationDate = value; } }
        [FwLogicProperty(Id: "WmBh7OZx4d3WI")]
        public string PurchaseId { get { return depreciation.PurchaseId; } set { depreciation.PurchaseId = value; } }
        [FwLogicProperty(Id: "wmorvSA2WKXiu", IsReadOnly: true)]
        public string PurchaseDate { get; set; }
        [FwLogicProperty(Id: "WmzlGKcMDzsaz", IsReadOnly: true)]
        public string ReceiveDate { get; set; }
        [FwLogicProperty(Id: "wnMFRQBPr8mqT", IsReadOnly: true)]
        public string PurchaseWarehouseId { get; set; }
        [FwLogicProperty(Id: "wnmZuyqAejD4v", IsReadOnly: true)]
        public string PurchaseWarehouseCode { get; set; }
        [FwLogicProperty(Id: "WNS6AF16YBuxy", IsReadOnly: true)]
        public string PurchaseWarehouse { get; set; }
        [FwLogicProperty(Id: "WoBRqGF4lUoMY", IsReadOnly: true)]
        public string DebitGlAccountId { get; set; }
        [FwLogicProperty(Id: "wOGokCzHh2BGa", IsReadOnly: true)]
        public string DebitGlAccountNo { get; set; }
        [FwLogicProperty(Id: "WPc0JEE2aea8v", IsReadOnly: true)]
        public string DebitGlAccountDescription { get; set; }
        [FwLogicProperty(Id: "wPF3PHglTY0wt", IsReadOnly: true)]
        public string CreditGlAccountId { get; set; }
        [FwLogicProperty(Id: "WPN5tO5e4wkSb", IsReadOnly: true)]
        public string CreditGlAccountNo { get; set; }
        [FwLogicProperty(Id: "wqADTBWJEo1iS", IsReadOnly: true)]
        public string CreditGlAccountDescription { get; set; }
        [FwLogicProperty(Id: "wqdwxADyO0da1")]
        public int? DepreciationQuantity { get { return depreciation.DepreciationQuantity; } set { depreciation.DepreciationQuantity = value; } }
        [FwLogicProperty(Id: "wqHBSvmEHrepC")]
        public decimal? UnitDepreciationAmount { get { return depreciation.UnitDepreciationAmount; } set { depreciation.UnitDepreciationAmount = value; } }
        [FwLogicProperty(Id: "wqHET17jMKid9", IsReadOnly: true)]
        public decimal? DepreciationExtended { get; set; }
        [FwLogicProperty(Id: "wqN56hzDkmOXw")]
        public bool? IsAdjustment { get { return depreciation.IsAdjustment; } set { depreciation.IsAdjustment = value; } }
        [FwLogicProperty(Id: "WHstD7yI3oqQX")]
        public string DepreciationExtendedColor { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        { 
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                DepreciationLogic orig = (DepreciationLogic)original;
                if (!orig.IsAdjustment.GetValueOrDefault(false))
                {
                    isValid = false;
                    validateMsg = "Cannot edit a " + BusinessLogicModuleName + " record.  Add an Adjustment entry instead.";
                }
            }

            return isValid; 
        } 
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                IsAdjustment = true;
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            if (!IsAdjustment.GetValueOrDefault(false))
            {
                e.PerformDelete = false;
                e.ErrorMessage = "Cannot delete a " + BusinessLogicModuleName + " record.  Add an Adjustment entry instead.";
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

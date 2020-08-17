using WebApi.Logic;
using FwStandard.AppManager;
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
        public bool? Adjustment { get { return depreciation.Adjustment; } set { depreciation.Adjustment = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}

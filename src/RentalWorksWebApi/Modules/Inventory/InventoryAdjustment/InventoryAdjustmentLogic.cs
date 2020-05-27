using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryAdjustment
{
    [FwLogic(Id: "s6S5lvMtCCZeq")]
    public class InventoryAdjustmentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryAdjustmentRecord inventoryAdjustment = new InventoryAdjustmentRecord();
        InventoryAdjustmentLoader inventoryAdjustmentLoader = new InventoryAdjustmentLoader();
        public InventoryAdjustmentLogic()
        {
            dataRecords.Add(inventoryAdjustment);
            dataLoader = inventoryAdjustmentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(IsPrimaryKey: true, Id: "SestxkqyhQbGq")]
        public string InventoryAdjustmentId { get { return inventoryAdjustment.InventoryAdjustmentId; } set { inventoryAdjustment.InventoryAdjustmentId = value; } }
        [FwLogicProperty(Id: "S82TveGQaP2I1")]
        public string AdjustmentDate { get { return inventoryAdjustment.AdjustmentDate; } set { inventoryAdjustment.AdjustmentDate = value; } }
        [FwLogicProperty(Id: "S8QpLvyjHlCmu", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "S8Z3pdC0B0R5a", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "sawvL6RFsVJEP", IsReadOnly: true)]
        public string InventoryAdjustmentReason { get; set; }
        [FwLogicProperty(Id: "Sbp8acVjwZAIo", IsReadOnly: true)]
        public string FieldsAdjusted { get; set; }
        [FwLogicProperty(Id: "sBSYbphSd1Qqr")]
        public decimal? OldQuantity { get { return inventoryAdjustment.OldQuantity; } set { inventoryAdjustment.OldQuantity = value; } }
        [FwLogicProperty(Id: "Sc4vsl35jAUpx")]
        public decimal? NewQuantity { get { return inventoryAdjustment.NewQuantity; } set { inventoryAdjustment.NewQuantity = value; } }
        [FwLogicProperty(Id: "sc7LT6zH23IfG")]
        public decimal? OldUnitCost { get { return inventoryAdjustment.OldUnitCost; } set { inventoryAdjustment.OldUnitCost = value; } }
        [FwLogicProperty(Id: "ScczxAM8qUCyD")]
        public decimal? NewUnitCost { get { return inventoryAdjustment.NewUnitCost; } set { inventoryAdjustment.NewUnitCost = value; } }
        [FwLogicProperty(Id: "sd0Ddr5cQV2R9")]
        public string InventoryAdjustmentReasonId { get { return inventoryAdjustment.InventoryAdjustmentReasonId; } set { inventoryAdjustment.InventoryAdjustmentReasonId = value; } }
        [FwLogicProperty(Id: "SDcOxYD2n7ZXk")]
        public string InventoryId { get { return inventoryAdjustment.InventoryId; } set { inventoryAdjustment.InventoryId = value; } }
        [FwLogicProperty(Id: "sdgOyHVXiRyAD", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "SDspSiw8xetUM", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "SDVcKDK9ZQujQ", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "sEn4kEb4Aln2q")]
        public string WarehouseId { get { return inventoryAdjustment.WarehouseId; } set { inventoryAdjustment.WarehouseId = value; } }
        [FwLogicProperty(Id: "SeThG6DXQ8tMY")]
        public string PhysicalInventoryId { get { return inventoryAdjustment.PhysicalInventoryId; } set { inventoryAdjustment.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "SEyqtxtuH9KKd")]
        public string Reference { get { return inventoryAdjustment.Reference; } set { inventoryAdjustment.Reference = value; } }
        [FwLogicProperty(Id: "seyZ0nZJyxntJ")]
        public string AdjustmentType { get { return inventoryAdjustment.AdjustmentType; } set { inventoryAdjustment.AdjustmentType = value; } }
        [FwLogicProperty(Id: "sF3W1E2g0pZG8")]
        public string AdjustmentTime { get { return inventoryAdjustment.AdjustmentTime; } set { inventoryAdjustment.AdjustmentTime = value; } }
        [FwLogicProperty(Id: "Sf7hoVGiq6AJy")]
        public decimal? AverageAdjustment { get { return inventoryAdjustment.AverageAdjustment; } set { inventoryAdjustment.AverageAdjustment = value; } }
        [FwLogicProperty(Id: "SFDbm8apZaLEp")]
        public decimal? AverageCostAdjustment { get { return inventoryAdjustment.AverageCostAdjustment; } set { inventoryAdjustment.AverageCostAdjustment = value; } }
        [FwLogicProperty(Id: "SgStOxDsPn7tf")]
        public string ModifiedByUserId { get { return inventoryAdjustment.ModifiedByUserId; } set { inventoryAdjustment.ModifiedByUserId = value; } }
        [FwLogicProperty(Id: "SgV4KWGVvojxx")]
        public string Notes { get { return inventoryAdjustment.Notes; } set { inventoryAdjustment.Notes = value; } }
        [FwLogicProperty(Id: "Shk6ydT1e1I1E")]
        public decimal? OnHandAdjustment { get { return inventoryAdjustment.OnHandAdjustment; } set { inventoryAdjustment.OnHandAdjustment = value; } }
        [FwLogicProperty(Id: "shQTvPv7vb4ye")]
        public decimal? UnitCost { get { return inventoryAdjustment.UnitCost; } set { inventoryAdjustment.UnitCost = value; } }
        [FwLogicProperty(Id: "Shwt9xjiT5atV")]
        public string DateStamp { get { return inventoryAdjustment.DateStamp; } set { inventoryAdjustment.DateStamp = value; } }
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

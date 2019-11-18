using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PhysicalInventoryInventory
{
    [FwLogic(Id: "LBOBUD1WoqZ9M")]
    public class PhysicalInventoryInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PhysicalInventoryInventoryRecord physicalInventoryInventory = new PhysicalInventoryInventoryRecord();
        PhysicalInventoryInventoryLoader physicalInventoryInventoryLoader = new PhysicalInventoryInventoryLoader();
        public PhysicalInventoryInventoryLogic()
        {
            dataRecords.Add(physicalInventoryInventory);
            dataLoader = physicalInventoryInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DXUmi9XxkxE0L", IsPrimaryKey: true)]
        public int? Id { get { return physicalInventoryInventory.Id; } set { physicalInventoryInventory.Id = value; } }
        [FwLogicProperty(Id: "q3up3ch5XHBfd")]
        public string PhysicalInventoryId { get { return physicalInventoryInventory.PhysicalInventoryId; } set { physicalInventoryInventory.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "EHz4QaOpzuvje")]
        public string InventoryId { get { return physicalInventoryInventory.InventoryId; } set { physicalInventoryInventory.InventoryId = value; } }
        [FwLogicProperty(Id: "f4f3aUe7Q2cA4", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "45r9c1SU1fUZm", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "GD7it50mpCwpo", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "k9QbrRLJgLcw4", IsReadOnly: true)]
        public string AvailableForDisplay { get; set; }
        [FwLogicProperty(Id: "SLe3KsksdSKjo")]
        public decimal? QuantityOwned { get { return physicalInventoryInventory.QuantityOwned; } set { physicalInventoryInventory.QuantityOwned = value; } }
        [FwLogicProperty(Id: "y9gc0SqHNP7T4")]
        public string TrackedBy { get { return physicalInventoryInventory.TrackedBy; } set { physicalInventoryInventory.TrackedBy = value; } }
        [FwLogicProperty(Id: "K0QaxBzCvh93T")]
        public decimal? UnitCost { get { return physicalInventoryInventory.UnitCost; } set { physicalInventoryInventory.UnitCost = value; } }
        [FwLogicProperty(Id: "iDqhmtxNzT3gU")]
        public bool? IsRecount { get { return physicalInventoryInventory.IsRecount; } set { physicalInventoryInventory.IsRecount = value; } }
        [FwLogicProperty(Id: "1hXmK3Ri9pycK")]
        public string AisleLocation { get { return physicalInventoryInventory.AisleLocation; } set { physicalInventoryInventory.AisleLocation = value; } }
        [FwLogicProperty(Id: "DMa9aL6K4W95y")]
        public string ShelfLocation { get { return physicalInventoryInventory.ShelfLocation; } set { physicalInventoryInventory.ShelfLocation = value; } }
        [FwLogicProperty(Id: "m6idZPk1qoYIs")]
        public int? IsNegativeInventory { get { return physicalInventoryInventory.IsNegativeInventory; } set { physicalInventoryInventory.IsNegativeInventory = value; } }
        [FwLogicProperty(Id: "9pKzKKU4ExIzM")]
        public decimal? QuantityAdded { get { return physicalInventoryInventory.QuantityAdded; } set { physicalInventoryInventory.QuantityAdded = value; } }
        [FwLogicProperty(Id: "LUkfgWU2awPcJ")]
        public decimal? QuantityRetired { get { return physicalInventoryInventory.QuantityRetired; } set { physicalInventoryInventory.QuantityRetired = value; } }
        [FwLogicProperty(Id: "QYA1dsFsvlZnx")]
        public decimal? LastPurchaseUnitCost { get { return physicalInventoryInventory.LastPurchaseUnitCost; } set { physicalInventoryInventory.LastPurchaseUnitCost = value; } }
        [FwLogicProperty(Id: "MLxypJCXwF1Vg")]
        public decimal? NewUnitCost { get { return physicalInventoryInventory.NewUnitCost; } set { physicalInventoryInventory.NewUnitCost = value; } }
        [FwLogicProperty(Id: "tfWamBAj5CYUI")]
        public string CurrentSpaceId { get { return physicalInventoryInventory.CurrentSpaceId; } set { physicalInventoryInventory.CurrentSpaceId = value; } }
        [FwLogicProperty(Id: "sLaAy5Asd1j7x")]
        public string ConsignorId { get { return physicalInventoryInventory.ConsignorId; } set { physicalInventoryInventory.ConsignorId = value; } }
        [FwLogicProperty(Id: "Fydi2hzihbyis")]
        public string DateStamp { get { return physicalInventoryInventory.DateStamp; } set { physicalInventoryInventory.DateStamp = value; } }
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

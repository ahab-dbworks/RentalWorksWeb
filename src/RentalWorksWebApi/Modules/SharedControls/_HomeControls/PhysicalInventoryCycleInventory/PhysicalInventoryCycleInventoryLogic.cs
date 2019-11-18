using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PhysicalInventoryCycleInventory
{
    [FwLogic(Id: "GGqhgCNt7r8c4")]
    public class PhysicalInventoryCycleInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PhysicalInventoryCycleInventoryRecord physicalInventoryCycleInventory = new PhysicalInventoryCycleInventoryRecord();
        PhysicalInventoryCycleInventoryLoader physicalInventoryCycleInventoryLoader = new PhysicalInventoryCycleInventoryLoader();
        public PhysicalInventoryCycleInventoryLogic()
        {
            dataRecords.Add(physicalInventoryCycleInventory);
            dataLoader = physicalInventoryCycleInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "GsAavIGGksgk5", IsPrimaryKey: true)]
        public int? Id { get { return physicalInventoryCycleInventory.Id; } set { physicalInventoryCycleInventory.Id = value; } }
        [FwLogicProperty(Id: "WC7j7UlEL6lwc")]
        public string PhysicalInventoryId { get { return physicalInventoryCycleInventory.PhysicalInventoryId; } set { physicalInventoryCycleInventory.PhysicalInventoryId = value; } }
        [FwLogicProperty(Id: "mKYMakCPl9Kvw")]
        public string InventoryId { get { return physicalInventoryCycleInventory.InventoryId; } set { physicalInventoryCycleInventory.InventoryId = value; } }
        [FwLogicProperty(Id: "vAAH7j8hnSweo")]
        public string ConsignorId { get { return physicalInventoryCycleInventory.ConsignorId; } set { physicalInventoryCycleInventory.ConsignorId = value; } }
        [FwLogicProperty(Id: "Kn6U0A43OuKYK", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "jAKo2li2st3hP", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "8GAKL4DBfieQt", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "Ag5RBctacyMLy", IsReadOnly: true)]
        public string AvailableForDisplay { get; set; }
        [FwLogicProperty(Id: "6cTXCb55YXgd2", IsReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwLogicProperty(Id: "MGY5CEZabnIAr", IsReadOnly: true)]
        public string AisleLocation { get; set; }
        [FwLogicProperty(Id: "MvC6LaKuEGyvN", IsReadOnly: true)]
        public string ShelfLocation { get; set; }
        [FwLogicProperty(Id: "j9eg3hwCmbn0t")]
        public string DateStamp { get { return physicalInventoryCycleInventory.DateStamp; } set { physicalInventoryCycleInventory.DateStamp = value; } }
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

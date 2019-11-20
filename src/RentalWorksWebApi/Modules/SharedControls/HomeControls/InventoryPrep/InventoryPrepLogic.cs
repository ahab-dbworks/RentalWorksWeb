using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryPrep
{
    [FwLogic(Id:"exLFdX8gQEjK")]
    public class InventoryPrepLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryPrepRecord inventoryPrep = new InventoryPrepRecord();
        InventoryPrepLoader inventoryPrepLoader = new InventoryPrepLoader();
        public InventoryPrepLogic()
        {
            dataRecords.Add(inventoryPrep);
            dataLoader = inventoryPrepLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"W30Zf2oeptpU", IsPrimaryKey:true)]
        public string InventoryPrepId { get { return inventoryPrep.InventoryPrepId; } set { inventoryPrep.InventoryPrepId = value; } }

        [FwLogicProperty(Id:"4d1GUdGY9f8Q")]
        public string InventoryId { get { return inventoryPrep.InventoryId; } set { inventoryPrep.InventoryId = value; } }

        [FwLogicProperty(Id:"k0oPNbkWk02B", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"JPEmXiZBXUod", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"3TEg31KjFuUM")]
        public string PrepRateId { get { return inventoryPrep.PrepRateId; } set { inventoryPrep.PrepRateId = value; } }

        [FwLogicProperty(Id:"k0oPNbkWk02B", IsReadOnly:true)]
        public string PrepICode { get; set; }

        [FwLogicProperty(Id:"JPEmXiZBXUod", IsReadOnly:true)]
        public string PrepDescription { get; set; }

        [FwLogicProperty(Id:"fD2R9ziCMKCJ", IsReadOnly:true)]
        public string PrepUnit { get; set; }

        [FwLogicProperty(Id:"fD2R9ziCMKCJ", IsReadOnly:true)]
        public string PrepUnitType { get; set; }

        [FwLogicProperty(Id:"Cmvwg6sYTssW")]
        public bool? IsDefault { get { return inventoryPrep.IsDefault; } set { inventoryPrep.IsDefault = value; } }

        [FwLogicProperty(Id:"5L0ehBEeI636")]
        public decimal? PrepRate { get { return inventoryPrep.PrepRate; } set { inventoryPrep.PrepRate = value; } }

        [FwLogicProperty(Id:"0Sh3MW2gcHyM")]
        public string PrepTime { get { return inventoryPrep.PrepTime; } set { inventoryPrep.PrepTime = value; } }

        [FwLogicProperty(Id:"bovswDznMPq9", IsReadOnly:true)]
        public decimal? PrepExtended { get; set; }

        [FwLogicProperty(Id:"hm6cHlP4xyMO", IsReadOnly:true)]
        public decimal? QtyOrdered { get; set; }

        [FwLogicProperty(Id:"CJnd5CzrU8UB", IsReadOnly:true)]
        public decimal? Price { get; set; }

        [FwLogicProperty(Id:"yaEPQpU4rs27", IsReadOnly:true)]
        public bool? OrderId { get; set; }

        [FwLogicProperty(Id:"FRdxGlymVfKn", IsReadOnly:true)]
        public bool? MasteritemId { get; set; }

        [FwLogicProperty(Id:"UCqBxXUntE4I")]
        public string DateStamp { get { return inventoryPrep.DateStamp; } set { inventoryPrep.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

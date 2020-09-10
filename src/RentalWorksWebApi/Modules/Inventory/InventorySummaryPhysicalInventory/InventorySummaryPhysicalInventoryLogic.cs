using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventorySummaryPhysicalInventory
{
    [FwLogic(Id: "4Gqld2vodAs1T")]
    public class InventorySummaryPhysicalInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventorySummaryPhysicalInventoryLoader inventorySummaryPhysicalInventoryLoader = new InventorySummaryPhysicalInventoryLoader();
        public InventorySummaryPhysicalInventoryLogic()
        {
            dataLoader = inventorySummaryPhysicalInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "4JblrkAlpoFyB", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "4jMZJMuTJyKyn", IsReadOnly: true)]
        public string ItemDescription { get; set; }
        [FwLogicProperty(Id: "4L92nZVDmpuhk", IsReadOnly: true)]
        public string Whcode { get; set; }
        [FwLogicProperty(Id: "4MqQ0Nn65KIST", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "4o4q46IbwBJGY", IsReadOnly: true)]
        public string Physicalno { get; set; }
        [FwLogicProperty(Id: "4PxvWPOfJ2nCR", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "54SKohwafPYjI", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "55uQ2ewbCccSf", IsReadOnly: true)]
        public string Inputdate { get; set; }
        [FwLogicProperty(Id: "56WvuTPSclpF8", IsReadOnly: true)]
        public string Approvedate { get; set; }
        [FwLogicProperty(Id: "5AGbAH7aLcjI0", IsReadOnly: true)]
        public decimal? Total { get; set; }
        [FwLogicProperty(Id: "5BCbSDT9zTzDX", IsReadOnly: true)]
        public string PhysicalId { get; set; }
        [FwLogicProperty(Id: "5fI4u4iFryjGy", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "5kWYlMQ4l21qp", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

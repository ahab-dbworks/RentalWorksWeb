using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    public class InventoryAvailabilityDateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryAvailabilityDateLoader inventoryAvailabilityDateLoader = new InventoryAvailabilityDateLoader();
        public InventoryAvailabilityDateLogic()
        {
            dataLoader = inventoryAvailabilityDateLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string html { get; set; }
        public string backColor { get; set; }
        public string textColor { get; set; }
        public string id { get; set; } = "";
        public string resource { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;

namespace WebApi.Modules.Home.RentalInventory
{
    public class RentalInventoryLogic : InventoryLogic 
    {
        //------------------------------------------------------------------------------------ 
        RentalInventoryLoader inventoryLoader = new RentalInventoryLoader();
        public RentalInventoryLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            AvailFor = "R";
        }
        //------------------------------------------------------------------------------------ 
    }
}
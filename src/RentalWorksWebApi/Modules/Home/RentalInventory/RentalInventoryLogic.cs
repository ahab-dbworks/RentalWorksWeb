using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;

namespace RentalWorksWebApi.Modules.Home.RentalInventory
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
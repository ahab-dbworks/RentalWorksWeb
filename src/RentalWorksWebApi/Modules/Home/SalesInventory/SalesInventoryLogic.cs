using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;

namespace RentalWorksWebApi.Modules.Home.SalesInventory
{
    public class SalesInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        SalesInventoryLoader inventoryLoader = new SalesInventoryLoader();
        public SalesInventoryLogic()
        {
            dataLoader = inventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            AvailFor = "S";
        }
        //------------------------------------------------------------------------------------ 
    }
}
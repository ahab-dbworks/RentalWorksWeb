using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Home.Inventory;

namespace WebApi.Modules.Home.SalesInventory
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
using FwStandard.BusinessLogic;
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
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = "S";
        }
        //------------------------------------------------------------------------------------ 
    }
}
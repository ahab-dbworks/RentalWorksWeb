using FwStandard.BusinessLogic;
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
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            AvailFor = "R";
        }
        //------------------------------------------------------------------------------------ 
    }
}
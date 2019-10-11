using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.Home.Inventory;
using WebLibrary;

namespace WebApi.Modules.Inventory.PartsInventory
{
    public class PartsInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        PartsInventoryLoader inventoryLoader = new PartsInventoryLoader();
        public PartsInventoryLogic()
        {
            dataLoader = inventoryLoader;
            BeforeSave += OnBeforeSave;
            ((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

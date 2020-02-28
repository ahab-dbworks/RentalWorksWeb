using FwStandard.BusinessLogic;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;
using FwStandard.AppManager;

namespace WebApi.Modules.Inventory.SalesInventory
{
    public class SalesInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        SalesInventoryLoader inventoryLoader = new SalesInventoryLoader();
        public SalesInventoryLogic()
        {
            dataLoader = inventoryLoader;
            BeforeSave += OnBeforeSave;
            ((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 

        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "dgkeKcMgo8t5S", IsReadOnly: true)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 


        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

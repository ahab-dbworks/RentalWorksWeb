using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;

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


        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "5Sgx0MOAu7o6i", IsReadOnly: true)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "h4ZysSKAePyj9", IsReadOnly: true)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "EUzZ0o6AGeaEY", IsReadOnly: true)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 



        //------------------------------------------------------------------------------------ 
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

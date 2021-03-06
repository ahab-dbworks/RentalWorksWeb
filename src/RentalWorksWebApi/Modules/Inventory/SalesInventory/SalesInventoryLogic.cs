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
            //BeforeSave += OnBeforeSave;
            //((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
            ((InventoryLoader)dataLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 

        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "dgkeKcMgo8t5S", IsReadOnly: true)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "cCsueAb8weXt1", IsReadOnly: true)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "x32dthjbtSdf5", IsReadOnly: true)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 




        //------------------------------------------------------------------------------------ 
        protected override void SetDefaultAvailFor()
        {
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

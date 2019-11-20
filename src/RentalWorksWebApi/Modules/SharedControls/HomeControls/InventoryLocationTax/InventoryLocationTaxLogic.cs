using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.HomeControls.MasterLocation;

namespace WebApi.Modules.HomeControls.InventoryLocationTax
{
    public class InventoryLocationTaxLogic : MasterLocationLogic
    {
        //------------------------------------------------------------------------------------ 

        InventoryLocationTaxLoader inventoryLocationTaxLoader = new InventoryLocationTaxLoader();
        public InventoryLocationTaxLogic() : base()
        {
            dataLoader = inventoryLocationTaxLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"IMmoLO9RDAvj")]
        public string InventoryId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

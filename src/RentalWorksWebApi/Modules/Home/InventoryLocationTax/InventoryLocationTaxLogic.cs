using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.MasterLocation;

namespace WebApi.Modules.Home.InventoryLocationTax
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
        public string InventoryId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.MasterLocation;

namespace RentalWorksWebApi.Modules.Home.InventoryLocationTax
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
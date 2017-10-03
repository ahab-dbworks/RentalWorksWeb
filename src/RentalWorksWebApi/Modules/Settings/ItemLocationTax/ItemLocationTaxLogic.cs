using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.MasterLocation;

namespace RentalWorksWebApi.Modules.Settings.ItemLocationTax
{
    public class ItemLocationTaxLogic : MasterLocationLogic
    {
        //------------------------------------------------------------------------------------ 

        ItemLocationTaxLoader itemLocationTaxLoader = new ItemLocationTaxLoader();
        public ItemLocationTaxLogic() : base()
        {
            dataLoader = itemLocationTaxLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ItemId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
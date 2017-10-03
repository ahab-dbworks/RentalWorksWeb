using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.MasterLocation;

namespace RentalWorksWebApi.Modules.Settings.RateLocationTax
{
    public class RateLocationTaxLogic : MasterLocationLogic
    {
        //------------------------------------------------------------------------------------ 

        RateLocationTaxLoader rateLocationTaxLoader = new RateLocationTaxLoader();
        public RateLocationTaxLogic() : base()
        {
            dataLoader = rateLocationTaxLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string RateId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
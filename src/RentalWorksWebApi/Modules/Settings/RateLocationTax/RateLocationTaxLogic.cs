using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.MasterLocation;

namespace WebApi.Modules.Settings.RateLocationTax
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
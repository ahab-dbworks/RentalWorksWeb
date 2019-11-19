using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.HomeControls.MasterLocation;

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
        [FwLogicProperty(Id:"QEepHRTLcmi8")]
        public string RateId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

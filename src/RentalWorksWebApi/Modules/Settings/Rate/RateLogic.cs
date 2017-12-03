using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;

namespace RentalWorksWebApi.Modules.Settings.Rate
{
    public abstract class RateLogic : MasterLogic 
    {
        //------------------------------------------------------------------------------------ 
        //[FwBusinessLogicField(isPrimaryKey: true)]
        //public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        public string RateType { get { return master.RateType; } set { master.RateType = value; } }
        public bool? IncludeAsProfitAndLossCategory { get { return master.IncludeAsProfitAndLossCategory; } set { master.IncludeAsProfitAndLossCategory = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
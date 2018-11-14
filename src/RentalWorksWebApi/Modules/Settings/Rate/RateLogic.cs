using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebLibrary;

namespace WebApi.Modules.Settings.Rate
{
    public abstract class RateLogic : MasterLogic 
    {
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id:"cBzTRIkblHSE", IsPrimaryKey:true)]
        //public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }

        [FwLogicProperty(Id:"I0eKzxiu2rrI")]
        public string RateType { get { return master.RateType; } set { master.RateType = value; } }

        [FwLogicProperty(Id:"QmBtKwVSnVsv")]
        public bool? IncludeAsProfitAndLossCategory { get { return master.IncludeAsProfitAndLossCategory; } set { master.IncludeAsProfitAndLossCategory = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

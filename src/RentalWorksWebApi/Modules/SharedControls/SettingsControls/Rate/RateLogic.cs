using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi;

namespace WebApi.Modules.Settings.Rate
{
    public abstract class RateLogic : MasterLogic 
    {
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id:"cBzTRIkblHSE", IsPrimaryKey:true)]
        //public string RateId { get { return master.MasterId; } set { master.MasterId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"I0eKzxiu2rrI")]
        public string RateType { get { return master.RateType; } set { master.RateType = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"QmBtKwVSnVsv")]
        public bool? IncludeAsProfitAndLossCategory { get { return master.IncludeAsProfitAndLossCategory; } set { master.IncludeAsProfitAndLossCategory = value; } }
        //------------------------------------------------------------------------------------ 



        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "UNzs46dlv6Cp", IsReadOnly: true)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "e5K61KIbG0gM", IsReadOnly: true)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Q0QO7Org8S4p", IsReadOnly: true)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Ob3c7a2aDsfZ", IsReadOnly: true)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "RRCfhjN9mpHe", IsReadOnly: true)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "tirMfEYeSFmx", IsReadOnly: true)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "gGlUdsCrYR7s", IsReadOnly: true)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "S66xon2aQYh5", IsReadOnly: true)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "vQTaNoqRKB4t", IsReadOnly: true)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "XoJv9FLFq1ko", IsReadOnly: true)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
    }
}

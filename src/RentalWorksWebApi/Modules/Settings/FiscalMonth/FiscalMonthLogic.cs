using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.FiscalMonth
{
    [FwLogic(Id:"8S4JOjioC4aq")]
    public class FiscalMonthLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        FiscalMonthRecord fiscalMonth = new FiscalMonthRecord();
        FiscalMonthLoader fiscalMonthLoader = new FiscalMonthLoader();
        public FiscalMonthLogic()
        {
            dataRecords.Add(fiscalMonth);
            dataLoader = fiscalMonthLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"p84Ch9uusnTy", IsPrimaryKey:true)]
        public string FiscalMonthId { get { return fiscalMonth.FiscalMonthId; } set { fiscalMonth.FiscalMonthId = value; } }

        [FwLogicProperty(Id:"RAlxmqUdLd")]
        public string FiscalYearId { get { return fiscalMonth.FiscalYearId; } set { fiscalMonth.FiscalYearId = value; } }

        [FwLogicProperty(Id:"0fibf6gppi7a", IsReadOnly:true)]
        public string Year { get; set; }

        [FwLogicProperty(Id:"W6szlBe7fV")]
        public int? Month { get { return fiscalMonth.Month; } set { fiscalMonth.Month = value; } }

        [FwLogicProperty(Id:"IqEzPYGLcxBz", IsReadOnly:true)]
        public string MonthDisplay { get; set; }

        [FwLogicProperty(Id:"E4RhVaKMiI")]
        public string FromDate { get { return fiscalMonth.FromDate; } set { fiscalMonth.FromDate = value; } }

        [FwLogicProperty(Id:"jpnbklM6py")]
        public string ToDate { get { return fiscalMonth.ToDate; } set { fiscalMonth.ToDate = value; } }

        [FwLogicProperty(Id:"IqEzPYGLcxBz", IsRecordTitle:true, IsReadOnly:true)]
        public string MonthYear { get; set; }

        [FwLogicProperty(Id:"UhkSgHtuq3H")]
        public int? WorkDays { get { return fiscalMonth.WorkDays; } set { fiscalMonth.WorkDays = value; } }

        [FwLogicProperty(Id:"11peXh0Jd4c")]
        public string FiscalOrder { get { return fiscalMonth.FiscalOrder; } set { fiscalMonth.FiscalOrder = value; } }

        [FwLogicProperty(Id:"ko8IP0eZrsf")]
        public bool? Closed { get { return fiscalMonth.Closed; } set { fiscalMonth.Closed = value; } }

        [FwLogicProperty(Id:"iPuWnqss4Sr")]
        public string DateStamp { get { return fiscalMonth.DateStamp; } set { fiscalMonth.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

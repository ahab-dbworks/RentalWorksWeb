using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.FiscalMonth
{
    public class FiscalMonthLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string FiscalMonthId { get { return fiscalMonth.FiscalMonthId; } set { fiscalMonth.FiscalMonthId = value; } }
        public string FiscalYearId { get { return fiscalMonth.FiscalYearId; } set { fiscalMonth.FiscalYearId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Year { get; set; }
        public int Month { get { return fiscalMonth.Month; } set { fiscalMonth.Month = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MonthDisplay { get; set; }
        public string FromDate { get { return fiscalMonth.FromDate; } set { fiscalMonth.FromDate = value; } }
        public string ToDate { get { return fiscalMonth.ToDate; } set { fiscalMonth.ToDate = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string MonthYear { get; set; }
        public int WorkDays { get { return fiscalMonth.WorkDays; } set { fiscalMonth.WorkDays = value; } }
        public string FiscalOrder { get { return fiscalMonth.FiscalOrder; } set { fiscalMonth.FiscalOrder = value; } }
        public bool Closed { get { return fiscalMonth.Closed; } set { fiscalMonth.Closed = value; } }
        public string DateStamp { get { return fiscalMonth.DateStamp; } set { fiscalMonth.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
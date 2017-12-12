using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.FiscalYear
{
    public class FiscalYearLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        FiscalYearRecord fiscalYear = new FiscalYearRecord();
        public FiscalYearLogic()
        {
            dataRecords.Add(fiscalYear);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string FiscalYearId { get { return fiscalYear.FiscalYearId; } set { fiscalYear.FiscalYearId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Year { get { return fiscalYear.Year; } set { fiscalYear.Year = value; } }
        public string DateStamp { get { return fiscalYear.DateStamp; } set { fiscalYear.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
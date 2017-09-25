using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.FiscalYear
{
    public class FiscalYearLogic : RwBusinessLogic
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
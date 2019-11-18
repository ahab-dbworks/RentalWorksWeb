using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.FiscalYear
{
    [FwLogic(Id:"plXY1qJr6w0b")]
    public class FiscalYearLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        FiscalYearRecord fiscalYear = new FiscalYearRecord();
        public FiscalYearLogic()
        {
            dataRecords.Add(fiscalYear);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"Isxe2FbWa0DF", IsPrimaryKey:true)]
        public string FiscalYearId { get { return fiscalYear.FiscalYearId; } set { fiscalYear.FiscalYearId = value; } }

        [FwLogicProperty(Id:"XL5yT3xMaaPf", IsRecordTitle:true)]
        public string Year { get { return fiscalYear.Year; } set { fiscalYear.Year = value; } }

        [FwLogicProperty(Id:"6eZUjCpmbZP3")]
        public string DateStamp { get { return fiscalYear.DateStamp; } set { fiscalYear.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

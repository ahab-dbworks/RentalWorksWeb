using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.CreditsOnAccountReport
{
    public class CreditsOnAccountReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CreditsOnAccountReportLoader creditsOnAccountLoader = new CreditsOnAccountReportLoader();
        public CreditsOnAccountReportLogic()
        {
            dataLoader = creditsOnAccountLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalDepletingDeposit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalCredit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalOverpayment { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalDeposit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalApplied { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalRefunded { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

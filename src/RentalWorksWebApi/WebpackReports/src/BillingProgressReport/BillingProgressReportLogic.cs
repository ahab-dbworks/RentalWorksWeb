using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.BillingProgressReport
{
    public class BillingProgressReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BillingProgressReportLoader billingProgressReportLoader = new BillingProgressReportLoader();
        public BillingProgressReportLogic()
        {
            dataLoader = billingProgressReportLoader;
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
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Status { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? OrderTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Billed { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Remaining { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? BilledPercent { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

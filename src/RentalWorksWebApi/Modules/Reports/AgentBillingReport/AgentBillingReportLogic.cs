using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.AgentBillingReport
{
    public class AgentBillingReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AgentBillingReportLoader agentBillingReportLoader = new AgentBillingReportLoader();
        public AgentBillingReportLogic()
        {
            dataLoader = agentBillingReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Status { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingStartDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingEndDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingNote { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycleType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WorkAuthorizationNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GroupNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastBatchNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsNoCharge { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MeterTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscellaneousTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AssetTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsNonBillable { get; set; }
    }
}

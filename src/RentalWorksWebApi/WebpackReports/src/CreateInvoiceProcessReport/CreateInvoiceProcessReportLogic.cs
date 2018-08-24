using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.CreateInvoiceProcessReport
{
    public class CreateInvoiceProcessReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CreateInvoiceProcessReportLoader createInvoiceProcessReportLoader = new CreateInvoiceProcessReportLoader();
        public CreateInvoiceProcessReportLogic()
        {
            dataLoader = createInvoiceProcessReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? BatchNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BatchDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingNote { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsException { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ExceptionMessage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingStart { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingEnd { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycle { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsNoCharge { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PriorInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

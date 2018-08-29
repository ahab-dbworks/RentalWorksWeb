using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.ArAgingReport
{
    public class ArAgingReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ArAgingReportLoader arAgingReportLoader = new ArAgingReportLoader();
        public ArAgingReportLogic()
        {
            dataLoader = arAgingReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReceiptId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealCsrId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealCsr { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderAgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderAgent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? InvoiceAgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? InvoiceAgent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total0030 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total3160 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total6190 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total91x { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PendingFinanceCharge { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DivisionId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

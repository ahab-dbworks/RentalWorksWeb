using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.CustomerRevenueByTypeReport
{
    public class CustomerRevenueByTypeReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomerRevenueByTypeReportLoader customerRevenueByTypeReportLoader = new CustomerRevenueByTypeReportLoader();
        public CustomerRevenueByTypeReportLogic()
        {
            dataLoader = customerRevenueByTypeReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingStart { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingEnd { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Rental { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Sales { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Facilities { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Labor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Miscelleaneous { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AssetSale { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Parts { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Tax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Billing.InvoiceBatch
{
    [FwLogic(Id: "0O44iwDR9eny")]
    public class InvoiceBatchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceBatchLoader invoiceBatchLoader = new InvoiceBatchLoader();
        public InvoiceBatchLogic()
        {
            dataLoader = invoiceBatchLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0OpICc7tFPGj", IsReadOnly: true)]
        public string RowType { get; set; }
        [FwLogicProperty(Id: "0pAd2vfiYMrO", IsReadOnly: true)]
        public string BatchId { get; set; }
        [FwLogicProperty(Id: "0pDkJrQPIgnD", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "0QdcB6PsZKbl", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "0QZQNx8RsKqq", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "0sAVC3PU1y3s", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "0SNtcYBJd3iN", IsReadOnly: true)]
        public string DealNumber { get; set; }
        [FwLogicProperty(Id: "0T70ZEjpRglm", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "0tq0aTRo9Kyj", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "0U0oPd1Glurp", IsReadOnly: true)]
        public string BatchNumber { get; set; }
        [FwLogicProperty(Id: "0U0y41itLyRq", IsReadOnly: true)]
        public string BatchDate { get; set; }
        [FwLogicProperty(Id: "0UxYnFyDIuRb", IsReadOnly: true)]
        public string InvoiceDescription { get; set; }
        [FwLogicProperty(Id: "0Vpf2hcgY9RH", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "0YM2CBeKFLBu", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "0zHwRWTvmTp9", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "0zwyuAztMtXE", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "10QoDbvtGoaW", IsReadOnly: true)]
        public string Departemnt { get; set; }
        [FwLogicProperty(Id: "11STkNJkJWhl", IsReadOnly: true)]
        public string DepartmentCode { get; set; }
        [FwLogicProperty(Id: "1214vf3gMl2r", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "129dGV8436Q ", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "13FoX6tdPoKQ", IsReadOnly: true)]
        public string BillingStartDate { get; set; }
        [FwLogicProperty(Id: "13TzHRSjRVZS", IsReadOnly: true)]
        public string BillingEndDate { get; set; }
        [FwLogicProperty(Id: "13yreD0dL7n ", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "15f38G4dFquu", IsReadOnly: true)]
        public bool? IsNoCharge { get; set; }
        [FwLogicProperty(Id: "15vIkFUqMAJj", IsReadOnly: true)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

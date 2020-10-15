using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PurchaseOrderItemVendorInvoiceStatus
{
    [FwLogic(Id: "1vU56flyJJvk")]
    public class PurchaseOrderItemVendorInvoiceStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseOrderItemVendorInvoiceStatusLoader purchaseOrderItemVendorInvoiceStatusLoader = new PurchaseOrderItemVendorInvoiceStatusLoader();
        public PurchaseOrderItemVendorInvoiceStatusLogic()
        {
            dataLoader = purchaseOrderItemVendorInvoiceStatusLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1vZPuR29hs2l", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "1wHqs5vRJzb1", IsReadOnly: true)]
        public string PurchaseOrderItemId { get; set; }
        [FwLogicProperty(Id: "1wQaMsklDF5I", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "1wxFQztX1hmx", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "1xjXkwW60m7Z", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "1XliKzeGJySs", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "1XU457aAX0Sj", IsReadOnly: true)]
        public string SubOrderId { get; set; }
        [FwLogicProperty(Id: "1yjExEq0CjTM", IsReadOnly: true)]
        public string SubOrderItemId { get; set; }
        [FwLogicProperty(Id: "1ZgchcKqnKev", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "20nEQQ84eWYV", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "227pFN8mJXD7", IsReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwLogicProperty(Id: "25VMQvPFNSOi", IsReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }
        [FwLogicProperty(Id: "280j87UKIfO9", IsReadOnly: true)]
        public decimal? NetQuantityOrdered { get; set; }
        [FwLogicProperty(Id: "29jdAVUInW3N", IsReadOnly: true)]
        public decimal? QuantityReceived { get; set; }
        [FwLogicProperty(Id: "2A8h5zaP401a", IsReadOnly: true)]
        public decimal? QuantityRemaining { get; set; }
        [FwLogicProperty(Id: "2Aqu9kSNVtYd", IsReadOnly: true)]
        public decimal? QuantityApproved { get; set; }
        [FwLogicProperty(Id: "2bkHvqLW88A5", IsReadOnly: true)]
        public decimal? QuantityReturned { get; set; }
        [FwLogicProperty(Id: "2CAeXVGCGhIb", IsReadOnly: true)]
        public int? QuantityReturnable { get; set; }
        [FwLogicProperty(Id: "2CPBMatRWsvT", IsReadOnly: true)]
        public decimal? QuantityCanApprove { get; set; }
        [FwLogicProperty(Id: "2cpRTMM7QlzN", IsReadOnly: true)]
        public decimal? Rate { get; set; }
        [FwLogicProperty(Id: "2CXAUygcGc77", IsReadOnly: true)]
        public decimal? DiscountPercent { get; set; }
        [FwLogicProperty(Id: "2dtsetVO5Sn0", IsReadOnly: true)]
        public decimal? DaysPerWeek { get; set; }
        [FwLogicProperty(Id: "2EiVBghewmEQ", IsReadOnly: true)]
        public decimal? TotalCost { get; set; }
        [FwLogicProperty(Id: "2EkcfX5EHNfP", IsReadOnly: true)]
        public decimal? ReceivedCost { get; set; }
        [FwLogicProperty(Id: "2Eq9U0scbaEZ", IsReadOnly: true)]
        public decimal? TotalInvoiced { get; set; }
        [FwLogicProperty(Id: "2ERfsURsJOm1", IsReadOnly: true)]
        public decimal? CostRemaining { get; set; }
        [FwLogicProperty(Id: "2EZMnk2rI85Q", IsReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwLogicProperty(Id: "2f6wLaUgSeJw", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "2fpTU5G3ixqB", IsReadOnly: true)]
        public string ItemClass { get; set; }
        [FwLogicProperty(Id: "2gqXJKl3iLON", IsReadOnly: true)]
        public bool? OptionColor { get; set; }
        [FwLogicProperty(Id: "2H5u1X8IrA3 ", IsReadOnly: true)]
        public bool? Taxable { get; set; }
        [FwLogicProperty(Id: "2h6HMP5g84fE", IsReadOnly: true)]
        public string AisleLocation { get; set; }
        [FwLogicProperty(Id: "2HpAeeSvENP9", IsReadOnly: true)]
        public string ShelfLocation { get; set; }
        [FwLogicProperty(Id: "2jeUaaEsu3Ey", IsReadOnly: true)]
        public string ManufacturerPartNumber { get; set; }
        [FwLogicProperty(Id: "2jofj3jsx3Cz", IsReadOnly: true)]
        public string ChargeType { get; set; }
        [FwLogicProperty(Id: "2JT57jM7HG36", IsReadOnly: true)]
        public string BillingStartDate { get; set; }
        [FwLogicProperty(Id: "2keOFtvbyJpW", IsReadOnly: true)]
        public string BillingEndDate { get; set; }
        [FwLogicProperty(Id: "bzv8mVumiTf2", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "KPgTxzbDwJa7", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "aKTbY7Lzfdjb", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "uiryc29YJWct", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "3860PRlGWuc7", IsReadOnly: true)]
        public decimal? Tax { get; set; }
        [FwLogicProperty(Id: "vEsxJkuB9mZh", IsReadOnly: true)]
        public decimal? TaxReceived { get; set; }
        [FwLogicProperty(Id: "L4IOeoY6g2E6", IsReadOnly: true)]
        public decimal? TaxRemaining { get; set; }
        [FwLogicProperty(Id: "YJcoEPEFZ8MQ", IsReadOnly: true)]
        public decimal? TotalInvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

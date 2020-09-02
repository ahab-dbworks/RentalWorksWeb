using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.InvoiceRevenue
{
    [FwLogic(Id: "TMlD2dhrkO20")]
    public class InvoiceRevenueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceRevenueLoader invoiceRevenueLoader = new InvoiceRevenueLoader();
        public InvoiceRevenueLogic()
        {
            dataLoader = invoiceRevenueLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "tmYvnTK45oS", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "L1bTZXyMrWDQ", IsReadOnly: true)]
        public string RevenueDate { get; set; }
        [FwLogicProperty(Id: "fKV27mQMqDLH", IsReadOnly: true)]
        public string ItemNumber { get; set; }
        [FwLogicProperty(Id: "sRYefjUsOPJ0c", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "Esp40KwIk6WZD", IsReadOnly: true)]
        public string BarCode { get; set; }
        [FwLogicProperty(Id: "haBH4BKPrW7", IsReadOnly: true)]
        public string MfgSerial { get; set; }
        [FwLogicProperty(Id: "dSZlPUWMSuco", IsReadOnly: true)]
        public string TrackedByCode { get; set; }
        [FwLogicProperty(Id: "s4gk9THCBJcG", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwLogicProperty(Id: "TlYVASCTmXeI", IsReadOnly: true)]
        public decimal? InvoiceRevenue { get; set; }
        [FwLogicProperty(Id: "W7ZI03euGi25Z", IsReadOnly: true)]
        public decimal? Cost { get; set; }
        [FwLogicProperty(Id: "2X7nkTK3kYLBc", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "G7YrMwTrG98z3", IsReadOnly: true)]
        public string ItemId { get; set; }
        [FwLogicProperty(Id: "ihzeU00uVzAa", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "5YlCJT4Wo6UJ", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "BJidB3sHiP0Ta", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "aAbieiUMowsN", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "cFJBLkapb2nSh", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "y4kR8bUGsY8ax", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "k9CG0CSek5OxO", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "kDFNQieEiE3Xf", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

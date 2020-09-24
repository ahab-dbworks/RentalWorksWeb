using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Billing.PaymentVendorInvoice
{
    [FwLogic(Id: "zrTtuUPLGpbv5")]
    public class PaymentVendorInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PaymentVendorInvoiceLoader paymentVendorInvoiceLoader = new PaymentVendorInvoiceLoader();
        public PaymentVendorInvoiceLogic()
        {
            dataLoader = paymentVendorInvoiceLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "zRZQDmEUF9FPF", IsPrimaryKey: true, IsReadOnly: true)]
        public string VendorInvoicePaymentId { get; set; } = "";
        [FwLogicProperty(Id: "ZSbEiHeVWNXwK", IsReadOnly: true)]
        public string VendorInvoiceId { get; set; }
        [FwLogicProperty(Id: "ZSL85hY7QtFCu", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "zsUSOdof6ogt6", IsReadOnly: true)]
        public string VendorInvoiceNumber { get; set; }
        [FwLogicProperty(Id: "ZswWHZMPJyNeD", IsReadOnly: true)]
        public string VendorInvoiceDate { get; set; }
        [FwLogicProperty(Id: "Zt8unmUUs3QFH", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "zToHoJU26iBE9", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "ZTR5pfFSzN6SP", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "ZtyQVLEHUTe2l", IsReadOnly: true)]
        public string PurchaseOrderDescription { get; set; }
        [FwLogicProperty(Id: "ZUcs9SpOiuXfz", IsReadOnly: true)]
        public string RequisitionNumber { get; set; }
        [FwLogicProperty(Id: "Zuq9kLJP99xZZ", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "zUtSBNFYiKfJT", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "ZUX8DXB03KKTb", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "zVAov0R4VpWPf", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "zVbQefGAr92I7", IsReadOnly: true)]
        public decimal? Total { get; set; }
        [FwLogicProperty(Id: "zvzdyI51ggMMj", IsReadOnly: true)]
        public string InvoiceType { get; set; }
        [FwLogicProperty(Id: "zw3mCgz3U41o3", IsReadOnly: true)]
        public decimal? Applied { get; set; }
        [FwLogicProperty(Id: "zw5gkBYr0YBDz", IsReadOnly: true)]
        public decimal? Amount { get; set; }
        [FwLogicProperty(Id: "zw6DDCeYNTJ3y", IsReadOnly: true)]
        public decimal? Due { get; set; }
        [FwLogicProperty(Id: "zW7SqkG7tLsaH", IsReadOnly: true)]
        public string ExternalDocumentNumber { get; set; }
        [FwLogicProperty(Id: "zwbgIfar3vVxn", IsReadOnly: true)]
        public string ExternalDocumentDate { get; set; }
        [FwLogicProperty(Id: "zWKsZx8x5sy6O", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "zXm0YMDTyE0aS", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "ZY400NMEWZ9w9", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

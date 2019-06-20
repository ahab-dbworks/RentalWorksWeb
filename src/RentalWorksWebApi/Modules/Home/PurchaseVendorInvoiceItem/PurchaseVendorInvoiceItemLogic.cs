using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.PurchaseVendorInvoiceItem
{
    [FwLogic(Id: "YZgwh5hpsw6B")]
    public class PurchaseVendorInvoiceItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseVendorInvoiceItemRecord purchaseVendorInvoiceItem = new PurchaseVendorInvoiceItemRecord();
        PurchaseVendorInvoiceItemLoader purchaseVendorInvoiceItemLoader = new PurchaseVendorInvoiceItemLoader();
        public PurchaseVendorInvoiceItemLogic()
        {
            dataRecords.Add(purchaseVendorInvoiceItem);
            dataLoader = purchaseVendorInvoiceItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "uJiaNge07QpN")]
        public string PurchaseId { get { return purchaseVendorInvoiceItem.PurchaseId; } set { purchaseVendorInvoiceItem.PurchaseId = value; } }
        [FwLogicProperty(Id: "3tVnvdL9Y1y7")]
        public string VendorInvoiceId { get { return purchaseVendorInvoiceItem.VendorInvoiceId; } set { purchaseVendorInvoiceItem.VendorInvoiceId = value; } }
        [FwLogicProperty(Id: "cJfJdgJknbze")]
        public string VendorInvoiceItemId { get { return purchaseVendorInvoiceItem.VendorInvoiceItemId; } set { purchaseVendorInvoiceItem.VendorInvoiceItemId = value; } }
        [FwLogicProperty(Id: "b9J0JMzpmUtr", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "NwSYkSwLslDHB", IsReadOnly: true)]
        public string PurchaseOrderItemId { get; set; }
        [FwLogicProperty(Id: "ZAk4uwhcpvST", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "STgYSQ78Norh", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "yH6kp9nYiAMu")]
        public int? Quantity { get { return purchaseVendorInvoiceItem.Quantity; } set { purchaseVendorInvoiceItem.Quantity = value; } }
        [FwLogicProperty(Id: "oHZTnoI4D3PU", IsReadOnly: true)]
        public decimal? UnitCost { get; set; }
        [FwLogicProperty(Id: "i9cLuFcoIGZA", IsReadOnly: true)]
        public decimal? Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}

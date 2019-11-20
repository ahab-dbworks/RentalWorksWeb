using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.InvoiceReceipt
{
    [FwLogic(Id: "49h7jrBiX2cm")]
    public class InvoiceReceiptLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceReceiptRecord invoiceReceipt = new InvoiceReceiptRecord();
        InvoiceReceiptLoader invoiceReceiptLoader = new InvoiceReceiptLoader();
        public InvoiceReceiptLogic()
        {
            dataRecords.Add(invoiceReceipt);
            dataLoader = invoiceReceiptLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "IYOn52uiMD4rQ", IsPrimaryKey: true)]
        public int? InvoiceReceiptId { get { return invoiceReceipt.InvoiceReceiptId; } set { invoiceReceipt.InvoiceReceiptId = value; } }
        [FwLogicProperty(Id: "Ggi7HGuMNdZg")]
        public string InvoiceId { get { return invoiceReceipt.InvoiceId; } set { invoiceReceipt.InvoiceId = value; } }
        [FwLogicProperty(Id: "G99XZCAOUpSR")]
        public string ReceiptId { get { return invoiceReceipt.ReceiptId; } set { invoiceReceipt.ReceiptId = value; } }
        [FwLogicProperty(Id: "X0xDQDANjLj5O", IsReadOnly: true)]
        public string ReceiptDate { get; set; }
        [FwLogicProperty(Id: "6Rou679GB8Z", IsReadOnly: true)]
        public string PaymentTypeId { get; set; }
        [FwLogicProperty(Id: "FhZoC0RyV2S", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "uJvygdAwOzDZ", IsReadOnly: true)]
        public string CheckNumber { get; set; }
        [FwLogicProperty(Id: "MjiveJjqqAHr1", IsReadOnly: true)]
        public bool? RecType { get; set; }
        [FwLogicProperty(Id: "xz5JtiKfg9gDI", IsReadOnly: true)]
        public string PaymentBy { get; set; }
        [FwLogicProperty(Id: "mujaLJdYOQ2n", IsReadOnly: true)]
        public string ReceiptCustomerId { get; set; }
        [FwLogicProperty(Id: "EHLQmOFtEsh7", IsReadOnly: true)]
        public string ReceiptCustomer { get; set; }
        [FwLogicProperty(Id: "AR3NVD7oCErC", IsReadOnly: true)]
        public string ReceiptDealId { get; set; }
        [FwLogicProperty(Id: "DUxM0ebCBynPf", IsReadOnly: true)]
        public string ReceiptDeal { get; set; }
        [FwLogicProperty(Id: "CMLo5k5UuE2W", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "76x3oDoRYFij3", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "AHkE57zLovh7i", IsReadOnly: true)]
        public string InvoiceDescription { get; set; }
        [FwLogicProperty(Id: "FIyhBJTiP3U2", IsReadOnly: true)]
        public string InvoiceDealId { get; set; }
        [FwLogicProperty(Id: "1jzzfUPzT2HQ", IsReadOnly: true)]
        public string InvoiceDeal { get; set; }
        [FwLogicProperty(Id: "RvNDPhUphSjY")]
        public decimal? Amount { get { return invoiceReceipt.Amount; } set { invoiceReceipt.Amount = value; } }
        [FwLogicProperty(Id: "Id2KXjJdiNi", IsReadOnly: true)]
        public string AppliedById { get; set; }
        [FwLogicProperty(Id: "ncZWr1Qgp22", IsReadOnly: true)]
        public string AppliedBy { get; set; }
        [FwLogicProperty(Id: "hzNeDn8OyNI", IsReadOnly: true)]
        public string PaymentMemo { get; set; }
        [FwLogicProperty(Id: "LszY68davOcH")]
        public string DateStamp { get { return invoiceReceipt.DateStamp; } set { invoiceReceipt.DateStamp = value; } }
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

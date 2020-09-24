using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoicePayment
{
    [FwLogic(Id: "MPczRY8zfX58E")]
    public class VendorInvoicePaymentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoicePaymentRecord vendorInvoicePayment = new VendorInvoicePaymentRecord();
        VendorInvoicePaymentLoader vendorInvoicePaymentLoader = new VendorInvoicePaymentLoader();
        public VendorInvoicePaymentLogic()
        {
            dataRecords.Add(vendorInvoicePayment);
            dataLoader = vendorInvoicePaymentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ZLmPdpgQM3Hw", IsPrimaryKey: true)]
        public int? VendorInvoicePaymentId { get { return vendorInvoicePayment.VendorInvoicePaymentId; } set { vendorInvoicePayment.VendorInvoicePaymentId = value; } }
        [FwLogicProperty(Id: "VIt18X0ws98U", IsReadOnly: true)]
        public string VendorInvoiceId { get { return vendorInvoicePayment.VendorInvoiceId; } set { vendorInvoicePayment.VendorInvoiceId = value; } }
        [FwLogicProperty(Id: "Dvhbcg4afUkR", IsReadOnly: true)]
        public string PaymentId { get { return vendorInvoicePayment.PaymentId; } set { vendorInvoicePayment.PaymentId = value; } }
        [FwLogicProperty(Id: "UGuP0TQglVMp", IsReadOnly: true)]
        public decimal? Amount { get { return vendorInvoicePayment.Amount; } set { vendorInvoicePayment.Amount = value; } }
        [FwLogicProperty(Id: "2dgCcqwYO3S", IsReadOnly: true)]
        public string PaymentDate { get; set; }
        [FwLogicProperty(Id: "vsdVdQXOF595D", IsReadOnly: true)]
        public string LocationId { get; set; }
        [FwLogicProperty(Id: "xkqzBTAPucpt", IsReadOnly: true)]
        public string PaymentTypeId { get; set; }
        [FwLogicProperty(Id: "Fhk5bIHHg3gm", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "95yAfrvz93WmI", IsReadOnly: true)]
        public string CheckNumber { get; set; }
        [FwLogicProperty(Id: "xIfiJhRqbdJEs", IsReadOnly: true)]
        public string AppliedBy { get; set; }
        [FwLogicProperty(Id: "nNSy94N3s7yJA", IsReadOnly: true)]
        public string ModifiedById { get; set; }
        [FwLogicProperty(Id: "mwunt5ZT9cLe", IsReadOnly: true)]
        public string ModifiedBy { get; set; }
        [FwLogicProperty(Id: "1KHpkotytrFk", IsReadOnly: true)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

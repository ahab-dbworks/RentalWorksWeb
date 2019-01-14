using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.VendorInvoicePayment
{
    [FwLogic(Id: "MPczRY8zfX58E")]
    public class VendorInvoicePaymentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoicePaymentLoader vendorInvoicePaymentLoader = new VendorInvoicePaymentLoader();
        public VendorInvoicePaymentLogic()
        {
            dataLoader = vendorInvoicePaymentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "VIt18X0ws98U", IsReadOnly: true)]
        public string VendorInvoiceId { get; set; }
        [FwLogicProperty(Id: "Dvhbcg4afUkR", IsReadOnly: true)]
        public string PaymentId { get; set; }
        [FwLogicProperty(Id: "UGuP0TQglVMp", IsReadOnly: true)]
        public decimal? Amount { get; set; }
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

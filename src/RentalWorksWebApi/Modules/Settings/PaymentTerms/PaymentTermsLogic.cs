using FwStandard.AppManager;
using WebApi.Logic;
using RRentalWorksWebApi.Modules.Settings.PaymentTerms;

namespace WebApi.Modules.Settings.PaymentTerms
{
    [FwLogic(Id:"qGnQQLpuZ0M4")]
    public class PaymentTermsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PaymentTermsRecord paymentTerms = new PaymentTermsRecord();
        public PaymentTermsLogic()
        {
            dataRecords.Add(paymentTerms);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"FoQQ8tHUBkgA", IsPrimaryKey:true)]
        public string PaymentTermsId { get { return paymentTerms.PaymentTermsId; } set { paymentTerms.PaymentTermsId = value; } }

        [FwLogicProperty(Id:"FoQQ8tHUBkgA", IsRecordTitle:true)]
        public string PaymentTerms { get { return paymentTerms.PaymentTerms; } set { paymentTerms.PaymentTerms = value; } }

        [FwLogicProperty(Id:"jyZkqJZavEQw")]
        public string InvoiceMessage { get { return paymentTerms.InvoiceMessage; } set { paymentTerms.InvoiceMessage = value; } }

        [FwLogicProperty(Id:"0vY33ieqF1CT")]
        public int? DueInDays { get { return paymentTerms.DueInDays; } set { paymentTerms.DueInDays = value; } }

        [FwLogicProperty(Id:"fgcopymX9tZ5")]
        public bool? COD { get { return paymentTerms.COD; } set { paymentTerms.COD = value; } }

        [FwLogicProperty(Id:"5dS4DKwruMXo")]
        public string PaymentTermsCode { get { return paymentTerms.PaymentTermsCode; } set { paymentTerms.PaymentTermsCode = value; } }

        [FwLogicProperty(Id:"sL03hbirGCm6")]
        public bool? Inactive { get { return paymentTerms.Inactive; } set { paymentTerms.Inactive = value; } }

        [FwLogicProperty(Id:"uuDqncGPDgCD")]
        public string DateStamp { get { return paymentTerms.DateStamp; } set { paymentTerms.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

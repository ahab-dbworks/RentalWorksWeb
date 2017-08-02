using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RRentalWorksWebApi.Modules.Settings.PaymentTerms;

namespace RentalWorksWebApi.Modules.Settings.PaymentTerms
{
    public class PaymentTermsLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PaymentTermsRecord paymentTerms = new PaymentTermsRecord();
        public PaymentTermsLogic()
        {
            dataRecords.Add(paymentTerms);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PaymentTermsId { get { return paymentTerms.PaymentTermsId; } set { paymentTerms.PaymentTermsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PaymentTerms { get { return paymentTerms.PaymentTerms; } set { paymentTerms.PaymentTerms = value; } }
        public string InvoiceMessage { get { return paymentTerms.InvoiceMessage; } set { paymentTerms.InvoiceMessage = value; } }
        public int DueInDays { get { return paymentTerms.DueInDays; } set { paymentTerms.DueInDays = value; } }
        public bool COD { get { return paymentTerms.COD; } set { paymentTerms.COD = value; } }
        public string PaymentTermsCode { get { return paymentTerms.PaymentTermsCode; } set { paymentTerms.PaymentTermsCode = value; } }
        public bool Inactive { get { return paymentTerms.Inactive; } set { paymentTerms.Inactive = value; } }
        public string DateStamp { get { return paymentTerms.DateStamp; } set { paymentTerms.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using FwStandard.BusinessLogic.Attributes;
using WebApi.Data.Settings;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PaymentType
{
    public class PaymentTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PaymentTypeRecord paymentType = new PaymentTypeRecord();
        PaymentTypeLoader paymentTypeLoader = new PaymentTypeLoader();

        public PaymentTypeLogic()
        {
            dataRecords.Add(paymentType);
            dataLoader = paymentTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PaymentTypeId { get { return paymentType.PaymentTypeId; } set { paymentType.PaymentTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PaymentType { get { return paymentType.PaymentType; } set { paymentType.PaymentType = value; } }
        public string ShortName { get { return paymentType.ShortName; } set { paymentType.ShortName = value; } }
        public string PaymentTypeType { get { return paymentType.PaymentTypeType; } set { paymentType.PaymentTypeType = value; } }
        public string GlAccountId { get { return paymentType.GlAccountId; } set { paymentType.GlAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GlAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GlAccountDescription { get; set; }
        public bool? AccountingTransaction { get { return paymentType.AccountingTransaction; } set { paymentType.AccountingTransaction = value; } }
        public string ExportPaymentMethod { get { return paymentType.ExportPaymentMethod; } set { paymentType.ExportPaymentMethod = value; } }
        public string ExportPaymentType { get { return paymentType.ExportPaymentType; } set { paymentType.ExportPaymentType = value; } }
        public bool? IncludeInRentalWorksNet { get { return paymentType.IncludeInRentalWorksNet; } set { paymentType.IncludeInRentalWorksNet = value; } }
        public string RentalWorksNetCaption { get { return paymentType.RentalWorksNetCaption; } set { paymentType.RentalWorksNetCaption = value; } }
        public bool? Inactive { get { return paymentType.Inactive; } set { paymentType.Inactive = value; } }
        public string DateStamp { get { return paymentType.DateStamp; } set { paymentType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

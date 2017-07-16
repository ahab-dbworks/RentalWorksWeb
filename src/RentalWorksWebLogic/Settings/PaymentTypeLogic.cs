using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class PaymentTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PaymentTypeRecord paymentType = new PaymentTypeRecord();
        public PaymentTypeLogic()
        {
            dataRecords.Add(paymentType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PaymentTypeId { get { return paymentType.PaymentTypeId; } set { paymentType.PaymentTypeId = value; } }
        [FwBusinessLogicField(isTitle: true)]
        public string PaymentType { get { return paymentType.PaymentType; } set { paymentType.PaymentType = value; } }
        public string ShortName { get { return paymentType.ShortName; } set { paymentType.ShortName = value; } }
        public string PaymentTypeType { get { return paymentType.PaymentTypeType; } set { paymentType.PaymentTypeType = value; } }
        public string AccountingTransaction { get { return paymentType.AccountingTransaction; } set { paymentType.AccountingTransaction = value; } }
        public string Inactive { get { return paymentType.Inactive; } set { paymentType.Inactive = value; } }
        public DateTime? DateStamp { get { return paymentType.DateStamp; } set { paymentType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

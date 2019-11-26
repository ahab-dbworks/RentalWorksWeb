using FwStandard.AppManager;
using WebApi.Data.Settings;
using WebApi.Data.Settings.PaymentSettings.PaymentType;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PaymentSettings.PaymentType
{
    [FwLogic(Id:"vjb0v4uLTk6O")]
    public class PaymentTypeLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"tXRm6S81hzPI", IsPrimaryKey:true)]
        public string PaymentTypeId { get { return paymentType.PaymentTypeId; } set { paymentType.PaymentTypeId = value; } }

        [FwLogicProperty(Id:"tXRm6S81hzPI", IsRecordTitle:true)]
        public string PaymentType { get { return paymentType.PaymentType; } set { paymentType.PaymentType = value; } }

        [FwLogicProperty(Id:"I06SJMLgiUgY")]
        public string ShortName { get { return paymentType.ShortName; } set { paymentType.ShortName = value; } }

        [FwLogicProperty(Id:"pUCJzGq5P23z")]
        public string PaymentTypeType { get { return paymentType.PaymentTypeType; } set { paymentType.PaymentTypeType = value; } }

        [FwLogicProperty(Id:"EznGbmp4IUeX")]
        public string GlAccountId { get { return paymentType.GlAccountId; } set { paymentType.GlAccountId = value; } }

        [FwLogicProperty(Id:"ykL3Vq3STYh6", IsReadOnly:true)]
        public string GlAccountNo { get; set; }

        [FwLogicProperty(Id:"gNIYrsb4hEYs", IsReadOnly:true)]
        public string GlAccountDescription { get; set; }

        [FwLogicProperty(Id:"qolwOfsfXIUD")]
        public bool? AccountingTransaction { get { return paymentType.AccountingTransaction; } set { paymentType.AccountingTransaction = value; } }

        [FwLogicProperty(Id:"F9JSSniRbEhF")]
        public string ExportPaymentMethod { get { return paymentType.ExportPaymentMethod; } set { paymentType.ExportPaymentMethod = value; } }

        [FwLogicProperty(Id:"iLm9Yj4ylbYq")]
        public string ExportPaymentType { get { return paymentType.ExportPaymentType; } set { paymentType.ExportPaymentType = value; } }

        [FwLogicProperty(Id:"5HrHi5gzoDtE")]
        public bool? IncludeInRentalWorksNet { get { return paymentType.IncludeInRentalWorksNet; } set { paymentType.IncludeInRentalWorksNet = value; } }

        [FwLogicProperty(Id:"u6HqO3Z683DF")]
        public string RentalWorksNetCaption { get { return paymentType.RentalWorksNetCaption; } set { paymentType.RentalWorksNetCaption = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxx")]
        public string Color { get { return paymentType.Color; } set { paymentType.Color = value; } }

        [FwLogicProperty(Id:"Pd9IDWpCOmzM")]
        public bool? Inactive { get { return paymentType.Inactive; } set { paymentType.Inactive = value; } }

        [FwLogicProperty(Id:"Ia3IC4XZxJBD")]
        public string DateStamp { get { return paymentType.DateStamp; } set { paymentType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

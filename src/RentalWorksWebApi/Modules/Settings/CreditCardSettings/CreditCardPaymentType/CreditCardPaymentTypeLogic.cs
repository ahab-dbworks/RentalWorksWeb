using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.CreditCardSettings.CreditCardPaymentType
{
    [FwLogic(Id: "RJypXojhK4wV")]
    public class CreditCardPaymentTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CreditCardPaymentTypeRecord creditCardPayType = new CreditCardPaymentTypeRecord();
        CreditCardPaymentTypeLoader creditCardPayTypeLoader = new CreditCardPaymentTypeLoader();
        public CreditCardPaymentTypeLogic()
        {
            dataRecords.Add(creditCardPayType);
            dataLoader = creditCardPayTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "39Va7UxXMhnr", IsPrimaryKey: true)]
        public int? CreditCardPaymentTypeId { get { return creditCardPayType.CreditCardPaymentTypeId; } set { creditCardPayType.CreditCardPaymentTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "vXkCma6qThnF")]
        public string Description { get { return creditCardPayType.Description; } set { creditCardPayType.Description = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "zO3NNmrVqw14")]
        public string ChargePaymentTypeId { get { return creditCardPayType.ChargePaymentTypeId; } set { creditCardPayType.ChargePaymentTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "EW4zMJrrzNDE", IsReadOnly: true)]
        public string ChargePaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "j8MTDMOEUUtd")]
        public string RefundPaymentTypeId { get { return creditCardPayType.RefundPaymentTypeId; } set { creditCardPayType.RefundPaymentTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2hcVL2UrRFwI")]
        public string RefundPaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "knVQmjVVWrKt")]
        public string DateStamp { get { return creditCardPayType.DateStamp; } set { creditCardPayType.DateStamp = value; } }
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

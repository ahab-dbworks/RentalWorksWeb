using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.Payment
{
    [FwLogic(Id: "YAxLRif4rWXiK")]
    public class PaymentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PaymentRecord payment = new PaymentRecord();
        PaymentLoader paymentLoader = new PaymentLoader();
        public PaymentLogic()
        {
            dataRecords.Add(payment);
            dataLoader = paymentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "yCrCqskyzZl0e", IsPrimaryKey: true)]
        public string PaymentId { get { return payment.PaymentId; } set { payment.PaymentId = value; } }
        [FwLogicProperty(Id: "YCsK3PiyvQWgC")]
        public string PaymentDate { get { return payment.PaymentDate; } set { payment.PaymentDate = value; } }
        [FwLogicProperty(Id: "YDDO3FCMvkhD8")]
        public string OfficeLocationId { get { return payment.OfficeLocationId; } set { payment.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "YDfcZ3tuykY31")]
        public string DepartmentId { get { return payment.DepartmentId; } set { payment.DepartmentId = value; } }
        [FwLogicProperty(Id: "YDnknCM97UjHd", IsReadOnly: true)]
        public string LocationCode { get; set; }
        [FwLogicProperty(Id: "yDxDcyN8jMfa8", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "YdXm4VICUzQKB")]
        public string VendorId { get { return payment.VendorId; } set { payment.VendorId = value; } }
        [FwLogicProperty(Id: "YEXjcF65Inc9k", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "YeYAiaG7yIG0H")]
        public string PaymentTypeId { get { return payment.PaymentTypeId; } set { payment.PaymentTypeId = value; } }
        [FwLogicProperty(Id: "YFwa55mvcaFvR", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "YFzhaQ9kteU5Y")]
        public int? BankAccountId { get { return payment.BankAccountId; } set { payment.BankAccountId = value; } }
        [FwLogicProperty(Id: "yGOghdOViV5aK", IsReadOnly: true)]
        public string AccountName { get; set; }
        [FwLogicProperty(Id: "yGs9XcrH9KiTd", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "yh6sFlfKRP3rd")]
        public string CurrencyId { get { return payment.CurrencyId; } set { payment.CurrencyId = value; } }
        [FwLogicProperty(Id: "yH87lNko2L0vr", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "yHP8QjxTjE1Cn", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "yhr3OKDaRjTHK", IsReadOnly: true)]
        public bool? CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "Yhtdr097snj6H")]
        public string CheckNumber { get { return payment.CheckNumber; } set { payment.CheckNumber = value; } }
        [FwLogicProperty(Id: "yiS2CuKG1E0Ps")]
        public string PaymentDocumentNumber { get { return payment.PaymentDocumentNumber; } set { payment.PaymentDocumentNumber = value; } }
        [FwLogicProperty(Id: "yiuQU9Tm5XQT1")]
        public decimal? Amount { get { return payment.Amount; } set { payment.Amount = value; } }
        [FwLogicProperty(Id: "yjGOxisrRWI9r")]
        public string AppliedById { get { return payment.AppliedById; } set { payment.AppliedById = value; } }
        [FwLogicProperty(Id: "yjPQpg5eexOnS", IsReadOnly: true)]
        public string AppliedBy { get; set; }
        [FwLogicProperty(Id: "yKbg8dRpHx2Cg")]
        public string ModifiedById { get { return payment.ModifiedById; } set { payment.ModifiedById = value; } }
        [FwLogicProperty(Id: "yKjsz46xGmpnK", IsReadOnly: true)]
        public string ModifiedBy { get; set; }
        [FwLogicProperty(Id: "ykSeB0sWuztzW")]
        public string PaymentMemo { get { return payment.PaymentMemo; } set { payment.PaymentMemo = value; } }
        [FwLogicProperty(Id: "YKSMgdeGwa5H4")]
        public string RecType { get { return payment.RecType; } set { payment.RecType = value; } }
        [FwLogicProperty(Id: "YKu7te3KZAgDt", IsReadOnly: true)]
        public string ChargeBatchId { get; set; }
        [FwLogicProperty(Id: "yKxooboo3mWAw", IsReadOnly: true)]
        public string ChargeBatchNumber { get; set; }
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

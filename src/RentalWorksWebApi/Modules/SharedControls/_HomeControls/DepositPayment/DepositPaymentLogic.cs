using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.DepositPayment
{
    [FwLogic(Id: "0eQ5P0fWWbPQQ")]
    public class DepositPaymentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DepositPaymentRecord depositPayment = new DepositPaymentRecord();
        public DepositPaymentLogic()
        {
            dataRecords.Add(depositPayment);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0Fp4jMXWA6yPn", IsPrimaryKey: true)]
        public int? Id { get { return depositPayment.Id; } set { depositPayment.Id = value; } }
        [FwLogicProperty(Id: "0ERqizkbnR5iz")]
        public string DepositId { get { return depositPayment.DepositId; } set { depositPayment.DepositId = value; } }
        [FwLogicProperty(Id: "0fh7WS4w8HP9Q")]
        public string PaymentId { get { return depositPayment.PaymentId; } set { depositPayment.PaymentId = value; } }
        [FwLogicProperty(Id: "0flT5vdJ3azzy")]
        public decimal? Applied { get { return depositPayment.Applied; } set { depositPayment.Applied = value; } }
        [FwLogicProperty(Id: "0GUEIgdHnKYwz")]
        public string DateStamp { get { return depositPayment.DateStamp; } set { depositPayment.DateStamp = value; } }
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

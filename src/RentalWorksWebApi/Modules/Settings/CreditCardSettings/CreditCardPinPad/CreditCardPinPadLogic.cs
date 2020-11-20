using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.CreditCardSettings.CreditCardPinPad
{
    [FwLogic(Id: "")]
    public class CreditCardPinPadLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CreditCardPinPadRecord creditCardPinPad = new CreditCardPinPadRecord();
        CreditCardPinPadLoader creditCardPinPadLoader = new CreditCardPinPadLoader();
        public CreditCardPinPadLogic()
        {
            dataRecords.Add(creditCardPinPad);
            dataLoader = creditCardPinPadLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qY9BzzXSJX8O", IsPrimaryKey: true)]
        public int? CreditCardPinPadId { get { return creditCardPinPad.CreditCardPinPadId; } set { creditCardPinPad.CreditCardPinPadId = value; } }
        [FwLogicProperty(Id: "sqmDQWRNs5cl")]
        public string Code { get { return creditCardPinPad.Code; } set { creditCardPinPad.Code = value; } }
        [FwLogicProperty(Id: "TBF48nRWILIa")]
        public string Description { get { return creditCardPinPad.Description; } set { creditCardPinPad.Description = value; } }
        [FwLogicProperty(Id: "jsAFaDl03klc")]
        public bool? Inactive { get { return creditCardPinPad.Inactive; } set { creditCardPinPad.Inactive = value; } }
        [FwLogicProperty(Id: "WDC3YRGuVYR8")]
        public string DateStamp { get { return creditCardPinPad.DateStamp; } set { creditCardPinPad.DateStamp = value; } }
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

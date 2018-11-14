using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Currency
{
    [FwLogic(Id:"osF6u7SxopZX")]
    public class CurrencyLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CurrencyRecord currency = new CurrencyRecord();
        public CurrencyLogic()
        {
            dataRecords.Add(currency);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"MflGZuonIOGe", IsPrimaryKey:true)]
        public string CurrencyId { get { return currency.CurrencyId; } set { currency.CurrencyId = value; } }

        [FwLogicProperty(Id:"MflGZuonIOGe", IsRecordTitle:true)]
        public string Currency { get { return currency.Currency; } set { currency.Currency = value; } }

        [FwLogicProperty(Id:"pdo9yY5wArVt")]
        public string CurrencyCode { get { return currency.CurrencyCode;  } set { currency.CurrencyCode = value; } }

        [FwLogicProperty(Id:"VVRw4ZRJNJHm")]
        public string CurrencySymbol { get { return currency.CurrencySymbol;  } set { currency.CurrencySymbol = value; } }

        [FwLogicProperty(Id:"R7rvcYA4IPyE")]
        public string DateStamp { get { return currency.DateStamp; } set { currency.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

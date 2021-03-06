using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Settings.CurrencyExchangeRate
{
    [FwLogic(Id: "uIlu5prs2gIGn")]
    public class CurrencyExchangeRateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CurrencyExchangeRateRecord currencyExchangeRate = new CurrencyExchangeRateRecord();
        CurrencyExchangeRateLoader currencyExchangeRateLoader = new CurrencyExchangeRateLoader();
        public CurrencyExchangeRateLogic()
        {
            dataRecords.Add(currencyExchangeRate);
            dataLoader = currencyExchangeRateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "uiQZTLCY6q0Dc", IsPrimaryKey: true)]
        public string CurrencyExchangeRateId { get { return currencyExchangeRate.CurrencyExchangeRateId; } set { currencyExchangeRate.CurrencyExchangeRateId = value; } }
        [FwLogicProperty(Id: "uIWlcvGqBAmIM")]
        public string AsOfDate { get { return currencyExchangeRate.AsOfDate; } set { currencyExchangeRate.AsOfDate = value; } }
        [FwLogicProperty(Id: "uj6Z4kHDdENZI")]
        public string FromCurrencyId { get { return currencyExchangeRate.FromCurrencyId; } set { currencyExchangeRate.FromCurrencyId = value; } }
        [FwLogicProperty(Id: "ujKb7Gvj61fqd")]
        public string ImportDate { get { return currencyExchangeRate.ImportDate; } set { currencyExchangeRate.ImportDate = value; } }
        [FwLogicProperty(Id: "UjqXNJGVuWsMn")]
        public decimal? ExchangeRate { get { return currencyExchangeRate.ExchangeRate; } set { currencyExchangeRate.ExchangeRate = value; } }
        [FwLogicProperty(Id: "Ujwwj4e4B7lB6")]
        public string ToCurrencyId { get { return currencyExchangeRate.ToCurrencyId; } set { currencyExchangeRate.ToCurrencyId = value; } }
        [FwLogicProperty(Id: "uKCTo3hOBx0Pj", IsReadOnly: true)]
        public string FromCurrencyCode { get; set; }
        [FwLogicProperty(Id: "UKTn5T56tHWFm", IsReadOnly: true)]
        public string FromCurrency { get; set; }
        [FwLogicProperty(Id: "krrDqznA7SqfQ", IsReadOnly: true)]
        public string FromCurrencySymbol { get; set; }
        [FwLogicProperty(Id: "ul55M6LfrqGPj", IsReadOnly: true)]
        public string ToCurrencyCode { get; set; }
        [FwLogicProperty(Id: "ULkQiTE3OXyon", IsReadOnly: true)]
        public string ToCurrency { get; set; }
        [FwLogicProperty(Id: "ylHiVdzmRFVwm", IsReadOnly: true)]
        public string ToCurrencySymbol { get; set; }
        [FwLogicProperty(Id: "s4Ns6pDkcm7qr", IsReadOnly: true)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                bool sameCurrency = false;
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    if ((!string.IsNullOrEmpty(FromCurrencyId)) && (!string.IsNullOrEmpty(ToCurrencyId)) && (FromCurrencyId.Equals(ToCurrencyId)))
                    {
                        sameCurrency = true;
                    }
                }
                else //smUpdate
                {
                    CurrencyExchangeRateLogic orig = (CurrencyExchangeRateLogic)original;
                    string fromCurrencyId = FromCurrencyId;
                    string toCurrencyId = ToCurrencyId;
                    if (orig != null)
                    {
                        fromCurrencyId = fromCurrencyId ?? orig.FromCurrencyId;
                        toCurrencyId = toCurrencyId ?? orig.ToCurrencyId;
                    }
                    if ((!string.IsNullOrEmpty(fromCurrencyId)) && (!string.IsNullOrEmpty(toCurrencyId)) && (fromCurrencyId.Equals(toCurrencyId)))
                    {
                        sameCurrency = true;
                    }
                }
                if (sameCurrency)
                {
                    isValid = false;
                    validateMsg = "From Currency and To Currency cannot be the same.";
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}

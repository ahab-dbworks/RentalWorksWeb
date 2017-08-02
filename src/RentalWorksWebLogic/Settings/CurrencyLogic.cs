using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class CurrencyLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CurrencyRecord currency = new CurrencyRecord();
        public CurrencyLogic()
        {
            dataRecords.Add(currency);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CurrencyId { get { return currency.CurrencyId; } set { currency.CurrencyId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Currency { get { return currency.Currency; } set { currency.Currency = value; } }
        public string CurrencyCode { get { return currency.CurrencyCode;  } set { currency.CurrencyCode = value; } }
        public string DateStamp { get { return currency.DateStamp; } set { currency.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.SqlServer
{
    public enum FwDataTypes {
        Text,
        Date,
        Time,
        DateTime,
        DateTimeOffset,
        Decimal,
        Boolean,
        CurrencyString,
        CurrencyStringNoDollarSign,
        CurrencyStringNoDollarSignNoDecimalPlaces,
        PhoneUS,
        ZipcodeUS,
        Percentage,
        OleToHtmlColor,
        Integer,
        JpgDataUrl,
        UTCDateTime
    }
}

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
        DecimalStringNoTrailingZeros,
        DecimalString1Digit,
        DecimalString2Digits,
        DecimalString3Digits,
        DecimalString4Digits,
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

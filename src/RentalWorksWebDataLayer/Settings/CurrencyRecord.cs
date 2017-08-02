using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("currency")]
    public class CurrencyRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "currencyid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CurrencyId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "currency", dataType: FwDataTypes.Text, length: 50)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "code", dataType: FwDataTypes.Text, length: 10)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

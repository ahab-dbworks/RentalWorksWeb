using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("country")]
    public class CountryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "countryid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CountryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "country", dataType: FwDataTypes.Text, length: 12)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "countrycode", dataType: FwDataTypes.Text, length: 1)]
        public string CountryCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "isusa", dataType: FwDataTypes.Boolean)]
        public string IsUSA { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "Metric", dataType: FwDataTypes.Boolean)]
        public string Metric { get; set; }
        //------------------------------------------------------------------------------------
        
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

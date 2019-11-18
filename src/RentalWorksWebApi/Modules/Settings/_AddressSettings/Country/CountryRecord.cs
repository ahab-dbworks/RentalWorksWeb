using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Country
{
    [FwSqlTable("country")]
    public class CountryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CountryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countrycode", modeltype: FwDataTypes.Text, maxlength: 3)]
        public string CountryCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isusa", modeltype: FwDataTypes.Boolean)]
        public bool? IsUSA { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "metric", modeltype: FwDataTypes.Boolean)]
        public bool? Metric { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

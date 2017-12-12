using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Region
{
    [FwSqlTable("region")]
    public class RegionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "regionid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string RegionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "region", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string Region { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

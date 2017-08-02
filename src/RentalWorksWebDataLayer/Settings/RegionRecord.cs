using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("region")]
    public class RegionRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "regionid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string RegionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "region", dataType: FwDataTypes.Text, length: 20)]
        public string Region { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

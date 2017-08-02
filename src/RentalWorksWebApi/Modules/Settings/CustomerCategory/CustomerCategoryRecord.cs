using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.CustomerCategory
{
    [FwSqlTable("custcat")]
    public class CustomerCategoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custcatid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CustomerCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custcatdesc", dataType: FwDataTypes.Text, length: 20)]
        public string CustomerCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }

    }
}

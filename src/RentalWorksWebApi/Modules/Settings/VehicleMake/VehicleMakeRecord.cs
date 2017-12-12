using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VehicleMake
{
    [FwSqlTable("vehiclemake")]
    public class VehicleMakeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemakeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string VehicleMakeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemake", modeltype: FwDataTypes.Text, maxlength: 15, required: true)]
        public string VehicleMake { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

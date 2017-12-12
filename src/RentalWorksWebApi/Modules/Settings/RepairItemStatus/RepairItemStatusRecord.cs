using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.RepairItemStatus
{
    [FwSqlTable("repairitemstatus")]
    public class RepairItemStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "repairitemstatusid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string RepairItemStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "repairitemstatus", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string RepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

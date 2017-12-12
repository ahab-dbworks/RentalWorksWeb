using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.InventoryAdjustmentReason
{
    [FwSqlTable("adjreason")]
    public class InventoryAdjustmentReasonRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invadjid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string InventoryAdjustmentReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "adjreason", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string InventoryAdjustmentReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

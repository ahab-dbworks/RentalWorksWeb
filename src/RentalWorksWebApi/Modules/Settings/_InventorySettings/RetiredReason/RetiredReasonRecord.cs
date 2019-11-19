using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.InventorySettings.RetiredReason
{
    [FwSqlTable("retiredreason")]
    public class RetiredReasonRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string RetiredReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "reasontype", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string ReasonType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

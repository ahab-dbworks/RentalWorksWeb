using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.UnretiredReason
{
    [FwSqlTable("unretiredreason")]
    public class UnretiredReasonRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unretiredreasonid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string UnretiredReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unretiredreason", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string UnretiredReason { get; set; }
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

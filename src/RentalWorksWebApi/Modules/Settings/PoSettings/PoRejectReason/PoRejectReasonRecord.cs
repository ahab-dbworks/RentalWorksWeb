using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.PoSettings.PoRejectReason
{
    [FwSqlTable("porejectreason")]
    public class PoRejectReasonRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "porejectreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PoRejectReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "porejectreason", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 60)]
        public string PoRejectReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactiveflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
} 

using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.PoApprovalStatus
{
    [FwSqlTable("poapprovalstatus")]
    public class PoApprovalStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PoApprovalStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, required: true)]
        public string PoApprovalStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatustype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, required: true)]
        public string PoApprovalStatusType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
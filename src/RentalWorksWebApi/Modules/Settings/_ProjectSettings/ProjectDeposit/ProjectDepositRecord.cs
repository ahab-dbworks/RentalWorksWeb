using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDeposit
{
    [FwSqlTable("deposit")]
    public class ProjectDepositRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depositid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ProjectDepositId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deposit", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string ProjectDeposit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
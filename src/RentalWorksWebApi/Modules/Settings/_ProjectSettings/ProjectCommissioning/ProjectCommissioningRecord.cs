using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectCommissioning
{
    [FwSqlTable("commissioning")]
    public class ProjectCommissioningRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissioningid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ProjectCommissioningId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissioning", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string ProjectCommissioning { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
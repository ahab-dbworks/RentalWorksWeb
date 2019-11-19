using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDrawings
{
    [FwSqlTable("drawing")]
    public class ProjectDrawingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "drawingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ProjectDrawingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "drawing", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string ProjectDrawings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
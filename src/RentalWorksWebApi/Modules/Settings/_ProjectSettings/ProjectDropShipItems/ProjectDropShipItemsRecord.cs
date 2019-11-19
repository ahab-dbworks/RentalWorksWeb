using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDropShipItems
{
    [FwSqlTable("dropship")]
    public class ProjectDropShipItemsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropshipid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ProjectDropShipItemsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string ProjectDropShipItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
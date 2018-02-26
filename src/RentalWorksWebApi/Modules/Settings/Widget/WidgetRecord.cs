using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.Widget
{
    [FwSqlTable("widget")]
    public class WidgetRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WidgetId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widget", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Widget { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "apiname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ApiName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string DefaultType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
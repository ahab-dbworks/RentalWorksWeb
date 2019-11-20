using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.WebUserWidget
{
    [FwSqlTable("webuserswidget")]
    public class UserWidgetRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webuserswidgetid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string UserWidgetId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WidgetId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgettype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string WidgetType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datapoints", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DataPoints { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "axisnumberformatid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string AxisNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datanumberformatid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string DataNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datebehaviorid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string DateBehaviorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefield", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string DateField { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stacked", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Stacked { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disabled", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Disabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

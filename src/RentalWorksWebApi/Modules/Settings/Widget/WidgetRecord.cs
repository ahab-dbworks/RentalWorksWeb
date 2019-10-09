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
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "apiname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ApiName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "procedurename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ProcedureName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counterfieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string CounterFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label1fieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Label1FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label2fieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Label2FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "clickpath", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ClickPath { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string DefaultType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatapoints", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DefaultDataPoints { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultaxisnumberformatid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string DefaultAxisNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatanumberformatid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string DefaultDataNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatebehaviorid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string DefaultDateBehaviorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefielddisplaynames", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string DateFieldDisplayNames { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefields", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string DateFields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatefield", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string DefaultDateField { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultfromdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string DefaultFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttodate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string DefaultToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultstacked", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultStacked { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignto", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string AssignTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
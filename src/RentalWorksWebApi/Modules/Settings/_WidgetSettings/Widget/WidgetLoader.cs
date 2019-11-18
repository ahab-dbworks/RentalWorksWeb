using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.Widget
{
    [FwSqlTable("widgetview")]
    public class WidgetLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WidgetId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widget", modeltype: FwDataTypes.Text)]
        public string Widget { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "apiname", modeltype: FwDataTypes.Text)]
        public string ApiName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "procedurename", modeltype: FwDataTypes.Text)]
        public string ProcedureName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counterfieldname", modeltype: FwDataTypes.Text)]
        public string CounterFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label1fieldname", modeltype: FwDataTypes.Text)]
        public string Label1FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "label2fieldname", modeltype: FwDataTypes.Text)]
        public string Label2FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "clickpath", modeltype: FwDataTypes.Text)]
        public string ClickPath { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttype", modeltype: FwDataTypes.Text)]
        public string DefaultType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatapoints", modeltype: FwDataTypes.Integer)]
        public int? DefaultDataPoints { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultaxisnumberformatid", modeltype: FwDataTypes.Text)]
        public string DefaultAxisNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultaxisnumberformat", modeltype: FwDataTypes.Text)]
        public string DefaultAxisNumberFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultaxisnumberformatmask", modeltype: FwDataTypes.Text)]
        public string DefaultAxisNumberFormatMask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatanumberformatid", modeltype: FwDataTypes.Text)]
        public string DefaultDataNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatanumberformat", modeltype: FwDataTypes.Text)]
        public string DefaultDataNumberFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatanumberformatmask", modeltype: FwDataTypes.Text)]
        public string DefaultDataNumberFormatMask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatebehaviorid", modeltype: FwDataTypes.Text)]
        public string DefaultDateBehaviorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatebehavior", modeltype: FwDataTypes.Text)]
        public string DefaultDateBehavior { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefielddisplaynames", modeltype: FwDataTypes.Text)]
        public string DateFieldDisplayNames { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefields", modeltype: FwDataTypes.Text)]
        public string DateFields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatefield", modeltype: FwDataTypes.Text)]
        public string DefaultDateField { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultfromdate", modeltype: FwDataTypes.Date)]
        public string DefaultFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttodate", modeltype: FwDataTypes.Date)]
        public string DefaultToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignto", modeltype: FwDataTypes.Text)]
        public string AssignTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
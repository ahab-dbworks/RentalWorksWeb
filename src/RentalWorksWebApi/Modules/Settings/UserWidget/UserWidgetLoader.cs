using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.WebUserWidget
{
    [FwSqlTable("webuserswidgetview")]
    public class UserWidgetLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webuserswidgetid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserWidgetId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text)]
        public string WidgetId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widget", modeltype: FwDataTypes.Text)]
        public string Widget { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaulttype", modeltype: FwDataTypes.Text)]
        public string DefaultType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgettype", modeltype: FwDataTypes.Text)]
        public string WidgetType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdatapoints", modeltype: FwDataTypes.Integer)]
        public int? DefaultDataPoints { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datapoints", modeltype: FwDataTypes.Integer)]
        public int? DataPoints { get; set; }
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
        [FwSqlDataField(column: "axisnumberformatid", modeltype: FwDataTypes.Text)]
        public string AxisNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "axisnumberformat", modeltype: FwDataTypes.Text)]
        public string AxisNumberFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "axisnumberformatmask", modeltype: FwDataTypes.Text)]
        public string AxisNumberFormatMask { get; set; }
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
        [FwSqlDataField(column: "datanumberformatid", modeltype: FwDataTypes.Text)]
        public string DataNumberFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datanumberformat", modeltype: FwDataTypes.Text)]
        public string DataNumberFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datanumberformatmask", modeltype: FwDataTypes.Text)]
        public string DataNumberFormatMask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disabled", modeltype: FwDataTypes.Boolean)]
        public bool? Disabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WidgetId", "widgetid", select, request); 
            addFilterToSelect("UserId", "webusersid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

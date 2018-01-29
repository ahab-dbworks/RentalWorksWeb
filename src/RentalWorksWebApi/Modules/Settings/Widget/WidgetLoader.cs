using FwStandard.DataLayer; 
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
        [FwSqlDataField(column: "apiname", modeltype: FwDataTypes.Text)]
        public string ApiName { get; set; }
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
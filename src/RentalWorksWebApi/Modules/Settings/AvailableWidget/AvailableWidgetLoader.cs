using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 

namespace WebApi.Modules.Settings.AvailableWidget
{
    [FwSqlTable("dbo.funcavailablewidgets(@webusersid)")]
    public class AvailableWidgetLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widgetid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WidgetId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widget", modeltype: FwDataTypes.Text)]
        public string Widget { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            string webUserId = GetUniqueIdAsString("WebUserId", request);
            if (string.IsNullOrEmpty(webUserId))
            {
                webUserId = "~xx~";
            }
            select.AddParameter("@webusersid", webUserId);

        }
        //------------------------------------------------------------------------------------ 
    }
}
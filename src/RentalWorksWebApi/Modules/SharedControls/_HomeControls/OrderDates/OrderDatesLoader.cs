using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.OrderDates
{
    [FwSqlTable("dbo.funcorderdates(@orderid, @ordertypeid)")]
    public class OrderDatesLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedatetypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeDateTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemtype", modeltype: FwDataTypes.Boolean)]
        public bool? IsSystemType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydesc", modeltype: FwDataTypes.Text)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydescdisplay", modeltype: FwDataTypes.Text)]
        public string ActivityDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptiondisplay", modeltype: FwDataTypes.Text)]
        public string Descriptiondisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enabled", modeltype: FwDataTypes.Boolean)]
        public bool? IsEnabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "date", modeltype: FwDataTypes.Date)]
        public string Date { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "time", modeltype: FwDataTypes.Text)]
        public string Time { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datetimestr", modeltype: FwDataTypes.Text)]
        public string DateAndTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dayofweek", modeltype: FwDataTypes.Text)]
        public string DayOfWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualdate", modeltype: FwDataTypes.Date)]
        public string ActualDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualtime", modeltype: FwDataTypes.Text)]
        public string ActualTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualdayofweek", modeltype: FwDataTypes.Text)]
        public string ActualDayOfWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "required", modeltype: FwDataTypes.Boolean)]
        public bool? IsRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionactivity", modeltype: FwDataTypes.Boolean)]
        public bool? IsProductionActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "milestone", modeltype: FwDataTypes.Boolean)]
        public bool? IsMilestone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string orderId = GetUniqueIdAsString("OrderId", request) ?? ""; 
            string orderTypeId = GetUniqueIdAsString("OrderTypeId", request) ?? ""; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddParameter("@orderid", orderId); 
            select.AddParameter("@ordertypeid", orderTypeId); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

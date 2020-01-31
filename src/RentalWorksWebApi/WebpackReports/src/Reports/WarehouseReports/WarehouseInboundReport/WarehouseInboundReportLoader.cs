using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Text;

namespace WebApi.Modules.Reports.WarehouseReports.WarehouseInboundReport
{
    [FwSqlTable("inboundrptview")]
    public class WarehouseInboundReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydate", modeltype: FwDataTypes.Date)]
        public string ActivityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytime", modeltype: FwDataTypes.Text)]
        public string ActivityTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydatetime", modeltype: FwDataTypes.Date)]
        public string ActivityDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer)]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypedesc", modeltype: FwDataTypes.Text)]
        public string ActivityTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqty", modeltype: FwDataTypes.Integer)]
        public int? TotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeqty", modeltype: FwDataTypes.Integer)]
        public int? CompleteQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remainingqty", modeltype: FwDataTypes.Integer)]
        public int? RemainingQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completepct", modeltype: FwDataTypes.Decimal)]
        public decimal? CompletePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusid", modeltype: FwDataTypes.Integer)]
        public int? ActivityStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatus", modeltype: FwDataTypes.Text)]
        public string ActivityStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusdesc", modeltype: FwDataTypes.Text)]
        public string ActivityStatusDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousersid", modeltype: FwDataTypes.Text)]
        public string AssignedToUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousername", modeltype: FwDataTypes.Text)]
        public string AssignedToUserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadoutdatetime", modeltype: FwDataTypes.Date)]
        public string LoadOutDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(WarehouseInboundReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            CheckBoxListItems sortBy = new CheckBoxListItems();

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    addDateFilterToSelect("activitydate", request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect("activitydate", request.ToDate, select, "<=", "todate");
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("activitytypeid", request.ActivityTypeId);
                    select.AddWhereIn("assignedtousersid", request.AgentId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddOrderBy("warehouseid");
                    if (request.SortBy != null)
                    {
                        CheckBoxListItems requestedSortBy = request.SortBy.GetSelectedItems();
                        if (requestedSortBy.Count > 0)
                        {
                            sortBy = requestedSortBy;
                        }
                    }

                    StringBuilder orderBy = new StringBuilder();
                    foreach (CheckBoxListItem item in sortBy)
                    {
                        if (orderBy.Length > 0)
                        {
                            orderBy.Append(",");
                        }
                        //  orderBy.Append(item.value.Equals("OfficeLocation") ? "location" : "");  // can use reflection for this
                        //  orderBy.Append(item.value.Equals("Name") ? "name" : "");
                        //  orderBy.Append(item.value.Equals("PaymentType") ? "paytype" : "");
                    }
                    //  orderBy.Append(", arid, invoiceid");
                    select.AddOrderBy(orderBy.ToString());
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "CompleteQuantity" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

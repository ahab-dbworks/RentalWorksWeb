using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.WarehouseInboundReport
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
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    //select.AddWhere("(xxxxid ^> ')"); 
                    //select.AddWhereIn("locationid", request.OfficeLocationId); 
                    //select.AddWhereIn("departmentid", request.DepartmentId); 
                    //addDateFilterToSelect("datefieldname1", request.DateValue1, select, "^>=", "dateparametername(optional)"); 
                    //addDateFilterToSelect("datefieldname2", request.DateValue2, select, "^<=", "dateparametername(optional)"); 
                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
                    select.AddOrderBy("field1,field2");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            //if (request.IncludeSubHeadingsAndSubTotals)
            //{
            //    string[] totalFields = new string[] { "RentalTotal", "SalesTotal" };
            //    dt.InsertSubTotalRows("GroupField1", "RowType", totalFields);
            //    dt.InsertSubTotalRows("GroupField2", "RowType", totalFields);
            //    dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            //}
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

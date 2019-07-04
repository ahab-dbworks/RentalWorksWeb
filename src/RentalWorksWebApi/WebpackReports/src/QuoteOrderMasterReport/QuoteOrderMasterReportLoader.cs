using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.QuoteOrderMasterReport
{
    [FwSqlTable("quoteordermasterrptview")]
    public class QuoteOrderMasterReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text)]
        public string CustomerStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
        public string DealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "typedesc", modeltype: FwDataTypes.Text)]
        public string TypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Integer)]
        public int? VersionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quotestatus", modeltype: FwDataTypes.Text)]
        public string QuoteStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date)]
        public string PrepDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadindate", modeltype: FwDataTypes.Date)]
        public string LoadinDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadintime", modeltype: FwDataTypes.Text)]
        public string LoadinTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "testdate", modeltype: FwDataTypes.Date)]
        public string TestDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "testtime", modeltype: FwDataTypes.Text)]
        public string TestTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string Estrentfrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string Estrentto { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikedate", modeltype: FwDataTypes.Date)]
        public string StrikeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "striketime", modeltype: FwDataTypes.Text)]
        public string StrikeTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickupdate", modeltype: FwDataTypes.Date)]
        public string PickupDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickuptime", modeltype: FwDataTypes.Text)]
        public string PickupTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillPeriodStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillPeriodEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillPeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "event", modeltype: FwDataTypes.Text)]
        public string Event { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grosstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? GrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? Subtotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grandtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? GrandTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(QuoteOrderMasterReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                //--------------------------------------------------------------------------------- 
                // below uses a "select" query to populate the FwJsonDataTable 
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
                //--------------------------------------------------------------------------------- 
                //--------------------------------------------------------------------------------- 
                //--------------------------------------------------------------------------------- 
                // below uses a stored procedure to populate the FwJsonDataTable 
                using (FwSqlCommand qry = new FwSqlCommand(conn, "procedurename", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    //qry.AddParameter("@someid", SqlDbType.Text, ParameterDirection.Input, request.SomeId);
                    //qry.AddParameter("@somedate", SqlDbType.Date, ParameterDirection.Input, request.SomeDate);
                    //AddPropertiesAsQueryColumns(qry);
                    //dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "RentalTotal", "SalesTotal" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Customer", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

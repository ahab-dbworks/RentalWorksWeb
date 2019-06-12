using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.CustomerRevenueByMonthReport
{
    [FwSqlTable("tmpreporttable")]
    public class CustomerRevenueByMonthReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorder", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month01", modeltype: FwDataTypes.Decimal)]
        public decimal? Month01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month01name", modeltype: FwDataTypes.Text)]
        public string Month01Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month02", modeltype: FwDataTypes.Decimal)]
        public decimal? Month02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month02name", modeltype: FwDataTypes.Text)]
        public string Month02Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month03", modeltype: FwDataTypes.Decimal)]
        public decimal? Month03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month03name", modeltype: FwDataTypes.Text)]
        public string Month03Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month04", modeltype: FwDataTypes.Decimal)]
        public decimal? Month04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month04name", modeltype: FwDataTypes.Text)]
        public string Month04Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month05", modeltype: FwDataTypes.Decimal)]
        public decimal? Month05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month05name", modeltype: FwDataTypes.Text)]
        public string Month05Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month06", modeltype: FwDataTypes.Decimal)]
        public decimal? Month06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month06name", modeltype: FwDataTypes.Text)]
        public string Month06Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month07", modeltype: FwDataTypes.Decimal)]
        public decimal? Month07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month07name", modeltype: FwDataTypes.Text)]
        public string Month07Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month08", modeltype: FwDataTypes.Decimal)]
        public decimal? Month08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month08name", modeltype: FwDataTypes.Text)]
        public string Month08Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month09", modeltype: FwDataTypes.Decimal)]
        public decimal? Month09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month09name", modeltype: FwDataTypes.Text)]
        public string Month09Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month10", modeltype: FwDataTypes.Decimal)]
        public decimal? Month10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month10name", modeltype: FwDataTypes.Text)]
        public string Month10Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month11", modeltype: FwDataTypes.Decimal)]
        public decimal? Month11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month11name", modeltype: FwDataTypes.Text)]
        public string Month11Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month12", modeltype: FwDataTypes.Decimal)]
        public decimal? Month12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month12name", modeltype: FwDataTypes.Text)]
        public string Month12Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allmonths", modeltype: FwDataTypes.Decimal)]
        public decimal? AllMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(CustomerRevenueByMonthReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getcustomerrevenuebymonthrptweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@customertypeid", SqlDbType.Text, ParameterDirection.Input, request.CustomerTypeId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, request.DealTypeId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    // qry.AddParameter("@inventorytypes", SqlDbType.Text, ParameterDirection.Input, request.RevenueTypes.ToString()); unsure of implemntation
                    qry.AddParameter("@summary", SqlDbType.Text, ParameterDirection.Input, request.IsSummary);

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Month01", "Month02", "Month03", "Month04", "Month05", "Month06", "Month07", "Month08", "Month09", "Month10", "Month11", "Month12", "AllMonths" };
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

using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryUsageReport
{
    [FwSqlTable("funcinventoryusagerpt(@fromdate, @todate)")]
    public class RentalInventoryUsageReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprimaryitemofcomplete", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimaryItemOfComplete { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportdays", modeltype: FwDataTypes.Integer)]
        public int? ReportDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availdays", modeltype: FwDataTypes.Decimal)]
        public decimal? AvailableDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currowned", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentOwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysout", modeltype: FwDataTypes.Integer)]
        public int? DaysOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysoutofservice", modeltype: FwDataTypes.Integer)]
        public int? DaysOutOfService { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysbilled", modeltype: FwDataTypes.Integer)]
        public int? DaysBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "srdays", modeltype: FwDataTypes.Integer)]
        public int? DaysSubRented { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "avgowned", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageOwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Revenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "srcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SubRentalCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "srrevenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SubRentalRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consfees", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ConsignmentFees { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustedrevenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AdjustedRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagepercent", modeltype: FwDataTypes.Decimal)]
        public decimal? UsagePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryUsageReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);

                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("rank", request.Ranks.ToString(), false);
                    select.AddWhereIn("trackedby", request.TrackedBys.ToString());

                    if (request.UtilizationFilterMode.Equals("LT"))
                    {
                        select.AddWhere("usagepercent < " + request.UtilizationFilterAmount.ToString());
                    }
                    else if (request.UtilizationFilterMode.Equals("GT"))
                    {
                        select.AddWhere("usagepercent > " + request.UtilizationFilterAmount.ToString());
                    }

                    if (request.ExcludeZeroOwned.GetValueOrDefault(false))
                    {
                        select.AddWhere("currowned <> 0");
                    }

                    if (request.OnlyIncludeItemsThatAreTheMainItemOfAComplete.GetValueOrDefault(false))
                    {
                        select.AddWhere("isprimaryitemofcomplete = 'T'");
                    }

                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "AvailableDays", "CurrentOwnedQuantity", "DaysOut", "DaysOutOfService", "DaysBilled", "DaysSubRented", "AverageOwnedQuantity", "Revenue", "SubRentalCost", "SubRentalRevenue", "ConsignmentFees", "AdjustedRevenue", };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

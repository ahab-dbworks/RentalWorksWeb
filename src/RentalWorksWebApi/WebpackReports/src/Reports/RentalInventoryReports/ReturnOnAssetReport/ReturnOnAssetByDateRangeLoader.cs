using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.RentalInventoryReports.ReturnOnAssetReport
{
    [FwSqlTable("dbo.dwreturnonasset(@fromdate, @todate, @icode)")]
    public class ReturnOnAssetByDateRangeReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MinDate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MaxDate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ReportPeriod", modeltype: FwDataTypes.Text)]
        public string ReportPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Department", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "SubCategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ICodeRank", modeltype: FwDataTypes.Text)]
        public string ICodeRank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ICode", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Description", modeltype: FwDataTypes.Text)]
        public string ICodeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TrackedBy", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "CurrentOwnedQty", modeltype: FwDataTypes.Integer)]
        public int? CurrentOwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "FromDateOwnedQty", modeltype: FwDataTypes.Integer)]
        public int? FromDateOwnedQty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ToDateOwnedQty", modeltype: FwDataTypes.Integer)]
        public int? ToDateOwnedQty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "AverageOwnedQty", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageOwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AverageWeeklyAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AverageWeeklyAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Utilization", modeltype: FwDataTypes.Percentage)]
        public decimal? Utilization { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "OutUtilization", modeltype: FwDataTypes.Percentage)]
        public decimal? OutUtilization { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ROA", modeltype: FwDataTypes.Percentage)]
        public decimal? AnnualROA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ROR", modeltype: FwDataTypes.Percentage)]
        public decimal? AnnualROR { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ROUV", modeltype: FwDataTypes.Percentage)]
        public decimal? AnnualROUV { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "WeeksBilled", modeltype: FwDataTypes.Integer)]
        public int? WeeksBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "OwnedRevenueAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? OwnedRevenueAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "VendorRevenueAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VendorRevenueAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "VendorCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VendorCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AverageCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AverageCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AssetAverageCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AssetAverageCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ReplacementCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TotalReplacementCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalReplacementCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "UnitValueCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValueCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AssetUnitValueCostAmt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AssetUnitValueCostAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RepairPct", modeltype: FwDataTypes.Percentage)]
        public decimal? RepairPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysRented", modeltype: FwDataTypes.Integer)]
        public int? DaysRented { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysOut", modeltype: FwDataTypes.Integer)]
        public int? DaysOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DailyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "WeeklyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MonthlyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MaxDailyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MaxDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MaxWeeklyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MaxWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MaxMonthlyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MaxMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RevenuePerDailyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RevenuePerDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RevenuePerWeeklyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RevenuePerWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RevenuePerMonthlyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RevenuePerMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "LTDRevenuePerDailyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LTDRevenuePerDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "LTDRevenuePerWeeklyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LTDRevenuePerWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "LTDRevenuePerMonthlyRate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LTDRevenuePerMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysOwned", modeltype: FwDataTypes.Integer)]
        public int? DaysOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ReturnOnAssetReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DataWarehouseDatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;

                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DataWarehouseDatabaseSettings.ReportTimeout))
                {
                    useWithNoLock = false;

                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);
                    select.AddParameter("@icode", "");

                    select.AddWhereIn("WarehouseKey", request.WarehouseId);
                    select.AddWhereIn("DepartmentKey", request.InventoryTypeId);
                    select.AddWhereIn("CategoryKey", request.CategoryId);
                    select.AddWhereIn("SubCategoryKey", request.SubCategoryId);
                    select.AddWhereIn("InventoryKey", request.InventoryId);

                    select.AddWhereIn("TrackedBy", request.TrackedBys);
                    select.AddWhereIn("ICodeRank", request.Ranks);

                    if (!request.IncludeZeroCurrentOwned.GetValueOrDefault(false))
                    {
                        select.AddWhere("CurrentOwnedQty <> 0");
                    }
                    if (!request.IncludeZeroAverageOwned.GetValueOrDefault(false))
                    {
                        select.AddWhere("AverageOwnedQty <> 0");
                    }
                    select.AddOrderBy("Warehouse, Department, Category, SubCategory, ICode");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "CurrentOwnedQuantity", "AverageOwnedQuantity", "AverageWeeklyAmount", "OwnedRevenueAmount", "VendorRevenueAmount", "VendorCostAmount", "TotalReplacementCostAmount", "AssetUnitValueCostAmount" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertSubTotalRows("SubCategory", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

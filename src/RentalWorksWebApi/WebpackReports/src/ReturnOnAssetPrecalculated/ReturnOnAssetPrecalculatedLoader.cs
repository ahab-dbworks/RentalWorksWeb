using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.ReturnOnAssetPrecalculated
{
    [FwSqlTable("ReturnOnAsset")]
    public class ReturnOnAssetPrecalculatedLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ReturnOnAssetKey", modeltype: FwDataTypes.Integer)]
        public int? ReturnOnAssetKey { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "FromDate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ToDate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ReportDateRangeCode", modeltype: FwDataTypes.Text)]
        public string ReportDateRangeCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RptYear", modeltype: FwDataTypes.Text)]
        public string RptYear { get; set; }
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
        [FwSqlDataField(column: "ICodeRank", modeltype: FwDataTypes.Boolean)]
        public bool? ICodeRank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ICode", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ICodeDescription", modeltype: FwDataTypes.Text)]
        public string ICodeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AverageOwnedQty", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageOwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AverageWeeklyAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageWeeklyAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Utilization", modeltype: FwDataTypes.Decimal)]
        public decimal? Utilization { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "OutUtilization", modeltype: FwDataTypes.Decimal)]
        public decimal? OutUtilization { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AnnualROA", modeltype: FwDataTypes.Decimal)]
        public decimal? AnnualROA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AnnualROR", modeltype: FwDataTypes.Decimal)]
        public decimal? AnnualROR { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AnnualROUV", modeltype: FwDataTypes.Decimal)]
        public decimal? AnnualROUV { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "WeeksBilled", modeltype: FwDataTypes.Integer)]
        public int? WeeksBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "OwnedRevenueAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedRevenueAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "VendorRevenueAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorRevenueAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "VendorCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AverageCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AssetAverageCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? AssetAverageCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ReplacementCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TotalReplacementCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalReplacementCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "UnitValueCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValueCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AssetUnitValueCostAmt", modeltype: FwDataTypes.Decimal)]
        public decimal? AssetUnitValueCostAmt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RepairPct", modeltype: FwDataTypes.Decimal)]
        public decimal? RepairPct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysRented", modeltype: FwDataTypes.Integer)]
        public int? DaysRented { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysOut", modeltype: FwDataTypes.Integer)]
        public int? DaysOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "EffectiveDate", modeltype: FwDataTypes.Date)]
        public string EffectiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "MaxDailyRate", modeltype: FwDataTypes.Decimal)]
        public decimal? MaxDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ExportDate", modeltype: FwDataTypes.Date)]
        public string ExportDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DaysOwned", modeltype: FwDataTypes.Integer)]
        public int? DaysOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ReturnOnAssetPrecalculatedRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DataWarehouseDatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DataWarehouseDatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    //select.AddWhere("(xxxxid ^> ')"); 
                    //addStringFilterToSelect("filter1fieldname", request.FiterValue1, select); 
                    //addStringFilterToSelect("filter2fieldname", request.FiterValue2, select); 
                    //addDateFilterToSelect("datefieldname1", request.DateValue1, select, "^>=", "dateparametername(optional)"); 
                    //addDateFilterToSelect("datefieldname2", request.DateValue2, select, "^<=", "dateparametername(optional)"); 
                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
                    select.AddOrderBy("Warehouse");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            //string[] totalFields = new string[] { "RentalTotal", "SalesTotal" };
            //dt.InsertSubTotalRows("GroupField1", "RowType", totalFields);
            //dt.InsertSubTotalRows("GroupField2", "RowType", totalFields);
            //dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryChangeReport
{
    [FwSqlTable("dbo.dwinventorychange(@fromdate, @todate)")]
    public class RentalInventoryChangeReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RowType", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "InventoryKey", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "WarehouseKey", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "DepartmentKey", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Department", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "CategoryKey", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "Category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "SubCategoryKey", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
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
        [FwSqlDataField(column: "ICodeDescription", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TrackedBy", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TransactionDate", modeltype: FwDataTypes.Date)]
        public string TransactionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TransactionType", modeltype: FwDataTypes.Text)]
        public string TransactionType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ChangeDescription", modeltype: FwDataTypes.Text)]
        public string ChangeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "AdditionQty", modeltype: FwDataTypes.Integer)]
        public int? AdditionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "UnitPriceAddition", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitPriceAddition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ExtendedAdditionPrice", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ExtendedAdditionPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "SubtractionQty", modeltype: FwDataTypes.Integer)]
        public int? SubtractionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "UnitPriceSubtraction", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitPriceSubtraction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ExtendedSubtractionPrice", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ExtendedSubtractionPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RunningTotalQty", modeltype: FwDataTypes.Integer)]
        public int? RunningTotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "RunningTotalValue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RunningTotalValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "TransactionSequence", modeltype: FwDataTypes.Integer)]
        public int? TransactionSequence { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryChangeReportRequest request)
        {
            useWithNoLock = false;
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
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);

                    if (!string.IsNullOrEmpty(request.WarehouseId))
                    {
                        select.AddWhereIn("and", "WarehouseKey", request.WarehouseId, false);
                    }
                    if (!string.IsNullOrEmpty(request.InventoryTypeId))
                    {
                        select.AddWhereIn("and", "DepartmentKey", request.InventoryTypeId, false);
                    }
                    if (!string.IsNullOrEmpty(request.CategoryId))
                    {
                        select.AddWhereIn("and", "CategoryKey", request.CategoryId, false);
                    }
                    if (!string.IsNullOrEmpty(request.SubCategoryId))
                    {
                        select.AddWhereIn("and", "SubCategoryKey", request.SubCategoryId, false);
                    }
                    if (!string.IsNullOrEmpty(request.InventoryId))
                    {
                        select.AddWhereIn("and", "InventoryKey", request.InventoryId, false);
                    }
                    select.AddWhereIn("and", "TrackedBy", request.TrackedBys.ToString(), false);
                    select.AddWhereIn("and", "ICodeRank", request.Ranks.ToString(), false);

                    select.AddOrderBy("Warehouse, Department, CategoryOrderBy, SubCategoryOrderBy, ICodeRank, ICode, ICodeDescription, TransactionDate, TransactionSequence");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "AdditionQuantity", "ExtendedAdditionPrice", "SubtractionQuantity", "ExtendedSubtractionPrice" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Description", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.SalesInventoryMasterReport
{
    [FwSqlTable("dbo.funcsalesmasterrpt(@revenuefromdate, @revenuetodate)")]
    public class SalesInventoryMasterReportLoader : AppDataLoadRecord
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
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public bool? Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averagecost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitAverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturerid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyallocated", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityAllocated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyonpo", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOnPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinaveragecost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? QuantityInAverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyindefaultcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? QuantityInDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinprice", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? QuantityInPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastsaledate", modeltype: FwDataTypes.Date)]
        public string LastSaleDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Revenue { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(SalesInventoryMasterReportRequest request)
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

                    select.AddParameter("@revenuefromdate", request.RevenueFromDate);
                    select.AddParameter("@revenuetodate", request.RevenueToDate);

                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("and", "rank", request.Ranks.ToString(), false);

                    if (request.RevenueFilterMode.Equals("LT"))
                    {
                        select.AddWhere("revenue < " + request.RevenueFilterAmount.ToString());
                    }
                    else if (request.RevenueFilterMode.Equals("GT"))
                    {
                        select.AddWhere("revenue > " + request.RevenueFilterAmount.ToString());
                    }

                    if (request.ExcludeZeroOwned.GetValueOrDefault(false))
                    {
                        select.AddWhere("qty <> 0");
                    }

                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "QuantityIn", "QuantityAllocated" , "QuantityInTransit", "QuantityOnPO", "QuantityInAverageCost", "QuantityInDefaultCost", "QuantityInPrice", "Revenue" };
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

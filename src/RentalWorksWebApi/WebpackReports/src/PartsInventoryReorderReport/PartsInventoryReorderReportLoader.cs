using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebLibrary;

namespace WebApi.Modules.Reports.PartsInventoryReorderReport
{
    public class PartsInventoryReorderReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyallocated", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityAllocated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyonpo", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOnPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderpoint", modeltype: FwDataTypes.Decimal)]
        public decimal? ReorderPoint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReorderQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpurchasedate", modeltype: FwDataTypes.Date)]
        public string LastPurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpurchaseqty", modeltype: FwDataTypes.Decimal)]
        public decimal? LastPurchaseQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpurchasevendor", modeltype: FwDataTypes.Text)]
        public string LastPurchaseVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastsaledate", modeltype: FwDataTypes.Date)]
        public string LastSaleDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(PartsInventoryReorderReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventoryreorderrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@availfor", SqlDbType.Text, ParameterDirection.Input, RwConstants.INVENTORY_AVAILABLE_FOR_PARTS);
                    qry.AddParameter("@mode", SqlDbType.Text, ParameterDirection.Input, request.ReorderPointMode);
                    qry.AddParameter("@includezeroreorderpoint", SqlDbType.Text, ParameterDirection.Input, request.IncludeZeroReorderPoint);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "Quantity", "QuantityAllocated", "QuantityStaged", "QuantityInTransit", "QuantityOnPo", "LastPurchaseQuantity" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi;
using System.Threading.Tasks;
using System.Data;
namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryValueReport
{
    public class RentalInventoryValueReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
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
        [FwSqlDataField(column: "categorysubcategory", modeltype: FwDataTypes.Text)]
        public string CategorySubCategory { get; set; }
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
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedate", modeltype: FwDataTypes.Date)]
        public string ChangeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changetype", modeltype: FwDataTypes.Text)]
        public string ChangeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedesc", modeltype: FwDataTypes.Text)]
        public string ChangeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "value", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ExtendedValue { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryValueReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventoryvaluerpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@availfor", SqlDbType.Text, ParameterDirection.Input, RwConstants.INVENTORY_AVAILABLE_FOR_RENT);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@includeowned", SqlDbType.Text, ParameterDirection.Input, request.IncludeOwned);
                    qry.AddParameter("@includeconsigned", SqlDbType.Text, ParameterDirection.Input, request.IncludeConsigned);
                    qry.AddParameter("@includezeroqty", SqlDbType.Text, ParameterDirection.Input, request.IncludeZeroQuantity);
                    qry.AddParameter("@groupbyicode", SqlDbType.Text, ParameterDirection.Input, request.GroupByICode);
                    qry.AddParameter("@serializedvaluebasedon", SqlDbType.Text, ParameterDirection.Input, request.SerializedValueBasedOn);
                    qry.AddParameter("@ranks", SqlDbType.Text, ParameterDirection.Input, request.Ranks.ToString());
                    qry.AddParameter("@trackedbys", SqlDbType.Text, ParameterDirection.Input, request.TrackedBys.ToString());
                    qry.AddParameter("@summary", SqlDbType.Text, ParameterDirection.Input, request.Summary);
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
                string[] totalFields = new string[] { "Quantity", "ExtendedValue" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertSubTotalRows("SubCategory", "RowType", totalFields);
                if (!request.Summary.GetValueOrDefault(false))
                {
                    dt.InsertSubTotalRows("ICode", "RowType", totalFields);
                }
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

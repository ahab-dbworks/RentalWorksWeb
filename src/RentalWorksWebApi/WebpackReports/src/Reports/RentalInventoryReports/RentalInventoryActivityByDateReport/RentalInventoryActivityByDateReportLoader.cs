using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryActivityByDateReport
{
    public class RentalInventoryActivityByDateReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownershiptypes", modeltype: FwDataTypes.Text)]
        public string OwnershipTypes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousecode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
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
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Integer)]
        public int? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Integer)]
        public int? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgurl", modeltype: FwDataTypes.Text)]
        public string ManufacturerUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? IsInactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classification", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nondiscountable", modeltype: FwDataTypes.Boolean)]
        public bool? IsNonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Boolean)]
        public bool? IsHazardousMaterial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqty", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyin", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtystaged", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyout", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyincontainer", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyinrepair", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyintransit", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyontruck", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryActivityByDateReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getrentalinventoryactivitybydateweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    if (request.FixedAssets.Equals(IncludeExcludeAll.IncludeOnly))
                    {
                        qry.AddParameter("@fixedassets", SqlDbType.Text, ParameterDirection.Input, RwConstants.INCLUDE);
                    }
                    else if (request.FixedAssets.Equals(IncludeExcludeAll.Exclude))
                    {
                        qry.AddParameter("@fixedassets", SqlDbType.Text, ParameterDirection.Input, RwConstants.EXCLUDE);
                    }
                    qry.AddParameter("@includeowned", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_OWNED));
                    qry.AddParameter("@includesubbed", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_SUBBED));
                    qry.AddParameter("@includeconsigned", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_CONSIGNED));
                    qry.AddParameter("@includeleased", SqlDbType.Text, ParameterDirection.Input, request.OwnershipTypes.ToString().Contains(RwConstants.INVENTORY_OWNERSHIP_LEASED));

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "QuantityOut", "QuantityIn" };
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

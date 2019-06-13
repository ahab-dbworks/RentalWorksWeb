using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryAvailabilityReport
{
    [FwSqlTable("availabilitymasterwhview")]
    public class RentalInventoryAvailabilityReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityByHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        public bool? NoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqty", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryAvailabilityReportRequest request)
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
                    //select.AddWhere("(xxxxid ^> ')"); 
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId); 
                    select.AddWhereIn("categoryid", request.CategoryId); 
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("class", request.Classifications.ToString());
                    select.AddWhereIn("trackedby", request.TrackedBys.ToString());
                    select.AddWhereIn("rank", request.Ranks.ToString());


                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, master");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "TotalQuantity" };  // need more total fields here
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                if (!request.IsSummary.GetValueOrDefault(false))
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

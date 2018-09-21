using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.RentalInventoryCatalogReport
{
    [FwSqlTable("inventorycatalogrptview")]
    public class RentalInventoryCatalogReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageid", modeltype: FwDataTypes.Text)]
        public string PackageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Boolean)]
        public bool? Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightlbs", modeltype: FwDataTypes.Integer)]
        public int? ShippingWeightPounds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightoz", modeltype: FwDataTypes.Integer)]
        public int? ShippingWeightOunces { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageprice", modeltype: FwDataTypes.Text)]
        public string PackagePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? SubCategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryTypeOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyownedweb", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightus", modeltype: FwDataTypes.Text)]
        public string WeightUS { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightmetric", modeltype: FwDataTypes.Text)]
        public string WeightMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryCatalogReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhere("(availfor = 'R')");
                    addStringFilterToSelect("warehouseid", request.WarehouseId, select);
                    addStringFilterToSelect("inventorydepartmentid", request.InventoryTypeId, select);
                    addStringFilterToSelect("categoryid", request.CategoryId, select);
                    addStringFilterToSelect("subcategoryid", request.SubCategoryId, select);
                    addStringFilterToSelect("masterid", request.InventoryId, select);

                    select.AddWhereIn("and", "class", request.Classifications.ToString(), false);
                    select.AddWhereIn("and", "trackedby", request.TrackedBys.ToString(), false);
                    select.AddWhereIn("and", "rank", request.Ranks.ToString(), false);

                    if (!request.IncludeZeroQuantity.GetValueOrDefault(false))
                    {
                        select.AddWhere("(qtyownedweb > 0)");
                    }

                    select.AddOrderBy("warehouse, departmentorderby, categoryorderby, subcategoryorderby, masterorderby, masterno");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            string[] totalFields = new string[] { "QuantityOwned" };
            dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
            dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
            dt.InsertSubTotalRows("Category", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}

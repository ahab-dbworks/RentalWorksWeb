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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string filterId = ""; 
            //DateTime filterDate = DateTime.MinValue; 
            //bool filterBoolean = false; 
            // 
            //if ((request != null) && (request.uniqueids != null)) 
            //{ 
            //    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids); 
            //    if (uniqueIds.ContainsKey("FilterId")) 
            //    { 
            //        filterId = uniqueIds["FilterId"].ToString(); 
            //    } 
            //    if (uniqueIds.ContainsKey("FilterDate")) 
            //    { 
            //        filterDate = FwConvert.ToDateTime(uniqueIds["FilterDate"].ToString()); 
            //    } 
            //    if (uniqueIds.ContainsKey("FilterBoolean")) 
            //    { 
            //        filterBoolean = FwConvert.ToBoolean(uniqueIds["FilterBoolean"].ToString()); 
            //    } 
            //} 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(availfor = 'R')");
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request); 
            addFilterToSelect("CategoryId", "categoryid", select, request);
            addFilterToSelect("SubCategoryId", "subcategoryid", select, request); 
            addFilterToSelect("RentalInventoryId", "masterid", select, request);
            //select.AddParameter("@filterid", filterId); 
            //select.AddParameter("@filterdate", filterDate); 
            //select.AddParameter("@filterboolean", filterBoolean); 


            /*

            dynamic classificationlist, trackedbylist, ranklist;

            classificationlist = request.parameters.classificationlist;
            trackedbylist = request.parameters.trackedbylist;
            ranklist = request.parameters.ranklist;

            select.AddWhereInFromCheckboxList("and", "m.class", classificationlist, GetClassificationList(), false);
            select.AddWhereInFromCheckboxList("and", "m.trackedby", trackedbylist, GetTrackedByList(), false);
            select.AddWhereInFromCheckboxList("and", "m.rank", ranklist, GetRankList(), false);
            select.AddOrderBy("m.warehouse, departmentorderby, categoryorderby, subcategoryorderby, m.masterorderby, m.masterno");

             */
        }
        //------------------------------------------------------------------------------------ 
    }
}

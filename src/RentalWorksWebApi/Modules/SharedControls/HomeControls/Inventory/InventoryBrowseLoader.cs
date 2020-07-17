using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Modules.HomeControls.Master;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.Inventory
{
    [FwSqlTable("inventoryview")]
    public class InventoryBrowseLoader : MasterLoader
    {
        //------------------------------------------------------------------------------------ 
        //public InventoryBrowseLoader()
        //{
        //    AfterBrowse += OnAfterBrowse;
        //}
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        //public string AvailFor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        //public string ICode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        //public string Description { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        //public string Category { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        //public string CategoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        //public string SubCategoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        //public string SubCategory { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        //public string Classification { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        //public string ClassificationDescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        //public string ClassificationColor
        //{
        //    get
        //    {
        //        return AppFunc.GetItemClassICodeColor(Classification);
        //    }
        //    set { }
        //}
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "q.qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.manifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.defaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.cost", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.hourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.dailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.weeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.monthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "ml.taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        //public bool? Inactive { get; set; }
        ////------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            OverrideFromClause = " from inventoryview [t] with (nolock) " +
                  " outer apply(select top 1 mw.manifestvalue, mw.aisleloc, mw.shelfloc, mw.hourlyrate, mw.dailyrate, mw.weeklyrate, mw.week2rate, mw.week3rate, mw.week4rate, mw.monthlyrate, mw.defaultcost, mw.hourlycost, mw.dailycost, mw.weeklycost, mw.monthlycost, mw.cost, mw.price, mw.replacementcost, mw.qcrequired" +
                  "              from  masterwh mw with(nolock)" +
                  "              where mw.masterid    = t.masterid" +
                  "              and   mw.warehouseid = @warehouseid) mw" +
                  " outer apply(select top 1 q.qty" +
                  "              from  masterwhqty q with(nolock) " +
                  "              where q.masterid    = t.masterid" +
                  "              and   q.warehouseid = @warehouseid) q" +
                  " outer apply(select top 1 ml.taxable" +
                  "              from  masterlocation ml with(nolock)" +
                  "              where ml.masterid   = t.masterid" +
                  "              and   ml.locationid = @locationid) ml";

            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();

            select.AddWhere("(availfor = @availfor)");
            select.AddParameter("@availfor", AvailFor);

            addFilterToSelect("TrackedBy", "trackedby", select, request);
            addFilterToSelect("Classification", "class", select, request);
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
            addFilterToSelect("CategoryId", "categoryid", select, request);
            addFilterToSelect("SubCategoryId", "subcategoryid", select, request);

            string containerId = GetUniqueIdAsString("ContainerId", request);
            if (!string.IsNullOrEmpty(containerId))
            {
                select.AddWhere("exists (select * from masteritem mi where mi.masterid = " + TableAlias + ".masterid and mi.orderid = @containerid)");
                select.AddParameter("@containerid", containerId);
            }

            string warehouseId = GetUniqueIdAsString("WarehouseId", request);
            if (string.IsNullOrEmpty(warehouseId))
            {
                warehouseId = "xxx~~xxx";
            }
            select.AddParameter("@warehouseid", warehouseId);

            string locationId = GetUniqueIdAsString("LocationId", request);
            if (string.IsNullOrEmpty(locationId))
            {
                locationId = "xxx~~xxx";
            }
            select.AddParameter("@locationid", locationId);

            AddActiveViewFieldToSelect("Classification", "class", select, request);
        }
        //------------------------------------------------------------------------------------ 
        //public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        //{
        //    if (e.DataTable != null)
        //    {
        //        FwJsonDataTable dt = e.DataTable;
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (List<object> row in dt.Rows)
        //            {
        //                string itemClass = row[dt.GetColumnNo("Classification")].ToString();
        //                row[dt.GetColumnNo("ClassificationColor")] = AppFunc.GetItemClassICodeColor(itemClass);
        //            }
        //        }
        //    }
        //}
        ////------------------------------------------------------------------------------------
    }
}
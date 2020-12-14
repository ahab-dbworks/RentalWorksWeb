using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Modules.HomeControls.Master;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using System.Text;

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
        [FwSqlDataField(calculatedColumnSql: "mwr.manifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.defaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.cost", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.hourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.dailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.weeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.monthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mwr.replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "lp.lastpurchaseprice", modeltype: FwDataTypes.Decimal)]
        public decimal? LastPurchasePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "mw.qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "ml.taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "curr.currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "curr.currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "curr.currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "curr.currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        //public bool? Inactive { get; set; }
        ////------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {

            string warehouseId = GetUniqueIdAsString("WarehouseId", request);
            string currencyId = GetUniqueIdAsString("CurrencyId", request) ?? "";
            string lastPurchaseVendorId = GetUniqueIdAsString("LastPurchaseVendorId", request) ?? "";

            bool foreignCurrency = false;
            if (string.IsNullOrEmpty(warehouseId))
            {
                warehouseId = "xxx~~xxx";
            }
            else
            {
                if (!string.IsNullOrEmpty(currencyId))
                {
                    WarehouseLogic w = new WarehouseLogic();
                    w.SetDependencies(AppConfig, UserSession);
                    w.WarehouseId = warehouseId;
                    if (w.LoadAsync<WarehouseLogic>().Result)
                    {
                        foreignCurrency = ((!string.IsNullOrEmpty(w.CurrencyId)) && (!currencyId.Equals(w.CurrencyId)));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" from inventoryview [t] with (nolock) ");
            sb.AppendLine(" outer apply(select top 1 mw.warehouseid, mw.aisleloc, mw.shelfloc, mw.qcrequired");
            sb.AppendLine("              from  masterwh mw with(nolock)");
            sb.AppendLine("              where mw.masterid    = t.masterid");
            sb.AppendLine("              and   mw.warehouseid = @warehouseid) mw");
            if (foreignCurrency)
            {
                //slower
                sb.AppendLine(" outer apply(select top 1 r.manifestvalue, r.hourlyrate, r.dailyrate, r.weeklyrate, r.week2rate, r.week3rate, r.week4rate, r.monthlyrate, r.defaultcost, r.hourlycost, r.dailycost, r.weeklycost, r.monthlycost, r.cost, r.price, r.replacementcost");
                sb.AppendLine("              from dbo.funcmasterwhratesweb(t.masterid, @warehouseid, @warehouseid, @currencyid) r) mwr");
            }
            else
            {
                //faster
                sb.AppendLine(" outer apply(select top 1 r.manifestvalue, r.hourlyrate, r.dailyrate, r.weeklyrate, r.week2rate, r.week3rate, r.week4rate, r.monthlyrate, r.defaultcost, r.hourlycost, r.dailycost, r.weeklycost, r.monthlycost, r.cost, r.price, r.replacementcost");
                sb.AppendLine("              from  masterwh r with(nolock)");
                sb.AppendLine("              where r.masterid    = t.masterid");
                sb.AppendLine("              and   r.warehouseid = @warehouseid) mwr");
            }
            sb.AppendLine(" outer apply(select top 1 q.qty");
            sb.AppendLine("              from  masterwhqty q with(nolock) ");
            sb.AppendLine("              where q.masterid    = t.masterid");
            sb.AppendLine("              and   q.warehouseid = @warehouseid) q");
            sb.AppendLine(" outer apply(select top 1 ml.taxable");
            sb.AppendLine("              from  masterlocation ml with(nolock)");
            sb.AppendLine("              where ml.masterid   = t.masterid");
            sb.AppendLine("              and   ml.locationid = @locationid) ml");
            sb.AppendLine(" outer apply(select top 1 currencyid = w.currencyid, currency = c.currency, currencycode = c.code, currencysymbol = c.currencysymbol");
            sb.AppendLine("              from  warehouse w with (nolock)");
            sb.AppendLine("                         join currency c on (w.currencyid = c.currencyid)");
            sb.AppendLine("              where mw.warehouseid = w.warehouseid) curr");

            if (string.IsNullOrEmpty(lastPurchaseVendorId))
            {
                sb.AppendLine(" outer apply(select lastpurchaseprice = 0.00) lp");
            }
            else {
                sb.AppendLine(" outer apply(select top 1 lastpurchaseprice = v.price * ((100.00 - v.discountpct) / 100.00)       ");
                sb.AppendLine("              from  lastpurchaseview v with (nolock)                                              ");
                sb.AppendLine("              where v.masterid    = t.masterid                                                    ");
                sb.AppendLine("              and   v.warehouseid = @warehouseid                                                  ");
                sb.AppendLine("             order by v.podate desc) lp                                                           ");
            }

            OverrideFromClause = sb.ToString();

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

            select.AddParameter("@warehouseid", warehouseId);
            select.AddParameter("@currencyid", currencyId);

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
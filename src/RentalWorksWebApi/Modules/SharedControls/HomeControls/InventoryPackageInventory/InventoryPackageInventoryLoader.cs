using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.InventoryPackageInventory
{
    [FwSqlTable("dbo.funcpackageitem(@packageid, @warehouseid)")]
    public class InventoryPackageInventoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public InventoryPackageInventoryLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryPackageInventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "linecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string LineColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultqty", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DefaultQuantityColor
        {
            get { return getDefaultQuantityColor(DefaultQuantity); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isoption", modeltype: FwDataTypes.Boolean)]
        public bool? IsOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "charge", modeltype: FwDataTypes.Boolean)]
        public bool? Charge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "required", modeltype: FwDataTypes.Boolean)]
        public bool? IsRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemtrackedby", modeltype: FwDataTypes.Text)]
        public string ItemTrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemcolor", modeltype: FwDataTypes.Integer)]
        public int? ItemColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isnestedcomplete", modeltype: FwDataTypes.Boolean)]
        public bool? IsNestedComplete { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iteminactive", modeltype: FwDataTypes.Text)]
        public string ItemInactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageitemclass", modeltype: FwDataTypes.Text)]
        public string PackageItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemnodiscount", modeltype: FwDataTypes.Boolean)]
        public bool? ItemNonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarymasterid", modeltype: FwDataTypes.Text)]
        public string PrimaryInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageid", modeltype: FwDataTypes.Text)]
        public string PackageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string packageId = "";
            string warehouseId = "xnonex";
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("PackageId"))
                {
                    packageId = uniqueIds["PackageId"].ToString();
                }
                if (uniqueIds.ContainsKey("WarehouseId"))
                {
                    warehouseId = uniqueIds["WarehouseId"].ToString();
                }
            }


            if (string.IsNullOrEmpty(packageId))
            {
                if (!string.IsNullOrEmpty(InventoryPackageInventoryId))
                {
                    packageId = AppFunc.GetStringDataAsync(AppConfig, "packageitem", "packageitemid", InventoryPackageInventoryId, "packageid").Result;
                }
            }


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddParameter("@packageid", packageId);
            select.AddParameter("@warehouseid", warehouseId);
        }
        //------------------------------------------------------------------------------------ 
        private string getDefaultQuantityColor(decimal? defaultQuantity)
        {
            string qColor = null;
            if ((defaultQuantity != null) && (defaultQuantity.GetValueOrDefault(0) > 0) && (defaultQuantity.GetValueOrDefault(1) < 1))
            {
                qColor = RwGlobals.PERCENTAGE_ITEM_COLOR;
            }
            return qColor;
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("DefaultQuantityColor")] = getDefaultQuantityColor(FwConvert.ToDecimal(row[dt.GetColumnNo("DefaultQuantity")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
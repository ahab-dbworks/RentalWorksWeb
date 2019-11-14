using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Modules.Home.Master;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.Home.Inventory
{
    [FwSqlTable("inventoryview")]
    public class InventoryBrowseLoader : AppDataLoadRecord
    {
        public InventoryBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
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
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ClassificationColor
        {
            get
            {
                return AppFunc.GetItemClassICodeColor(Classification);
            }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "(select q.qty from masterwhqty q where q.masterid = t.masterid and q.warehouseid = @warehouseid)", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

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

            AddActiveViewFieldToSelect("Classification", "class", select, request);
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
                        string itemClass = row[dt.GetColumnNo("Classification")].ToString();
                        row[dt.GetColumnNo("ClassificationColor")] = AppFunc.GetItemClassICodeColor(itemClass);
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
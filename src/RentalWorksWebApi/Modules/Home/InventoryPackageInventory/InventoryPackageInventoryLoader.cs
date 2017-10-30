using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.InventoryPackageInventory
{
    [FwSqlTable("packageitemview")]
    public class InventoryPackageInventoryLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryPackageInventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean)]
        public bool isPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultqty", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isoption", modeltype: FwDataTypes.Boolean)]
        public bool IsOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "charge", modeltype: FwDataTypes.Boolean)]
        public bool Charge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "required", modeltype: FwDataTypes.Boolean)]
        public bool Required { get; set; }
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
        public bool IsNestedComplete { get; set; }
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
        public bool ItemNonDiscountable { get; set; }
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
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("PackageId", "packageid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
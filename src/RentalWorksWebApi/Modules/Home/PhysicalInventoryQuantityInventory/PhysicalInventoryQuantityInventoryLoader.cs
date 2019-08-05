using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.PhysicalInventoryQuantityInventory
{
    [FwSqlTable("dbo.funcphysicalcountquantity(@physicalid)")]
    public class PhysicalInventoryQuantityInventoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer)]
        public int? PhysicalInventoryItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryTypeOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightunitid", modeltype: FwDataTypes.Text)]
        public string WeightUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthunitid", modeltype: FwDataTypes.Text)]
        public string LengthUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysession", modeltype: FwDataTypes.Integer)]
        public int? SessionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqty", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weight", modeltype: FwDataTypes.Integer)]
        public int? Weight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "length", modeltype: FwDataTypes.Integer)]
        public int? Length { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightunit", modeltype: FwDataTypes.Text)]
        public string WeightUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthunit", modeltype: FwDataTypes.Text)]
        public string LengthUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentspaceid", modeltype: FwDataTypes.Text)]
        public string CurrentSpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentspace", modeltype: FwDataTypes.Text)]
        public string CurrentSpace { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recount", modeltype: FwDataTypes.Boolean)]
        public bool? IsRecount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string physicalInventoryId = PhysicalInventoryId;
    
            if (string.IsNullOrEmpty(physicalInventoryId))
            {
                physicalInventoryId = GetUniqueIdAsString("PhysicalInventoryId", request);
            }
            if (string.IsNullOrEmpty(physicalInventoryId))
            {
                physicalInventoryId = "~xx~";
            }

            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 

            select.AddParameter("@physicalid", physicalInventoryId);

        }
        //------------------------------------------------------------------------------------ 
    }
}

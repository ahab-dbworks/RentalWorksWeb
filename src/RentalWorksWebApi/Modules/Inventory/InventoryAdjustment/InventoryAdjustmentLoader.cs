using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.InventoryAdjustment
{
    [FwSqlTable("inventoryadjustmentview")]
    public class InventoryAdjustmentLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string InventoryAdjustmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustdate", modeltype: FwDataTypes.Date)]
        public string AdjustmentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjreason", modeltype: FwDataTypes.Text)]
        public string InventoryAdjustmentReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldsadjusted", modeltype: FwDataTypes.Text)]
        public string FieldsAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OldQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newqty", modeltype: FwDataTypes.Decimal)]
        public decimal? NewQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invadjid", modeltype: FwDataTypes.Text)]
        public string InventoryAdjustmentReasonId { get; set; }
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
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reference", modeltype: FwDataTypes.Text)]
        public string Reference { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustmenttype", modeltype: FwDataTypes.Text)]
        public string AdjustmentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusttime", modeltype: FwDataTypes.Text)]
        public string AdjustmentTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averageadjustment", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averagecostadjustment", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCostAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moduserid", modeltype: FwDataTypes.Text)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onhandadjustment", modeltype: FwDataTypes.Decimal)]
        public decimal? OnHandAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

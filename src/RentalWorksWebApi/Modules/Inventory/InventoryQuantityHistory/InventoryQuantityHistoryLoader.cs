using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.InventoryQuantityHistory
{
    [FwSqlTable("masterwhqtyauditview")]
    public class InventoryQuantityHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? InventoryQuantityHistoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean)]
        //public bool? Internalchar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
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
        [FwSqlDataField(column: "trandatetime", modeltype: FwDataTypes.Date)]
        public string TransactionDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trantype", modeltype: FwDataTypes.Text)]
        public string TransactionType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtytype", modeltype: FwDataTypes.Text)]
        public string QuantityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OldQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtychange", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityChange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newqty", modeltype: FwDataTypes.Decimal)]
        public decimal? NewQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldavgcost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldAverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newavgcost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewAverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid3", modeltype: FwDataTypes.Text)]
        public string UniqueId3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid4", modeltype: FwDataTypes.Integer)]
        public int? UniqueId4 { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryId", "masterid", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("QuantityType", "qtytype", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}

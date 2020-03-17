using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Inventory.Repair
{
    [FwSqlTable("repairwebview")]
    public class RepairBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string RepairId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdate", modeltype: FwDataTypes.Date)]
        public string RepairDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfordisp", modeltype: FwDataTypes.Text)]
        public string AvailForDisplay { get; set; }
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
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedeal", modeltype: FwDataTypes.Text)]
        public string DamageDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prioritydesc", modeltype: FwDataTypes.Text)]
        public string PriorityDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releasedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReleasedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairnocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RepairNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prioritycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PriorityColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BarCodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedealcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DamageDealColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "repairitemstatus", modeltype: FwDataTypes.Text)]
        public string RepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingrepair", modeltype: FwDataTypes.Boolean)]
        public bool? PendingRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepair", modeltype: FwDataTypes.Boolean)]
        public bool? OutsideRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderno", modeltype: FwDataTypes.Text)]
        public string DamageOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderdesc", modeltype: FwDataTypes.Text)]
        public string DamageOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedby", modeltype: FwDataTypes.Text)]
        public string DamageScannedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractno", modeltype: FwDataTypes.Text)]
        public string DamageContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractdate", modeltype: FwDataTypes.Date)]
        public string DamageContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean)]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabletext", modeltype: FwDataTypes.Text)]
        public string BillableDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notbilled", modeltype: FwDataTypes.Boolean)]
        public bool? NotBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Text)]
        public string Priority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtype", modeltype: FwDataTypes.Text)]
        public string RepairType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popending", modeltype: FwDataTypes.Boolean)]
        public bool? PoPending { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimateby", modeltype: FwDataTypes.Text)]
        public string EstimateBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedate", modeltype: FwDataTypes.Date)]
        public string EstimateDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeby", modeltype: FwDataTypes.Text)]
        public string CompleteBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completedate", modeltype: FwDataTypes.Date)]
        public string CompleteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("ItemId", "rentalitemid", select, request);
            addFilterToSelect("OrderId", "damageorderid", select, request);

            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.Repair
{
    [FwSqlTable("repairview")]
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
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("ItemId", "rentalitemid", select, request);

            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}

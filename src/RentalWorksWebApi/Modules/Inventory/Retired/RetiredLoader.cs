using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.Retired
{
    [FwSqlTable("retiredwebview")]
    public class RetiredLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string RetiredId { get; set; }
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
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
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
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agreementno", modeltype: FwDataTypes.Text)]
        public string AgreementNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retireddate", modeltype: FwDataTypes.Date)]
        public string RetiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredby", modeltype: FwDataTypes.Text)]
        public string RetiredBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text)]
        public string RetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredbyid", modeltype: FwDataTypes.Text)]
        public string RetiredByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retirednotes", modeltype: FwDataTypes.Text)]
        public string RetiredNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredqty", modeltype: FwDataTypes.Decimal)]
        public decimal? RetiredQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredqty", modeltype: FwDataTypes.Decimal)]
        public decimal? UnretiredQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostorderid", modeltype: FwDataTypes.Text)]
        public string LostOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostorderno", modeltype: FwDataTypes.Text)]
        public string LostOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soldtoorderid", modeltype: FwDataTypes.Text)]
        public string SoldToOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soldtomasteritemid", modeltype: FwDataTypes.Text)]
        public string SoldToOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soldorderno", modeltype: FwDataTypes.Text)]
        public string SoldToOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
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
            addFilterToSelect("ItemId", "rentalitemid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}

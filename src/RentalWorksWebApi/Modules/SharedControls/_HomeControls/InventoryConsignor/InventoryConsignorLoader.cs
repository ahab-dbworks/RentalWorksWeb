using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.InventoryConsignor
{
    [FwSqlTable("masterconsignoragreementview")]
    public class InventoryConsignorLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agreementno", modeltype: FwDataTypes.Text)]
        public string AgreementNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agreementdesc", modeltype: FwDataTypes.Text)]
        public string AgreementDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "treatconsignedqtyasowned", modeltype: FwDataTypes.Boolean)]
        public bool? TreatConsignedQtyAsOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyconsigned", modeltype: FwDataTypes.Integer)]
        public int? QtyConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorpercent", modeltype: FwDataTypes.Integer)]
        public int? ConsignorPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "housepercent", modeltype: FwDataTypes.Integer)]
        public int? HousePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatrate", modeltype: FwDataTypes.Boolean)]
        public bool? FlatRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatrateamount", modeltype: FwDataTypes.Decimal)]
        public decimal? FlatRateAmount { get; set; }
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
        [FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text)]
        public string InventoryStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string InventoryStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        public string InventoryStatusType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Text)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer)]
        public int? Color { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
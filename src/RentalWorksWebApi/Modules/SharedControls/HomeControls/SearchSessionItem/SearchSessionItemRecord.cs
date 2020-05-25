using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.SearchSessionItem
{
    [FwSqlTable("tmpsearchsession")]
    public class SearchSessionItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? SearchSessionItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 4)]
        public decimal? RepairQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PackageItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weight", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Weight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "length", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Length { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorfeeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorFeeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorPurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 18, scale: 8)]
        public decimal? ActivityQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remainingqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 18, scale: 8)]
        public decimal? RemainingQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grandparentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GrandParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 18, scale: 8)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

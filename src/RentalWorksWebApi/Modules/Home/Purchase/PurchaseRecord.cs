using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.Purchase
{
    [FwSqlTable("purchase")]
    public class PurchaseRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PurchaseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchasePurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? PhysicalItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 3)]
        public decimal? PurchaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendpartno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string VendorPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "received", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 11)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasevendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepurchasedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string LeasePurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeasePurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeaseNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepurchaseamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 3)]
        public decimal? LeasePurchaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasereceivedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string LeaseReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string LeaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? LeaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasepartno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string LeasePartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasecontact", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string LeaseContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leasedocid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeaseDocumentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasenotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string PurchaseNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsidepono", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 25)]
        public string OutsidePurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseorderedpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeaseOrderedPurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "leaseorderedvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LeaseOrderedVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReceiveContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchasePurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryreceiptid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventoryreceiptitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryReceiptItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchamtwithtax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 3)]
        public decimal? PurchaseAmountWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origpurchaseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OriginalPurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryAdjustmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.Retired
{
    [FwSqlTable("retired")]
    public class RetiredRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string RetiredId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reprentalitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReprentalitemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? PhysicalInventoryItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soldtoorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SoldToOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donateorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DonateorderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LostOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LostvendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tradedpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TradedpoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string StolenvendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tradedvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TradedvendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtoorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BilledtoorderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostamountpaid", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? LostamountpaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostpolice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string Lostpolice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostfiled", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Lostfiled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostadjuster", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Lostadjuster { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostclaimno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Lostclaimno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lostreplacedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Lostreplacedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loststatus", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Loststatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tradedreplacedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Tradedreplacedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tradeamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? Tradeamt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donatedvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? Donatedvalue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retirednotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string RetiredNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenfiled", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Stolenfiled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenadjuster", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Stolenadjuster { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenstatus", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Stolenstatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenreplacedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Stolenreplacedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? Stolenamount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RetiredByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retireddate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string RetiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soldtomasteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SoldToOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenpolice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Stolenpolice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stolenclaimno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Stolenclaimno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtomasteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BilledtomasteritemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConsignorpoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? UnretiredQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RepmasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AdjustmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

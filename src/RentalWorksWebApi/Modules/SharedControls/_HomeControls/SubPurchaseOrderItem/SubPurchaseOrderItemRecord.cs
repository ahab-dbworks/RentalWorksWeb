using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.SubPurchaseOrderItem
{
    [FwSqlTable("tmppoitem")]
    public class SubPurchaseOrderItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, isPrimaryKey: true, sqltype: "char", maxlength: 8)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true, sqltype: "char", maxlength: 8)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? DealRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? DealDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? DealDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? VendorRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? VendorDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? VendorDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

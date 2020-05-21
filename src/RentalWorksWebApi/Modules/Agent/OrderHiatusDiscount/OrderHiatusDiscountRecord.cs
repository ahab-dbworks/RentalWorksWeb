using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Agent.OrderHiatusDiscount
{
    [FwSqlTable("orderdiscount")]
    public class OrderHiatusDiscountRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdiscountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderHiatusDiscountId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changescheduleflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DoChangeSchedule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorateflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsProrated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datefrom", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dateto", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.InvoiceItem
{
    [FwSqlTable("invoiceitem")]
    public class InvoiceItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InvoiceItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 2)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 4)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? Adjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string RatemasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "split", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Split { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterin", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? MeterIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterout", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? MeterOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusted", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenuepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 8)]
        public decimal? Revenuepct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastervalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? InventoryValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageprice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string PackagePrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenuebase", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? RevenueBase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrevenuebaseadj", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? AdjustedQuikPayRevenueBase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "days", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 3)]
        public decimal? Days { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeks", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 19, scale: 6)]
        public decimal? Weeks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? HoursOvertime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? HoursDoubletime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costbase", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Costbase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrevenuebase", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 18, scale: 6)]
        public decimal? QuikPayRevenueBase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityadjustment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ActivityAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromtime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string FromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string ToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewactualhours", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CrewActualHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

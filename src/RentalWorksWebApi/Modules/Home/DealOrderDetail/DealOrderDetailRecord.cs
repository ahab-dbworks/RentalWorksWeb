using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.DealOrderDetail
{
    [FwSqlTable("dealorderdetail")]
    public class DealOrderDetailRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lockbillingdates", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? LockBillingDates { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "delaybillingsearchuntil", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string DelayBillingSearchUntil { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prepfeesinrentalrate", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? IncludePrepFeesInRentalRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "differentbilladdress", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? BillToAddressDifferentFromIssuedToAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicediscountreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DiscountReasonId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "requirecontactconfirmation", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? RequireContactConfirmation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "roundtriprentals", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? RoundTripRentals { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

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
        //[FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? MaximumCumulativeDiscount { get; set; }
        ////------------------------------------------------------------------------------------
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
        [FwSqlDataField(column: "coverletterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string CoverLetterId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrepresentativecontactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string SalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string MarketSegmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegmentjobid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string MarketSegmentJobId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poccprimarywhenemailbackup", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? CcPrimaryApproverWhenEmailingBackupApprover { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.HomeControls.DealOrderDetail
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
        [FwSqlDataField(column: "billperioddatesbytype", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? SpecifyBillingDatesByType { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "billperiodstartrental", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string RentalBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendrental", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string RentalBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartlabor", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string LaborBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendlabor", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string LaborBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartmisc", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string MiscellaneousBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendmisc", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string MiscellaneousBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartspace", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string FacilitiesBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendspace", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string FacilitiesBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartvehicle", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string VehicleBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendvehicle", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string VehicleBillingEndDate { get; set; }
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
        public string OutsideSalesRepresentativeId { get; set; }
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
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "receivetime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string ReceiveTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isreturntransferorder", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? IsReturnTransferOrder { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "manualsort", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? IsManualSort { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "misccomplete", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? MiscellaneousIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "submisccomplete", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? SubMiscellaneousIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborcomplete", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? LaborIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sublaborcomplete", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? SubLaborIsComplete { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

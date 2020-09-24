using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Administrator.Control
{
    [FwSqlTable("syscontrol")]
    public class SysControlRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ControlId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentrestockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? Rentrestockpercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultunitid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fymonth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Fymonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invmask", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Invmask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultDealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesrestockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Salesrestockpercent { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultCustomerStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hintseconds", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Hintseconds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalinvadjid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalinvadjId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "logmessages", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Logmessages { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chkindeptfromuser", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Chkindeptfromuser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwserver", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Dwserver { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwdatabase", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Dwdatabase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "demomode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Demomode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignmasterno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Userassignmasterno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Masterno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderorderby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Orderorderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyinvretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhyinvretiredreasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedicoderetiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ChangedicoderetiredreasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sharedeals", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShareDealsAcrossOfficeLocations { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowdecreaseorderwhenstaged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowdecreaseorderwhenstaged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availprogressmeter", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availprogressmeter { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyinvcost", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Phyinvcost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availprocessrows", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Availprocessrows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loginventorychanges", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Loginventorychanges { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkchargesplitsonapproval", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Checkchargesplitsonapproval { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crfpaytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CrfpaytypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Orbitsapchgcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapfinallddetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapfinallddetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaplabormiscdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsaplabormiscdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaprentaldetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsaprentaldetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaprentalsaledetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsaprentalsaledetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapsalesdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapsalesdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderheaderexportfilename", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 100)]
        public string Orderheaderexportfilename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentsforfuturemonths", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Paymentsforfuturemonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "univcrfno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Univcrfno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "univexporttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string Univexporttype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delaygls", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Delaygls { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicelocationfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Invoicelocationfrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicenofrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string Invoicenofrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultbillperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultDealBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordefaultbillperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendordefaultbillperiodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentsforfuturedates", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Paymentsforfuturedates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mapsystem", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Mapsystem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyclosewithoutadj", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Phyclosewithoutadj { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonrecurbillperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultNonRecurringBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "revenueforcompletes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Revenueforcompletes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "revenueforkits", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Revenueforkits { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "completerevenuebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        //public string Completerevenuebasedon { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "kitrevenuebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        //public string Kitrevenuebasedon { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setconditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SetconditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "propsconditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PropsconditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultspacedw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? Defaultspacedw { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocrosswhcheckin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Nocrosswhcheckin { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "markupreplacementcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Markupreplacementcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "replacementcostmarkuppct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Replacementcostmarkuppct { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crosslocationadds", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Crosslocationadds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderhistoryyears", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Orderhistoryyears { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icodeprefix", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Icodeprefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "synccustomercreditstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Synccustomercreditstatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "syncdealcreditstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Syncdealcreditstatus { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copyinactiveitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Copyinactiveitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availconflictlogdays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Availconflictlogdays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowdeletebatchedreceipt", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowDeleteExportedReceipts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "posecondapprovalwithoutfirst", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Posecondapprovalwithoutfirst { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "codamountincheckoutprompt", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Codamountincheckoutprompt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefaultpaytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultCustomerPaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availhourlydays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Availhourlydays { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "treatconsignedqtyasowned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Treatconsignedqtyasowned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomercredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultusecustomercredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomerinsurance", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultusecustomerinsurance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomertax", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultusecustomertax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowstoautoupdatetotal", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Rowstoautoupdatetotal { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "migrateuseeststart", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Migrateuseeststart { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requirecontactconfirmation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Requirecontactconfirmation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "enableunconfirmationworkflow", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Enableunconfirmationworkflow { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowamountchanged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowamountchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowamountchangeddiff", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 8, scale: 2)]
        //public decimal? Workflowamountchangeddiff { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowequipchanged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowequipchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowzerodallor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowzerodallor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowloadpickchanged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowloadpickchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowbillingperiodchanged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowbillingperiodchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disableconfirmation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disableconfirmation { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flowsheetformat", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Flowsheetformat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowtransfertosamewh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Allowtransfertosamewh { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalculatepackages", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Availcalculatepackages { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowapprovalamountchanged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Workflowapprovalamountchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availpromptconflicts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Availpromptconflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "useorderitem", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Useorderitem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restartweeklytiers", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Restartweeklytiers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availonquotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Availonquotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "saveinvoiceprintsettings", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Saveinvoiceprintsettings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shareordergroups", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Shareordergroups { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poccprimarywhenemailbackup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Poccprimarywhenemailbackup { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Noorderscustomerstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string NoorderscustomerstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatusdays", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        //public int? Noorderscustomerstatusdays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availexcludeconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availexcludeconsigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availreservationhistorydays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Availreservationhistorydays { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availreserveconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availreserveconsigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultorderdesc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultorderdesc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableoldavaildetail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enableoldavaildetail { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "previewinvoicetitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        //public string Previewinvoicetitle { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "estimateinvoicetitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        //public string Estimateinvoicetitle { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noinvoicedays", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Noinvoicedays { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicecustomerstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Noinvoicecustomerstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicecustomerstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string NoinvoicecustomerstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicedealstatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Noinvoicedealstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicedealstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string NoinvoicedealstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchangecolor", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Pendingexchangecolor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repsignaturecaption", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string Repsignaturecaption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsonorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Warehousebuttonsonorder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsonpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Warehousebuttonsonpo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsontransfer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Warehousebuttonsontransfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsontruck", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Warehousebuttonsontruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteraccrualthrough", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string Pomasteraccrualthrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablelocationfilter", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enablelocationfilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalshowcounted", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Physicalshowcounted { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfhidepayment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderconfhidepayment { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfdisablepo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderconfdisablepo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfdisablenote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderconfdisablenote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfrequiredeclinereason", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderconfrequiredeclinereason { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcacheallwarehouses", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Availcacheallwarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalcunowneditems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Availcalcunowneditems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consfeesonflatpo", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Consfeesonflatpo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showonlinetracking", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Showonlinetracking { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        public decimal? Defaultitemdiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscounthiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultitemdiscounthiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscountseparate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultitemdiscountseparate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultproratefacilities", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultproratefacilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultallowbillschedoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultallowbillschedoverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultallowrebatecredits", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultallowrebatecredits { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultorderoverridebillsched", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultorderoverridebillsched { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemsinrooms", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Itemsinrooms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscaldaysweekenddefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Fiscaldaysweekenddefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscaldaysholidaydefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Fiscaldaysholidaydefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireoriginalshow", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Requireoriginalshow { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavailcolor", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Noavailcolor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manuallyretireusedsales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Manuallyretireusedsales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpopulatepomasteraccruals", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string Lastpopulatepomasteraccruals { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "facilitytypeincurrentlocation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Facilitytypeincurrentlocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsaptype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultsaptype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsapcostobject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Defaultsapcostobject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsapaccountno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Defaultsapaccountno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcustomerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultcustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomerdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Defaultusecustomerdiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefdiscounttemplateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomerdefdiscounttemplateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowconsignchangeicode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Allowconsignchangeicode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteordersearchbyaka", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Quoteordersearchbyaka { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "candeletecontactwhenorderedby", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Candeletecontactwhenorderedby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refreshquikactivity", modeltype: FwDataTypes.Integer, sqltype: "tinyint")]
        public int? Refreshquikactivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i35includepoprefix", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? I35includepoprefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableorderunit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enableorderunit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "makequotedealanddescriptionunique", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Makequotedealanddescriptionunique { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablecustomeractivityrestrictions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enablecustomeractivityrestrictions { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatemanifestvalue", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updatemanifestvalue { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivemisclabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Receivemisclabor { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availenableqcdelay", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availenableqcdelay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayexcludeweekend", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availqcdelayexcludeweekend { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayexcludeholiday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availqcdelayexcludeholiday { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayindefinite", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availqcdelayindefinite { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i035summarizesr", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? I035summarizesr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultpaytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealdefaultpaytypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showfixedassetregister", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Showfixedassetregister { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "editldcheckcurrency", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Editldcheckcurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "editrepaircheckcurrency", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Editrepaircheckcurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includenonbillable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Includenonbillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignedvendorno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsVendorNumberAssignedByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? LastVendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "capslock", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CapsLock { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documentbarcodestyle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string DocumentBarCodeStyle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrank", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public string DefaultRank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enable3weekpricing", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enable3WeekPricing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdealporeq", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DefaultDealPoRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdealpotype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public string DefaultDealPoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckoutretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesCheckOutRetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckinunretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesCheckInUnretiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcreditstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultCreditStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablereceipts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? EnableReceipts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrentalsaleretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultRentalSaleRetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultlossdamageretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultLossAndDamageRetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "betaupdates", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? EnableBetaUpdates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qaupdates", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? EnableQaUpdates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationstartsnextmonth", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablepayments", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? EnablePayments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowdeletebatchedpayment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowDeleteExportedPayments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assetcostcalculation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string RentalQuantityInventoryValueMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescostcalculation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string SalesQuantityInventoryValueMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partscostcalculation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string PartsQuantityInventoryValueMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
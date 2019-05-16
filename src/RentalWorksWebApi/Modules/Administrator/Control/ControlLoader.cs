using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Administrator.Control
{
    [FwSqlTable("controlview")]
    public class ControlLoader : ControlBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxrows", modeltype: FwDataTypes.Integer)]
        public int? Maxrows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imagepath", modeltype: FwDataTypes.Text)]
        public string Imagepath { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dbversion", modeltype: FwDataTypes.Text)]
        public string DatabaseVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "build", modeltype: FwDataTypes.Text)]
        public string Build { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignedvendorno", modeltype: FwDataTypes.Boolean)]
        public bool? Userassignedvendorno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Integer)]
        public int? Vendorno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentrestockpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? Rentrestockpercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultunitid", modeltype: FwDataTypes.Text)]
        public string DefaultUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultunit", modeltype: FwDataTypes.Text)]
        public string DefaultUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fymonth", modeltype: FwDataTypes.Integer)]
        public int? Fymonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invmask", modeltype: FwDataTypes.Text)]
        public string ICodeMask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
        public string DefaultDealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DefaultDealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesrestockpercent", modeltype: FwDataTypes.Decimal)]
        //public decimal? Salesrestockpercent { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hintseconds", modeltype: FwDataTypes.Integer)]
        public int? Hintseconds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalinvadjid", modeltype: FwDataTypes.Text)]
        public string PhysicalinvadjId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "logmessages", modeltype: FwDataTypes.Boolean)]
        public bool? Logmessages { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chkindeptfromuser", modeltype: FwDataTypes.Boolean)]
        //public bool? Chkindeptfromuser { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwserver", modeltype: FwDataTypes.Text)]
        public string Dwserver { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dwdatabase", modeltype: FwDataTypes.Text)]
        public string Dwdatabase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "demomode", modeltype: FwDataTypes.Boolean)]
        public bool? Demomode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? UserAssignedICodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Integer)]
        public int? NextICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderorderby", modeltype: FwDataTypes.Text)]
        public string Orderorderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyinvretiredreasonid", modeltype: FwDataTypes.Text)]
        public string PhyinvretiredreasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedicoderetiredreasonid", modeltype: FwDataTypes.Text)]
        public string ChangedicoderetiredreasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sharedeals", modeltype: FwDataTypes.Boolean)]
        public bool? Sharedeals { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowdecreaseorderwhenstaged", modeltype: FwDataTypes.Boolean)]
        //public bool? Allowdecreaseorderwhenstaged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availprogressmeter", modeltype: FwDataTypes.Boolean)]
        //public bool? Availprogressmeter { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyinvcost", modeltype: FwDataTypes.Text)]
        public string Phyinvcost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availprocessrows", modeltype: FwDataTypes.Integer)]
        public int? Availprocessrows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loginventorychanges", modeltype: FwDataTypes.Boolean)]
        public bool? Loginventorychanges { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkchargesplitsonapproval", modeltype: FwDataTypes.Boolean)]
        public bool? Checkchargesplitsonapproval { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crfpaytypeid", modeltype: FwDataTypes.Text)]
        public string CrfpaytypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgcode", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapfinallddetail", modeltype: FwDataTypes.Text)]
        public string Orbitsapfinallddetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaplabormiscdetail", modeltype: FwDataTypes.Text)]
        public string Orbitsaplabormiscdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaprentaldetail", modeltype: FwDataTypes.Text)]
        public string Orbitsaprentaldetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaprentalsaledetail", modeltype: FwDataTypes.Text)]
        public string Orbitsaprentalsaledetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapsalesdetail", modeltype: FwDataTypes.Text)]
        public string Orbitsapsalesdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderheaderexportfilename", modeltype: FwDataTypes.Text)]
        public string Orderheaderexportfilename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentsforfuturemonths", modeltype: FwDataTypes.Boolean)]
        public bool? Paymentsforfuturemonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "univcrfno", modeltype: FwDataTypes.Integer)]
        public int? Univcrfno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "univexporttype", modeltype: FwDataTypes.Text)]
        public string Univexporttype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delaygls", modeltype: FwDataTypes.Boolean)]
        public bool? Delaygls { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicelocationfrom", modeltype: FwDataTypes.Text)]
        public string Invoicelocationfrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicenofrom", modeltype: FwDataTypes.Text)]
        public string Invoicenofrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultbillperiodid", modeltype: FwDataTypes.Text)]
        public string DefaultDealBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultbillperiod", modeltype: FwDataTypes.Text)]
        public string DefaultDealBillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordefaultbillperiodid", modeltype: FwDataTypes.Text)]
        public string VendordefaultbillperiodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentsforfuturedates", modeltype: FwDataTypes.Text)]
        public string Paymentsforfuturedates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mapsystem", modeltype: FwDataTypes.Text)]
        public string Mapsystem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phyclosewithoutadj", modeltype: FwDataTypes.Boolean)]
        public bool? Phyclosewithoutadj { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonrecurbillperiodid", modeltype: FwDataTypes.Text)]
        public string NonrecurbillperiodId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "revenueforcompletes", modeltype: FwDataTypes.Boolean)]
        //public bool? Revenueforcompletes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "revenueforkits", modeltype: FwDataTypes.Boolean)]
        //public bool? Revenueforkits { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "completerevenuebasedon", modeltype: FwDataTypes.Text)]
        //public string Completerevenuebasedon { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "kitrevenuebasedon", modeltype: FwDataTypes.Text)]
        //public string Kitrevenuebasedon { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setconditionid", modeltype: FwDataTypes.Text)]
        public string SetconditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "propsconditionid", modeltype: FwDataTypes.Text)]
        public string PropsconditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultspacedw", modeltype: FwDataTypes.Decimal)]
        public decimal? Defaultspacedw { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocrosswhcheckin", modeltype: FwDataTypes.Boolean)]
        public bool? Nocrosswhcheckin { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "markupreplacementcost", modeltype: FwDataTypes.Boolean)]
        //public bool? Markupreplacementcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "replacementcostmarkuppct", modeltype: FwDataTypes.Decimal)]
        //public decimal? Replacementcostmarkuppct { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crosslocationadds", modeltype: FwDataTypes.Boolean)]
        public bool? Crosslocationadds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderhistoryyears", modeltype: FwDataTypes.Integer)]
        public int? Orderhistoryyears { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icodeprefix", modeltype: FwDataTypes.Text)]
        public string ICodePrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "synccustomercreditstatus", modeltype: FwDataTypes.Boolean)]
        public bool? Synccustomercreditstatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "syncdealcreditstatus", modeltype: FwDataTypes.Boolean)]
        public bool? Syncdealcreditstatus { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copyinactiveitems", modeltype: FwDataTypes.Boolean)]
        //public bool? Copyinactiveitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availconflictlogdays", modeltype: FwDataTypes.Integer)]
        public int? Availconflictlogdays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowdeletebatchedreceipt", modeltype: FwDataTypes.Boolean)]
        public bool? Allowdeletebatchedreceipt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "posecondapprovalwithoutfirst", modeltype: FwDataTypes.Boolean)]
        public bool? Posecondapprovalwithoutfirst { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "codamountincheckoutprompt", modeltype: FwDataTypes.Boolean)]
        public bool? Codamountincheckoutprompt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefaultpaytermsid", modeltype: FwDataTypes.Text)]
        public string CustomerdefaultpaytermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availhourlydays", modeltype: FwDataTypes.Integer)]
        //public int? Availhourlydays { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "treatconsignedqtyasowned", modeltype: FwDataTypes.Boolean)]
        public bool? Treatconsignedqtyasowned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomercredit", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultusecustomercredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomerinsurance", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultusecustomerinsurance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomertax", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultusecustomertax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowstoautoupdatetotal", modeltype: FwDataTypes.Integer)]
        public int? Rowstoautoupdatetotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "migrateuseeststart", modeltype: FwDataTypes.Boolean)]
        public bool? Migrateuseeststart { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requirecontactconfirmation", modeltype: FwDataTypes.Boolean)]
        //public bool? Requirecontactconfirmation { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "enableunconfirmationworkflow", modeltype: FwDataTypes.Boolean)]
        //public bool? Enableunconfirmationworkflow { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowamountchanged", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowamountchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowamountchangeddiff", modeltype: FwDataTypes.Decimal)]
        //public decimal? Workflowamountchangeddiff { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowequipchanged", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowequipchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowzerodallor", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowzerodallor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowloadpickchanged", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowloadpickchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowbillingperiodchanged", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowbillingperiodchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disableconfirmation", modeltype: FwDataTypes.Boolean)]
        //public bool? Disableconfirmation { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flowsheetformat", modeltype: FwDataTypes.Text)]
        public string Flowsheetformat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowtransfertosamewh", modeltype: FwDataTypes.Boolean)]
        public bool? Allowtransfertosamewh { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalculatepackages", modeltype: FwDataTypes.Boolean)]
        public bool? Availcalculatepackages { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "workflowapprovalamountchanged", modeltype: FwDataTypes.Boolean)]
        //public bool? Workflowapprovalamountchanged { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availpromptconflicts", modeltype: FwDataTypes.Boolean)]
        public bool? Availpromptconflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "useorderitem", modeltype: FwDataTypes.Boolean)]
        public bool? Useorderitem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restartweeklytiers", modeltype: FwDataTypes.Boolean)]
        public bool? Restartweeklytiers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availonquotes", modeltype: FwDataTypes.Boolean)]
        public bool? Availonquotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "saveinvoiceprintsettings", modeltype: FwDataTypes.Boolean)]
        public bool? Saveinvoiceprintsettings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shareordergroups", modeltype: FwDataTypes.Boolean)]
        public bool? Shareordergroups { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poccprimarywhenemailbackup", modeltype: FwDataTypes.Boolean)]
        public bool? Poccprimarywhenemailbackup { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatus", modeltype: FwDataTypes.Boolean)]
        //public bool? Noorderscustomerstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatusid", modeltype: FwDataTypes.Text)]
        //public string NoorderscustomerstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noorderscustomerstatusdays", modeltype: FwDataTypes.Integer)]
        //public int? Noorderscustomerstatusdays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availexcludeconsigned", modeltype: FwDataTypes.Boolean)]
        //public bool? Availexcludeconsigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availreservationhistorydays", modeltype: FwDataTypes.Integer)]
        public int? Availreservationhistorydays { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availreserveconsigned", modeltype: FwDataTypes.Boolean)]
        //public bool? Availreserveconsigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultorderdesc", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultorderdesc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableoldavaildetail", modeltype: FwDataTypes.Boolean)]
        public bool? Enableoldavaildetail { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "previewinvoicetitle", modeltype: FwDataTypes.Text)]
        //public string Previewinvoicetitle { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "estimateinvoicetitle", modeltype: FwDataTypes.Text)]
        //public string Estimateinvoicetitle { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicedays", modeltype: FwDataTypes.Integer)]
        //public int? Noinvoicedays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicecustomerstatus", modeltype: FwDataTypes.Boolean)]
        //public bool? Noinvoicecustomerstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicecustomerstatusid", modeltype: FwDataTypes.Text)]
        //public string NoinvoicecustomerstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicedealstatus", modeltype: FwDataTypes.Boolean)]
        //public bool? Noinvoicedealstatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinvoicedealstatusid", modeltype: FwDataTypes.Text)]
        //public string NoinvoicedealstatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchangecolor", modeltype: FwDataTypes.Integer)]
        public int? Pendingexchangecolor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repsignaturecaption", modeltype: FwDataTypes.Text)]
        public string Repsignaturecaption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsonorder", modeltype: FwDataTypes.Boolean)]
        public bool? Warehousebuttonsonorder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsonpo", modeltype: FwDataTypes.Boolean)]
        public bool? Warehousebuttonsonpo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsontransfer", modeltype: FwDataTypes.Boolean)]
        public bool? Warehousebuttonsontransfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousebuttonsontruck", modeltype: FwDataTypes.Boolean)]
        public bool? Warehousebuttonsontruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteraccrualthrough", modeltype: FwDataTypes.Date)]
        public string Pomasteraccrualthrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablelocationfilter", modeltype: FwDataTypes.Boolean)]
        public bool? Enablelocationfilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalshowcounted", modeltype: FwDataTypes.Boolean)]
        public bool? Physicalshowcounted { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfhidepayment", modeltype: FwDataTypes.Boolean)]
        //public bool? Orderconfhidepayment { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfdisablepo", modeltype: FwDataTypes.Boolean)]
        //public bool? Orderconfdisablepo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfdisablenote", modeltype: FwDataTypes.Boolean)]
        //public bool? Orderconfdisablenote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderconfrequiredeclinereason", modeltype: FwDataTypes.Boolean)]
        //public bool? Orderconfrequiredeclinereason { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcacheallwarehouses", modeltype: FwDataTypes.Boolean)]
        public bool? Availcacheallwarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availcalcunowneditems", modeltype: FwDataTypes.Boolean)]
        public bool? Availcalcunowneditems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consfeesonflatpo", modeltype: FwDataTypes.Text)]
        public string Consfeesonflatpo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showonlinetracking", modeltype: FwDataTypes.Boolean)]
        public bool? Showonlinetracking { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? Defaultitemdiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscounthiatus", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultitemdiscounthiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultitemdiscountseparate", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultitemdiscountseparate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultproratefacilities", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultproratefacilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultallowbillschedoverride", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultallowbillschedoverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultallowrebatecredits", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultallowrebatecredits { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultorderoverridebillsched", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultorderoverridebillsched { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemsinrooms", modeltype: FwDataTypes.Boolean)]
        public bool? Itemsinrooms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscaldaysweekenddefault", modeltype: FwDataTypes.Boolean)]
        public bool? Fiscaldaysweekenddefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscaldaysholidaydefault", modeltype: FwDataTypes.Boolean)]
        public bool? Fiscaldaysholidaydefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireoriginalshow", modeltype: FwDataTypes.Boolean)]
        public bool? Requireoriginalshow { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavailcolor", modeltype: FwDataTypes.Integer)]
        public int? Noavailcolor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manuallyretireusedsales", modeltype: FwDataTypes.Boolean)]
        public bool? Manuallyretireusedsales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpopulatepomasteraccruals", modeltype: FwDataTypes.Date)]
        public string Lastpopulatepomasteraccruals { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "facilitytypeincurrentlocation", modeltype: FwDataTypes.Boolean)]
        public bool? Facilitytypeincurrentlocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsaptype", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultsaptype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsapcostobject", modeltype: FwDataTypes.Text)]
        public string Defaultsapcostobject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsapaccountno", modeltype: FwDataTypes.Text)]
        public string Defaultsapaccountno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcustomerid", modeltype: FwDataTypes.Text)]
        public string DefaultcustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultusecustomerdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? Defaultusecustomerdiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefdiscounttemplateid", modeltype: FwDataTypes.Text)]
        public string CustomerdefdiscounttemplateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowconsignchangeicode", modeltype: FwDataTypes.Boolean)]
        public bool? Allowconsignchangeicode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteordersearchbyaka", modeltype: FwDataTypes.Boolean)]
        public bool? Quoteordersearchbyaka { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "candeletecontactwhenorderedby", modeltype: FwDataTypes.Boolean)]
        public bool? Candeletecontactwhenorderedby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refreshquikactivity", modeltype: FwDataTypes.Integer)]
        public int? Refreshquikactivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i35includepoprefix", modeltype: FwDataTypes.Boolean)]
        public bool? I35includepoprefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableorderunit", modeltype: FwDataTypes.Boolean)]
        public bool? Enableorderunit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "makequotedealanddescriptionunique", modeltype: FwDataTypes.Boolean)]
        public bool? Makequotedealanddescriptionunique { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablecustomeractivityrestrictions", modeltype: FwDataTypes.Boolean)]
        public bool? Enablecustomeractivityrestrictions { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatemanifestvalue", modeltype: FwDataTypes.Boolean)]
        //public bool? Updatemanifestvalue { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivemisclabor", modeltype: FwDataTypes.Boolean)]
        public bool? Receivemisclabor { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availenableqcdelay", modeltype: FwDataTypes.Boolean)]
        //public bool? Availenableqcdelay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayexcludeweekend", modeltype: FwDataTypes.Boolean)]
        //public bool? Availqcdelayexcludeweekend { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayexcludeholiday", modeltype: FwDataTypes.Boolean)]
        //public bool? Availqcdelayexcludeholiday { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelayindefinite", modeltype: FwDataTypes.Boolean)]
        //public bool? Availqcdelayindefinite { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i035summarizesr", modeltype: FwDataTypes.Boolean)]
        public bool? I035summarizesr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultpaytypeid", modeltype: FwDataTypes.Text)]
        public string DealdefaultpaytypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showfixedassetregister", modeltype: FwDataTypes.Boolean)]
        public bool? Showfixedassetregister { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "editldcheckcurrency", modeltype: FwDataTypes.Boolean)]
        public bool? Editldcheckcurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "editrepaircheckcurrency", modeltype: FwDataTypes.Boolean)]
        public bool? Editrepaircheckcurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includenonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? Includenonbillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimageid", modeltype: FwDataTypes.Text)]
        public string ReportLogoImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimage", modeltype: FwDataTypes.JpgDataUrl)]
        public string ReportLogoImage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimageheight", modeltype: FwDataTypes.Integer)]
        public int? ReportLogoImageHeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimagewidth", modeltype: FwDataTypes.Integer)]
        public int? ReportLogoImageWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
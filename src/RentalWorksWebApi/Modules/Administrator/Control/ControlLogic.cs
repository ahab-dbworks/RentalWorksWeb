using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.Control
{
    public class ControlLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ControlRecord control = new ControlRecord();
        SysControlRecord sysControl = new SysControlRecord();
        WebControlRecord webControl = new WebControlRecord();
        ControlLoader controlLoader = new ControlLoader();
        ControlBrowseLoader controlBrowseLoader = new ControlBrowseLoader();

        public ControlLogic()
        {
            dataRecords.Add(control);
            dataRecords.Add(sysControl);
            dataLoader = controlLoader;
            browseLoader = controlBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ControlId { get { return control.ControlId; } set { control.ControlId = value; sysControl.ControlId = value; } }
        public string Company { get { return control.Company; } set { control.Company = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SystemName { get { return control.System; } set { control.System = value; } }
        //public int? Maxrows { get { return control.Maxrows; } set { control.Maxrows = value; } }
        //public string Imagepath { get { return control.Imagepath; } set { control.Imagepath = value; } }
        //public string Settings { get { return control.Settings; } set { control.Settings = value; } }
        public string DatabaseVersion { get { return control.Dbversion; } set { control.Dbversion = value; } }
        //public string Build { get { return control.Build; } set { control.Build = value; } }
        //public bool? Userassignedvendorno { get { return sysControl.Userassignedvendorno; } set { sysControl.Userassignedvendorno = value; } }
        //public int? Vendorno { get { return sysControl.Vendorno; } set { sysControl.Vendorno = value; } }
        //public decimal? Rentrestockpercent { get { return sysControl.Rentrestockpercent; } set { sysControl.Rentrestockpercent = value; } }
        //public string Masterunit { get { return sysControl.Masterunit; } set { sysControl.Masterunit = value; } }
        //public int? Fymonth { get { return sysControl.Fymonth; } set { sysControl.Fymonth = value; } }
        public string ICodeMask { get { return sysControl.Invmask; } set { sysControl.Invmask = value; } }
        public string DefaultDealStatusId { get { return sysControl.DefaultDealStatusId; } set { sysControl.DefaultDealStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultDealStatus { get; set; }
        //public decimal? Salesrestockpercent { get { return sysControl.Salesrestockpercent; } set { sysControl.Salesrestockpercent = value; } }
        public string DefaultCustomerStatusId { get { return sysControl.DefaultCustomerStatusId; } set { sysControl.DefaultCustomerStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultCustomerStatus { get; set; }
        //public int? Hintseconds { get { return sysControl.Hintseconds; } set { sysControl.Hintseconds = value; } }
        //public string PhysicalinvadjId { get { return sysControl.PhysicalinvadjId; } set { sysControl.PhysicalinvadjId = value; } }
        //public bool? Logmessages { get { return sysControl.Logmessages; } set { sysControl.Logmessages = value; } }
        //public bool? Chkindeptfromuser { get { return sysControl.Chkindeptfromuser; } set { sysControl.Chkindeptfromuser = value; } }
        //public string Dwserver { get { return sysControl.Dwserver; } set { sysControl.Dwserver = value; } }
        //public string Dwdatabase { get { return sysControl.Dwdatabase; } set { sysControl.Dwdatabase = value; } }
        //public bool? Demomode { get { return sysControl.Demomode; } set { sysControl.Demomode = value; } }
        public bool? UserAssignedICodes { get { return sysControl.Userassignmasterno; } set { sysControl.Userassignmasterno = value; } }
        public int? NextICode { get { return sysControl.Masterno; } set { sysControl.Masterno = value; } }
        public string ICodePrefix { get { return sysControl.Icodeprefix; } set { sysControl.Icodeprefix = value; } }
        //public string Orderorderby { get { return sysControl.Orderorderby; } set { sysControl.Orderorderby = value; } }
        //public string PhyinvretiredreasonId { get { return sysControl.PhyinvretiredreasonId; } set { sysControl.PhyinvretiredreasonId = value; } }
        //public string ChangedicoderetiredreasonId { get { return sysControl.ChangedicoderetiredreasonId; } set { sysControl.ChangedicoderetiredreasonId = value; } }
        //public bool? Sharedeals { get { return sysControl.Sharedeals; } set { sysControl.Sharedeals = value; } }
        //public bool? Allowdecreaseorderwhenstaged { get { return sysControl.Allowdecreaseorderwhenstaged; } set { sysControl.Allowdecreaseorderwhenstaged = value; } }
        //public bool? Availprogressmeter { get { return sysControl.Availprogressmeter; } set { sysControl.Availprogressmeter = value; } }
        //public string Phyinvcost { get { return sysControl.Phyinvcost; } set { sysControl.Phyinvcost = value; } }
        //public int? Availprocessrows { get { return sysControl.Availprocessrows; } set { sysControl.Availprocessrows = value; } }
        //public bool? Loginventorychanges { get { return sysControl.Loginventorychanges; } set { sysControl.Loginventorychanges = value; } }
        //public bool? Checkchargesplitsonapproval { get { return sysControl.Checkchargesplitsonapproval; } set { sysControl.Checkchargesplitsonapproval = value; } }
        //public string CrfpaytypeId { get { return sysControl.CrfpaytypeId; } set { sysControl.CrfpaytypeId = value; } }
        //public string Orbitsapchgcode { get { return sysControl.Orbitsapchgcode; } set { sysControl.Orbitsapchgcode = value; } }
        //public string Orbitsapfinallddetail { get { return sysControl.Orbitsapfinallddetail; } set { sysControl.Orbitsapfinallddetail = value; } }
        //public string Orbitsaplabormiscdetail { get { return sysControl.Orbitsaplabormiscdetail; } set { sysControl.Orbitsaplabormiscdetail = value; } }
        //public string Orbitsaprentaldetail { get { return sysControl.Orbitsaprentaldetail; } set { sysControl.Orbitsaprentaldetail = value; } }
        //public string Orbitsaprentalsaledetail { get { return sysControl.Orbitsaprentalsaledetail; } set { sysControl.Orbitsaprentalsaledetail = value; } }
        //public string Orbitsapsalesdetail { get { return sysControl.Orbitsapsalesdetail; } set { sysControl.Orbitsapsalesdetail = value; } }
        //public string Orderheaderexportfilename { get { return sysControl.Orderheaderexportfilename; } set { sysControl.Orderheaderexportfilename = value; } }
        //public bool? Paymentsforfuturemonths { get { return sysControl.Paymentsforfuturemonths; } set { sysControl.Paymentsforfuturemonths = value; } }
        //public int? Univcrfno { get { return sysControl.Univcrfno; } set { sysControl.Univcrfno = value; } }
        //public string Univexporttype { get { return sysControl.Univexporttype; } set { sysControl.Univexporttype = value; } }
        //public bool? Delaygls { get { return sysControl.Delaygls; } set { sysControl.Delaygls = value; } }
        //public string Invoicelocationfrom { get { return sysControl.Invoicelocationfrom; } set { sysControl.Invoicelocationfrom = value; } }
        //public string Invoicenofrom { get { return sysControl.Invoicenofrom; } set { sysControl.Invoicenofrom = value; } }
        public string DefaultDealBillingCycleId { get { return sysControl.DefaultDealBillingCycleId; } set { sysControl.DefaultDealBillingCycleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultDealBillingCycle { get; set; }
        //public string VendordefaultbillperiodId { get { return sysControl.VendordefaultbillperiodId; } set { sysControl.VendordefaultbillperiodId = value; } }
        //public string Paymentsforfuturedates { get { return sysControl.Paymentsforfuturedates; } set { sysControl.Paymentsforfuturedates = value; } }
        //public string Mapsystem { get { return sysControl.Mapsystem; } set { sysControl.Mapsystem = value; } }
        //public bool? Phyclosewithoutadj { get { return sysControl.Phyclosewithoutadj; } set { sysControl.Phyclosewithoutadj = value; } }
        //public string NonrecurbillperiodId { get { return sysControl.NonrecurbillperiodId; } set { sysControl.NonrecurbillperiodId = value; } }
        //public bool? Revenueforcompletes { get { return sysControl.Revenueforcompletes; } set { sysControl.Revenueforcompletes = value; } }
        //public bool? Revenueforkits { get { return sysControl.Revenueforkits; } set { sysControl.Revenueforkits = value; } }
        //public string Completerevenuebasedon { get { return sysControl.Completerevenuebasedon; } set { sysControl.Completerevenuebasedon = value; } }
        //public string Kitrevenuebasedon { get { return sysControl.Kitrevenuebasedon; } set { sysControl.Kitrevenuebasedon = value; } }
        //public string SetconditionId { get { return sysControl.SetconditionId; } set { sysControl.SetconditionId = value; } }
        //public string PropsconditionId { get { return sysControl.PropsconditionId; } set { sysControl.PropsconditionId = value; } }
        //public decimal? Defaultspacedw { get { return sysControl.Defaultspacedw; } set { sysControl.Defaultspacedw = value; } }
        //public bool? Nocrosswhcheckin { get { return sysControl.Nocrosswhcheckin; } set { sysControl.Nocrosswhcheckin = value; } }
        //public bool? Markupreplacementcost { get { return sysControl.Markupreplacementcost; } set { sysControl.Markupreplacementcost = value; } }
        //public decimal? Replacementcostmarkuppct { get { return sysControl.Replacementcostmarkuppct; } set { sysControl.Replacementcostmarkuppct = value; } }
        //public bool? Crosslocationadds { get { return sysControl.Crosslocationadds; } set { sysControl.Crosslocationadds = value; } }
        //public int? Orderhistoryyears { get { return sysControl.Orderhistoryyears; } set { sysControl.Orderhistoryyears = value; } }
        //public bool? Synccustomercreditstatus { get { return sysControl.Synccustomercreditstatus; } set { sysControl.Synccustomercreditstatus = value; } }
        //public bool? Syncdealcreditstatus { get { return sysControl.Syncdealcreditstatus; } set { sysControl.Syncdealcreditstatus = value; } }
        //public bool? Copyinactiveitems { get { return sysControl.Copyinactiveitems; } set { sysControl.Copyinactiveitems = value; } }
        //public int? Availconflictlogdays { get { return sysControl.Availconflictlogdays; } set { sysControl.Availconflictlogdays = value; } }
        //public bool? Allowdeletebatchedreceipt { get { return sysControl.Allowdeletebatchedreceipt; } set { sysControl.Allowdeletebatchedreceipt = value; } }
        //public bool? Posecondapprovalwithoutfirst { get { return sysControl.Posecondapprovalwithoutfirst; } set { sysControl.Posecondapprovalwithoutfirst = value; } }
        //public bool? Codamountincheckoutprompt { get { return sysControl.Codamountincheckoutprompt; } set { sysControl.Codamountincheckoutprompt = value; } }
        //public string CustomerdefaultpaytermsId { get { return sysControl.CustomerdefaultpaytermsId; } set { sysControl.CustomerdefaultpaytermsId = value; } }
        //public int? Availhourlydays { get { return sysControl.Availhourlydays; } set { sysControl.Availhourlydays = value; } }
        //public bool? Treatconsignedqtyasowned { get { return sysControl.Treatconsignedqtyasowned; } set { sysControl.Treatconsignedqtyasowned = value; } }
        //public bool? Defaultusecustomercredit { get { return sysControl.Defaultusecustomercredit; } set { sysControl.Defaultusecustomercredit = value; } }
        //public bool? Defaultusecustomerinsurance { get { return sysControl.Defaultusecustomerinsurance; } set { sysControl.Defaultusecustomerinsurance = value; } }
        //public bool? Defaultusecustomertax { get { return sysControl.Defaultusecustomertax; } set { sysControl.Defaultusecustomertax = value; } }
        //public int? Rowstoautoupdatetotal { get { return sysControl.Rowstoautoupdatetotal; } set { sysControl.Rowstoautoupdatetotal = value; } }
        //public bool? Migrateuseeststart { get { return sysControl.Migrateuseeststart; } set { sysControl.Migrateuseeststart = value; } }
        //public bool? Requirecontactconfirmation { get { return sysControl.Requirecontactconfirmation; } set { sysControl.Requirecontactconfirmation = value; } }
        //public bool? Enableunconfirmationworkflow { get { return sysControl.Enableunconfirmationworkflow; } set { sysControl.Enableunconfirmationworkflow = value; } }
        //public bool? Workflowamountchanged { get { return sysControl.Workflowamountchanged; } set { sysControl.Workflowamountchanged = value; } }
        //public decimal? Workflowamountchangeddiff { get { return sysControl.Workflowamountchangeddiff; } set { sysControl.Workflowamountchangeddiff = value; } }
        //public bool? Workflowequipchanged { get { return sysControl.Workflowequipchanged; } set { sysControl.Workflowequipchanged = value; } }
        //public bool? Workflowzerodallor { get { return sysControl.Workflowzerodallor; } set { sysControl.Workflowzerodallor = value; } }
        //public bool? Workflowloadpickchanged { get { return sysControl.Workflowloadpickchanged; } set { sysControl.Workflowloadpickchanged = value; } }
        //public bool? Workflowbillingperiodchanged { get { return sysControl.Workflowbillingperiodchanged; } set { sysControl.Workflowbillingperiodchanged = value; } }
        //public bool? Disableconfirmation { get { return sysControl.Disableconfirmation; } set { sysControl.Disableconfirmation = value; } }
        //public string Flowsheetformat { get { return sysControl.Flowsheetformat; } set { sysControl.Flowsheetformat = value; } }
        //public bool? Allowtransfertosamewh { get { return sysControl.Allowtransfertosamewh; } set { sysControl.Allowtransfertosamewh = value; } }
        //public bool? Availcalculatepackages { get { return sysControl.Availcalculatepackages; } set { sysControl.Availcalculatepackages = value; } }
        //public bool? Workflowapprovalamountchanged { get { return sysControl.Workflowapprovalamountchanged; } set { sysControl.Workflowapprovalamountchanged = value; } }
        //public bool? Availpromptconflicts { get { return sysControl.Availpromptconflicts; } set { sysControl.Availpromptconflicts = value; } }
        //public bool? Useorderitem { get { return sysControl.Useorderitem; } set { sysControl.Useorderitem = value; } }
        //public bool? Restartweeklytiers { get { return sysControl.Restartweeklytiers; } set { sysControl.Restartweeklytiers = value; } }
        //public bool? Availonquotes { get { return sysControl.Availonquotes; } set { sysControl.Availonquotes = value; } }
        //public bool? Saveinvoiceprintsettings { get { return sysControl.Saveinvoiceprintsettings; } set { sysControl.Saveinvoiceprintsettings = value; } }
        //public bool? Shareordergroups { get { return sysControl.Shareordergroups; } set { sysControl.Shareordergroups = value; } }
        //public bool? Poccprimarywhenemailbackup { get { return sysControl.Poccprimarywhenemailbackup; } set { sysControl.Poccprimarywhenemailbackup = value; } }
        //public bool? Noorderscustomerstatus { get { return sysControl.Noorderscustomerstatus; } set { sysControl.Noorderscustomerstatus = value; } }
        //public string NoorderscustomerstatusId { get { return sysControl.NoorderscustomerstatusId; } set { sysControl.NoorderscustomerstatusId = value; } }
        //public int? Noorderscustomerstatusdays { get { return sysControl.Noorderscustomerstatusdays; } set { sysControl.Noorderscustomerstatusdays = value; } }
        //public bool? Availexcludeconsigned { get { return sysControl.Availexcludeconsigned; } set { sysControl.Availexcludeconsigned = value; } }
        //public int? Availreservationhistorydays { get { return sysControl.Availreservationhistorydays; } set { sysControl.Availreservationhistorydays = value; } }
        //public bool? Availreserveconsigned { get { return sysControl.Availreserveconsigned; } set { sysControl.Availreserveconsigned = value; } }
        //public bool? Defaultorderdesc { get { return sysControl.Defaultorderdesc; } set { sysControl.Defaultorderdesc = value; } }
        //public bool? Enableoldavaildetail { get { return sysControl.Enableoldavaildetail; } set { sysControl.Enableoldavaildetail = value; } }
        //public string Previewinvoicetitle { get { return sysControl.Previewinvoicetitle; } set { sysControl.Previewinvoicetitle = value; } }
        //public string Estimateinvoicetitle { get { return sysControl.Estimateinvoicetitle; } set { sysControl.Estimateinvoicetitle = value; } }
        //public int? Noinvoicedays { get { return sysControl.Noinvoicedays; } set { sysControl.Noinvoicedays = value; } }
        //public bool? Noinvoicecustomerstatus { get { return sysControl.Noinvoicecustomerstatus; } set { sysControl.Noinvoicecustomerstatus = value; } }
        //public string NoinvoicecustomerstatusId { get { return sysControl.NoinvoicecustomerstatusId; } set { sysControl.NoinvoicecustomerstatusId = value; } }
        //public bool? Noinvoicedealstatus { get { return sysControl.Noinvoicedealstatus; } set { sysControl.Noinvoicedealstatus = value; } }
        //public string NoinvoicedealstatusId { get { return sysControl.NoinvoicedealstatusId; } set { sysControl.NoinvoicedealstatusId = value; } }
        //public int? Pendingexchangecolor { get { return sysControl.Pendingexchangecolor; } set { sysControl.Pendingexchangecolor = value; } }
        //public string Repsignaturecaption { get { return sysControl.Repsignaturecaption; } set { sysControl.Repsignaturecaption = value; } }
        //public bool? Warehousebuttonsonorder { get { return sysControl.Warehousebuttonsonorder; } set { sysControl.Warehousebuttonsonorder = value; } }
        //public bool? Warehousebuttonsonpo { get { return sysControl.Warehousebuttonsonpo; } set { sysControl.Warehousebuttonsonpo = value; } }
        //public bool? Warehousebuttonsontransfer { get { return sysControl.Warehousebuttonsontransfer; } set { sysControl.Warehousebuttonsontransfer = value; } }
        //public bool? Warehousebuttonsontruck { get { return sysControl.Warehousebuttonsontruck; } set { sysControl.Warehousebuttonsontruck = value; } }
        //public string Pomasteraccrualthrough { get { return sysControl.Pomasteraccrualthrough; } set { sysControl.Pomasteraccrualthrough = value; } }
        //public bool? Enablelocationfilter { get { return sysControl.Enablelocationfilter; } set { sysControl.Enablelocationfilter = value; } }
        //public bool? Physicalshowcounted { get { return sysControl.Physicalshowcounted; } set { sysControl.Physicalshowcounted = value; } }
        //public bool? Orderconfhidepayment { get { return sysControl.Orderconfhidepayment; } set { sysControl.Orderconfhidepayment = value; } }
        //public bool? Orderconfdisablepo { get { return sysControl.Orderconfdisablepo; } set { sysControl.Orderconfdisablepo = value; } }
        //public bool? Orderconfdisablenote { get { return sysControl.Orderconfdisablenote; } set { sysControl.Orderconfdisablenote = value; } }
        //public bool? Orderconfrequiredeclinereason { get { return sysControl.Orderconfrequiredeclinereason; } set { sysControl.Orderconfrequiredeclinereason = value; } }
        //public bool? Availcacheallwarehouses { get { return sysControl.Availcacheallwarehouses; } set { sysControl.Availcacheallwarehouses = value; } }
        //public bool? Availcalcunowneditems { get { return sysControl.Availcalcunowneditems; } set { sysControl.Availcalcunowneditems = value; } }
        //public string Consfeesonflatpo { get { return sysControl.Consfeesonflatpo; } set { sysControl.Consfeesonflatpo = value; } }
        //public bool? Showonlinetracking { get { return sysControl.Showonlinetracking; } set { sysControl.Showonlinetracking = value; } }
        //public decimal? Defaultitemdiscountpct { get { return sysControl.Defaultitemdiscountpct; } set { sysControl.Defaultitemdiscountpct = value; } }
        //public bool? Defaultitemdiscounthiatus { get { return sysControl.Defaultitemdiscounthiatus; } set { sysControl.Defaultitemdiscounthiatus = value; } }
        //public bool? Defaultitemdiscountseparate { get { return sysControl.Defaultitemdiscountseparate; } set { sysControl.Defaultitemdiscountseparate = value; } }
        //public bool? Defaultproratefacilities { get { return sysControl.Defaultproratefacilities; } set { sysControl.Defaultproratefacilities = value; } }
        //public bool? Defaultallowbillschedoverride { get { return sysControl.Defaultallowbillschedoverride; } set { sysControl.Defaultallowbillschedoverride = value; } }
        //public bool? Defaultallowrebatecredits { get { return sysControl.Defaultallowrebatecredits; } set { sysControl.Defaultallowrebatecredits = value; } }
        //public bool? Defaultorderoverridebillsched { get { return sysControl.Defaultorderoverridebillsched; } set { sysControl.Defaultorderoverridebillsched = value; } }
        //public bool? Itemsinrooms { get { return sysControl.Itemsinrooms; } set { sysControl.Itemsinrooms = value; } }
        //public bool? Fiscaldaysweekenddefault { get { return sysControl.Fiscaldaysweekenddefault; } set { sysControl.Fiscaldaysweekenddefault = value; } }
        //public bool? Fiscaldaysholidaydefault { get { return sysControl.Fiscaldaysholidaydefault; } set { sysControl.Fiscaldaysholidaydefault = value; } }
        //public bool? Requireoriginalshow { get { return sysControl.Requireoriginalshow; } set { sysControl.Requireoriginalshow = value; } }
        //public int? Noavailcolor { get { return sysControl.Noavailcolor; } set { sysControl.Noavailcolor = value; } }
        //public bool? Manuallyretireusedsales { get { return sysControl.Manuallyretireusedsales; } set { sysControl.Manuallyretireusedsales = value; } }
        //public string Lastpopulatepomasteraccruals { get { return sysControl.Lastpopulatepomasteraccruals; } set { sysControl.Lastpopulatepomasteraccruals = value; } }
        //public bool? Facilitytypeincurrentlocation { get { return sysControl.Facilitytypeincurrentlocation; } set { sysControl.Facilitytypeincurrentlocation = value; } }
        //public bool? Defaultsaptype { get { return sysControl.Defaultsaptype; } set { sysControl.Defaultsaptype = value; } }
        //public string Defaultsapcostobject { get { return sysControl.Defaultsapcostobject; } set { sysControl.Defaultsapcostobject = value; } }
        //public string Defaultsapaccountno { get { return sysControl.Defaultsapaccountno; } set { sysControl.Defaultsapaccountno = value; } }
        //public string DefaultcustomerId { get { return sysControl.DefaultcustomerId; } set { sysControl.DefaultcustomerId = value; } }
        //public bool? Defaultusecustomerdiscount { get { return sysControl.Defaultusecustomerdiscount; } set { sysControl.Defaultusecustomerdiscount = value; } }
        //public string CustomerdefdiscounttemplateId { get { return sysControl.CustomerdefdiscounttemplateId; } set { sysControl.CustomerdefdiscounttemplateId = value; } }
        //public bool? Allowconsignchangeicode { get { return sysControl.Allowconsignchangeicode; } set { sysControl.Allowconsignchangeicode = value; } }
        //public bool? Quoteordersearchbyaka { get { return sysControl.Quoteordersearchbyaka; } set { sysControl.Quoteordersearchbyaka = value; } }
        //public bool? Candeletecontactwhenorderedby { get { return sysControl.Candeletecontactwhenorderedby; } set { sysControl.Candeletecontactwhenorderedby = value; } }
        //public int? Refreshquikactivity { get { return sysControl.Refreshquikactivity; } set { sysControl.Refreshquikactivity = value; } }
        //public bool? I35includepoprefix { get { return sysControl.I35includepoprefix; } set { sysControl.I35includepoprefix = value; } }
        //public bool? Enableorderunit { get { return sysControl.Enableorderunit; } set { sysControl.Enableorderunit = value; } }
        //public bool? Makequotedealanddescriptionunique { get { return sysControl.Makequotedealanddescriptionunique; } set { sysControl.Makequotedealanddescriptionunique = value; } }
        //public bool? Enablecustomeractivityrestrictions { get { return sysControl.Enablecustomeractivityrestrictions; } set { sysControl.Enablecustomeractivityrestrictions = value; } }
        //public bool? Updatemanifestvalue { get { return sysControl.Updatemanifestvalue; } set { sysControl.Updatemanifestvalue = value; } }
        //public string CurrencyId { get { return sysControl.CurrencyId; } set { sysControl.CurrencyId = value; } }
        //public bool? Receivemisclabor { get { return sysControl.Receivemisclabor; } set { sysControl.Receivemisclabor = value; } }
        //public bool? Availenableqcdelay { get { return sysControl.Availenableqcdelay; } set { sysControl.Availenableqcdelay = value; } }
        //public bool? Availqcdelayexcludeweekend { get { return sysControl.Availqcdelayexcludeweekend; } set { sysControl.Availqcdelayexcludeweekend = value; } }
        //public bool? Availqcdelayexcludeholiday { get { return sysControl.Availqcdelayexcludeholiday; } set { sysControl.Availqcdelayexcludeholiday = value; } }
        //public bool? Availqcdelayindefinite { get { return sysControl.Availqcdelayindefinite; } set { sysControl.Availqcdelayindefinite = value; } }
        //public bool? I035summarizesr { get { return sysControl.I035summarizesr; } set { sysControl.I035summarizesr = value; } }
        //public string DealdefaultpaytypeId { get { return sysControl.DealdefaultpaytypeId; } set { sysControl.DealdefaultpaytypeId = value; } }
        //public bool? Showfixedassetregister { get { return sysControl.Showfixedassetregister; } set { sysControl.Showfixedassetregister = value; } }
        //public bool? Editldcheckcurrency { get { return sysControl.Editldcheckcurrency; } set { sysControl.Editldcheckcurrency = value; } }
        //public bool? Editrepaircheckcurrency { get { return sysControl.Editrepaircheckcurrency; } set { sysControl.Editrepaircheckcurrency = value; } }
        //public bool? Includenonbillable { get { return sysControl.Includenonbillable; } set { sysControl.Includenonbillable = value; } }

        //------------------------------------------------------------------------------------ 

        public string ReportLogoImageId { get { return webControl.ReportLogoImageId; } set { webControl.ReportLogoImageId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReportLogoImage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ReportLogoImageHeight { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ReportLogoImageWidth { get; set; }
        //------------------------------------------------------------------------------------ 


        public string DateStamp { get { return control.DateStamp; } set { control.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                isValid = false;
                validateMsg = "Cannot add new records to Control";
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}
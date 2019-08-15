using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.SystemSettings
{
    [FwLogic(Id: "uhJu1bDJPDTRQ")]
    public class SystemSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ControlRecord control = new ControlRecord();
        SystemSettingsLoader systemSettingsLoader = new SystemSettingsLoader();

        //------------------------------------------------------------------------------------ 
        public SystemSettingsLogic()
        {
            dataRecords.Add(control);
            dataLoader = systemSettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bb0kgMEmdIsAz", IsPrimaryKey: true)]
        public string SystemSettingsId { get { return control.ControlId; } set { control.ControlId = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxxxxxxx", IsRecordTitle: true)]
        public string SystemSettingsName { get { return "System Settings"; } }

        [FwLogicProperty(Id: "AnIwydk6lUHGC")]
        public string CompanyName { get { return control.Company; } set { control.Company = value; } }

        [FwLogicProperty(Id: "AC60eiSh5Pri8")]
        public string SystemName { get { return control.System; } set { control.System = value; } }

        [FwLogicProperty(Id: "mjQeIXHL6lIKJ")]
        public string DatabaseVersion { get { return control.Dbversion; } set { control.Dbversion = value; } }

        /*
        //[FwLogicProperty(Id:"3RwUcgRBA7gxg")]
        //public int? Maxrows { get { return control.Maxrows; } set { control.Maxrows = value; } }

        //[FwLogicProperty(Id:"ucP26Wc7eVs4K")]
        //public string Imagepath { get { return control.Imagepath; } set { control.Imagepath = value; } }

        //[FwLogicProperty(Id:"EYCv8EDHIIJJ7")]
        //public string Settings { get { return control.Settings; } set { control.Settings = value; } }

        //[FwLogicProperty(Id:"11PX6fP8NNq5Q")]
        //public string Build { get { return control.Build; } set { control.Build = value; } }

        //[FwLogicProperty(Id:"NrvJDomVVcrcM")]
        //public bool? Userassignedvendorno { get { return sysControl.Userassignedvendorno; } set { sysControl.Userassignedvendorno = value; } }

        //[FwLogicProperty(Id:"CSiC9ZwpMLerq")]
        //public int? Vendorno { get { return sysControl.Vendorno; } set { sysControl.Vendorno = value; } }

        //[FwLogicProperty(Id:"YixTIZ4ozMPAB")]
        //public decimal? Rentrestockpercent { get { return sysControl.Rentrestockpercent; } set { sysControl.Rentrestockpercent = value; } }

        [FwLogicProperty(Id: "T7ViEleQjMRLN")]
        public string DefaultUnitId { get { return sysControl.DefaultUnitId; } set { sysControl.DefaultUnitId = value; } }

        [FwLogicProperty(Id: "5v3RXZYFF8UAb", IsReadOnly: true)]
        public string DefaultUnit { get; set; }

        //[FwLogicProperty(Id:"h3nNvL0CPcra3")]
        //public int? Fymonth { get { return sysControl.Fymonth; } set { sysControl.Fymonth = value; } }

        [FwLogicProperty(Id: "qaFBGGoda7K4z")]
        public string ICodeMask { get { return sysControl.Invmask; } set { sysControl.Invmask = value; } }

        [FwLogicProperty(Id: "3YsgigYNvtskX")]
        public string DefaultDealStatusId { get { return sysControl.DefaultDealStatusId; } set { sysControl.DefaultDealStatusId = value; } }

        [FwLogicProperty(Id: "S3ypeH3bdn3u", IsReadOnly: true)]
        public string DefaultDealStatus { get; set; }

        //[FwLogicProperty(Id:"Y2bMhNlUCFsMc")]
        //public decimal? Salesrestockpercent { get { return sysControl.Salesrestockpercent; } set { sysControl.Salesrestockpercent = value; } }

        [FwLogicProperty(Id: "Fd5P5CsJfDPMU")]
        public string DefaultCustomerStatusId { get { return sysControl.DefaultCustomerStatusId; } set { sysControl.DefaultCustomerStatusId = value; } }

        [FwLogicProperty(Id: "GFUsV5JJHmXf", IsReadOnly: true)]
        public string DefaultCustomerStatus { get; set; }

        //[FwLogicProperty(Id:"l198PWg18nO3d")]
        //public int? Hintseconds { get { return sysControl.Hintseconds; } set { sysControl.Hintseconds = value; } }

        //[FwLogicProperty(Id:"7vtxbiTe2wwKg")]
        //public string PhysicalinvadjId { get { return sysControl.PhysicalinvadjId; } set { sysControl.PhysicalinvadjId = value; } }

        //[FwLogicProperty(Id:"IxwSjnT5bp2Lp")]
        //public bool? Logmessages { get { return sysControl.Logmessages; } set { sysControl.Logmessages = value; } }

        //[FwLogicProperty(Id:"eoYwR0VHIH6FE")]
        //public bool? Chkindeptfromuser { get { return sysControl.Chkindeptfromuser; } set { sysControl.Chkindeptfromuser = value; } }

        //[FwLogicProperty(Id:"VGKd75j0b6K9a")]
        //public string Dwserver { get { return sysControl.Dwserver; } set { sysControl.Dwserver = value; } }

        //[FwLogicProperty(Id:"nVUxwaiG4qgGF")]
        //public string Dwdatabase { get { return sysControl.Dwdatabase; } set { sysControl.Dwdatabase = value; } }

        //[FwLogicProperty(Id:"kfjZCENVfrojh")]
        //public bool? Demomode { get { return sysControl.Demomode; } set { sysControl.Demomode = value; } }

        [FwLogicProperty(Id: "ilW6pZ8VGXX34")]
        public bool? UserAssignedICodes { get { return sysControl.Userassignmasterno; } set { sysControl.Userassignmasterno = value; } }

        [FwLogicProperty(Id: "WEPlMoaddmey2")]
        public int? NextICode { get { return sysControl.Masterno; } set { sysControl.Masterno = value; } }

        [FwLogicProperty(Id: "cwGqDxLgDJ0CA")]
        public string ICodePrefix { get { return sysControl.Icodeprefix; } set { sysControl.Icodeprefix = value; } }

        //[FwLogicProperty(Id:"9d4YNgmgZJS91")]
        //public string Orderorderby { get { return sysControl.Orderorderby; } set { sysControl.Orderorderby = value; } }

        //[FwLogicProperty(Id:"W0RV10hr8Dkew")]
        //public string PhyinvretiredreasonId { get { return sysControl.PhyinvretiredreasonId; } set { sysControl.PhyinvretiredreasonId = value; } }

        //[FwLogicProperty(Id:"NPycQpsd2JJb0")]
        //public string ChangedicoderetiredreasonId { get { return sysControl.ChangedicoderetiredreasonId; } set { sysControl.ChangedicoderetiredreasonId = value; } }

        //[FwLogicProperty(Id:"hKPSTpdF2vEOf")]
        //public bool? Sharedeals { get { return sysControl.Sharedeals; } set { sysControl.Sharedeals = value; } }

        //[FwLogicProperty(Id:"chejcaizCPapA")]
        //public bool? Allowdecreaseorderwhenstaged { get { return sysControl.Allowdecreaseorderwhenstaged; } set { sysControl.Allowdecreaseorderwhenstaged = value; } }

        //[FwLogicProperty(Id:"Lhw7P0xVFldTY")]
        //public bool? Availprogressmeter { get { return sysControl.Availprogressmeter; } set { sysControl.Availprogressmeter = value; } }

        //[FwLogicProperty(Id:"Z6je3P5zAapLS")]
        //public string Phyinvcost { get { return sysControl.Phyinvcost; } set { sysControl.Phyinvcost = value; } }

        //[FwLogicProperty(Id:"NtPiswlrxvueE")]
        //public int? Availprocessrows { get { return sysControl.Availprocessrows; } set { sysControl.Availprocessrows = value; } }

        //[FwLogicProperty(Id:"FwSYfppmgu6Ok")]
        //public bool? Loginventorychanges { get { return sysControl.Loginventorychanges; } set { sysControl.Loginventorychanges = value; } }

        //[FwLogicProperty(Id:"T5Z1Hi2vtmgxH")]
        //public bool? Checkchargesplitsonapproval { get { return sysControl.Checkchargesplitsonapproval; } set { sysControl.Checkchargesplitsonapproval = value; } }

        //[FwLogicProperty(Id:"rwYFLnjQXqr6f")]
        //public string CrfpaytypeId { get { return sysControl.CrfpaytypeId; } set { sysControl.CrfpaytypeId = value; } }

        //[FwLogicProperty(Id:"WnKfqIAgnusG5")]
        //public string Orbitsapchgcode { get { return sysControl.Orbitsapchgcode; } set { sysControl.Orbitsapchgcode = value; } }

        //[FwLogicProperty(Id:"fcLXYjt6oBKNH")]
        //public string Orbitsapfinallddetail { get { return sysControl.Orbitsapfinallddetail; } set { sysControl.Orbitsapfinallddetail = value; } }

        //[FwLogicProperty(Id:"Z7bx6jdTa1KUR")]
        //public string Orbitsaplabormiscdetail { get { return sysControl.Orbitsaplabormiscdetail; } set { sysControl.Orbitsaplabormiscdetail = value; } }

        //[FwLogicProperty(Id:"uOy2l4F9eb5rm")]
        //public string Orbitsaprentaldetail { get { return sysControl.Orbitsaprentaldetail; } set { sysControl.Orbitsaprentaldetail = value; } }

        //[FwLogicProperty(Id:"EI1oT6jRJ99hi")]
        //public string Orbitsaprentalsaledetail { get { return sysControl.Orbitsaprentalsaledetail; } set { sysControl.Orbitsaprentalsaledetail = value; } }

        //[FwLogicProperty(Id:"n606b3I6smOWZ")]
        //public string Orbitsapsalesdetail { get { return sysControl.Orbitsapsalesdetail; } set { sysControl.Orbitsapsalesdetail = value; } }

        //[FwLogicProperty(Id:"6jhfpieDHraMI")]
        //public string Orderheaderexportfilename { get { return sysControl.Orderheaderexportfilename; } set { sysControl.Orderheaderexportfilename = value; } }

        //[FwLogicProperty(Id:"EJuhvae3JflZW")]
        //public bool? Paymentsforfuturemonths { get { return sysControl.Paymentsforfuturemonths; } set { sysControl.Paymentsforfuturemonths = value; } }

        //[FwLogicProperty(Id:"3U6XeOmNn1WXH")]
        //public int? Univcrfno { get { return sysControl.Univcrfno; } set { sysControl.Univcrfno = value; } }

        //[FwLogicProperty(Id:"xMdpV0vziavvt")]
        //public string Univexporttype { get { return sysControl.Univexporttype; } set { sysControl.Univexporttype = value; } }

        //[FwLogicProperty(Id:"J48YT62yMZeN5")]
        //public bool? Delaygls { get { return sysControl.Delaygls; } set { sysControl.Delaygls = value; } }

        //[FwLogicProperty(Id:"k1R5JMM0iqvjL")]
        //public string Invoicelocationfrom { get { return sysControl.Invoicelocationfrom; } set { sysControl.Invoicelocationfrom = value; } }

        //[FwLogicProperty(Id:"iOwfmHc2u1Zpn")]
        //public string Invoicenofrom { get { return sysControl.Invoicenofrom; } set { sysControl.Invoicenofrom = value; } }

        [FwLogicProperty(Id: "t0eDT6XoXBiaK")]
        public string DefaultDealBillingCycleId { get { return sysControl.DefaultDealBillingCycleId; } set { sysControl.DefaultDealBillingCycleId = value; } }

        [FwLogicProperty(Id: "I5ev1tRp5Vht", IsReadOnly: true)]
        public string DefaultDealBillingCycle { get; set; }

        //[FwLogicProperty(Id:"2pboITyACuAgQ")]
        //public string VendordefaultbillperiodId { get { return sysControl.VendordefaultbillperiodId; } set { sysControl.VendordefaultbillperiodId = value; } }

        //[FwLogicProperty(Id:"ITTZRwUOgRN2V")]
        //public string Paymentsforfuturedates { get { return sysControl.Paymentsforfuturedates; } set { sysControl.Paymentsforfuturedates = value; } }

        //[FwLogicProperty(Id:"8jUpYeZWfTNOi")]
        //public string Mapsystem { get { return sysControl.Mapsystem; } set { sysControl.Mapsystem = value; } }

        //[FwLogicProperty(Id:"fE8OXhLt4iUVW")]
        //public bool? Phyclosewithoutadj { get { return sysControl.Phyclosewithoutadj; } set { sysControl.Phyclosewithoutadj = value; } }

        [FwLogicProperty(Id: "vJHCCcfjlFN18")]
        public string DefaultNonRecurringBillingCycleId { get { return sysControl.DefaultNonRecurringBillingCycleId; } set { sysControl.DefaultNonRecurringBillingCycleId = value; } }

        [FwLogicProperty(Id: "I4WKllH1cIfdI", IsReadOnly: true)]
        public string DefaultNonRecurringBillingCycle { get; set; }

        //[FwLogicProperty(Id:"p2ROs4QHzemcG")]
        //public bool? Revenueforcompletes { get { return sysControl.Revenueforcompletes; } set { sysControl.Revenueforcompletes = value; } }

        //[FwLogicProperty(Id:"MauiqYUP7FodD")]
        //public bool? Revenueforkits { get { return sysControl.Revenueforkits; } set { sysControl.Revenueforkits = value; } }

        //[FwLogicProperty(Id:"5BoOyHCp9kwfQ")]
        //public string Completerevenuebasedon { get { return sysControl.Completerevenuebasedon; } set { sysControl.Completerevenuebasedon = value; } }

        //[FwLogicProperty(Id:"TWDgu7DalMdyA")]
        //public string Kitrevenuebasedon { get { return sysControl.Kitrevenuebasedon; } set { sysControl.Kitrevenuebasedon = value; } }

        //[FwLogicProperty(Id:"anpaNFli1EnpZ")]
        //public string SetconditionId { get { return sysControl.SetconditionId; } set { sysControl.SetconditionId = value; } }

        //[FwLogicProperty(Id:"Ee9UBOmRnxFU2")]
        //public string PropsconditionId { get { return sysControl.PropsconditionId; } set { sysControl.PropsconditionId = value; } }

        //[FwLogicProperty(Id:"fU39zx5EUDwmy")]
        //public decimal? Defaultspacedw { get { return sysControl.Defaultspacedw; } set { sysControl.Defaultspacedw = value; } }

        //[FwLogicProperty(Id:"kGxyfYOODFem8")]
        //public bool? Nocrosswhcheckin { get { return sysControl.Nocrosswhcheckin; } set { sysControl.Nocrosswhcheckin = value; } }

        //[FwLogicProperty(Id:"jc1xiLESOfKXX")]
        //public bool? Markupreplacementcost { get { return sysControl.Markupreplacementcost; } set { sysControl.Markupreplacementcost = value; } }

        //[FwLogicProperty(Id:"BgOBcWbTZEREv")]
        //public decimal? Replacementcostmarkuppct { get { return sysControl.Replacementcostmarkuppct; } set { sysControl.Replacementcostmarkuppct = value; } }

        //[FwLogicProperty(Id:"c7IaQ93o6HFcb")]
        //public bool? Crosslocationadds { get { return sysControl.Crosslocationadds; } set { sysControl.Crosslocationadds = value; } }

        //[FwLogicProperty(Id:"lKq1DitfckPVT")]
        //public int? Orderhistoryyears { get { return sysControl.Orderhistoryyears; } set { sysControl.Orderhistoryyears = value; } }

        //[FwLogicProperty(Id:"CQMvyJ4YKVJH8")]
        //public bool? Synccustomercreditstatus { get { return sysControl.Synccustomercreditstatus; } set { sysControl.Synccustomercreditstatus = value; } }

        //[FwLogicProperty(Id:"ReR9goMh9TmaC")]
        //public bool? Syncdealcreditstatus { get { return sysControl.Syncdealcreditstatus; } set { sysControl.Syncdealcreditstatus = value; } }

        //[FwLogicProperty(Id:"waLjnTZ3IiaCB")]
        //public bool? Copyinactiveitems { get { return sysControl.Copyinactiveitems; } set { sysControl.Copyinactiveitems = value; } }

        //[FwLogicProperty(Id:"b10uwgeURoRej")]
        //public int? Availconflictlogdays { get { return sysControl.Availconflictlogdays; } set { sysControl.Availconflictlogdays = value; } }

        //[FwLogicProperty(Id:"d4eQnaJ5riiZo")]
        //public bool? Allowdeletebatchedreceipt { get { return sysControl.Allowdeletebatchedreceipt; } set { sysControl.Allowdeletebatchedreceipt = value; } }

        //[FwLogicProperty(Id:"u2xBSKT7By478")]
        //public bool? Posecondapprovalwithoutfirst { get { return sysControl.Posecondapprovalwithoutfirst; } set { sysControl.Posecondapprovalwithoutfirst = value; } }

        //[FwLogicProperty(Id:"4WiHlmCIIMKm3")]
        //public bool? Codamountincheckoutprompt { get { return sysControl.Codamountincheckoutprompt; } set { sysControl.Codamountincheckoutprompt = value; } }

        //[FwLogicProperty(Id:"Pv4HAjwvt8Is4")]
        //public string CustomerdefaultpaytermsId { get { return sysControl.CustomerdefaultpaytermsId; } set { sysControl.CustomerdefaultpaytermsId = value; } }

        //[FwLogicProperty(Id:"JyRSbW46GHBAq")]
        //public int? Availhourlydays { get { return sysControl.Availhourlydays; } set { sysControl.Availhourlydays = value; } }

        //[FwLogicProperty(Id:"I8Pi5ErAxiVgb")]
        //public bool? Treatconsignedqtyasowned { get { return sysControl.Treatconsignedqtyasowned; } set { sysControl.Treatconsignedqtyasowned = value; } }

        //[FwLogicProperty(Id:"ZF9KgHtSxXzBE")]
        //public bool? Defaultusecustomercredit { get { return sysControl.Defaultusecustomercredit; } set { sysControl.Defaultusecustomercredit = value; } }

        //[FwLogicProperty(Id:"boujcyNSQ1sfP")]
        //public bool? Defaultusecustomerinsurance { get { return sysControl.Defaultusecustomerinsurance; } set { sysControl.Defaultusecustomerinsurance = value; } }

        //[FwLogicProperty(Id:"XvijztYyG1rbI")]
        //public bool? Defaultusecustomertax { get { return sysControl.Defaultusecustomertax; } set { sysControl.Defaultusecustomertax = value; } }

        //[FwLogicProperty(Id:"vCcuGceaHnL49")]
        //public int? Rowstoautoupdatetotal { get { return sysControl.Rowstoautoupdatetotal; } set { sysControl.Rowstoautoupdatetotal = value; } }

        //[FwLogicProperty(Id:"mQCboqf2tXXJF")]
        //public bool? Migrateuseeststart { get { return sysControl.Migrateuseeststart; } set { sysControl.Migrateuseeststart = value; } }

        //[FwLogicProperty(Id:"5xXCBgzumUYnt")]
        //public bool? Requirecontactconfirmation { get { return sysControl.Requirecontactconfirmation; } set { sysControl.Requirecontactconfirmation = value; } }

        //[FwLogicProperty(Id:"ExBoCZN9NnsGw")]
        //public bool? Enableunconfirmationworkflow { get { return sysControl.Enableunconfirmationworkflow; } set { sysControl.Enableunconfirmationworkflow = value; } }

        //[FwLogicProperty(Id:"XPwH0kIF9G89N")]
        //public bool? Workflowamountchanged { get { return sysControl.Workflowamountchanged; } set { sysControl.Workflowamountchanged = value; } }

        //[FwLogicProperty(Id:"Law5p5NtZqfxD")]
        //public decimal? Workflowamountchangeddiff { get { return sysControl.Workflowamountchangeddiff; } set { sysControl.Workflowamountchangeddiff = value; } }

        //[FwLogicProperty(Id:"Kq7chy6vQoOpv")]
        //public bool? Workflowequipchanged { get { return sysControl.Workflowequipchanged; } set { sysControl.Workflowequipchanged = value; } }

        //[FwLogicProperty(Id:"83oF9xszb1Hz8")]
        //public bool? Workflowzerodallor { get { return sysControl.Workflowzerodallor; } set { sysControl.Workflowzerodallor = value; } }

        //[FwLogicProperty(Id:"uXoJP1hnpp1SX")]
        //public bool? Workflowloadpickchanged { get { return sysControl.Workflowloadpickchanged; } set { sysControl.Workflowloadpickchanged = value; } }

        //[FwLogicProperty(Id:"7UgRFGozuTeHX")]
        //public bool? Workflowbillingperiodchanged { get { return sysControl.Workflowbillingperiodchanged; } set { sysControl.Workflowbillingperiodchanged = value; } }

        //[FwLogicProperty(Id:"d7a3ozpD5xewT")]
        //public bool? Disableconfirmation { get { return sysControl.Disableconfirmation; } set { sysControl.Disableconfirmation = value; } }

        //[FwLogicProperty(Id:"h3t8rTvocVRPt")]
        //public string Flowsheetformat { get { return sysControl.Flowsheetformat; } set { sysControl.Flowsheetformat = value; } }

        //[FwLogicProperty(Id:"kxTEax3Wfk81K")]
        //public bool? Allowtransfertosamewh { get { return sysControl.Allowtransfertosamewh; } set { sysControl.Allowtransfertosamewh = value; } }

        //[FwLogicProperty(Id:"yKiQFx3Myhktm")]
        //public bool? Availcalculatepackages { get { return sysControl.Availcalculatepackages; } set { sysControl.Availcalculatepackages = value; } }

        //[FwLogicProperty(Id:"tmA79s7HH3Ow9")]
        //public bool? Workflowapprovalamountchanged { get { return sysControl.Workflowapprovalamountchanged; } set { sysControl.Workflowapprovalamountchanged = value; } }

        //[FwLogicProperty(Id:"fA5b4vd0XX52t")]
        //public bool? Availpromptconflicts { get { return sysControl.Availpromptconflicts; } set { sysControl.Availpromptconflicts = value; } }

        //[FwLogicProperty(Id:"tX5nRyL5lHnVc")]
        //public bool? Useorderitem { get { return sysControl.Useorderitem; } set { sysControl.Useorderitem = value; } }

        //[FwLogicProperty(Id:"Bgn88OHWLQc5W")]
        //public bool? Restartweeklytiers { get { return sysControl.Restartweeklytiers; } set { sysControl.Restartweeklytiers = value; } }

        //[FwLogicProperty(Id:"pTsiCIeTnhOEh")]
        //public bool? Availonquotes { get { return sysControl.Availonquotes; } set { sysControl.Availonquotes = value; } }

        //[FwLogicProperty(Id:"DG5ZniPrHTtgr")]
        //public bool? Saveinvoiceprintsettings { get { return sysControl.Saveinvoiceprintsettings; } set { sysControl.Saveinvoiceprintsettings = value; } }

        //[FwLogicProperty(Id:"XM5j793WQXWI9")]
        //public bool? Shareordergroups { get { return sysControl.Shareordergroups; } set { sysControl.Shareordergroups = value; } }

        //[FwLogicProperty(Id:"Tif5LE0tQqTJg")]
        //public bool? Poccprimarywhenemailbackup { get { return sysControl.Poccprimarywhenemailbackup; } set { sysControl.Poccprimarywhenemailbackup = value; } }

        //[FwLogicProperty(Id:"jyVOlJwK4kyUn")]
        //public bool? Noorderscustomerstatus { get { return sysControl.Noorderscustomerstatus; } set { sysControl.Noorderscustomerstatus = value; } }

        //[FwLogicProperty(Id:"12ZTFrrrELCTv")]
        //public string NoorderscustomerstatusId { get { return sysControl.NoorderscustomerstatusId; } set { sysControl.NoorderscustomerstatusId = value; } }

        //[FwLogicProperty(Id:"M4u1im72csFv4")]
        //public int? Noorderscustomerstatusdays { get { return sysControl.Noorderscustomerstatusdays; } set { sysControl.Noorderscustomerstatusdays = value; } }

        //[FwLogicProperty(Id:"ZZGrtl5LymCmu")]
        //public bool? Availexcludeconsigned { get { return sysControl.Availexcludeconsigned; } set { sysControl.Availexcludeconsigned = value; } }

        //[FwLogicProperty(Id:"dEecKPeMOXWRk")]
        //public int? Availreservationhistorydays { get { return sysControl.Availreservationhistorydays; } set { sysControl.Availreservationhistorydays = value; } }

        //[FwLogicProperty(Id:"hFGsU0Bquwztw")]
        //public bool? Availreserveconsigned { get { return sysControl.Availreserveconsigned; } set { sysControl.Availreserveconsigned = value; } }

        //[FwLogicProperty(Id:"xeuYuL7NegdPJ")]
        //public bool? Defaultorderdesc { get { return sysControl.Defaultorderdesc; } set { sysControl.Defaultorderdesc = value; } }

        //[FwLogicProperty(Id:"lcuQr7OAKnOn5")]
        //public bool? Enableoldavaildetail { get { return sysControl.Enableoldavaildetail; } set { sysControl.Enableoldavaildetail = value; } }

        //[FwLogicProperty(Id:"E3EBZ2C42PmBE")]
        //public string Previewinvoicetitle { get { return sysControl.Previewinvoicetitle; } set { sysControl.Previewinvoicetitle = value; } }

        //[FwLogicProperty(Id:"VqFNIy6uPz1n8")]
        //public string Estimateinvoicetitle { get { return sysControl.Estimateinvoicetitle; } set { sysControl.Estimateinvoicetitle = value; } }

        //[FwLogicProperty(Id:"2gYYoB1doevDK")]
        //public int? Noinvoicedays { get { return sysControl.Noinvoicedays; } set { sysControl.Noinvoicedays = value; } }

        //[FwLogicProperty(Id:"kTjYtCStyaCmo")]
        //public bool? Noinvoicecustomerstatus { get { return sysControl.Noinvoicecustomerstatus; } set { sysControl.Noinvoicecustomerstatus = value; } }

        //[FwLogicProperty(Id:"uLhA5hIHn91N9")]
        //public string NoinvoicecustomerstatusId { get { return sysControl.NoinvoicecustomerstatusId; } set { sysControl.NoinvoicecustomerstatusId = value; } }

        //[FwLogicProperty(Id:"cbHTKsdSB0N8E")]
        //public bool? Noinvoicedealstatus { get { return sysControl.Noinvoicedealstatus; } set { sysControl.Noinvoicedealstatus = value; } }

        //[FwLogicProperty(Id:"fv56ab9ILUxmY")]
        //public string NoinvoicedealstatusId { get { return sysControl.NoinvoicedealstatusId; } set { sysControl.NoinvoicedealstatusId = value; } }

        //[FwLogicProperty(Id:"uULkvAYlf320b")]
        //public int? Pendingexchangecolor { get { return sysControl.Pendingexchangecolor; } set { sysControl.Pendingexchangecolor = value; } }

        //[FwLogicProperty(Id:"ajbTrQktVeLXD")]
        //public string Repsignaturecaption { get { return sysControl.Repsignaturecaption; } set { sysControl.Repsignaturecaption = value; } }

        //[FwLogicProperty(Id:"whrfv1AyUE7ij")]
        //public bool? Warehousebuttonsonorder { get { return sysControl.Warehousebuttonsonorder; } set { sysControl.Warehousebuttonsonorder = value; } }

        //[FwLogicProperty(Id:"WvvTO7Os4Zgir")]
        //public bool? Warehousebuttonsonpo { get { return sysControl.Warehousebuttonsonpo; } set { sysControl.Warehousebuttonsonpo = value; } }

        //[FwLogicProperty(Id:"W8FCldrofGPPR")]
        //public bool? Warehousebuttonsontransfer { get { return sysControl.Warehousebuttonsontransfer; } set { sysControl.Warehousebuttonsontransfer = value; } }

        //[FwLogicProperty(Id:"Y0hNWVb7KPFv7")]
        //public bool? Warehousebuttonsontruck { get { return sysControl.Warehousebuttonsontruck; } set { sysControl.Warehousebuttonsontruck = value; } }

        //[FwLogicProperty(Id:"qV2qV9cvfQQzL")]
        //public string Pomasteraccrualthrough { get { return sysControl.Pomasteraccrualthrough; } set { sysControl.Pomasteraccrualthrough = value; } }

        //[FwLogicProperty(Id:"uStyrOk5xJgEe")]
        //public bool? Enablelocationfilter { get { return sysControl.Enablelocationfilter; } set { sysControl.Enablelocationfilter = value; } }

        //[FwLogicProperty(Id:"sV0b8RC3u1cbI")]
        //public bool? Physicalshowcounted { get { return sysControl.Physicalshowcounted; } set { sysControl.Physicalshowcounted = value; } }

        //[FwLogicProperty(Id:"2lDQzDe5aIr5L")]
        //public bool? Orderconfhidepayment { get { return sysControl.Orderconfhidepayment; } set { sysControl.Orderconfhidepayment = value; } }

        //[FwLogicProperty(Id:"DO3J2bGPCWqlt")]
        //public bool? Orderconfdisablepo { get { return sysControl.Orderconfdisablepo; } set { sysControl.Orderconfdisablepo = value; } }

        //[FwLogicProperty(Id:"2jrQurHLVHV1y")]
        //public bool? Orderconfdisablenote { get { return sysControl.Orderconfdisablenote; } set { sysControl.Orderconfdisablenote = value; } }

        //[FwLogicProperty(Id:"qxHiDY1srsWng")]
        //public bool? Orderconfrequiredeclinereason { get { return sysControl.Orderconfrequiredeclinereason; } set { sysControl.Orderconfrequiredeclinereason = value; } }

        //[FwLogicProperty(Id:"QdxasaVnMZbvz")]
        //public bool? Availcacheallwarehouses { get { return sysControl.Availcacheallwarehouses; } set { sysControl.Availcacheallwarehouses = value; } }

        //[FwLogicProperty(Id:"vUFxvhcE4r4d8")]
        //public bool? Availcalcunowneditems { get { return sysControl.Availcalcunowneditems; } set { sysControl.Availcalcunowneditems = value; } }

        //[FwLogicProperty(Id:"M4FPW9AQXI62o")]
        //public string Consfeesonflatpo { get { return sysControl.Consfeesonflatpo; } set { sysControl.Consfeesonflatpo = value; } }

        //[FwLogicProperty(Id:"OZHoJWpjsY06J")]
        //public bool? Showonlinetracking { get { return sysControl.Showonlinetracking; } set { sysControl.Showonlinetracking = value; } }

        //[FwLogicProperty(Id:"FsCC0dE538T43")]
        //public decimal? Defaultitemdiscountpct { get { return sysControl.Defaultitemdiscountpct; } set { sysControl.Defaultitemdiscountpct = value; } }

        //[FwLogicProperty(Id:"Wvvnh23FjxB4g")]
        //public bool? Defaultitemdiscounthiatus { get { return sysControl.Defaultitemdiscounthiatus; } set { sysControl.Defaultitemdiscounthiatus = value; } }

        //[FwLogicProperty(Id:"Q8utikXLPmot6")]
        //public bool? Defaultitemdiscountseparate { get { return sysControl.Defaultitemdiscountseparate; } set { sysControl.Defaultitemdiscountseparate = value; } }

        //[FwLogicProperty(Id:"ZMXsvwTvfAnin")]
        //public bool? Defaultproratefacilities { get { return sysControl.Defaultproratefacilities; } set { sysControl.Defaultproratefacilities = value; } }

        //[FwLogicProperty(Id:"8UAaAV0U1eBOX")]
        //public bool? Defaultallowbillschedoverride { get { return sysControl.Defaultallowbillschedoverride; } set { sysControl.Defaultallowbillschedoverride = value; } }

        //[FwLogicProperty(Id:"F3CydgmtJwJ6g")]
        //public bool? Defaultallowrebatecredits { get { return sysControl.Defaultallowrebatecredits; } set { sysControl.Defaultallowrebatecredits = value; } }

        //[FwLogicProperty(Id:"niZZAoXa5IAnt")]
        //public bool? Defaultorderoverridebillsched { get { return sysControl.Defaultorderoverridebillsched; } set { sysControl.Defaultorderoverridebillsched = value; } }

        //[FwLogicProperty(Id:"PdTE9dOVdewGv")]
        //public bool? Itemsinrooms { get { return sysControl.Itemsinrooms; } set { sysControl.Itemsinrooms = value; } }

        //[FwLogicProperty(Id:"i2YuqmJM6LJEE")]
        //public bool? Fiscaldaysweekenddefault { get { return sysControl.Fiscaldaysweekenddefault; } set { sysControl.Fiscaldaysweekenddefault = value; } }

        //[FwLogicProperty(Id:"BVaXJnL6s3oWy")]
        //public bool? Fiscaldaysholidaydefault { get { return sysControl.Fiscaldaysholidaydefault; } set { sysControl.Fiscaldaysholidaydefault = value; } }

        //[FwLogicProperty(Id:"BlhtEqbVnFwW2")]
        //public bool? Requireoriginalshow { get { return sysControl.Requireoriginalshow; } set { sysControl.Requireoriginalshow = value; } }

        //[FwLogicProperty(Id:"rOKGJcukCivMf")]
        //public int? Noavailcolor { get { return sysControl.Noavailcolor; } set { sysControl.Noavailcolor = value; } }

        //[FwLogicProperty(Id:"efzUvOU4MOMZY")]
        //public bool? Manuallyretireusedsales { get { return sysControl.Manuallyretireusedsales; } set { sysControl.Manuallyretireusedsales = value; } }

        //[FwLogicProperty(Id:"fWmoghwTZtORP")]
        //public string Lastpopulatepomasteraccruals { get { return sysControl.Lastpopulatepomasteraccruals; } set { sysControl.Lastpopulatepomasteraccruals = value; } }

        //[FwLogicProperty(Id:"hs7rDwbgkVUap")]
        //public bool? Facilitytypeincurrentlocation { get { return sysControl.Facilitytypeincurrentlocation; } set { sysControl.Facilitytypeincurrentlocation = value; } }

        //[FwLogicProperty(Id:"WyPuAIBGujegs")]
        //public bool? Defaultsaptype { get { return sysControl.Defaultsaptype; } set { sysControl.Defaultsaptype = value; } }

        //[FwLogicProperty(Id:"M0uzXKffdRvpp")]
        //public string Defaultsapcostobject { get { return sysControl.Defaultsapcostobject; } set { sysControl.Defaultsapcostobject = value; } }

        //[FwLogicProperty(Id:"j6Js8iavLNKtD")]
        //public string Defaultsapaccountno { get { return sysControl.Defaultsapaccountno; } set { sysControl.Defaultsapaccountno = value; } }

        //[FwLogicProperty(Id:"vZTYFVqGYEmmz")]
        //public string DefaultcustomerId { get { return sysControl.DefaultcustomerId; } set { sysControl.DefaultcustomerId = value; } }

        //[FwLogicProperty(Id:"Wd60QDr5KKP4d")]
        //public bool? Defaultusecustomerdiscount { get { return sysControl.Defaultusecustomerdiscount; } set { sysControl.Defaultusecustomerdiscount = value; } }

        //[FwLogicProperty(Id:"FBtYTm9lzkYTv")]
        //public string CustomerdefdiscounttemplateId { get { return sysControl.CustomerdefdiscounttemplateId; } set { sysControl.CustomerdefdiscounttemplateId = value; } }

        //[FwLogicProperty(Id:"ajLz8beaVrCD7")]
        //public bool? Allowconsignchangeicode { get { return sysControl.Allowconsignchangeicode; } set { sysControl.Allowconsignchangeicode = value; } }

        //[FwLogicProperty(Id:"rSvyxUA9vnKi5")]
        //public bool? Quoteordersearchbyaka { get { return sysControl.Quoteordersearchbyaka; } set { sysControl.Quoteordersearchbyaka = value; } }

        //[FwLogicProperty(Id:"OWvZvBKczWU1L")]
        //public bool? Candeletecontactwhenorderedby { get { return sysControl.Candeletecontactwhenorderedby; } set { sysControl.Candeletecontactwhenorderedby = value; } }

        //[FwLogicProperty(Id:"sIjvy3N7wIQHE")]
        //public int? Refreshquikactivity { get { return sysControl.Refreshquikactivity; } set { sysControl.Refreshquikactivity = value; } }

        //[FwLogicProperty(Id:"QkCLGXtPeU7Db")]
        //public bool? I35includepoprefix { get { return sysControl.I35includepoprefix; } set { sysControl.I35includepoprefix = value; } }

        //[FwLogicProperty(Id:"9xW5N8Gtp933T")]
        //public bool? Enableorderunit { get { return sysControl.Enableorderunit; } set { sysControl.Enableorderunit = value; } }

        //[FwLogicProperty(Id:"2oGgj49jR0vzd")]
        //public bool? Makequotedealanddescriptionunique { get { return sysControl.Makequotedealanddescriptionunique; } set { sysControl.Makequotedealanddescriptionunique = value; } }

        //[FwLogicProperty(Id:"Z9u1C0cZwAr7I")]
        //public bool? Enablecustomeractivityrestrictions { get { return sysControl.Enablecustomeractivityrestrictions; } set { sysControl.Enablecustomeractivityrestrictions = value; } }

        //[FwLogicProperty(Id:"uxnb3SmFxHKt2")]
        //public bool? Updatemanifestvalue { get { return sysControl.Updatemanifestvalue; } set { sysControl.Updatemanifestvalue = value; } }

        //[FwLogicProperty(Id:"LRg5jBK7LuqPY")]
        //public string CurrencyId { get { return sysControl.CurrencyId; } set { sysControl.CurrencyId = value; } }

        //[FwLogicProperty(Id:"7OzmjeHNB3InR")]
        //public bool? Receivemisclabor { get { return sysControl.Receivemisclabor; } set { sysControl.Receivemisclabor = value; } }

        //[FwLogicProperty(Id:"ignAmDgS4pfwu")]
        //public bool? Availenableqcdelay { get { return sysControl.Availenableqcdelay; } set { sysControl.Availenableqcdelay = value; } }

        //[FwLogicProperty(Id:"rAzRzxT3auike")]
        //public bool? Availqcdelayexcludeweekend { get { return sysControl.Availqcdelayexcludeweekend; } set { sysControl.Availqcdelayexcludeweekend = value; } }

        //[FwLogicProperty(Id:"P6guYSuu6Pn5s")]
        //public bool? Availqcdelayexcludeholiday { get { return sysControl.Availqcdelayexcludeholiday; } set { sysControl.Availqcdelayexcludeholiday = value; } }

        //[FwLogicProperty(Id:"S1jrUCdlAJAeT")]
        //public bool? Availqcdelayindefinite { get { return sysControl.Availqcdelayindefinite; } set { sysControl.Availqcdelayindefinite = value; } }

        //[FwLogicProperty(Id:"lNjmHVVeHPsng")]
        //public bool? I035summarizesr { get { return sysControl.I035summarizesr; } set { sysControl.I035summarizesr = value; } }

        //[FwLogicProperty(Id:"TQhqY8sCSBquW")]
        //public string DealdefaultpaytypeId { get { return sysControl.DealdefaultpaytypeId; } set { sysControl.DealdefaultpaytypeId = value; } }

        //[FwLogicProperty(Id:"LGRN8TFvUsqRO")]
        //public bool? Showfixedassetregister { get { return sysControl.Showfixedassetregister; } set { sysControl.Showfixedassetregister = value; } }

        //[FwLogicProperty(Id:"0E82SI7Ib5ezq")]
        //public bool? Editldcheckcurrency { get { return sysControl.Editldcheckcurrency; } set { sysControl.Editldcheckcurrency = value; } }

        //[FwLogicProperty(Id:"osBxnK9yFlacP")]
        //public bool? Editrepaircheckcurrency { get { return sysControl.Editrepaircheckcurrency; } set { sysControl.Editrepaircheckcurrency = value; } }

        //[FwLogicProperty(Id:"KJI3jkYmTOM01")]
        //public bool? Includenonbillable { get { return sysControl.Includenonbillable; } set { sysControl.Includenonbillable = value; } }

        [FwLogicProperty(Id: "FAUWH9brrKpvy")]
        public bool? CapsLock { get { return sysControl.CapsLock; } set { sysControl.CapsLock = value; } }


        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "DX9uPbVpvrpC5")]
        public string ReportLogoImageId { get { return webControl.ReportLogoImageId; } set { webControl.ReportLogoImageId = value; } }

        [FwLogicProperty(Id: "PIb9sMxpPM6wz")]
        public string ReportLogoImage { get; set; }

        [FwLogicProperty(Id: "WSt8FUWjtKL8I")]
        public int? ReportLogoImageHeight { get; set; }

        [FwLogicProperty(Id: "dNWR2lpvE2JLp")]
        public int? ReportLogoImageWidth { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "CHKY9htidEel")]
        public string DefaultContactGroupId { get { return webControl.DefaultContactGroupId; } set { webControl.DefaultContactGroupId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "OI5NyvH5W25h")]
        public string DefaultContactGroupName { get; set; }
        //------------------------------------------------------------------------------------ 
    */



        [FwLogicProperty(Id: "54tMuScs2fSPS")]
        public string DateStamp { get { return control.DateStamp; } set { control.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                isValid = false;
                validateMsg = "Cannot add new records to " + this.BusinessLogicModuleName;
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}

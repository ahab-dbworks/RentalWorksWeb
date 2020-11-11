using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.SystemSettings.SystemSettings
{
    [FwLogic(Id: "uhJu1bDJPDTRQ")]
    public class SystemSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ControlRecord control = new ControlRecord();
        SysControlRecord sysControl = new SysControlRecord();
        SystemSettingsLoader systemSettingsLoader = new SystemSettingsLoader();

        //------------------------------------------------------------------------------------ 
        public SystemSettingsLogic()
        {
            dataRecords.Add(control);
            dataRecords.Add(sysControl);
            dataLoader = systemSettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bb0kgMEmdIsAz", IsPrimaryKey: true)]
        public string SystemSettingsId { get { return control.ControlId; } set { control.ControlId = value;  sysControl.ControlId = value; } }

        [FwLogicProperty(Id: "MjkpoXIHIgjh4", IsRecordTitle: true)]
        public string SystemSettingsName { get { return "System Settings"; } }

        [FwLogicProperty(Id: "AnIwydk6lUHGC")]
        public string CompanyName { get { return control.Company; } set { control.Company = value; } }

        [FwLogicProperty(Id: "AC60eiSh5Pri8")]
        public string SystemName { get { return control.System; } set { control.System = value; } }

        [FwLogicProperty(Id: "mjQeIXHL6lIKJ")]
        public string DatabaseVersion { get { return control.Dbversion; } set { control.Dbversion = value; } }

        [FwLogicProperty(Id: "R1FCd2eL0sjEO")]
        public bool? ShareDealsAcrossOfficeLocations { get { return sysControl.ShareDealsAcrossOfficeLocations; } set { sysControl.ShareDealsAcrossOfficeLocations = value; } }

        [FwLogicProperty(Id: "NrvJDomVVcrcM")]
        public bool? IsVendorNumberAssignedByUser { get { return sysControl.IsVendorNumberAssignedByUser; } set { sysControl.IsVendorNumberAssignedByUser = value; } }

        [FwLogicProperty(Id: "CSiC9ZwpMLerq")]
        public int? LastVendorNumber { get { return sysControl.LastVendorNumber; } set { sysControl.LastVendorNumber = value; } }

        [FwLogicProperty(Id: "mxRFY4F28GOiA")]
        public bool? AllowDeleteExportedReceipts { get { return sysControl.AllowDeleteExportedReceipts; } set { sysControl.AllowDeleteExportedReceipts = value; } }

        [FwLogicProperty(Id: "XrAtztYJOWSQs")]
        public bool? EnableReceipts { get { return sysControl.EnableReceipts; } set { sysControl.EnableReceipts = value; } }

        [FwLogicProperty(Id: "dteBUFDHXpGHK")]
        public bool? EnableBetaUpdates { get { return sysControl.EnableBetaUpdates; } set { sysControl.EnableBetaUpdates = value; } }

        [FwLogicProperty(Id: "3OYrAZDic1nMx")]
        public bool? EnableQaUpdates { get { return sysControl.EnableQaUpdates; } set { sysControl.EnableQaUpdates = value; } }

        [FwLogicProperty(Id: "dImRdF8DPGzQ")]
        public bool? EnablePayments { get { return sysControl.EnablePayments; } set { sysControl.EnablePayments = value; } }

        [FwLogicProperty(Id: "LNSlJiSGmnjG")]
        public bool? AllowDeleteExportedPayments { get { return sysControl.AllowDeleteExportedPayments; } set { sysControl.AllowDeleteExportedPayments = value; } }

        [FwLogicProperty(Id: "XuNW5diLKlWu")]
        public bool? AllowDeleteInvoices { get { return sysControl.AllowDeleteInvoices; } set { sysControl.AllowDeleteInvoices = value; } }

        [FwLogicProperty(Id: "mYWrAlicPDZB")]
        public bool? AllowInvoiceDateChange { get { return sysControl.AllowInvoiceDateChange; } set { sysControl.AllowInvoiceDateChange = value; } }

        [FwLogicProperty(Id: "JBnuWS7lFnoQG")]
        public bool? OrdersCompleteWithItemsNotYetStaged { get { return sysControl.OrdersCompleteWithItemsNotYetStaged; } set { sysControl.OrdersCompleteWithItemsNotYetStaged = value; } }

        [FwLogicProperty(Id: "54tMuScs2fSPS")]
        public string DateStamp { get { return control.DateStamp; } set { control.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                isValid = false;
                validateMsg = "Cannot add new records to " + this.BusinessLogicModuleName;
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}

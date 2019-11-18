using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.LogoSettings
{
    [FwLogic(Id: "MntRC6TY09126")]
    public class LogoSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebControlRecord webControl = new WebControlRecord();
        LogoSettingsLoader logoSettingsLoader = new LogoSettingsLoader();

        //------------------------------------------------------------------------------------ 
        public LogoSettingsLogic()
        {
            dataRecords.Add(webControl);
            dataLoader = logoSettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "AKWdF40QgF8QH", IsPrimaryKey: true)]
        public string LogoSettingsId { get { return webControl.ControlId; } set { webControl.ControlId = value; } }

        [FwLogicProperty(Id: "e7oXaqLskaz8C", IsRecordTitle: true)]
        public string LogoSettingsName { get { return "Logo Settings"; } }

        [FwLogicProperty(Id: "nRf0qTwq1dHLZ")]
        public string LogoImageId { get { return webControl.ReportLogoImageId; } set { webControl.ReportLogoImageId = value; } }

        [FwLogicProperty(Id: "voFyRQtIiYJBQ")]
        public string LogoImage { get; set; }

        [FwLogicProperty(Id: "GDrXcUO2W9GnR")]
        public int? LogoImageHeight { get; set; }

        [FwLogicProperty(Id: "KcC7qC6m5M87I")]
        public int? LogoImageWidth { get; set; }

        [FwLogicProperty(Id: "0nsf8j1uh67h7")]
        public string DateStamp { get { return webControl.DateStamp; } set { webControl.DateStamp = value; } }
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

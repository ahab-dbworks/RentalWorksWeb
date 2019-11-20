using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;
using WebApi;

namespace WebApi.Modules.Settings.DocumentBarCodeSettings
{
    [FwLogic(Id: "HotqsnZYW3q9X")]
    public class DocumentBarCodeSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SysControlRecord sysControl = new SysControlRecord();
        DocumentBarCodeSettingsLoader documentBarCodeSettingsLoader = new DocumentBarCodeSettingsLoader();

        //------------------------------------------------------------------------------------ 
        public DocumentBarCodeSettingsLogic()
        {
            dataRecords.Add(sysControl);
            dataLoader = documentBarCodeSettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "QGImrzeiV7OLC", IsPrimaryKey: true)]
        public string DocumentBarCodeSettingsId { get { return sysControl.ControlId; } set { sysControl.ControlId = value; } }

        [FwLogicProperty(Id: "Iwt4EhWF8R8G8", IsRecordTitle: true)]
        public string DocumentBarCodeSettingsName { get { return "Document Bar Code Settings"; } }

        [FwLogicProperty(Id: "5bBdkvBam03Rq")]
        public string DocumentBarCodeStyle { get { return sysControl.DocumentBarCodeStyle; } set { sysControl.DocumentBarCodeStyle = value; } }

        [FwLogicProperty(Id: "3roEaqX1b2XJG")]
        public string DateStamp { get { return sysControl.DateStamp; } set { sysControl.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                isValid = false;
                validateMsg = "Cannot add new records to " + this.BusinessLogicModuleName;
            }

            if (isValid)
            {
                PropertyInfo property = typeof(DocumentBarCodeSettingsLogic).GetProperty(nameof(DocumentBarCodeSettingsLogic.DocumentBarCodeStyle));
                string[] acceptableValues = { RwConstants.BAR_CODE_STYLE_1D, RwConstants.BAR_CODE_STYLE_2D };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}

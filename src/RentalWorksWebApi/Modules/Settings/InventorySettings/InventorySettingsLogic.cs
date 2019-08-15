using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.InventorySettings
{
    [FwLogic(Id: "9tcMsqDFxmN11")]
    public class InventorySettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SysControlRecord sysControl = new SysControlRecord();
        InventorySettingsLoader inventorySettingsLoader = new InventorySettingsLoader();

        //------------------------------------------------------------------------------------ 
        public InventorySettingsLogic()
        {
            dataRecords.Add(sysControl);
            dataLoader = inventorySettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "GRycCnSNyPO18", IsPrimaryKey: true)]
        public string InventorySettingsId { get { return sysControl.ControlId; } set { sysControl.ControlId = value; } }

        [FwLogicProperty(Id: "oUiejSCAshZ26", IsRecordTitle: true)]
        public string InventorySettingsName { get { return "Inventory Settings"; } }

        [FwLogicProperty(Id: "d6hQNE6E9Sxag")]
        public string ICodeMask { get { return sysControl.Invmask; } set { sysControl.Invmask = value; } }

        [FwLogicProperty(Id: "v007HOEK0GyW4")]
        public bool? UserAssignedICodes { get { return sysControl.Userassignmasterno; } set { sysControl.Userassignmasterno = value; } }

        [FwLogicProperty(Id: "FTFg0VWQ9Iy6y")]
        public int? NextICode { get { return sysControl.Masterno; } set { sysControl.Masterno = value; } }

        [FwLogicProperty(Id: "ntSppR4GkjOc3")]
        public string ICodePrefix { get { return sysControl.Icodeprefix; } set { sysControl.Icodeprefix = value; } }

        [FwLogicProperty(Id: "E0PTF3DZXRoNX")]
        public string DateStamp { get { return sysControl.DateStamp; } set { sysControl.DateStamp = value; } }
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

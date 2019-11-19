using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebLibrary;
using WebApi.Modules.HomeControls.InventoryAvailability;

namespace WebApi.Modules.Settings.SystemSettings.AvailabilitySettings
{
    [FwLogic(Id: "Z6m6LSSDjZQoO")]
    public class AvailabilitySettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AvailabilitySettingsRecord availabilitySettings = new AvailabilitySettingsRecord();
        public AvailabilitySettingsLogic()
        {
            dataRecords.Add(availabilitySettings);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "MYekyvdKkYNtj", IsPrimaryKey: true)]
        public string ControlId { get { return availabilitySettings.ControlId; } set { availabilitySettings.ControlId = value; } }
        [FwLogicProperty(Id: "AX82wGx541Aro", IsReadOnly: true, IsRecordTitle: true)]
        public string AvailabilitySettings { get { return "Availability Settings"; } }
        [FwLogicProperty(Id: "QOFoP6kCZrMGX")]
        public int? PollForStaleAvailabilitySeconds { get { return availabilitySettings.PollForStaleAvailabilitySeconds; } set { availabilitySettings.PollForStaleAvailabilitySeconds = value; } }
        [FwLogicProperty(Id: "P13cxcyt5ET2T")]
        public bool? KeepAvailabilityCacheCurrent { get { return availabilitySettings.KeepAvailabilityCacheCurrent; } set { availabilitySettings.KeepAvailabilityCacheCurrent = value; } }
        [FwLogicProperty(Id: "cLtuI1Mlbrpil")]
        public int? KeepCurrentSeconds { get { return availabilitySettings.KeepCurrentSeconds; } set { availabilitySettings.KeepCurrentSeconds = value; } }
        [FwLogicProperty(Id: "FURV6hJjjsQVQ")]
        public string DateStamp { get { return availabilitySettings.DateStamp; } set { availabilitySettings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            AvailabilitySettingsLogic orig = null;

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    isValid = false;
                    validateMsg = "Cannot insert new Availability Settings.";
                }
            }

            if (isValid)
            {
                orig = (AvailabilitySettingsLogic)original;
            }

            if (isValid)
            {
                PropertyInfo property = typeof(AvailabilitySettingsLogic).GetProperty(nameof(AvailabilitySettingsLogic.PollForStaleAvailabilitySeconds));
                int? pollForStaleAvailabilitySeconds = PollForStaleAvailabilitySeconds ?? orig.PollForStaleAvailabilitySeconds;
                isValid = (pollForStaleAvailabilitySeconds.GetValueOrDefault(0) > 0);
                if (!isValid)
                {
                    validateMsg = "Invalid " + property.Name + ": " + PollForStaleAvailabilitySeconds.ToString() + ".  Value must be greater than zero.";
                }
            }

            if (isValid)
            {
                bool keepCacheCurrent = (KeepAvailabilityCacheCurrent ?? orig.KeepAvailabilityCacheCurrent).GetValueOrDefault(false);
                if (keepCacheCurrent)
                {
                    PropertyInfo property = typeof(AvailabilitySettingsLogic).GetProperty(nameof(AvailabilitySettingsLogic.KeepCurrentSeconds));
                    int? keepCurrentSeconds = KeepCurrentSeconds ?? orig.KeepCurrentSeconds;
                    isValid = (keepCurrentSeconds.GetValueOrDefault(0) > 0);
                    if (!isValid)
                    {
                        validateMsg = "Invalid " + property.Name + ": " + KeepCurrentSeconds.ToString() + ".  Value must be greater than zero.";
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.SystemNumber
{
    [FwLogic(Id: "AwxOVRsijWuF8")]
    public class SystemNumberLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SystemNumberRecord systemNumber = new SystemNumberRecord();
        public SystemNumberLogic()
        {
            dataRecords.Add(systemNumber);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "aWyjWI1wt1yx8", IsPrimaryKey: true)]
        public string SystemNumberId { get { return systemNumber.SystemNumberId; } set { systemNumber.SystemNumberId = value; } }
        [FwLogicProperty(Id: "ay1yxhdBmcrwH", DisableDirectModify: true)]
        public string OfficeLocationId { get { return systemNumber.OfficeLocationId; } set { systemNumber.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "AyoOldF5V6xQN", DisableDirectModify: true)]
        public string Module { get { return systemNumber.Module; } set { systemNumber.Module = value; } }
        [FwLogicProperty(Id: "azKjqTR5ulBgo")]
        public bool? IsAssignByUser { get { return systemNumber.IsAssignByUser; } set { systemNumber.IsAssignByUser = value; } }
        [FwLogicProperty(Id: "azkMkX9SdwzSd")]
        public int? Counter { get { return systemNumber.Counter; } set { systemNumber.Counter = value; } }
        [FwLogicProperty(Id: "B0BX4wkNcujl5")]
        public int? Increment { get { return systemNumber.Increment; } set { systemNumber.Increment = value; } }
        [FwLogicProperty(Id: "B0FN4YSPQFF4s")]
        public string DateStamp { get { return systemNumber.DateStamp; } set { systemNumber.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}

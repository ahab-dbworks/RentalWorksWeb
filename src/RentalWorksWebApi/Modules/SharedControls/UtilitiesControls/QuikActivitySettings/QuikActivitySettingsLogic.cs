using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.UtilitiesControls.QuikActivitySettings
{
    [FwLogic(Id: "3BG1PpJauKT2")]
    public class QuikActivitySettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        QuikActivitySettingsRecord quikActivitySettings = new QuikActivitySettingsRecord();
        public QuikActivitySettingsLogic()
        {
            dataRecords.Add(quikActivitySettings);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "3BLXvLqNUHVM", IsPrimaryKey: true)]
        public int? Id { get { return quikActivitySettings.Id; } set { quikActivitySettings.Id = value; } }
        [FwLogicProperty(Id: "3COkWZC39Wh3")]
        public string WebUsersId { get { return quikActivitySettings.WebUsersId; } set { quikActivitySettings.WebUsersId = value; } }
        [FwLogicProperty(Id: "3DddrThpuooP")]
        public string Description { get { return quikActivitySettings.Description; } set { quikActivitySettings.Description = value; } }
        [FwLogicProperty(Id: "3DXt6O1rZdNLl")]
        public string Settings { get { return quikActivitySettings.Settings; } set { quikActivitySettings.Settings = value; } }
        [FwLogicProperty(Id: "3EEShYLYfD9f")]
        public string DateStamp { get { return quikActivitySettings.DateStamp; } set { quikActivitySettings.DateStamp = value; } }
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

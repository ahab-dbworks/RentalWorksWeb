using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeLabel
{
    [FwLogic(Id:"GlWN2vYS19sn6")]
    public class WardrobeLabelLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeLabelRecord wardrobeLabel = new WardrobeLabelRecord();
        public WardrobeLabelLogic()
        {
            dataRecords.Add(wardrobeLabel);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"5ku6RYz6FLtrK", IsPrimaryKey:true)]
        public string WardrobeLabelId { get { return wardrobeLabel.WardrobeLabelId; } set { wardrobeLabel.WardrobeLabelId = value; } }

        [FwLogicProperty(Id:"5ku6RYz6FLtrK", IsRecordTitle:true)]
        public string WardrobeLabel { get { return wardrobeLabel.WardrobeLabel; } set { wardrobeLabel.WardrobeLabel = value; } }

        [FwLogicProperty(Id:"NfWOo4687JPj")]
        public bool? Inactive { get { return wardrobeLabel.Inactive; } set { wardrobeLabel.Inactive = value; } }

        [FwLogicProperty(Id:"JRJi9SWQONwr")]
        public string DateStamp { get { return wardrobeLabel.DateStamp; } set { wardrobeLabel.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

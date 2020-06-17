using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.UserSettings.Sound
{
    [FwLogic(Id:"BaBkE3l3wT0PS")]
    public class SoundLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SoundRecord sound = new SoundRecord();
        SoundLoader soundLoader = new SoundLoader();

        public SoundLogic()
        {
            dataRecords.Add(sound);
            dataLoader = soundLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"BxnmypNOOR1km", IsPrimaryKey:true)]
        public string SoundId { get { return sound.SoundId; } set { sound.SoundId = value; } }

        [FwLogicProperty(Id:"BxnmypNOOR1km", IsRecordTitle:true)]
        public string Sound { get { return sound.Sound; } set { sound.Sound = value; } }

        [FwLogicProperty(Id: "m9qb4BHJp59YS")]
        public string Blob { get { return sound.Blob; } set { sound.Blob = value; } }

        [FwLogicProperty(Id:"eATtkRbvm9BL")]
        public string FileName { get { return sound.FileName; } set { sound.FileName = value; } }

        [FwLogicProperty(Id:"ZsajC9EwX1lp")]
        public bool? SystemSound { get { return sound.SystemSound; } set { sound.SystemSound = value; } }

        [FwLogicProperty(Id:"Bo7JeC3jrWzI")]
        public string SoundColor { get; set; }

        [FwLogicProperty(Id:"Go76mPvD6yLP")]
        public string DateStamp { get { return sound.DateStamp; } set { sound.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

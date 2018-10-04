using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Sound
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SoundId { get { return sound.SoundId; } set { sound.SoundId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Sound { get { return sound.Sound; } set { sound.Sound = value; } }
        public string FileName { get { return sound.FileName; } set { sound.FileName = value; } }
        public bool? SystemSound { get { return sound.SystemSound; } set { sound.SystemSound = value; } }
        public string SoundColor { get; set; }
        public string DateStamp { get { return sound.DateStamp; } set { sound.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

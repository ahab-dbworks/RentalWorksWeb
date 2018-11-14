using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SetOpening
{
    [FwLogic(Id:"2m4MQpNQoImRH")]
    public class SetOpeningLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SetOpeningRecord setOpening = new SetOpeningRecord();
        public SetOpeningLogic()
        {
            dataRecords.Add(setOpening);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"IEEeCp7H9kBgx", IsPrimaryKey:true)]
        public string SetOpeningId { get { return setOpening.SetOpeningId; } set { setOpening.SetOpeningId = value; } }

        [FwLogicProperty(Id:"IEEeCp7H9kBgx", IsRecordTitle:true)]
        public string SetOpening { get { return setOpening.SetOpening; } set { setOpening.SetOpening = value; } }

        [FwLogicProperty(Id:"ae45P6rcIqDu")]
        public bool? Inactive { get { return setOpening.Inactive; } set { setOpening.Inactive = value; } }

        [FwLogicProperty(Id:"hiyLLZgYOjxo")]
        public string DateStamp { get { return setOpening.DateStamp; } set { setOpening.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

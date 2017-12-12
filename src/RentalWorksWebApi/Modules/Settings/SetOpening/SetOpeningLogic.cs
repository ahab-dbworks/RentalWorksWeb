using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SetOpening
{
    public class SetOpeningLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SetOpeningRecord setOpening = new SetOpeningRecord();
        public SetOpeningLogic()
        {
            dataRecords.Add(setOpening);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SetOpeningId { get { return setOpening.SetOpeningId; } set { setOpening.SetOpeningId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SetOpening { get { return setOpening.SetOpening; } set { setOpening.SetOpening = value; } }
        public bool? Inactive { get { return setOpening.Inactive; } set { setOpening.Inactive = value; } }
        public string DateStamp { get { return setOpening.DateStamp; } set { setOpening.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}

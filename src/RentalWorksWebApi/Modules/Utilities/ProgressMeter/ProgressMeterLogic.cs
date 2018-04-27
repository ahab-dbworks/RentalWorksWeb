using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Utilities.ProgressMeter
{
    public class ProgressMeterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProgressMeterRecord progressMeter = new ProgressMeterRecord();
        public ProgressMeterLogic()
        {
            dataRecords.Add(progressMeter);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SessionId { get { return progressMeter.SessionId; } set { progressMeter.SessionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Caption { get { return progressMeter.Caption; } set { progressMeter.Caption = value; } }
        public int? CurrentStep { get { return progressMeter.CurrentStep; } set { progressMeter.CurrentStep = value; } }
        public int? TotalSteps { get { return progressMeter.TotalSteps; } set { progressMeter.TotalSteps = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
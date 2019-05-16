using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.Utilities.ProgressMeter
{
    [FwLogic(Id:"uTM8pjYl5uZri")]
    public class ProgressMeterLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProgressMeterRecord progressMeter = new ProgressMeterRecord();
        public ProgressMeterLogic()
        {
            dataRecords.Add(progressMeter);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"TV1BHg5X9QRDf", IsPrimaryKey:true)]
        public string SessionId { get { return progressMeter.SessionId; } set { progressMeter.SessionId = value; } }

        [FwLogicProperty(Id:"Ct97Izs3oudHv", IsRecordTitle:true)]
        public string Caption { get { return progressMeter.Caption; } set { progressMeter.Caption = value; } }

        [FwLogicProperty(Id:"A92vmQeBp6P")]
        public int? CurrentStep { get { return progressMeter.CurrentStep; } set { progressMeter.CurrentStep = value; } }

        [FwLogicProperty(Id:"N67quCXAtC08")]
        public int? TotalSteps { get { return progressMeter.TotalSteps; } set { progressMeter.TotalSteps = value; } }

        [FwLogicProperty(Id: "zabf3gXddnXSH", IsReadOnly: true)]
        public int? PercentComplete
        {
            get
            {
                int p = 0;
                if (TotalSteps.GetValueOrDefault(0) != 0)
                {
                    decimal currentStepDec = (decimal)CurrentStep;
                    decimal totalStepsDec = (decimal)TotalSteps;
                    decimal oneHundred = 100;
                    p = (int)(oneHundred * (currentStepDec / totalStepsDec));
                }
                return p;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

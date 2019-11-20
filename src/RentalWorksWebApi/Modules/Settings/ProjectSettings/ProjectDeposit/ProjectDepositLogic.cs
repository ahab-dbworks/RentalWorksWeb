using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDeposit
{
    [FwLogic(Id:"u0Ho1ODhdCb8w")]
    public class ProjectDepositLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDepositRecord projectDeposit = new ProjectDepositRecord();
        public ProjectDepositLogic()
        {
            dataRecords.Add(projectDeposit);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"f9R7YXP5bccIt", IsPrimaryKey:true)]
        public string ProjectDepositId { get { return projectDeposit.ProjectDepositId; } set { projectDeposit.ProjectDepositId = value; } }

        [FwLogicProperty(Id:"f9R7YXP5bccIt", IsRecordTitle:true)]
        public string ProjectDeposit { get { return projectDeposit.ProjectDeposit; } set { projectDeposit.ProjectDeposit = value; } }

        [FwLogicProperty(Id:"JVlrLExyxcXF")]
        public bool? Inactive { get { return projectDeposit.Inactive; } set { projectDeposit.Inactive = value; } }

        [FwLogicProperty(Id:"bSPGKQTRlS8g")]
        public string DateStamp { get { return projectDeposit.DateStamp; } set { projectDeposit.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

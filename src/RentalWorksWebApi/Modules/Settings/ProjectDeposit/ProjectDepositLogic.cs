using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.ProjectDeposit
{
    public class ProjectDepositLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDepositRecord projectDeposit = new ProjectDepositRecord();
        public ProjectDepositLogic()
        {
            dataRecords.Add(projectDeposit);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectDepositId { get { return projectDeposit.ProjectDepositId; } set { projectDeposit.ProjectDepositId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectDeposit { get { return projectDeposit.ProjectDeposit; } set { projectDeposit.ProjectDeposit = value; } }
        public bool Inactive { get { return projectDeposit.Inactive; } set { projectDeposit.Inactive = value; } }
        public string DateStamp { get { return projectDeposit.DateStamp; } set { projectDeposit.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
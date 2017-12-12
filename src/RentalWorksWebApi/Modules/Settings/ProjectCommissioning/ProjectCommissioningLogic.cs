using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectCommissioning
{
    public class ProjectCommissioningLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectCommissioningRecord projectCommissioning = new ProjectCommissioningRecord();
        public ProjectCommissioningLogic()
        {
            dataRecords.Add(projectCommissioning);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectCommissioningId { get { return projectCommissioning.ProjectCommissioningId; } set { projectCommissioning.ProjectCommissioningId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectCommissioning { get { return projectCommissioning.ProjectCommissioning; } set { projectCommissioning.ProjectCommissioning = value; } }
        public bool? Inactive { get { return projectCommissioning.Inactive; } set { projectCommissioning.Inactive = value; } }
        public string DateStamp { get { return projectCommissioning.DateStamp; } set { projectCommissioning.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
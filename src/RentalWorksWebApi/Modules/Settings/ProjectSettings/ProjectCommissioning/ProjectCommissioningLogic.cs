using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectCommissioning
{
    [FwLogic(Id:"qpj0egEFyvLP5")]
    public class ProjectCommissioningLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectCommissioningRecord projectCommissioning = new ProjectCommissioningRecord();
        public ProjectCommissioningLogic()
        {
            dataRecords.Add(projectCommissioning);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"U1JbNod3dpRSe", IsPrimaryKey:true)]
        public string ProjectCommissioningId { get { return projectCommissioning.ProjectCommissioningId; } set { projectCommissioning.ProjectCommissioningId = value; } }

        [FwLogicProperty(Id:"U1JbNod3dpRSe", IsRecordTitle:true)]
        public string ProjectCommissioning { get { return projectCommissioning.ProjectCommissioning; } set { projectCommissioning.ProjectCommissioning = value; } }

        [FwLogicProperty(Id:"ju0sRYmzgTjL")]
        public bool? Inactive { get { return projectCommissioning.Inactive; } set { projectCommissioning.Inactive = value; } }

        [FwLogicProperty(Id:"npbOuCjyimXD")]
        public string DateStamp { get { return projectCommissioning.DateStamp; } set { projectCommissioning.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.ProjectAsBuild
{
    public class ProjectAsBuildLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectAsBuildRecord projectAsBuild = new ProjectAsBuildRecord();
        public ProjectAsBuildLogic()
        {
            dataRecords.Add(projectAsBuild);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectAsBuildId { get { return projectAsBuild.ProjectAsBuildId; } set { projectAsBuild.ProjectAsBuildId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectAsBuild { get { return projectAsBuild.ProjectAsBuild; } set { projectAsBuild.ProjectAsBuild = value; } }
        public bool Inactive { get { return projectAsBuild.Inactive; } set { projectAsBuild.Inactive = value; } }
        public string DateStamp { get { return projectAsBuild.DateStamp; } set { projectAsBuild.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
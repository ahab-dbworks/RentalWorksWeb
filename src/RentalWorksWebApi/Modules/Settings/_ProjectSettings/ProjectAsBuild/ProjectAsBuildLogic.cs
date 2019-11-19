using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectAsBuild
{
    [FwLogic(Id:"Mj6JHlcmlnoaU")]
    public class ProjectAsBuildLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectAsBuildRecord projectAsBuild = new ProjectAsBuildRecord();
        public ProjectAsBuildLogic()
        {
            dataRecords.Add(projectAsBuild);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"0R5liHxL2yuAT", IsPrimaryKey:true)]
        public string ProjectAsBuildId { get { return projectAsBuild.ProjectAsBuildId; } set { projectAsBuild.ProjectAsBuildId = value; } }

        [FwLogicProperty(Id:"0R5liHxL2yuAT", IsRecordTitle:true)]
        public string ProjectAsBuild { get { return projectAsBuild.ProjectAsBuild; } set { projectAsBuild.ProjectAsBuild = value; } }

        [FwLogicProperty(Id:"Yza6XUAS3pf4")]
        public bool? Inactive { get { return projectAsBuild.Inactive; } set { projectAsBuild.Inactive = value; } }

        [FwLogicProperty(Id:"8O9tEyRSf9tY")]
        public string DateStamp { get { return projectAsBuild.DateStamp; } set { projectAsBuild.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

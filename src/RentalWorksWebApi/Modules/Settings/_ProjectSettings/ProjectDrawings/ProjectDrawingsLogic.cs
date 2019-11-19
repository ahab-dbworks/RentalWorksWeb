using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDrawings
{
    [FwLogic(Id:"ywwlGkGhMp8pq")]
    public class ProjectDrawingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDrawingsRecord projectDrawings = new ProjectDrawingsRecord();
        public ProjectDrawingsLogic()
        {
            dataRecords.Add(projectDrawings);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qLCJciQsmRl0z", IsPrimaryKey:true)]
        public string ProjectDrawingsId { get { return projectDrawings.ProjectDrawingsId; } set { projectDrawings.ProjectDrawingsId = value; } }

        [FwLogicProperty(Id:"qLCJciQsmRl0z", IsRecordTitle:true)]
        public string ProjectDrawings { get { return projectDrawings.ProjectDrawings; } set { projectDrawings.ProjectDrawings = value; } }

        [FwLogicProperty(Id:"kayQhqC6mxD8")]
        public bool? Inactive { get { return projectDrawings.Inactive; } set { projectDrawings.Inactive = value; } }

        [FwLogicProperty(Id:"BpWAbuwRMBB7")]
        public string DateStamp { get { return projectDrawings.DateStamp; } set { projectDrawings.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

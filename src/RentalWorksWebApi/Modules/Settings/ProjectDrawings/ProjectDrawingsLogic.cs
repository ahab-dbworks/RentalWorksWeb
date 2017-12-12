using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectDrawings
{
    public class ProjectDrawingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDrawingsRecord projectDrawings = new ProjectDrawingsRecord();
        public ProjectDrawingsLogic()
        {
            dataRecords.Add(projectDrawings);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectDrawingsId { get { return projectDrawings.ProjectDrawingsId; } set { projectDrawings.ProjectDrawingsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectDrawings { get { return projectDrawings.ProjectDrawings; } set { projectDrawings.ProjectDrawings = value; } }
        public bool? Inactive { get { return projectDrawings.Inactive; } set { projectDrawings.Inactive = value; } }
        public string DateStamp { get { return projectDrawings.DateStamp; } set { projectDrawings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
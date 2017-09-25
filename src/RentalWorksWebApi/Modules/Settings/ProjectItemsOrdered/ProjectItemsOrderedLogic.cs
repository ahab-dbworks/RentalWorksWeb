using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.ProjectItemsOrdered
{
    public class ProjectItemsOrderedLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectItemsOrderedRecord projectItemsOrdered = new ProjectItemsOrderedRecord();
        public ProjectItemsOrderedLogic()
        {
            dataRecords.Add(projectItemsOrdered);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectItemsOrderedId { get { return projectItemsOrdered.ProjectItemsOrderedId; } set { projectItemsOrdered.ProjectItemsOrderedId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectItemsOrdered { get { return projectItemsOrdered.ProjectItemsOrdered; } set { projectItemsOrdered.ProjectItemsOrdered = value; } }
        public bool Inactive { get { return projectItemsOrdered.Inactive; } set { projectItemsOrdered.Inactive = value; } }
        public string DateStamp { get { return projectItemsOrdered.DateStamp; } set { projectItemsOrdered.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
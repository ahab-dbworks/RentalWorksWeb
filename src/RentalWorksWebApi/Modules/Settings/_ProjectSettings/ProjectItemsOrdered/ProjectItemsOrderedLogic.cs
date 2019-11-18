using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectItemsOrdered
{
    [FwLogic(Id:"G68bAi6lbvzfq")]
    public class ProjectItemsOrderedLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectItemsOrderedRecord projectItemsOrdered = new ProjectItemsOrderedRecord();
        public ProjectItemsOrderedLogic()
        {
            dataRecords.Add(projectItemsOrdered);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"rpkH9QASRAj7l", IsPrimaryKey:true)]
        public string ProjectItemsOrderedId { get { return projectItemsOrdered.ProjectItemsOrderedId; } set { projectItemsOrdered.ProjectItemsOrderedId = value; } }

        [FwLogicProperty(Id:"rpkH9QASRAj7l", IsRecordTitle:true)]
        public string ProjectItemsOrdered { get { return projectItemsOrdered.ProjectItemsOrdered; } set { projectItemsOrdered.ProjectItemsOrdered = value; } }

        [FwLogicProperty(Id:"A5H6Yb5juTJK")]
        public bool? Inactive { get { return projectItemsOrdered.Inactive; } set { projectItemsOrdered.Inactive = value; } }

        [FwLogicProperty(Id:"85bzSuJ6uo5X")]
        public string DateStamp { get { return projectItemsOrdered.DateStamp; } set { projectItemsOrdered.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectDropShipItems
{
    public class ProjectDropShipItemsLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDropShipItemsRecord projectDropShipItems = new ProjectDropShipItemsRecord();
        public ProjectDropShipItemsLogic()
        {
            dataRecords.Add(projectDropShipItems);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectDropShipItemsId { get { return projectDropShipItems.ProjectDropShipItemsId; } set { projectDropShipItems.ProjectDropShipItemsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectDropShipItems { get { return projectDropShipItems.ProjectDropShipItems; } set { projectDropShipItems.ProjectDropShipItems = value; } }
        public bool? Inactive { get { return projectDropShipItems.Inactive; } set { projectDropShipItems.Inactive = value; } }
        public string DateStamp { get { return projectDropShipItems.DateStamp; } set { projectDropShipItems.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
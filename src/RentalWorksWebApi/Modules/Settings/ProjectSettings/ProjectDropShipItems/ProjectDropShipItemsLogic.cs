using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDropShipItems
{
    [FwLogic(Id:"oqUi44jNq91eq")]
    public class ProjectDropShipItemsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ProjectDropShipItemsRecord projectDropShipItems = new ProjectDropShipItemsRecord();
        public ProjectDropShipItemsLogic()
        {
            dataRecords.Add(projectDropShipItems);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"bYFzWgjXSVjus", IsPrimaryKey:true)]
        public string ProjectDropShipItemsId { get { return projectDropShipItems.ProjectDropShipItemsId; } set { projectDropShipItems.ProjectDropShipItemsId = value; } }

        [FwLogicProperty(Id:"bYFzWgjXSVjus", IsRecordTitle:true)]
        public string ProjectDropShipItems { get { return projectDropShipItems.ProjectDropShipItems; } set { projectDropShipItems.ProjectDropShipItems = value; } }

        [FwLogicProperty(Id:"J8WSaxWg23nB")]
        public bool? Inactive { get { return projectDropShipItems.Inactive; } set { projectDropShipItems.Inactive = value; } }

        [FwLogicProperty(Id:"dGz53lpCBvCb")]
        public string DateStamp { get { return projectDropShipItems.DateStamp; } set { projectDropShipItems.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

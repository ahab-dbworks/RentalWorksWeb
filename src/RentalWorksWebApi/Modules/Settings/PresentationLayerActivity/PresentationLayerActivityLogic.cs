using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    public class PresentationLayerActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerActivityRecord presentationLayerActivity = new PresentationLayerActivityRecord();
        PresentationLayerActivityLoader presentationLayerActivityLoader = new PresentationLayerActivityLoader();
        public PresentationLayerActivityLogic()
        {
            dataRecords.Add(presentationLayerActivity);
            dataLoader = presentationLayerActivityLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PresentationLayerActivityId { get { return presentationLayerActivity.PresentationLayerActivityId; } set { presentationLayerActivity.PresentationLayerActivityId = value; } }
        public string PresentationLayerId { get { return presentationLayerActivity.PresentationLayerId; } set { presentationLayerActivity.PresentationLayerId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Activity { get { return presentationLayerActivity.Activity; } set { presentationLayerActivity.Activity = value; } }
        public string ActivityRename { get { return presentationLayerActivity.ActivityRename; } set { presentationLayerActivity.ActivityRename = value; } }
        public string GroupNo { get { return presentationLayerActivity.GroupNo; } set { presentationLayerActivity.GroupNo = value; } }
        public string RecType { get { return presentationLayerActivity.RecType; } set { presentationLayerActivity.RecType = value; } }
        public string ExportCode { get { return presentationLayerActivity.ExportCode; } set { presentationLayerActivity.ExportCode = value; } }
        public decimal? OrderBy { get { return presentationLayerActivity.OrderBy; } set { presentationLayerActivity.OrderBy = value; } }
        public string DateStamp { get { return presentationLayerActivity.DateStamp; } set { presentationLayerActivity.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
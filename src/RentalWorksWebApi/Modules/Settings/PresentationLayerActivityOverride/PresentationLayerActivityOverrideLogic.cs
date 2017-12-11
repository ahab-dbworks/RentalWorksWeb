using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    public class PresentationLayerActivityOverrideLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerActivityOverrideRecord presentationLayerActivityOverride = new PresentationLayerActivityOverrideRecord();
        PresentationLayerActivityOverrideLoader presentationLayerActivityOverrideLoader = new PresentationLayerActivityOverrideLoader();
        public PresentationLayerActivityOverrideLogic()
        {
            dataRecords.Add(presentationLayerActivityOverride);
            dataLoader = presentationLayerActivityOverrideLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PresentationLayerActivityOverrideId { get { return presentationLayerActivityOverride.PresentationLayerActivityOverrideId; } set { presentationLayerActivityOverride.PresentationLayerActivityOverrideId = value; } }
        public string PresentationLayerId { get { return presentationLayerActivityOverride.PresentationLayerId; } set { presentationLayerActivityOverride.PresentationLayerId = value; } }
        public string PresentationLayerActivityId { get { return presentationLayerActivityOverride.PresentationLayerActivityId; } set { presentationLayerActivityOverride.PresentationLayerActivityId = value; } }
        public string MasterId { get { return presentationLayerActivityOverride.MasterId; } set { presentationLayerActivityOverride.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Activity { get; set; }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ActivityRename { get; set; }
        public string ExportCode { get { return presentationLayerActivityOverride.ExportCode; } set { presentationLayerActivityOverride.ExportCode = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
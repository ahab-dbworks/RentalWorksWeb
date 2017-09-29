using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.PresentationLayerActivityOverride
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
        public int Id { get { return presentationLayerActivityOverride.Id; } set { presentationLayerActivityOverride.Id = value; } }
        [FwBusinessLogicField(isPrimaryKey: true, isPrimaryKeyOptional: true)]
        public string InternalChar { get { return presentationLayerActivityOverride.InternalChar; } set { presentationLayerActivityOverride.InternalChar = value; } }
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
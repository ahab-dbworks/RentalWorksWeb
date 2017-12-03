using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.PresentationLayer
{
    public class PresentationLayerLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerRecord presentationLayer = new PresentationLayerRecord();
        public PresentationLayerLogic()
        {
            dataRecords.Add(presentationLayer);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PresentationLayerId { get { return presentationLayer.PresentationLayerId; } set { presentationLayer.PresentationLayerId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PresentationLayer { get { return presentationLayer.PresentationLayer; } set { presentationLayer.PresentationLayer = value; } }
        public bool? Inactive { get { return presentationLayer.Inactive; } set { presentationLayer.Inactive = value; } }
        public string DateStamp { get { return presentationLayer.DateStamp; } set { presentationLayer.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
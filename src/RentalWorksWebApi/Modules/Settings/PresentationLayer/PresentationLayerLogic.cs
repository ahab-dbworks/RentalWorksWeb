using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PresentationLayer
{
    [FwLogic(Id:"3EY8IbeYdEU9q")]
    public class PresentationLayerLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerRecord presentationLayer = new PresentationLayerRecord();
        public PresentationLayerLogic()
        {
            dataRecords.Add(presentationLayer);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"yhWJJi0LU84kw", IsPrimaryKey:true)]
        public string PresentationLayerId { get { return presentationLayer.PresentationLayerId; } set { presentationLayer.PresentationLayerId = value; } }

        [FwLogicProperty(Id:"yhWJJi0LU84kw", IsRecordTitle:true)]
        public string PresentationLayer { get { return presentationLayer.PresentationLayer; } set { presentationLayer.PresentationLayer = value; } }

        [FwLogicProperty(Id:"PSg4MzJ1uecy")]
        public bool? Inactive { get { return presentationLayer.Inactive; } set { presentationLayer.Inactive = value; } }

        [FwLogicProperty(Id:"BpKAJb2uj7R4")]
        public string DateStamp { get { return presentationLayer.DateStamp; } set { presentationLayer.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

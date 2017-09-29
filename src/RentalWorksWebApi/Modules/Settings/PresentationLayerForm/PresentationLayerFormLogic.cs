using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.PresentationLayerForm
{
    public class PresentationLayerFormLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerFormRecord presentationLayerForm = new PresentationLayerFormRecord();
        public PresentationLayerFormLogic()
        {
            dataRecords.Add(presentationLayerForm);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PresentationLayerFormId { get { return presentationLayerForm.PresentationLayerFormId; } set { presentationLayerForm.PresentationLayerFormId = value; } }
        public string PresentationLayerId { get { return presentationLayerForm.PresentationLayerId; } set { presentationLayerForm.PresentationLayerId = value; } }
        public string FormType { get { return presentationLayerForm.FormType; } set { presentationLayerForm.FormType = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string FormTitle { get { return presentationLayerForm.FormTitle; } set { presentationLayerForm.FormTitle = value; } }
        public string DateStamp { get { return presentationLayerForm.DateStamp; } set { presentationLayerForm.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
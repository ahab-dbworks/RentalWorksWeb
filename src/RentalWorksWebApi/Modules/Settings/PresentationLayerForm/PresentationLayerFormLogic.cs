using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PresentationLayerForm
{
    [FwLogic(Id:"VgtpOsaHh4fNE")]
    public class PresentationLayerFormLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerFormRecord presentationLayerForm = new PresentationLayerFormRecord();
        public PresentationLayerFormLogic()
        {
            dataRecords.Add(presentationLayerForm);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"fdkiNfvcx2F6D", IsPrimaryKey:true)]
        public string PresentationLayerFormId { get { return presentationLayerForm.PresentationLayerFormId; } set { presentationLayerForm.PresentationLayerFormId = value; } }

        [FwLogicProperty(Id:"6wpX0lm3tBsa")]
        public string PresentationLayerId { get { return presentationLayerForm.PresentationLayerId; } set { presentationLayerForm.PresentationLayerId = value; } }

        [FwLogicProperty(Id:"VTpianESfA2Q")]
        public string FormType { get { return presentationLayerForm.FormType; } set { presentationLayerForm.FormType = value; } }

        [FwLogicProperty(Id:"tGzgPUvC80Eq7", IsRecordTitle:true)]
        public string FormTitle { get { return presentationLayerForm.FormTitle; } set { presentationLayerForm.FormTitle = value; } }

        [FwLogicProperty(Id:"mvNWTup4WO6Q")]
        public string DateStamp { get { return presentationLayerForm.DateStamp; } set { presentationLayerForm.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

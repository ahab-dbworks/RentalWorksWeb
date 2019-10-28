using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [FwLogic(Id:"Zbmz7vbdw2N57")]
    public class PresentationLayerActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PresentationLayerActivityRecord presentationLayerActivity = new PresentationLayerActivityRecord();
        PresentationLayerActivityLoader presentationLayerActivityLoader = new PresentationLayerActivityLoader();
        public PresentationLayerActivityLogic()
        {
            dataRecords.Add(presentationLayerActivity);
            dataLoader = presentationLayerActivityLoader;
            BeforeValidate += OnBeforeValidate;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"WkTnKDo8j7nKS", IsPrimaryKey:true)]
        public string PresentationLayerActivityId { get { return presentationLayerActivity.PresentationLayerActivityId; } set { presentationLayerActivity.PresentationLayerActivityId = value; } }

        [FwLogicProperty(Id:"VQpC3Fhq07rS")]
        public string PresentationLayerId { get { return presentationLayerActivity.PresentationLayerId; } set { presentationLayerActivity.PresentationLayerId = value; } }

        [FwLogicProperty(Id:"WkTnKDo8j7nKS", IsRecordTitle:true)]
        public string Activity { get { return presentationLayerActivity.Activity; } set { presentationLayerActivity.Activity = value; } }

        [FwLogicProperty(Id:"6Uqmuur61CM3")]
        public string ActivityRename { get { return presentationLayerActivity.ActivityRename; } set { presentationLayerActivity.ActivityRename = value; } }

        [FwLogicProperty(Id:"NLYctwI0nkwN")]
        public string GroupNo { get { return presentationLayerActivity.GroupNo; } set { presentationLayerActivity.GroupNo = value; } }

        [FwLogicProperty(Id:"JVuHF0RSbTav")]
        public string RecType { get { return presentationLayerActivity.RecType; } set { presentationLayerActivity.RecType = value; } }

        [FwLogicProperty(Id:"T86Ec8SgYeZP")]
        public string ExportCode { get { return presentationLayerActivity.ExportCode; } set { presentationLayerActivity.ExportCode = value; } }

        [FwLogicProperty(Id:"2joWQlrQn0vi")]
        public decimal? OrderBy { get { return presentationLayerActivity.OrderBy; } set { presentationLayerActivity.OrderBy = value; } }

        [FwLogicProperty(Id:"mQ3ZlxTzfpPs")]
        public string DateStamp { get { return presentationLayerActivity.DateStamp; } set { presentationLayerActivity.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        private void OnBeforeValidate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(RecType))
                {
                    RecType = RwConstants.PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}

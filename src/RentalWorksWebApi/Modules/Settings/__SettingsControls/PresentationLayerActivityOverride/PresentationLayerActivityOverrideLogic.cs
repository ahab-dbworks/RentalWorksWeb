using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [FwLogic(Id:"B3djSgruuH8S1")]
    public class PresentationLayerActivityOverrideLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"SpToElurmy40E", IsPrimaryKey:true)]
        public string PresentationLayerActivityOverrideId { get { return presentationLayerActivityOverride.PresentationLayerActivityOverrideId; } set { presentationLayerActivityOverride.PresentationLayerActivityOverrideId = value; } }

        [FwLogicProperty(Id:"bUeyxKMShTvm")]
        public string PresentationLayerId { get { return presentationLayerActivityOverride.PresentationLayerId; } set { presentationLayerActivityOverride.PresentationLayerId = value; } }

        [FwLogicProperty(Id:"WbpE8Tf4D5EM")]
        public string PresentationLayerActivityId { get { return presentationLayerActivityOverride.PresentationLayerActivityId; } set { presentationLayerActivityOverride.PresentationLayerActivityId = value; } }

        [FwLogicProperty(Id:"jqRC3z0l7diE")]
        public string MasterId { get { return presentationLayerActivityOverride.MasterId; } set { presentationLayerActivityOverride.MasterId = value; } }

        [FwLogicProperty(Id:"SpToElurmy40E", IsRecordTitle:true, IsReadOnly:true)]
        public string Activity { get; set; }

        [FwLogicProperty(Id:"qD9AlMcpKqS0G", IsRecordTitle:true, IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"2GSyNaPYl5tQo", IsRecordTitle:true, IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"SpToElurmy40E", IsReadOnly:true)]
        public string ActivityRename { get; set; }

        [FwLogicProperty(Id:"CmtxIBnV9SIx")]
        public string ExportCode { get { return presentationLayerActivityOverride.ExportCode; } set { presentationLayerActivityOverride.ExportCode = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.WidgetDateBehavior
{
    [FwLogic(Id: "uHOgvrXVrAAc")]
    public class WidgetDateBehaviorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WidgetDateBehaviorLoader widgetDateBehaviorLoader = new WidgetDateBehaviorLoader();
        public WidgetDateBehaviorLogic()
        {
            dataLoader = widgetDateBehaviorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0ImCJW6XY4DU", IsReadOnly: true, IsPrimaryKey: true)]
        public string WidgetDateBehaviorId { get; set; }
        [FwLogicProperty(Id: "n0YAIUXT8abzN", IsReadOnly: true, IsRecordTitle: true)]
        public string WidgetDateBehavior { get; set; }
        [FwLogicProperty(Id: "JX9x0ROf3qUm", IsReadOnly: true)]
        public string FromDate { get; set; }
        [FwLogicProperty(Id: "pvjaBuv6Lpt5K", IsReadOnly: true)]
        public string ToDate { get; set; }
        [FwLogicProperty(Id: "CvMDNrcr3cdm", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

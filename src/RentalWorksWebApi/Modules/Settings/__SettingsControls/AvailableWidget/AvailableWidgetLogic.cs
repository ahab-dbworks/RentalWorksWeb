using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Settings.AvailableWidget
{
    [FwLogic(Id: "xpiaF5CIFwgIr")]
    public class AvailableWidgetLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AvailableWidgetLoader availableWidgetLoader = new AvailableWidgetLoader();
        public AvailableWidgetLogic()
        {
            dataLoader = availableWidgetLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "sIOc4pCSaqu5u", IsPrimaryKey: true)]
        public string WidgetId { get; set; }

        [FwLogicProperty(Id: "5irWEMf2sDFym", IsRecordTitle: true)]
        public string Widget { get; set; }

        //------------------------------------------------------------------------------------ 

        //jh 01/22/2019 moved to here to fix automapping issue
        [FwLogicProperty(Id: "FztPO7OTOeLpy")]
        public string value { get { return WidgetId; } }

        [FwLogicProperty(Id: "H4Moy15nq1oGS")]
        public string text { get { return Widget; } }

        //------------------------------------------------------------------------------------ 
    }
}

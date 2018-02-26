using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.Widget
{
    public class WidgetLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WidgetRecord widget = new WidgetRecord();
        WidgetLoader widgetLoader = new WidgetLoader();
        public WidgetLogic()
        {
            dataRecords.Add(widget);
            dataLoader = widgetLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WidgetId { get { return widget.WidgetId; } set { widget.WidgetId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Widget { get { return widget.Widget; } set { widget.Widget = value; } }
        public string ApiName { get { return widget.ApiName; } set { widget.ApiName = value; } }
        public string DefaultType { get { return widget.DefaultType; } set { widget.DefaultType = value; } }
        public string DateStamp { get { return widget.DateStamp; } set { widget.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
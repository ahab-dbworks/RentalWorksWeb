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
        public string Sql { get { return widget.Sql; } set { widget.Sql = value; } }
        public string CounterFieldName { get { return widget.CounterFieldName; } set { widget.CounterFieldName = value; } }
        public string LabelFieldName { get { return widget.LabelFieldName; } set { widget.LabelFieldName = value; } }
        public string ClickPath { get { return widget.ClickPath; } set { widget.ClickPath = value; } }
        public string DefaultType { get { return widget.DefaultType; } set { widget.DefaultType = value; } }
        public int? DefaultDataPoints { get { return widget.DefaultDataPoints; } set { widget.DefaultDataPoints = value; } }
        public string DateStamp { get { return widget.DateStamp; } set { widget.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
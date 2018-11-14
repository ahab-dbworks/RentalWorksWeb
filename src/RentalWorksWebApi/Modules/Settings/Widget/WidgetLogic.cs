using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Widget
{
    [FwLogic(Id:"eh62kos81TRJh")]
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
        [FwLogicProperty(Id:"bzC9gd3ruF2Ck", IsPrimaryKey:true)]
        public string WidgetId { get { return widget.WidgetId; } set { widget.WidgetId = value; } }

        [FwLogicProperty(Id:"bzC9gd3ruF2Ck", IsRecordTitle:true)]
        public string Widget { get { return widget.Widget; } set { widget.Widget = value; } }

        [FwLogicProperty(Id:"FuQOXtzL8B9")]
        public string ApiName { get { return widget.ApiName; } set { widget.ApiName = value; } }

        [FwLogicProperty(Id:"X5NtJ7MHJ2f")]
        public string Sql { get { return widget.Sql; } set { widget.Sql = value; } }

        [FwLogicProperty(Id:"uCPgDGUf6xN")]
        public string CounterFieldName { get { return widget.CounterFieldName; } set { widget.CounterFieldName = value; } }

        [FwLogicProperty(Id:"IgNzBmcS7Nq")]
        public string LabelFieldName { get { return widget.LabelFieldName; } set { widget.LabelFieldName = value; } }

        [FwLogicProperty(Id:"5rnMg2K4Tkq")]
        public string ClickPath { get { return widget.ClickPath; } set { widget.ClickPath = value; } }

        [FwLogicProperty(Id:"jRsUIf0c9Nx")]
        public string DefaultType { get { return widget.DefaultType; } set { widget.DefaultType = value; } }

        [FwLogicProperty(Id:"nFId1wCsoJQ")]
        public int? DefaultDataPoints { get { return widget.DefaultDataPoints; } set { widget.DefaultDataPoints = value; } }

        [FwLogicProperty(Id:"Fmc0kBADhLN")]
        public string DefaultAxisNumberFormatId { get { return widget.DefaultAxisNumberFormatId; } set { widget.DefaultAxisNumberFormatId = value; } }

        [FwLogicProperty(Id:"M5Yc2ijTB9fdU", IsReadOnly:true)]
        public string DefaultAxisNumberFormat { get; set; }

        [FwLogicProperty(Id:"M5Yc2ijTB9fdU", IsReadOnly:true)]
        public string DefaultAxisNumberFormatMask { get; set; }

        [FwLogicProperty(Id:"2KkKses8cmv")]
        public string DefaultDataNumberFormatId { get { return widget.DefaultDataNumberFormatId; } set { widget.DefaultDataNumberFormatId = value; } }

        [FwLogicProperty(Id:"xwOVJMoqThM3X", IsReadOnly:true)]
        public string DefaultDataNumberFormat { get; set; }

        [FwLogicProperty(Id:"xwOVJMoqThM3X", IsReadOnly:true)]
        public string DefaultDataNumberFormatMask { get; set; }

        [FwLogicProperty(Id:"ukHI6jdx08K")]
        public string DateStamp { get { return widget.DateStamp; } set { widget.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

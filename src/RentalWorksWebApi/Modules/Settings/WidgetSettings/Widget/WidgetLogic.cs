using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Settings.WidgetSettings.Widget
{
    [FwLogic(Id: "eh62kos81TRJh")]
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
        [FwLogicProperty(Id: "bzC9gd3ruF2Ck", IsPrimaryKey: true)]
        public string WidgetId { get { return widget.WidgetId; } set { widget.WidgetId = value; } }

        [FwLogicProperty(Id: "bzC9gd3ruF2Ck", IsRecordTitle: true)]
        public string Widget { get { return widget.Widget; } set { widget.Widget = value; } }

        [FwLogicProperty(Id: "yMhqb0DauaG")]
        public string ModuleName { get { return widget.ModuleName; } set { widget.ModuleName = value; } }

        [FwLogicProperty(Id: "FuQOXtzL8B9")]
        public string ApiName { get { return widget.ApiName; } set { widget.ApiName = value; } }

        [FwLogicProperty(Id: "MF2KXqkBDu9rP")]
        public string ProcedureName { get { return widget.ProcedureName; } set { widget.ProcedureName = value; } }

        [FwLogicProperty(Id: "uCPgDGUf6xN")]
        public string CounterFieldName { get { return widget.CounterFieldName; } set { widget.CounterFieldName = value; } }

        [FwLogicProperty(Id: "IgNzBmcS7Nq")]
        public string Label1FieldName { get { return widget.Label1FieldName; } set { widget.Label1FieldName = value; } }

        [FwLogicProperty(Id: "tDInqMNN4gKIn")]
        public string Label2FieldName { get { return widget.Label2FieldName; } set { widget.Label2FieldName = value; } }

        [FwLogicProperty(Id: "5rnMg2K4Tkq")]
        public string ClickPath { get { return widget.ClickPath; } set { widget.ClickPath = value; } }

        [FwLogicProperty(Id: "jRsUIf0c9Nx")]
        public string DefaultType { get { return widget.DefaultType; } set { widget.DefaultType = value; } }

        [FwLogicProperty(Id: "nFId1wCsoJQ")]
        public int? DefaultDataPoints { get { return widget.DefaultDataPoints; } set { widget.DefaultDataPoints = value; } }

        [FwLogicProperty(Id: "Fmc0kBADhLN")]
        public string DefaultAxisNumberFormatId { get { return widget.DefaultAxisNumberFormatId; } set { widget.DefaultAxisNumberFormatId = value; } }

        [FwLogicProperty(Id: "M5Yc2ijTB9fdU", IsReadOnly: true)]
        public string DefaultAxisNumberFormat { get; set; }

        [FwLogicProperty(Id: "M5Yc2ijTB9fdU", IsReadOnly: true)]
        public string DefaultAxisNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "2KkKses8cmv")]
        public string DefaultDataNumberFormatId { get { return widget.DefaultDataNumberFormatId; } set { widget.DefaultDataNumberFormatId = value; } }

        [FwLogicProperty(Id: "xwOVJMoqThM3X", IsReadOnly: true)]
        public string DefaultDataNumberFormat { get; set; }

        [FwLogicProperty(Id: "xwOVJMoqThM3X", IsReadOnly: true)]
        public string DefaultDataNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "Fryzh6zXEaTO0")]
        public string DefaultDateBehaviorId { get { return widget.DefaultDateBehaviorId; } set { widget.DefaultDateBehaviorId = value; } }

        [FwLogicProperty(Id: "KnTWzyiSxwqYP", IsReadOnly: true)]
        public string DefaultDateBehavior { get; set; }

        [FwLogicProperty(Id: "cJiQf1cRTGl7V")]
        public string DateFieldDisplayNames { get { return widget.DateFieldDisplayNames; } set { widget.DateFieldDisplayNames = value; } }

        [FwLogicProperty(Id: "KvpOTH3VvRfSz")]
        public string DateFields { get { return widget.DateFields; } set { widget.DateFields = value; } }

        [FwLogicProperty(Id: "epLRO6hZEsbID")]
        public string DefaultDateField { get { return widget.DefaultDateField; } set { widget.DefaultDateField = value; } }

        [FwLogicProperty(Id: "SQ5yzlxwFl4b0")]
        public string DefaultFromDate { get { return widget.DefaultFromDate; } set { widget.DefaultFromDate = value; } }

        [FwLogicProperty(Id: "csSIjXVFEkh0a")]
        public string DefaultToDate { get { return widget.DefaultToDate; } set { widget.DefaultToDate = value; } }

        [FwLogicProperty(Id: "KUKHZiEtDT8uq")]
        public bool? DefaultStacked { get { return widget.DefaultStacked; } set { widget.DefaultStacked = value; } }

        [FwLogicProperty(Id: "o2FIojJBmpwis")]
        public string AssignTo { get { return widget.AssignTo; } set { widget.AssignTo = value; } }

        [FwLogicProperty(Id: "ukHI6jdx08K")]
        public string DateStamp { get { return widget.DateStamp; } set { widget.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 

        //jh 01/22/2019 moved to here to fix automapping issue
        [FwLogicProperty(Id: "3nSLO6CTowZlO")]
        public string value { get { return WidgetId; } }

        [FwLogicProperty(Id: "wNIy6uz4ptVYw")]
        public string text { get { return Widget; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(WidgetLogic).GetProperty(nameof(WidgetLogic.AssignTo));
                string[] acceptableValues = { RwConstants.WIDGET_ASSIGN_TO_ALL, RwConstants.WIDGET_ASSIGN_TO_GROUPS, RwConstants.WIDGET_ASSIGN_TO_USERS };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------

    }
}

using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WebUserWidget
{
    [FwLogic(Id: "7b6roWugr12fa")]
    public class UserWidgetLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserWidgetRecord userWidget = new UserWidgetRecord();
        UserWidgetLoader userWidgetLoader = new UserWidgetLoader();
        public UserWidgetLogic()
        {
            dataRecords.Add(userWidget);
            dataLoader = userWidgetLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2yGrDWCR1JNwE", IsPrimaryKey: true)]
        public string UserWidgetId { get { return userWidget.UserWidgetId; } set { userWidget.UserWidgetId = value; } }

        [FwLogicProperty(Id: "vtYVY2AGKWD0")]
        public string UserId { get { return userWidget.UserId; } set { userWidget.UserId = value; } }

        [FwLogicProperty(Id: "Eo0xqYH7ZQWZj", IsReadOnly: true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id: "0jF2HNcX8G2z")]
        public string WidgetId { get { return userWidget.WidgetId; } set { userWidget.WidgetId = value; } }

        [FwLogicProperty(Id: "455mgbNCMz9Vn", IsReadOnly: true)]
        public string Widget { get; set; }

        [FwLogicProperty(Id: "DMhUs7ZGhRlVD", IsReadOnly: true)]
        public string DefaultType { get; set; }

        [FwLogicProperty(Id: "DjEbrW7MhpKw")]
        public string WidgetType { get { return userWidget.WidgetType; } set { userWidget.WidgetType = value; } }

        [FwLogicProperty(Id: "SWJn2kBJRRe1H", IsReadOnly: true)]
        public int? DefaultDataPoints { get; set; }

        [FwLogicProperty(Id: "Mb0RRsGlFGbc")]
        public int? DataPoints { get { return userWidget.DataPoints; } set { userWidget.DataPoints = value; } }

        [FwLogicProperty(Id: "7szF6E2pAS6lZ", IsReadOnly: true)]
        public string DefaultAxisNumberFormatId { get; set; }

        [FwLogicProperty(Id: "7szF6E2pAS6lZ", IsReadOnly: true)]
        public string DefaultAxisNumberFormat { get; set; }

        [FwLogicProperty(Id: "7szF6E2pAS6lZ", IsReadOnly: true)]
        public string DefaultAxisNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "QExKvlAttWbL")]
        public string AxisNumberFormatId { get { return userWidget.AxisNumberFormatId; } set { userWidget.AxisNumberFormatId = value; } }

        [FwLogicProperty(Id: "7szF6E2pAS6lZ", IsReadOnly: true)]
        public string AxisNumberFormat { get; set; }

        [FwLogicProperty(Id: "7szF6E2pAS6lZ", IsReadOnly: true)]
        public string AxisNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "HlDA4Oq6XPJlV", IsReadOnly: true)]
        public string DefaultDataNumberFormatId { get; set; }

        [FwLogicProperty(Id: "HlDA4Oq6XPJlV", IsReadOnly: true)]
        public string DefaultDataNumberFormat { get; set; }

        [FwLogicProperty(Id: "HlDA4Oq6XPJlV", IsReadOnly: true)]
        public string DefaultDataNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "JLLUlisK8cll")]
        public string DataNumberFormatId { get { return userWidget.DataNumberFormatId; } set { userWidget.DataNumberFormatId = value; } }

        [FwLogicProperty(Id: "HlDA4Oq6XPJlV", IsReadOnly: true)]
        public string DataNumberFormat { get; set; }

        [FwLogicProperty(Id: "HlDA4Oq6XPJlV", IsReadOnly: true)]
        public string DataNumberFormatMask { get; set; }

        [FwLogicProperty(Id: "PBaBr0YPnVhA0", IsReadOnly: true)]
        public string DefaultDateBehaviorId { get; set; }
        [FwLogicProperty(Id: "qDChYGxR0e3DO", IsReadOnly: true)]
        public string DefaultDateBehavior { get; set; }

        [FwLogicProperty(Id: "Myob17DjListW")]
        public string DateBehaviorId { get { return userWidget.DateBehaviorId; } set { userWidget.DateBehaviorId = value; } }

        [FwLogicProperty(Id: "2MExrJsp8Dqx0", IsReadOnly: true)]
        public string DateBehavior { get; set; }

        [FwLogicProperty(Id: "LEwhr5fCzYgFD", IsReadOnly: true)]
        public string DateFieldDisplayNames { get; set; }

        [FwLogicProperty(Id: "rJD7VxzM0nRUL", IsReadOnly: true)]
        public string DateFields { get; set; }

        [FwLogicProperty(Id: "NKJA1Ps4yJncP", IsReadOnly: true)]
        public string DefaultDateField { get; set; }

        [FwLogicProperty(Id: "iHKW9xVnzWzS3")]
        public string DateField { get { return userWidget.DateField; } set { userWidget.DateField = value; } }

        [FwLogicProperty(Id: "qHbFvPszfYCIx", IsReadOnly: true)]
        public string DefaultFromDate { get; set; }

        [FwLogicProperty(Id: "XWzqyIKachLYb")]
        public string FromDate { get { return userWidget.FromDate; } set { userWidget.FromDate = value; } }

        [FwLogicProperty(Id: "2mdNqnFpiYwwO", IsReadOnly: true)]
        public string DefaultToDate { get; set; }

        [FwLogicProperty(Id: "Zmd1iFAogjrN6")]
        public string ToDate { get { return userWidget.ToDate; } set { userWidget.ToDate = value; } }

        [FwLogicProperty(Id: "xUe2AinjCmIg")]
        public string Settings { get { return userWidget.Settings; } set { userWidget.Settings = value; } }

        [FwLogicProperty(Id: "G4obcaq7FEfVi")]
        public string OfficeLocationId { get { return userWidget.OfficeLocationId; } set { userWidget.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "Kc72ZAFoZ9HBm", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "ol7Rnkn7YH35b", IsReadOnly: true)]
        public string OfficeLocationCode { get; set; }


        [FwLogicProperty(Id: "gb1hYGnT0hW6H", IsReadOnly: true)]
        public bool? DefaultStacked { get; set; }


        [FwLogicProperty(Id: "zecSx1uaNUfmN")]
        public bool? Stacked { get { return userWidget.Stacked; } set { userWidget.Stacked = value; } }


        [FwLogicProperty(Id: "jX6rdY9ENpKO")]
        public bool? Disabled { get { return userWidget.Disabled; } set { userWidget.Disabled = value; } }

        [FwLogicProperty(Id: "8nRwAjXbg2B2")]
        public decimal? OrderBy { get { return userWidget.OrderBy; } set { userWidget.OrderBy = value; } }

        [FwLogicProperty(Id: "vRqBiUe7xQ5S")]
        public string DateStamp { get { return userWidget.DateStamp; } set { userWidget.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

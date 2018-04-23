using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WebUserWidget
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserWidgetId { get { return userWidget.UserWidgetId; } set { userWidget.UserWidgetId = value; } }
        public string UserId { get { return userWidget.UserId; } set { userWidget.UserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        public string WidgetId { get { return userWidget.WidgetId; } set { userWidget.WidgetId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Widget { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultType { get; set; }
        public string WidgetType { get { return userWidget.WidgetType; } set { userWidget.WidgetType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? DefaultDataPoints { get; set; }
        public int? DataPoints { get { return userWidget.DataPoints; } set { userWidget.DataPoints = value; } }
        public string Settings { get { return userWidget.Settings; } set { userWidget.Settings = value; } }
        public bool? Disabled { get { return userWidget.Disabled; } set { userWidget.Disabled = value; } }
        public decimal? OrderBy { get { return userWidget.OrderBy; } set { userWidget.OrderBy = value; } }
        public string DateStamp { get { return userWidget.DateStamp; } set { userWidget.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Settings.WidgetUser
{
    [FwLogic(Id: "2WNqxtENxB23P")]
    public class WidgetUserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WidgetUserRecord widgetUser = new WidgetUserRecord();
        WidgetUserLoader widgetUserLoader = new WidgetUserLoader();
        public WidgetUserLogic()
        {
            dataRecords.Add(widgetUser);
            dataLoader = widgetUserLoader;

            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "RcLuUhYEaXbXd", IsPrimaryKey: true)]
        public string WidgetUserId { get { return widgetUser.WidgetUserId; } set { widgetUser.WidgetUserId = value; } }
        [FwLogicProperty(Id: "8UuAoNAl6KjZM")]
        public string WidgetId { get { return widgetUser.WidgetId; } set { widgetUser.WidgetId = value; } }
        [FwLogicProperty(Id: "QHtrphorsk4n9", IsReadOnly: true)]
        public string WidgetDescription { get; set; }
        [FwLogicProperty(Id: "EOrd2xYYzMz6o")]
        public string WebUserId { get { return widgetUser.WebUserId; } set { widgetUser.WebUserId = value; } }
        [FwLogicProperty(Id: "QEzeitnAQqm47", IsReadOnly: true)]
        public string UserId { get; set; }
        [FwLogicProperty(Id: "JanImLNy1zU38", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "OI1iNXvMDSEkR")]
        public string DateStamp { get { return widgetUser.DateStamp; } set { widgetUser.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if ((string.IsNullOrEmpty(WebUserId)) && (!string.IsNullOrEmpty(UserId)))
            {
                WebUserId = AppFunc.GetStringDataAsync(AppConfig, "webusers", "usersid", UserId, "webusersid").Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}

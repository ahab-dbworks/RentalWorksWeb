using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.UserDashboardSettings
{
    public class UserDashboardSettingsLogic : AppBusinessLogic
    {
        protected UserDashboardSettingsLoader userDashboardSettingsLoader = new UserDashboardSettingsLoader();
        //------------------------------------------------------------------------------------
        public UserDashboardSettingsLogic()
        {
            dataLoader = userDashboardSettingsLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserWidgetId { get; set; }
        public string WidgetId { get; set; }
        public string Widget { get; set; }
        public string DefaultType { get; set; }
        public string WidgetType { get; set; }
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
    }
}

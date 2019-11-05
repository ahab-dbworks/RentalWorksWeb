using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Administrator.CustomReportLayoutUser
{
    [FwLogic(Id: "E1jhUJDMUTRYJ")]
    public class CustomReportLayoutUserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomReportLayoutUserRecord customReportLayoutUser = new CustomReportLayoutUserRecord();
        CustomReportLayoutUserLoader customReportLayoutUserLoader = new CustomReportLayoutUserLoader();
        public CustomReportLayoutUserLogic()
        {
            dataRecords.Add(customReportLayoutUser);
            dataLoader = customReportLayoutUserLoader;

            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Ma7NEp03BmRtV", IsPrimaryKey: true)]
        public string CustomReportLayoutUserId { get { return customReportLayoutUser.CustomReportLayoutUserId; } set { customReportLayoutUser.CustomReportLayoutUserId = value; } }
        [FwLogicProperty(Id: "rcMEdmP31JBXs")]
        public string CustomReportLayoutId { get { return customReportLayoutUser.CustomReportLayoutId; } set { customReportLayoutUser.CustomReportLayoutId = value; } }
        [FwLogicProperty(Id: "pKwuoJTYtOL41", IsReadOnly: true)]
        public string CustomReportLayoutDescription { get; set; }
        [FwLogicProperty(Id: "g2NRUPvQDBAaP")]
        public string WebUserId { get { return customReportLayoutUser.WebUserId; } set { customReportLayoutUser.WebUserId = value; } }
        [FwLogicProperty(Id: "HSb4cHP6Wrfdx", IsReadOnly: true)]
        public string UserId { get; set; }
        [FwLogicProperty(Id: "gGxENYdzvYMhs", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "Dx2X2YPOTpfaj")]
        public string DateStamp { get { return customReportLayoutUser.DateStamp; } set { customReportLayoutUser.DateStamp = value; } }
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

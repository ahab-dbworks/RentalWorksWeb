using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.AdministratorControls.CustomReportLayoutUser
{
    [FwLogic(Id: "TmTQgoMDJ69p")]
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
        [FwLogicProperty(Id: "Jp3PumFGkmWt", IsPrimaryKey: true)]
        public string CustomReportLayoutUserId { get { return customReportLayoutUser.CustomReportLayoutUserId; } set { customReportLayoutUser.CustomReportLayoutUserId = value; } }
        [FwLogicProperty(Id: "xnmLIcxFLFTHz")]
        public string CustomReportLayoutId { get { return customReportLayoutUser.CustomReportLayoutId; } set { customReportLayoutUser.CustomReportLayoutId = value; } }
        [FwLogicProperty(Id: "sX6UMCMUuMWs", IsReadOnly: true)]
        public string CustomReportLayoutDescription { get; set; }
        [FwLogicProperty(Id: "O6jgQSSNeNfco")]
        public string WebUserId { get { return customReportLayoutUser.WebUserId; } set { customReportLayoutUser.WebUserId = value; } }
        [FwLogicProperty(Id: "GghtWW1kGvAch", IsReadOnly: true)]
        public string UserId { get; set; }
        [FwLogicProperty(Id: "hppO7YlvA6ni", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "tn2JDiAgkQFp")]
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

using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Administrator.CustomFormUser
{
    [FwLogic(Id: "TmTQgoMDJ69p")]
    public class CustomFormUserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomFormUserRecord webFormUser = new CustomFormUserRecord();
        CustomFormUserLoader webFormUserLoader = new CustomFormUserLoader();
        public CustomFormUserLogic()
        {
            dataRecords.Add(webFormUser);
            dataLoader = webFormUserLoader;

            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Jp3PumFGkmWt", IsPrimaryKey: true)]
        public string CustomFormUserId { get { return webFormUser.CustomFormUserId; } set { webFormUser.CustomFormUserId = value; } }
        [FwLogicProperty(Id: "xnmLIcxFLFTHz")]
        public string CustomFormId { get { return webFormUser.CustomFormId; } set { webFormUser.CustomFormId = value; } }
        [FwLogicProperty(Id: "sX6UMCMUuMWs", IsReadOnly: true)]
        public string CustomFormDescription { get; set; }
        [FwLogicProperty(Id: "O6jgQSSNeNfco")]
        public string WebUserId { get { return webFormUser.WebUserId; } set { webFormUser.WebUserId = value; } }
        [FwLogicProperty(Id: "GghtWW1kGvAch", IsReadOnly: true)]
        public string UserId { get; set; }
        [FwLogicProperty(Id: "hppO7YlvA6ni", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "tn2JDiAgkQFp")]
        public string DateStamp { get { return webFormUser.DateStamp; } set { webFormUser.DateStamp = value; } }
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

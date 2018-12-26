using WebApi.Logic;
using FwStandard.AppManager;
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
        [FwLogicProperty(Id: "hppO7YlvA6ni", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "tn2JDiAgkQFp")]
        public string DateStamp { get { return webFormUser.DateStamp; } set { webFormUser.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.Modules.Administrator.Alert;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Settings.SystemSettings.EmailSettings
{
    [FwLogic(Id: "K33XTFCoSEq6")]
    public class EmailSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        EmailSettingsRecord emailSettings = new EmailSettingsRecord();
        public EmailSettingsLogic()
        {
            dataRecords.Add(emailSettings);
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "toCtkFmW1fghM", IsPrimaryKey: true)]
        public string EmailSettingsId { get { return emailSettings.EmailSettingsId; } set { emailSettings.EmailSettingsId = value; } }
        //[FwLogicProperty(Id: "qA4GDrku5VME")]
        //public string Pdfpath { get { return emailSettings.Pdfpath; } set { emailSettings.Pdfpath = value; } }
        [FwLogicProperty(Id: "DPJiDlt3HQ7T", IsRecordTitle: true)]
        public string Host { get { return emailSettings.Host; } set { emailSettings.Host = value; } }
        [FwLogicProperty(Id: "4QNpHAik4FDZ")]
        public int? Port { get { return emailSettings.Port; } set { emailSettings.Port = value; } }
        [FwLogicProperty(Id: "btbzFofqg1IW", IsNotAudited: true)]
        public string AccountPassword { get { return emailSettings.AccountPassword; } set { emailSettings.AccountPassword = value; } }
        [FwLogicProperty(Id: "ThwG5S5XA5iL")]
        public string AccountUsername { get { return emailSettings.AccountUsername; } set { emailSettings.AccountUsername = value; } }
        [FwLogicProperty(Id: "O29ycfBZh3mM")]
        public string AuthenticationType { get { return emailSettings.AuthenticationType; } set { emailSettings.AuthenticationType = value; } }
        [FwLogicProperty(Id: "JDf3YokaCbPE")]
        public int? DeleteDays { get { return emailSettings.DeleteDays; } set { emailSettings.DeleteDays = value; } }
        [FwLogicProperty(Id: "vLeligULwI3O")]
        public string DateStamp { get { return emailSettings.DateStamp; } set { emailSettings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            AlertFunc.RefreshAlerts(AppConfig);
        }
        //------------------------------------------------------------------------------------ 
    }
}
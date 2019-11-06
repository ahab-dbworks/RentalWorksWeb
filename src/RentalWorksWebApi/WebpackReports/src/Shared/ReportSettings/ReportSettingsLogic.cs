using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.ReportSettings
{
    [FwLogic(Id: "7yuAvAv1q8k5")]
    public class ReportSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReportSettingsRecord reportSettings = new ReportSettingsRecord();
        public ReportSettingsLogic()
        {
            dataRecords.Add(reportSettings);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qHw2lu0e41SJ", IsPrimaryKey: true)]
        public int? Id { get { return reportSettings.Id; } set { reportSettings.Id = value; } }
        [FwLogicProperty(Id: "Uqjpi6vOWUru")]
        public string WebUserId { get { return reportSettings.WebUserId; } set { reportSettings.WebUserId = value; } }
        [FwLogicProperty(Id: "1CEP1rk7ZJXE")]
        public string ReportName { get { return reportSettings.ReportName; } set { reportSettings.ReportName = value; } }
        [FwLogicProperty(Id: "BkdL3m92jUQgD")]
        public string Description { get { return reportSettings.Description; } set { reportSettings.Description = value; } }
        [FwLogicProperty(Id: "JXBOzthEfW0l")]
        public string Settings { get { return reportSettings.Settings; } set { reportSettings.Settings = value; } }
        [FwLogicProperty(Id: "2hakp7G1Qp0v")]
        public string DateStamp { get { return reportSettings.DateStamp; } set { reportSettings.DateStamp = value; } }
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

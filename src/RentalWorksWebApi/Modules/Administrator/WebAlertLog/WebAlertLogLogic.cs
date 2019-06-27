using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.WebAlertLog
{
    [FwLogic(Id: "Zu7c1C5nMXgnm")]
    public class WebAlertLogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebAlertLogRecord webAlertLog = new WebAlertLogRecord();
        public WebAlertLogLogic()
        {
            dataRecords.Add(webAlertLog);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "KD5s4c0XTmBxO", IsPrimaryKey: true)]
        public int? WebAlertLogId { get { return webAlertLog.WebAlertLogId; } set { webAlertLog.WebAlertLogId = value; } }
        [FwLogicProperty(Id: "COSY7QEyTmog ")]
        public string AlertId { get { return webAlertLog.AlertId; } set { webAlertLog.AlertId = value; } }
        [FwLogicProperty(Id: "1ZW3ABx5Gbnh ")]
        public string CreateDateTime { get { return webAlertLog.CreateDateTime; } set { webAlertLog.CreateDateTime = value; } }
        [FwLogicProperty(Id: "rFMy5SjOMT5D ")]
        public string AlertSubject { get { return webAlertLog.AlertSubject; } set { webAlertLog.AlertSubject = value; } }
        [FwLogicProperty(Id: "x37GeMfaUcxZ ")]
        public string AlertBody { get { return webAlertLog.AlertBody; } set { webAlertLog.AlertBody = value; } }
        [FwLogicProperty(Id: "0RtEQwkkMxnDT")]
        public string AlertFrom { get { return webAlertLog.AlertFrom; } set { webAlertLog.AlertFrom = value; } }
        [FwLogicProperty(Id: "eAI7RqIg1dTU ")]
        public string AlertTo { get { return webAlertLog.AlertTo; } set { webAlertLog.AlertTo = value; } }
        [FwLogicProperty(Id: "FbgomYSxgPDIA")]
        public string Status { get { return webAlertLog.Status; } set { webAlertLog.Status = value; } }
        [FwLogicProperty(Id: "TRdhapFJUhDNn")]
        public string ErrorMessage { get { return webAlertLog.ErrorMessage; } set { webAlertLog.ErrorMessage = value; } }
        [FwLogicProperty(Id: "zx1RlgAY0SRP ")]
        public string DateStamp { get { return webAlertLog.DateStamp; } set { webAlertLog.DateStamp = value; } }
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

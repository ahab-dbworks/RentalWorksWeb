using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.SystemUpdateHistoryLog
{
    [FwLogic(Id: "PS9uUJ0qEDP4V")]
    public class SystemUpdateHistoryLogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SystemUpdateHistoryLogRecord systemUpdateHistoryLog = new SystemUpdateHistoryLogRecord();
        SystemUpdateHistoryLogLoader systemUpdateHistoryLogLoader = new SystemUpdateHistoryLogLoader();
        public SystemUpdateHistoryLogLogic()
        {
            dataRecords.Add(systemUpdateHistoryLog);
            dataLoader = systemUpdateHistoryLogLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "PSATyt2HtFkST", IsPrimaryKey: true)]
        public int? SystemUpdateHistoryLogId { get { return systemUpdateHistoryLog.SystemUpdateHistoryLogId; } set { systemUpdateHistoryLog.SystemUpdateHistoryLogId = value; } }
        [FwLogicProperty(Id: "pSChP5xQXTw7F")]
        public int? SystemUpdateHistoryId { get { return systemUpdateHistoryLog.SystemUpdateHistoryId; } set { systemUpdateHistoryLog.SystemUpdateHistoryId = value; } }
        [FwLogicProperty(Id: "psE0ezx6zBbCu")]
        public string Messsage { get { return systemUpdateHistoryLog.Messsage; } set { systemUpdateHistoryLog.Messsage = value; } }
        [FwLogicProperty(Id: "PspboRwaFkvmx")]
        public string DateStamp { get { return systemUpdateHistoryLog.DateStamp; } set { systemUpdateHistoryLog.DateStamp = value; } }
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

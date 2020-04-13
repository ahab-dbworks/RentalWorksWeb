using WebApi.Logic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Administrator.SystemUpdateHistory
{
    [FwLogic(Id: "mCp5yAlCPEsSg")]
    public class SystemUpdateHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SystemUpdateHistoryRecord systemUpdateHistory = new SystemUpdateHistoryRecord();
        SystemUpdateHistoryLoader systemUpdateHistoryLoader = new SystemUpdateHistoryLoader();
        public SystemUpdateHistoryLogic()
        {
            dataRecords.Add(systemUpdateHistory);
            dataLoader = systemUpdateHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "meE6LEPA0ut2x", IsPrimaryKey: true)]
        public int? SystemUpdateHistoryId { get { return systemUpdateHistory.SystemUpdateHistoryId; } set { systemUpdateHistory.SystemUpdateHistoryId = value; } }
        [FwLogicProperty(Id: "meiGEjzhUvYUd")]
        public string UsersId { get { return systemUpdateHistory.UsersId; } set { systemUpdateHistory.UsersId = value; } }
        [FwLogicProperty(Id: "meLzKgyRWSj4p", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "MEpgcJdPnPGfA")]
        public DateTime UpdateDateTime { get { return systemUpdateHistory.UpdateDateTime; } set { systemUpdateHistory.UpdateDateTime = value; } }
        [FwLogicProperty(Id: "mfMLDiiy2QQsO")]
        public string FromVersion { get { return systemUpdateHistory.FromVersion; } set { systemUpdateHistory.FromVersion = value; } }
        [FwLogicProperty(Id: "MfXnH8x91R0Op")]
        public string ToVersion { get { return systemUpdateHistory.ToVersion; } set { systemUpdateHistory.ToVersion = value; } }
        [FwLogicProperty(Id: "mgatmJEsol8kR")]
        public string ErrorMessage { get { return systemUpdateHistory.ErrorMessage; } set { systemUpdateHistory.ErrorMessage = value; } }
        [FwLogicProperty(Id: "MGlOv6DltFdxh")]
        public string DateStamp { get { return systemUpdateHistory.DateStamp; } set { systemUpdateHistory.DateStamp = value; } }
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

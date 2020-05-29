using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.StagingSubstituteSession
{
    [FwLogic(Id: "QKED8mSzS8lzR")]
    public class StagingSubstituteSessionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StagingSubstituteSessionRecord stagingSubstituteSession = new StagingSubstituteSessionRecord();
        public StagingSubstituteSessionLogic()
        {
            dataRecords.Add(stagingSubstituteSession);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qkjg1z1g5L9f3", IsPrimaryKey: true)]
        public string SessionId { get { return stagingSubstituteSession.SessionId; } set { stagingSubstituteSession.SessionId = value; } }
        [FwLogicProperty(Id: "QKV8GzeBg1YA8")]
        public string OrderId { get { return stagingSubstituteSession.OrderId; } set { stagingSubstituteSession.OrderId = value; } }
        [FwLogicProperty(Id: "QLHSQvzzgvO4m")]
        public string OrderItemId { get { return stagingSubstituteSession.OrderItemId; } set { stagingSubstituteSession.OrderItemId = value; } }
        [FwLogicProperty(Id: "QLOWcn0XkQnSj")]
        public string DateStamp { get { return stagingSubstituteSession.DateStamp; } set { stagingSubstituteSession.DateStamp = value; } }
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

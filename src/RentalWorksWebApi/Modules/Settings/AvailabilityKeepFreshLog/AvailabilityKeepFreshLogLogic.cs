using WebApi.Logic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Settings.AvailabilityKeepFreshLog
{
    [FwLogic(Id: "W4DbxTlehM6ln")]
    public class AvailabilityKeepFreshLogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AvailabilityKeepFreshLogRecord availabilityKeepFreshLog = new AvailabilityKeepFreshLogRecord();
        public AvailabilityKeepFreshLogLogic()
        {
            dataRecords.Add(availabilityKeepFreshLog);
            HasAudit = false;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YKS3TbCdrl4tU", IsPrimaryKey: true)]
        public int? Id { get { return availabilityKeepFreshLog.Id; } set { availabilityKeepFreshLog.Id = value; } }
        [FwLogicProperty(Id: "pHmPrl3YArzdw")]
        public int? BatchSize { get { return availabilityKeepFreshLog.BatchSize; } set { availabilityKeepFreshLog.BatchSize = value; } }
        [FwLogicProperty(Id: "3F0jGlwyD47Ub")]
        public DateTime? StartDateTime { get { return availabilityKeepFreshLog.StartDateTime; } set { availabilityKeepFreshLog.StartDateTime = value; } }
        [FwLogicProperty(Id: "iWfpotLcigvcf")]
        public DateTime? EndDateTime { get { return availabilityKeepFreshLog.EndDateTime; } set { availabilityKeepFreshLog.EndDateTime = value; } }
        [FwLogicProperty(Id: "DSLLtKQ2jOqqF")]
        public string DateStamp { get { return availabilityKeepFreshLog.DateStamp; } set { availabilityKeepFreshLog.DateStamp = value; } }
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

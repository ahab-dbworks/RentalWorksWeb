using WebApi.Logic;
using FwStandard.AppManager;
using System;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.AvailabilityKeepFreshLog
{
    [FwLogic(Id: "W4DbxTlehM6ln")]
    public class AvailabilityKeepFreshLogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AvailabilityKeepFreshLogRecord availabilityKeepFreshLog = new AvailabilityKeepFreshLogRecord();
        AvailabilityKeepFreshLogLoader availabilityKeepFreshLogLoader = new AvailabilityKeepFreshLogLoader();
        public AvailabilityKeepFreshLogLogic()
        {
            dataRecords.Add(availabilityKeepFreshLog);
            dataLoader = availabilityKeepFreshLogLoader;
            HasAudit = false;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YKS3TbCdrl4tU", IsPrimaryKey: true)]
        public int? Id { get { return availabilityKeepFreshLog.Id; } set { availabilityKeepFreshLog.Id = value; } }
        [FwLogicProperty(Id: "pHmPrl3YArzdw")]
        public int? BatchSize { get { return availabilityKeepFreshLog.BatchSize; } set { availabilityKeepFreshLog.BatchSize = value; } }
        [FwLogicProperty(Id: "3F0jGlwyD47Ub")]
        public DateTime? StartDateTime { get { return availabilityKeepFreshLog.StartDateTime; } set { availabilityKeepFreshLog.StartDateTime = value; } }
        [FwLogicProperty(Id: "DiqKQSxPrI8s2", IsReadOnly: true)]
        public string StartDateTimeString { get; set; }
        [FwLogicProperty(Id: "iWfpotLcigvcf")]
        public DateTime? EndDateTime { get { return availabilityKeepFreshLog.EndDateTime; } set { availabilityKeepFreshLog.EndDateTime = value; } }
        [FwLogicProperty(Id: "uYH9yxzRpy55d", IsReadOnly: true)]
        public string EndDateTimeString { get; set; }
        [FwLogicProperty(Id: "QC4XcRlZYMS44", IsReadOnly: true)]
        public decimal? DurationInSeconds { get; set; }
        [FwLogicProperty(Id: "QbU0ydjUXnv3P", IsReadOnly: true)]
        public decimal? DurationInMinutes { get; set; }
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

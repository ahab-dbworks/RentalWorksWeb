using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DealSettings.ScheduleType
{
    [FwLogic(Id:"fji5ac5mq8j4m")]
    public class ScheduleTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleTypeRecord scheduleType = new ScheduleTypeRecord();
        public ScheduleTypeLogic()
        {
            dataRecords.Add(scheduleType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"S3NVH5SqLAxAD", IsPrimaryKey:true)]
        public string ScheduleTypeId { get { return scheduleType.ScheduleTypeId; } set { scheduleType.ScheduleTypeId = value; } }

        [FwLogicProperty(Id:"S3NVH5SqLAxAD", IsRecordTitle:true)]
        public string ScheduleType { get { return scheduleType.ScheduleType; } set { scheduleType.ScheduleType = value; } }

        [FwLogicProperty(Id:"SWjgFAnja6dd")]
        public bool? Inactive { get { return scheduleType.Inactive; } set { scheduleType.Inactive = value; } }

        [FwLogicProperty(Id:"GxVCNaDy4DEm")]
        public string DateStamp { get { return scheduleType.DateStamp; } set { scheduleType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

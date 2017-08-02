using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.ScheduleType
{
    public class ScheduleTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleTypeRecord scheduleType = new ScheduleTypeRecord();
        public ScheduleTypeLogic()
        {
            dataRecords.Add(scheduleType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ScheduleTypeId { get { return scheduleType.ScheduleTypeId; } set { scheduleType.ScheduleTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ScheduleType { get { return scheduleType.ScheduleType; } set { scheduleType.ScheduleType = value; } }
        public bool Inactive { get { return scheduleType.Inactive; } set { scheduleType.Inactive = value; } }
        public string DateStamp { get { return scheduleType.DateStamp; } set { scheduleType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

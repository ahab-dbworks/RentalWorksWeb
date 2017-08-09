using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.ScheduleStatus;

namespace RentalWorksWebApi.Modules.Settings.CrewScheduleStatus
{
    public class CrewScheduleStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleStatusRecord scheduleStatus = new ScheduleStatusRecord();
        CrewScheduleStatusLoader scheduleStatusLoader = new CrewScheduleStatusLoader();
        public CrewScheduleStatusLogic()
        {
            dataRecords.Add(scheduleStatus);
            dataLoader = scheduleStatusLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ScheduleStatusId { get { return scheduleStatus.ScheduleStatusId; } set { scheduleStatus.ScheduleStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ScheduleStatus { get { return scheduleStatus.ScheduleStatus; } set { scheduleStatus.ScheduleStatus = value; } }
        public string StatusAction { get { return scheduleStatus.StatusAction; } set { scheduleStatus.StatusAction = value; } }
        public string RecType { get { return scheduleStatus.RecType; } set { scheduleStatus.RecType = value; } }
        public string Color { get { return scheduleStatus.Color; } set { scheduleStatus.Color = value; } }
        public bool WhiteText { get { return scheduleStatus.WhiteText; } set { scheduleStatus.WhiteText = value; } }
        public bool Inactive { get { return scheduleStatus.Inactive; } set { scheduleStatus.Inactive = value; } }
        public string DateStamp { get { return scheduleStatus.DateStamp; } set { scheduleStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RecType = "C";
        }
        //------------------------------------------------------------------------------------

    }

}

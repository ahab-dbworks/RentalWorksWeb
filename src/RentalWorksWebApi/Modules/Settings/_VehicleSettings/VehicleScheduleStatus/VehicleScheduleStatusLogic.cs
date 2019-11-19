using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ScheduleStatus;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleScheduleStatus
{
    [FwLogic(Id:"6XWydFQkG3fSZ")]
    public class VehicleScheduleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleStatusRecord scheduleStatus = new ScheduleStatusRecord();
        VehicleScheduleStatusLoader scheduleStatusLoader = new VehicleScheduleStatusLoader();
        public VehicleScheduleStatusLogic()
        {
            dataRecords.Add(scheduleStatus);
            dataLoader = scheduleStatusLoader;
            RecType = RwConstants.SCHEDULE_STATUS_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"EUc3uPUfXsyF4", IsPrimaryKey:true)]
        public string ScheduleStatusId { get { return scheduleStatus.ScheduleStatusId; } set { scheduleStatus.ScheduleStatusId = value; } }

        [FwLogicProperty(Id:"EUc3uPUfXsyF4", IsRecordTitle:true)]
        public string ScheduleStatus { get { return scheduleStatus.ScheduleStatus; } set { scheduleStatus.ScheduleStatus = value; } }

        [FwLogicProperty(Id:"ceoZy4NV5gBe")]
        public string StatusAction { get { return scheduleStatus.StatusAction; } set { scheduleStatus.StatusAction = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"qZMxgvP1jEt8")]
        public string RecType { get { return scheduleStatus.RecType; } set { scheduleStatus.RecType = value; } }

        [FwLogicProperty(Id:"Nf9f4LT1OEgq")]
        public string Color { get { return scheduleStatus.Color; } set { scheduleStatus.Color = value; } }

        [FwLogicProperty(Id:"Sro6n2mQjaHe")]
        public bool? WhiteText { get { return scheduleStatus.WhiteText; } set { scheduleStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"bttd0TX1zNgV")]
        public bool? Inactive { get { return scheduleStatus.Inactive; } set { scheduleStatus.Inactive = value; } }

        [FwLogicProperty(Id:"ddkI7Ncjdd9d")]
        public string DateStamp { get { return scheduleStatus.DateStamp; } set { scheduleStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

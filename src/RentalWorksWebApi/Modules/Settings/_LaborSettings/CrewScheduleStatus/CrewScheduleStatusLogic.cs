using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ScheduleStatus;
using WebLibrary;

namespace WebApi.Modules.Settings.LaborSettings.CrewScheduleStatus
{
    [FwLogic(Id:"gOnirA1oE6Mb")]
    public class CrewScheduleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleStatusRecord scheduleStatus = new ScheduleStatusRecord();
        CrewScheduleStatusLoader scheduleStatusLoader = new CrewScheduleStatusLoader();
        public CrewScheduleStatusLogic()
        {
            dataRecords.Add(scheduleStatus);
            dataLoader = scheduleStatusLoader;
            RecType = RwConstants.SCHEDULE_STATUS_TYPE_CREW;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"y5GA0110zuk5", IsPrimaryKey:true)]
        public string ScheduleStatusId { get { return scheduleStatus.ScheduleStatusId; } set { scheduleStatus.ScheduleStatusId = value; } }

        [FwLogicProperty(Id:"y5GA0110zuk5", IsRecordTitle:true)]
        public string ScheduleStatus { get { return scheduleStatus.ScheduleStatus; } set { scheduleStatus.ScheduleStatus = value; } }

        [FwLogicProperty(Id:"rknz2bnA6P6p")]
        public string StatusAction { get { return scheduleStatus.StatusAction; } set { scheduleStatus.StatusAction = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"wTINVgnBWWrZ")]
        public string RecType { get { return scheduleStatus.RecType; } set { scheduleStatus.RecType = value; } }

        [FwLogicProperty(Id:"FAcCZ42EPkQr")]
        public string Color { get { return scheduleStatus.Color; } set { scheduleStatus.Color = value; } }

        [FwLogicProperty(Id:"DAbTXFmlMXjj")]
        public bool? WhiteText { get { return scheduleStatus.WhiteText; } set { scheduleStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"h89huswYh4gA")]
        public bool? Inactive { get { return scheduleStatus.Inactive; } set { scheduleStatus.Inactive = value; } }

        [FwLogicProperty(Id:"kO1SkuiIG8Ff")]
        public string DateStamp { get { return scheduleStatus.DateStamp; } set { scheduleStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}

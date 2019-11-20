using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ScheduleStatus;
using WebApi;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityScheduleStatus
{
    [FwLogic(Id:"Fbnjl7MYUu7")]
    public class FacilityScheduleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleStatusRecord scheduleStatus = new ScheduleStatusRecord();
        FacilityScheduleStatusLoader scheduleStatusLoader = new FacilityScheduleStatusLoader();
        public FacilityScheduleStatusLogic()
        {
            dataRecords.Add(scheduleStatus);
            dataLoader = scheduleStatusLoader;
            RecType = RwConstants.SCHEDULE_STATUS_TYPE_FACILITY;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"e9OkKUNeJg8", IsPrimaryKey:true)]
        public string ScheduleStatusId { get { return scheduleStatus.ScheduleStatusId; } set { scheduleStatus.ScheduleStatusId = value; } }

        [FwLogicProperty(Id:"e9OkKUNeJg8", IsRecordTitle:true)]
        public string ScheduleStatus { get { return scheduleStatus.ScheduleStatus; } set { scheduleStatus.ScheduleStatus = value; } }

        [FwLogicProperty(Id:"uGyknGqJO7yO")]
        public string StatusAction { get { return scheduleStatus.StatusAction; } set { scheduleStatus.StatusAction = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"qZ4foK2ikeSQ")]
        public string RecType { get { return scheduleStatus.RecType; } set { scheduleStatus.RecType = value; } }

        [FwLogicProperty(Id:"E0TAgMqA0y3O")]
        public string Color { get { return scheduleStatus.Color; } set { scheduleStatus.Color = value; } }

        [FwLogicProperty(Id:"WDRcZyN1853C")]
        public bool? WhiteText { get { return scheduleStatus.WhiteText; } set { scheduleStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"ki52Ojw8MFnp")]
        public bool? Inactive { get { return scheduleStatus.Inactive; } set { scheduleStatus.Inactive = value; } }

        [FwLogicProperty(Id:"LCIuCJh0pc0G")]
        public string DateStamp { get { return scheduleStatus.DateStamp; } set { scheduleStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}

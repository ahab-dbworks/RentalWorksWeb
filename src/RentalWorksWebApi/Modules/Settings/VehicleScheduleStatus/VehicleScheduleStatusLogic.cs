﻿using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ScheduleStatus;

namespace WebApi.Modules.Settings.VehicleScheduleStatus
{
    public class VehicleScheduleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ScheduleStatusRecord scheduleStatus = new ScheduleStatusRecord();
        VehicleScheduleStatusLoader scheduleStatusLoader = new VehicleScheduleStatusLoader();
        public VehicleScheduleStatusLogic()
        {
            dataRecords.Add(scheduleStatus);
            dataLoader = scheduleStatusLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ScheduleStatusId { get { return scheduleStatus.ScheduleStatusId; } set { scheduleStatus.ScheduleStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ScheduleStatus { get { return scheduleStatus.ScheduleStatus; } set { scheduleStatus.ScheduleStatus = value; } }
        public string StatusAction { get { return scheduleStatus.StatusAction; } set { scheduleStatus.StatusAction = value; } }
        [JsonIgnore]
        public string RecType { get { return scheduleStatus.RecType; } set { scheduleStatus.RecType = value; } }
        public string Color { get { return scheduleStatus.Color; } set { scheduleStatus.Color = value; } }
        public bool? WhiteText { get { return scheduleStatus.WhiteText; } set { scheduleStatus.WhiteText = value; } }
        public bool? Inactive { get { return scheduleStatus.Inactive; } set { scheduleStatus.Inactive = value; } }
        public string DateStamp { get { return scheduleStatus.DateStamp; } set { scheduleStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "V";
        }
        //------------------------------------------------------------------------------------

    }

}

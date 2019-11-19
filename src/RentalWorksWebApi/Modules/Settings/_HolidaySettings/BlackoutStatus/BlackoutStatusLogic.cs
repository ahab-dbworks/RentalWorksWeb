using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;

namespace WebApi.Modules.Settings.HolidaySettings.BlackoutStatus
{
    [FwLogic(Id:"jlBuK8QM4LB")]
    public class BlackoutStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        BlackoutStatusLoader resourceStatusLoader = new BlackoutStatusLoader();
        public BlackoutStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = "B";
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"DYoHbNtA82T", IsPrimaryKey:true)]
        public string BlackoutStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }

        [FwLogicProperty(Id:"DYoHbNtA82T", IsRecordTitle:true)]
        public string BlackoutStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }

        [FwLogicProperty(Id:"nLR1mALcs89Y")]
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"SlpPjnw4r45V")]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }

        [FwLogicProperty(Id:"AZw35nzSxCLw")]
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }

        [FwLogicProperty(Id:"8MY00cDilq3E")]
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"7sz6M7GAbVDq")]
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }

        [FwLogicProperty(Id:"o50qK8PZaydt")]
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }

        ////------------------------------------------------------------------------------------
    }
}

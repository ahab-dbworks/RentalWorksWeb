using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;

namespace WebApi.Modules.Settings.LaborSettings.CrewStatus
{
    [FwLogic(Id:"Zs6srGWv6D8i")]
    public class CrewStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        CrewStatusLoader resourceStatusLoader = new CrewStatusLoader();
        public CrewStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = "C";
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ktSREa0YV8bI", IsPrimaryKey:true)]
        public string CrewStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }

        [FwLogicProperty(Id:"ktSREa0YV8bI", IsRecordTitle:true)]
        public string CrewStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }

        [FwLogicProperty(Id:"INBpRyGApCQa")]
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"YAsRCDovzkcQ")]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }

        [FwLogicProperty(Id:"5D7MJuyVf9lv")]
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }

        [FwLogicProperty(Id:"8oUM8jTIunif")]
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"TorDn8z6rAvK")]
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }

        [FwLogicProperty(Id:"t3JuV9YNS8X9")]
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

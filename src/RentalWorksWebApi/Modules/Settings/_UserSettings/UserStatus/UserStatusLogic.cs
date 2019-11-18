using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;

namespace WebApi.Modules.Settings.UserStatus
{
    [FwLogic(Id:"TKvt6VYodyXnf")]
    public class UserStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        UserStatusLoader resourceStatusLoader = new UserStatusLoader();
        public UserStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = "U";
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"xl1wdw6L6w6i0", IsPrimaryKey:true)]
        public string UserStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }

        [FwLogicProperty(Id:"xl1wdw6L6w6i0", IsRecordTitle:true)]
        public string UserStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }

        [FwLogicProperty(Id:"ncoUIerU8pl9")]
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"kcxy0cuCJDPp")]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }

        [FwLogicProperty(Id:"PfYEG7VAoH2Q")]
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }

        [FwLogicProperty(Id:"BQH8WJjPs75V")]
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"KXhXAhXMYkxN")]
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }

        [FwLogicProperty(Id:"dntXthLWjfgR")]
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;

namespace WebApi.Modules.Settings.VehicleStatus
{
    public class VehicleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        VehicleStatusLoader resourceStatusLoader = new VehicleStatusLoader();
        public VehicleStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = "V";
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }
        [JsonIgnore]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}

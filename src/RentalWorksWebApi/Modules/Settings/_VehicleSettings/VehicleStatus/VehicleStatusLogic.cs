using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleStatus
{
    [FwLogic(Id:"DbukR2TY1PMGl")]
    public class VehicleStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        VehicleStatusLoader resourceStatusLoader = new VehicleStatusLoader();
        public VehicleStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = RwConstants.RESOURCE_STATUS_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"16MNQA5fmZqtr", IsPrimaryKey:true)]
        public string VehicleStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }

        [FwLogicProperty(Id:"16MNQA5fmZqtr", IsRecordTitle:true)]
        public string VehicleStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }

        [FwLogicProperty(Id:"8ItTz9WF9ydi")]
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"bfDwcH3cUcUt")]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }

        [FwLogicProperty(Id:"Vi8ED41qL7f6")]
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }

        [FwLogicProperty(Id:"tkC5aKWLk2eT")]
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"f3sYuq2bsLNy")]
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }

        [FwLogicProperty(Id:"K4w5SMUWyVqg")]
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

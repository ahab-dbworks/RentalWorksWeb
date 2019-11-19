using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.ResourceStatus;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityStatus
{
    [FwLogic(Id:"NMgY3ZJk2Tl")]
    public class FacilityStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ResourceStatusRecord resourceStatus = new ResourceStatusRecord();
        FacilityStatusLoader resourceStatusLoader = new FacilityStatusLoader();
        public FacilityStatusLogic()
        {
            dataRecords.Add(resourceStatus);
            dataLoader = resourceStatusLoader;
            RecType = "S";
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"lZZHkC7oNSl", IsPrimaryKey:true)]
        public string FacilityStatusId { get { return resourceStatus.ResourceStatusId; } set { resourceStatus.ResourceStatusId = value; } }

        [FwLogicProperty(Id:"lZZHkC7oNSl", IsRecordTitle:true)]
        public string FacilityStatus { get { return resourceStatus.ResourceStatus; } set { resourceStatus.ResourceStatus = value; } }

        [FwLogicProperty(Id:"GQXhaidhzprQ")]
        public bool? AvailableToSchedule { get { return resourceStatus.AvailableToSchedule; } set { resourceStatus.AvailableToSchedule = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"4ME2HnI2M5QR")]
        public string RecType { get { return resourceStatus.RecType; } set { resourceStatus.RecType = value; } }

        [FwLogicProperty(Id:"G8TOEUa2BZAT")]
        public string Color { get { return resourceStatus.Color; } set { resourceStatus.Color = value; } }

        [FwLogicProperty(Id:"r2pzPwikZ1gr")]
        public bool? WhiteText { get { return resourceStatus.WhiteText; } set { resourceStatus.WhiteText = value; } }

        [FwLogicProperty(Id:"fiuAbecmBiPM")]
        public bool? Inactive { get { return resourceStatus.Inactive; } set { resourceStatus.Inactive = value; } }

        [FwLogicProperty(Id:"QfLHxwLJ9twd")]
        public string DateStamp { get { return resourceStatus.DateStamp; } set { resourceStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

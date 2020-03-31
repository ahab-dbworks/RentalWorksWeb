using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.FacilitySettings.Building
{
    [FwLogic(Id:"XxD1nl5CkAe")]
    public class BuildingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BuildingRecord building = new BuildingRecord();
        BuildingLoader buildingLoader = new BuildingLoader();
        public BuildingLogic()
        {
            dataRecords.Add(building);
            dataLoader = buildingLoader;
            BuildingType = RwConstants.BUILDING_TYPE_BUILDING;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"9jGaeG2zahO", IsPrimaryKey:true)]
        public string BuildingId { get { return building.BuildingId; } set { building.BuildingId = value; } }

        [FwLogicProperty(Id:"9jGaeG2zahO", IsRecordTitle:true)]
        public string Building { get { return building.Building; } set { building.Building = value; } }

        [FwLogicProperty(Id:"ynzUrGz1pvCy")]
        public string BuildingCode { get { return building.BuildingCode; } set { building.BuildingCode = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"OITUrC5UYA0b")]
        public string BuildingType { get { return building.BuildingType; } set { building.BuildingType = value; } }

        [FwLogicProperty(Id:"LmsUzGUOF9CG")]
        public string OfficeLocationId { get { return building.OfficeLocationId; } set { building.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"EDt4oQ2ivQk", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"OVedtXhQz21R")]
        public bool? Inactive { get { return building.Inactive; } set { building.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

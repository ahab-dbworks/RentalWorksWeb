using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Building
{
    public class BuildingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BuildingRecord building = new BuildingRecord();
        BuildingLoader buildingLoader = new BuildingLoader();
        public BuildingLogic()
        {
            dataRecords.Add(building);
            dataLoader = buildingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string BuildingId { get { return building.BuildingId; } set { building.BuildingId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Building { get { return building.Building; } set { building.Building = value; } }
        public string BuildingCode { get { return building.BuildingCode; } set { building.BuildingCode = value; } }
        [JsonIgnore]
        public string BuildingType { get { return building.BuildingType; } set { building.BuildingType = value; } }
        public string OfficeLocationId { get { return building.OfficeLocationId; } set { building.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        //public string Webaddress { get { return building.Webaddress; } set { building.Webaddress = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Add1 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Add2 { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string City { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string State { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string CountryId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Country { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Zip { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Phone { get; set; }
        //public string TaxoptionId { get { return building.TaxoptionId; } set { building.TaxoptionId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Primarycontact { get; set; }
        public bool? Inactive { get { return building.Inactive; } set { building.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            BuildingType = "BUILDING";
        }
        //------------------------------------------------------------------------------------
    }
}
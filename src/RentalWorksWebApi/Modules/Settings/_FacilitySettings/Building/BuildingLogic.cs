using FwStandard.AppManager;
using FwStandard.BusinessLogic;
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
            BeforeSave += OnBeforeSave;
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

        //[FwLogicProperty(Id:"Mhmx7EErYZqq")]
        //public string Webaddress { get { return building.Webaddress; } set { building.Webaddress = value; } }

        //[FwLogicProperty(Id:"WalCN1Ib2CHZ")]
        //public string Add1 { get; set; }

        //[FwLogicProperty(Id:"kJqZWyIE5zCy")]
        //public string Add2 { get; set; }

        //[FwLogicProperty(Id:"znZaERrpUzEp")]
        //public string City { get; set; }

        //[FwLogicProperty(Id:"TW5iFfch2UGw")]
        //public string State { get; set; }

        //[FwLogicProperty(Id:"edYv6tdzCpeB")]
        //public string CountryId { get; set; }

        //[FwLogicProperty(Id:"VqKmlRtx6UNh")]
        //public string Country { get; set; }

        //[FwLogicProperty(Id:"ovqsLp2UXcOx")]
        //public string Zip { get; set; }

        //[FwLogicProperty(Id:"ERlhkmd12sgF")]
        //public string Phone { get; set; }

        //[FwLogicProperty(Id:"1SpwBUyjOywi")]
        //public string TaxoptionId { get { return building.TaxoptionId; } set { building.TaxoptionId = value; } }

        //[FwLogicProperty(Id:"rurGYiZZsUT5")]
        //public string Primarycontact { get; set; }

        [FwLogicProperty(Id:"OVedtXhQz21R")]
        public bool? Inactive { get { return building.Inactive; } set { building.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            BuildingType = "BUILDING";
        }
        //------------------------------------------------------------------------------------
    }
}

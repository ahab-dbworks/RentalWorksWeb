using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [FwLogic(Id: "vdIQcn1VYkpcB")]
    public class VenueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VenueRecord venue = new VenueRecord();
        VenueLoader venueLoader = new VenueLoader();
        public VenueLogic()
        {
            dataRecords.Add(venue);
            dataLoader = venueLoader;
            BuildingType = RwConstants.BUILDING_TYPE_VENUE;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YbAvXDsZI8piP", IsPrimaryKey:true)]
        public string VenueId { get { return venue.VenueId; } set { venue.VenueId = value; } }

        [FwLogicProperty(Id: "FN11XbUjJBiVk", IsRecordTitle:true)]
        public string Venue { get { return venue.Venue; } set { venue.Venue = value; } }

        [FwLogicProperty(Id: "X36qEs6N1Vk3D")]
        public string VenueCode { get { return venue.VenueCode; } set { venue.VenueCode = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "dOyvKfK1Yo8k8")]
        public string BuildingType { get { return venue.BuildingType; } set { venue.BuildingType = value; } }

        [FwLogicProperty(Id: "dJtOeSFLjxWTU")]
        public string OfficeLocationId { get { return venue.OfficeLocationId; } set { venue.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "TcXV42nf2kTFv", IsReadOnly:true)]
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

        [FwLogicProperty(Id: "gUDDhOO9kOWQW")]
        public bool? Inactive { get { return venue.Inactive; } set { venue.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

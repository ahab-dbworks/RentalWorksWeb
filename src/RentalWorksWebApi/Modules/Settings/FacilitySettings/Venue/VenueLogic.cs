using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Address;

namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [FwLogic(Id: "vdIQcn1VYkpcB")]
    public class VenueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VenueRecord venue = new VenueRecord();
        AddressRecord address = new AddressRecord();
        VenueLoader venueLoader = new VenueLoader();
        public VenueLogic()
        {
            dataRecords.Add(venue);
            dataRecords.Add(address);
            dataLoader = venueLoader;

            address.BeforeSave += OnBeforeSaveAddress;
            address.UniqueId1 = venue.VenueId;

            BuildingType = RwConstants.BUILDING_TYPE_VENUE;

            ForceSave = true;
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

        [FwLogicProperty(Id: "id2kWitqoCAlQ")]
        public string AddressId { get { return address.AddressId; } set { address.AddressId = value; } }

        [FwLogicProperty(Id: "l2PYMzsOy90Q2")]
        public string Address1 { get { return address.Address1; } set { address.Address1 = value; } }

        [FwLogicProperty(Id: "r0HXJCrJza1Kj")]
        public string Address2 { get { return address.Address2; } set { address.Address2 = value; } }

        [FwLogicProperty(Id: "JLmNZr23FdEVp")]
        public string City { get { return address.City; } set { address.City = value; } }

        [FwLogicProperty(Id: "5FvoloXD52hrC")]
        public string State { get { return address.State; } set { address.State = value; } }

        [FwLogicProperty(Id: "k3u9IGQB7aDBL")]
        public string ZipCode { get { return address.ZipCode; } set { address.ZipCode = value; } }

        [FwLogicProperty(Id: "1cn3f2NEbt1qu")]
        public string CountryId { get { return address.CountryId; } set { address.CountryId = value; } }

        [FwLogicProperty(Id: "LSgii7lqfhD0J", IsReadOnly: true)]
        public string Country { get; set; }

        [FwLogicProperty(Id: "1juZDHpNyzxOG")]
        public string Phone { get { return address.Phone; } set { address.Phone = value; } }

        [FwLogicProperty(Id: "Mhmx7EErYZqq")]
        public string WebAddress { get { return venue.WebAddress; } set { venue.WebAddress = value; } }

        //[FwLogicProperty(Id:"1SpwBUyjOywi")]
        //public string TaxoptionId { get { return building.TaxoptionId; } set { building.TaxoptionId = value; } }

        [FwLogicProperty(Id: "AspLhkonHVWEV")]
        public string PrimaryContact { get; set; }

        [FwLogicProperty(Id: "gUDDhOO9kOWQW")]
        public bool? Inactive { get { return venue.Inactive; } set { venue.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveAddress(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (AddressId.Equals(string.Empty))
            {
                e.PerformSave = false;
            }
        }
        //------------------------------------------------------------------------------------
    }
}

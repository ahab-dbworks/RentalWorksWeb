using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.HomeControls.Address;
using WebApi.Modules.HomeControls.CompanyContact;

namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [FwLogic(Id: "vdIQcn1VYkpcB")]
    public class VenueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VenueRecord venue = new VenueRecord();
        AddressRecord address = new AddressRecord();
        ContactRecord contact = new ContactRecord();
        CompanyContactRecord compContact = new CompanyContactRecord();
        VenueLoader venueLoader = new VenueLoader();

        public VenueLogic()
        {
            dataRecords.Add(venue);
            dataRecords.Add(address);
            dataRecords.Add(contact);
            dataRecords.Add(compContact);
            dataLoader = venueLoader;

            address.BeforeSave += OnBeforeSaveAddress;
            address.UniqueId1 = venue.VenueId;

            BuildingType = RwConstants.BUILDING_TYPE_VENUE;

            BeforeSave += OnBeforeSave;

            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YbAvXDsZI8piP", IsPrimaryKey: true)]
        public string VenueId { get { return venue.VenueId; } set { venue.VenueId = value; } }

        [FwLogicProperty(Id: "FN11XbUjJBiVk", IsRecordTitle: true)]
        public string Venue { get { return venue.Venue; } set { venue.Venue = value; } }

        [FwLogicProperty(Id: "X36qEs6N1Vk3D")]
        public string VenueCode { get { return venue.VenueCode; } set { venue.VenueCode = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "dOyvKfK1Yo8k8")]
        public string BuildingType { get { return venue.BuildingType; } set { venue.BuildingType = value; } }

        [FwLogicProperty(Id: "dJtOeSFLjxWTU")]
        public string OfficeLocationId { get { return venue.OfficeLocationId; } set { venue.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "TcXV42nf2kTFv", IsReadOnly: true)]
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

        [FwLogicProperty(Id: "yJi4WTATpF07V")]
        public string PrimaryContactId { get { return contact.ContactId; } set { contact.ContactId = value; } }

        [FwLogicProperty(Id: "a04by4SzrpOAs", IsReadOnly: true)]
        public string PrimaryCompanyContactId { get; set; }

        [FwLogicProperty(Id: "AspLhkonHVWEV", IsReadOnly: true)]
        public string PrimaryContact { get; set; }

        [FwLogicProperty(Id: "gUDDhOO9kOWQW")]
        public bool? Inactive { get { return venue.Inactive; } set { venue.Inactive = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveAddress(object sender, BeforeSaveDataRecordEventArgs e)
        {
            address.UniqueId1 = VenueId;
            if (AddressId.Equals(string.Empty))
            {
                e.PerformSave = false;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    VenueLogic orig = ((VenueLogic)e.Original);
                    AddressId = orig.AddressId;
                }
            }


            //if ContactId is provided
            if (PrimaryContactId != null)
            {
                //if CompanyContactId not provided, go get it from the database
                if (string.IsNullOrEmpty(PrimaryCompanyContactId))
                {
                    string[] columns = new string[] { "compcontactid" };
                    string[] wherecolumns = new string[] { "companyid", "primaryflag" };
                    string[] wherevalues = new string[] { VenueId , "T"};
                    PrimaryCompanyContactId = AppFunc.GetStringDataAsync(AppConfig, "compcontact", wherecolumns, wherevalues, columns).Result[0];
                }

                //if CompanyContactId not provided or found, then create a new one
                if ((string.IsNullOrEmpty(PrimaryCompanyContactId)) && (!string.IsNullOrEmpty(PrimaryContactId)))
                {
                    CompanyContactLogic l3 = new CompanyContactLogic();
                    l3.SetDependencies(this.AppConfig, this.UserSession);
                    l3.CompanyId = VenueId;
                    l3.ContactId = PrimaryContactId;
                    l3.IsPrimary = true;
                    int i1 = l3.SaveAsync(null).Result;
                    PrimaryCompanyContactId = l3.CompanyContactId; // we need this ID saved and indicated as Primary
                }

                CompanyContactLogic l4 = new CompanyContactLogic();
                l4.SetDependencies(this.AppConfig, this.UserSession);
                l4.CompanyContactId = PrimaryCompanyContactId;
                l4.ContactId = PrimaryContactId;
                l4.IsPrimary = true;
                int i2 = l4.SaveAsync(null).Result;


            }
        }
        //------------------------------------------------------------------------------------
    }
}

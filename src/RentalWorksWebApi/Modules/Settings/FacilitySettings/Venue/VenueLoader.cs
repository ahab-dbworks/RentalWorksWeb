using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [FwSqlTable("venuewebview")]
    public class VenueLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VenueId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "building", modeltype: FwDataTypes.Text)]
        public string Venue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingcode", modeltype: FwDataTypes.Text)]
        public string VenueCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingtype", modeltype: FwDataTypes.Text)]
        public string VenueType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        //public string TaxoptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarycontactid", modeltype: FwDataTypes.Text)]
        public string PrimaryContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarycompcontactid", modeltype: FwDataTypes.Text)]
        public string PrimaryCompanyContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarycontact", modeltype: FwDataTypes.Text)]
        public string PrimaryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------    
    }
}
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Vendor
{
    [FwSqlTable("vendorview")]
    public class VendorLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendornametype", modeltype: FwDataTypes.Text)]
        public string VendorNameType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fedid", modeltype: FwDataTypes.Text)]
        public string FederalIdNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text)]
        public string Salutation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text)]
        public string LastName { get; set; }
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
        [FwSqlDataField(column: "remitadd1", modeltype: FwDataTypes.Text)]
        public string RemitAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitadd2", modeltype: FwDataTypes.Text)]
        public string RemitAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitcity", modeltype: FwDataTypes.Text)]
        public string RemitCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitstate", modeltype: FwDataTypes.Text)]
        public string RemitState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitcountryid", modeltype: FwDataTypes.Text)]
        public string RemitCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitcountry", modeltype: FwDataTypes.Text)]
        public string RemitCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitzip", modeltype: FwDataTypes.Text)]
        public string RemitZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text)]
        public string Phone800 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text)]
        public string OtherPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internet", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

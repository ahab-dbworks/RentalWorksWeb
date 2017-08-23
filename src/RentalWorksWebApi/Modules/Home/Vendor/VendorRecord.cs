using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Vendor
{
    [FwSqlTable("vendor")]
    public class VendorRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string VendorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendornametype", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string VendorNameType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fedid", modeltype: FwDataTypes.Text, maxlength: 11)]
        public string FederalIdNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text, maxlength: 100, required: true)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string Salutation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text, maxlength: 1)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitadd1", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string RemitAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitadd2", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string RemitAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitcity", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string RemitCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitstate", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string RemitState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitcountryid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string RemitCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitzip", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string RemitZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Phone800 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string OtherPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internet", modeltype: FwDataTypes.Text, maxlength: 255)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, maxlength: 255)]
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

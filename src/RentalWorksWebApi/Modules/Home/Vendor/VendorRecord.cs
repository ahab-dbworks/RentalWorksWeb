using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.Vendor
{
    [FwSqlTable("vendor")]
    public class VendorRecord : AppDataReadWriteRecord
    {


/*
TODO:
        modby           char(20)
        inputby         char(20)
*/

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
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text, maxlength: 100)]
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
        [FwSqlDataField(column: "vendorfml", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string VendorDisplayName { get; set; }
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
        [FwSqlDataField(column: "vendorclassid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string VendorClassId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string OtherPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internet", modeltype: FwDataTypes.Text, maxlength: 255)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, maxlength: 255)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "activedt", modeltype: FwDataTypes.Date)]
        public string ActiveDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactivedt", modeltype: FwDataTypes.Date)]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsales", modeltype: FwDataTypes.Boolean)]
        public bool? SubSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean)]
        public bool? SubMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean)]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalinventory", modeltype: FwDataTypes.Boolean)]
        public bool? RentalInventory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesinventory", modeltype: FwDataTypes.Boolean)]
        public bool? SalesPartsInventory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Boolean)]
        public bool? Manufacturer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "freight", modeltype: FwDataTypes.Boolean)]
        public bool? Freight { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "insurance", modeltype: FwDataTypes.Boolean)]
        public bool? Insurance { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "daysinweek", modeltype: FwDataTypes.Decimal, precision: 5, scale: 3)]
        public decimal? DefaultSubRentDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "discountrate", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? DefaultSubRentDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ssdiscountrate", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? DefaultSubSaleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string DefaultRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customernumber", modeltype: FwDataTypes.Text, maxlength: 15)]
        public string AccountNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string DefaultPoClassificationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "organizationtypeid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string OrganizationTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string DefaultCurrencyId { get; set; }
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
        [FwSqlDataField(column: "remitpayeeno", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string RemitPayeeNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "externalid", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string ExternalId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "adjustcontractdate", modeltype: FwDataTypes.Boolean)]
        public bool? AutomaticallyAdjustContractDates { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "trackingnolink", modeltype: FwDataTypes.Text, maxlength: 255)]
        public string ShippingTrackingLink { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string DefaultOutgoingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string DefaultIncomingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date)]
        public string LastModifiedDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

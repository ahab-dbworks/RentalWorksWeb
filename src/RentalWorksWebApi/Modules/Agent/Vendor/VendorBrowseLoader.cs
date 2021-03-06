﻿using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Vendor
{
    [FwSqlTable("vendorview")]
    public class VendorBrowseLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorfml", modeltype: FwDataTypes.Text)]
        public string VendorDisplayName { get; set; }
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
        [FwSqlDataField(column: "vendorclass", modeltype: FwDataTypes.Text)]
        public string VendorClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string DefaultRate { get; set; }
        ////------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prcontactid", modeltype: FwDataTypes.Text)]
        public string PrimaryContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prcontact", modeltype: FwDataTypes.Text)]
        public string PrimaryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prphone", modeltype: FwDataTypes.Text)]
        public string PrimaryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prext", modeltype: FwDataTypes.Text)]
        public string PrimaryContactExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendornametype", modeltype: FwDataTypes.Text)]
        public string VendorNameType { get; set; }
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
        [FwSqlDataField(column: "vendorclassid", modeltype: FwDataTypes.Text)]
        public string VendorClassId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text)]
        public string OtherPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internet", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date)]
        public string ActiveDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date)]
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
        [FwSqlDataField(column: "daysinweek", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSubRentDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "discountrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSubRentDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ssdiscountrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSubSaleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customernumber", modeltype: FwDataTypes.Text)]
        public string AccountNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text)]
        public string DefaultPoClassificationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text)]
        public string DefaultPoClassification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "organizationtypeid", modeltype: FwDataTypes.Text)]
        public string OrganizationTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "organizationtype", modeltype: FwDataTypes.Text)]
        public string OrganizationType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "multiplecurrencies", modeltype: FwDataTypes.Boolean)]
        public bool? MultipleCurrencies { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string DefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string DefaultCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string DefaultCurrency { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string DefaultCurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitatt1", modeltype: FwDataTypes.Text)]
        public string RemitAttention1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remitatt2", modeltype: FwDataTypes.Text)]
        public string RemitAttention2 { get; set; }
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
        [FwSqlDataField(column: "remitpayeeno", modeltype: FwDataTypes.Text)]
        public string RemitPayeeNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "externalid", modeltype: FwDataTypes.Text)]
        public string ExternalId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "adjustcontractdate", modeltype: FwDataTypes.Boolean)]
        public bool? AutomaticallyAdjustContractDates { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "trackingnolink", modeltype: FwDataTypes.Text)]
        public string ShippingTrackingLink { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text)]
        public string DefaultOutgoingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text)]
        public string DefaultIncomingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("Manufacturer", "manufacturer", select, request);
            addFilterToSelect("Insurance", "insurance", select, request);
            addFilterToSelect("Freight", "freight", select, request);
            addFilterToSelect("SubRent", "subrent", select, request);
            addFilterToSelect("SubSale", "subsales", select, request);
            addFilterToSelect("SubMisc", "submisc", select, request);
            addFilterToSelect("SubLabor", "sublabor", select, request);
            addFilterToSelect("RentalInventory", "rentalinventory", select, request);
            addFilterToSelect("SalesInventory", "salesinventory", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}

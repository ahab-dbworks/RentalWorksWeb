using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Home.Vendor
{
    public class VendorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorRecord vendor = new VendorRecord();
        VendorLoader vendorLoader = new VendorLoader();
        public VendorLogic()
        {
            dataRecords.Add(vendor);
            dataLoader = vendorLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorId { get { return vendor.VendorId; } set { vendor.VendorId = value; } }
        public string VendorNameType { get { return vendor.VendorNameType; } set { vendor.VendorNameType = value; } }
        public string VendorNumber { get { return vendor.VendorNumber; } set { vendor.VendorNumber = value; } }
        public string FederalIdNumber { get { return vendor.FederalIdNumber; } set { vendor.FederalIdNumber = value; } }
        public string OfficeLocationId { get { return vendor.OfficeLocationId; } set { vendor.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string Vendor { get { return vendor.Vendor; } set { vendor.Vendor = value; } }
        public string Salutation { get { return vendor.Salutation; } set { vendor.Salutation = value; } }
        public string FirstName { get { return vendor.FirstName; } set { vendor.FirstName = value; } }
        public string MiddleInitial { get { return vendor.MiddleInitial; } set { vendor.MiddleInitial = value; } }
        public string LastName { get { return vendor.LastName; } set { vendor.LastName = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string VendorDisplayName { get; set; }
        public string Address1 { get { return vendor.Address1; } set { vendor.Address1 = value; } }
        public string Address2 { get { return vendor.Address2; } set { vendor.Address2 = value; } }
        public string City { get { return vendor.City; } set { vendor.City = value; } }
        public string State { get { return vendor.State; } set { vendor.State = value; } }
        public string CountryId { get { return vendor.CountryId; } set { vendor.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return vendor.ZipCode; } set { vendor.ZipCode = value; } }
        public string VendorClassId { get { return vendor.VendorClassId; } set { vendor.VendorClassId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorClass { get; set; }
        public string Phone { get { return vendor.Phone; } set { vendor.Phone = value; } }
        public string Fax { get { return vendor.Fax; } set { vendor.Fax = value; } }
        public string Phone800 { get { return vendor.Phone800; } set { vendor.Phone800 = value; } }
        public string OtherPhone { get { return vendor.OtherPhone; } set { vendor.OtherPhone = value; } }
        public string WebAddress { get { return vendor.WebAddress; } set { vendor.WebAddress = value; } }
        public string Email { get { return vendor.Email; } set { vendor.Email = value; } }
        public string ActiveDate { get { return vendor.ActiveDate; } set { vendor.ActiveDate = value; } }
        public string InactiveDate { get { return vendor.InactiveDate; } set { vendor.InactiveDate = value; } }
        public bool? SubRent { get { return vendor.SubRent; } set { vendor.SubRent = value; } }
        public bool? SubSales { get { return vendor.SubSales; } set { vendor.SubSales = value; } }
        public bool? SubMisc { get { return vendor.SubMisc; } set { vendor.SubMisc = value; } }
        public bool? SubLabor { get { return vendor.SubLabor; } set { vendor.SubLabor = value; } }
        public bool? SubVehicle { get { return vendor.SubVehicle; } set { vendor.SubVehicle = value; } }
        public bool? Repair { get { return vendor.Repair; } set { vendor.Repair = value; } }
        public bool? RentalInventory { get { return vendor.RentalInventory; } set { vendor.RentalInventory = value; } }
        public bool? SalesPartsInventory { get { return vendor.SalesPartsInventory; } set { vendor.SalesPartsInventory = value; } }
        public bool? Manufacturer { get { return vendor.Manufacturer; } set { vendor.Manufacturer = value; } }
        public bool? Freight { get { return vendor.Freight; } set { vendor.Freight = value; } }
        public bool? Insurance { get { return vendor.Insurance; } set { vendor.Insurance = value; } }
        public bool? Consignment { get { return vendor.Consignment; } set { vendor.Consignment = value; } }
        public decimal? DefaultSubRentDaysInWeek { get { return vendor.DefaultSubRentDaysInWeek; } set { vendor.DefaultSubRentDaysInWeek = value; } }
        public decimal? DefaultSubRentDiscountPercent { get { return vendor.DefaultSubRentDiscountPercent; } set { vendor.DefaultSubRentDiscountPercent = value; } }
        public decimal? DefaultSubSaleDiscountPercent { get { return vendor.DefaultSubSaleDiscountPercent; } set { vendor.DefaultSubSaleDiscountPercent = value; } }
        public string DefaultRate { get { return vendor.DefaultRate; } set { vendor.DefaultRate = value; } }
        public string BillingCycleId { get { return vendor.BillingCycleId; } set { vendor.BillingCycleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycle { get; set; }
        public string PaymentTermsId { get { return vendor.PaymentTermsId; } set { vendor.PaymentTermsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public string AccountNumber { get { return vendor.AccountNumber; } set { vendor.AccountNumber = value; } }
        public string DefaultPoClassificationId { get { return vendor.DefaultPoClassificationId; } set { vendor.DefaultPoClassificationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultPoClassification { get; set; }
        public string OrganizationTypeId { get { return vendor.OrganizationTypeId; } set { vendor.OrganizationTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrganizationType { get; set; }
        public string DefaultCurrencyId{ get { return vendor.DefaultCurrencyId; } set { vendor.DefaultCurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultCurrencyCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultCurrency { get; set; }
        public string RemitAddress1 { get { return vendor.RemitAddress1; } set { vendor.RemitAddress1 = value; } }
        public string RemitAddress2 { get { return vendor.RemitAddress2; } set { vendor.RemitAddress2 = value; } }
        public string RemitCity { get { return vendor.RemitCity; } set { vendor.RemitCity = value; } }
        public string RemitState { get { return vendor.RemitState; } set { vendor.RemitState = value; } }
        public string RemitCountryId { get { return vendor.RemitCountryId; } set { vendor.RemitCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RemitCountry { get; set; }
        public string RemitZipCode { get { return vendor.RemitZipCode; } set { vendor.RemitZipCode = value; } }
        public string RemitPayeeNo { get { return vendor.RemitPayeeNo; } set { vendor.RemitPayeeNo = value; } }
        public string ExternalId { get { return vendor.ExternalId; } set { vendor.ExternalId = value; } }
        public bool? AutomaticallyAdjustContractDates { get { return vendor.AutomaticallyAdjustContractDates; } set { vendor.AutomaticallyAdjustContractDates = value; } }
        public string ShippingTrackingLink { get { return vendor.ShippingTrackingLink; } set { vendor.ShippingTrackingLink = value; } }
        public string DefaultOutgoingDeliveryType { get { return vendor.DefaultOutgoingDeliveryType; } set { vendor.DefaultOutgoingDeliveryType = value; } }
        public string DefaultIncomingDeliveryType { get { return vendor.DefaultIncomingDeliveryType; } set { vendor.DefaultIncomingDeliveryType = value; } }
        public string CustomerId { get { return vendor.CustomerId; } set { vendor.CustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        public string InputDate { get { return vendor.InputDate; } set { vendor.InputDate = value; } }
        public string LastModifiedDate { get { return vendor.LastModifiedDate; } set { vendor.LastModifiedDate = value; } }
        public bool? Inactive { get { return vendor.Inactive; } set { vendor.Inactive = value; } }
        public string DateStamp { get { return vendor.DateStamp; } set { vendor.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

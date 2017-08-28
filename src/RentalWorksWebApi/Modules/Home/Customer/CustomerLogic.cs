using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Customer
{
    public class CustomerLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerRecord customer = new CustomerRecord();
        CustomerLoader customerLoader = new CustomerLoader();
        public CustomerLogic()
        {
            dataRecords.Add(customer);
            dataLoader = customerLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerId { get { return customer.CustomerId; } set { customer.CustomerId = value; } }
        public string CustomerNumber { get { return customer.CustomerNumber; } set { customer.CustomerNumber = value; } }
        public string OfficeLocationId { get { return customer.OfficeLocationId; } set { customer.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Customer { get { return customer.Customer; } set { customer.Customer = value; } }
        //public string Salutation { get { return customer.Salutation; } set { customer.Salutation = value; } }
        //public string FirstName { get { return customer.FirstName; } set { customer.FirstName = value; } }
        //public string MiddleInitial { get { return customer.MiddleInitial; } set { customer.MiddleInitial = value; } }
        //public string LastName { get { return customer.LastName; } set { customer.LastName = value; } }
        //public string Address1 { get { return customer.Address1; } set { customer.Address1 = value; } }
        //public string Address2 { get { return customer.Address2; } set { customer.Address2 = value; } }
        //public string City { get { return customer.City; } set { customer.City = value; } }
        //public string State { get { return customer.State; } set { customer.State = value; } }
        //public string CountryId { get { return customer.CountryId; } set { customer.CountryId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Country { get; set; }
        //public string ZipCode { get { return customer.ZipCode; } set { customer.ZipCode = value; } }
        //public string Phone { get { return customer.Phone; } set { customer.Phone = value; } }
        //public string Fax { get { return customer.Fax; } set { customer.Fax = value; } }
        //public string Phone800 { get { return customer.Phone800; } set { customer.Phone800 = value; } }
        //public string OtherPhone { get { return customer.OtherPhone; } set { customer.OtherPhone = value; } }
        //public string WebAddress { get { return customer.WebAddress; } set { customer.WebAddress = value; } }
        //public string Email { get { return customer.Email; } set { customer.Email = value; } }
        //public string ActiveDate { get { return customer.ActiveDate; } set { customer.ActiveDate = value; } }
        //public string InactiveDate { get { return customer.InactiveDate; } set { customer.InactiveDate = value; } }
        //public bool SubRent { get { return customer.SubRent; } set { customer.SubRent = value; } }
        //public bool SubSales { get { return customer.SubSales; } set { customer.SubSales = value; } }
        //public bool SubMisc { get { return customer.SubMisc; } set { customer.SubMisc = value; } }
        //public bool SubLabor { get { return customer.SubLabor; } set { customer.SubLabor = value; } }
        //public bool SubVehicle { get { return customer.SubVehicle; } set { customer.SubVehicle = value; } }
        //public bool Repair { get { return customer.Repair; } set { customer.Repair = value; } }
        //public bool RentalInventory { get { return customer.RentalInventory; } set { customer.RentalInventory = value; } }
        //public bool SalesPartsInventory { get { return customer.SalesPartsInventory; } set { customer.SalesPartsInventory = value; } }
        //public bool Manufacturer { get { return customer.Manufacturer; } set { customer.Manufacturer = value; } }
        //public bool Freight { get { return customer.Freight; } set { customer.Freight = value; } }
        //public bool Insurance { get { return customer.Insurance; } set { customer.Insurance = value; } }
        //public bool Consignment { get { return customer.Consignment; } set { customer.Consignment = value; } }
        //public decimal DefaultSubRentDaysInWeek { get { return customer.DefaultSubRentDaysInWeek; } set { customer.DefaultSubRentDaysInWeek = value; } }
        //public decimal DefaultSubRentDiscountPercent { get { return customer.DefaultSubRentDiscountPercent; } set { customer.DefaultSubRentDiscountPercent = value; } }
        //public decimal DefaultSubSaleDiscountPercent { get { return customer.DefaultSubSaleDiscountPercent; } set { customer.DefaultSubSaleDiscountPercent = value; } }
        //public string DefaultRate { get { return customer.DefaultRate; } set { customer.DefaultRate = value; } }
        //public string BillingCycleId { get { return customer.BillingCycleId; } set { customer.BillingCycleId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string BillingCycle { get; set; }
        //public string PaymentTermsId { get { return customer.PaymentTermsId; } set { customer.PaymentTermsId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PaymentTerms { get; set; }
        //public string AccountNumber { get { return customer.AccountNumber; } set { customer.AccountNumber = value; } }
        //public string DefaultPoClassificationId { get { return customer.DefaultPoClassificationId; } set { customer.DefaultPoClassificationId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DefaultPoClassification { get; set; }
        //public string OrganizationTypeId { get { return customer.OrganizationTypeId; } set { customer.OrganizationTypeId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string OrganizationType { get; set; }
        //public string DefaultCurrencyId{ get { return customer.DefaultCurrencyId; } set { customer.DefaultCurrencyId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DefaultCurrencyCode { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DefaultCurrency { get; set; }
        //public string RemitAddress1 { get { return customer.RemitAddress1; } set { customer.RemitAddress1 = value; } }
        //public string RemitAddress2 { get { return customer.RemitAddress2; } set { customer.RemitAddress2 = value; } }
        //public string RemitCity { get { return customer.RemitCity; } set { customer.RemitCity = value; } }
        //public string RemitState { get { return customer.RemitState; } set { customer.RemitState = value; } }
        //public string RemitCountryId { get { return customer.RemitCountryId; } set { customer.RemitCountryId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string RemitCountry { get; set; }
        //public string RemitZipCode { get { return customer.RemitZipCode; } set { customer.RemitZipCode = value; } }
        //public string RemitPayeeNo { get { return customer.RemitPayeeNo; } set { customer.RemitPayeeNo = value; } }
        //public string ExternalId { get { return customer.ExternalId; } set { customer.ExternalId = value; } }
        //public bool AutomaticallyAdjustContractDates { get { return customer.AutomaticallyAdjustContractDates; } set { customer.AutomaticallyAdjustContractDates = value; } }
        //public string ShippingTrackingLink { get { return customer.ShippingTrackingLink; } set { customer.ShippingTrackingLink = value; } }
        //public string DefaultOutgoingDeliveryType { get { return customer.DefaultOutgoingDeliveryType; } set { customer.DefaultOutgoingDeliveryType = value; } }
        //public string DefaultIncomingDeliveryType { get { return customer.DefaultIncomingDeliveryType; } set { customer.DefaultIncomingDeliveryType = value; } }
        public bool Inactive { get { return customer.Inactive; } set { customer.Inactive = value; } }
        public string DateStamp { get { return customer.DateStamp; } set { customer.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Agent.Vendor
{
    [FwLogic(Id:"NfWAU19hFpk10")]
    public class VendorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorRecord vendor = new VendorRecord();
        VendorLoader vendorLoader = new VendorLoader();
        VendorBrowseLoader vendorBrowseLoader = new VendorBrowseLoader();
        public VendorLogic()
        {
            dataRecords.Add(vendor);
            dataLoader = vendorLoader;
            browseLoader = vendorBrowseLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"OpfqKqQFmxZmU", IsPrimaryKey:true)]
        public string VendorId { get { return vendor.VendorId; } set { vendor.VendorId = value; } }

        [FwLogicProperty(Id:"wqIa0XOyxH8b")]
        public string VendorNameType { get { return vendor.VendorNameType; } set { vendor.VendorNameType = value; } }

        [FwLogicProperty(Id:"ttni35STEcwa")]
        public string VendorNumber { get { return vendor.VendorNumber; } set { vendor.VendorNumber = value; } }

        [FwLogicProperty(Id:"3EKLQbJ81eDZ")]
        public string FederalIdNumber { get { return vendor.FederalIdNumber; } set { vendor.FederalIdNumber = value; } }

        [FwLogicProperty(Id:"iwb5CoWnbc0x")]
        public string OfficeLocationId { get { return vendor.OfficeLocationId; } set { vendor.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"IM2uMgOAltwrv", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"pjOAyzQsyj52")]
        public string Vendor { get { return vendor.Vendor; } set { vendor.Vendor = value; } }

        [FwLogicProperty(Id:"iklzH7mgueNr")]
        public string Salutation { get { return vendor.Salutation; } set { vendor.Salutation = value; } }

        [FwLogicProperty(Id:"8We5SWGoj3TX")]
        public string FirstName { get { return vendor.FirstName; } set { vendor.FirstName = value; } }

        [FwLogicProperty(Id:"UHMiFDxOvwId")]
        public string MiddleInitial { get { return vendor.MiddleInitial; } set { vendor.MiddleInitial = value; } }

        [FwLogicProperty(Id:"12ICHJIuP7q3")]
        public string LastName { get { return vendor.LastName; } set { vendor.LastName = value; } }

        [FwLogicProperty(Id:"OpfqKqQFmxZmU", IsRecordTitle:true, IsReadOnly:true)]
        public string VendorDisplayName { get; set; }

        [FwLogicProperty(Id:"OkrSLjsSsZyj")]
        public string Address1 { get { return vendor.Address1; } set { vendor.Address1 = value; } }

        [FwLogicProperty(Id:"DYGQntOwwCC9")]
        public string Address2 { get { return vendor.Address2; } set { vendor.Address2 = value; } }

        [FwLogicProperty(Id:"I4MSkkNOBmh0")]
        public string City { get { return vendor.City; } set { vendor.City = value; } }

        [FwLogicProperty(Id:"xSdOCmqLLsJ1")]
        public string State { get { return vendor.State; } set { vendor.State = value; } }

        [FwLogicProperty(Id:"h7d3E56LDhfV")]
        public string CountryId { get { return vendor.CountryId; } set { vendor.CountryId = value; } }

        [FwLogicProperty(Id:"Zcmi5vvkA7KSJ", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"fuvDbzyRrxLK")]
        public string ZipCode { get { return vendor.ZipCode; } set { vendor.ZipCode = value; } }

        [FwLogicProperty(Id:"6LSnLn5WJX5c")]
        public string VendorClassId { get { return vendor.VendorClassId; } set { vendor.VendorClassId = value; } }

        [FwLogicProperty(Id:"OpfqKqQFmxZmU", IsReadOnly:true)]
        public string VendorClass { get; set; }

        [FwLogicProperty(Id:"eu5yVqcarC6F")]
        public string Phone { get { return vendor.Phone; } set { vendor.Phone = value; } }

        [FwLogicProperty(Id:"57woPgYDEiWI")]
        public string Fax { get { return vendor.Fax; } set { vendor.Fax = value; } }

        [FwLogicProperty(Id:"tPj88RfxNz1t")]
        public string PhoneTollFree { get { return vendor.PhoneTollFree; } set { vendor.PhoneTollFree = value; } }

        [FwLogicProperty(Id:"tyIXDggf9G2j")]
        public string OtherPhone { get { return vendor.OtherPhone; } set { vendor.OtherPhone = value; } }

        [FwLogicProperty(Id:"EGbxkmLxaisB")]
        public string WebAddress { get { return vendor.WebAddress; } set { vendor.WebAddress = value; } }

        [FwLogicProperty(Id:"IODsg83oNQZ1")]
        public string Email { get { return vendor.Email; } set { vendor.Email = value; } }

        [FwLogicProperty(Id:"Wb3IsGMT0bwzi", IsReadOnly:true)]
        public string ActiveDate { get { return vendor.ActiveDate; } set { vendor.ActiveDate = value; } }

        [FwLogicProperty(Id:"4NG3UDWRpDLp2", IsReadOnly:true)]
        public string InactiveDate { get { return vendor.InactiveDate; } set { vendor.InactiveDate = value; } }

        [FwLogicProperty(Id:"vWobo6fsnt3B")]
        public bool? SubRent { get { return vendor.SubRent; } set { vendor.SubRent = value; } }

        [FwLogicProperty(Id:"P5V6QzW91DZk")]
        public bool? SubSales { get { return vendor.SubSales; } set { vendor.SubSales = value; } }

        [FwLogicProperty(Id:"lUJHf44CWhNZ")]
        public bool? SubMisc { get { return vendor.SubMisc; } set { vendor.SubMisc = value; } }

        [FwLogicProperty(Id:"HAAi7Lg0KaAl")]
        public bool? SubLabor { get { return vendor.SubLabor; } set { vendor.SubLabor = value; } }

        [FwLogicProperty(Id:"s37JwbEqWQ1l")]
        public bool? SubVehicle { get { return vendor.SubVehicle; } set { vendor.SubVehicle = value; } }

        [FwLogicProperty(Id:"tsDyKnr87bHC")]
        public bool? Repair { get { return vendor.Repair; } set { vendor.Repair = value; } }

        [FwLogicProperty(Id:"x74VZxHTqNTD")]
        public bool? RentalInventory { get { return vendor.RentalInventory; } set { vendor.RentalInventory = value; } }

        [FwLogicProperty(Id:"tOFxF2vPK1xQ")]
        public bool? SalesPartsInventory { get { return vendor.SalesPartsInventory; } set { vendor.SalesPartsInventory = value; } }

        [FwLogicProperty(Id:"Gi3qEZKDEimt")]
        public bool? Manufacturer { get { return vendor.Manufacturer; } set { vendor.Manufacturer = value; } }

        [FwLogicProperty(Id:"u9IRPxOtxS05")]
        public bool? Freight { get { return vendor.Freight; } set { vendor.Freight = value; } }

        [FwLogicProperty(Id:"yJGFwnzgzOlt")]
        public bool? Insurance { get { return vendor.Insurance; } set { vendor.Insurance = value; } }

        [FwLogicProperty(Id:"WZjKPm8R2yBP")]
        public bool? Consignment { get { return vendor.Consignment; } set { vendor.Consignment = value; } }

        [FwLogicProperty(Id:"UKf5ZCItm9Wt")]
        public decimal? DefaultSubRentDaysPerWeek { get { return vendor.DefaultSubRentDaysPerWeek; } set { vendor.DefaultSubRentDaysPerWeek = value; } }

        [FwLogicProperty(Id:"OKNBLkwlj9RE")]
        public decimal? DefaultSubRentDiscountPercent { get { return vendor.DefaultSubRentDiscountPercent; } set { vendor.DefaultSubRentDiscountPercent = value; } }

        [FwLogicProperty(Id:"a8SaQS67nyxv")]
        public decimal? DefaultSubSaleDiscountPercent { get { return vendor.DefaultSubSaleDiscountPercent; } set { vendor.DefaultSubSaleDiscountPercent = value; } }

        [FwLogicProperty(Id:"87Cq0lPxcPrB")]
        public string DefaultRate { get { return vendor.DefaultRate; } set { vendor.DefaultRate = value; } }

        [FwLogicProperty(Id:"SN5TwjplLArP")]
        public string BillingCycleId { get { return vendor.BillingCycleId; } set { vendor.BillingCycleId = value; } }

        [FwLogicProperty(Id:"cMo9vh9HBDbbE", IsReadOnly:true)]
        public string BillingCycle { get; set; }

        [FwLogicProperty(Id:"fUE9NGFkKUcy")]
        public string PaymentTermsId { get { return vendor.PaymentTermsId; } set { vendor.PaymentTermsId = value; } }

        [FwLogicProperty(Id:"pney8rG9Y0moK", IsReadOnly:true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id:"R7BtuwF9v4Yw")]
        public string AccountNumber { get { return vendor.AccountNumber; } set { vendor.AccountNumber = value; } }

        [FwLogicProperty(Id:"FbNxruD65mSl")]
        public string DefaultPoClassificationId { get { return vendor.DefaultPoClassificationId; } set { vendor.DefaultPoClassificationId = value; } }

        [FwLogicProperty(Id:"Qkb5Ty2mHdsA2", IsReadOnly:true)]
        public string DefaultPoClassification { get; set; }

        [FwLogicProperty(Id:"glLRfID4NFNm")]
        public string OrganizationTypeId { get { return vendor.OrganizationTypeId; } set { vendor.OrganizationTypeId = value; } }

        [FwLogicProperty(Id:"GS36s38qXKGQj", IsReadOnly:true)]
        public string OrganizationType { get; set; }

        [FwLogicProperty(Id:"cVQmxGBAORmV")]
        public string DefaultCurrencyId{ get { return vendor.DefaultCurrencyId; } set { vendor.DefaultCurrencyId = value; } }

        [FwLogicProperty(Id:"jg0Xdg8tDIZYH", IsReadOnly:true)]
        public string DefaultCurrencyCode { get; set; }

        [FwLogicProperty(Id:"jg0Xdg8tDIZYH", IsReadOnly:true)]
        public string DefaultCurrency { get; set; }

        [FwLogicProperty(Id:"IN08Lces3riq")]
        public string RemitAddress1 { get { return vendor.RemitAddress1; } set { vendor.RemitAddress1 = value; } }

        [FwLogicProperty(Id:"c8zGO7lkcUBc")]
        public string RemitAddress2 { get { return vendor.RemitAddress2; } set { vendor.RemitAddress2 = value; } }

        [FwLogicProperty(Id:"CJG2oN2MZiO2")]
        public string RemitCity { get { return vendor.RemitCity; } set { vendor.RemitCity = value; } }

        [FwLogicProperty(Id:"IKuaRsc3bUh5")]
        public string RemitState { get { return vendor.RemitState; } set { vendor.RemitState = value; } }

        [FwLogicProperty(Id:"qWfVOw7Ln6JE")]
        public string RemitCountryId { get { return vendor.RemitCountryId; } set { vendor.RemitCountryId = value; } }

        [FwLogicProperty(Id:"Zcmi5vvkA7KSJ", IsReadOnly:true)]
        public string RemitCountry { get; set; }

        [FwLogicProperty(Id:"jQ72Qr4nSvJV")]
        public string RemitZipCode { get { return vendor.RemitZipCode; } set { vendor.RemitZipCode = value; } }

        [FwLogicProperty(Id:"i0jFsk2JQeFF")]
        public string RemitPayeeNo { get { return vendor.RemitPayeeNo; } set { vendor.RemitPayeeNo = value; } }

        [FwLogicProperty(Id:"EJkSwerketcX")]
        public string ExternalId { get { return vendor.ExternalId; } set { vendor.ExternalId = value; } }

        [FwLogicProperty(Id:"kSuMtEBWrw3q")]
        public bool? AutomaticallyAdjustContractDates { get { return vendor.AutomaticallyAdjustContractDates; } set { vendor.AutomaticallyAdjustContractDates = value; } }

        [FwLogicProperty(Id:"2twybnYYWw44")]
        public string ShippingTrackingLink { get { return vendor.ShippingTrackingLink; } set { vendor.ShippingTrackingLink = value; } }

        [FwLogicProperty(Id:"gmYtR7kbaSRR")]
        public string DefaultOutgoingDeliveryType { get { return vendor.DefaultOutgoingDeliveryType; } set { vendor.DefaultOutgoingDeliveryType = value; } }

        [FwLogicProperty(Id:"5sJnaDpp1Dxd")]
        public string DefaultIncomingDeliveryType { get { return vendor.DefaultIncomingDeliveryType; } set { vendor.DefaultIncomingDeliveryType = value; } }

        [FwLogicProperty(Id:"AmdPVOJjNpWk")]
        public string CustomerId { get { return vendor.CustomerId; } set { vendor.CustomerId = value; } }

        [FwLogicProperty(Id:"gjc6bM4jeKgXk", IsReadOnly:true)]
        public string Customer { get; set; }

        [FwLogicProperty(Id:"2ZypXYr5arSU")]
        public string InputDate { get { return vendor.InputDate; } set { vendor.InputDate = value; } }

        [FwLogicProperty(Id:"Y33qZNaHylmg")]
        public string LastModifiedDate { get { return vendor.LastModifiedDate; } set { vendor.LastModifiedDate = value; } }

        [FwLogicProperty(Id:"YYQSf8BHXNnk")]
        public string PrimaryContactId { get; set; }

        [FwLogicProperty(Id:"U3smptINfu9V")]
        public string PrimaryContact { get; set; }

        [FwLogicProperty(Id:"cRNDG2CHCP7O")]
        public string PrimaryContactPhone { get; set; }

        [FwLogicProperty(Id:"VMaKwDcPOJH3")]
        public string PrimaryContactExtension { get; set; }

        [FwLogicProperty(Id:"GgWj503bpjIe")]
        public bool? Inactive { get { return vendor.Inactive; } set { vendor.Inactive = value; } }

        [FwLogicProperty(Id:"f3MsFrMzyFct")]
        public string DateStamp { get { return vendor.DateStamp; } set { vendor.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Agent.Deal
{
    [FwLogic(Id:"kXtdBU97WHSu")]
    public class DealLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealRecord deal = new DealRecord();
        DealLoader dealLoader = new DealLoader();
        DealBrowseLoader dealBrowseLoader = new DealBrowseLoader();
        private string tmpDealNumber = string.Empty;


        public DealLogic()
        {
            dataRecords.Add(deal);
            dataLoader = dealLoader;
            browseLoader = dealBrowseLoader;

            BeforeValidate += OnBeforeValidate;
            BeforeSave += OnBeforeSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qJWJGegtsy0x", IsPrimaryKey:true)]
        public string DealId { get { return deal.DealId; } set { deal.DealId = value; } }

        [FwLogicProperty(Id:"qJWJGegtsy0x", IsRecordTitle:true)]
        public string Deal { get { return deal.Deal; } set { deal.Deal = value; } }

        [FwLogicProperty(Id:"Mv53Cddofgam")]
        public string DealNumber { get { return deal.DealNumber; } set { deal.DealNumber = value; } }

        [FwLogicProperty(Id:"Fm0hHmX9HkYB")]
        public string CustomerId { get { return deal.CustomerId; } set { deal.CustomerId = value; } }

        [FwLogicProperty(Id:"wRdPpA7fzApR", IsReadOnly:true)]
        public string Customer { get; set; }

        [FwLogicProperty(Id:"AHNWRG32QMHN")]
        public string OfficeLocationId { get { return deal.OfficeLocationId; } set { deal.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"hE75eNnZR5cQ", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"qJWJGegtsy0x", IsReadOnly:true)]
        public string DealTypeId { get { return deal.DealTypeId; } set { deal.DealTypeId = value; } }

        [FwLogicProperty(Id:"Q3Fsazhsim2W")]
        public string DealType { get; set; }

        [FwLogicProperty(Id:"ky5h6oMh2TL2")]
        public string Address1 { get { return deal.Address1; } set { deal.Address1 = value; } }

        [FwLogicProperty(Id:"veTX89GwMl6f")]
        public string Address2 { get { return deal.Address2; } set { deal.Address2 = value; } }

        [FwLogicProperty(Id:"W72CLBwWyQyy")]
        public string City { get { return deal.City; } set { deal.City = value; } }

        [FwLogicProperty(Id:"grpsuyGYEhNB")]
        public string State { get { return deal.State; } set { deal.State = value; } }

        [FwLogicProperty(Id:"Fl42IB1DNB8K")]
        public string ZipCode { get { return deal.ZipCode; } set { deal.ZipCode = value; } }

        [FwLogicProperty(Id:"E4WZ0JLRYqup")]
        public string CountryId { get { return deal.CountryId; } set { deal.CountryId = value; } }

        [FwLogicProperty(Id:"jtXuaOZcqC3g", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"DX7IS2KZpm4m")]
        public string Phone { get { return deal.Phone; } set { deal.Phone = value; } }

        [FwLogicProperty(Id:"QZPCuAO1lD8x")]
        public string PhoneTollFree { get { return deal.PhoneTollFree; } set { deal.PhoneTollFree = value; } }

        [FwLogicProperty(Id:"xbmlSIpuZHRf")]
        public string Fax { get { return deal.Fax; } set { deal.Fax = value; } }

        [FwLogicProperty(Id:"LAnRe8p8JF81")]
        public string PhoneOther { get { return deal.PhoneOther; } set { deal.PhoneOther = value; } }

        [FwLogicProperty(Id:"58rOQpdse9Nt")]
        public string DepartmentId { get { return deal.DepartmentId; } set { deal.DepartmentId = value; } }

        [FwLogicProperty(Id:"vRTRDzcF3yCq", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"bsidO2kcPnQf")]
        public string CsrId { get { return deal.CsrId; } set { deal.CsrId = value; } }

        [FwLogicProperty(Id:"BTUNCs2oxp6r", IsReadOnly:true)]
        public string Csr { get; set; }

        [FwLogicProperty(Id:"Q58PKOv8Ohq9")]
        public string DefaultAgentId { get { return deal.DefaultAgentId; } set { deal.DefaultAgentId = value; } }

        [FwLogicProperty(Id:"WlEslE5YyH32", IsReadOnly:true)]
        public string DefaultAgent { get; set; }

        [FwLogicProperty(Id:"7pKtKXsMuRCq")]
        public string DefaultProjectManagerId { get { return deal.DefaultProjectManagerId; } set { deal.DefaultProjectManagerId = value; } }

        [FwLogicProperty(Id:"qP4MZj8m2Av5", IsReadOnly:true)]
        public string DefaultProjectManager { get; set; }

        [FwLogicProperty(Id:"DzhDCNp6wTzr")]
        public string DealClassificationId { get { return deal.DealClassificationId; } set { deal.DealClassificationId = value; } }

        [FwLogicProperty(Id:"qJWJGegtsy0x", IsReadOnly:true)]
        public string DealClassification { get; set; }

        [FwLogicProperty(Id:"nS48rzQii0be")]
        public string ProductionTypeId { get { return deal.ProductionTypeId; } set { deal.ProductionTypeId = value; } }

        [FwLogicProperty(Id:"SiQI1oYMazvb", IsReadOnly:true)]
        public string ProductionType { get; set; }

        [FwLogicProperty(Id:"igoatbos6HWd")]
        public string DealStatusId { get { return deal.DealStatusId; } set { deal.DealStatusId = value; } }

        [FwLogicProperty(Id:"qJWJGegtsy0x", IsReadOnly:true)]
        public string DealStatus { get; set; }

        [FwLogicProperty(Id:"YyKZJ7g2EqCq")]
        public string StatusAsOf { get { return deal.StatusAsOf; } set { deal.StatusAsOf = value; } }

        [FwLogicProperty(Id:"bhW4Xx46xyMf")]
        public string ExpectedWrapDate { get { return deal.ExpectedWrapDate; } set { deal.ExpectedWrapDate = value; } }

        [FwLogicProperty(Id:"z0dRjYhydFzz")]
        public string BillingCycleId { get { return deal.BillingCycleId; } set { deal.BillingCycleId = value; } }

        [FwLogicProperty(Id:"CRw5YQfcokIg", IsReadOnly:true)]
        public string BillingCycle { get; set; }

        [FwLogicProperty(Id:"ssk7P1o6KBqX")]
        public string PaymentTermsId { get { return deal.PaymentTermsId; } set { deal.PaymentTermsId = value; } }

        [FwLogicProperty(Id:"zWiesEbMuEI6", IsReadOnly:true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id:"gH7OXZkgV3bz")]
        public string PaymentTypeId { get { return deal.PaymentTypeId; } set { deal.PaymentTypeId = value; } }

        [FwLogicProperty(Id:"YTvQIzoxnOki", IsReadOnly:true)]
        public string PaymentType { get; set; }

        [FwLogicProperty(Id:"xozBWzsF5fyr")]
        public string DefaultRate { get { return deal.DefaultRate; } set { deal.DefaultRate = value; } }


        [FwLogicProperty(Id:"w4NMUZnj45BH")]
        public bool? UseCustomerDiscount { get { return deal.UseCustomerDiscount; } set { deal.UseCustomerDiscount = value; } }

        [FwLogicProperty(Id: "Y0ylW3kC5enRF", IsReadOnly: true)]
        public string CustomerDiscountTemplateId { get; set; }

        [FwLogicProperty(Id:"8zgDWbPPvAc0")]
        public bool? UseDiscountTemplate { get { return deal.UseDiscountTemplate; } set { deal.UseDiscountTemplate = value; } }

        [FwLogicProperty(Id:"OwilIoNLIPz4")]
        public string DiscountTemplateId { get { return deal.DiscountTemplateId; } set { deal.DiscountTemplateId = value; } }

        [FwLogicProperty(Id:"cEARwV3J6npn", IsReadOnly:true)]
        public string DiscountTemplate { get; set; }

        [FwLogicProperty(Id: "PL01YhahTMjQv", IsReadOnly: true)]
        public decimal? RentalDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "xnuv8y3sCAtsc", IsReadOnly: true)]
        public decimal? RentalDiscountPercent { get; set; }

        [FwLogicProperty(Id: "1NRcXcUae4wMg", IsReadOnly: true)]
        public decimal? SalesDiscountPercent { get; set; }

        [FwLogicProperty(Id: "uNhrmdXcY9AaI", IsReadOnly: true)]
        public decimal? FacilitiesDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "N7KbLVlhKEOKu", IsReadOnly: true)]
        public decimal? FacilitiesDiscountPercent { get; set; }

        [FwLogicProperty(Id:"fTFp2vNaRpJs")]
        public string OutsideSalesRepresentativeId { get { return deal.OutsideSalesRepresentativeId; } set { deal.OutsideSalesRepresentativeId = value; } }

        [FwLogicProperty(Id:"7HeZi7dpeA2r", IsReadOnly:true)]
        public string OutsideSalesRepresentative { get; set; }

        [FwLogicProperty(Id:"0gy8C0Qxf5D3")]
        public decimal? CommissionRate { get { return deal.CommissionRate; } set { deal.CommissionRate = value; } }

        [FwLogicProperty(Id:"TiWFR9GhAn5Z")]
        public bool? CommissionIncludesVendorItems { get { return deal.CommissionIncludesVendorItems; } set { deal.CommissionIncludesVendorItems = value; } }

        [FwLogicProperty(Id:"Qkn9vh7gNjsf")]
        public bool? PoRequired { get { return deal.PoRequired; } set { deal.PoRequired = value; } }

        [FwLogicProperty(Id:"KSRPwl9gLzdE")]
        public string PoType { get { return deal.PoType; } set { deal.PoType = value; } }

        [FwLogicProperty(Id:"ruUsPFRphuT6")]
        public string BillToAddressType { get { return deal.BillToAddressType; } set { deal.BillToAddressType = value; } }

        [FwLogicProperty(Id:"B0Ue0lAawmPx")]
        public string BillToAttention1 { get { return deal.BillToAttention1; } set { deal.BillToAttention1 = value; } }

        [FwLogicProperty(Id:"weLv3A73MDDd")]
        public string BillToAttention2 { get { return deal.BillToAttention2; } set { deal.BillToAttention2 = value; } }

        [FwLogicProperty(Id:"Y7701mK8CQOF")]
        public string BillToAddress1 { get { return deal.BillToAddress1; } set { deal.BillToAddress1 = value; } }

        [FwLogicProperty(Id:"mhh4M3pdqzIq")]
        public string BillToAddress2 { get { return deal.BillToAddress2; } set { deal.BillToAddress2 = value; } }

        [FwLogicProperty(Id:"YzuCxobvgsvz")]
        public string BillToCity { get { return deal.BillToCity; } set { deal.BillToCity = value; } }

        [FwLogicProperty(Id:"S7mRu9RXEyur")]
        public string BillToState { get { return deal.BillToState; } set { deal.BillToState = value; } }

        [FwLogicProperty(Id:"OhOHIqIFOaqD")]
        public string BillToCountryId { get { return deal.BillToCountryId; } set { deal.BillToCountryId = value; } }

        [FwLogicProperty(Id:"ixFxNDtZfryQ", IsReadOnly:true)]
        public string BillToCountry { get; set; }

        [FwLogicProperty(Id:"3Fm702iiEpI4")]
        public string BillToZipCode { get { return deal.BillToZipCode; } set { deal.BillToZipCode = value; } }

        [FwLogicProperty(Id:"u15LvlnIqS0N")]
        public bool? AssessFinanceCharge { get { return deal.AssessFinanceCharge; } set { deal.AssessFinanceCharge = value; } }

        [FwLogicProperty(Id:"gIaYRiRo8P9f")]
        public bool? AllowBillingScheduleOverride { get { return deal.AllowBillingScheduleOverride; } set { deal.AllowBillingScheduleOverride = value; } }

        [FwLogicProperty(Id:"uypyjqGXZFiQ")]
        public bool? AllowRebateCreditInvoices { get { return deal.AllowRebateCreditInvoices; } set { deal.AllowRebateCreditInvoices = value; } }

        [FwLogicProperty(Id:"CynEqiZ6XCrC")]
        public bool? UseCustomerCredit { get { return deal.UseCustomerCredit; } set { deal.UseCustomerCredit = value; } }

        [FwLogicProperty(Id:"q79aryyUuqWH")]
        public string CreditStatusId { get { return deal.CreditStatusId; } set { deal.CreditStatusId = value; } }

        [FwLogicProperty(Id:"h49LjMCSpHbA", IsReadOnly:true)]
        public string CreditStatus { get; set; }

        [FwLogicProperty(Id:"9omygUcZU8I4")]
        public string CreditStatusThrough { get { return deal.CreditStatusThrough; } set { deal.CreditStatusThrough = value; } }

        [FwLogicProperty(Id:"NdnwXoZpIInh")]
        public bool? CreditApplicationOnFile { get { return deal.CreditApplicationOnFile; } set { deal.CreditApplicationOnFile = value; } }

        [FwLogicProperty(Id:"8e1hECrzudjK")]
        public bool? UnlimitedCredit { get { return deal.UnlimitedCredit; } set { deal.UnlimitedCredit = value; } }

        [FwLogicProperty(Id:"ROQq0wWLn3sB")]
        public int? CreditLimit { get { return deal.CreditLimit; } set { deal.CreditLimit = value; } }

        [FwLogicProperty(Id:"6dpk4yjdItoG", IsReadOnly:true)]
        public int? CreditBalance { get; set; }

        [FwLogicProperty(Id:"zpF5eZJfBSz0", IsReadOnly:true)]
        public int? CreditAvailable { get; set; }

        [FwLogicProperty(Id:"xmyr7QYxHTsC", IsReadOnly:true)]
        public int? CustomerCreditLimit { get; set; }

        [FwLogicProperty(Id:"6dpk4yjdItoG", IsReadOnly:true)]
        public int? CustomerCreditBalance { get; set; }

        [FwLogicProperty(Id:"zpF5eZJfBSz0", IsReadOnly:true)]
        public int? CustomerCreditAvailable { get; set; }

        [FwLogicProperty(Id:"p3JfLYmkUZdv")]
        public bool? CreditResponsiblePartyOnFile { get { return deal.CreditResponsiblePartyOnFile; } set { deal.CreditResponsiblePartyOnFile = value; } }

        [FwLogicProperty(Id:"11kw5EgBrl58")]
        public string CreditResponsibleParty { get { return deal.CreditResponsibleParty; } set { deal.CreditResponsibleParty = value; } }

        [FwLogicProperty(Id:"qeibxFnhCMH3")]
        public bool? TradeReferencesVerified { get { return deal.TradeReferencesVerified; } set { deal.TradeReferencesVerified = value; } }

        [FwLogicProperty(Id:"Emnf1y5XMqQv")]
        public string TradeReferencesVerifiedBy { get { return deal.TradeReferencesVerifiedBy; } set { deal.TradeReferencesVerifiedBy = value; } }

        [FwLogicProperty(Id:"2ZIBWvkEfgXv")]
        public string TradeReferencesVerifiedOn { get { return deal.TradeReferencesVerifiedOn; } set { deal.TradeReferencesVerifiedOn = value; } }

        [FwLogicProperty(Id:"0dFjje8VaPSa")]
        public string CreditCardTypeId { get { return deal.CreditCardTypeId; } set { deal.CreditCardTypeId = value; } }

        [FwLogicProperty(Id:"t6AXB7e4BPAc", IsReadOnly:true)]
        public string CreditCardType { get; set; }

        [FwLogicProperty(Id:"6n1O5I1emrzI")]
        public int? CreditCardLimit { get { return deal.CreditCardLimit; } set { deal.CreditCardLimit = value; } }

        [FwLogicProperty(Id:"vdjClEWWdIA0")]
        public string CreditCardNumber { get { return deal.CreditCardNumber; } set { deal.CreditCardNumber = value; } }

        [FwLogicProperty(Id:"45yTAHuUVw6O")]
        public string CreditCardCode { get { return deal.CreditCardCode; } set { deal.CreditCardCode = value; } }

        [FwLogicProperty(Id:"kZUYUYqScruI")]
        public string CreditCardName { get { return deal.CreditCardName; } set { deal.CreditCardName = value; } }

        [FwLogicProperty(Id:"jaJfKGIQuEq1")]
        public int? CreditCardExpirationMonth { get { return deal.CreditCardExpirationMonth; } set { deal.CreditCardExpirationMonth = value; } }

        [FwLogicProperty(Id:"g0L8a8FDLtY5")]
        public int? CreditCardExpirationYear { get { return deal.CreditCardExpirationYear; } set { deal.CreditCardExpirationYear = value; } }

        [FwLogicProperty(Id:"sfFCgOyYB5bY")]
        public bool? CreditCardAuthorizationFormOnFile { get { return deal.CreditCardAuthorizationFormOnFile; } set { deal.CreditCardAuthorizationFormOnFile = value; } }

        [FwLogicProperty(Id:"7zvmUBIJ9PEY")]
        public decimal? DepletingDepositThresholdAmount { get { return deal.DepletingDepositThresholdAmount; } set { deal.DepletingDepositThresholdAmount = value; } }

        [FwLogicProperty(Id:"x8aSO9PFLT8G")]
        public int? DepletingDepositThresholdPercent { get { return deal.DepletingDepositThresholdPercent; } set { deal.DepletingDepositThresholdPercent = value; } }

        [FwLogicProperty(Id:"2Ux5UK5e5LjE", IsReadOnly:true)]
        public decimal? DepletingDepositTotal { get; set; }

        [FwLogicProperty(Id:"9YgLBK2mNrVw", IsReadOnly:true)]
        public decimal? DepletingDepositApplied { get; set; }

        [FwLogicProperty(Id:"I9X94nanz0c1", IsReadOnly:true)]
        public decimal? DepletingDepositRemaining { get; set; }

        [FwLogicProperty(Id:"obrbxdbYsC1v")]
        public bool? UseCustomerInsurance { get { return deal.UseCustomerInsurance; } set { deal.UseCustomerInsurance = value; } }

        [FwLogicProperty(Id:"vrSPGwhSNgxf")]
        public bool? InsuranceCertification { get { return deal.InsuranceCertification; } set { deal.InsuranceCertification = value; } }

        [FwLogicProperty(Id:"Yukv9dGG7BCl")]
        public string InsuranceCertificationValidThrough { get { return deal.InsuranceCertificationValidThrough; } set { deal.InsuranceCertificationValidThrough = value; } }

        [FwLogicProperty(Id:"0EVVNGNUUoYP")]
        public int? InsuranceCoverageLiability { get { return deal.InsuranceCoverageLiability; } set { deal.InsuranceCoverageLiability = value; } }

        [FwLogicProperty(Id:"hf3bHCSg9k5d")]
        public int? InsuranceCoverageLiabilityDeductible { get { return deal.InsuranceCoverageLiabilityDeductible; } set { deal.InsuranceCoverageLiabilityDeductible = value; } }

        [FwLogicProperty(Id:"5VatLawTYZyR")]
        public int? InsuranceCoverageProperty { get { return deal.InsuranceCoverageProperty; } set { deal.InsuranceCoverageProperty = value; } }

        [FwLogicProperty(Id:"jGjBkS40BA3v")]
        public int? InsuranceCoveragePropertyDeductible { get { return deal.InsuranceCoveragePropertyDeductible; } set { deal.InsuranceCoveragePropertyDeductible = value; } }

        [FwLogicProperty(Id:"RWBrEh4hyH4O", IsReadOnly:true)]
        public decimal? SecurityDepositAmount { get; set; }

        [FwLogicProperty(Id:"3cP0IyRvJQBl")]
        public string InsuranceCompanyId { get { return deal.InsuranceCompanyId; } set { deal.InsuranceCompanyId = value; } }

        [FwLogicProperty(Id:"V3eOCH5jRVVX", IsReadOnly:true)]
        public string InsuranceCompany { get; set; }

        [FwLogicProperty(Id:"ruwtGvkP1uDG")]
        public string InsuranceCompanyAgent { get { return deal.InsuranceCompanyAgent; } set { deal.InsuranceCompanyAgent = value; } }

        [FwLogicProperty(Id:"R9Dyroa0ZJuv", IsReadOnly:true)]
        public string InsuranceCompanyAddress1 { get; set; }

        [FwLogicProperty(Id:"BFgjwmmV2gri", IsReadOnly:true)]
        public string InsuranceCompanyAddress2 { get; set; }

        [FwLogicProperty(Id:"dvUNmeKHeta0", IsReadOnly:true)]
        public string InsuranceCompanyCity { get; set; }

        [FwLogicProperty(Id:"V3eOCH5jRVVX", IsReadOnly:true)]
        public string InsuranceCompanyState { get; set; }

        [FwLogicProperty(Id:"V3eOCH5jRVVX", IsReadOnly:true)]
        public string InsuranceCompanyZipCode { get; set; }

        [FwLogicProperty(Id:"jtXuaOZcqC3g", IsReadOnly:true)]
        public string InsuranceCompanyCountryId { get; set; }

        [FwLogicProperty(Id:"jtXuaOZcqC3g", IsReadOnly:true)]
        public string InsuranceCompanyCountry { get; set; }

        [FwLogicProperty(Id:"V3eOCH5jRVVX", IsReadOnly:true)]
        public string InsuranceCompanyPhone { get; set; }

        [FwLogicProperty(Id:"IjMZCiel9urU", IsReadOnly:true)]
        public string InsuranceCompanyFax { get; set; }

        [FwLogicProperty(Id:"ZvKvGzoYxzH1")]
        public bool? VehicleInsuranceCertification { get { return deal.VehicleInsuranceCertification; } set { deal.VehicleInsuranceCertification = value; } }

        [FwLogicProperty(Id:"cb44l3QDJYMu")]
        public bool? UseCustomerTax { get { return deal.UseCustomerTax; } set { deal.UseCustomerTax = value; } }

        [FwLogicProperty(Id:"hi0IOXNasbKn")]
        public bool? Taxable { get { return deal.Taxable; } set { deal.Taxable = value; } }

        [FwLogicProperty(Id:"6OthUK1c16ZA")]
        public string TaxStateOfIncorporationId { get { return deal.TaxStateOfIncorporationId; } set { deal.TaxStateOfIncorporationId = value; } }

        [FwLogicProperty(Id:"L4vrdVHsLzV3", IsReadOnly:true)]
        public string TaxStateOfIncorporation { get; set; }

        [FwLogicProperty(Id:"O5HNqxJtqg15")]
        public string TaxFederalNo { get { return deal.TaxFederalNo; } set { deal.TaxFederalNo = value; } }

        [FwLogicProperty(Id:"67gWI8jagQuz")]
        public int? NonTaxableYear { get { return deal.NonTaxableYear; } set { deal.NonTaxableYear = value; } }

        [FwLogicProperty(Id:"POHe38OvMZOD")]
        public string NonTaxableCertificateNo { get { return deal.NonTaxableCertificateNo; } set { deal.NonTaxableCertificateNo = value; } }

        [FwLogicProperty(Id:"7CW0F6E8re09")]
        public string NonTaxableCertificateValidThrough { get { return deal.NonTaxableCertificateValidThrough; } set { deal.NonTaxableCertificateValidThrough = value; } }

        [FwLogicProperty(Id:"P4HINQSZDef0")]
        public bool? NonTaxableCertificateOnFile { get { return deal.NonTaxableCertificateOnFile; } set { deal.NonTaxableCertificateOnFile = value; } }

        [FwLogicProperty(Id:"sOXv5UMdttK0")]
        public bool? DisableQuoteOrderActivity { get { return deal.DisableQuoteOrderActivity; } set { deal.DisableQuoteOrderActivity = value; } }

        [FwLogicProperty(Id:"UeyhkLK2eW5S")]
        public bool? DisableRental { get { return deal.DisableRental; } set { deal.DisableRental = value; } }

        [FwLogicProperty(Id:"B9I6TWgHcjy8")]
        public bool? DisableSales { get { return deal.DisableSales; } set { deal.DisableSales = value; } }

        [FwLogicProperty(Id:"1cWC6Qou4Vod")]
        public bool? DisableFacilities { get { return deal.DisableFacilities; } set { deal.DisableFacilities = value; } }

        [FwLogicProperty(Id:"FB3GL4T3GqNz")]
        public bool? DisableTransportation { get { return deal.DisableTransportation; } set { deal.DisableTransportation = value; } }

        [FwLogicProperty(Id:"108F3plHF6R4")]
        public bool? DisableLabor { get { return deal.DisableLabor; } set { deal.DisableLabor = value; } }

        [FwLogicProperty(Id:"TBWPelj2gMks")]
        public bool? DisableMisc { get { return deal.DisableMisc; } set { deal.DisableMisc = value; } }

        [FwLogicProperty(Id:"xKs9LOSi92r4")]
        public bool? DisableRentalSale { get { return deal.DisableRentalSale; } set { deal.DisableRentalSale = value; } }

        [FwLogicProperty(Id:"FykwQcHwt7WZ")]
        public bool? DisableSubRental { get { return deal.DisableSubRental; } set { deal.DisableSubRental = value; } }

        [FwLogicProperty(Id:"qA0PQRmLYNSv")]
        public bool? DisableSubSale { get { return deal.DisableSubSale; } set { deal.DisableSubSale = value; } }

        [FwLogicProperty(Id:"iPaoS0tHceKB")]
        public bool? DisableSubLabor { get { return deal.DisableSubLabor; } set { deal.DisableSubLabor = value; } }

        [FwLogicProperty(Id:"285eshUlnbOV")]
        public bool? DisableSubMisc { get { return deal.DisableSubMisc; } set { deal.DisableSubMisc = value; } }

        [FwLogicProperty(Id:"P5iIYpCwAjby")]
        public string DefaultOutgoingDeliveryType { get { return deal.DefaultOutgoingDeliveryType; } set { deal.DefaultOutgoingDeliveryType = value; } }

        [FwLogicProperty(Id:"1J1XblQ1lZFR")]
        public string DefaultIncomingDeliveryType { get { return deal.DefaultIncomingDeliveryType; } set { deal.DefaultIncomingDeliveryType = value; } }

        [FwLogicProperty(Id:"U4FybS3zD95Y")]
        public string ShippingAddressType { get { return deal.ShippingAddressType; } set { deal.ShippingAddressType = value; } }

        [FwLogicProperty(Id:"PnK4pbm53yZl")]
        public string ShipAttention { get { return deal.ShipAttention; } set { deal.ShipAttention = value; } }

        [FwLogicProperty(Id:"nGBIJwusc0zT")]
        public string ShipAddress1 { get { return deal.ShipAddress1; } set { deal.ShipAddress1 = value; } }

        [FwLogicProperty(Id:"OIFGTHjWs2Qx")]
        public string ShipAddress2 { get { return deal.ShipAddress2; } set { deal.ShipAddress2 = value; } }

        [FwLogicProperty(Id:"wrnnaa2zErfh")]
        public string ShipCity { get { return deal.ShipCity; } set { deal.ShipCity = value; } }

        [FwLogicProperty(Id:"ypcBxKmmmgCU")]
        public string ShipState { get { return deal.ShipState; } set { deal.ShipState = value; } }

        [FwLogicProperty(Id:"crlkRFtABjsn")]
        public string ShipCountryId { get { return deal.ShipCountryId; } set { deal.ShipCountryId = value; } }

        [FwLogicProperty(Id:"jtXuaOZcqC3g", IsReadOnly:true)]
        public string ShipCountry { get; set; }

        [FwLogicProperty(Id:"O5gdHiQ8Nigc")]
        public string ShipZipCode { get { return deal.ShipZipCode; } set { deal.ShipZipCode = value; } }

        [FwLogicProperty(Id:"ic8YO2guZJj6")]
        public bool? RebateRental { get { return deal.RebateRental; } set { deal.RebateRental = value; } }

        [FwLogicProperty(Id:"owdgR4Hp0X9s")]
        public string RebateCustomerId { get { return deal.RebateCustomerId; } set { deal.RebateCustomerId = value; } }

        [FwLogicProperty(Id:"wRdPpA7fzApR", IsReadOnly:true)]
        public string RebateCustomer { get; set; }

        [FwLogicProperty(Id:"axk8nP4Tqbpp")]
        public int? OwnedEquipmentRebateRentalPerecent { get { return deal.OwnedEquipmentRebateRentalPerecent; } set { deal.OwnedEquipmentRebateRentalPerecent = value; } }

        [FwLogicProperty(Id:"vqCwNU6wSRPv")]
        public int? SubRentalEquipmentRebateRentalPerecent { get { return deal.SubRentalEquipmentRebateRentalPerecent; } set { deal.SubRentalEquipmentRebateRentalPerecent = value; } }

        [FwLogicProperty(Id: "iKVnae22M2DY")]
        public bool? EnableWebQuoteRequest { get { return deal.EnableWebQuoteRequest; } set { deal.EnableWebQuoteRequest = value; } }

        [FwLogicProperty(Id: "NPAGfWCWVefvJ")]
        public string Email { get { return deal.Email; } set { deal.Email = value; } }

        [FwLogicProperty(Id: "PVNBtgZdqW5")]
        public string DateStamp { get { return deal.DateStamp; } set { deal.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeValidate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(DealNumber))
                {
                    tmpDealNumber = AppFunc.GetNextIdAsync(AppConfig).Result;
                    DealNumber = tmpDealNumber;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if ((string.IsNullOrEmpty(DealNumber)) || (DealNumber.Equals(tmpDealNumber)))
                {
                    bool x = deal.SetNumber(e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

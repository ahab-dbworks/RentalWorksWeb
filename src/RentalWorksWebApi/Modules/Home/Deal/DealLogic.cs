using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.Deal
{
    public class DealLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealRecord deal = new DealRecord();
        DealLoader dealLoader = new DealLoader();
        DealBrowseLoader dealBrowseLoader = new DealBrowseLoader();
        public DealLogic()
        {
            dataRecords.Add(deal);
            dataLoader = dealLoader;
            browseLoader = dealBrowseLoader;

            deal.BeforeSave += OnBeforeSaveDeal;

        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealId { get { return deal.DealId; } set { deal.DealId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Deal { get { return deal.Deal; } set { deal.Deal = value; } }
        public string DealNumber { get { return deal.DealNumber; } set { deal.DealNumber = value; } }
        public string CustomerId { get { return deal.CustomerId; } set { deal.CustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        public string LocationId { get { return deal.LocationId; } set { deal.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealTypeId { get { return deal.DealTypeId; } set { deal.DealTypeId = value; } }
        public string DealType { get; set; }
        public string Address1 { get { return deal.Address1; } set { deal.Address1 = value; } }
        public string Address2 { get { return deal.Address2; } set { deal.Address2 = value; } }
        public string City { get { return deal.City; } set { deal.City = value; } }
        public string State { get { return deal.State; } set { deal.State = value; } }
        public string ZipCode { get { return deal.ZipCode; } set { deal.ZipCode = value; } }
        public string CountryId { get { return deal.CountryId; } set { deal.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string Phone { get { return deal.Phone; } set { deal.Phone = value; } }
        public string Phone800 { get { return deal.Phone800; } set { deal.Phone800 = value; } }
        public string Fax { get { return deal.Fax; } set { deal.Fax = value; } }
        public string PhoneOther { get { return deal.PhoneOther; } set { deal.PhoneOther = value; } }
        public string DepartmentId { get { return deal.DepartmentId; } set { deal.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string CsrId { get { return deal.CsrId; } set { deal.CsrId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Csr { get; set; }
        public string DefaultAgentId { get { return deal.DefaultAgentId; } set { deal.DefaultAgentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultAgent { get; set; }
        public string DefaultProjectManagerId { get { return deal.DefaultProjectManagerId; } set { deal.DefaultProjectManagerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DefaultProjectManager { get; set; }
        public string DealClassificationId { get { return deal.DealClassificationId; } set { deal.DealClassificationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealClassification { get; set; }
        public string ProductionTypeId { get { return deal.ProductionTypeId; } set { deal.ProductionTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProductionType { get; set; }
        public string DealStatusId { get { return deal.DealStatusId; } set { deal.DealStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealStatus { get; set; }
        public string StatusAsOf { get { return deal.StatusAsOf; } set { deal.StatusAsOf = value; } }
        public string ExpectedWrapDate { get { return deal.ExpectedWrapDate; } set { deal.ExpectedWrapDate = value; } }
        public string BillingCycleId { get { return deal.BillingCycleId; } set { deal.BillingCycleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingCycle { get; set; }
        public string PaymentTermsId { get { return deal.PaymentTermsId; } set { deal.PaymentTermsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public string PaymentTypeId { get { return deal.PaymentTypeId; } set { deal.PaymentTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentType { get; set; }
        public string DefaultRate { get { return deal.DefaultRate; } set { deal.DefaultRate = value; } }

        public bool? UseCustomerDiscount { get { return deal.UseCustomerDiscount; } set { deal.UseCustomerDiscount = value; } }
        public bool? UseDiscountTemplate { get { return deal.UseDiscountTemplate; } set { deal.UseDiscountTemplate = value; } }
        public string DiscountTemplateId { get { return deal.DiscountTemplateId; } set { deal.DiscountTemplateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DiscountTemplate { get; set; }
        public string OutsideSalesRepresentativeId { get { return deal.OutsideSalesRepresentativeId; } set { deal.OutsideSalesRepresentativeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }
        public decimal? CommissionRate { get { return deal.CommissionRate; } set { deal.CommissionRate = value; } }
        public bool? CommissionIncludesVendorItems { get { return deal.CommissionIncludesVendorItems; } set { deal.CommissionIncludesVendorItems = value; } }
        public bool? PoRequired { get { return deal.PoRequired; } set { deal.PoRequired = value; } }
        public string PoType { get { return deal.PoType; } set { deal.PoType = value; } }
        public string BillToAddressType { get { return deal.BillToAddressType; } set { deal.BillToAddressType = value; } }
        public string BillToAttention1 { get { return deal.BillToAttention1; } set { deal.BillToAttention1 = value; } }
        public string BillToAttention2 { get { return deal.BillToAttention2; } set { deal.BillToAttention2 = value; } }
        public string BillToAddress1 { get { return deal.BillToAddress1; } set { deal.BillToAddress1 = value; } }
        public string BillToAddress2 { get { return deal.BillToAddress2; } set { deal.BillToAddress2 = value; } }
        public string BillToCity { get { return deal.BillToCity; } set { deal.BillToCity = value; } }
        public string BillToState { get { return deal.BillToState; } set { deal.BillToState = value; } }
        public string BillToCountryId { get { return deal.BillToCountryId; } set { deal.BillToCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToCountry { get; set; }
        public string BillToZipCode { get { return deal.BillToZipCode; } set { deal.BillToZipCode = value; } }
        public bool? AssessFinanceCharge { get { return deal.AssessFinanceCharge; } set { deal.AssessFinanceCharge = value; } }
        public bool? AllowBillingScheduleOverride { get { return deal.AllowBillingScheduleOverride; } set { deal.AllowBillingScheduleOverride = value; } }
        public bool? AllowRebateCreditInvoices { get { return deal.AllowRebateCreditInvoices; } set { deal.AllowRebateCreditInvoices = value; } }
        public bool? UseCustomerCredit { get { return deal.UseCustomerCredit; } set { deal.UseCustomerCredit = value; } }
        public string CreditStatusId { get { return deal.CreditStatusId; } set { deal.CreditStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CreditStatus { get; set; }
        public string CreditStatusThrough { get { return deal.CreditStatusThrough; } set { deal.CreditStatusThrough = value; } }
        public bool? CreditApplicationOnFile { get { return deal.CreditApplicationOnFile; } set { deal.CreditApplicationOnFile = value; } }
        public bool? UnlimitedCredit { get { return deal.UnlimitedCredit; } set { deal.UnlimitedCredit = value; } }
        public int? CreditLimit { get { return deal.CreditLimit; } set { deal.CreditLimit = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CreditBalance { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CreditAvailable { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CustomerCreditLimit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CustomerCreditBalance { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CustomerCreditAvailable { get; set; }
        public bool? CreditResponsiblePartyOnFile { get { return deal.CreditResponsiblePartyOnFile; } set { deal.CreditResponsiblePartyOnFile = value; } }
        public string CreditResponsibleParty { get { return deal.CreditResponsibleParty; } set { deal.CreditResponsibleParty = value; } }
        public bool? TradeReferencesVerified { get { return deal.TradeReferencesVerified; } set { deal.TradeReferencesVerified = value; } }
        public string TradeReferencesVerifiedBy { get { return deal.TradeReferencesVerifiedBy; } set { deal.TradeReferencesVerifiedBy = value; } }
        public string TradeReferencesVerifiedOn { get { return deal.TradeReferencesVerifiedOn; } set { deal.TradeReferencesVerifiedOn = value; } }
        public string CreditCardTypeId { get { return deal.CreditCardTypeId; } set { deal.CreditCardTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CreditCardType { get; set; }
        public int? CreditCardLimit { get { return deal.CreditCardLimit; } set { deal.CreditCardLimit = value; } }
        public string CreditCardNumber { get { return deal.CreditCardNumber; } set { deal.CreditCardNumber = value; } }
        public string CreditCardCode { get { return deal.CreditCardCode; } set { deal.CreditCardCode = value; } }
        public string CreditCardName { get { return deal.CreditCardName; } set { deal.CreditCardName = value; } }
        public int? CreditCardExpirationMonth { get { return deal.CreditCardExpirationMonth; } set { deal.CreditCardExpirationMonth = value; } }
        public int? CreditCardExpirationYear { get { return deal.CreditCardExpirationYear; } set { deal.CreditCardExpirationYear = value; } }
        public bool? CreditCardAuthorizationFormOnFile { get { return deal.CreditCardAuthorizationFormOnFile; } set { deal.CreditCardAuthorizationFormOnFile = value; } }
        public decimal? DepletingDepositThresholdAmount { get { return deal.DepletingDepositThresholdAmount; } set { deal.DepletingDepositThresholdAmount = value; } }
        public int? DepletingDepositThresholdPercent { get { return deal.DepletingDepositThresholdPercent; } set { deal.DepletingDepositThresholdPercent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DepletingDepositTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DepletingDepositApplied { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DepletingDepositRemaining { get; set; }
        public bool? UseCustomerInsurance { get { return deal.UseCustomerInsurance; } set { deal.UseCustomerInsurance = value; } }
        public bool? InsuranceCertification { get { return deal.InsuranceCertification; } set { deal.InsuranceCertification = value; } }
        public string InsuranceCertificationValidThrough { get { return deal.InsuranceCertificationValidThrough; } set { deal.InsuranceCertificationValidThrough = value; } }
        public int? InsuranceCoverageLiability { get { return deal.InsuranceCoverageLiability; } set { deal.InsuranceCoverageLiability = value; } }
        public int? InsuranceCoverageLiabilityDeductible { get { return deal.InsuranceCoverageLiabilityDeductible; } set { deal.InsuranceCoverageLiabilityDeductible = value; } }
        public int? InsuranceCoverageProperty { get { return deal.InsuranceCoverageProperty; } set { deal.InsuranceCoverageProperty = value; } }
        public int? InsuranceCoveragePropertyDeductible { get { return deal.InsuranceCoveragePropertyDeductible; } set { deal.InsuranceCoveragePropertyDeductible = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SecurityDepositAmount { get; set; }
        public string InsuranceCompanyId { get { return deal.InsuranceCompanyId; } set { deal.InsuranceCompanyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompany { get; set; }
        public string InsuranceCompanyAgent { get { return deal.InsuranceCompanyAgent; } set { deal.InsuranceCompanyAgent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyAddress1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyAddress2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyCity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyState { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyZipCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyCountryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyCountry { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyPhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompanyFax { get; set; }
        public bool? VehicleInsuranceCertification { get { return deal.VehicleInsuranceCertification; } set { deal.VehicleInsuranceCertification = value; } }
        public bool? UseCustomerTax { get { return deal.UseCustomerTax; } set { deal.UseCustomerTax = value; } }
        public bool? Taxable { get { return deal.Taxable; } set { deal.Taxable = value; } }
        public string TaxStateOfIncorporationId { get { return deal.TaxStateOfIncorporationId; } set { deal.TaxStateOfIncorporationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxStateOfIncorporation { get; set; }
        public string TaxFederalNo { get { return deal.TaxFederalNo; } set { deal.TaxFederalNo = value; } }
        public int? NonTaxableYear { get { return deal.NonTaxableYear; } set { deal.NonTaxableYear = value; } }
        public string NonTaxableCertificateNo { get { return deal.NonTaxableCertificateNo; } set { deal.NonTaxableCertificateNo = value; } }
        public string NonTaxableCertificateValidThrough { get { return deal.NonTaxableCertificateValidThrough; } set { deal.NonTaxableCertificateValidThrough = value; } }
        public bool? NonTaxableCertificateOnFile { get { return deal.NonTaxableCertificateOnFile; } set { deal.NonTaxableCertificateOnFile = value; } }
        public bool? DisableQuoteOrderActivity { get { return deal.DisableQuoteOrderActivity; } set { deal.DisableQuoteOrderActivity = value; } }
        public bool? DisableRental { get { return deal.DisableRental; } set { deal.DisableRental = value; } }
        public bool? DisableSales { get { return deal.DisableSales; } set { deal.DisableSales = value; } }
        public bool? DisableFacilities { get { return deal.DisableFacilities; } set { deal.DisableFacilities = value; } }
        public bool? DisableTransportation { get { return deal.DisableTransportation; } set { deal.DisableTransportation = value; } }
        public bool? DisableLabor { get { return deal.DisableLabor; } set { deal.DisableLabor = value; } }
        public bool? DisableMisc { get { return deal.DisableMisc; } set { deal.DisableMisc = value; } }
        public bool? DisableRentalSale { get { return deal.DisableRentalSale; } set { deal.DisableRentalSale = value; } }
        public bool? DisableSubRental { get { return deal.DisableSubRental; } set { deal.DisableSubRental = value; } }
        public bool? DisableSubSale { get { return deal.DisableSubSale; } set { deal.DisableSubSale = value; } }
        public bool? DisableSubLabor { get { return deal.DisableSubLabor; } set { deal.DisableSubLabor = value; } }
        public bool? DisableSubMisc { get { return deal.DisableSubMisc; } set { deal.DisableSubMisc = value; } }
        public string DefaultOutgoingDeliveryType { get { return deal.DefaultOutgoingDeliveryType; } set { deal.DefaultOutgoingDeliveryType = value; } }
        public string DefaultIncomingDeliveryType { get { return deal.DefaultIncomingDeliveryType; } set { deal.DefaultIncomingDeliveryType = value; } }
        public string ShippingAddressType { get { return deal.ShippingAddressType; } set { deal.ShippingAddressType = value; } }
        public string ShipAttention { get { return deal.ShipAttention; } set { deal.ShipAttention = value; } }
        public string ShipAddress1 { get { return deal.ShipAddress1; } set { deal.ShipAddress1 = value; } }
        public string ShipAddress2 { get { return deal.ShipAddress2; } set { deal.ShipAddress2 = value; } }
        public string ShipCity { get { return deal.ShipCity; } set { deal.ShipCity = value; } }
        public string ShipState { get { return deal.ShipState; } set { deal.ShipState = value; } }
        public string ShipCountryId { get { return deal.ShipCountryId; } set { deal.ShipCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ShipCountry { get; set; }
        public string ShipZipCode { get { return deal.ShipZipCode; } set { deal.ShipZipCode = value; } }
        public bool? RebateRental { get { return deal.RebateRental; } set { deal.RebateRental = value; } }
        public string RebateCustomerId { get { return deal.RebateCustomerId; } set { deal.RebateCustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RebateCustomer { get; set; }
        public int? OwnedEquipmentRebateRentalPerecent { get { return deal.OwnedEquipmentRebateRentalPerecent; } set { deal.OwnedEquipmentRebateRentalPerecent = value; } }
        public int? SubRentalEquipmentRebateRentalPerecent { get { return deal.SubRentalEquipmentRebateRentalPerecent; } set { deal.SubRentalEquipmentRebateRentalPerecent = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSaveDeal(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(DealNumber))
                {
                    bool x = deal.SetNumber().Result;
                }
            }
            else  // updating
            {
            }
        }
        //------------------------------------------------------------------------------------


    }
}
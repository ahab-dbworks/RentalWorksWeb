using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Customer
{
    public class CustomerLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CustomerRecord customer = new CustomerRecord();
        CustomerLoader customerLoader = new CustomerLoader();
        CustomerBrowseLoader customerBrowseLoader = new CustomerBrowseLoader();
        public CustomerLogic()
        {
            dataRecords.Add(customer);
            dataLoader = customerLoader;
            browseLoader = customerBrowseLoader;

            //BeforeSave += BeforeSaveCustomer;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CustomerId { get { return customer.CustomerId; } set { customer.CustomerId = value; } }
        public string CustomerNumber { get { return customer.CustomerNumber; } set { customer.CustomerNumber = value; } }
        public string OfficeLocationId { get { return customer.OfficeLocationId; } set { customer.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string DepartmentId { get { return customer.DepartmentId; } set { customer.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string CustomerTypeId { get { return customer.CustomerTypeId; } set { customer.CustomerTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerType { get; set; }
        public string CustomerCategoryId { get { return customer.CustomerCategoryId; } set { customer.CustomerCategoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerCategory { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Customer { get { return customer.Customer; } set { customer.Customer = value; } }
        public string Address1 { get { return customer.Address1; } set { customer.Address1 = value; } }
        public string Address2 { get { return customer.Address2; } set { customer.Address2 = value; } }
        public string City { get { return customer.City; } set { customer.City = value; } }
        public string State { get { return customer.State; } set { customer.State = value; } }
        public string CountryId { get { return customer.CountryId; } set { customer.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return customer.ZipCode; } set { customer.ZipCode = value; } }
        public string ParentCustomerId { get { return customer.ParentCustomerId; } set { customer.ParentCustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentCustomer { get; set; }
        public string Phone { get { return customer.Phone; } set { customer.Phone = value; } }
        public string Fax { get { return customer.Fax; } set { customer.Fax = value; } }
        public string Phone800 { get { return customer.Phone800; } set { customer.Phone800 = value; } }
        public string OtherPhone { get { return customer.OtherPhone; } set { customer.OtherPhone = value; } }
        public string WebAddress { get { return customer.WebAddress; } set { customer.WebAddress = value; } }
        public string CustomerStatusId { get { return customer.CustomerStatusId; } set { customer.CustomerStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerStatus { get; set; }
        public string StatusAsOf { get { return customer.StatusAsOf; } set { customer.StatusAsOf = value; } }
        public bool? TermsAndConditionsOnFile { get { return customer.TermsAndConditionsOnFile; } set { customer.TermsAndConditionsOnFile = value; } }
        public string BillingAddressType { get { return customer.BillingAddressType; } set { customer.BillingAddressType = value; } }
        public string BillToAttention1 { get { return customer.BillToAttention1; } set { customer.BillToAttention1 = value; } }
        public string BillToAttention2 { get { return customer.BillToAttention2; } set { customer.BillToAttention2 = value; } }
        public string BillToAddress1 { get { return customer.BillToAddress1; } set { customer.BillToAddress1 = value; } }
        public string BillToAddress2 { get { return customer.BillToAddress2; } set { customer.BillToAddress2 = value; } }
        public string BillToCity { get { return customer.BillToCity; } set { customer.BillToCity = value; } }
        public string BillToState { get { return customer.BillToState; } set { customer.BillToState = value; } }
        public string BillToCountryId { get { return customer.BillToCountryId; } set { customer.BillToCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToCountry { get; set; }
        public string BillToZipCode { get { return customer.BillToZipCode; } set { customer.BillToZipCode = value; } }
        public string PaymentTermsId { get { return customer.PaymentTermsId; } set { customer.PaymentTermsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public bool? VehicleRentalAgreementComplete { get { return customer.VehicleRentalAgreementComplete; } set { customer.VehicleRentalAgreementComplete = value; } }
        public bool? UseDiscountTemplate { get { return customer.UseDiscountTemplate; } set { customer.UseDiscountTemplate = value; } }
        public string DiscountTemplateId { get { return customer.DiscountTemplateId; } set { customer.DiscountTemplateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DiscountTemplate { get; set; }
        public string CreditStatusId { get { return customer.CreditStatusId; } set { customer.CreditStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CreditStatus { get; set; }
        public string CreditStatusThroughDate { get { return customer.CreditStatusThroughDate; } set { customer.CreditStatusThroughDate = value; } }
        public bool? CreditApplicationOnFile { get { return customer.CreditApplicationOnFile; } set { customer.CreditApplicationOnFile = value; } }
        public bool? CreditUnlimited { get { return customer.CreditUnlimited; } set { customer.CreditUnlimited = value; } }
        public int? CreditLimit { get { return customer.CreditLimit; } set { customer.CreditLimit = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CreditBalance { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? CreditAvailable { get; set; }
        public bool? CreditResponsiblePartyOnFile { get { return customer.CreditResponsiblePartyOnFile; } set { customer.CreditResponsiblePartyOnFile = value; } }
        public string CreditResponsibleParty { get { return customer.CreditResponsibleParty; } set { customer.CreditResponsibleParty = value; } }
        public bool? TradeReferencesVerified { get { return customer.TradeReferencesVerified; } set { customer.TradeReferencesVerified = value; } }
        public string TradeReferencesVerifiedBy { get { return customer.TradeReferencesVerifiedBy; } set { customer.TradeReferencesVerifiedBy = value; } }
        public string TradeReferencesVerifiedOn { get { return customer.TradeReferencesVerifiedOn; } set { customer.TradeReferencesVerifiedOn = value; } }
        public string CreditCardTypeId { get { return customer.CreditCardTypeId; } set { customer.CreditCardTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CreditCardType { get; set; }
        public int? CreditCardLimit { get { return customer.CreditCardLimit; } set { customer.CreditCardLimit = value; } }
        public string CreditCardNo { get { return customer.CreditCardNo; } set { customer.CreditCardNo = value; } }
        public string CreditCardCode { get { return customer.CreditCardCode; } set { customer.CreditCardCode = value; } }
        public int? CreditCardExpirationMonth { get { return customer.CreditCardExpirationMonth; } set { customer.CreditCardExpirationMonth = value; } }
        public int? CreditCardExpirationYear { get { return customer.CreditCardExpirationYear; } set { customer.CreditCardExpirationYear = value; } }
        public string CreditCardName { get { return customer.CreditCardName; } set { customer.CreditCardName = value; } }
        public bool? CreditCardAuthorizationOnFile { get { return customer.CreditCardAuthorizationOnFile; } set { customer.CreditCardAuthorizationOnFile = value; } }
        public bool? InsuranceCertificationOnFile { get { return customer.InsuranceCertificationOnFile; } set { customer.InsuranceCertificationOnFile = value; } }
        public string InsuranceCertificationValidThrough { get { return customer.InsuranceCertificationValidThrough; } set { customer.InsuranceCertificationValidThrough = value; } }
        public int? InsuranceCoverageLiability { get { return customer.InsuranceCoverageLiability; } set { customer.InsuranceCoverageLiability = value; } }
        public int? InsuranceCoverageLiabilityDeductible { get { return customer.InsuranceCoverageLiabilityDeductible; } set { customer.InsuranceCoverageLiabilityDeductible = value; } }
        public int? InsuranceCoveragePropertyValue { get { return customer.InsuranceCoveragePropertyValue; } set { customer.InsuranceCoveragePropertyValue = value; } }
        public int? InsuranceCoveragePropertyValueDeductible { get { return customer.InsuranceCoveragePropertyValueDeductible; } set { customer.InsuranceCoveragePropertyValueDeductible = value; } }
        public string InsuranceCompanyId { get { return customer.InsuranceCompanyId; } set { customer.InsuranceCompanyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InsuranceCompany { get; set; }
        public string InsuranceAgent { get { return customer.InsuranceAgent; } set { customer.InsuranceAgent = value; } }
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
        public bool? VehicleInsuranceCertficationOnFile { get { return customer.VehicleInsuranceCertficationOnFile; } set { customer.VehicleInsuranceCertficationOnFile = value; } }
        public bool? Taxable { get { return customer.Taxable; } set { customer.Taxable = value; } }
        public string TaxStateOfIncorporationId { get { return customer.TaxStateOfIncorporationId; } set { customer.TaxStateOfIncorporationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxStateOfIncorporation { get; set; }
        public string TaxFederalNo { get { return customer.TaxFederalNo; } set { customer.TaxFederalNo = value; } }
        public int? NonTaxableYear { get { return customer.NonTaxableYear; } set { customer.NonTaxableYear = value; } }
        public string NonTaxableCertificateNo { get { return customer.NonTaxableCertificateNo; } set { customer.NonTaxableCertificateNo = value; } }
        public string NonTaxableCertificateValidThrough { get { return customer.NonTaxableCertificateValidThrough; } set { customer.NonTaxableCertificateValidThrough = value; } }
        public bool? NonTaxableCertificateOnFile { get { return customer.NonTaxableCertificateOnFile; } set { customer.NonTaxableCertificateOnFile = value; } }
        public bool? DisableQuoteOrderActivity { get { return customer.DisableQuoteOrderActivity; } set { customer.DisableQuoteOrderActivity = value; } }
        public bool? DisableRental { get { return customer.DisableRental; } set { customer.DisableRental = value; } }
        public bool? DisableSales { get { return customer.DisableSales; } set { customer.DisableSales = value; } }
        public bool? DisableFacilities { get { return customer.DisableFacilities; } set { customer.DisableFacilities = value; } }
        public bool? DisableTransportation { get { return customer.DisableTransportation; } set { customer.DisableTransportation = value; } }
        public bool? DisableLabor { get { return customer.DisableLabor; } set { customer.DisableLabor = value; } }
        public bool? DisableMisc { get { return customer.DisableMisc; } set { customer.DisableMisc = value; } }
        public bool? DisableRentalSale { get { return customer.DisableRentalSale; } set { customer.DisableRentalSale = value; } }
        public bool? DisableSubRental { get { return customer.DisableSubRental; } set { customer.DisableSubRental = value; } }
        public bool? DisableSubSale { get { return customer.DisableSubSale; } set { customer.DisableSubSale = value; } }
        public bool? DisableSubLabor { get { return customer.DisableSubLabor; } set { customer.DisableSubLabor = value; } }
        public bool? DisableSubMisc { get { return customer.DisableSubMisc; } set { customer.DisableSubMisc = value; } }
        public bool? SplitRental { get { return customer.SplitRental; } set { customer.SplitRental = value; } }
        public bool? SplitRentalTaxCustomer { get { return customer.SplitRentalTaxCustomer; } set { customer.SplitRentalTaxCustomer = value; } }
        public int? OwnedEquipmentSplitRentalPerecent { get { return customer.OwnedEquipmentSplitRentalPerecent; } set { customer.OwnedEquipmentSplitRentalPerecent = value; } }
        public int? SubRentalEquipmentSplitRentalPerecent { get { return customer.SubRentalEquipmentSplitRentalPerecent; } set { customer.SubRentalEquipmentSplitRentalPerecent = value; } }
        public bool? RebateRental { get { return customer.RebateRental; } set { customer.RebateRental = value; } }
        public int? OwnedEquipmentRebateRentalPerecent { get { return customer.OwnedEquipmentRebateRentalPerecent; } set { customer.OwnedEquipmentRebateRentalPerecent = value; } }
        public int? SubRentalEquipmentRebateRentalPerecent { get { return customer.SubRentalEquipmentRebateRentalPerecent; } set { customer.SubRentalEquipmentRebateRentalPerecent = value; } }
        public string SplitRentalLogoFileName { get { return customer.SplitRentalLogoFileName; } set { customer.SplitRentalLogoFileName = value; } }
        public int? SplitRentalLogoWidth { get { return customer.SplitRentalLogoWidth; } set { customer.SplitRentalLogoWidth = value; } }
        public int? SplitRentalLogoHeight { get { return customer.SplitRentalLogoHeight; } set { customer.SplitRentalLogoHeight = value; } }
        public string ShippingAddressType { get { return customer.ShippingAddressType; } set { customer.ShippingAddressType = value; } }
        public string ShipAttention { get { return customer.ShipAttention; } set { customer.ShipAttention = value; } }
        public string ShipAddress1 { get { return customer.ShipAddress1; } set { customer.ShipAddress1 = value; } }
        public string ShipAddress2 { get { return customer.ShipAddress2; } set { customer.ShipAddress2 = value; } }
        public string ShipCity { get { return customer.ShipCity; } set { customer.ShipCity = value; } }
        public string ShipState { get { return customer.ShipState; } set { customer.ShipState = value; } }
        public string ShipCountryId { get { return customer.ShipCountryId; } set { customer.ShipCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ShipCountry { get; set; }
        public string ShipZipCode { get { return customer.ShipZipCode; } set { customer.ShipZipCode = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Inactive { get; set; }
        public string DateStamp { get { return customer.DateStamp; } set { customer.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            if (BillingAddressType != null)
            {
                if (!(BillingAddressType.Equals(RwConstants.BILLING_ADDRESS_TYPE_CUSTOMER) || BillingAddressType.Equals(RwConstants.BILLING_ADDRESS_TYPE_OTHER)))
                {
                    isValid = false;
                    validateMsg = "Invalid Billing Address Type: " + BillingAddressType + ".  Acceptable values are " + RwConstants.BILLING_ADDRESS_TYPE_CUSTOMER + " or " + RwConstants.BILLING_ADDRESS_TYPE_OTHER;
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        //justin WIP
        //public void BeforeSaveCustomer(object sender, BeforeSaveEventArgs e)
        //{
        //    if ((BillingAddressType != null) && (BillingAddressType.Equals(RwConstants.BILLING_ADDRESS_TYPE_CUSTOMER)))
        //    {
        //        BillToAddress1 = Address1;
        //        BillToAddress2 = Address2;
        //        BillToCity = City;
        //        BillToState = State;
        //        BillToCountry = Country;
        //        BillToCountryId = CountryId;
        //        BillToZipCode = ZipCode;
        //    }
        //}
        //------------------------------------------------------------------------------------ 
    }
}

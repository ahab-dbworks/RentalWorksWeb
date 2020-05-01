using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Deal
{
    [FwSqlTable("dealwebview")]
    public class DealBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string DefaultRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
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
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text)]
        public string PhoneOther { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csrid", modeltype: FwDataTypes.Text)]
        public string CsrId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csr", modeltype: FwDataTypes.Text)]
        public string Csr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultprojectmanagerid", modeltype: FwDataTypes.Text)]
        public string DefaultProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string DefaultProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultagentid", modeltype: FwDataTypes.Text)]
        public string DefaultAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string DefaultAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealclassificationid", modeltype: FwDataTypes.Text)]
        public string DealClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealclassification", modeltype: FwDataTypes.Text)]
        public string DealClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prodtypeid", modeltype: FwDataTypes.Text)]
        public string ProductionTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prodtype", modeltype: FwDataTypes.Text)]
        public string ProductionType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
        public string DealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        //public string DealStatus { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.Date)]
        public string StatusAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expwrapdate", modeltype: FwDataTypes.Date)]
        public string ExpectedWrapDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usecustomerdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? UseCustomerDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usediscounttemplate", modeltype: FwDataTypes.Boolean)]
        public bool? UseDiscountTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text)]
        public string DiscountTemplateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplate", modeltype: FwDataTypes.Text)]
        public string DiscountTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplaterentaldw", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplaterentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplatesalesdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplatespacedw", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplatespacediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionrate", modeltype: FwDataTypes.Decimal)]
        public decimal? CommissionRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionincludesvendoritems", modeltype: FwDataTypes.Boolean)]
        public bool? CommissionIncludesVendorItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreq", modeltype: FwDataTypes.Boolean)]
        public bool? PoRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "potype", modeltype: FwDataTypes.Text)]
        public string PoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd", modeltype: FwDataTypes.Text)]
        public string BillToAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoatt", modeltype: FwDataTypes.Text)]
        public string BillToAttention1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoatt2", modeltype: FwDataTypes.Text)]
        public string BillToAttention2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text)]
        public string BillToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text)]
        public string BillToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "financechargeflg", modeltype: FwDataTypes.Boolean)]
        public bool? AssessFinanceCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowbillschedoverride", modeltype: FwDataTypes.Boolean)]
        public bool? AllowBillingScheduleOverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowrebatecredits", modeltype: FwDataTypes.Boolean)]
        public bool? AllowRebateCreditInvoices { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custcredit", modeltype: FwDataTypes.Boolean)]
        public bool? UseCustomerCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcreditstatusid", modeltype: FwDataTypes.Text)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcreditstatus", modeltype: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crappthru", modeltype: FwDataTypes.Date)]
        public string CreditStatusThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crapponfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditApplicationOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unlimitedcredit", modeltype: FwDataTypes.Boolean)]
        public bool? UnlimitedCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crlimitdeal", modeltype: FwDataTypes.Integer)]
        public int? CreditLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcreditbalance", modeltype: FwDataTypes.Integer)]
        public int? CreditBalance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcreditavailable", modeltype: FwDataTypes.Integer)]
        public int? CreditAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custcreditlimit", modeltype: FwDataTypes.Integer)]
        public int? CustomerCreditLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custcreditbalance", modeltype: FwDataTypes.Integer)]
        public int? CustomerCreditBalance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custcreditavailable", modeltype: FwDataTypes.Integer)]
        public int? CustomerCreditAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crresponfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditResponsiblePartyOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsibilityparty", modeltype: FwDataTypes.Text)]
        public string CreditResponsibleParty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crver", modeltype: FwDataTypes.Boolean)]
        public bool? TradeReferencesVerified { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crby", modeltype: FwDataTypes.Text)]
        public string TradeReferencesVerifiedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cron", modeltype: FwDataTypes.Date)]
        public string TradeReferencesVerifiedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardtypeid", modeltype: FwDataTypes.Text)]
        public string CreditCardTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardtype", modeltype: FwDataTypes.Text)]
        public string CreditCardType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crclimit", modeltype: FwDataTypes.Integer)]
        public int? CreditCardLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcno", modeltype: FwDataTypes.Text)]
        public string CreditCardNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardcode", modeltype: FwDataTypes.Text)]
        public string CreditCardCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcname", modeltype: FwDataTypes.Text)]
        public string CreditCardName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcexpmonth", modeltype: FwDataTypes.Integer)]
        public int? CreditCardExpirationMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcexpyear", modeltype: FwDataTypes.Integer)]
        public int? CreditCardExpirationYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcauthonfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditCardAuthorizationFormOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thresholdamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DepletingDepositThresholdAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thresholdpercent", modeltype: FwDataTypes.Integer)]
        public int? DepletingDepositThresholdPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depletingdeposittotal", modeltype: FwDataTypes.Decimal)]
        public decimal? DepletingDepositTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depletingdepositapplied", modeltype: FwDataTypes.Decimal)]
        public decimal? DepletingDepositApplied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depletingdepositremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? DepletingDepositRemaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custinsurance", modeltype: FwDataTypes.Boolean)]
        public bool? UseCustomerInsurance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscertification", modeltype: FwDataTypes.Boolean)]
        public bool? InsuranceCertification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insvalidthru", modeltype: FwDataTypes.Date)]
        public string InsuranceCertificationValidThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovliab", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoverageLiability { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovliabded", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoverageLiabilityDeductible { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovprop", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoverageProperty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovpropded", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoveragePropertyDeductible { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insvendor", modeltype: FwDataTypes.Text)]
        public string InsuranceCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscompagent", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscompadd1", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompadd2", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompcity", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompstate", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompzip", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompcountryid", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompcountry", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompphone", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompfax", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyFax { get; set; }
        //------------------------------------------------------------------------------------



        [FwSqlDataField(column: "vehicleinsurancecertification", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleInsuranceCertification { get; set; }
        //------------------------------------------------------------------------------------         
        [FwSqlDataField(column: "usecustomertax", modeltype: FwDataTypes.Boolean)]
        public bool? UseCustomerTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stateofincid", modeltype: FwDataTypes.Text)]
        public string TaxStateOfIncorporationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stateofinc", modeltype: FwDataTypes.Text)]
        public string TaxStateOfIncorporation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxfedno", modeltype: FwDataTypes.Text)]
        public string TaxFederalNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxyear", modeltype: FwDataTypes.Integer)]
        public int? NonTaxableYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxcertificateno", modeltype: FwDataTypes.Text)]
        public string NonTaxableCertificateNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxvalidthrough", modeltype: FwDataTypes.Date)]
        public string NonTaxableCertificateValidThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxcertificateonfile", modeltype: FwDataTypes.Boolean)]
        public bool? NonTaxableCertificateOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableactivityoverride", modeltype: FwDataTypes.Boolean)]
        public bool? DisableQuoteOrderActivity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerental", modeltype: FwDataTypes.Boolean)]
        public bool? DisableRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesales", modeltype: FwDataTypes.Boolean)]
        public bool? DisableSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablefacilities", modeltype: FwDataTypes.Boolean)]
        public bool? DisableFacilities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disabletransportation", modeltype: FwDataTypes.Boolean)]
        public bool? DisableTransportation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablelabor", modeltype: FwDataTypes.Boolean)]
        public bool? DisableLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablemisc", modeltype: FwDataTypes.Boolean)]
        public bool? DisableMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? DisableRentalSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubrental", modeltype: FwDataTypes.Boolean)]
        public bool? DisableSubRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubsales", modeltype: FwDataTypes.Boolean)]
        public bool? DisableSubSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesublabor", modeltype: FwDataTypes.Boolean)]
        public bool? DisableSubLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubmisc", modeltype: FwDataTypes.Boolean)]
        public bool? DisableSubMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text)]
        public string DefaultOutgoingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text)]
        public string DefaultIncomingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd", modeltype: FwDataTypes.Text)]
        public string ShippingAddressType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipatt", modeltype: FwDataTypes.Text)]
        public string ShipAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd1", modeltype: FwDataTypes.Text)]
        public string ShipAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd2", modeltype: FwDataTypes.Text)]
        public string ShipAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipcity", modeltype: FwDataTypes.Text)]
        public string ShipCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipstate", modeltype: FwDataTypes.Text)]
        public string ShipState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipcountryid", modeltype: FwDataTypes.Text)]
        public string ShipCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipcountry", modeltype: FwDataTypes.Text)]
        public string ShipCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipzip", modeltype: FwDataTypes.Text)]
        public string ShipZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean)]
        public bool? RebateRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text)]
        public string RebateCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebatecustomer", modeltype: FwDataTypes.Text)]
        public string RebateCustomer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer)]
        public int? OwnedEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer)]
        public int? SubRentalEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "securitydepositamt", modeltype: FwDataTypes.Decimal)]
        public decimal? SecurityDepositAmount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "enablewebquoterequest", modeltype: FwDataTypes.Boolean)]
        public bool? EnableWebQuoteRequest { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("CustomerId", "customerid", select, request);
            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("DealTypeId", "dealtypeid", select, request);
            addFilterToSelect("CsrId", "csrid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
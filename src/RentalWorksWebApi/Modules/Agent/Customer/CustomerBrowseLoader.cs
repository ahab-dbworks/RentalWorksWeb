using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Customer
{
    [FwSqlTable("customerwebview")]
    public class CustomerBrowseLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
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
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcatid", modeltype: FwDataTypes.Text)]
        public string CustomerCategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcat", modeltype: FwDataTypes.Text)]
        public string CustomerCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "parentcustomerid", modeltype: FwDataTypes.Text)]
        public string ParentCustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "parentcustomer", modeltype: FwDataTypes.Text)]
        public string ParentCustomer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "faxno", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text)]
        public string OtherPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text)]
        public string CustomerStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.Date)]
        public string StatusAsOf { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsandconditiononfile", modeltype: FwDataTypes.Boolean)]
        public bool? TermsAndConditionsOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoadd", modeltype: FwDataTypes.Text)]
        public string BillingAddressType { get; set; }
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
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclerentalagreementcomplete", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleRentalAgreementComplete { get; set; }
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
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditthroughdate", modeltype: FwDataTypes.Date)]
        public string CreditStatusThroughDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditapponfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditApplicationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditunlimited", modeltype: FwDataTypes.Boolean)]
        public bool? CreditUnlimited { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditlimit", modeltype: FwDataTypes.Integer)]
        public int? CreditLimit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditbalance", modeltype: FwDataTypes.Integer)]
        public int? CreditBalance { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditavailable", modeltype: FwDataTypes.Integer)]
        public int? CreditAvailable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditresponfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditResponsiblePartyOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "responsibilityparty", modeltype: FwDataTypes.Text)]
        public string CreditResponsibleParty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditverifiedflag", modeltype: FwDataTypes.Boolean)]
        public bool? TradeReferencesVerified { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditverby", modeltype: FwDataTypes.Text)]
        public string TradeReferencesVerifiedBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditveron", modeltype: FwDataTypes.Date)]
        public string TradeReferencesVerifiedOn { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string CreditCardTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string CreditCardType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcardlimit", modeltype: FwDataTypes.Integer)]
        public int? CreditCardLimit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcardno", modeltype: FwDataTypes.Text)]
        public string CreditCardNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcardcode", modeltype: FwDataTypes.Text)]
        public string CreditCardCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "crcexpmonth", modeltype: FwDataTypes.Integer)]
        public int? CreditCardExpirationMonth { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "crcexpyear", modeltype: FwDataTypes.Integer)]
        public int? CreditCardExpirationYear { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcname", modeltype: FwDataTypes.Text)]
        public string CreditCardName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcautonfile", modeltype: FwDataTypes.Boolean)]
        public bool? CreditCardAuthorizationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "certofins", modeltype: FwDataTypes.Boolean)]
        public bool? InsuranceCertificationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "insvalidthru", modeltype: FwDataTypes.Date)]
        public string InsuranceCertificationValidThrough { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovliability", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoverageLiability { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovliabdeduct", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoverageLiabilityDeductible { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovpropvalue", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoveragePropertyValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovpropvaldeduct", modeltype: FwDataTypes.Integer)]
        public int? InsuranceCoveragePropertyValueDeductible { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "insvendorid", modeltype: FwDataTypes.Text)]
        public string InsuranceCompanyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "insvendor", modeltype: FwDataTypes.Text)]
        public string InsuranceCompany { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscompagent", modeltype: FwDataTypes.Text)]
        public string InsuranceAgent { get; set; }
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
        public bool? VehicleInsuranceCertficationOnFile { get; set; }
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
        [FwSqlDataField(column: "splitrentalflg", modeltype: FwDataTypes.Boolean)]
        public bool? SplitRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "splitrentaltaxflg", modeltype: FwDataTypes.Boolean)]
        public bool? SplitRentalTaxCustoemr { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ownedsplitrate", modeltype: FwDataTypes.Integer)]
        public int? OwnedEquipmentSplitRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorsplitrate", modeltype: FwDataTypes.Integer)]
        public int? SubRentalEquipmentSplitRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean)]
        public bool? RebateRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer)]
        public int? OwnedEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer)]
        public int? SubRentalEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "splitrentallogofilename", modeltype: FwDataTypes.Text)]
        public string SplitRentalLogoFileName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "splitrentallogowidth", modeltype: FwDataTypes.Integer)]
        public int? SplitRentalLogoWidth { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "splitrentallogoheight", modeltype: FwDataTypes.Integer)]
        public int? SplitRentalLogoHeight { get; set; }
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
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Customer
{
    [FwSqlTable("customerview")]
    public class CustomerLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
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
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcatid", modeltype: FwDataTypes.Text)]
        public string CustomerCategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcat", modeltype: FwDataTypes.Text)]
        public string CustomerCategory { get; set; }
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
        public string Phone800 { get; set; }
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
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.Date)]
        public string StatusAsOf { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsandconditiononfile", modeltype: FwDataTypes.Boolean)]
        public bool TermsAndConditionsOnFile { get; set; }
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
        public bool VehicleRentalAgreementComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usediscounttemplate", modeltype: FwDataTypes.Boolean)]
        public bool UseDiscountTemplate { get; set; }
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
        public bool CreditApplicationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditunlimited", modeltype: FwDataTypes.Boolean)]
        public bool CreditUnlimited { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditlimit", modeltype: FwDataTypes.Integer)]
        public int CreditLimit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditresponfile", modeltype: FwDataTypes.Boolean)]
        public bool CreditResponsiblePartyOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "responsibilityparty", modeltype: FwDataTypes.Text)]
        public string CreditResponsibleParty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditverifiedflag", modeltype: FwDataTypes.Boolean)]
        public bool TradeReferencesVerified { get; set; }
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
        public int CreditCardLimit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcardno", modeltype: FwDataTypes.Text)]
        public string CreditCardNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "crcexpmonth", modeltype: FwDataTypes.Integer)]
        public int CreditCardExpirationMonth { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "crcexpyear", modeltype: FwDataTypes.Integer)]
        public int CreditCardExpirationYear { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcname", modeltype: FwDataTypes.Text)]
        public string CreditCardName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditcautonfile", modeltype: FwDataTypes.Boolean)]
        public bool CreditCardAuthorizationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "certofins", modeltype: FwDataTypes.Boolean)]
        public bool InsuranceCertificationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "insvalidthru", modeltype: FwDataTypes.Date)]
        public string InsuranceCertificationValidThrough { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovliability", modeltype: FwDataTypes.Integer)]
        public int InsuranceCoverageLiability { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovliabdeduct", modeltype: FwDataTypes.Integer)]
        public int InsuranceCoverageLiabilityDeductible { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovpropvalue", modeltype: FwDataTypes.Integer)]
        public int InsuranceCoveragePropertyValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inscovpropvaldeduct", modeltype: FwDataTypes.Integer)]
        public int InsuranceCoveragePropertyValueDeductible { get; set; }
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
        [FwSqlDataField(column: "vehicleinsurancecertification", modeltype: FwDataTypes.Boolean)]
        public bool VehicleInsuranceCertficationOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool Taxable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stateofinc", modeltype: FwDataTypes.Text)]
        public string TaxStateOfIncorporation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxfedno", modeltype: FwDataTypes.Text)]
        public string TaxFederalNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nontaxyear", modeltype: FwDataTypes.Integer)]
        public int NonTaxableYear { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nontaxcertificateno", modeltype: FwDataTypes.Text)]
        public string NonTaxableCertificateNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nontaxvalidthrough", modeltype: FwDataTypes.Date)]
        public string NonTaxableCertificateValidThrough { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nontaxcertificateonfile", modeltype: FwDataTypes.Boolean)]
        public bool NonTaxableCertificateOnFile { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "enableactivityoverride", modeltype: FwDataTypes.Boolean)]
        public bool DisableQuoteOrderActivity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerental", modeltype: FwDataTypes.Boolean)]
        public bool DisableRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesales", modeltype: FwDataTypes.Boolean)]
        public bool DisableSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablefacilities", modeltype: FwDataTypes.Boolean)]
        public bool DisableFacilities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disabletransportation", modeltype: FwDataTypes.Boolean)]
        public bool DisableTransportation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablelabor", modeltype: FwDataTypes.Boolean)]
        public bool DisableLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablemisc", modeltype: FwDataTypes.Boolean)]
        public bool DisableMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerentalsale", modeltype: FwDataTypes.Boolean)]
        public bool DisableRentalSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubrental", modeltype: FwDataTypes.Boolean)]
        public bool DisableSubRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubsales", modeltype: FwDataTypes.Boolean)]
        public bool DisableSubSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesublabor", modeltype: FwDataTypes.Boolean)]
        public bool DisableSubLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubmisc", modeltype: FwDataTypes.Boolean)]
        public bool DisableSubMisc { get; set; }
        //------------------------------------------------------------------------------------







        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

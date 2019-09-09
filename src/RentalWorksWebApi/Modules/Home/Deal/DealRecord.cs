using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Deal
{
    [FwSqlTable("deal")]
    public class DealRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DealId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string Deal { get; set; }
            //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PhoneOther { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "csrid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CsrId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultagentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultprojectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealclassificationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prodtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ProductionTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusasof", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string StatusAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expwrapdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ExpectedWrapDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usecustomerdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseCustomerDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usediscounttemplate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseDiscountTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DiscountTemplateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? CommissionRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionincludesvendoritems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CommissionIncludesVendorItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreq", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PoRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "potype", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string PoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string BillToAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoatt", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string BillToAttention1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoatt2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string BillToAttention2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string BillToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BillToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "financechargeflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AssessFinanceCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowbillschedoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowBillingScheduleOverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowrebatecredits", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowRebateCreditInvoices { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseCustomerCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crappthru", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string CreditStatusThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crapponfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CreditApplicationOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unlimitedcredit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UnlimitedCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crlimitdeal", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? CreditLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crresponfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CreditResponsiblePartyOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsibilityparty", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 35)]
        public string CreditResponsibleParty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crver", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? TradeReferencesVerified { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string TradeReferencesVerifiedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cron", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string TradeReferencesVerifiedOn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardtypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CreditCardTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crclimit", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? CreditCardLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string CreditCardNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardcode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string CreditCardCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string CreditCardName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcexpmonth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? CreditCardExpirationMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcexpyear", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? CreditCardExpirationYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcauthonfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CreditCardAuthorizationFormOnFile{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thresholdamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? DepletingDepositThresholdAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thresholdpercent", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? DepletingDepositThresholdPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custinsurance", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseCustomerInsurance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscertification", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? InsuranceCertification { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inscert", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Inscert { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insvalidthru", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InsuranceCertificationValidThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovliab", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? InsuranceCoverageLiability { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovliabded", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? InsuranceCoverageLiabilityDeductible { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovprop", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? InsuranceCoverageProperty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscovpropded", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? InsuranceCoveragePropertyDeductible { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string InsuranceCompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inscompagent", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string InsuranceCompanyAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicleinsurancecertification", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? VehicleInsuranceCertification { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "usecustomertax", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseCustomerTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stateofincid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxStateOfIncorporationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxfedno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string TaxFederalNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxyear", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? NonTaxableYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxcertificateno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string NonTaxableCertificateNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxvalidthrough", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string NonTaxableCertificateValidThrough { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxcertificateonfile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? NonTaxableCertificateOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableactivityoverride", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableQuoteOrderActivity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablefacilities", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableFacilities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disabletransportation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableTransportation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablelabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablemisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablerentalsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableRentalSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubrental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableSubRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubsales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableSubSale { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesublabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableSubLabor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablesubmisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DisableSubMisc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text, maxlength: 8, sqltype: "char")]
        public string DefaultOutgoingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text, maxlength: 8, sqltype: "char")]
        public string DefaultIncomingDeliveryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd", modeltype: FwDataTypes.Text, maxlength: 20, sqltype: "varchar")]
        public string ShippingAddressType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipatt", modeltype: FwDataTypes.Text, maxlength: 50, sqltype: "varchar")]
        public string ShipAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd1", modeltype: FwDataTypes.Text, maxlength: 50, sqltype: "varchar")]
        public string ShipAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipadd2", modeltype: FwDataTypes.Text, maxlength: 50, sqltype: "varchar")]
        public string ShipAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipcity", modeltype: FwDataTypes.Text, maxlength: 30, sqltype: "varchar")]
        public string ShipCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipstate", modeltype: FwDataTypes.Text, maxlength: 20, sqltype: "varchar")]
        public string ShipState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipcountryid", modeltype: FwDataTypes.Text, maxlength: 8, sqltype: "char")]
        public string ShipCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipzip", modeltype: FwDataTypes.Text, maxlength: 10, sqltype: "varchar")]
        public string ShipZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RebateRental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RebateCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? OwnedEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? SubRentalEquipmentRebateRentalPerecent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablewebquoterequest", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? EnableWebQuoteRequest { get; set; }
        //------------------------------------------------------------------------------------


        /*
                    [FwSqlDataField(column: "billdiscrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
                    public decimal? Billdiscrate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Inputdate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Moddate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Inputby { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "modby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
                    public string Modby { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "printlaborcomponent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Printlaborcomponent { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "laborcomponentpct", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int? Laborcomponentpct { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
                    public int? Color { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "enablewebreports", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Enablewebreports { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Textcolor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "spacebillweekends", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Spacebillweekends { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "balance", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
                    public decimal? Balance { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "budocs", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Budocs { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "invformatid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
                    public string InvformatId { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "onlot", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Onlot { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
                    public string Orbitsapchgdeal { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgdetail { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgmajor { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
                    public string Orbitsapchgset { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
                    public string Orbitsapchgsub { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "revdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
                    public string Revdate { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapaccountno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
                    public string Sapaccountno { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapcopytoorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Sapcopytoorder { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "sapcostobject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 24)]
                    public string Sapcostobject { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "saptype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
                    public bool? Saptype { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "maxrequestequipmentdays", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int? Maxrequestequipmentdays { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "maxrequestequipmenthours", modeltype: FwDataTypes.Integer, sqltype: "int")]
                    public int? Maxrequestequipmenthours { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "playpreview", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Playpreview { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "playopen", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Playopen { get; set; }
                    //------------------------------------------------------------------------------------ 
                    [FwSqlDataField(column: "lastorderdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
                    public string Lastorderdate { get; set; }
             */
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 

        public async Task<bool> SetNumber(FwSqlConnection conn)
        {
            DealNumber = await AppFunc.GetNextModuleCounterAsync(AppConfig, UserSession, RwConstants.MODULE_DEAL, OfficeLocationId, conn);
            return true;
        }
        //-------------------------------------------------------------------------------------------------------    

    }
}
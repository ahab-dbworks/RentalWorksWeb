using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi;

namespace WebApi.Modules.Agent.Customer
{
    [FwLogic(Id:"kgohEqlOFFv7")]
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

            customer.BeforeSave += OnBeforeSaveCustomer;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsPrimaryKey:true)]
        public string CustomerId { get { return customer.CustomerId; } set { customer.CustomerId = value; } }

        [FwLogicProperty(Id:"rheRmVThaqgd")]
        public string CustomerNumber { get { return customer.CustomerNumber; } set { customer.CustomerNumber = value; } }

        [FwLogicProperty(Id:"xwEVJOfzIAzg")]
        public string OfficeLocationId { get { return customer.OfficeLocationId; } set { customer.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"spKIvApl9d5O", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"e22oOxWssdEx")]
        public string DepartmentId { get { return customer.DepartmentId; } set { customer.DepartmentId = value; } }

        [FwLogicProperty(Id:"3OAZaTxlyoxy", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"8oEyTIUhrsZG")]
        public string CustomerTypeId { get { return customer.CustomerTypeId; } set { customer.CustomerTypeId = value; } }

        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsReadOnly:true)]
        public string CustomerType { get; set; }

        [FwLogicProperty(Id:"4g1ZVLaIxUrz")]
        public string CustomerCategoryId { get { return customer.CustomerCategoryId; } set { customer.CustomerCategoryId = value; } }

        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsReadOnly:true)]
        public string CustomerCategory { get; set; }

        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsRecordTitle:true)]
        public string Customer { get { return customer.Customer; } set { customer.Customer = value; } }

        [FwLogicProperty(Id:"TFWyRp9gAzWE")]
        public string Address1 { get { return customer.Address1; } set { customer.Address1 = value; } }

        [FwLogicProperty(Id:"qbj1xqe414ia")]
        public string Address2 { get { return customer.Address2; } set { customer.Address2 = value; } }

        [FwLogicProperty(Id:"h1dqUUIqg4fz")]
        public string City { get { return customer.City; } set { customer.City = value; } }

        [FwLogicProperty(Id:"aVfAQF9gspeK")]
        public string State { get { return customer.State; } set { customer.State = value; } }

        [FwLogicProperty(Id:"myXUH7dYimNE")]
        public string CountryId { get { return customer.CountryId; } set { customer.CountryId = value; } }

        [FwLogicProperty(Id:"7s2pA0V3FSil", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"M6tESXBHoso1")]
        public string ZipCode { get { return customer.ZipCode; } set { customer.ZipCode = value; } }

        [FwLogicProperty(Id:"AUwSHUws3CGL")]
        public string ParentCustomerId { get { return customer.ParentCustomerId; } set { customer.ParentCustomerId = value; } }

        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsReadOnly:true)]
        public string ParentCustomer { get; set; }

        [FwLogicProperty(Id:"mbYXbzk1dGnS")]
        public string Phone { get { return customer.Phone; } set { customer.Phone = value; } }

        [FwLogicProperty(Id:"FgY8PmUqfTiK")]
        public string Fax { get { return customer.Fax; } set { customer.Fax = value; } }

        [FwLogicProperty(Id:"JWeIRH34q31b")]
        public string PhoneTollFree { get { return customer.PhoneTollFree; } set { customer.PhoneTollFree = value; } }

        [FwLogicProperty(Id:"hfV2XOUyDJgW")]
        public string OtherPhone { get { return customer.OtherPhone; } set { customer.OtherPhone = value; } }

        [FwLogicProperty(Id:"t1dI7k1QPdVS")]
        public string WebAddress { get { return customer.WebAddress; } set { customer.WebAddress = value; } }

        [FwLogicProperty(Id:"Dpx89nVjaFX8")]
        public string CustomerStatusId { get { return customer.CustomerStatusId; } set { customer.CustomerStatusId = value; } }

        [FwLogicProperty(Id:"XQpZso1m6zlQ", IsReadOnly:true)]
        public string CustomerStatus { get; set; }

        [FwLogicProperty(Id:"g0L7OKtqHApq")]
        public string StatusAsOf { get { return customer.StatusAsOf; } set { customer.StatusAsOf = value; } }

        [FwLogicProperty(Id:"df7kF8e0kJLf")]
        public bool? TermsAndConditionsOnFile { get { return customer.TermsAndConditionsOnFile; } set { customer.TermsAndConditionsOnFile = value; } }

        [FwLogicProperty(Id:"oHPrJe1Liduw")]
        public string BillingAddressType { get { return customer.BillingAddressType; } set { customer.BillingAddressType = value; } }

        [FwLogicProperty(Id:"A2vxsXICyQg6")]
        public string BillToAttention1 { get { return customer.BillToAttention1; } set { customer.BillToAttention1 = value; } }

        [FwLogicProperty(Id:"Ix72rOrPZ9SM")]
        public string BillToAttention2 { get { return customer.BillToAttention2; } set { customer.BillToAttention2 = value; } }

        [FwLogicProperty(Id:"8zB6Ak1yBBrd")]
        public string BillToAddress1 { get { return customer.BillToAddress1; } set { customer.BillToAddress1 = value; } }

        [FwLogicProperty(Id:"JtF15ilmV0Qa")]
        public string BillToAddress2 { get { return customer.BillToAddress2; } set { customer.BillToAddress2 = value; } }

        [FwLogicProperty(Id:"rdzDqygDBwG2")]
        public string BillToCity { get { return customer.BillToCity; } set { customer.BillToCity = value; } }

        [FwLogicProperty(Id:"G3gpzMy1WiJR")]
        public string BillToState { get { return customer.BillToState; } set { customer.BillToState = value; } }

        [FwLogicProperty(Id:"In3XDEsMJoAg")]
        public string BillToCountryId { get { return customer.BillToCountryId; } set { customer.BillToCountryId = value; } }

        [FwLogicProperty(Id:"oVi2GrPvrUDF", IsReadOnly:true)]
        public string BillToCountry { get; set; }

        [FwLogicProperty(Id:"bzjKkWtt2q0g")]
        public string BillToZipCode { get { return customer.BillToZipCode; } set { customer.BillToZipCode = value; } }

        [FwLogicProperty(Id:"HiyOtZtRTWG6")]
        public string PaymentTermsId { get { return customer.PaymentTermsId; } set { customer.PaymentTermsId = value; } }

        [FwLogicProperty(Id:"AUCS5oz0Nab3", IsReadOnly:true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id:"y9RVzdqARx5Q")]
        public bool? VehicleRentalAgreementComplete { get { return customer.VehicleRentalAgreementComplete; } set { customer.VehicleRentalAgreementComplete = value; } }

        [FwLogicProperty(Id:"KMxCoVeT8FoU")]
        public bool? UseDiscountTemplate { get { return customer.UseDiscountTemplate; } set { customer.UseDiscountTemplate = value; } }

        [FwLogicProperty(Id:"0HSQksT8zw7C")]
        public string DiscountTemplateId { get { return customer.DiscountTemplateId; } set { customer.DiscountTemplateId = value; } }

        [FwLogicProperty(Id:"1iiJXmInfV0R", IsReadOnly:true)]
        public string DiscountTemplate { get; set; }

        [FwLogicProperty(Id:"k2j7TUs2JlM1")]
        public string CreditStatusId { get { return customer.CreditStatusId; } set { customer.CreditStatusId = value; } }

        [FwLogicProperty(Id:"BQU8ygOvMSHC", IsReadOnly:true)]
        public string CreditStatus { get; set; }

        [FwLogicProperty(Id:"OlqWWuKLYkU9")]
        public string CreditStatusThroughDate { get { return customer.CreditStatusThroughDate; } set { customer.CreditStatusThroughDate = value; } }

        [FwLogicProperty(Id:"LhfhQyU9vigh")]
        public bool? CreditApplicationOnFile { get { return customer.CreditApplicationOnFile; } set { customer.CreditApplicationOnFile = value; } }

        [FwLogicProperty(Id:"c9WcHYqisjif")]
        public bool? CreditUnlimited { get { return customer.CreditUnlimited; } set { customer.CreditUnlimited = value; } }

        [FwLogicProperty(Id:"w3LVHUAJOjkL")]
        public int? CreditLimit { get { return customer.CreditLimit; } set { customer.CreditLimit = value; } }

        [FwLogicProperty(Id:"n1GK8mYsszuV", IsReadOnly:true)]
        public int? CreditBalance { get; set; }

        [FwLogicProperty(Id:"OuugAA14hLdq", IsReadOnly:true)]
        public int? CreditAvailable { get; set; }

        [FwLogicProperty(Id:"ORMlSNfOmLLg")]
        public bool? CreditResponsiblePartyOnFile { get { return customer.CreditResponsiblePartyOnFile; } set { customer.CreditResponsiblePartyOnFile = value; } }

        [FwLogicProperty(Id:"purIRZuhiwpW")]
        public string CreditResponsibleParty { get { return customer.CreditResponsibleParty; } set { customer.CreditResponsibleParty = value; } }

        [FwLogicProperty(Id:"UxLkF0Qpy1dM")]
        public bool? TradeReferencesVerified { get { return customer.TradeReferencesVerified; } set { customer.TradeReferencesVerified = value; } }

        [FwLogicProperty(Id:"cne25Ow0DY0s")]
        public string TradeReferencesVerifiedBy { get { return customer.TradeReferencesVerifiedBy; } set { customer.TradeReferencesVerifiedBy = value; } }

        [FwLogicProperty(Id:"xzqxzSnZmF3Z")]
        public string TradeReferencesVerifiedOn { get { return customer.TradeReferencesVerifiedOn; } set { customer.TradeReferencesVerifiedOn = value; } }

        [FwLogicProperty(Id:"GPKjpcpisuIv")]
        public string CreditCardTypeId { get { return customer.CreditCardTypeId; } set { customer.CreditCardTypeId = value; } }

        [FwLogicProperty(Id:"vFbVHCRMausC", IsReadOnly:true)]
        public string CreditCardType { get; set; }

        [FwLogicProperty(Id:"ePJW0aOkPUHV")]
        public int? CreditCardLimit { get { return customer.CreditCardLimit; } set { customer.CreditCardLimit = value; } }

        [FwLogicProperty(Id:"mjpdzCD694rZ")]
        public string CreditCardNo { get { return customer.CreditCardNo; } set { customer.CreditCardNo = value; } }

        [FwLogicProperty(Id:"yL4cLijQR1yd")]
        public string CreditCardCode { get { return customer.CreditCardCode; } set { customer.CreditCardCode = value; } }

        [FwLogicProperty(Id:"7p2UPCgffBOe")]
        public int? CreditCardExpirationMonth { get { return customer.CreditCardExpirationMonth; } set { customer.CreditCardExpirationMonth = value; } }

        [FwLogicProperty(Id:"GsqoZpmK5Nv0")]
        public int? CreditCardExpirationYear { get { return customer.CreditCardExpirationYear; } set { customer.CreditCardExpirationYear = value; } }

        [FwLogicProperty(Id:"PSHw15kUXbew")]
        public string CreditCardName { get { return customer.CreditCardName; } set { customer.CreditCardName = value; } }

        [FwLogicProperty(Id:"Mx6ColVHOJPL")]
        public bool? CreditCardAuthorizationOnFile { get { return customer.CreditCardAuthorizationOnFile; } set { customer.CreditCardAuthorizationOnFile = value; } }

        [FwLogicProperty(Id:"7QKdfG4umV8V")]
        public bool? InsuranceCertificationOnFile { get { return customer.InsuranceCertificationOnFile; } set { customer.InsuranceCertificationOnFile = value; } }

        [FwLogicProperty(Id:"Af9qRmwYd29z")]
        public string InsuranceCertificationValidThrough { get { return customer.InsuranceCertificationValidThrough; } set { customer.InsuranceCertificationValidThrough = value; } }

        [FwLogicProperty(Id:"qofZsK0iyYeO")]
        public int? InsuranceCoverageLiability { get { return customer.InsuranceCoverageLiability; } set { customer.InsuranceCoverageLiability = value; } }

        [FwLogicProperty(Id:"e9tlFbls9Sha")]
        public int? InsuranceCoverageLiabilityDeductible { get { return customer.InsuranceCoverageLiabilityDeductible; } set { customer.InsuranceCoverageLiabilityDeductible = value; } }

        [FwLogicProperty(Id:"MugESalq9LAh")]
        public int? InsuranceCoveragePropertyValue { get { return customer.InsuranceCoveragePropertyValue; } set { customer.InsuranceCoveragePropertyValue = value; } }

        [FwLogicProperty(Id:"0N2BG7fnlmrr")]
        public int? InsuranceCoveragePropertyValueDeductible { get { return customer.InsuranceCoveragePropertyValueDeductible; } set { customer.InsuranceCoveragePropertyValueDeductible = value; } }

        [FwLogicProperty(Id:"p6baZCxEFGCn")]
        public string InsuranceCompanyId { get { return customer.InsuranceCompanyId; } set { customer.InsuranceCompanyId = value; } }

        [FwLogicProperty(Id:"DEjqDVuSQnwv", IsReadOnly:true)]
        public string InsuranceCompany { get; set; }

        [FwLogicProperty(Id:"ajn4Okabrhfe")]
        public string InsuranceAgent { get { return customer.InsuranceAgent; } set { customer.InsuranceAgent = value; } }

        [FwLogicProperty(Id:"K1VTZOE2T6o8", IsReadOnly:true)]
        public string InsuranceCompanyAddress1 { get; set; }

        [FwLogicProperty(Id:"FXuemxg8LSiK", IsReadOnly:true)]
        public string InsuranceCompanyAddress2 { get; set; }

        [FwLogicProperty(Id:"aC8vY3nkNgFj", IsReadOnly:true)]
        public string InsuranceCompanyCity { get; set; }

        [FwLogicProperty(Id:"DEjqDVuSQnwv", IsReadOnly:true)]
        public string InsuranceCompanyState { get; set; }

        [FwLogicProperty(Id:"DEjqDVuSQnwv", IsReadOnly:true)]
        public string InsuranceCompanyZipCode { get; set; }

        [FwLogicProperty(Id:"7s2pA0V3FSil", IsReadOnly:true)]
        public string InsuranceCompanyCountryId { get; set; }

        [FwLogicProperty(Id:"7s2pA0V3FSil", IsReadOnly:true)]
        public string InsuranceCompanyCountry { get; set; }

        [FwLogicProperty(Id:"DEjqDVuSQnwv", IsReadOnly:true)]
        public string InsuranceCompanyPhone { get; set; }

        [FwLogicProperty(Id:"v5N1Oy9auuWB", IsReadOnly:true)]
        public string InsuranceCompanyFax { get; set; }

        [FwLogicProperty(Id:"gkwWrDkflQ8A")]
        public bool? VehicleInsuranceCertficationOnFile { get { return customer.VehicleInsuranceCertficationOnFile; } set { customer.VehicleInsuranceCertficationOnFile = value; } }

        [FwLogicProperty(Id:"9ByazBEqFvyr")]
        public bool? Taxable { get { return customer.Taxable; } set { customer.Taxable = value; } }

        [FwLogicProperty(Id:"ab23fu8xWKdS")]
        public string TaxStateOfIncorporationId { get { return customer.TaxStateOfIncorporationId; } set { customer.TaxStateOfIncorporationId = value; } }

        [FwLogicProperty(Id:"29MyKKkMXYsu", IsReadOnly:true)]
        public string TaxStateOfIncorporation { get; set; }

        [FwLogicProperty(Id:"cteyZtOBcCvp")]
        public string TaxFederalNo { get { return customer.TaxFederalNo; } set { customer.TaxFederalNo = value; } }

        [FwLogicProperty(Id:"gTP7XyMLs74S")]
        public int? NonTaxableYear { get { return customer.NonTaxableYear; } set { customer.NonTaxableYear = value; } }

        [FwLogicProperty(Id:"8V7OXKtaGmhF")]
        public string NonTaxableCertificateNo { get { return customer.NonTaxableCertificateNo; } set { customer.NonTaxableCertificateNo = value; } }

        [FwLogicProperty(Id:"lTOceEYLsx1Z")]
        public string NonTaxableCertificateValidThrough { get { return customer.NonTaxableCertificateValidThrough; } set { customer.NonTaxableCertificateValidThrough = value; } }

        [FwLogicProperty(Id:"3WdD9VWmELuw")]
        public bool? NonTaxableCertificateOnFile { get { return customer.NonTaxableCertificateOnFile; } set { customer.NonTaxableCertificateOnFile = value; } }

        [FwLogicProperty(Id:"oJ4fRqI5E8j9")]
        public bool? DisableQuoteOrderActivity { get { return customer.DisableQuoteOrderActivity; } set { customer.DisableQuoteOrderActivity = value; } }

        [FwLogicProperty(Id:"x0edTfpZrVBk")]
        public bool? DisableRental { get { return customer.DisableRental; } set { customer.DisableRental = value; } }

        [FwLogicProperty(Id:"cWONhWPaCkmQ")]
        public bool? DisableSales { get { return customer.DisableSales; } set { customer.DisableSales = value; } }

        [FwLogicProperty(Id:"gHu53qKWyZzl")]
        public bool? DisableFacilities { get { return customer.DisableFacilities; } set { customer.DisableFacilities = value; } }

        [FwLogicProperty(Id:"S2dkl2dHXOKr")]
        public bool? DisableTransportation { get { return customer.DisableTransportation; } set { customer.DisableTransportation = value; } }

        [FwLogicProperty(Id:"xwidczonHI1w")]
        public bool? DisableLabor { get { return customer.DisableLabor; } set { customer.DisableLabor = value; } }

        [FwLogicProperty(Id:"mumj3fi7ZOFv")]
        public bool? DisableMisc { get { return customer.DisableMisc; } set { customer.DisableMisc = value; } }

        [FwLogicProperty(Id:"Bh5BWiRfK7QD")]
        public bool? DisableRentalSale { get { return customer.DisableRentalSale; } set { customer.DisableRentalSale = value; } }

        [FwLogicProperty(Id:"bWZcV2NRPCU3")]
        public bool? DisableSubRental { get { return customer.DisableSubRental; } set { customer.DisableSubRental = value; } }

        [FwLogicProperty(Id:"buZyAS6s3AU0")]
        public bool? DisableSubSale { get { return customer.DisableSubSale; } set { customer.DisableSubSale = value; } }

        [FwLogicProperty(Id:"E3h3jWGSSOt0")]
        public bool? DisableSubLabor { get { return customer.DisableSubLabor; } set { customer.DisableSubLabor = value; } }

        [FwLogicProperty(Id:"uaILJPvWcBqa")]
        public bool? DisableSubMisc { get { return customer.DisableSubMisc; } set { customer.DisableSubMisc = value; } }

        [FwLogicProperty(Id:"k2wIjo3YHFr5")]
        public bool? SplitRental { get { return customer.SplitRental; } set { customer.SplitRental = value; } }

        [FwLogicProperty(Id:"42KNQdtL2R9s")]
        public bool? SplitRentalTaxCustomer { get { return customer.SplitRentalTaxCustomer; } set { customer.SplitRentalTaxCustomer = value; } }

        [FwLogicProperty(Id:"T7z3MftmqNal")]
        public int? OwnedEquipmentSplitRentalPerecent { get { return customer.OwnedEquipmentSplitRentalPerecent; } set { customer.OwnedEquipmentSplitRentalPerecent = value; } }

        [FwLogicProperty(Id:"krzzAAXJUMlQ")]
        public int? SubRentalEquipmentSplitRentalPerecent { get { return customer.SubRentalEquipmentSplitRentalPerecent; } set { customer.SubRentalEquipmentSplitRentalPerecent = value; } }

        [FwLogicProperty(Id:"W2eWjT92ewA5")]
        public bool? RebateRental { get { return customer.RebateRental; } set { customer.RebateRental = value; } }

        [FwLogicProperty(Id:"b0h1RhBVpnML")]
        public int? OwnedEquipmentRebateRentalPerecent { get { return customer.OwnedEquipmentRebateRentalPerecent; } set { customer.OwnedEquipmentRebateRentalPerecent = value; } }

        [FwLogicProperty(Id:"erwGW2UKC9y9")]
        public int? SubRentalEquipmentRebateRentalPerecent { get { return customer.SubRentalEquipmentRebateRentalPerecent; } set { customer.SubRentalEquipmentRebateRentalPerecent = value; } }

        [FwLogicProperty(Id:"uqhuFSy2MoQ8")]
        public string SplitRentalLogoFileName { get { return customer.SplitRentalLogoFileName; } set { customer.SplitRentalLogoFileName = value; } }

        [FwLogicProperty(Id:"wMS3eMcPI6py")]
        public int? SplitRentalLogoWidth { get { return customer.SplitRentalLogoWidth; } set { customer.SplitRentalLogoWidth = value; } }

        [FwLogicProperty(Id:"uKSZaMMPJYmB")]
        public int? SplitRentalLogoHeight { get { return customer.SplitRentalLogoHeight; } set { customer.SplitRentalLogoHeight = value; } }

        [FwLogicProperty(Id:"zSnn98O3iisV")]
        public string ShippingAddressType { get { return customer.ShippingAddressType; } set { customer.ShippingAddressType = value; } }

        [FwLogicProperty(Id:"rIKrPLqo6awP")]
        public string ShipAttention { get { return customer.ShipAttention; } set { customer.ShipAttention = value; } }

        [FwLogicProperty(Id:"zfwlggEpY6Xc")]
        public string ShipAddress1 { get { return customer.ShipAddress1; } set { customer.ShipAddress1 = value; } }

        [FwLogicProperty(Id:"M2LQNKIHCkCY")]
        public string ShipAddress2 { get { return customer.ShipAddress2; } set { customer.ShipAddress2 = value; } }

        [FwLogicProperty(Id:"KkXBeBDmMiKi")]
        public string ShipCity { get { return customer.ShipCity; } set { customer.ShipCity = value; } }

        [FwLogicProperty(Id:"ozM8vUsqYMJG")]
        public string ShipState { get { return customer.ShipState; } set { customer.ShipState = value; } }

        [FwLogicProperty(Id:"ctNBGtdypIUw")]
        public string ShipCountryId { get { return customer.ShipCountryId; } set { customer.ShipCountryId = value; } }

        [FwLogicProperty(Id:"7s2pA0V3FSil", IsReadOnly:true)]
        public string ShipCountry { get; set; }

        [FwLogicProperty(Id:"cBkQMzxnKtgX")]
        public string ShipZipCode { get { return customer.ShipZipCode; } set { customer.ShipZipCode = value; } }

        [FwLogicProperty(Id:"jgUoBTFkpbw2", IsReadOnly:true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id:"FT7KQMjGTphv")]
        public string DateStamp { get { return customer.DateStamp; } set { customer.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(CustomerLogic).GetProperty(nameof(CustomerLogic.BillingAddressType));
                string[] acceptableValues = { RwConstants.BILLING_ADDRESS_TYPE_CUSTOMER, RwConstants.BILLING_ADDRESS_TYPE_OTHER };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveCustomer(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(CustomerNumber))
                {
                    bool x = customer.SetNumber(e.SqlConnection).Result;
                }
            }
            else  // updating
            {
            }
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
        public async Task<GetManyResponse<GetManyOfficeLocationModel>> GetOfficeLocationsAsync(GetManyOfficeLocationRequest request)
        {
            var officeLocationLogic = CreateBusinessLogic<OfficeLocationLogic>(this.AppConfig, this.UserSession);
            request.filters["Inactive"] = new GetManyRequestFilter("Inactive", "ne", "true", false);
            var result = await officeLocationLogic.GetManyAsync<GetManyOfficeLocationModel>(request);
            return result;
        }
        //------------------------------------------------------------------------------------ 
    }
}

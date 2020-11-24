using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [FwSqlTable("locationview")]
    public class OfficeLocationLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string LocationId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
        public string CompanyName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittofedid", modeltype: FwDataTypes.Text)]
        public string FederalId { get; set; }
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
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittocompany", modeltype: FwDataTypes.Text)]
        public string RemitToCompanyName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittoadd1", modeltype: FwDataTypes.Text)]
        public string RemitToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittoadd2", modeltype: FwDataTypes.Text)]
        public string RemitToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittocity", modeltype: FwDataTypes.Text)]
        public string RemitToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittozip", modeltype: FwDataTypes.Text)]
        public string RemitToZip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittostate", modeltype: FwDataTypes.Text)]
        public string RemitToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittophone", modeltype: FwDataTypes.Text)]
        public string RemitToPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittocountryid", modeltype: FwDataTypes.Text)]
        public string RemitToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittocountry", modeltype: FwDataTypes.Text)]
        public string RemitToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittofax", modeltype: FwDataTypes.Text)]
        public string RemitToFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remittoemail", modeltype: FwDataTypes.Text)]
        public string RemitToEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetypedisplay", modeltype: FwDataTypes.Text)]
        public string RateTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpoordertypeid", modeltype: FwDataTypes.Text)]
        public string DefaultPurchasePoTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpoordertype", modeltype: FwDataTypes.Text)]
        public string DefaultPurchasePoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "useprefix", modeltype: FwDataTypes.Boolean)]
        public bool? UseNumberPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefix", modeltype: FwDataTypes.Text)]
        public string NumberPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usereq", modeltype: FwDataTypes.Boolean)]
        public bool? UseRequisitionNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samequoteno", modeltype: FwDataTypes.Boolean)]
        public bool? UseSameNumberForQuoteAndOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samebatchno", modeltype: FwDataTypes.Boolean)]
        public bool? UseSameNumberForAllExportBatches { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedbyorder", modeltype: FwDataTypes.Boolean)]
        public bool? UserOrderNumberAndSuffixForInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedforhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? UseHInHiatusInvoiceNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string DefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcurrencycode", modeltype: FwDataTypes.Text)]
        public string DefaultCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcurrency", modeltype: FwDataTypes.Text)]
        public string DefaultCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1referencename", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax1referenceno", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2referencename", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2referenceno", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disablecredit", modeltype: FwDataTypes.Boolean)]
        public bool? DisableCreditStatusMessages { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disablecreditthroughdate", modeltype: FwDataTypes.Boolean)]
        public bool? DisableCreditThroughDateMessages { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableinsurance", modeltype: FwDataTypes.Boolean)]
        public bool? DisableInsuranceStatusMessages { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableinsurancethroughdate", modeltype: FwDataTypes.Boolean)]
        public bool? DisableInsuranceThroughDateMessages { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warniftandcnotonfile", modeltype: FwDataTypes.Boolean)]
        public bool? WarnIfTermsAndConditionsNotOnFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicemessage", modeltype: FwDataTypes.Text)]
        public string InvoiceMessage { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "autoapplydepletingdeposittoinvoice", modeltype: FwDataTypes.Boolean)]
        public bool AutoApplyDepletingDepositToInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depositreplacmentvaluepercent", modeltype: FwDataTypes.Decimal)]
        public decimal DepositReplacmentValuePercent { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

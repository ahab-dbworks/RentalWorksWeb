using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace WebApi.Modules.Home.Invoice
{
    [FwSqlTable("invoicewebview")]
    public class InvoiceLoader : InvoiceBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceduedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "episodeno", modeltype: FwDataTypes.Integer)]
        public int? EpisodeNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchno", modeltype: FwDataTypes.Integer)]
        public int? InvoiceCreationBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invgroupno", modeltype: FwDataTypes.Text)]
        public string InvoiceGroupNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "crfid", modeltype: FwDataTypes.Text)]
        //public string CrfId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "crfno", modeltype: FwDataTypes.Integer)]
        //public int? Crfno { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "finalld", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text)]
        public string FlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text)]
        public string RebateCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgmajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgsub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgdeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludefromflat", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeFromFlatPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "splitrentalflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsSplitRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsRebateRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicelisttotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceListTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicegrosstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceGrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicediscounttotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceDiscountTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedwdiscounttotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceDaysPerWeekDiscountTotal { get; set; }
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
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text)]
        public string InvoiceClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printnotes", modeltype: FwDataTypes.Text)]
        public string PrintNotes { get; set; }
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
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxitemcode", modeltype: FwDataTypes.Text)]
        public string TaxItemCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxvendor", modeltype: FwDataTypes.Text)]
        public string TaxVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxcountry", modeltype: FwDataTypes.Text)]
        public string TaxCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text)]
        public string ChargeBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string ChargeBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscount", modeltype: FwDataTypes.Boolean)]
        public bool? QuikPayDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikPayRentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaytotal", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikPayTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ReceivedTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignmentrevenue", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignmentRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? IsNonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        public string DivisionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exporttaxaslineitem", modeltype: FwDataTypes.Boolean)]
        public bool? ExportTaxAsLineItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasmeteritem", modeltype: FwDataTypes.Boolean)]
        public bool? HasMeterItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hassaleitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasSalesItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haslaboritem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLaborItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasmiscitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasMiscellaneousItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasspaceitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasFacilityItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasvehicleitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasTransportationItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalsaleitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? MeterTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? MiscellaneousTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assettotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric")]
        public decimal? AssetSaleTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismisc", modeltype: FwDataTypes.Boolean)]
        public bool? IsStandAloneInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

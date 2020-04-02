using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Modules.HomeControls.OrderDates;

namespace WebApi.Modules.Agent.Order
{
    [FwSqlTable("orderwebview")]
    public abstract class OrderBaseLoader : OrderBaseBrowseLoader
    {
        //------------------------------------------------------------------------------------
        public OrderBaseLoader()
        {
            AfterLoad += OnAfterLoad;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hassalesitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasSalesItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasmiscitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasMiscellaneousItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haslaboritem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLaborItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasspaceitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasFacilitiesItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasfinallditem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLossAndDamageItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrentalsaleitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrepair", modeltype: FwDataTypes.Boolean)]
        public bool? HasRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypecombineactivitytabs", modeltype: FwDataTypes.Boolean)]
        public bool? OrderTypeCombineActivityTabs { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean)]
        public bool? FlatPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean)]
        public bool? PendingPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "pickupdate", modeltype: FwDataTypes.Date)]
        public string PickUpDate { get; set; }
        [FwSqlDataField(column: "pickuptime", modeltype: FwDataTypes.Text)]
        public string PickUpTime { get; set; }
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date)]
        public string PrepDate { get; set; }
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string PrepTime { get; set; }
        [FwSqlDataField(column: "loadindate", modeltype: FwDataTypes.Date)]
        public string LoadInDate { get; set; }
        [FwSqlDataField(column: "loadintime", modeltype: FwDataTypes.Text)]
        public string LoadInTime { get; set; }
        [FwSqlDataField(column: "strikedate", modeltype: FwDataTypes.Date)]
        public string StrikeDate { get; set; }
        [FwSqlDataField(column: "striketime", modeltype: FwDataTypes.Text)]
        public string StrikeTime { get; set; }
        [FwSqlDataField(column: "testdate", modeltype: FwDataTypes.Date)]
        public string TestDate { get; set; }
        [FwSqlDataField(column: "testtime", modeltype: FwDataTypes.Text)]
        public string TestTime { get; set; }
        //------------------------------------------------------------------------------------ 



        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lockbillingdates", modeltype: FwDataTypes.Boolean)]
        public bool? LockBillingDates { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "delaybillingsearchuntil", modeltype: FwDataTypes.Date)]
        public string DelayBillingSearchUntil { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperioddatesbytype", modeltype: FwDataTypes.Boolean)]
        public bool? SpecifyBillingDatesByType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartrental", modeltype: FwDataTypes.Date)]
        public string RentalBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendrental", modeltype: FwDataTypes.Date)]
        public string RentalBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartlabor", modeltype: FwDataTypes.Date)]
        public string LaborBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendlabor", modeltype: FwDataTypes.Date)]
        public string LaborBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartmisc", modeltype: FwDataTypes.Date)]
        public string MiscellaneousBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendmisc", modeltype: FwDataTypes.Date)]
        public string MiscellaneousBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartspace", modeltype: FwDataTypes.Date)]
        public string FacilitiesBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendspace", modeltype: FwDataTypes.Date)]
        public string FacilitiesBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstartvehicle", modeltype: FwDataTypes.Date)]
        public string VehicleBillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodendvehicle", modeltype: FwDataTypes.Date)]
        public string VehicleBillingEndDate { get; set; }
        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prepfeesinrentalrate", modeltype: FwDataTypes.Boolean)]
        public bool? IncludePrepFeesInRentalRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingweeks", modeltype: FwDataTypes.Decimal)]
        public decimal? BillingWeeks { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingmonths", modeltype: FwDataTypes.Decimal)]
        public decimal? BillingMonths { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        public string DetermineQuantitiesToBillBasedOn { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
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
        [FwSqlDataField(column: "nochargereason", modeltype: FwDataTypes.Text)]
        public string NoChargeReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd", modeltype: FwDataTypes.Text)]
        public string PrintIssuedToAddressFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoname", modeltype: FwDataTypes.Text)]
        public string IssuedToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoattention", modeltype: FwDataTypes.Text)]
        public string IssuedToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoattention2", modeltype: FwDataTypes.Text)]
        public string IssuedToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd1", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd2", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocity", modeltype: FwDataTypes.Text)]
        public string IssuedToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtostate", modeltype: FwDataTypes.Text)]
        public string IssuedToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtozip", modeltype: FwDataTypes.Text)]
        public string IssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocountryid", modeltype: FwDataTypes.Text)]
        public string IssuedToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtocountry", modeltype: FwDataTypes.Text)]
        public string IssuedToCountry { get; set; }
        //------------------------------------------------------------------------------------



        [FwSqlDataField(column: "differentbilladdress", modeltype: FwDataTypes.Boolean)]
        public bool? BillToAddressDifferentFromIssuedToAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoaddressid", modeltype: FwDataTypes.Text)]
        public string BillToAddressId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoname", modeltype: FwDataTypes.Text)]
        public string BillToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoattention", modeltype: FwDataTypes.Text)]
        public string BillToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtoattention2", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtocountryid", modeltype: FwDataTypes.Text)]
        public string BillToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billtocountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "invoicediscountreasonid", modeltype: FwDataTypes.Text)]
        public string DiscountReasonId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicediscountreason", modeltype: FwDataTypes.Text)]
        public string DiscountReason { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "requirecontactconfirmation", modeltype: FwDataTypes.Boolean)]
        public bool? RequireContactConfirmation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeInBillingAnalysis { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text)]
        public string HiatusDiscountFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "roundtriprentals", modeltype: FwDataTypes.Boolean)]
        public bool? RoundTripRentals { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "summaryinvoice", modeltype: FwDataTypes.Boolean)]
        public bool? InGroup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer)]
        public int GroupNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "coverletterid", modeltype: FwDataTypes.Text)]
        public string CoverLetterId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "coverletter", modeltype: FwDataTypes.Text)]
        public string CoverLetter { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text)]
        public string TermsConditionsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "termsconditions", modeltype: FwDataTypes.Text)]
        public string TermsConditions { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "presentationlayer", modeltype: FwDataTypes.Text)]
        public string PresentationLayer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "markettypeid", modeltype: FwDataTypes.Text)]
        public string MarketTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "markettype", modeltype: FwDataTypes.Text)]
        public string MarketType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegmentid", modeltype: FwDataTypes.Text)]
        public string MarketSegmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegment", modeltype: FwDataTypes.Text)]
        public string MarketSegment { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegmentjobid", modeltype: FwDataTypes.Text)]
        public string MarketSegmentJobId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "marketsegmentjob", modeltype: FwDataTypes.Text)]
        public string MarketSegmentJob { get; set; }
        //------------------------------------------------------------------------------------





        [FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverydeliverytype", modeltype: FwDataTypes.Text)]
        public string OutDeliveryDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryrequireddate", modeltype: FwDataTypes.Date)]
        public string OutDeliveryRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryrequiredtime", modeltype: FwDataTypes.Text)]
        public string OutDeliveryRequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytargetshipdate", modeltype: FwDataTypes.Date)]
        public string OutDeliveryTargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytargetshiptime", modeltype: FwDataTypes.Text)]
        public string OutDeliveryTargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromlocation", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcontact", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcontactalternate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcontactphone", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromattention", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromadd1", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromadd2", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcity", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromstate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromzip", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcountry", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcrossstreets", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytolocation", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytoattention", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytoadd1", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytoadd2", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocity", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytostate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytozip", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocountry", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocountryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocrossstreets", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytometric", modeltype: FwDataTypes.Boolean)]
        public bool? OutDeliveryToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocontact", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocontactalternate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocontactphone", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytocontactfax", modeltype: FwDataTypes.Text)]
        public string OutDeliveryToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string OutDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycarrierid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycarrier", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCarrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryshipviaid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryshipvia", modeltype: FwDataTypes.Text)]
        public string OutDeliveryShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryinvoiceid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryvendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycarrieracct", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryestimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? OutDeliveryEstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfreightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? OutDeliveryFreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverychargetype", modeltype: FwDataTypes.Text)]
        public string OutDeliveryChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfreighttrackno", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytrackingurl", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryfromcountryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryFromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytemplate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryaddresstype", modeltype: FwDataTypes.Text)]
        public string OutDeliveryAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverydirection", modeltype: FwDataTypes.Text)]
        public string OutDeliveryDirection { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? OutDeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "outdeliveryorderid", modeltype: FwDataTypes.Text)]
        //public string OutDeliveryOrderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverypackagecode", modeltype: FwDataTypes.Text)]
        public string OutDeliveryPackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverybillpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? OutDeliveryBillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryonlineorderno", modeltype: FwDataTypes.Text)]
        public string OutDeliveryOnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryonlineorderstatus", modeltype: FwDataTypes.Text)]
        public string OutDeliveryOnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverydatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string OutDeliveryDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverydeliverytype", modeltype: FwDataTypes.Text)]
        public string InDeliveryDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryrequireddate", modeltype: FwDataTypes.Date)]
        public string InDeliveryRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryrequiredtime", modeltype: FwDataTypes.Text)]
        public string InDeliveryRequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytargetshipdate", modeltype: FwDataTypes.Date)]
        public string InDeliveryTargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytargetshiptime", modeltype: FwDataTypes.Text)]
        public string InDeliveryTargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromlocation", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcontact", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcontactalternate", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcontactphone", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromattention", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromadd1", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromadd2", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcity", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromstate", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromzip", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcountry", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcrossstreets", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytolocation", modeltype: FwDataTypes.Text)]
        public string InDeliveryToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytoattention", modeltype: FwDataTypes.Text)]
        public string InDeliveryToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytoadd1", modeltype: FwDataTypes.Text)]
        public string InDeliveryToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytoadd2", modeltype: FwDataTypes.Text)]
        public string InDeliveryToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocity", modeltype: FwDataTypes.Text)]
        public string InDeliveryToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytostate", modeltype: FwDataTypes.Text)]
        public string InDeliveryToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytozip", modeltype: FwDataTypes.Text)]
        public string InDeliveryToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocountry", modeltype: FwDataTypes.Text)]
        public string InDeliveryToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocountryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocrossstreets", modeltype: FwDataTypes.Text)]
        public string InDeliveryToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytometric", modeltype: FwDataTypes.Boolean)]
        public bool? InDeliveryToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocontact", modeltype: FwDataTypes.Text)]
        public string InDeliveryToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocontactalternate", modeltype: FwDataTypes.Text)]
        public string InDeliveryToAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocontactphone", modeltype: FwDataTypes.Text)]
        public string InDeliveryToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string InDeliveryToAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytocontactfax", modeltype: FwDataTypes.Text)]
        public string InDeliveryToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string InDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycarrierid", modeltype: FwDataTypes.Text)]
        public string InDeliveryCarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycarrier", modeltype: FwDataTypes.Text)]
        public string InDeliveryCarrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryshipviaid", modeltype: FwDataTypes.Text)]
        public string InDeliveryShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryshipvia", modeltype: FwDataTypes.Text)]
        public string InDeliveryShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryinvoiceid", modeltype: FwDataTypes.Text)]
        public string InDeliveryInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryvendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string InDeliveryVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycarrieracct", modeltype: FwDataTypes.Text)]
        public string InDeliveryCarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryestimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? InDeliveryEstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfreightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? InDeliveryFreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverychargetype", modeltype: FwDataTypes.Text)]
        public string InDeliveryChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfreighttrackno", modeltype: FwDataTypes.Text)]
        public string InDeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytrackingurl", modeltype: FwDataTypes.Text)]
        public string InDeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryfromcountryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryFromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytemplate", modeltype: FwDataTypes.Text)]
        public string InDeliveryTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryaddresstype", modeltype: FwDataTypes.Text)]
        public string InDeliveryAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverydirection", modeltype: FwDataTypes.Text)]
        public string InDeliveryDirection { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? InDeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "indeliveryorderid", modeltype: FwDataTypes.Text)]
        //public string InDeliveryOrderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverypackagecode", modeltype: FwDataTypes.Text)]
        public string InDeliveryPackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverybillpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? InDeliveryBillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryonlineorderno", modeltype: FwDataTypes.Text)]
        public string InDeliveryOnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryonlineorderstatus", modeltype: FwDataTypes.Text)]
        public string InDeliveryOnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverydatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string InDeliveryDateStamp { get; set; }


        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklyRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlyRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? SalesTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PartsTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacedaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? SpaceDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacediscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SpaceDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacesplitpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SpaceSplitPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyspacetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySpaceTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyspacetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySpaceTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodspacetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSpaceTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyspacetotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySpaceTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyspacetotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySpaceTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodspacetotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSpaceTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicledaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklyVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlyVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklymisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlymisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklymisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklyMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlymisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlyMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labordiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklylabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlylabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklylabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklyLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlylabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlyLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rsdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rstotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSaleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lddiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? LossAndDamageDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LossAndDamageTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldtotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamageTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "rentaldaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? CombinedDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? CombinedDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCombinedTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCombinedTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodCombinedTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklyrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklyCombinedTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlyrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlyCombinedTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodCombinedTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "departmentdisableeditraterental", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingRentalRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentdisableeditratesales", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingSalesRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentdisableeditratemisc", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingMiscellaneousRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentdisableeditratelabor", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingLaborRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentdisableeditrateusedsale", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingUsedSaleRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentdisableeditrateld", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingLossAndDamageRate { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "rentalextended", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalExtendedTotal { get; set; }
        [FwSqlDataField(column: "salesextended", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesExtendedTotal { get; set; }
        [FwSqlDataField(column: "laborextended", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborExtendedTotal { get; set; }
        [FwSqlDataField(column: "miscextended", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscellaneousExtendedTotal { get; set; }
        [FwSqlDataField(column: "rentalsaleextended", modeltype: FwDataTypes.Decimal)]
        public decimal? UsedSaleExtendedTotal { get; set; }
        [FwSqlDataField(column: "ldextended", modeltype: FwDataTypes.Decimal)]
        public decimal? LossAndDamageExtendedTotal { get; set; }



        [FwSqlDataField(column: "hasnotes", modeltype: FwDataTypes.Boolean)]
        public bool? HasNotes { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "totalreplacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalReplacementCost { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "manualsort", modeltype: FwDataTypes.Boolean)]
        public bool? IsManualSort { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "quoteorderid", modeltype: FwDataTypes.Text)]
        public string RelatedQuoteOrderId { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "quoteordertitle", modeltype: FwDataTypes.Text)]
        public string QuoteOrderTitle { get; set; }
        //------------------------------------------------------------------------------------


        public List<OrderDatesLogic> ActivityDatesAndTimes { get; set; } = new List<OrderDatesLogic>();

        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        public void OnAfterLoad(object sender, AfterLoadEventArgs e)
        {
            if ((e.Record != null) && (e.Record is OrderBaseLoader))
            {
                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = "OrderBy";
                request.uniqueids = new Dictionary<string, object>();
                request.uniqueids.Add("OrderId", GetPrimaryKeys()[0].ToString());
                request.uniqueids.Add("Enabled", true);
                OrderDatesLogic l = new OrderDatesLogic();
                l.SetDependencies(AppConfig, UserSession);
                ((OrderBaseLoader)e.Record).ActivityDatesAndTimes = l.SelectAsync<OrderDatesLogic>(request).Result;
            }
        }
        //------------------------------------------------------------------------------------    
    }
}

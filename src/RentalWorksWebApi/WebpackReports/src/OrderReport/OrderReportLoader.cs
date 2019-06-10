using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.OrderReport
{

    public class OrderItemReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> LoadItems(OrderReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetorderprintdetails", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
            string[] totalFields = new string[] { "PeriodExtended" };
            dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }


    public class OrderReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccompany", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd1", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd2", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccitystatezip", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locphone", modeltype: FwDataTypes.Text)]
        public string OfficeLocationPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealwithno", modeltype: FwDataTypes.Text)]
        public string DealAndDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorwithno", modeltype: FwDataTypes.Text)]
        public string VendorAndVendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Text)]
        public string VersionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poamt", modeltype: FwDataTypes.Decimal)]
        public decimal? PoAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billname", modeltype: FwDataTypes.Text)]
        public string BillToName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string BillToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text)]
        public string BillToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billzip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcitystatezip", modeltype: FwDataTypes.Text)]
        public string BillToCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcitystatezipcounrty", modeltype: FwDataTypes.Text)]
        public string BillToCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernodesc", modeltype: FwDataTypes.Text)]
        public string OrderNumberAndDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfromdatetime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrenttodatetime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagedates", modeltype: FwDataTypes.Text)]
        public string UsageDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagedatesandtimes", modeltype: FwDataTypes.Text)]
        public string UsageDatesAndTimes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        public string BillingDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "terms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentemail", modeltype: FwDataTypes.Text)]
        public string AgentEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphoneext", modeltype: FwDataTypes.Text)]
        public string AgentPhoneAndExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphone", modeltype: FwDataTypes.Text)]
        public string AgentPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentext", modeltype: FwDataTypes.Text)]
        public string AgentExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentfax", modeltype: FwDataTypes.Text)]
        public string AgentFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanageremail", modeltype: FwDataTypes.Text)]
        public string ProjectManagerEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerphoneext", modeltype: FwDataTypes.Text)]
        public string ProjectManagerPhoneAndExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerphone", modeltype: FwDataTypes.Text)]
        public string ProjectManagerPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerext", modeltype: FwDataTypes.Text)]
        public string ProjectManagerExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerfax", modeltype: FwDataTypes.Text)]
        public string ProjectManagerFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text)]
        public string TermsconditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsfilename", modeltype: FwDataTypes.Text)]
        public string TermsAndConditionsFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsnewpage", modeltype: FwDataTypes.Boolean)]
        public bool? TermsAndConditionsNewPage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditions", modeltype: FwDataTypes.Text)]
        public string TermsAndConditions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approver", modeltype: FwDataTypes.Text)]
        public string Approver { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date)]
        public string ApprovedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondapprover", modeltype: FwDataTypes.Text)]
        public string SecondApprover { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondapproveddate", modeltype: FwDataTypes.Date)]
        public string SecondApprovedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredate", modeltype: FwDataTypes.Date)]
        public string RequireDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text)]
        public string SubPoOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderdesc", modeltype: FwDataTypes.Text)]
        public string SubPoOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeal", modeltype: FwDataTypes.Text)]
        public string SubPoDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealno", modeltype: FwDataTypes.Text)]
        public string SubPoDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealwithno", modeltype: FwDataTypes.Text)]
        public string SubPoDealAndDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitiondate", modeltype: FwDataTypes.Date)]
        public string RequisitionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Decimal)]
        public decimal? SummaryInvoiceGroup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadindate", modeltype: FwDataTypes.Date)]
        public string LoadInDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loadintime", modeltype: FwDataTypes.Text)]
        public string LoadInTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikedate", modeltype: FwDataTypes.Date)]
        public string StrikeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "striketime", modeltype: FwDataTypes.Text)]
        public string StrikeTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "coverletterfilename", modeltype: FwDataTypes.Text)]
        public string CoverLetterFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productioncontactid", modeltype: FwDataTypes.Text)]
        public string ProductionContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "confirmedby", modeltype: FwDataTypes.Text)]
        public string ConfirmedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "confirmedsignature", modeltype: FwDataTypes.Text)]
        public string ConfirmedSignature { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "confirmeddatetime", modeltype: FwDataTypes.Date)]
        public string ConfirmedDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customernumber", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderunit", modeltype: FwDataTypes.Text)]
        public string OrderUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocompany", modeltype: FwDataTypes.Text)]
        public string IssuedToCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtoatt1", modeltype: FwDataTypes.Text)]
        public string IssuedToAttention1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtoatt2", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "issuedtocountry", modeltype: FwDataTypes.Text)]
        public string IssuedToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocountryid", modeltype: FwDataTypes.Text)]
        public string IssuedToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtophone", modeltype: FwDataTypes.Text)]
        public string IssuedToPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtofax", modeltype: FwDataTypes.Text)]
        public string IssuedToFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocontact", modeltype: FwDataTypes.Text)]
        public string IssuedToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocontactphone", modeltype: FwDataTypes.Text)]
        public string IssuedToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocontactemail", modeltype: FwDataTypes.Text)]
        public string IssuedToContactEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytype", modeltype: FwDataTypes.Text)]
        public string OutDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverytypedisplay", modeltype: FwDataTypes.Text)]
        public string OutDeliveryTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryattention", modeltype: FwDataTypes.Text)]
        public string OutDeliveryAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverylocation", modeltype: FwDataTypes.Text)]
        public string OutDeliveryLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryadd1", modeltype: FwDataTypes.Text)]
        public string OutDeliveryAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryadd2", modeltype: FwDataTypes.Text)]
        public string OutDeliveryAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycity", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverystate", modeltype: FwDataTypes.Text)]
        public string OutDeliveryState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryzip", modeltype: FwDataTypes.Text)]
        public string OutDeliveryZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycountryid", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycountry", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycontact", modeltype: FwDataTypes.Text)]
        public string OutDeliveryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycontactphone", modeltype: FwDataTypes.Text)]
        public string OutDeliveryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryrequiredbydate", modeltype: FwDataTypes.Date)]
        public string OutDeliveryRequiredByDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryrequiredbytime", modeltype: FwDataTypes.Text)]
        public string OutDeliveryRequiredbyTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliveryrequiredbydatetime", modeltype: FwDataTypes.Text)]
        public string OutDeliveryRequiredByDateTime { get; set; }
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
        [FwSqlDataField(column: "outdeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string OutDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytype", modeltype: FwDataTypes.Text)]
        public string InDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverytypedisplay", modeltype: FwDataTypes.Text)]
        public string InDeliveryTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryattention", modeltype: FwDataTypes.Text)]
        public string InDeliveryAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverylocation", modeltype: FwDataTypes.Text)]
        public string InDeliveryLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryadd1", modeltype: FwDataTypes.Text)]
        public string InDeliveryAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryadd2", modeltype: FwDataTypes.Text)]
        public string InDeliveryAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycity", modeltype: FwDataTypes.Text)]
        public string InDeliveryCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverystate", modeltype: FwDataTypes.Text)]
        public string InDeliveryState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryzip", modeltype: FwDataTypes.Text)]
        public string InDeliveryZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycountryid", modeltype: FwDataTypes.Text)]
        public string InDeliveryCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycountry", modeltype: FwDataTypes.Text)]
        public string InDeliveryCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string InDeliveryCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycontact", modeltype: FwDataTypes.Text)]
        public string InDeliveryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliverycontactphone", modeltype: FwDataTypes.Text)]
        public string InDeliveryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryrequiredbydate", modeltype: FwDataTypes.Date)]
        public string InDeliveryRequiredByDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryrequiredbytime", modeltype: FwDataTypes.Text)]
        public string InDeliveryRequiredbyTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indeliveryrequiredbydatetime", modeltype: FwDataTypes.Text)]
        public string InDeliveryRequiredByDateTime { get; set; }
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
        [FwSqlDataField(column: "indeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string InDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        public FwJsonDataTable Items { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<OrderReportLoader> RunReportAsync(OrderReportRequest request)
        {
            Task<OrderReportLoader> taskOrder;
            Task<FwJsonDataTable> taskOrderItems;

            OrderReportLoader Order = null;
            OrderItemReportLoader OrderItems = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetorderprintheader", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    taskOrder = qry.QueryToTypedObjectAsync<OrderReportLoader>();

                    OrderItems = new OrderItemReportLoader();
                    OrderItems.SetDependencies(AppConfig, UserSession);
                    taskOrderItems = OrderItems.LoadItems(request);

                    await Task.WhenAll(new Task[] { taskOrder, taskOrderItems });

                    Order = taskOrder.Result;

                    if (Order != null)
                    {
                        Order.Items = taskOrderItems.Result;
                    }
                }
            }
            return Order;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Modules.HomeControls.OrderDates;

namespace WebApi.Modules.Agent.PurchaseOrder
{
    [FwSqlTable("powebview")]
    public class PurchaseOrderLoader : PurchaseOrderBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        public PurchaseOrderLoader()
        {
            AfterLoad += OnAfterLoad;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitiondate", modeltype: FwDataTypes.Date)]
        public string RequisitionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbyusersid", modeltype: FwDataTypes.Text)]
        public string ApprovedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbysecondusersid", modeltype: FwDataTypes.Text)]
        public string ApprovedBySecondUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean)]
        public bool? SubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean)]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignment", modeltype: FwDataTypes.Boolean)]
        public bool? Consignment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
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
        [FwSqlDataField(column: "tax1name", modeltype: FwDataTypes.Text)]
        public string Tax1Name { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2name", modeltype: FwDataTypes.Text)]
        public string Tax2Name { get; set; }
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
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptlocrequiresapproval", modeltype: FwDataTypes.Boolean)]
        public bool? DepartmentLocationRequiresApproval { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbydate", modeltype: FwDataTypes.Date)]
        public string RequiredByDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text)]
        public string PoClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text)]
        public string PoClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string RemitToAttention1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention2", modeltype: FwDataTypes.Text)]
        public string RemitToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text)]
        public string RemitToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text)]
        public string RemitToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text)]
        public string RemitToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text)]
        public string RemitToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcountryid", modeltype: FwDataTypes.Text)]
        public string RemitToCountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcountry", modeltype: FwDataTypes.Text)]
        public string RemitToCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billemail", modeltype: FwDataTypes.Text)]
        public string RemitToEmail { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billphone", modeltype: FwDataTypes.Text)]
        public string RemitToPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsdays", modeltype: FwDataTypes.Integer)]
        public int? PaymentTermsDueInDays { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicedamount", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoicedAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agreementno", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatus", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovalstatustype", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatustype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "outdeliveryid", modeltype: FwDataTypes.Text)]
        //public string OutDeliveryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "indeliveryid", modeltype: FwDataTypes.Text)]
        //public string InDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "receivedeliveryid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverydeliverytype", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryrequireddate", modeltype: FwDataTypes.Date)]
        public string ReceiveDeliveryRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryrequiredtime", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryRequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytargetshipdate", modeltype: FwDataTypes.Date)]
        public string ReceiveDeliveryTargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytargetshiptime", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryTargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromlocation", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcontact", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcontactalternate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcontactphone", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromattention", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromadd1", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromadd2", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcity", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromstate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromzip", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcountry", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcrossstreets", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytolocation", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytoattention", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytoadd1", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytoadd2", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocity", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytostate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytozip", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocountry", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocountryid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocrossstreets", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytometric", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveDeliveryToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocontact", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocontactalternate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocontactphone", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytocontactfax", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverycarrierid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryCarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverycarrier", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryCarrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryshipviaid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryshipvia", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryinvoiceid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryvendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverycarrieracct", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryCarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryestimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? ReceiveDeliveryEstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfreightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? ReceiveDeliveryFreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverychargetype", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfreighttrackno", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytrackingurl", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryfromcountryid", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryFromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverytemplate", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryaddresstype", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverydirection", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryDirection { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveDeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "receivedeliveryorderid", modeltype: FwDataTypes.Text)]
        //public string ReceiveDeliveryOrderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverypackagecode", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryPackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverybillpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveDeliveryBillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryonlineorderno", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryOnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliveryonlineorderstatus", modeltype: FwDataTypes.Text)]
        public string ReceiveDeliveryOnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedeliverydatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string ReceiveDeliveryDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "returndeliveryid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydeliverytype", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryrequireddate", modeltype: FwDataTypes.Date)]
        public string ReturnDeliveryRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryrequiredtime", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryRequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytargetshipdate", modeltype: FwDataTypes.Date)]
        public string ReturnDeliveryTargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytargetshiptime", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryTargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromlocation", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcontact", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcontactalternate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcontactphone", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromattention", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromadd1", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromadd2", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcity", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromstate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromzip", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcountry", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcrossstreets", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytolocation", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytoattention", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytoadd1", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytoadd2", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocity", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytostate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytozip", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocountry", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocountryid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocrossstreets", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytometric", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnDeliveryToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocontact", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocontactalternate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocontactphone", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytocontactfax", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverycarrierid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryCarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverycarrier", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryCarrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryshipviaid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryshipvia", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryinvoiceid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryvendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverycarrieracct", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryCarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryestimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? ReturnDeliveryEstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfreightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? ReturnDeliveryFreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverychargetype", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfreighttrackno", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytrackingurl", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryfromcountryid", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryFromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverytemplate", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryaddresstype", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydirection", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryDirection { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnDeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returndeliveryorderid", modeltype: FwDataTypes.Text)]
        //public string ReturnDeliveryOrderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverypackagecode", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryPackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverybillpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnDeliveryBillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryonlineorderno", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryOnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliveryonlineorderstatus", modeltype: FwDataTypes.Text)]
        public string ReturnDeliveryOnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string ReturnDeliveryDateStamp { get; set; }









        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectdesc", modeltype: FwDataTypes.Text)]
        public string ProjectDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcostobject", modeltype: FwDataTypes.Text)]
        public string Orbitsapcostobject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dateacquired", modeltype: FwDataTypes.Date)]
        public string DateAcquired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "production", modeltype: FwDataTypes.Text)]
        public string Production { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setcharacter", modeltype: FwDataTypes.Text)]
        public string SetCharacter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "setno", modeltype: FwDataTypes.Text)]
        public string Setno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tranactionno", modeltype: FwDataTypes.Text)]
        public string Tranactionno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacture", modeltype: FwDataTypes.Text)]
        public string Manufacture { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
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




        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? RentalTotalIncludesTax { get; set; }
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
        [FwSqlDataField(column: "vehiclediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? VehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labordiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodlabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? LaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------







        [FwSqlDataField(column: "subrentaldaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? SubRentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subrentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SubRentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubrentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubRentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubrentaltotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubRentalTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalesdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubSalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalestotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubSalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subsalestotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? SubSalesTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvehicledaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? SubVehicleDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvehiclediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SubvehicleDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubvehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubVehicleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubvehicletotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubVehicleTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "submiscdiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubMiscDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubmisctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubMiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsubmisctotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubMiscTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sublabordiscpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? SubLaborDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklySubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlySubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsublabortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PeriodSubLaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weeklysublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? WeeklySubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "monthlysublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? MonthlySubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodsublabortotalinctax", modeltype: FwDataTypes.Boolean)]
        public bool? PeriodSubLaborTotalIncludesTax { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "misccomplete", modeltype: FwDataTypes.Boolean)]
        public bool? MiscellaneousIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "submisccomplete", modeltype: FwDataTypes.Boolean)]
        public bool? SubMiscellaneousIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborcomplete", modeltype: FwDataTypes.Boolean)]
        public bool? LaborIsComplete { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sublaborcomplete", modeltype: FwDataTypes.Boolean)]
        public bool? SubLaborIsComplete { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------

        public List<OrderDatesLogic> ActivityDatesAndTimes { get; set; } = new List<OrderDatesLogic>();
        //------------------------------------------------------------------------------------

        public void OnAfterLoad(object sender, AfterLoadEventArgs e)
        {
            if ((e.Record != null) && (e.Record is PurchaseOrderLoader))
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
                ((PurchaseOrderLoader)e.Record).ActivityDatesAndTimes = l.SelectAsync<OrderDatesLogic>(request).Result;
            }
        }
        //------------------------------------------------------------------------------------    

    }
}

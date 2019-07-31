using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace WebApi.Modules.Home.Contract
{
    [FwSqlTable("contractwebview")]
    public class ContractLoader : ContractBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "needreconcile", modeltype: FwDataTypes.Boolean)]
        public bool? NeedReconcile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchange", modeltype: FwDataTypes.Boolean)]
        public bool? PendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangecontractid", modeltype: FwDataTypes.Text)]
        public string ExchangeContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchange", modeltype: FwDataTypes.Boolean)]
        public bool? Exchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyuser", modeltype: FwDataTypes.Text)]
        public string InputByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealinactive", modeltype: FwDataTypes.Boolean)]
        public bool? DealInactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "truck", modeltype: FwDataTypes.Boolean)]
        public bool? Truck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text)]
        public string DeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverydeliverytype", modeltype: FwDataTypes.Text)]
        public string DeliveryDeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryrequireddate", modeltype: FwDataTypes.Date)]
        public string DeliveryRequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryrequiredtime", modeltype: FwDataTypes.Text)]
        public string DeliveryRequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytargetshipdate", modeltype: FwDataTypes.Date)]
        public string DeliveryTargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytargetshiptime", modeltype: FwDataTypes.Text)]
        public string DeliveryTargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromlocation", modeltype: FwDataTypes.Text)]
        public string DeliveryFromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcontact", modeltype: FwDataTypes.Text)]
        public string DeliveryFromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcontactalternate", modeltype: FwDataTypes.Text)]
        public string DeliveryFromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcontactphone", modeltype: FwDataTypes.Text)]
        public string DeliveryFromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string DeliveryFromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromattention", modeltype: FwDataTypes.Text)]
        public string DeliveryFromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromadd1", modeltype: FwDataTypes.Text)]
        public string DeliveryFromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromadd2", modeltype: FwDataTypes.Text)]
        public string DeliveryFromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcity", modeltype: FwDataTypes.Text)]
        public string DeliveryFromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromstate", modeltype: FwDataTypes.Text)]
        public string DeliveryFromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromzip", modeltype: FwDataTypes.Text)]
        public string DeliveryFromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcountry", modeltype: FwDataTypes.Text)]
        public string DeliveryFromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcrossstreets", modeltype: FwDataTypes.Text)]
        public string DeliveryFromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytolocation", modeltype: FwDataTypes.Text)]
        public string DeliveryToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytoattention", modeltype: FwDataTypes.Text)]
        public string DeliveryToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytoadd1", modeltype: FwDataTypes.Text)]
        public string DeliveryToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytoadd2", modeltype: FwDataTypes.Text)]
        public string DeliveryToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocity", modeltype: FwDataTypes.Text)]
        public string DeliveryToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytostate", modeltype: FwDataTypes.Text)]
        public string DeliveryToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytozip", modeltype: FwDataTypes.Text)]
        public string DeliveryToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocountry", modeltype: FwDataTypes.Text)]
        public string DeliveryToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocountryid", modeltype: FwDataTypes.Text)]
        public string DeliveryToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocrossstreets", modeltype: FwDataTypes.Text)]
        public string DeliveryToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytometric", modeltype: FwDataTypes.Boolean)]
        public bool? DeliveryToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocontact", modeltype: FwDataTypes.Text)]
        public string DeliveryToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocontactalternate", modeltype: FwDataTypes.Text)]
        public string DeliveryToAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocontactphone", modeltype: FwDataTypes.Text)]
        public string DeliveryToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string DeliveryToAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytocontactfax", modeltype: FwDataTypes.Text)]
        public string DeliveryToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverydeliverynotes", modeltype: FwDataTypes.Text)]
        public string DeliveryDeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverycarrierid", modeltype: FwDataTypes.Text)]
        public string DeliveryCarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverycarrier", modeltype: FwDataTypes.Text)]
        public string DeliveryCarrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryshipviaid", modeltype: FwDataTypes.Text)]
        public string DeliveryShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryshipvia", modeltype: FwDataTypes.Text)]
        public string DeliveryShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryinvoiceid", modeltype: FwDataTypes.Text)]
        public string DeliveryInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryvendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string DeliveryVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverycarrieracct", modeltype: FwDataTypes.Text)]
        public string DeliveryCarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryestimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? DeliveryEstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfreightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DeliveryFreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverychargetype", modeltype: FwDataTypes.Text)]
        public string DeliveryChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfreighttrackno", modeltype: FwDataTypes.Text)]
        public string DeliveryFreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytrackingurl", modeltype: FwDataTypes.Text)]
        public string DeliveryFreightTrackingUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryfromcountryid", modeltype: FwDataTypes.Text)]
        public string DeliveryFromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytemplate", modeltype: FwDataTypes.Text)]
        public string DeliveryTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryaddresstype", modeltype: FwDataTypes.Text)]
        public string DeliveryAddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverydirection", modeltype: FwDataTypes.Text)]
        public string DeliveryDirection { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? DeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "deliveryorderid", modeltype: FwDataTypes.Text)]
        //public string DeliveryOrderId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverypackagecode", modeltype: FwDataTypes.Text)]
        public string DeliveryPackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverybillpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? DeliveryBillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryonlineorderno", modeltype: FwDataTypes.Text)]
        public string DeliveryOnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryonlineorderstatus", modeltype: FwDataTypes.Text)]
        public string DeliveryOnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverydatestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DeliveryDateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

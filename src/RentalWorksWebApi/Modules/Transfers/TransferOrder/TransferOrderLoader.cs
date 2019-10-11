using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Transfers.TransferOrder
{
    [FwSqlTable("transferwebview")]
    public class TransferOrderLoader : TransferOrderBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromwarehousecode", modeltype: FwDataTypes.Text)]
        public string FromWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "towarehousecode", modeltype: FwDataTypes.Text)]
        public string ToWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
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
    }
}

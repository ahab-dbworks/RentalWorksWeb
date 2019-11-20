using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Delivery
{
    [FwSqlTable("delivery")]
    public class DeliveryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DeliveryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contact", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Contact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrieracct", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)]
        public string CarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverynotes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string DeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedfreight", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? EstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "freightinvamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? FreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargetype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "freighttrackno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 35)]
        public string FreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactfax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromadd1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromadd2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FromAdd2ress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontact", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string FromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromlocation", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromstate", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string FromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromzip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string FromZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "template", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Template { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addresstype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string AddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "direction", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)]
        public string Direction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromattention", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string FromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ondate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string TargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packagecode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbydate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string RequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbytime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactphone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string FromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactalternate", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string AlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphonealternate", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string AlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crossstreets", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string CrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactalternate", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string FromAlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactphonealternate", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string FromAlternateContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcrossstreets", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billpofreightonorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? BillPoFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ontime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string TargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlineorderno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlineorderstatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string OnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

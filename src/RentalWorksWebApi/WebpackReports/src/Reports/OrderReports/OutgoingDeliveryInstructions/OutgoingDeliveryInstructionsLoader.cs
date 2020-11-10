using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.OutgoingDeliveryInstructions
{
    [FwSqlTable("deliveryview")]
    public class OutgoingDeliveryInstructionsLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OutgoingDeliveryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverytype", modeltype: FwDataTypes.Text)]
        public string DeliveryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)]
        public string RequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "targetshipdate", modeltype: FwDataTypes.Date)]
        public string TargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "targetshiptime", modeltype: FwDataTypes.Text)]
        public string TargetShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromlocation", modeltype: FwDataTypes.Text)]
        public string FromLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontact", modeltype: FwDataTypes.Text)]
        public string FromContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactalternate", modeltype: FwDataTypes.Text)]
        public string FromContactAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactphone", modeltype: FwDataTypes.Text)]
        public string FromContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcontactphonealternate", modeltype: FwDataTypes.Text)]
        public string FromContactPhoneAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromattention", modeltype: FwDataTypes.Text)]
        public string FromAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromadd1", modeltype: FwDataTypes.Text)]
        public string FromAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromadd2", modeltype: FwDataTypes.Text)]
        public string FromAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcity", modeltype: FwDataTypes.Text)]
        public string FromCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromstate", modeltype: FwDataTypes.Text)]
        public string FromState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromzip", modeltype: FwDataTypes.Text)]
        public string FromZip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcountry", modeltype: FwDataTypes.Text)]
        public string FromCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcrossstreets", modeltype: FwDataTypes.Text)]
        public string FromCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tolocation", modeltype: FwDataTypes.Text)]
        public string ToLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toattention", modeltype: FwDataTypes.Text)]
        public string ToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toadd1", modeltype: FwDataTypes.Text)]
        public string ToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toadd2", modeltype: FwDataTypes.Text)]
        public string ToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocity", modeltype: FwDataTypes.Text)]
        public string ToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tostate", modeltype: FwDataTypes.Text)]
        public string ToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tozip", modeltype: FwDataTypes.Text)]
        public string ToZip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocountry", modeltype: FwDataTypes.Text)]
        public string ToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocountryid", modeltype: FwDataTypes.Text)]
        public string ToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocrossstreets", modeltype: FwDataTypes.Text)]
        public string ToCrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tometric", modeltype: FwDataTypes.Boolean)]
        public bool? ToMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocontact", modeltype: FwDataTypes.Text)]
        public string ToContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocontactalternate", modeltype: FwDataTypes.Text)]
        public string ToContactAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocontactphone", modeltype: FwDataTypes.Text)]
        public string ToContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocontactphonealternate", modeltype: FwDataTypes.Text)]
        public string ToContactPhoneAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocontactfax", modeltype: FwDataTypes.Text)]
        public string ToContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverynotes", modeltype: FwDataTypes.Text)]
        public string DeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
        public string Carrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text)]
        public string ShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text)]
        public string ShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string DeliverType { get; set; }
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
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contact", modeltype: FwDataTypes.Text)]
        public string Contact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphone", modeltype: FwDataTypes.Text)]
        public string ContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrieracct", modeltype: FwDataTypes.Text)]
        public string CarrierAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedfreight", modeltype: FwDataTypes.Decimal)]
        public decimal? EstimatedFreight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "freightinvamt", modeltype: FwDataTypes.Decimal)]
        public decimal? FreightInvoiceAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargetype", modeltype: FwDataTypes.Text)]
        public string ChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "freighttrackno", modeltype: FwDataTypes.Text)]
        public string FreightTrackingNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackingurl", modeltype: FwDataTypes.Text)]
        public string TrackingURL { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactfax", modeltype: FwDataTypes.Text)]
        public string ContactFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcountryid", modeltype: FwDataTypes.Text)]
        public string FromCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "template", modeltype: FwDataTypes.Text)]
        public string Template { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addresstype", modeltype: FwDataTypes.Text)]
        public string AddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "direction", modeltype: FwDataTypes.Text)]
        public string Direction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Boolean)]
        public bool? Dropship { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ondate", modeltype: FwDataTypes.Date)]
        public string OnDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packagecode", modeltype: FwDataTypes.Text)]
        public string PackageCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactalternate", modeltype: FwDataTypes.Text)]
        public string ContactAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphonealternate", modeltype: FwDataTypes.Text)]
        public string ContactPhoneAlternate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crossstreets", modeltype: FwDataTypes.Text)]
        public string CrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billpofreightonorder", modeltype: FwDataTypes.Boolean)]
        public bool? BillPurchaseOrderFreightOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlineorderno", modeltype: FwDataTypes.Text)]
        public string OnlineOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlineorderstatus", modeltype: FwDataTypes.Text)]
        public string OnlineOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "venueid", modeltype: FwDataTypes.Text)]
        public string VenueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "venue", modeltype: FwDataTypes.Text)]
        public string Venue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(OutgoingDeliveryInstructionsRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhereIn("deliveryId", request.OutgoingDeliveryId);
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

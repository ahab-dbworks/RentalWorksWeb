using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.IncomingDeliveryInstructions
{
    [FwSqlTable("deliverywebview")]
    public class IncomingDeliveryInstructionsLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InDeliveryId { get; set; } = "";
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
        [FwSqlDataField(column: "tometric", modeltype: FwDataTypes.Text)]
        public string ToMetric { get; set; }
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
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentname", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphone", modeltype: FwDataTypes.Text)]
        public string AgentPhone { get; set; }
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
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Decimal)]
        public string WeightPounds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Decimal)]
        public string WeightOunces { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Decimal)]
        public string WeightKilograms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Decimal)]
        public string WeightGrams { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightus", modeltype: FwDataTypes.Text)]
        public string WeightUS { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightmetric", modeltype: FwDataTypes.Text)]
        public string WeightMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightlbs", modeltype: FwDataTypes.Decimal)]
        public string CaseWeightPounds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightoz", modeltype: FwDataTypes.Decimal)]
        public string CaseWeightOunces { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightkg", modeltype: FwDataTypes.Decimal)]
        public string CaseWeightKilograms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightgr", modeltype: FwDataTypes.Decimal)]
        public string CaseWeightGrams { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightus", modeltype: FwDataTypes.Text)]
        public string CaseWeightUS { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caseweightmetric", modeltype: FwDataTypes.Text)]
        public string CaseWeightMetric { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal)]
        public string ManifestValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementvalue", modeltype: FwDataTypes.Decimal)]
        public string ReplacementValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<IncomingDeliveryInstructionsLoader> RunReportAsync(IncomingDeliveryInstructionsRequest request)
        {
            IncomingDeliveryInstructionsLoader report = null;
            Task<IncomingDeliveryInstructionsLoader> taskReport;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.Add("select *                           ");
                    qry.Add(" from  " + TableName + " d         ");
                    qry.Add(" where d.deliveryid = @deliveryid  ");
                    qry.AddParameter("@deliveryid", request.InDeliveryId);
                    AddPropertiesAsQueryColumns(qry);
                    taskReport = qry.QueryToTypedObjectAsync<IncomingDeliveryInstructionsLoader>();
                    await Task.WhenAll(new Task[] { taskReport });
                    report = taskReport.Result;
                }
            }
            return report;
        }
        //------------------------------------------------------------------------------------ 
    }
}

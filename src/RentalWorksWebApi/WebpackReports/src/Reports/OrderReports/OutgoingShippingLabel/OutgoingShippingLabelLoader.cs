using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.OrderReports.OutgoingShippingLabel
{
    [FwSqlTable("outgoingshippinglabelwebview")]
    public class OutgoingShippingLabelLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string DeliverType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addresstype", modeltype: FwDataTypes.Text)]
        public string AddressType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contact", modeltype: FwDataTypes.Text)]
        public string Contact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphone", modeltype: FwDataTypes.Text)]
        public string ContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactalternate", modeltype: FwDataTypes.Text)]
        public string AlternateContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactphonealternate", modeltype: FwDataTypes.Text)]
        public string AlternateContactPhone { get; set; }
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
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "citystatezip", modeltype: FwDataTypes.Text)]
        public string CityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "citystatezipcountry", modeltype: FwDataTypes.Text)]
        public string CityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliverynotes", modeltype: FwDataTypes.Text)]
        public string DeliveryNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crossstreets", modeltype: FwDataTypes.Text)]
        public string CrossStreets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ondate", modeltype: FwDataTypes.Date)]
        public string DeliverOnDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbydate", modeltype: FwDataTypes.Date)]
        public string RequiredByDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredbytime", modeltype: FwDataTypes.Text)]
        public string RequiredByTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
        public string Carrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text)]
        public string ShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(OutgoingShippingLabelRequest request)
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
                    select.AddWhereIn("orderid", request.OrderId); 
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

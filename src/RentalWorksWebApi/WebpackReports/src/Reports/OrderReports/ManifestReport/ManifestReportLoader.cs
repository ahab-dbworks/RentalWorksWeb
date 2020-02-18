using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using FwStandard.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Reports.ManifestReport
{
    [FwSqlTable("dbo.funcvaluesheetweb(@orderid, @rentalvalue, @salesvalue, @filterby, @mode)")]
    public class ManifestReportLoader : AppReportLoader
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
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quantity", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string CountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueperitem", modeltype: FwDataTypes.Decimal)]
        public decimal? ValuePerItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dimensionslwh", modeltype: FwDataTypes.Text)]
        public string DimensionsLWH { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string MfgSerial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string MfgPartNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueextended", modeltype: FwDataTypes.Decimal)]
        public decimal? ValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Integer)]
        public int? WeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Integer)]
        public int? WeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Integer)]
        public int? WeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Integer)]
        public int? WeightGr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightlbs", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightoz", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightkg", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightgr", modeltype: FwDataTypes.Integer)]
        public int? ExtendedWeightGr { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "manifestshippingcontainer", modeltype: FwDataTypes.Boolean)]
        public bool? ManifestShippingContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifeststandaloneitem", modeltype: FwDataTypes.Boolean)]
        public bool? ManifestStandAloneItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordervaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedvaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvaluetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subreplacementtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingcontainertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ShippingContainerTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingitemtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ShippingItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "piececounttotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PieceCountTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "standaloneitemtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? StandAloneItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> GetItemsTable(ManifestReportRequest request)
        {
                useWithNoLock = false;
                FwJsonDataTable dt = null;
                if (request.manifestReportItems == "SUMMARY")
                {
                    request.rentalValueSelector = "REPLACEMENT COST";
                }
                else
                {
                    request.rentalValueSelector = "UNIT VALUE";
                }
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.EnablePaging = false;
                    select.UseOptionRecompile = true;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        SetBaseSelectQuery(select, qry);
                        select.Parse();
                        select.AddParameter("@orderid", request.OrderId);
                        select.AddParameter("@rentalvalue", request.rentalValueSelector);
                        select.AddParameter("@salesvalue", /*request.salesValueSelector*/ "SELL PRICE");
                        select.AddParameter("@filterby", /*request.manifestFilter*/ "ALL");
                        select.AddParameter("@mode", request.manifestReportItems);
                        dt = await qry.QueryToFwJsonTableAsync(select, false);
                    }
                }
            string[] totalFields = new string[] { "ValueExtended", "Quantity", "ExtendedWeightLbs", "ExtendedWeightOz" };
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            return dt;
        }
        //-------------------------------------------------------------------------------------
        public async Task<ManifestHeaderLoader> RunReportAsync(ManifestReportRequest request)
        {
            ManifestHeaderLoader Order = null;
            useWithNoLock = false;
            if (request.manifestReportItems == "SUMMARY")
            {
                request.rentalValueSelector = "REPLACEMENT COST";
            }
            else
            {
                request.rentalValueSelector = "UNIT VALUE";
            }
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetorderprintheader", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<ManifestHeaderLoader> taskOrder = qry.QueryToTypedObjectAsync<ManifestHeaderLoader>();
                    Order = taskOrder.Result;
                }

            }
            Order.ItemsTable = await GetItemsTable(request);
            return Order;
        }
        //------------------------------------------------------------------------------------ 
    }
        public class ManifestHeaderLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentemail", modeltype: FwDataTypes.Text)]
        public string AgentEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locphone", modeltype: FwDataTypes.Text)]
        public string OfficeLocationPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd1", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd2", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCityStateZipCodeCountry { get; set; }
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
        [FwSqlDataField(column: "issuedtophone", modeltype: FwDataTypes.Text)]
        public string IssuedToPhone { get; set; }
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
        [FwSqlDataField(column: "outdeliverycontactphone", modeltype: FwDataTypes.Text)]
        public string OutDeliveryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdeliverycitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string OutDeliveryCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagedates", modeltype: FwDataTypes.Text)]
        public string UsageDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        public string BillingDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        public FwJsonDataTable ItemsTable { get; set; }
    }
}

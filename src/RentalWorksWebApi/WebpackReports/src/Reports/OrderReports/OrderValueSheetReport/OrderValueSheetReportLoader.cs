using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using FwStandard.Data;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace WebApi.Modules.Reports.OrderValueSheetReport
{
    [FwSqlTable("dbo.funcvaluesheetweb(@orderid, @rentalvalue, @salesvalue, @filterby, @mode)")]
    public class OrderValueSheetReportItemLoader : AppReportLoader
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
        public string Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public string SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string CountryOfOrigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueperitem", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string ValuePerItem { get; set; }
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
        [FwSqlDataField(column: "valueextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string ValueExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Integer)]
        public string WeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Integer)]
        public string WeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Integer)]
        public string WeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Integer)]
        public string WeightGr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightlbs", modeltype: FwDataTypes.Integer)]
        public string ExtendedWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightoz", modeltype: FwDataTypes.Integer)]
        public string ExtendedWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightkg", modeltype: FwDataTypes.Integer)]
        public string ExtendedWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightgr", modeltype: FwDataTypes.Integer)]
        public string ExtendedWeightGr { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "manifestshippingcontainer", modeltype: FwDataTypes.Boolean)]
        public bool? OrderValueSheetShippingContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifeststandaloneitem", modeltype: FwDataTypes.Boolean)]
        public bool? OrderValueSheetStandAloneItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordervaluetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string OrderValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderreplacementtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string OrderReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedvaluetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string OwnedValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedreplacementtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string OwnedReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvaluetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string SubValueTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subreplacementtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string SubReplacementTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingcontainertotal", modeltype: FwDataTypes.Decimal)]
        public string ShippingContainerTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shippingitemtotal", modeltype: FwDataTypes.Decimal)]
        public string ShippingItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "piececounttotal", modeltype: FwDataTypes.Decimal)]
        public string PieceCountTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "standaloneitemtotal", modeltype: FwDataTypes.Decimal)]
        public string StandAloneItemTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<List<OrderValueSheetReportItemLoader>> LoadItems(OrderValueSheetReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
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
                    select.AddParameter("@rentalvalue", request.RentalValue);
                    select.AddParameter("@salesvalue", /*request.salesValueSelector*/ "SELL PRICE");
                    select.AddParameter("@filterby", /*request.manifestFilter*/ "ALL");
                    select.AddParameter("@mode", request.IsSummary.GetValueOrDefault(false) ? "SUMMARY" : "DETAIL");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            string[] totalFields = new string[] { "Quantity", "ExtendedWeightLbs", "ExtendedWeightOz", "ValueExtended", };
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            List<OrderValueSheetReportItemLoader> items = new List<OrderValueSheetReportItemLoader>();
            foreach (List<object> row in dt.Rows)
            {
                OrderValueSheetReportItemLoader item = (OrderValueSheetReportItemLoader)Activator.CreateInstance(typeof(OrderValueSheetReportItemLoader));
                PropertyInfo[] properties = item.GetType().GetProperties();

                bool isSubOrGrandTotalRow = (row[dt.GetColumnNo("RowType")].ToString().Equals("grandtotal") || row[dt.GetColumnNo("RowType")].ToString().Equals("RecTypeDisplayfooter"));
                string currencySymbol = dt.Rows[0][dt.GetColumnNo("CurrencySymbol")].ToString();
                string currencyCode = dt.Rows[0][dt.GetColumnNo("CurrencyCode")].ToString();
                //string currencyId = dt.Rows[0][dt.GetColumnNo("CurrencyId")].ToString();
                //string officeLocationDefaultCurrencyId = "";//dt.Rows[0][dt.GetColumnNo("OfficeLocationDefaultCurrencyId")].ToString();
                //bool isForeignCurrency = false;// ((!string.IsNullOrEmpty(currencyId)) && (!currencyId.Equals(officeLocationDefaultCurrencyId)));

                foreach (var property in properties)
                {
                    string fieldName = property.Name;
                    int columnIndex = dt.GetColumnNo(fieldName);
                    if (!columnIndex.Equals(-1))
                    {
                        object value = row[dt.GetColumnNo(fieldName)];
                        FwDataTypes propType = dt.Columns[columnIndex].DataType;
                        bool isDecimal = false;
                        NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;

                        // we need the 8-digit precision for summing above. But now that we have our sums, we need to go back down to 2-digit display
                        if (propType.Equals(FwDataTypes.DecimalString8Digits))
                        {
                            propType = FwDataTypes.DecimalString2Digits;
                        }

                        FwSqlCommand.FwDataTypeIsDecimal(propType, value, ref isDecimal, ref numberFormat);
                        if (isDecimal)
                        {
                            decimal d = FwConvert.ToDecimal((value ?? "0").ToString());
                            string stringValue = d.ToString("N", numberFormat);
                            if (isSubOrGrandTotalRow)
                            {
                                stringValue = "(" + currencyCode + ") " + currencySymbol + " " + stringValue;
                            }
                            property.SetValue(item, stringValue);
                        }
                        else if (propType.Equals(FwDataTypes.Boolean))
                        {
                            property.SetValue(item, FwConvert.ToBoolean((value ?? "").ToString()));
                        }
                        else
                        {
                            property.SetValue(item, (value ?? "").ToString());
                        }

                    }
                }
                items.Add(item);
            }

            return items;

        }
        //-------------------------------------------------------------------------------------
    }

    public class OrderValueSheetReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
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
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
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
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        public List<OrderValueSheetReportItemLoader> Items { get; set; } = new List<OrderValueSheetReportItemLoader>(new OrderValueSheetReportItemLoader[] { new OrderValueSheetReportItemLoader() });
        //------------------------------------------------------------------------------------
        public async Task<OrderValueSheetReportLoader> RunReportAsync(OrderValueSheetReportRequest request)
        {
            OrderValueSheetReportLoader Order = null;
            useWithNoLock = false;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetorderprintheader", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<OrderValueSheetReportLoader> taskOrder = qry.QueryToTypedObjectAsync<OrderValueSheetReportLoader>();
                    Order = taskOrder.Result;
                }

                //items
                Task<List<OrderValueSheetReportItemLoader>> taskItems;
                OrderValueSheetReportItemLoader Items = new OrderValueSheetReportItemLoader();
                Items.SetDependencies(AppConfig, UserSession);
                taskItems = Items.LoadItems(request);
                Order.Items = taskItems.Result;
            }
            return Order;
        }
        //------------------------------------------------------------------------------------ 
    }
}

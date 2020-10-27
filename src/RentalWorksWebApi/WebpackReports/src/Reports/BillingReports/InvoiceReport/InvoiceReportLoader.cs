using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Reports.Billing.InvoiceReport
{

    public class InvoiceItemReportLoader : AppReportLoader
    {
        protected string recType = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
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
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public string Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitextended", modeltype: FwDataTypes.DecimalString2Digits)]
        public string UnitExtended { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "extendednodisc", modeltype: FwDataTypes.DecimalString2Digits)]
        public string GrossExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendednodiscsubtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string GrossExtendedSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? HasDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamtsubtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DiscountAmountSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedsubtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string ExtendedSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpctsubtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DiscountPercentSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromtime", modeltype: FwDataTypes.Text)]
        public string FromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totime", modeltype: FwDataTypes.Text)]
        public string ToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? HasRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? IsRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string TaxNoCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Tax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Tax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxsubtotal", modeltype: FwDataTypes.DecimalString8Digits)]
        public string TaxSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedwithtax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string ExtendedWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedwithtaxsubtotal", modeltype: FwDataTypes.DecimalString8Digits)]
        public string ExtendedWithTaxSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextended", modeltype: FwDataTypes.DecimalString8Digits)]
        public string TotalExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalextendedwithtax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string TotalExtendedWithTax { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------

        public async Task<List<T>> LoadItems<T>(InvoiceReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetinvoiceprintdetails", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.InvoiceId);
                    qry.AddParameter("@rectype", SqlDbType.Text, ParameterDirection.Input, recType);
                    qry.AddParameter("@summary", SqlDbType.Text, ParameterDirection.Input, request.IsSummary);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
            string[] totalFields = new string[] { "GrossExtended", "GrossExtendedSubTotal", "DiscountAmount", "DiscountAmountSubTotal", "Extended", "ExtendedSubTotal", "TaxNoCurrency", "Tax", "Tax1", "Tax2", "TaxSubTotal", "ExtendedWithTax", "TotalExtended", "TotalExtendedWithTax" };
            dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalFields, nameHeaderColumns: new string[] { "TaxRate1", "TaxRate2", "CurrencyId", "OfficeLocationDefaultCurrencyId", "CurrencyCode", "CurrencySymbol" }, includeGroupColumnValueInFooter: true, totalFor: "");
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            List<T> items = new List<T>();
            bool hasDiscount = false;
            bool hasRecurring = false;
            foreach (List<object> row in dt.Rows)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = item.GetType().GetProperties();

                bool isSubOrGrandTotalRow = (row[dt.GetColumnNo("RowType")].ToString().Equals("grandtotal") || row[dt.GetColumnNo("RowType")].ToString().Equals("RecTypeDisplayfooter"));
                string currencySymbol = dt.Rows[0][dt.GetColumnNo("CurrencySymbol")].ToString();
                string currencyCode = dt.Rows[0][dt.GetColumnNo("CurrencyCode")].ToString();
                string currencyId = dt.Rows[0][dt.GetColumnNo("CurrencyId")].ToString();
                string officeLocationDefaultCurrencyId = dt.Rows[0][dt.GetColumnNo("OfficeLocationDefaultCurrencyId")].ToString();
                bool isForeignCurrency = ((!string.IsNullOrEmpty(currencyId)) && (!currencyId.Equals(officeLocationDefaultCurrencyId)));

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
                            if ((isSubOrGrandTotalRow) && (!fieldName.Contains("TaxRate")) && (!fieldName.Equals("TaxNoCurrency")))
                            {
                                stringValue = (isForeignCurrency ? "(" + currencyCode + ") " : "") + currencySymbol + " " + stringValue;
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

                        if (fieldName.Equals("HasDiscount"))
                        {
                            if (value != null)
                            {
                                if (value.Equals(true))
                                {
                                    hasDiscount = true;
                                    items[0].GetType().GetProperty("HasDiscount").SetValue(items[0], true);
                                }
                            }
                            else
                            {
                                item.GetType().GetProperty("HasDiscount").SetValue(item, hasDiscount);
                            }
                        }

                        if (fieldName.Equals("IsRecurring"))
                        {
                            if (value != null)
                            {
                                if (value.Equals(true))
                                {
                                    hasRecurring = true;
                                    items[0].GetType().GetProperty("HasRecurring").SetValue(items[0], true);
                                }
                            }
                            else
                            {
                                item.GetType().GetProperty("HasRecurring").SetValue(item, hasRecurring);
                            }
                        }
                    }
                }
                items.Add(item);
            }
            return items;
        }
        //------------------------------------------------------------------------------------ 
    }

    //------------------------------------------------------------------------------------ 
    public class RentalInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public RentalInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_RENTAL;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.DecimalString3Digits)]
        public string DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableperiods", modeltype: FwDataTypes.DecimalString2Digits)]
        public string BillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class SalesInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public SalesInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_SALE;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class MiscInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public MiscInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_MISCELLANEOUS;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.DecimalString3Digits)]
        public string DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableperiods", modeltype: FwDataTypes.DecimalString2Digits)]
        public string BillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class LaborInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public LaborInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_LABOR;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.DecimalString3Digits)]
        public string DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableperiods", modeltype: FwDataTypes.DecimalString2Digits)]
        public string BillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OverTimeHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DoubleTimeHours { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class AdjustmentInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public AdjustmentInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_ADJUSTMENT;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class LossAndDamageInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public LossAndDamageInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_LOSS_AND_DAMAGE;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderno", modeltype: FwDataTypes.Text)]
        public string OriginalRentalOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderdesc", modeltype: FwDataTypes.Text)]
        public string OriginalRentalOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldordernodesc", modeltype: FwDataTypes.Text)]
        public string OriginalRentalOrderNumberAndDescription { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class UsedSaleInvoiceItemReportLoader : InvoiceItemReportLoader
    {
        public UsedSaleInvoiceItemReportLoader()
        {
            recType = RwConstants.RECTYPE_USED_SALE;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class PaymentsInvoiceItemReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentdate", modeltype: FwDataTypes.Date)]
        public string PaymentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PayType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Amount { get; set; }
        //------------------------------------------------------------------------------------

        public async Task<List<T>> LoadPaymentItems<T>(InvoiceReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select rowtype = 'detail', v.paymentdate, v.paytype, v.checkno, v.amount");
                    qry.Add(" from  invoicepaymentview v  with (nolock)");
                    qry.Add(" where v.invoiceid = @invoiceid");
                    qry.Add(" and v.amount <> 0");
                    qry.Add(" order by v.paymentdate");
                    qry.AddParameter("@invoiceid", request.InvoiceId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
            string[] totalFields = new string[] { "Amount" };
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            List<T> items = new List<T>();
            foreach (List<object> row in dt.Rows)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = item.GetType().GetProperties();
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
                            property.SetValue(item, d.ToString("N", numberFormat));
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
    }
    //------------------------------------------------------------------------------------ 



    public class InvoiceNoteReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notedesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<List<T>> LoadItems<T>(InvoiceReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetinvoiceprintnotes", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.InvoiceId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;

            List<T> items = new List<T>();
            foreach (List<object> row in dt.Rows)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = item.GetType().GetProperties();
                foreach (var property in properties)
                {
                    string fieldName = property.Name;
                    int columnIndex = dt.GetColumnNo(fieldName);
                    if (!columnIndex.Equals(-1))
                    {
                        object value = row[dt.GetColumnNo(fieldName)];
                        property.SetValue(item, (value ?? "").ToString());
                    }
                }
                items.Add(item);
            }

            return items;
        }
        //------------------------------------------------------------------------------------ 
    }



    public class InvoiceReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceduedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
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
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custphone", modeltype: FwDataTypes.Text)]
        public string CustomerPhone { get; set; }
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
        [FwSqlDataField(column: "dealphone", modeltype: FwDataTypes.Text)]
        public string DealPhone { get; set; }
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
        [FwSqlDataField(column: "billcountryid", modeltype: FwDataTypes.Text)]
        public string BillToCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcitystatezip", modeltype: FwDataTypes.Text)]
        public string BillToCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcitystatezipcounrty", modeltype: FwDataTypes.Text)]
        public string BillToCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernodesc", modeltype: FwDataTypes.Text)]
        public string OrderNumberAndDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
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
        //[FwSqlDataField(column: "estrentfromdatetime", modeltype: FwDataTypes.Text)]
        //public string EstimatedStartDateTime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "estrenttodatetime", modeltype: FwDataTypes.Text)]
        //public string EstimatedStopDateTime { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagestart", modeltype: FwDataTypes.Date)]
        public string UsageStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usageend", modeltype: FwDataTypes.Date)]
        public string UsageEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usagedates", modeltype: FwDataTypes.Text)]
        //public string UsageDates { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        //public string BillingDates { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
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
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
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
        public string TermsAndConditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsfilename", modeltype: FwDataTypes.Text)]
        public string TermsAndConditionsFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsnewpage", modeltype: FwDataTypes.Boolean)]
        public bool? TermsAndConditionsNewPage { get; set; }
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
        //[FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Text)]
        //public string SummaryInvoiceGroup { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "coverletterfilename", modeltype: FwDataTypes.Text)]
        public string CoverLetterFileName { get; set; }
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
        public string Outdeliverycitystatezipcountry { get; set; }
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
        //[FwSqlDataField(column: "outdeliveryrequiredbydatetime", modeltype: FwDataTypes.Text)]
        //public string OutDeliveryRequiredByDateTime { get; set; }
        ////------------------------------------------------------------------------------------ 
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
        [FwSqlDataField(column: "remittocompany", modeltype: FwDataTypes.Text)]
        public string RemitToCompany { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittoadd1", modeltype: FwDataTypes.Text)]
        public string RemitToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittoadd2", modeltype: FwDataTypes.Text)]
        public string RemitToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittoadd1and2", modeltype: FwDataTypes.Text)]
        public string RemitToAddress1AndAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittocitystatezip", modeltype: FwDataTypes.Text)]
        public string RemitToCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittocountry", modeltype: FwDataTypes.Text)]
        public string RemitToCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittocitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string RemitToCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittofulladdress", modeltype: FwDataTypes.Text)]
        public string RemitToFullAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittophone", modeltype: FwDataTypes.Text)]
        public string RemitToPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittofax", modeltype: FwDataTypes.Text)]
        public string RemitToFax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittofedid", modeltype: FwDataTypes.Text)]
        public string RemitToFederalId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "remittoemail", modeltype: FwDataTypes.Text)]
        public string RemitToEmail { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbycontactid", modeltype: FwDataTypes.Text)]
        public string OrderedByContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbycompcontactid", modeltype: FwDataTypes.Text)]
        public string OrderedByCompanyContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedByName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyphone", modeltype: FwDataTypes.Text)]
        public string OrderedByPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyext", modeltype: FwDataTypes.Text)]
        public string OrderedByExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyphoneext", modeltype: FwDataTypes.Text)]
        public string OrderedByPhoneAndExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbyemail", modeltype: FwDataTypes.Text)]
        public string OrderedByEmail { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderedbytitle", modeltype: FwDataTypes.Text)]
        public string OrderedByTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax1name", modeltype: FwDataTypes.Text)]
        public string Tax1Name { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2name", modeltype: FwDataTypes.Text)]
        public string Tax2Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRentalRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRentalRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxSalesRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxSalesRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxLaborRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxLaborRate2 { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "nontaxcertificateno", modeltype: FwDataTypes.Text)]
        public string NonTaxableCertificateNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1referencename", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax1referenceno", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2referencename", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tax2referenceno", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "hastax", modeltype: FwDataTypes.Boolean)]
        public bool? HasTax { get; set; }
        //------------------------------------------------------------------------------------


        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receiptstotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string ReceiptsTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remainingbalance", modeltype: FwDataTypes.DecimalString2Digits)]
        public string RemainingBalance { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicemessage", modeltype: FwDataTypes.Text)]
        public string OfficeLocationInvoiceMessage { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "multipleactivities", modeltype: FwDataTypes.Boolean)]
        public bool? HasMultipleActivities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "hasrecurringitems", modeltype: FwDataTypes.Boolean)]
        public bool? HasRecurring { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------
        public List<RentalInvoiceItemReportLoader> RentalItems { get; set; } = new List<RentalInvoiceItemReportLoader>(new RentalInvoiceItemReportLoader[] { new RentalInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<SalesInvoiceItemReportLoader> SalesItems { get; set; } = new List<SalesInvoiceItemReportLoader>(new SalesInvoiceItemReportLoader[] { new SalesInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<MiscInvoiceItemReportLoader> MiscItems { get; set; } = new List<MiscInvoiceItemReportLoader>(new MiscInvoiceItemReportLoader[] { new MiscInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<LaborInvoiceItemReportLoader> LaborItems { get; set; } = new List<LaborInvoiceItemReportLoader>(new LaborInvoiceItemReportLoader[] { new LaborInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<UsedSaleInvoiceItemReportLoader> UsedSaleItems { get; set; } = new List<UsedSaleInvoiceItemReportLoader>(new UsedSaleInvoiceItemReportLoader[] { new UsedSaleInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<LossAndDamageInvoiceItemReportLoader> LossAndDamageItems { get; set; } = new List<LossAndDamageInvoiceItemReportLoader>(new LossAndDamageInvoiceItemReportLoader[] { new LossAndDamageInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<AdjustmentInvoiceItemReportLoader> AdjustmentItems { get; set; } = new List<AdjustmentInvoiceItemReportLoader>(new AdjustmentInvoiceItemReportLoader[] { new AdjustmentInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<InvoiceItemReportLoader> Items { get; set; } = new List<InvoiceItemReportLoader>(new InvoiceItemReportLoader[] { new InvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<PaymentsInvoiceItemReportLoader> PaymentItems { get; set; } = new List<PaymentsInvoiceItemReportLoader>(new PaymentsInvoiceItemReportLoader[] { new PaymentsInvoiceItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<InvoiceNoteReportLoader> Notes { get; set; } = new List<InvoiceNoteReportLoader>(new InvoiceNoteReportLoader[] { new InvoiceNoteReportLoader() });
        //------------------------------------------------------------------------------------ 



        public async Task<InvoiceReportLoader> RunReportAsync(InvoiceReportRequest request)
        {
            InvoiceReportLoader Invoice = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetinvoiceprintheader", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.InvoiceId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<InvoiceReportLoader> taskInvoice = qry.QueryToTypedObjectAsync<InvoiceReportLoader>();

                    //all items
                    Task<List<InvoiceItemReportLoader>> taskInvoiceItems;
                    InvoiceItemReportLoader InvoiceItems = new InvoiceItemReportLoader();
                    InvoiceItems.SetDependencies(AppConfig, UserSession);
                    taskInvoiceItems = InvoiceItems.LoadItems<InvoiceItemReportLoader>(request);

                    //rental items
                    Task<List<RentalInvoiceItemReportLoader>> taskRentalInvoiceItems;
                    RentalInvoiceItemReportLoader RentalItems = new RentalInvoiceItemReportLoader();
                    RentalItems.SetDependencies(AppConfig, UserSession);
                    taskRentalInvoiceItems = RentalItems.LoadItems<RentalInvoiceItemReportLoader>(request);

                    //sales items
                    Task<List<SalesInvoiceItemReportLoader>> taskSalesInvoiceItems;
                    SalesInvoiceItemReportLoader SalesItems = new SalesInvoiceItemReportLoader();
                    SalesItems.SetDependencies(AppConfig, UserSession);
                    taskSalesInvoiceItems = SalesItems.LoadItems<SalesInvoiceItemReportLoader>(request);

                    //misc items
                    Task<List<MiscInvoiceItemReportLoader>> taskMiscInvoiceItems;
                    MiscInvoiceItemReportLoader MiscItems = new MiscInvoiceItemReportLoader();
                    MiscItems.SetDependencies(AppConfig, UserSession);
                    taskMiscInvoiceItems = MiscItems.LoadItems<MiscInvoiceItemReportLoader>(request);

                    //labor items
                    Task<List<LaborInvoiceItemReportLoader>> taskLaborInvoiceItems;
                    LaborInvoiceItemReportLoader LaborItems = new LaborInvoiceItemReportLoader();
                    LaborItems.SetDependencies(AppConfig, UserSession);
                    taskLaborInvoiceItems = LaborItems.LoadItems<LaborInvoiceItemReportLoader>(request);

                    //loss and damage items
                    Task<List<LossAndDamageInvoiceItemReportLoader>> taskLossAndDamageInvoiceItems;
                    LossAndDamageInvoiceItemReportLoader LossAndDamageItems = new LossAndDamageInvoiceItemReportLoader();
                    LossAndDamageItems.SetDependencies(AppConfig, UserSession);
                    taskLossAndDamageInvoiceItems = LossAndDamageItems.LoadItems<LossAndDamageInvoiceItemReportLoader>(request);

                    //used sale items
                    Task<List<UsedSaleInvoiceItemReportLoader>> taskUsedSaleInvoiceItems;
                    UsedSaleInvoiceItemReportLoader UsedSaleItems = new UsedSaleInvoiceItemReportLoader();
                    UsedSaleItems.SetDependencies(AppConfig, UserSession);
                    taskUsedSaleInvoiceItems = UsedSaleItems.LoadItems<UsedSaleInvoiceItemReportLoader>(request);

                    //labor items
                    Task<List<AdjustmentInvoiceItemReportLoader>> taskAdjustmentInvoiceItems;
                    AdjustmentInvoiceItemReportLoader AdjustmentItems = new AdjustmentInvoiceItemReportLoader();
                    AdjustmentItems.SetDependencies(AppConfig, UserSession);
                    taskAdjustmentInvoiceItems = AdjustmentItems.LoadItems<AdjustmentInvoiceItemReportLoader>(request);

                    //payment items
                    Task<List<PaymentsInvoiceItemReportLoader>> taskPaymentInvoiceItems;
                    PaymentsInvoiceItemReportLoader PaymentItems = new PaymentsInvoiceItemReportLoader();
                    PaymentItems.SetDependencies(AppConfig, UserSession);
                    taskPaymentInvoiceItems = PaymentItems.LoadPaymentItems<PaymentsInvoiceItemReportLoader>(request);

                    //notes
                    Task<List<InvoiceNoteReportLoader>> taskNotesItems;
                    InvoiceNoteReportLoader NotesItems = new InvoiceNoteReportLoader();
                    NotesItems.SetDependencies(AppConfig, UserSession);
                    taskNotesItems = NotesItems.LoadItems<InvoiceNoteReportLoader>(request);

                    await Task.WhenAll(new Task[] { taskInvoice, taskInvoiceItems, taskRentalInvoiceItems, taskSalesInvoiceItems, taskMiscInvoiceItems, taskLaborInvoiceItems, taskLossAndDamageInvoiceItems, taskUsedSaleInvoiceItems, taskAdjustmentInvoiceItems, taskPaymentInvoiceItems, taskNotesItems });

                    Invoice = taskInvoice.Result;

                    if (Invoice != null)
                    {
                        Invoice.Items = taskInvoiceItems.Result;
                        Invoice.RentalItems = taskRentalInvoiceItems.Result;
                        Invoice.SalesItems = taskSalesInvoiceItems.Result;
                        Invoice.MiscItems = taskMiscInvoiceItems.Result;
                        Invoice.LaborItems = taskLaborInvoiceItems.Result;
                        Invoice.LossAndDamageItems = taskLossAndDamageInvoiceItems.Result;
                        Invoice.UsedSaleItems = taskUsedSaleInvoiceItems.Result;
                        Invoice.AdjustmentItems = taskAdjustmentInvoiceItems.Result;
                        Invoice.PaymentItems = taskPaymentInvoiceItems.Result;
                        Invoice.Notes = taskNotesItems.Result;
                    }
                }
            }


            /////////////////////////////////////////////////////////


            //Invoice.DateFields = new List<string>();
            //PropertyInfo[] properties = Invoice.GetType().GetProperties();
            //foreach (PropertyInfo property in properties)
            //{
            //    if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
            //    {
            //        FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
            //        if (sqlDataFieldAttribute.ModelType.Equals(FwDataTypes.Date))
            //        {
            //            Invoice.DateFields.Add(property.Name);
            //        }
            //    }
            //}
            //

            /////////////////////////////////////////////////////////



            return Invoice;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.HomeControls.SubPurchaseOrderItem
{
    [FwSqlTable("dbo.funcsubworksheetweb(@sessionid)")]
    public class SubPurchaseOrderItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public SubPurchaseOrderItemLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor
        {
            get { return getICodeColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nodiscount", modeltype: FwDataTypes.Boolean)]
        public bool? NonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? IsRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorateweeks", modeltype: FwDataTypes.Boolean)]
        public bool? ProrateWeeks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "proratemonths", modeltype: FwDataTypes.Boolean)]
        public bool? ProrateMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorate", modeltype: FwDataTypes.Boolean)]
        public bool? Prorate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "proratemonthsby", modeltype: FwDataTypes.Text)]
        public string ProrateMonthsBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recurringratetype", modeltype: FwDataTypes.Boolean)]
        public bool? RecurringRateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iscrewpositionhourly", modeltype: FwDataTypes.Boolean)]
        public bool? IsCrewPositionHourly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Decimal)]
        public decimal? Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.Decimal)]
        public decimal? OverTimeHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.Decimal)]
        public decimal? DoubleTimeHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorrate", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordw", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendordiscountpctdisplay", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorDiscountPercentDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorbillableperiods", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorweeklysubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorWeeklySubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorweeklydiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorWeeklyDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorweeklytax", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorWeeklyTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorweeklytotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorWeeklyTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendormonthlysubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorMonthlySubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendormonthlydiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorMonthlyDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendormonthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendormonthlytax", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorMonthlyTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendormonthlytotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorMonthlyTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorperiodsubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorPeriodSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorperioddiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorPeriodDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorperiodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorperiodtax", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorPeriodTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorperiodtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorPeriodTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DealRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdw", modeltype: FwDataTypes.Decimal)]
        public decimal? DealDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DealDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdiscountpctdisplay", modeltype: FwDataTypes.Decimal)]
        public decimal? DealDiscountPercentDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealbillableperiods", modeltype: FwDataTypes.Decimal)]
        public decimal? DealBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealweeklysubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? DealWeeklySubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealweeklydiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? DealWeeklyDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? DealWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealmonthlysubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? DealMonthlySubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealmonthlydiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? DealMonthlyDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealmonthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? DealMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealperiodsubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? DealPeriodSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealperioddiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? DealPeriodDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealperiodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? DealPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "variance", modeltype: FwDataTypes.Decimal)]
        public decimal? Variance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VarianceColor
        {
            get { return getVarianceColor(Variance); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accratio", modeltype: FwDataTypes.Decimal)]
        public decimal? AccessoryRatio { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorcurrencyid", modeltype: FwDataTypes.Text)]
        public string VendorCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcurrencyid", modeltype: FwDataTypes.Text)]
        public string DealCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyexchangerate", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyconvertedrate", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyConvertedRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyconvertedweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyConvertedWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyconvertedmonthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyConvertedMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyconvertedperiodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrencyConvertedPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        private string getICodeColor(string itemClass)
        {
            return AppFunc.GetItemClassICodeColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getDescriptionColor(string itemClass)
        {
            return AppFunc.GetItemClassDescriptionColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getVarianceColor(decimal? variance)
        {
            return (variance < 0 ? RwGlobals.NEGATIVE_VARIANCE_COLOR : null);
        }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string sessionId = SessionId;
            useWithNoLock = false;

            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = GetUniqueIdAsString("SessionId", request);
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = "~xx~";
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            select.AddParameter("@sessionid", sessionId);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("ICodeColor")] = getICodeColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("VarianceColor")] = getVarianceColor(FwConvert.ToDecimal(row[dt.GetColumnNo("Variance")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;

namespace WebApi.Modules.Billing.Billing
{
    [FwSqlTable("dbo.funcbilling(@sessionid)")]
    public class BillingLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        public BillingLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tmpbillingid", modeltype: FwDataTypes.Integer)]
        public int? BillingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text)]
        public string FlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpobillschedid", modeltype: FwDataTypes.Text)]
        public string FlatPoBillingScheduleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "supplementalpoid", modeltype: FwDataTypes.Text)]
        public string SupplementalPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordtype", modeltype: FwDataTypes.Text)]
        public string OrderTypeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billstartdate", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billstopdate", modeltype: FwDataTypes.Date)]
        public string BillingStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billasof", modeltype: FwDataTypes.Date)]
        public string BillAsOfDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? IsRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean)]
        public bool? IsFlatPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popending", modeltype: FwDataTypes.Boolean)]
        public bool? PendingPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poamount", modeltype: FwDataTypes.Decimal)]
        public decimal? PoAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingPeriodStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingPeriodEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "validchargeno", modeltype: FwDataTypes.Boolean)]
        public bool? Validchargeno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgmajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgsub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgdeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text)]
        public string Orbitsapchgset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingnotes", modeltype: FwDataTypes.Boolean)]
        public bool? BillingNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "worksheetid", modeltype: FwDataTypes.Text)]
        public string WorksheetId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodevent", modeltype: FwDataTypes.Text)]
        public string BillingCycleEvent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodeventorder", modeltype: FwDataTypes.Integer)]
        public int? BillingCycleEventOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer)]
        public int? SummaryInvoiceGroup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donotinvoice", modeltype: FwDataTypes.Boolean)]
        public bool? DoNotInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "episodeno", modeltype: FwDataTypes.Integer)]
        public int? EpisodeNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "finalld", modeltype: FwDataTypes.Boolean)]
        public bool? IsFinalLossAndDamage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? BillingTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? HasRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? BilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingcrewbreaktime", modeltype: FwDataTypes.Boolean)]
        public bool? MissingCrewBreakTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingcrewworktime", modeltype: FwDataTypes.Boolean)]
        public bool? MissingCrewWorkTime { get; set; }
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
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderNumberColor
        {
            get { return getOrderNumberColor(IsNoCharge); }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderDescriptionColor
        {
            get { return getOrderDescriptionColor(IsRepair, IsFinalLossAndDamage); }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BillingStopDateColor
        {
            get { return getBillingStopDateColor(BillingPeriodEndDate, BillingStopDate); }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderDateColor
        {
            get { return getOrderDateColor(BillingNotes); }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor
        {
            get { return getPurchaseOrderNumberColor(IsFlatPo, PendingPo); }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string TotalColor
        {
            get { return getTotalColor(BilledHiatus); }
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string sessionId = GetUniqueIdAsString("SessionId", request) ?? "";
            useWithNoLock = false;
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
                        row[dt.GetColumnNo("OrderNumberColor")] = getOrderNumberColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsNoCharge")].ToString()));
                        row[dt.GetColumnNo("OrderDescriptionColor")] = getOrderDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsRepair")].ToString()), FwConvert.ToBoolean(row[dt.GetColumnNo("IsFinalLossAndDamage")].ToString()));
                        row[dt.GetColumnNo("BillingStopDateColor")] = getBillingStopDateColor(row[dt.GetColumnNo("BillingPeriodEndDate")].ToString(), row[dt.GetColumnNo("BillingStopDate")].ToString());
                        row[dt.GetColumnNo("OrderDateColor")] = getOrderDateColor(FwConvert.ToBoolean(row[dt.GetColumnNo("BillingNotes")].ToString()));
                        row[dt.GetColumnNo("PurchaseOrderNumberColor")] = getPurchaseOrderNumberColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsFlatPo")].ToString()), FwConvert.ToBoolean(row[dt.GetColumnNo("PendingPo")].ToString()));
                        row[dt.GetColumnNo("TotalColor")] = getTotalColor(FwConvert.ToBoolean(row[dt.GetColumnNo("BilledHiatus")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
        protected string getOrderNumberColor(bool? isNoCharge)
        {
            string color = null;
            if (isNoCharge.GetValueOrDefault(false))
            {
                color = RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getOrderDescriptionColor(bool? isRepair, bool? isLossAndDamage)
        {
            string color = null;
            if (isRepair.GetValueOrDefault(false))
            {
                color = RwGlobals.ORDER_REPAIR_COLOR;
            }
            else if (isLossAndDamage.GetValueOrDefault(false))
            {
                color = RwGlobals.ORDER_LOSS_AND_DAMAGE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getBillingStopDateColor(string billingPeriodEndDate, string billingStopDate)
        {
            string color = null;
            if (!string.IsNullOrEmpty(billingPeriodEndDate))
            {
                if (FwConvert.ToDateTime(billingPeriodEndDate) < FwConvert.ToDateTime(billingStopDate))
                {
                    color = "#00FF00";
                }
            }
           
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getOrderDateColor(bool? billingNotes)
        {
            string color = null;
            if (billingNotes.GetValueOrDefault(false))
            {
                color = "#00FFFF";
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getPurchaseOrderNumberColor(bool? isFlatPo, bool? isPendingPo)
        {
            string color = null;
            if (isFlatPo.GetValueOrDefault(false))
            {
                color = RwGlobals.INVOICE_FLAT_PO_COLOR;
            }
            else if (isPendingPo.GetValueOrDefault(false))
            {
                color = RwGlobals.ORDER_PENDING_PO_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getTotalColor(bool? billedHiatus)
        {
            string color = null;
            if (billedHiatus.GetValueOrDefault(false))
            {
                color = "#00B95C";
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}

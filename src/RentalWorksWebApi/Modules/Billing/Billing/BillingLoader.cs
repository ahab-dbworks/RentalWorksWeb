using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Billing.Billing
{
    [FwSqlTable("dbo.funcbilling(@sessionid)")]
    public class BillingLoader : AppDataLoadRecord
    {
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
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "ordernocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstopcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BillingStopDateColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdatecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderDateColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ponocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string TotalColor { get; set; }
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
    }
}

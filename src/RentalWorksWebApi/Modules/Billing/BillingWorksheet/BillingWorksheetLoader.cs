using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Billing.BillingWorksheet
{
    [FwSqlTable("invoiceworksheetview")]
    public class BillingWorksheetLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagestart", modeltype: FwDataTypes.Date)]
        public string UsageStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usageend", modeltype: FwDataTypes.Date)]
        public string UsageEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "worksheetinvno", modeltype: FwDataTypes.Text)]
        public string ResultingInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "worksheetinvid", modeltype: FwDataTypes.Text)]
        public string ResultingInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string BillingWorksheetId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string WorksheetNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string WorksheetDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceduedate", modeltype: FwDataTypes.Date)]
        public string Invoiceduedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string Invoicetype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
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
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string WorksheetDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
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
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1name", modeltype: FwDataTypes.Text)]
        public string Tax1Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2name", modeltype: FwDataTypes.Text)]
        public string Tax2Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusted", modeltype: FwDataTypes.Boolean)]
        public bool? IsAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? IsBilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haslockedtotal", modeltype: FwDataTypes.Boolean)]
        public bool? HasLockedTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "altereddates", modeltype: FwDataTypes.Boolean)]
        public bool? IsAlteredDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text)]
        public string FlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "worksheettotal", modeltype: FwDataTypes.Decimal)]
        public decimal? WorksheetTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? ResultingInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
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
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);

            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            if (!string.IsNullOrEmpty(orderId))
            {
                select.AddWhere("exists (select * from orderinvoice oi where oi.invoiceid = " + TableAlias + ".invoiceid and oi.orderid = @orderid)");
                select.AddParameter("@orderid", orderId);
            }

            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            if (!string.IsNullOrEmpty(inventoryId))
            {
                select.AddWhere("exists (select * from invoiceitem ii where ii.invoiceid = " + TableAlias + ".invoiceid and ii.masterid = @masterid)");
                select.AddParameter("@masterid", inventoryId);
            }

            AddActiveViewFieldToSelect("Status", "status", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}

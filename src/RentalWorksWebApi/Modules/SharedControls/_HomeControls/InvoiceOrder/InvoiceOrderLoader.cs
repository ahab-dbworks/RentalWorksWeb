using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.InvoiceOrder
{
    [FwSqlTable("orderinvoiceview")]
    public class InvoiceOrderLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoiceid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderInvoiceId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text)]
        public string FlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludefromflat", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeFromFlatPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableflat", modeltype: FwDataTypes.Boolean)]
        public bool? BillableFlat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summaryinvoiceorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? SummaryInvoiceOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborsubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonrentalsubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? NonRentalSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicesubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderInvoiceSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicetax", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderInvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InvoiceId", "invoiceid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

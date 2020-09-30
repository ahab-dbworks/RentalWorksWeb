using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.InvoiceBatch
{
    [FwSqlTable("invoicechgbatchview")]
    public class InvoiceBatchLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text)]
        public string BatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
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
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string BatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchdate", modeltype: FwDataTypes.Date)]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
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
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departemnt", modeltype: FwDataTypes.Text)]
        public string Departemnt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptcode", modeltype: FwDataTypes.Text)]
        public string DepartmentCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InvoiceId", "invoiceid", select, request); 
            addFilterToSelect("BatchId", "chgbatchid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

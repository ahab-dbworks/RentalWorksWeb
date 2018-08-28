using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Reports.CreateInvoiceProcessReport
{
    [FwSqlTable("billingprocessrptview")]
    public class CreateInvoiceProcessReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text)]
        public string BatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchno", modeltype: FwDataTypes.Integer)]
        public int? BatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchdate", modeltype: FwDataTypes.Date)]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
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
        [FwSqlDataField(column: "billingnote", modeltype: FwDataTypes.Text)]
        public string BillingNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exceptionflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsException { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exceptionmsg", modeltype: FwDataTypes.Text)]
        public string ExceptionMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priorinvoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PriorInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicecount", modeltype: FwDataTypes.Integer)]
        public int? InvoiceCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exceptioncount", modeltype: FwDataTypes.Integer)]
        public int? ExceptionCount { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string batchId = ""; 
            bool exceptionsOnly = false;

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("BatchId"))
                {
                    batchId = uniqueIds["BatchId"].ToString();
                }
                if (uniqueIds.ContainsKey("ExceptionsOnly"))
                {
                    exceptionsOnly = FwConvert.ToBoolean(uniqueIds["ExceptionsOnly"].ToString());
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BatchId", "invoicebatchid", select, request);
            if (exceptionsOnly)
            {
                select.AddWhere("(exceptionflg = 'T')"); 
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

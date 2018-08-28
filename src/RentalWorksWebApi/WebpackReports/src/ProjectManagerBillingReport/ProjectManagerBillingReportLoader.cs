using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;

namespace WebApi.Modules.Reports.ProjectManagerBillingReport
{
    [FwSqlTable("agentbillingview")]
    public class ProjectManagerBillingReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customertype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingnote", modeltype: FwDataTypes.Text)]
        public string BillingNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text)]
        public string WorkAuthorizationNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupno", modeltype: FwDataTypes.Text)]
        public string GroupNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastbatchno", modeltype: FwDataTypes.Text)]
        public string LastBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MeterTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscellaneousTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assettotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AssetTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? IsNonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string dateType = "";
            string dateField = "";
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("FromDate"))
                {
                    fromDate = FwConvert.ToDateTime(uniqueIds["FromDate"].ToString());
                }
                if (uniqueIds.ContainsKey("ToDate"))
                {
                    toDate = FwConvert.ToDateTime(uniqueIds["ToDate"].ToString());
                }
                if (uniqueIds.ContainsKey("DateType"))
                {
                    dateType = uniqueIds["DateType"].ToString();
                }
            }

            dateField = "invoicedate";
            if (dateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
            {
                dateField = "billingstart";
            }


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("ProjectManagerId", "projectmanagerid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);

            if (fromDate != DateTime.MinValue)
            {
                select.AddWhere(dateField + " >= @fromdate");
                select.AddParameter("@fromdate", fromDate);
            }

            if (toDate != DateTime.MinValue)
            {
                select.AddWhere(dateField + " <= @todate");
                select.AddParameter("@todate", toDate);
            }


        }
        //------------------------------------------------------------------------------------ 
    }
}

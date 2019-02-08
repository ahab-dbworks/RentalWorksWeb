using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Home.Invoice
{
    [FwSqlTable("invoicewebbrowseview")]
    public class InvoiceBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InvoiceId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
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
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ponocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor { get; set; }
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
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text)]
        public string InvoiceCreationBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicenocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InvoiceNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DealColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesccolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstartdatecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BillingStartDateColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotalcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InvoiceTotalColor { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);
            addFilterToSelect("InvoiceCreationBatchId", "invoicebatchid", select, request);
            //addFilterToSelect("OrderId", "orderid", select, request);

            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";

            if (!string.IsNullOrEmpty(orderId))
            {
                select.AddWhere("exists (select * from orderinvoice oi where oi.invoiceid = " + TableAlias + ".invoiceid and oi.orderid = @orderid)");
                select.AddParameter("@orderid", orderId);
            }

            if ((request != null) && (request.activeview != null))
            {
                List<string> activeView = new List<string>(request.activeview.Split(','));

                foreach (string s in activeView)
                {
                    if (s.Contains("Status="))
                    {
                        string status = s.Replace("Status=", "").Trim();

                        switch (status)
                        {
                            case "NEW":
                                select.AddWhere("(status = @invoicestatus)");
                                select.AddParameter("@invoicestatus", RwConstants.INVOICE_STATUS_NEW);
                                break;
                            case "APPROVED":
                                select.AddWhere("(status = @invoicestatus)");
                                select.AddParameter("@invoicestatus", RwConstants.INVOICE_STATUS_APPROVED);
                                break;
                            case "NEWAPPROVED":
                                select.AddWhere("(status in (@invoicestatus0, @invoicestatus1))");
                                select.AddParameter("@invoicestatus0", RwConstants.INVOICE_STATUS_NEW);
                                select.AddParameter("@invoicestatus1", RwConstants.INVOICE_STATUS_APPROVED);
                                break;
                            case "PROCESSED":
                                select.AddWhere("(status = @invoicestatus)");
                                select.AddParameter("@invoicestatus", RwConstants.INVOICE_STATUS_PROCESSED);
                                break;
                            case "CLOSED":
                                select.AddWhere("(status = @invoicestatus)");
                                select.AddParameter("@invoicestatus", RwConstants.INVOICE_STATUS_CLOSED);
                                break;
                            case "VOID":
                                select.AddWhere("(status = @invoicestatus)");
                                select.AddParameter("@invoicestatus", RwConstants.INVOICE_STATUS_VOID);
                                break;
                            case "ALL":
                                break;
                        }
                    }

                    if (s.Contains("OfficeLocationId=") || s.Contains("LocationId="))
                    {
                        string locId = "ALL";
                        if (s.Contains("OfficeLocationId="))
                        {
                            locId = s.Replace("OfficeLocationId=", "").Trim();
                        }
                        else if (s.Contains("LocationId="))
                        {
                            locId = s.Replace("LocationId=", "").Trim();
                        }
                        if (!locId.Equals("ALL"))
                        {
                            select.AddWhere("(locationid = @locid)");
                            select.AddParameter("@locid", locId);
                        }
                    }
                }
            }
        }
    }
    //------------------------------------------------------------------------------------    
}

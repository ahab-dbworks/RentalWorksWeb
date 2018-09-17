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
    [FwSqlTable("invoiceview")]
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
        public string PoNumber { get; set; }
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);

            if ((request != null) && (request.activeview != null))
            {
                //    switch (request.activeview)
                //    {
                //        case "PROSPECT":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_PROSPECT);
                //            break;
                //        case "RESERVED":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_RESERVED);
                //            break;
                //        case "CONFIRMED":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CONFIRMED);
                //            break;
                //        case "HOLD":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_HOLD);
                //            break;
                //        case "ORDERED":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_ORDERED);
                //            break;
                //        case "ACTIVE":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_ACTIVE);
                //            break;
                //        case "COMPLETE":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_COMPLETE);
                //            break;
                //        case "CLOSED":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CLOSED);
                //            break;
                //        case "CANCELLED":
                //            select.AddWhere("(status = @orderstatus)");
                //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CANCELLED);
                //            break;
                //        case "ALL":
                //            break;
                //    }

                string locId = "ALL";
                if (request.activeview.Contains("OfficeLocationId="))
                {
                    locId = request.activeview.Replace("OfficeLocationId=", "");
                }
                else if (request.activeview.Contains("LocationId="))
                {
                    locId = request.activeview.Replace("LocationId=", "");
                }
                if (!locId.Equals("ALL"))
                {
                    select.AddWhere("(locationid = @locid)");
                    select.AddParameter("@locid", locId);
                }
            }

        }
    }
    //------------------------------------------------------------------------------------    
}

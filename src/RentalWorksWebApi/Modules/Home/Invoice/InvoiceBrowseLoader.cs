using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Home.Invoice
{
    [FwSqlTable("invoicewebbrowseview2")]
    public class InvoiceBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public InvoiceBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
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
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
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
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
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
        [FwSqlDataField(column: "orderhaslockedtotal", modeltype: FwDataTypes.Boolean)]
        public bool? OrderHasLockedTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? BilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasrepairitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasRepairItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haslditem", modeltype: FwDataTypes.Boolean)]
        public bool? HasLossAndDamageItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isflatpo", modeltype: FwDataTypes.Boolean)]
        public bool? IsFlatPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor
        {
            get { return getPurchaseOrderNumberColor(IsFlatPo.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InvoiceNumberColor
        {
            get { return getInvoiceNumberColor(InvoiceType, IsAdjusted.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor
        {
            get { return getStatusColor(InvoiceType); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OrderNumberColor
        {
            get { return getOrderNumberColor(OrderHasLockedTotal.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DealColor
        {
            get { return getDealColor(BilledHiatus.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(HasRepairItem.GetValueOrDefault(false), HasLossAndDamageItem.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BillingStartDateColor
        {
            get { return getBillingStartDateColor(IsAlteredDates.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InvoiceTotalColor
        {
            get { return getInvoiceTotalColor(IsNoCharge.GetValueOrDefault(false)); }
            set { }
        }
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
        private string getPurchaseOrderNumberColor(bool isFlatPo)
        {
            string color = null;
            if (isFlatPo)
            {
                color = RwGlobals.INVOICE_FLAT_PO_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getInvoiceNumberColor(string invoiceType, bool adjusted)
        {
            string color = null;
            if (invoiceType.Equals(RwConstants.INVOICE_TYPE_CREDIT))
            {
                color = RwGlobals.INVOICE_CREDIT_COLOR;
            }
            else if (adjusted)
            {
                color = RwGlobals.INVOICE_ADJUSTED_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getStatusColor(string invoiceType)
        {
            string color = null;
            if (invoiceType.Equals(RwConstants.INVOICE_TYPE_ESTIMATE))
            {
                color = RwGlobals.INVOICE_ESTIMATE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getOrderNumberColor(bool orderHasLockedTotal)
        {
            string color = null;
            if (orderHasLockedTotal)
            {
                color = RwGlobals.INVOICE_LOCKED_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getDealColor(bool billedHiatus)
        {
            string color = null;
            if (billedHiatus)
            {
                color = RwGlobals.INVOICE_HIATUS_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getDescriptionColor(bool hasRepairItem, bool hasLdItem)
        {
            string color = null;
            if (hasRepairItem)
            {
                color = RwGlobals.INVOICE_REPAIR_COLOR;
            }
            else if (hasLdItem)
            {
                color = RwGlobals.INVOICE_LOSS_AND_DAMAGE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getBillingStartDateColor(bool alteredDates)
        {
            string color = null;
            if (alteredDates)
            {
                color = RwGlobals.INVOICE_ALTERED_DATES_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------    
        private string getInvoiceTotalColor(bool noCharge)
        {
            string color = null;
            if (noCharge)
            {
                color = RwGlobals.INVOICE_NO_CHARGE_COLOR;
            }
            return color;
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
                        //row[dt.GetColumnNo("NumberColor")] = getNumberColor(row[dt.GetColumnNo("Type")].ToString(), row[dt.GetColumnNo("Status")].ToString(), row[dt.GetColumnNo("EstimatedStopDate")].ToString());
                        //row[dt.GetColumnNo("WarehouseColor")] = getWarehouseColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsMultiWarehouse")].ToString()));
                        row[dt.GetColumnNo("PurchaseOrderNumberColor")] = getPurchaseOrderNumberColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsFlatPo")].ToString()));
                        row[dt.GetColumnNo("InvoiceNumberColor")] = getInvoiceNumberColor(row[dt.GetColumnNo("InvoiceType")].ToString(), FwConvert.ToBoolean(row[dt.GetColumnNo("IsAdjusted")].ToString()));
                        row[dt.GetColumnNo("StatusColor")] = getStatusColor(row[dt.GetColumnNo("InvoiceType")].ToString());
                        row[dt.GetColumnNo("OrderNumberColor")] = getOrderNumberColor(FwConvert.ToBoolean(row[dt.GetColumnNo("OrderHasLockedTotal")].ToString()));
                        row[dt.GetColumnNo("DealColor")] = getDealColor(FwConvert.ToBoolean(row[dt.GetColumnNo("BilledHiatus")].ToString()));
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("HasRepairItem")].ToString()), FwConvert.ToBoolean(row[dt.GetColumnNo("HasLossAndDamageItem")].ToString()));
                        row[dt.GetColumnNo("BillingStartDateColor")] = getBillingStartDateColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsAlteredDates")].ToString()));
                        row[dt.GetColumnNo("InvoiceTotalColor")] = getInvoiceTotalColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsNoCharge")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
    }
}

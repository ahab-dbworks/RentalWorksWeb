using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Reports.SubRentalBillingAnalysisReport
{
    public class SubRentalBillingAnalysisReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
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
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
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
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poitemid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text)]
        public string PoClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text)]
        public string PoClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "postatus", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemdw", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorItemDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtaxable", modeltype: FwDataTypes.Boolean)]
        public bool? VendorItemTaxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorItemTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorItemTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtaxrule", modeltype: FwDataTypes.Boolean)]
        public bool? VendorItemTaxRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? NoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isflatpoactual", modeltype: FwDataTypes.Boolean)]
        public bool? IsFlatPoActual { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebillingstart", modeltype: FwDataTypes.Date)]
        public string InvoiceBillingStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebillingend", modeltype: FwDataTypes.Date)]
        public string InvoiceBillingEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicestatus", modeltype: FwDataTypes.Text)]
        public string InvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdescription", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealitemfromdate", modeltype: FwDataTypes.Date)]
        public string DealItemFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealitemtodate", modeltype: FwDataTypes.Date)]
        public string DealItemToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealitemdw", modeltype: FwDataTypes.Decimal)]
        public decimal? DealItemDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealitemdiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DealItemDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvno", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvdate", modeltype: FwDataTypes.Date)]
        public string VendorInvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorbillingstart", modeltype: FwDataTypes.Date)]
        public string VendorBillingStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorbillingend", modeltype: FwDataTypes.Date)]
        public string VendorBillingEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceitemid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorunitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorqty", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemfromdate", modeltype: FwDataTypes.Date)]
        public string VendorItemFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtodate", modeltype: FwDataTypes.Date)]
        public string VendorItemToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemtax", modeltype: FwDataTypes.Decimal)]
        public decimal? VendorItemTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itembilled", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemvariance", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ItemVariance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealitemdaterange", modeltype: FwDataTypes.Text)]
        public string DealItemDateRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoritemdaterange", modeltype: FwDataTypes.Text)]
        public string VendorItemDateRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndate", modeltype: FwDataTypes.Date)]
        public string ReturnDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstartdate", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(SubRentalBillingAnalysisReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getsrbillinganalysisrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@postatus", SqlDbType.Text, ParameterDirection.Input, request.PurchaseOrderStatus.ToString());
                    qry.AddParameter("@invoicestatus", SqlDbType.Text, ParameterDirection.Input, request.InvoiceStatus.ToString());
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@vendorid", SqlDbType.Text, ParameterDirection.Input, request.VendorId);
                    qry.AddParameter("@poclassificationid", SqlDbType.Text, ParameterDirection.Input, request.PoClassificationId);
                    qry.AddParameter("@poid", SqlDbType.Text, ParameterDirection.Input, request.PurchaseOrderId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    qry.AddParameter("@includevendortax", SqlDbType.Text, ParameterDirection.Input, request.ToDate);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "ItemBilled", "ItemCost" , "ItemVariance"};
                string[] headerFieldsPurchaseOrder = new string[] { "OrderNumber", "OrderDate", "OrderDescription", "PurchaseOrderNumber", "PurchaseOrderDate", "PoClassification", "Vendor", "ReceiveDate", "ReturnDate" };
                string[] headerFieldsInvoice = new string[] { "InvoiceNumber", "InvoiceDate", "InvoiceBillingStart", "InvoiceBillingEnd", "VendorInvoiceNumber", "VendorInvoiceDate", "VendorBillingStart", "VendorBillingEnd" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertSubTotalRows("PurchaseOrderNumber", "RowType", totalFields, headerFieldsPurchaseOrder,  totalFor: "Total for PO");
                dt.InsertSubTotalRows("InvoiceNumber", "RowType", totalFields, headerFieldsInvoice, totalFor: "Total for Order");
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

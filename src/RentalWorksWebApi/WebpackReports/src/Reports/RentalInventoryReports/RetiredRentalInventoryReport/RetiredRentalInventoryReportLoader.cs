using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using WebApi;

namespace WebApi.Modules.Reports.RentalInventoryReports.RetiredRentalInventoryReport
{
    public class RetiredRentalInventoryReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredid", modeltype: FwDataTypes.Text)]
        public string RetiredId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retireddate", modeltype: FwDataTypes.Date)]
        public string RetireDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasevendorid", modeltype: FwDataTypes.Text)]
        public string PurchaseVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasevendor", modeltype: FwDataTypes.Text)]
        public string PurchaseVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepoid", modeltype: FwDataTypes.Text)]
        public string PurchasePurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasepono", modeltype: FwDataTypes.Text)]
        public string PurchasePurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PurchaseAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencyid", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrencycode", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasecurrency", modeltype: FwDataTypes.Text)]
        public string PurchaseCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonid", modeltype: FwDataTypes.Text)]
        public string RetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason", modeltype: FwDataTypes.Text)]
        public string RetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reasontype", modeltype: FwDataTypes.Text)]
        public string RetiredReasonType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtybar", modeltype: FwDataTypes.Text)]
        public string QuantityOrBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredbyusersid", modeltype: FwDataTypes.Text)]
        public string RetiredByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredby", modeltype: FwDataTypes.Text)]
        public string RetiredBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string SoldToCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string SoldToCustomer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string SoldToDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string SoldToDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string SoldToOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string SoldToOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtoorderid", modeltype: FwDataTypes.Text)]
        public string BilledToOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtomasteritemid", modeltype: FwDataTypes.Text)]
        public string BilledToOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtoorderno", modeltype: FwDataTypes.Text)]
        public string BilledToOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtoinvoiceid", modeltype: FwDataTypes.Text)]
        public string BilledToInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtoinvoiceno", modeltype: FwDataTypes.Text)]
        public string BilledToInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? BilledAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredid", modeltype: FwDataTypes.Text)]
        public string UnretireId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretireddate", modeltype: FwDataTypes.Date)]
        public string UnretireDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredbyusersid", modeltype: FwDataTypes.Text)]
        public string UnretiredByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredby", modeltype: FwDataTypes.Text)]
        public string UnretiredBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonid", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason", modeltype: FwDataTypes.Text)]
        public string UnretiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isfinalld", modeltype: FwDataTypes.Boolean)]
        public bool? IsFinalLossAndDamage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isrentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? IsRentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retirednotes", modeltype: FwDataTypes.Text)]
        public string RetiredNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RetiredRentalInventoryReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getretireditemsrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@availfor", SqlDbType.Text, ParameterDirection.Input, RwConstants.INVENTORY_AVAILABLE_FOR_RENT);
                    qry.AddParameter("@retiredfromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@retiredtodate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@includeunretired", SqlDbType.Text, ParameterDirection.Input, request.IncludeUnretired);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@retiredreasonid", SqlDbType.Text, ParameterDirection.Input, request.RetiredReasonId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "Quantity" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

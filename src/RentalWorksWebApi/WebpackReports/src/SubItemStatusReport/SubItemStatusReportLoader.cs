using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.SubItemStatusReport
{
    [FwSqlTable("getsubitemstatus")]
    public class SubItemStatusReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernodesc", modeltype: FwDataTypes.Text)]
        public string OrderNumberDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderagentid", modeltype: FwDataTypes.Text)]
        public string OrderAgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderagent", modeltype: FwDataTypes.Text)]
        public string OrderAgent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderestrentfrom", modeltype: FwDataTypes.Date)]
        public string Orderestrentfrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderestrentto", modeltype: FwDataTypes.Date)]
        public string Orderestrentto { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderestrentperiod", modeltype: FwDataTypes.Text)]
        public string Orderestrentperiod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillperiodstart", modeltype: FwDataTypes.Date)]
        public string OrderBillingPeriodStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillperiodend", modeltype: FwDataTypes.Date)]
        public string OrderBillingPeriodEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderratetype", modeltype: FwDataTypes.Text)]
        public string OrderRateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderhiatusdiscfrom", modeltype: FwDataTypes.Text)]
        public string OrderHiatusDiscFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillperiodid", modeltype: FwDataTypes.Text)]
        public string OrderBillingPeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillperiodtype", modeltype: FwDataTypes.Text)]
        public string OrderBillingPeriodType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedLasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowarehouseid", modeltype: FwDataTypes.Text)]
        public string ReturnToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowarehouse", modeltype: FwDataTypes.Text)]
        public string ReturnToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returntowhcode", modeltype: FwDataTypes.Text)]
        public string ReturnWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? IsRRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorateweeks", modeltype: FwDataTypes.Boolean)]
        public bool? ProRateWeeks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderrecurringratetype", modeltype: FwDataTypes.Boolean)]
        public bool? OrderRecurringRateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderrate", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdw", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderqtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderQuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderqtycoefficient", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderQuantityCoefficient { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderWeeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordermonthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillableperiods", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderperiodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderMasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ponodesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumberDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "postatus", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poestrentfrom", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderEstrentFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poestrentto", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderEstrentTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poestrentperiod", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderEstrentPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillperiodstart", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderBillingPeriodStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillperiodend", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderBillingPeriodEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poratetype", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderRateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pohiatusdiscfrom", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderHiatusDiscFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillperiodid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderBillingPeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillperiodtype", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderBillingPeriodType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderClassificationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "porecurringratetype", modeltype: FwDataTypes.Boolean)]
        public bool? Porecurringratetype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "porate", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podw", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poqtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderQuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poqtycoefficient", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderQuantityCoefficient { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poweeklyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderweeklyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomonthlyextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderMonthlyExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillableperiods", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderBillablePeriods { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poperiodextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PurchaseOrderPeriodExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profit", modeltype: FwDataTypes.Decimal)]
        public decimal? Profit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receiveestimated", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveEstimated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuspendreceive", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendReceive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdate", modeltype: FwDataTypes.Date)]
        public string OutDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outestimated", modeltype: FwDataTypes.Boolean)]
        public bool? OutEstimated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuspendout", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indate", modeltype: FwDataTypes.Date)]
        public string InDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inestimated", modeltype: FwDataTypes.Boolean)]
        public bool? InEstimated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuspendin", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lost", modeltype: FwDataTypes.Boolean)]
        public bool? Lost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndate", modeltype: FwDataTypes.Date)]
        public string ReturnDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnestimated", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnEstimated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuspendreturn", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendReturn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualreceivedate", modeltype: FwDataTypes.Date)]
        public string ActualReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualoutdate", modeltype: FwDataTypes.Date)]
        public string ActualOutDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualindate", modeltype: FwDataTypes.Date)]
        public string ActualInDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actualreturndate", modeltype: FwDataTypes.Date)]
        public string ActualReturnDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text)]
        public string BillingDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractrentaldate", modeltype: FwDataTypes.Date)]
        public string ReceiveContractRentalDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outcontractrentaldate", modeltype: FwDataTypes.Date)]
        public string OutContractRentalDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractrentaldate", modeltype: FwDataTypes.Date)]
        public string InContractRentalDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returncontractrentaldate", modeltype: FwDataTypes.Date)]
        public string ReturnContractRentalDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillableperiodsfrom", modeltype: FwDataTypes.Date)]
        public string OrderBillablePeriodsFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderbillableperiodsto", modeltype: FwDataTypes.Date)]
        public string OrderBillablePeriodsTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillableperiodsfrom", modeltype: FwDataTypes.Date)]
        public string PoBillablePeriodsFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pobillableperiodsto", modeltype: FwDataTypes.Date)]
        public string PoBillablePeriodsTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(SubItemStatusReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "procedurename", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@vendorid", SqlDbType.Text, ParameterDirection.Input, request.VendorId);
                    qry.AddParameter("@poclassificationid", SqlDbType.Text, ParameterDirection.Input, request.PoClassificationId);

                    qry.AddParameter("@datetouse", SqlDbType.Text, ParameterDirection.Input, request.DateType); // needs more here
                    qry.AddParameter("@status", SqlDbType.Text, ParameterDirection.Input, request.Statuses.ToString());
                    qry.AddParameter("@rectype", SqlDbType.Text, ParameterDirection.Input, request.RecType.ToString());
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "OrderQuantityOrdered", "OrderPeriodExtended", "OrderPeriodExtended", "Profit" };
                string[] headerFieldsOrder = new string[] { "OrderDescription", "Deal", "OrderDate", "OrderPeriodStart", "OrderPeriodEnd" };
                string[] headerFieldsPurchaseOrder = new string[] { "PurchaseOrderDescription", "Vendor", "PurchaseOrderClassification", "PurchaseOrderPeriodStart", "PurchaseOrderPeriodEnd" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Order", "RowType", totalFields);
                dt.InsertSubTotalRows("PurchaseOrderNumber", "RowType", totalFields, headerFieldsPurchaseOrder, totalFor: "Total for PO");
                dt.InsertSubTotalRows("Order", "RowType", totalFields, headerFieldsOrder, totalFor: "Total for Order");
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

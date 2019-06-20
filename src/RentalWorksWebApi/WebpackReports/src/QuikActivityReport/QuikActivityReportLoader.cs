using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Reports.QuikActivityReport
{
    [FwSqlTable("getquikactivityrptweb(@fromdate, @todate)")]
    public class QuikActivityReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydesc", modeltype: FwDataTypes.Text)]
        public string ActivityDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydate", modeltype: FwDataTypes.Date)]
        public string ActivityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytime", modeltype: FwDataTypes.Text)]
        public string ActivityTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityorder", modeltype: FwDataTypes.Integer)]
        public int? ActivityOrder { get; set; }
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
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype2", modeltype: FwDataTypes.Text)]
        public string OrderType2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemcount", modeltype: FwDataTypes.Integer)]
        public int? ItemCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subitemcount", modeltype: FwDataTypes.Integer)]
        public int? SubItemCount { get; set; }
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
        [FwSqlDataField(column: "dealvendorno", modeltype: FwDataTypes.Text)]
        public string DealVendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealvendor", modeltype: FwDataTypes.Text)]
        public string DealVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "milestone", modeltype: FwDataTypes.Boolean)]
        public bool? Milestone { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(QuikActivityReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getquikactivityrptweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@summary", SqlDbType.Text, ParameterDirection.Input, request.IsSummary);
                    qry.AddParameter("@ordertype", SqlDbType.Text, ParameterDirection.Input, request.OrderTypes.ToString());
                    qry.AddParameter("@activitytype", SqlDbType.Text, ParameterDirection.Input, request.ActivityType);
                    qry.AddParameter("@includeinuse", SqlDbType.Text, ParameterDirection.Input, request.IncludeInUse);
                    qry.AddParameter("@onlysubs", SqlDbType.Text, ParameterDirection.Input, request.OnlySubs);
                    qry.AddParameter("@rectype", SqlDbType.Text, ParameterDirection.Input, request.RecType);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@agentid", SqlDbType.Text, ParameterDirection.Input, request.AgentId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "ItemCount", "SubItemCount" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

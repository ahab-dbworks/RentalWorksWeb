using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryMovementReport
{
    [FwSqlTable("dbo.getinventorymovementrpt(@fromdate, @todate, @includezeroowned, @value, @warehouseid, @rank, @trackedby, @inventorydepartmentid, @categoryid, @subcategoryid, @masterid)")]
    public class RentalInventoryMovementReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        //public string RowType { get; set; }
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
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
        public bool? Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpurchase", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LastPurchase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedfromdate", modeltype: FwDataTypes.Integer)]
        public int? OwnedFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostfromdate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchased", modeltype: FwDataTypes.Integer)]
        public int? Purchased { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retired", modeltype: FwDataTypes.Integer)]
        public int? Retired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretired", modeltype: FwDataTypes.Integer)]
        public int? Unretired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferredout", modeltype: FwDataTypes.Integer)]
        public int? TransferredOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferredin", modeltype: FwDataTypes.Integer)]
        public int? TransferredIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedtodate", modeltype: FwDataTypes.Integer)]
        public int? OwnedToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcosttodate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostchange", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCostChange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason001", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason001 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname001", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName001 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason002", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason002 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname002", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName002 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason003", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason003 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname003", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName003 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason004", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason004 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname004", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName004 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason005", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason005 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname005", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName005 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason006", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason006 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname006", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName006 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason007", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason007 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname007", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName007 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason008", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason008 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname008", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName008 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason009", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason009 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname009", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName009 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason010", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason010 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname010", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName010 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason011", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason011 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname011", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName011 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason012", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason012 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname012", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName012 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason013", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason013 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname013", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName013 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason014", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason014 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname014", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName014 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason015", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason015 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname015", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName015 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason016", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason016 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname016", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName016 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason017", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason017 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname017", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName017 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason018", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason018 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname018", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName018 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason019", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason019 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname019", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName019 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreason020", modeltype: FwDataTypes.Integer)]
        public int? RetiredReason020 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retiredreasonname020", modeltype: FwDataTypes.Text)]
        public string RetiredReasonName020 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason001", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason001 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname001", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName001 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason002", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason002 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname002", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName002 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason003", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason003 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname003", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName003 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason004", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason004 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname004", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName004 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason005", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason005 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname005", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName005 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason006", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason006 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname006", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName006 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason007", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason007 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname007", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName007 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason008", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason008 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname008", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName008 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason009", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason009 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname009", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName009 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason010", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason010 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname010", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName010 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason011", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason011 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname011", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName011 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason012", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason012 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname012", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName012 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason013", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason013 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname013", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName013 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason014", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason014 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname014", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName014 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason015", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason015 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname015", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName015 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason016", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason016 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname016", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName016 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason017", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason017 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname017", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName017 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason018", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason018 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname018", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName018 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason019", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason019 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname019", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName019 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreason020", modeltype: FwDataTypes.Integer)]
        public int? UnretiredReason020 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unretiredreasonname020", modeltype: FwDataTypes.Text)]
        public string UnretiredReasonName020 { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryMovementReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventorymovementrpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@includezeroowned", SqlDbType.Text, ParameterDirection.Input, request.IncludeZeroOwned);
                    qry.AddParameter("@value", SqlDbType.Text, ParameterDirection.Input, request.Value);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@rank", SqlDbType.Text, ParameterDirection.Input, request.Ranks.ToString());
                    qry.AddParameter("@trackedby", SqlDbType.Text, ParameterDirection.Input, request.TrackedBys.ToString());
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "OwnedFromDate", "ReplacementCostFromDate", "Purchased", "Retired", "Unretired", "TransferredOut", "TransferredIn", "OwnedToDate", "ReplacementCostToDate" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

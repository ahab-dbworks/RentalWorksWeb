using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryStatusAndRevenueReport
{
    [FwSqlTable("dbo.funcrentalinventorystatusandrevenueweb(@revenuefromdate, @revenuetodate)")]
    public class RentalInventoryStatusAndRevenueReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtype", modeltype: FwDataTypes.Text)]
        public string RecordType { get; set; }
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
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
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
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyonpo", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOnPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysubbed", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantitySubbed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastrenteddate", modeltype: FwDataTypes.Date)]
        public string LastRentedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Revenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutqty", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedOutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutstatus", modeltype: FwDataTypes.Text)]
        public string StagedOutStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutdeal", modeltype: FwDataTypes.Text)]
        public string StagedOutDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutvendor", modeltype: FwDataTypes.Text)]
        public string StagedOutVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutbarcode", modeltype: FwDataTypes.Text)]
        public string StagedOutBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutasof", modeltype: FwDataTypes.Date)]
        public string StagedOutAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutorderno", modeltype: FwDataTypes.Text)]
        public string StagedOutOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutorderdesc", modeltype: FwDataTypes.Text)]
        public string StagedOutOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasstagedoutdetails", modeltype: FwDataTypes.Boolean)]
        public bool? HasStagedOutDetails { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryStatusAndRevenueReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddParameter("@revenuefromdate", request.RevenueFromDate);
                    select.AddParameter("@revenuetodate", request.RevenueToDate);
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("rank", request.Ranks);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    if (!request.IncludeZeroOwned.GetValueOrDefault(false))
                    {
                        select.AddWhere("qty <> 0");
                    }
                    if (!request.ShowStagedAndOut.GetValueOrDefault(false))
                    {
                        select.AddWhere("recordtype <> 'stagedout'");
                    }

                    if (request.IncludePeriodRevenue.GetValueOrDefault(false))
                    {
                        if (request.RevenueFilterMode.Equals("LT"))
                        {
                            select.AddWhere("revenue < " + request.RevenueFilterAmount.ToString());
                        }
                        else if (request.RevenueFilterMode.Equals("GT"))
                        {
                            select.AddWhere("revenue > " + request.RevenueFilterAmount.ToString());
                        }
                    }

                    if (request.IncludeNotRentedSince.GetValueOrDefault(false))
                    {
                        if (request.NotRentedSinceDate != null)
                        {
                            select.AddWhere("(lastrenteddate is null or lastrenteddate <= @notrentedsincedate)");
                            select.AddParameter("@notrentedsincedate", request.NotRentedSinceDate);
                        }
                    }


                    select.AddOrderBy("warehouse, inventorydepartment, category, masterno, stagedoutbarcode, stagedoutqty desc");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Quantity", "QuantityIn", "QuantityStaged", "QuantityOut", "QuantityInContainer", "QuantityInRepair", "QuantityOnPo", "QuantitySubbed", "Revenue" };
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

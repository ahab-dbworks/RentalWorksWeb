using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using WebApi.Modules.Inventory.Inventory;

namespace WebApi.Modules.Reports.RateUpdateReport
{
    [FwSqlTable("rateupdatebatchitemview")]
    public class RateUpdateReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
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
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string PartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        //public decimal? Cost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal)]
        //public decimal? NewCost { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddefaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdefaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldprice", modeltype: FwDataTypes.Decimal)]
        public decimal? OldPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newprice", modeltype: FwDataTypes.Decimal)]
        public decimal? NewPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldretail", modeltype: FwDataTypes.Decimal)]
        public decimal? OldRetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newretail", modeltype: FwDataTypes.Decimal)]
        public decimal? NewRetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek2rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek3rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek4rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek5rate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmaxdiscount", modeltype: FwDataTypes.Percentage)]
        public decimal? OldMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlycost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmaxdiscount", modeltype: FwDataTypes.Percentage)]
        public decimal? NewMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? OldReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.CurrencyString)]
        public decimal? NewReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmindw", modeltype: FwDataTypes.Decimal)]
        public decimal? OldMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmindw", modeltype: FwDataTypes.Decimal)]
        public decimal? NewMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RateUpdateReportRequest request)
        {
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
                    select.AddWhere("rateupdatebatchid = @rateupdatebatchid");
                    if (request.PendingModificationsOnly.GetValueOrDefault(false))
                    {
                        select.AddParameter("@rateupdatebatchid", "0");
                        select.AddWhere(InventoryFunc.GetRateUpdatePendingModificationsWhere());
                    }
                    else
                    {
                        select.AddParameter("@rateupdatebatchid", request.RateUpdateBatchId);
                    }
                    select.AddOrderBy("availfor, warehouse, masterno");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "OldMinDaysPerWeek" }; 
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

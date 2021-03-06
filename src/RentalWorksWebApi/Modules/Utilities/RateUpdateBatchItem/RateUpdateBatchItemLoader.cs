using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Utilities.RateUpdateBatchItem
{
    [FwSqlTable("rateupdatebatchitemview")]
    public class RateUpdateBatchItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateupdatebatchitemid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? RateUpdateBatchItemId { get; set; } = 0;
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
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Boolean)]
        public bool? Rank { get; set; }
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
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
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
        [FwSqlDataField(column: "oldhourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? OldMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmaxdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? OldMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? NewMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmaxdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? NewMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? OldUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? NewUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? OldReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? NewReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmindw", modeltype: FwDataTypes.Decimal)]
        public decimal? OldMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmindw", modeltype: FwDataTypes.Decimal)]
        public decimal? NewMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateupdatebatchid", modeltype: FwDataTypes.Integer)]
        public int? RateUpdateBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

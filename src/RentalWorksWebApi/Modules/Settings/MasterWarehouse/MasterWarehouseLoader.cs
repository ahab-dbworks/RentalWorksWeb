using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Settings.MasterWarehouse
{
    [FwSqlTable("masterwhview")]
    public abstract class MasterWarehouseLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        //public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Qty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyconsigned", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyqcrequired", modeltype: FwDataTypes.Integer)]
        public int? QtyQcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyonpo", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyOnPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averagecost", modeltype: FwDataTypes.Decimal)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderpoint", modeltype: FwDataTypes.Integer)]
        public int? ReorderPoint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderqty", modeltype: FwDataTypes.Integer)]
        public int? ReorderQty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? MaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLoc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLoc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
        public bool AvailabilityByHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean)]
        public bool AvailabilityByDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean)]
        public bool AvailabilityByAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelay", modeltype: FwDataTypes.Integer)]
        public int? AvailabilityQcDelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowallusersaddtoorder", modeltype: FwDataTypes.Boolean)]
        public bool AllowAllUsersAddToOrder { get; set; }
        //------------------------------------------------------------------------------------ 




        //------------------------------------------------------------------------------------ 
        //protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        //{
        //    base.SetBaseSelectQuery(select, qry, customFields, request);
        //    select.Parse();
        //    addFilterToSelect("ItemId", "masterid", select, request);
        //    addFilterToSelect("WarehouseId", "warehouseid", select, request);
        //}
        ////------------------------------------------------------------------------------------    
    }
}
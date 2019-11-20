using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.HomeControls.MasterWarehouse
{
    [FwSqlTable("masterwh")]
    public class MasterWarehouseRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderpoint", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ReorderPoint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reorderqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ReorderQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? MaximumDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? HasTieredCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockfee", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? RestockingFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? RestockingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityByHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityByDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AvailabilityByAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QcRequired { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availqcdelay", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? AvailabilityQcDelay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowallusersaddtoorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowAllUsersAddToOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "starttime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string DefaultStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stoptime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        public string DefaultStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 











        //[FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newdailyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newmonthlyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newweeklyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldmonthlyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "lastqtyonhand", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Lastqtyonhand { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Olddailyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweeklyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newprice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)]
        //public string Description { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldprice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        //public decimal? Newretail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        //public decimal? Oldretail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        //public decimal? Oldcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newdefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Newdefaultcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "olddefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Olddefaultcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweek2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newweek2rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweek3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newweek3rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek2rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek3rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newhourlyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldhourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldhourlyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldhourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Oldhourlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newhourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newhourlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "olddailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Olddailycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newdailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newdailycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Oldweeklycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newweeklycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmonthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Oldmonthlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newmonthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newmonthlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmaxdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Oldmaxdiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newmaxdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Newmaxdiscount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweek4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newweek4rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek4rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newweek5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newweek5rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek5rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PhysicalId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
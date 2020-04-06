using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Utilities.RateUpdateItem
{
    [FwSqlTable("masterwh")]
    public class RateUpdateItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeeklyRate { get; set; }
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
        [FwSqlDataField(column: "newweek2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "mindw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        public decimal? MinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmindw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        public decimal? NewMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? MaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmaxdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? NewMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? NewRetail { get; set; }
        //------------------------------------------------------------------------------------ 



        //[FwSqlDataField(column: "maxdw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        //public decimal? Maxdw { get; set; }
        ////------------------------------------------------------------------------------------ 


        //[FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Price { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Cost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "reorderpoint", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Reorderpoint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        //public string Shelfloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        //public string Aisleloc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldmonthlyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "lastqtyonhand", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? LastQuantityonhand { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Olddailyrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweeklyrate { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Newprice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldprice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Dailycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Monthlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Weeklycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Newcost { get; set; }
        //////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        //public decimal? Oldretail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Oldcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Defaultcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newdefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Newdefaultcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "olddefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Olddefaultcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ModbyusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "reorderqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? ReorderQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qcrequired { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 3)]
        //public decimal? Hourlycost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek2rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek3rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availbyhour { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbydeal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availbydeal { get; set; }
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
        //[FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek4rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Oldweek5rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availbyasset", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Availbyasset { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "starttime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        //public string Starttime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "stoptime", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 5)]
        //public string Stoptime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availqcdelay", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Availqcdelay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowallusersaddtoorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowallusersaddtoorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultpurchasecurrencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string DefaultpurchasecurrencyId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Hastieredcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Manifestvalue { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Newmanifestvalue { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Newreplacementcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Oldmanifestvalue { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Oldreplacementcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        //public decimal? Replacementcost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "restockfee", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        //public decimal? Restockfee { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "restockpercent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        //public decimal? Restockpercent { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "seriesrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Seriesrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "year2leaserate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Year2leaserate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "year3leaserate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Year3leaserate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "year4leaserate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        //public decimal? Year4leaserate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "oldmindw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        //public decimal? Oldmindw { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PhysicalId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

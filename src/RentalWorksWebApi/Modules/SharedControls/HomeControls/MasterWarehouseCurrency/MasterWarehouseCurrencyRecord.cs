using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.MasterWarehouseCurrency
{
    [FwSqlTable("masterwhcurrency")]
    public class MasterWarehouseCurrencyRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        public MasterWarehouseCurrencyRecord() : base()
        {
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? HourlyRate { get; set; }
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
        [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModbyusersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewRetail { get; set; }
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
        [FwSqlDataField(column: "newweeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? NewWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddefaultcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldprice", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldretail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldRetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklycost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? OldWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? OldWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 20, scale: 8)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveDataRecordEventArgs e)
        {
            //08/17/2020 justin hoffman
            // this is an unusual scenario where the primary keys on this class will always be provided with any save, so the API cannot readily determine whether the save request should
            // be an Insert or an Update.  Instead, the API considers all saves as Updates.  I am adding the blank record to the database (if missing) just prior to the Update
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (!AppFunc.DataExistsAsync(AppConfig, TableName, new string[] { "masterid", "warehouseid", "currencyid" }, new string[] { InventoryId, WarehouseId, CurrencyId }).Result)
                {
                    bool b = AppFunc.InsertDataAsync(AppConfig, TableName, new string[] { "masterid", "warehouseid", "currencyid" }, new string[] { InventoryId, WarehouseId, CurrencyId }).Result;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}

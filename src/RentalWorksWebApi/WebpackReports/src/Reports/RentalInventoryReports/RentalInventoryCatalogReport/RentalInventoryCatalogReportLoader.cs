using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebApi;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryCatalogReport
{
    public class RentalInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValue{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

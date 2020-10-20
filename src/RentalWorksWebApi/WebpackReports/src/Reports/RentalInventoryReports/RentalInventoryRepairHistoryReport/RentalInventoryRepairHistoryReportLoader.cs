using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Reports.InventoryRepairHistoryReport;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryRepairHistoryReport
{
    public class RentalInventoryRepairHistoryReportLoader : InventoryRepairHistoryReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryRepairHistoryReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalvageValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciation", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AccumulatedDepreciation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bookvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? BookValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdepreciationmonths", modeltype: FwDataTypes.Integer)]
        public int? DefaultDepreciationMonths { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultsalvagevaluepct", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultSalvageValuePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyout", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyincontainer", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyintransit", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqtyontruck", modeltype: FwDataTypes.Integer)]
        public int? CurrentQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------     
    }
}

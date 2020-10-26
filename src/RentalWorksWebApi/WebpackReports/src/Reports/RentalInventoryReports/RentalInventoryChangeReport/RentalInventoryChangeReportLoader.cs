using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Reports.InventoryChangeReport;

namespace WebApi.Modules.Reports.RentalInventoryChangeReport
{
    public class RentalInventoryChangeReportLoader : InventoryChangeReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryChangeReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldepreciation", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDepreciation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bookvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? BookValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salvagevalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalvageValue { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

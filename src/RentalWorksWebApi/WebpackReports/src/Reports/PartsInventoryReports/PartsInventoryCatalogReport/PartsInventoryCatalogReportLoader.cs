using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebApi;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryCatalogReport
{
    public class PartsInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

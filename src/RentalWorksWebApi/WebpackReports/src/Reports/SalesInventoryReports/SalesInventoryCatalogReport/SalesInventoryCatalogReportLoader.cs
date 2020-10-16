using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebApi;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryCatalogReport
{
    public class SalesInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
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

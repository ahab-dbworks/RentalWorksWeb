using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Reports.InventoryRepairHistoryReport;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryRepairHistoryReport
{
    public class SalesInventoryRepairHistoryReportLoader : InventoryRepairHistoryReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryRepairHistoryReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string AverageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string Retail { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

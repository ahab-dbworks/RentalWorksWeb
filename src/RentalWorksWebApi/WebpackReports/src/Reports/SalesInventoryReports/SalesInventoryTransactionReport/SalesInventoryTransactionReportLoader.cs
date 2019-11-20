using WebApi.Modules.Reports.Shared.InventoryTransactionReport;
using WebApi;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryTransactionReport
{
    public class SalesInventoryTransactionReportLoader : InventoryTransactionReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryTransactionReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

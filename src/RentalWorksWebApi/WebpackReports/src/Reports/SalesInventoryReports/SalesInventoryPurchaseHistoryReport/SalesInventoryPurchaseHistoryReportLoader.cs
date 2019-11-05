using WebApi.Modules.Reports.Shared.InventoryPurchaseHistoryReport;
using WebLibrary;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryPurchaseHistoryReport
{
    public class SalesInventoryPurchaseHistoryReportLoader : InventoryPurchaseHistoryReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryPurchaseHistoryReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

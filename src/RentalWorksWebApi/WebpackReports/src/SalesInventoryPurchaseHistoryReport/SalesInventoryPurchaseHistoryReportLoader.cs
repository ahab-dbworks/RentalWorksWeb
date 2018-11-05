using WebLibrary;
using WebApi.Modules.Reports.InventoryPurchaseHistoryReport;

namespace WebApi.Modules.Reports.SalesInventoryPurchaseHistoryReport
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

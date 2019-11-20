using WebApi.Modules.Reports.Shared.InventoryPurchaseHistoryReport;
using WebApi;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryPurchaseHistoryReport
{
    public class PartsInventoryPurchaseHistoryReportLoader : InventoryPurchaseHistoryReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryPurchaseHistoryReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

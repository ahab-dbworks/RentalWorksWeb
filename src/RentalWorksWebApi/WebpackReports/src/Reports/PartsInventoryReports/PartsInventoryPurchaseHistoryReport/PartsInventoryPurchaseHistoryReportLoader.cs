using WebApi.Modules.Reports.Shared.InventoryPurchaseHistoryReport;
using WebLibrary;

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

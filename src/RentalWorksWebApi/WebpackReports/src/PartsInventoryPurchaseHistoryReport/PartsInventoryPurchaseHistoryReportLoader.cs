using WebLibrary;
using WebApi.Modules.Reports.InventoryPurchaseHistoryReport;

namespace WebApi.Modules.Reports.PartsInventoryPurchaseHistoryReport
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

using WebLibrary;
using WebApi.Modules.Reports.InventoryPurchaseHistoryReport;

namespace WebApi.Modules.Reports.RentalInventoryPurchaseHistoryReport
{
    public class RentalInventoryPurchaseHistoryReportLoader : InventoryPurchaseHistoryReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryPurchaseHistoryReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Modules.Reports.Shared.InventoryPurchaseHistoryReport;
using WebApi;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryPurchaseHistoryReport
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

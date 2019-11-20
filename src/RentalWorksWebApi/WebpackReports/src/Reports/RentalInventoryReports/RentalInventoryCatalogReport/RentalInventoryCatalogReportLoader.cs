using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebApi;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryCatalogReport
{
    public class RentalInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
    }
}

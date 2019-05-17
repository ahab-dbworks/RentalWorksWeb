using WebApi.Modules.Reports.InventoryCatalogReport;
using WebLibrary;

namespace WebApi.Modules.Reports.RentalInventoryCatalogReport
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

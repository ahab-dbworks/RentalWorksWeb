using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebLibrary;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryCatalogReport
{
    public class PartsInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

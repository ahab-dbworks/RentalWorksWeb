using WebApi.Modules.Reports.InventoryCatalogReport;
using WebLibrary;

namespace WebApi.Modules.Reports.PartsInventoryCatalogReport
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

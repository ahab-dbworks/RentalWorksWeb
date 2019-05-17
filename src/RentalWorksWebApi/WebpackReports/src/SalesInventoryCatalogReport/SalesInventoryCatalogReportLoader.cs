using WebApi.Modules.Reports.InventoryCatalogReport;
using WebLibrary;

namespace WebApi.Modules.Reports.SalesInventoryCatalogReport
{
    public class SalesInventoryCatalogReportLoader : InventoryCatalogReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryCatalogReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

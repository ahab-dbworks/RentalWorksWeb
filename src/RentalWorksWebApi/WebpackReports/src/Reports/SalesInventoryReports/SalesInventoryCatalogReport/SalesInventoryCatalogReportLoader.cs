using WebApi.Modules.Reports.Shared.InventoryCatalogReport;
using WebLibrary;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryCatalogReport
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

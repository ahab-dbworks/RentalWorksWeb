using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using WebLibrary;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryAttributesReport
{
    public class SalesInventoryAttributesReportLoader : InventoryAttributesReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryAttributesReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

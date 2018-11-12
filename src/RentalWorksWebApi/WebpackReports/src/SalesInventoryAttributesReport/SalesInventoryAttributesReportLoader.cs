using WebLibrary;
using WebApi.Modules.Reports.InventoryAttributesReport;

namespace WebApi.Modules.Reports.SalesInventoryAttributesReport
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

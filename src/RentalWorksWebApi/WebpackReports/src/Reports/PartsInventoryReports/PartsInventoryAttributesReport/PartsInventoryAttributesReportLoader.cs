using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using WebApi;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryAttributesReport
{
    public class PartsInventoryAttributesReportLoader : InventoryAttributesReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryAttributesReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

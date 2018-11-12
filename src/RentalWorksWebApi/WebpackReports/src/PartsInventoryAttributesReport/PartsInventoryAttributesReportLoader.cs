using WebLibrary;
using WebApi.Modules.Reports.InventoryAttributesReport;

namespace WebApi.Modules.Reports.PartsInventoryAttributesReport
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

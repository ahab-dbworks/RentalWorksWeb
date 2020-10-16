using WebApi.Modules.Reports.InventoryChangeReport;

namespace WebApi.Modules.Reports.PartsInventoryChangeReport
{
    public class PartsInventoryChangeReportLoader : InventoryChangeReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryChangeReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

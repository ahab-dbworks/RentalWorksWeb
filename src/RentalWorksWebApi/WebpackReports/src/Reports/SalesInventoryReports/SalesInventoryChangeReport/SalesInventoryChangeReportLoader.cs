using WebApi.Modules.Reports.InventoryChangeReport;

namespace WebApi.Modules.Reports.SalesInventoryChangeReport
{
    public class SalesInventoryChangeReportLoader : InventoryChangeReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryChangeReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

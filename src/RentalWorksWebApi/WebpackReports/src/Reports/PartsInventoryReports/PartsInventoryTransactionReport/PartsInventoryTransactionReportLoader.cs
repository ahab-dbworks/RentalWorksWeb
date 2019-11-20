using WebApi.Modules.Reports.Shared.InventoryTransactionReport;
using WebApi;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryTransactionReport
{
    public class PartsInventoryTransactionReportLoader : InventoryTransactionReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public PartsInventoryTransactionReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
        }
        //------------------------------------------------------------------------------------ 
    }
}

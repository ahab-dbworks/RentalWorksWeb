using WebLibrary;
using WebApi.Modules.Reports.InventoryTransactionReport;

namespace WebApi.Modules.Reports.PartsInventoryTransactionReport
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

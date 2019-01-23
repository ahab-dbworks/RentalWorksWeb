using WebLibrary;
using WebApi.Modules.Reports.InventoryTransactionReport;

namespace WebApi.Modules.Reports.SalesInventoryTransactionReport
{
    public class SalesInventoryTransactionReportLoader : InventoryTransactionReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesInventoryTransactionReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------ 
    }
}

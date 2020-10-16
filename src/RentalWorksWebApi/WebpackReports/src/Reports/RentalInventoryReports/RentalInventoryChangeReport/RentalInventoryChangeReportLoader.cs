using WebApi.Modules.Reports.InventoryChangeReport;

namespace WebApi.Modules.Reports.RentalInventoryChangeReport
{
    public class RentalInventoryChangeReportLoader : InventoryChangeReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryChangeReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
    }
}

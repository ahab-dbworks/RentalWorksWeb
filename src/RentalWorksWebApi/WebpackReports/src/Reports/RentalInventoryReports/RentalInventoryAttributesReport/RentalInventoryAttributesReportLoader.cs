using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using WebApi;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryAttributesReport
{
    public class RentalInventoryAttributesReportLoader : InventoryAttributesReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryAttributesReportLoader()
        {
            AvailableForFilter = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
    }
}

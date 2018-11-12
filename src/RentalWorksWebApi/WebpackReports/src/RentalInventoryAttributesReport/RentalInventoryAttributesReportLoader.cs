using FwStandard.SqlServer.Attributes;
using WebLibrary;
using WebApi.Modules.Reports.InventoryAttributesReport;

namespace WebApi.Modules.Reports.RentalInventoryAttributesReport
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

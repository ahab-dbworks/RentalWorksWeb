using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    public class InventoryAvailabilityDateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryAvailabilityDateLoader inventoryAvailabilityDateLoader = new InventoryAvailabilityDateLoader();
        public InventoryAvailabilityDateLogic()
        {
            dataLoader = inventoryAvailabilityDateLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string AvailabilityDate { get; set; }
        public decimal? QuantityAvailable { get; set; }
        public string AvailableColor { get; set; }
        public decimal? QuantityLate { get; set; }
        public string LateColor { get; set; }
        public decimal? QuantityReserved { get; set; }
        public string ReservedColor { get; set; }
        public decimal? QuantityReturning { get; set; }
        public string ReturningColor { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

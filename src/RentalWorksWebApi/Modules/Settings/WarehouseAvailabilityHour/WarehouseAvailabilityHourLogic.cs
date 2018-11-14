using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseAvailabilityHour
{
    [FwLogic(Id:"5ivBNE6ReIk1u")]
    public class WarehouseAvailabilityHourLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseAvailabilityHourRecord warehouseAvailabilityHour = new WarehouseAvailabilityHourRecord();
        public WarehouseAvailabilityHourLogic()
        {
            dataRecords.Add(warehouseAvailabilityHour);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"goNSgZ8RWO4VH", IsPrimaryKey:true)]
        public string WarehouseAvailabilityHourId { get { return warehouseAvailabilityHour.WarehouseAvailabilityHourId; } set { warehouseAvailabilityHour.WarehouseAvailabilityHourId = value; } }

        [FwLogicProperty(Id:"OgAOcq5YTre")]
        public string WarehouseId { get { return warehouseAvailabilityHour.WarehouseId; } set { warehouseAvailabilityHour.WarehouseId = value; } }

        [FwLogicProperty(Id:"goNSgZ8RWO4VH", IsRecordTitle:true)]
        public int? WarehouseAvailabilityHour { get { return warehouseAvailabilityHour.WarehouseAvailabilityHour; } set { warehouseAvailabilityHour.WarehouseAvailabilityHour = value; } }

        [FwLogicProperty(Id:"vM5hSMFcKNd")]
        public string DateStamp { get { return warehouseAvailabilityHour.DateStamp; } set { warehouseAvailabilityHour.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

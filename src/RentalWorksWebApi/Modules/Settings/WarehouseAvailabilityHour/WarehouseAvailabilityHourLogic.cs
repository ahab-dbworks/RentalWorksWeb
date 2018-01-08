using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseAvailabilityHour
{
    public class WarehouseAvailabilityHourLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseAvailabilityHourRecord warehouseAvailabilityHour = new WarehouseAvailabilityHourRecord();
        public WarehouseAvailabilityHourLogic()
        {
            dataRecords.Add(warehouseAvailabilityHour);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseAvailabilityHourId { get { return warehouseAvailabilityHour.WarehouseAvailabilityHourId; } set { warehouseAvailabilityHour.WarehouseAvailabilityHourId = value; } }
        public string WarehouseId { get { return warehouseAvailabilityHour.WarehouseId; } set { warehouseAvailabilityHour.WarehouseId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public int? WarehouseAvailabilityHour { get { return warehouseAvailabilityHour.WarehouseAvailabilityHour; } set { warehouseAvailabilityHour.WarehouseAvailabilityHour = value; } }
        public string DateStamp { get { return warehouseAvailabilityHour.DateStamp; } set { warehouseAvailabilityHour.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
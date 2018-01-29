using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseLocation
{
    public class WarehouseLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseLocationRecord warehouseLocation = new WarehouseLocationRecord();
        WarehouseLocationLoader warehouseLocationLoader = new WarehouseLocationLoader();
        public WarehouseLocationLogic()
        {
            dataRecords.Add(warehouseLocation);
            dataLoader = warehouseLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseLocationId { get { return warehouseLocation.WarehouseLocationId; } set { warehouseLocation.WarehouseLocationId = value; } }
        public string WarehouseId { get { return warehouseLocation.WarehouseId; } set { warehouseLocation.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public string OfficeLocationId { get { return warehouseLocation.OfficeLocationId; } set { warehouseLocation.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string DateStamp { get { return warehouseLocation.DateStamp; } set { warehouseLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
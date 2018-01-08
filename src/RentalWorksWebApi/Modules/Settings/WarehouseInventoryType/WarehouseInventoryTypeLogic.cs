using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseInventoryType
{
    public class WarehouseInventoryTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseInventoryTypeRecord warehouseInventoryType = new WarehouseInventoryTypeRecord();
        WarehouseInventoryTypeLoader warehouseInventoryTypeLoader = new WarehouseInventoryTypeLoader();
        public WarehouseInventoryTypeLogic()
        {
            dataRecords.Add(warehouseInventoryType);
            dataLoader = warehouseInventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return warehouseInventoryType.WarehouseId; } set { warehouseInventoryType.WarehouseId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryTypeId { get { return warehouseInventoryType.InventoryTypeId; } set { warehouseInventoryType.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public string RentalBarCodeRangeId { get { return warehouseInventoryType.RentalBarCodeRangeId; } set { warehouseInventoryType.RentalBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalBarCodeRange { get; set; }
        public string SalesBarCodeRangeId { get { return warehouseInventoryType.SalesBarCodeRangeId; } set { warehouseInventoryType.SalesBarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesBarCodeRange { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderBy { get; set; }
        public string DateStamp { get { return warehouseInventoryType.DateStamp; } set { warehouseInventoryType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
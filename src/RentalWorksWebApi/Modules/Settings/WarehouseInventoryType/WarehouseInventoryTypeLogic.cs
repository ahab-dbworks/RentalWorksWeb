using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseInventoryType
{
    [FwLogic(Id:"z0o6PfMJJsdkI")]
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
        [FwLogicProperty(Id:"JSBkgL3F0znS3", IsPrimaryKey:true)]
        public string WarehouseId { get { return warehouseInventoryType.WarehouseId; } set { warehouseInventoryType.WarehouseId = value; } }

        [FwLogicProperty(Id:"JSBkgL3F0znS3", IsPrimaryKey:true)]
        public string InventoryTypeId { get { return warehouseInventoryType.InventoryTypeId; } set { warehouseInventoryType.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"JSBkgL3F0znS3", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"VHEA4UQpTTF")]
        public string RentalBarCodeRangeId { get { return warehouseInventoryType.RentalBarCodeRangeId; } set { warehouseInventoryType.RentalBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"WOyKZREUyzPsT", IsReadOnly:true)]
        public string RentalBarCodeRange { get; set; }

        [FwLogicProperty(Id:"0fCB9oRVJSo")]
        public string SalesBarCodeRangeId { get { return warehouseInventoryType.SalesBarCodeRangeId; } set { warehouseInventoryType.SalesBarCodeRangeId = value; } }

        [FwLogicProperty(Id:"Q0xotipju6Fwd", IsReadOnly:true)]
        public string SalesBarCodeRange { get; set; }

        [FwLogicProperty(Id:"UjqhGI5BA3vPQ", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"A2f2XMDv9KR")]
        public string DateStamp { get { return warehouseInventoryType.DateStamp; } set { warehouseInventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

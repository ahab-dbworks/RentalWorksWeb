using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseLocation
{
    [FwLogic(Id:"iLdY4G1hhAc72")]
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
        [FwLogicProperty(Id:"6dpC9CK3pEFY4", IsPrimaryKey:true)]
        public string WarehouseLocationId { get { return warehouseLocation.WarehouseLocationId; } set { warehouseLocation.WarehouseLocationId = value; } }

        [FwLogicProperty(Id:"hKy4AVmN1rT")]
        public string WarehouseId { get { return warehouseLocation.WarehouseId; } set { warehouseLocation.WarehouseId = value; } }

        [FwLogicProperty(Id:"6dpC9CK3pEFY4", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"vX5DRWurSBV")]
        public string OfficeLocationId { get { return warehouseLocation.OfficeLocationId; } set { warehouseLocation.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"4rtNqPTIG8Rd3", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"FNTzGhhkhiQ")]
        public string DateStamp { get { return warehouseLocation.DateStamp; } set { warehouseLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.InventoryWarehouseSpecific
{
    public class InventoryWarehouseSpecificLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryWarehouseSpecificLoader inventoryWarehouseLoader = new InventoryWarehouseSpecificLoader();
        public InventoryWarehouseSpecificLogic() 
        {
            dataLoader = inventoryWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "AWeQcDYDVIXW", IsPrimaryKey: true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id: "hrnHCLcmqHiY", IsPrimaryKey: true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id: "1YIBbxvSTfm0", IsReadOnly: true)]
        public string Warehouse { get; set; }

        //------------------------------------------------------------------------------------
    }

}

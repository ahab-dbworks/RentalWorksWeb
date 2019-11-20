using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.InventorySettings.WarehouseCatalog
{
    [FwLogic(Id:"Vh0SVo9ugcRyN")]
    public class WarehouseCatalogLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseCatalogRecord warehouseCatalog = new WarehouseCatalogRecord();
        public WarehouseCatalogLogic()
        {
            dataRecords.Add(warehouseCatalog);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"LIZHIO6CKzkko", IsPrimaryKey:true)]
        public string WarehouseCatalogId { get { return warehouseCatalog.WarehouseCatalogId; } set { warehouseCatalog.WarehouseCatalogId = value; } }

        [FwLogicProperty(Id:"LIZHIO6CKzkko", IsRecordTitle:true)]
        public string WarehouseCatalog { get { return warehouseCatalog.WarehouseCatalog; } set { warehouseCatalog.WarehouseCatalog = value; } }

        [FwLogicProperty(Id:"t07c8MTWm7b")]
        public string CatalogType { get { return warehouseCatalog.CatalogType; } set { warehouseCatalog.CatalogType = value; } }

        [FwLogicProperty(Id:"3NvECIPJzoA")]
        public bool? Inactive { get { return warehouseCatalog.Inactive; } set { warehouseCatalog.Inactive = value; } }

        [FwLogicProperty(Id:"s0bxuI8iADt")]
        public string DateStamp { get { return warehouseCatalog.DateStamp; } set { warehouseCatalog.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

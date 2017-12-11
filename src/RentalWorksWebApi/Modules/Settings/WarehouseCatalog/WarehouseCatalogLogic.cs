using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseCatalog
{
    public class WarehouseCatalogLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseCatalogRecord warehouseCatalog = new WarehouseCatalogRecord();
        public WarehouseCatalogLogic()
        {
            dataRecords.Add(warehouseCatalog);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseCatalogId { get { return warehouseCatalog.WarehouseCatalogId; } set { warehouseCatalog.WarehouseCatalogId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WarehouseCatalog { get { return warehouseCatalog.WarehouseCatalog; } set { warehouseCatalog.WarehouseCatalog = value; } }
        public string CatalogType { get { return warehouseCatalog.CatalogType; } set { warehouseCatalog.CatalogType = value; } }
        public bool? Inactive { get { return warehouseCatalog.Inactive; } set { warehouseCatalog.Inactive = value; } }
        public string DateStamp { get { return warehouseCatalog.DateStamp; } set { warehouseCatalog.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
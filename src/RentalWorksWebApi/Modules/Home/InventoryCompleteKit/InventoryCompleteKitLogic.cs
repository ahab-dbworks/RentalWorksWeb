using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryCompleteKit
{
    public class InventoryCompleteKitLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryCompleteKitRecord inventoryCompleteKit = new InventoryCompleteKitRecord();
        InventoryCompleteKitLoader inventoryCompleteKitLoader = new InventoryCompleteKitLoader();
        public InventoryCompleteKitLogic()
        {
            dataRecords.Add(inventoryCompleteKit);
            dataLoader = inventoryCompleteKitLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return inventoryCompleteKit.InventoryId; } set { inventoryCompleteKit.InventoryId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PackageId { get { return inventoryCompleteKit.PackageId; } set { inventoryCompleteKit.PackageId = value; } }
        public string ICode { get { return inventoryCompleteKit.ICode; } set { inventoryCompleteKit.ICode = value; } }
        public string Description { get { return inventoryCompleteKit.Description; } set { inventoryCompleteKit.Description = value; } }
        public string ItemClass { get { return inventoryCompleteKit.ItemClass; } set { inventoryCompleteKit.ItemClass = value; } }
        public string InventoryTypeId { get { return inventoryCompleteKit.InventoryTypeId; } set { inventoryCompleteKit.InventoryTypeId = value; } }
        public string InventoryType { get { return inventoryCompleteKit.InventoryType; } set { inventoryCompleteKit.InventoryType = value; } }
        public string CategoryId { get { return inventoryCompleteKit.CategoryId; } set { inventoryCompleteKit.CategoryId = value; } }
        public string Category { get { return inventoryCompleteKit.Category; } set { inventoryCompleteKit.Category = value; } }
        public string SubCategoryId { get { return inventoryCompleteKit.SubCategoryId; } set { inventoryCompleteKit.SubCategoryId = value; } }
        public string SubCategory { get { return inventoryCompleteKit.SubCategory; } set { inventoryCompleteKit.SubCategory = value; } }
        public string WarehouseId { get { return inventoryCompleteKit.WarehouseId; } set { inventoryCompleteKit.WarehouseId = value; } }
        public string Warehouse { get { return inventoryCompleteKit.Warehouse; } set { inventoryCompleteKit.Warehouse = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
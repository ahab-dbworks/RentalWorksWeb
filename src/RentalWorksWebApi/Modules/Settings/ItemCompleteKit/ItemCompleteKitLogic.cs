using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.ItemCompleteKit
{
    public class ItemCompleteKitLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemCompleteKitRecord itemCompleteKit = new ItemCompleteKitRecord();
        ItemCompleteKitLoader itemCompleteKitLoader = new ItemCompleteKitLoader();
        public ItemCompleteKitLogic()
        {
            dataRecords.Add(itemCompleteKit);
            dataLoader = itemCompleteKitLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemId { get { return itemCompleteKit.ItemId; } set { itemCompleteKit.ItemId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PackageId { get { return itemCompleteKit.PackageId; } set { itemCompleteKit.PackageId = value; } }
        public string ICode { get { return itemCompleteKit.ICode; } set { itemCompleteKit.ICode = value; } }
        public string Description { get { return itemCompleteKit.Description; } set { itemCompleteKit.Description = value; } }
        public string ItemClass { get { return itemCompleteKit.ItemClass; } set { itemCompleteKit.ItemClass = value; } }
        public string InventoryTypeId { get { return itemCompleteKit.InventoryTypeId; } set { itemCompleteKit.InventoryTypeId = value; } }
        public string InventoryType { get { return itemCompleteKit.InventoryType; } set { itemCompleteKit.InventoryType = value; } }
        public string CategoryId { get { return itemCompleteKit.CategoryId; } set { itemCompleteKit.CategoryId = value; } }
        public string Category { get { return itemCompleteKit.Category; } set { itemCompleteKit.Category = value; } }
        public string SubCategoryId { get { return itemCompleteKit.SubCategoryId; } set { itemCompleteKit.SubCategoryId = value; } }
        public string SubCategory { get { return itemCompleteKit.SubCategory; } set { itemCompleteKit.SubCategory = value; } }
        public string WarehouseId { get { return itemCompleteKit.WarehouseId; } set { itemCompleteKit.WarehouseId = value; } }
        public string Warehouse { get { return itemCompleteKit.Warehouse; } set { itemCompleteKit.Warehouse = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
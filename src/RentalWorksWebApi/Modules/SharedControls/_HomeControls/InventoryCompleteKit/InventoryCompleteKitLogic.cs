using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryCompleteKit
{
    [FwLogic(Id:"fcfqEtxXGh8S")]
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
        [FwLogicProperty(Id:"tQwU12QduFsB", IsPrimaryKey:true)]
        public string InventoryId { get { return inventoryCompleteKit.InventoryId; } set { inventoryCompleteKit.InventoryId = value; } }

        [FwLogicProperty(Id:"IcyGeTV1JmDM", IsPrimaryKey:true)]
        public string PackageId { get { return inventoryCompleteKit.PackageId; } set { inventoryCompleteKit.PackageId = value; } }

        [FwLogicProperty(Id:"DS1vO9xtBxT1")]
        public string ICode { get { return inventoryCompleteKit.ICode; } set { inventoryCompleteKit.ICode = value; } }

        [FwLogicProperty(Id:"kaReb9xWZ32l")]
        public string Description { get { return inventoryCompleteKit.Description; } set { inventoryCompleteKit.Description = value; } }

        [FwLogicProperty(Id:"HUZyMmpVknmV")]
        public string ItemClass { get { return inventoryCompleteKit.ItemClass; } set { inventoryCompleteKit.ItemClass = value; } }

        [FwLogicProperty(Id:"l20jP9AmJshm")]
        public string InventoryTypeId { get { return inventoryCompleteKit.InventoryTypeId; } set { inventoryCompleteKit.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"KqHzEZkr9ibG")]
        public string InventoryType { get { return inventoryCompleteKit.InventoryType; } set { inventoryCompleteKit.InventoryType = value; } }

        [FwLogicProperty(Id:"dAQxmwebNXk7")]
        public string CategoryId { get { return inventoryCompleteKit.CategoryId; } set { inventoryCompleteKit.CategoryId = value; } }

        [FwLogicProperty(Id:"BAIKdSo7mYPJ")]
        public string Category { get { return inventoryCompleteKit.Category; } set { inventoryCompleteKit.Category = value; } }

        [FwLogicProperty(Id:"4Fls6zFmhvya")]
        public string SubCategoryId { get { return inventoryCompleteKit.SubCategoryId; } set { inventoryCompleteKit.SubCategoryId = value; } }

        [FwLogicProperty(Id:"BTvN9L0pFGxf")]
        public string SubCategory { get { return inventoryCompleteKit.SubCategory; } set { inventoryCompleteKit.SubCategory = value; } }

        [FwLogicProperty(Id:"aCjBV9ZL3NLA")]
        public string WarehouseId { get { return inventoryCompleteKit.WarehouseId; } set { inventoryCompleteKit.WarehouseId = value; } }

        [FwLogicProperty(Id:"pbmyOkVpu2vC")]
        public string Warehouse { get { return inventoryCompleteKit.Warehouse; } set { inventoryCompleteKit.Warehouse = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.InventoryPackageInventory
{
    public class InventoryPackageInventoryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryPackageInventoryRecord inventoryPackageInventory = new InventoryPackageInventoryRecord();
        InventoryPackageInventoryLoader inventoryPackageInventoryLoader = new InventoryPackageInventoryLoader();
        public InventoryPackageInventoryLogic()
        {
            dataRecords.Add(inventoryPackageInventory);
            dataLoader = inventoryPackageInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryPackageInventoryId { get { return inventoryPackageInventory.InventoryPackageInventoryId; } set { inventoryPackageInventory.InventoryPackageInventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PackageId { get { return inventoryPackageInventory.PackageId; } set { inventoryPackageInventory.PackageId = value; } }
        public string InventoryId { get { return inventoryPackageInventory.InventoryId; } set { inventoryPackageInventory.InventoryId = value; } }
        public string ICode { get; set; }
        public string Description { get { return inventoryPackageInventory.Description; } set { inventoryPackageInventory.Description = value; } }
        public bool IsPrimary { get { return inventoryPackageInventory.IsPrimary; } set { inventoryPackageInventory.IsPrimary = value; } }
        public decimal? DefaultQuantity { get { return inventoryPackageInventory.DefaultQuantity; } set { inventoryPackageInventory.DefaultQuantity = value; } }
        public bool IsOption { get { return inventoryPackageInventory.IsOption; } set { inventoryPackageInventory.IsOption = value; } }
        public bool Charge { get { return inventoryPackageInventory.Charge; } set { inventoryPackageInventory.Charge = value; } }
        public bool Required { get { return inventoryPackageInventory.Required; } set { inventoryPackageInventory.Required = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool OptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemTrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailFrom { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CategoryOrderBy { get; set; }
        public int? OrderBy { get { return inventoryPackageInventory.OrderBy; } set { inventoryPackageInventory.OrderBy = value; } }
        public int? ItemColor { get { return inventoryPackageInventory.ItemColor; } set { inventoryPackageInventory.ItemColor = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool IsNestedComplete { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemInactive { get; set; }
        public string WarehouseId { get { return inventoryPackageInventory.WarehouseId; } set { inventoryPackageInventory.WarehouseId = value; } }
        public string ParentId { get { return inventoryPackageInventory.ParentId; } set { inventoryPackageInventory.ParentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PackageItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool ItemNonDiscountable { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryInventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UnitId { get; set; }
        public string DateStamp { get { return inventoryPackageInventory.DateStamp; } set { inventoryPackageInventory.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
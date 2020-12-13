using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryQuantityHistory
{
    [FwLogic(Id: "A48JP5oavBN6")]
    public class InventoryQuantityHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryQuantityHistoryLoader inventoryQuantityHistoryLoader = new InventoryQuantityHistoryLoader();
        public InventoryQuantityHistoryLogic()
        {
            dataLoader = inventoryQuantityHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "A701Ce3J7UAq", IsPrimaryKey: true, IsReadOnly: true)]
        public int? InventoryQuantityHistoryId { get; set; }
        //[FwLogicProperty(Id: "aFC89lyAY7vW", IsReadOnly: true)]
        //public bool? Internalchar { get; set; }
        [FwLogicProperty(Id: "AfJs7YOok01j", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "aMPSmT97KyUS", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "aoZOu63AwQ3E", IsReadOnly: true)]
        public string ItemDescription { get; set; }
        [FwLogicProperty(Id: "AUWVBHuYIj31", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "aY1mNPhJlZyF", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "B128J00v9Io9", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "bEHCvd51AaI6", IsReadOnly: true)]
        public string TransactionDateTime { get; set; }
        [FwLogicProperty(Id: "BHMlRkiHCp9L", IsReadOnly: true)]
        public string TransactionType { get; set; }
        [FwLogicProperty(Id: "BRHIHQ6Rt54y", IsReadOnly: true)]
        public string QuantityType { get; set; }
        [FwLogicProperty(Id: "buwn5Xd5EcU0", IsReadOnly: true)]
        public decimal? OldQuantity { get; set; }
        [FwLogicProperty(Id: "bvdMNjg0TgwE", IsReadOnly: true)]
        public decimal? QuantityChange { get; set; }
        [FwLogicProperty(Id: "BW5CXcz9DuD0", IsReadOnly: true)]
        public decimal? NewQuantity { get; set; }
        [FwLogicProperty(Id: "cHLhNhDOf1lC", IsReadOnly: true)]
        public decimal? OldAverageCost { get; set; }
        [FwLogicProperty(Id: "cjquaSmNDmiO", IsReadOnly: true)]
        public decimal? NewAverageCost { get; set; }
        [FwLogicProperty(Id: "CuI9qg6injrL", IsReadOnly: true)]
        public string UsersId { get; set; }
        [FwLogicProperty(Id: "Cvvm75UKmE89", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "CVzwWmKFEbR5", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "CyKMteq02uLJ", IsReadOnly: true)]
        public string UniqueId1 { get; set; }
        [FwLogicProperty(Id: "dMFcJOGKyK9Z", IsReadOnly: true)]
        public string UniqueId2 { get; set; }
        [FwLogicProperty(Id: "dntxIPiTBmSG", IsReadOnly: true)]
        public string UniqueId3 { get; set; }
        [FwLogicProperty(Id: "DsBHWw8GpY89", IsReadOnly: true)]
        public int? UniqueId4 { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

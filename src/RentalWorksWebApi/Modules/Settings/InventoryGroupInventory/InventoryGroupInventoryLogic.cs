using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.InventoryGroupInventory
{
    [FwLogic(Id:"VpBuXIxKVLEw")]
    public class InventoryGroupInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryGroupInventoryRecord inventoryGroupInventory = new InventoryGroupInventoryRecord();
        InventoryGroupInventoryLoader inventoryGroupInventoryLoader = new InventoryGroupInventoryLoader();
        public InventoryGroupInventoryLogic()
        {
            dataRecords.Add(inventoryGroupInventory);
            dataLoader = inventoryGroupInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"UVUMkmhN4qji", IsPrimaryKey:true)]
        public string InventoryGroupInventoryId { get { return inventoryGroupInventory.InventoryGroupInventoryId; } set { inventoryGroupInventory.InventoryGroupInventoryId = value; } }

        [FwLogicProperty(Id:"pKmFm33ikCk")]
        public string InventoryGroupId { get { return inventoryGroupInventory.InventoryGroupId; } set { inventoryGroupInventory.InventoryGroupId = value; } }

        [FwLogicProperty(Id:"PeA9zagTkAE")]
        public string InventoryId { get { return inventoryGroupInventory.InventoryId; } set { inventoryGroupInventory.InventoryId = value; } }

        [FwLogicProperty(Id:"JPKelIygJ8UY", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"qlhWemzydbGb", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"qdNX3XOhKdE8", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"vu2c1tZ71gC5", IsReadOnly:true)]
        public string Rank { get; set; }

        [FwLogicProperty(Id:"XqnRHmLrfCJF", IsReadOnly:true)]
        public decimal? OrderBy { get; set; }

        [FwLogicProperty(Id:"z8cFfysPwcQ")]
        public string ConsignorId { get { return inventoryGroupInventory.ConsignorId; } set { inventoryGroupInventory.ConsignorId = value; } }

        [FwLogicProperty(Id:"c68wRKyLHavf", IsReadOnly:true)]
        public string Consignor { get; set; }

        [FwLogicProperty(Id:"9ZgtNE1yGKF")]
        public string DateStamp { get { return inventoryGroupInventory.DateStamp; } set { inventoryGroupInventory.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

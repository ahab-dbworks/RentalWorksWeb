using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventorySummaryRetiredHistory
{
    [FwLogic(Id: "5WWm4eUTUAXaV")]
    public class InventorySummaryRetiredHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventorySummaryRetiredHistoryLoader inventorySummaryRetiredHistoryLoader = new InventorySummaryRetiredHistoryLoader();
        public InventorySummaryRetiredHistoryLogic()
        {
            dataLoader = inventorySummaryRetiredHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "6E4NrKrtuAI2m", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "6E9PsnotL6rNN", IsReadOnly: true)]
        public string ItemDescription { get; set; }
        [FwLogicProperty(Id: "6JxCrcbwsH8P8", IsReadOnly: true)]
        public string Whcode { get; set; }
        [FwLogicProperty(Id: "6K2sDQmZA0j37", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "6QCZfVi7tnWZt", IsReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwLogicProperty(Id: "6r7KnnAv1VkRp", IsReadOnly: true)]
        public string Consignor { get; set; }
        [FwLogicProperty(Id: "6rT7cHDQxNKrp", IsReadOnly: true)]
        public string ConsignoragreementId { get; set; }
        [FwLogicProperty(Id: "6ryPC28egkAsq", IsReadOnly: true)]
        public string Agreementno { get; set; }
        [FwLogicProperty(Id: "6ToPJ8CofuB7o", IsReadOnly: true)]
        public string Retireddate { get; set; }
        [FwLogicProperty(Id: "6URaUiyDqVZQF", IsReadOnly: true)]
        public string Retiredreason { get; set; }
        [FwLogicProperty(Id: "6VmWRqg632xuP", IsReadOnly: true)]
        public string Retiredby { get; set; }
        [FwLogicProperty(Id: "6ySnTFv0uDpGU", IsReadOnly: true)]
        public string RetiredreasonId { get; set; }
        [FwLogicProperty(Id: "6ZP0R7orLvu6z", IsReadOnly: true)]
        public string RetiredbyId { get; set; }
        [FwLogicProperty(Id: "7D9NrL4YfjVsW", IsReadOnly: true)]
        public string Retirednotes { get; set; }
        [FwLogicProperty(Id: "7j93hWrhpITNZ", IsReadOnly: true)]
        public decimal? RetiredQuantity { get; set; }
        [FwLogicProperty(Id: "7kv6q9rQaAD9d", IsReadOnly: true)]
        public decimal? UnretiredQuantity { get; set; }
        [FwLogicProperty(Id: "7shOCMN945u6M", IsReadOnly: true)]
        public string Lostorderno { get; set; }
        [FwLogicProperty(Id: "7UuA1Yzg4xZKz", IsReadOnly: true)]
        public string Soldorderno { get; set; }
        [FwLogicProperty(Id: "7vdIFASaXCyVt", IsReadOnly: true)]
        public string RetiredId { get; set; }
        [FwLogicProperty(Id: "7woXX8gg5QlhJ", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "808x3m0aNWR8y", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "881mOYCq216ii", IsReadOnly: true)]
        public string InventorydepartmentId { get; set; }
        [FwLogicProperty(Id: "8aXESXryRQngI", IsReadOnly: true)]
        public string LostorderId { get; set; }
        [FwLogicProperty(Id: "8ioitWEIs7VlK", IsReadOnly: true)]
        public string SoldtoorderId { get; set; }
        [FwLogicProperty(Id: "8jM8lTLZ7dYsC", IsReadOnly: true)]
        public string SoldtomasteritemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

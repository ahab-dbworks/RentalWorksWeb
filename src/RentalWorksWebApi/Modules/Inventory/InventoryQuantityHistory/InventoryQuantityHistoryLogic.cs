using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryQuantityHistory
{
    [FwLogic(Id: "0Nx7lax9ZO51")]
    public class InventoryQuantityHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryQuantityHistoryLoader inventoryQuantityHistoryLoader = new InventoryQuantityHistoryLoader();
        public InventoryQuantityHistoryLogic()
        {
            dataLoader = inventoryQuantityHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0O3bqQ68h9NuV  ", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "0oCBWv08IUOhL  ", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "0olwiEauaIy36", IsReadOnly: true)]
        public string PurchaseId { get; set; }
        [FwLogicProperty(Id: "0OPBP3miBa52m  ", IsReadOnly: true)]
        public string Ownership { get; set; }
        [FwLogicProperty(Id: "0OrYc5giDw6Cj  ", IsReadOnly: true)]
        public string DateChange { get; set; }
        [FwLogicProperty(Id: "0ot4ZkZ3bcbd0  ", IsReadOnly: true)]
        public decimal? QuantityChange { get; set; }
        [FwLogicProperty(Id: "0otBC5VPLjSUt  ", IsReadOnly: true)]
        public decimal? UnitCost { get; set; }
        [FwLogicProperty(Id: "acHgnLsw05hss", IsReadOnly: true)]
        public decimal? UnitCostExtended { get; set; }
        [FwLogicProperty(Id: "bLXubvnu3XZ0q", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "vmcN2RWBFHBf6", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "7DZ7U2VJjEdHN", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "PmzsfMgIN76eR", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "PG6Yy8noi4SEW", IsReadOnly: true)]
        public string CurrencySymbolAndCode { get; set; }
        [FwLogicProperty(Id: "0PBkcLit6dATk  ", IsReadOnly: true)]
        public string Type { get; set; }
        [FwLogicProperty(Id: "0PPcOealqzZvg  ", IsReadOnly: true)]
        public string ChangeDescription { get; set; }
        [FwLogicProperty(Id: "0pxPogws9LHz7  ", IsReadOnly: true)]
        public string Uniqueid01 { get; set; }
        [FwLogicProperty(Id: "0Q2C6y480ewWx  ", IsReadOnly: true)]
        public string Uniqueid02 { get; set; }
        [FwLogicProperty(Id: "0Q6DPsU1wduGg  ", IsReadOnly: true)]
        public string Uniqueid03 { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

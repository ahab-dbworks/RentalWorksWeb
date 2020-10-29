using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.PurchaseRetired
{
    [FwLogic(Id: "0frU8oKJieydE")]
    public class PurchaseRetiredLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PurchaseRetiredLoader purchaseRetiredLoader = new PurchaseRetiredLoader();
        public PurchaseRetiredLogic()
        {
            dataLoader = purchaseRetiredLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0G4ThRuhkZsG", IsReadOnly: true)]
        public string PurchaseId { get; set; }
        [FwLogicProperty(Id: "0Grbtf3ux67HF", IsReadOnly: true)]
        public string RetiredId { get; set; }
        [FwLogicProperty(Id: "0HBmJT4tTCGmg  ", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "0hCxLwf3ymw2B  ", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "0HIlHxJwbvNYl", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "0HUJLHzYcmfXy", IsReadOnly: true)]
        public string PurchaseDate { get; set; }
        [FwLogicProperty(Id: "0hUkdllz03PgH  ", IsReadOnly: true)]
        public string ReceiveDate { get; set; }
        [FwLogicProperty(Id: "0Hx8hx3xT0qGh  ", IsReadOnly: true)]
        public decimal? UnitCost { get; set; }
        [FwLogicProperty(Id: "0IFCb87sHldS", IsReadOnly: true)]
        public decimal? CurrencyExchangeRate { get; set; }
        [FwLogicProperty(Id: "0IhBlQ9UatVp8  ", IsReadOnly: true)]
        public decimal? UnitCostCurrencyConverted { get; set; }
        [FwLogicProperty(Id: "0Int2nLdYoMkR  ", IsReadOnly: true)]
        public decimal? CostCurrencyConvertedExtended { get; set; }
        [FwLogicProperty(Id: "0IW28R7svG91K", IsReadOnly: true)]
        public decimal? UnitCostWithTaxCurrencyConverted { get; set; }
        [FwLogicProperty(Id: "0J7i18wMssKps  ", IsReadOnly: true)]
        public decimal? CostWithTaxCurrencyConvertedExtended { get; set; }
        [FwLogicProperty(Id: "0J7tyYDSw8mq", IsReadOnly: true)]
        public decimal? OriginalEquipmentCost { get; set; }
        [FwLogicProperty(Id: "0JaKKGAl3qtGk  ", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "0JE1EfuLmCBG", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "0JfAiUFa2ZA6r  ", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "0JGrfPDiVXLQn  ", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "0jHPSQ7HxW6Dm  ", IsReadOnly: true)]
        public string CurrencySymbolAndCode { get; set; }
        [FwLogicProperty(Id: "0Jn2R8tHgAIvo", IsReadOnly: true)]
        public string RetiredDate { get; set; }
        [FwLogicProperty(Id: "0jQ4tFnm2NiEC  ", IsReadOnly: true)]
        public string RetiredReasonId { get; set; }
        [FwLogicProperty(Id: "0JrEhT1scWxB6  ", IsReadOnly: true)]
        public string RetiredReason { get; set; }
        [FwLogicProperty(Id: "0Jwrw1nY4vrEe  ", IsReadOnly: true)]
        public string RetiredById { get; set; }
        [FwLogicProperty(Id: "0kafOowrfTkY7  ", IsReadOnly: true)]
        public string RetiredBy { get; set; }
        [FwLogicProperty(Id: "0KFL0YoKG0u30", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

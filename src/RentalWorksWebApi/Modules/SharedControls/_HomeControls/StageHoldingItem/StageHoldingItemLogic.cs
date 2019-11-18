using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.Home.StageHoldingItem
{
    [FwLogic(Id: "2MroCNVqkEAAb")]
    public class StageHoldingItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StageHoldingItemLoader stageHoldingItemLoader = new StageHoldingItemLoader();
        public StageHoldingItemLogic()
        {
            dataLoader = stageHoldingItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "u80olNTphaaLe")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "diJDhyYTxuVDX")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id: "mFeIkT9VJPUwM")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id: "80F5BbU1yAKfl")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id: "ukzEdiHhHVNQl")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "vWm0tPLbdreiR")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "hYeEz9t5G8i7C")]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "6MiWQMfZXphJw")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id: "zCE2X4XzmmUOE")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id: "55Ds6tTBNA7IZ")]
        public string Description { get; set; }

        [FwLogicProperty(Id: "KcWmbhBhfa4tw")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id: "fyVNEiGd2ODdN")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id: "sDsETHGjofHaZ")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "qRvu6ljExgbWi")]
        public string BarCode { get; set; }

        [FwLogicProperty(Id: "G2W7IOz3rEHrA")]
        public decimal? QuantityHolding { get; set; }

        [FwLogicProperty(Id: "wn5qo2X0xbVqA")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id: "sx7qE7eQr3LkP")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id: "BfgMmhF7WD9lH")]
        public string RecType { get; set; }

        [FwLogicProperty(Id: "BujQwndu9Dbsz")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id: "cumKyagfqCJyy")]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id: "7fA0izmau2Cmd")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id: "RjiVMT82kqvLh")]
        public string NestedOrderItemId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

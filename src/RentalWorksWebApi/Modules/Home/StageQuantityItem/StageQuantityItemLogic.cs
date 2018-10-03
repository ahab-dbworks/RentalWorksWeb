using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.StageQuantityItem
{
    public class StageQuantityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StageQuantityItemLoader stageQuantityItemLoader = new StageQuantityItemLoader();
        public StageQuantityItemLogic()
        {
            dataLoader = stageQuantityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public string ICode { get; set; }
        public string ICodeColor { get; set; }
        public string TrackedBy { get; set; }
        public string Description { get; set; }
        public string DescriptionColor { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public decimal? QuantityOut { get; set; }
        public decimal? QuantityStaged { get; set; }
        public string QuantityRemaining { get; set; }
        public decimal? Quantity { get; set; }
        public string ItemOrder { get; set; }
        public string RecType { get; set; }
        public string RecTypeDisplay { get; set; }
        public string RecTypeColor { get; set; }
        public string ItemClass { get; set; }
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.ContractItemSummary
{
    public class ContractItemSummaryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractItemSummaryLoader contractItemSummaryLoader = new ContractItemSummaryLoader();
        public ContractItemSummaryLogic()
        {
            dataLoader = contractItemSummaryLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ContractId { get; set; }
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string ICode { get; set; }
        public string ICodeColor { get; set; }
        public string ICodeDisplay { get; set; }
        public string Description { get; set; }
        public string DescriptionColor { get; set; }
        public decimal? Quantity { get; set; }
        public string TrackedBy { get; set; }
        public string CategoryId { get; set; }
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public string PrimaryOrderItemId { get; set; }
        public string ItemClass { get; set; }
        public string ItemOrder { get; set; }
        public string OrderBy { get; set; }
        public string Notes { get; set; }
        public string OrderType { get; set; }
        public string RecType { get; set; }
        public string RecTypeDisplay { get; set; }
        public string OptionColor { get; set; }
        public string ParentId { get; set; }
        public decimal? AccessoryRatio { get; set; }
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

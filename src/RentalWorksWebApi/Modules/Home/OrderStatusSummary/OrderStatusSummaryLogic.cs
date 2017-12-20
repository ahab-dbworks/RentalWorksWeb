using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderStatusSummary
{
    public class OrderStatusSummaryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderStatusSummaryLoader orderStatusSummaryLoader = new OrderStatusSummaryLoader();
        public OrderStatusSummaryLogic()
        {
            dataLoader = orderStatusSummaryLoader;
        }
        public string OrderId { get; set; }
        public string ICode { get; set; }
        public string ICodeDisplay { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string InventoryId { get; set; }
        public string MasterItemId { get; set; }
        public string NestedMasterItemId { get; set; }
        public string ParentId { get; set; }
        public string OutWarehouseId { get; set; }
        public string OutWarehouseCode { get; set; }
        public string OutWarehouse { get; set; }
        public string InWarehouseId { get; set; }
        public string InWarehouseCode { get; set; }
        public string InWarehouse { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public string QuantityOrderedColor { get; set; }
        public decimal? SubQuantity { get; set; }
        public decimal? StageQuantity { get; set; }
        public decimal? StageQuantityFilter { get; set; }
        public decimal? OutQuantity { get; set; }
        public decimal? OutQuantityfilter { get; set; }
        public bool? IsSuspendOut { get; set; }
        public decimal? InQuantity { get; set; }
        public decimal? InQuantityFilter { get; set; }
        public bool? IsSuspendIn { get; set; }
        public decimal? ReturnedQuantity { get; set; }
        public decimal? ActivityQuantity { get; set; }
        public decimal? SubActivityQuantity { get; set; }
        public decimal? QuantityReceived { get; set; }
        public decimal? QuantityReturned { get; set; }
        public decimal? NotYetStagedQuantity { get; set; }
        public bool? TooManyStagedOut { get; set; }
        public decimal? NotYetStagedQuantityFilter { get; set; }
        public decimal? StillOutQuantity { get; set; }
        public string ItemOrder { get; set; }
        public string ItemClass { get; set; }
        public string RecType { get; set; }
        public string IsReturn { get; set; }
        public string PoOrderId { get; set; }
        public string PoMasteritemId { get; set; }
        public string RecTypeDisplay { get; set; }
        public string RecTypeColor { get; set; }
        public string OptionColor { get; set; }
        public bool? Bold { get; set; }
        public bool? HasPoItem { get; set; }
        public string VendorId { get; set; }
        public string Notes { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string OrderBy { get; set; }
        public bool? IsWardrobe { get; set; }
        public bool? IsProps { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? StagedOutExtendedCost { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? StagedOutExtendedPrice { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
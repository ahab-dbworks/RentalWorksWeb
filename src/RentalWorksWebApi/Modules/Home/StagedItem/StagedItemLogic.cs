using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.StagedItem
{
    public class StagedItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StagedItemLoader stagedItemLoader = new StagedItemLoader();
        public StagedItemLogic()
        {
            dataLoader = stagedItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string OrderId { get; set; }
        public string BarCode { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string OrderItemId { get; set; }
        public string ParentId { get; set; }
        public string StagedDateTime { get; set; }
        public string ItemClass { get; set; }
        public string Notes { get; set; }
        public string RecType { get; set; }
        public string RecTypeDisplay { get; set; }
        public string RecTypeColor { get; set; }
        public string OptionColor { get; set; }
        public bool? QuotePrint { get; set; }
        public bool? OrderPrint { get; set; }
        public bool? PickListPrint { get; set; }
        public bool? ContractOutPrint { get; set; }
        public bool? ReturnListPrint { get; set; }
        public bool? InvoicePrint { get; set; }
        public bool? ContractInPrint { get; set; }
        public bool? PurchaseOrderPrint { get; set; }
        public bool? ContractReceivePrint { get; set; }
        public bool? ContractReturnPrint { get; set; }
        public bool? PurchaseOrderReceivelistPrint { get; set; }
        public bool? PurchaseOrderReturnlistPrint { get; set; }
        public string StagedUsersId { get; set; }
        public string StagedUser { get; set; }
        public string VendorId { get; set; }
        public string Vendor { get; set; }
        public string VendorColor { get; set; }
        public string NestedOrderItemId { get; set; }
        public string TrackedBy { get; set; }
        public string ICodeColor { get; set; }
        public string DescriptionColor { get; set; }
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

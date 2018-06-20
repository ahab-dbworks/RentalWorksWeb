using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.ContractItemDetail
{
    public class ContractItemDetailLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractItemDetailLoader contractItemDetailLoader = new ContractItemDetailLoader();
        public ContractItemDetailLogic()
        {
            dataLoader = contractItemDetailLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ContractId { get; set; }
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string ICode { get; set; }
        public string ICodeDisplay { get; set; }
        public string ICodeColor { get; set; }
        public string Description { get; set; }
        public string DescriptionColor { get; set; }
        public decimal? Quantity { get; set; }
        public string Barcode { get; set; }
        public string TrackedBy { get; set; }
        public string CategoryId { get; set; }
        public string VendorId { get; set; }
        public string Vendor { get; set; }
        public string VendorColor { get; set; }
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
        public string UsersId { get; set; }
        public string UserName { get; set; }
        public string TransactionDateTime { get; set; }
        public string ParentId { get; set; }
        public decimal? AccessoryRatio { get; set; }
        public string NestedOrderItemId { get; set; }
        public string ContainerItemId { get; set; }
        public string ContainerBarCode { get; set; }
        public bool? IsConsignment { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

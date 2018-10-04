using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.ExchangeItem
{
    public class ExchangeItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ExchangeItemLoader exchangeItemLoader = new ExchangeItemLoader();
        public ExchangeItemLogic()
        {
            dataLoader = exchangeItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string ContractId { get; set; }
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        public string LocationId { get; set; }
        public string Direction { get; set; }
        public string DirectionDisplay { get; set; }
        public int? OrderTranId { get; set; }
        public bool? InternalChar { get; set; }
        public bool? ItemStatus { get; set; }
        public string OutContractId { get; set; }
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string OrderType { get; set; }
        public string InventoryId { get; set; }
        public string TrackedBy { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public int? Quantity { get; set; }
        public int? ContractQuantity { get; set; }
        public string ItemId { get; set; }
        public string VendorId { get; set; }
        public string Vendor { get; set; }
        public string WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public string ItemClass { get; set; }
        public string ItemOrder { get; set; }
        public string RecType { get; set; }
        public string ScannedByUserId { get; set; }
        public string ScannedByUserName { get; set; }
        public string ScannedDate { get; set; }
        public bool? ToRepair { get; set; }
        public bool? IsMultiWarehouse { get; set; }
        public decimal? OrderBy { get; set; }
        public string NestedOrderItemId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

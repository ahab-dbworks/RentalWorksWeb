using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.OrderStatusDetail
{
    public class OrderStatusDetailLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderStatusDetailLoader orderStatusDetailLoader = new OrderStatusDetailLoader();
        public OrderStatusDetailLogic()
        {
            dataLoader = orderStatusDetailLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string OrderId { get; set; }
        public string MasterItemId { get; set; }
        public string PrimaryMasteritemId { get; set; }
        public string ParentId { get; set; }
        public string ICode { get; set; }
        public string ICodeDisplay { get; set; }
        public string TrackedBy { get; set; }
        public string Description { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string InventoryId { get; set; }
        public string OutWarehouseId { get; set; }
        public string InWarehouseId { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public string RecType { get; set; }
        public string RecTypeDisplay { get; set; }
        public string ItemClass { get; set; }
        public string ItemOrder { get; set; }
        public string OptionColor { get; set; }
        public bool? Bold { get; set; }
        public string OutWarehouseCode { get; set; }
        public string OutWarehouse { get; set; }
        public string InWarehouseCode { get; set; }
        public string InWarehouse { get; set; }
        public string OutContractId { get; set; }
        public bool? IsSuspendOut { get; set; }
        public string OutDateTime { get; set; }
        public bool? IsExchangeOut { get; set; }
        public bool? IsTruckOut { get; set; }
        public string InContractId { get; set; }
        public bool? IsSuspendIn { get; set; }
        public string InDateTime { get; set; }
        public bool? IsExchangeIn { get; set; }
        public bool? IsTruckIn { get; set; }
        public decimal? Quantity { get; set; }
        public string ItemId { get; set; }
        public string BarCodeSerialRfid { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int? ExchangeOrderTranId { get; set; }
        public string ExchangeInternalChar { get; set; }
        public string VendorId { get; set; }
        public string Vendor { get; set; }
        public bool? Consignment { get; set; }
        public string ItemStatus { get; set; }
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        //public bool? Chkininclude { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        //public string PoOrderId { get; set; }
        public string NestedMasteritemId { get; set; }
        public bool? IsContainer { get; set; }
        public bool? QcRequired { get; set; }
        public string Location { get; set; }
        public int? OrderOrderby { get; set; }
    }
}
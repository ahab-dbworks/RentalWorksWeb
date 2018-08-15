using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInException
{
    public class CheckInExceptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInExceptionLoader checkInExceptionLoader = new CheckInExceptionLoader();
        public CheckInExceptionLogic()
        {
            dataLoader = checkInExceptionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsException { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SomeItemsIn { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityStagedAndOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubQuantityStagedAndOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubQuantityOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityIn { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityStillOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsMissing { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MissingQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? OptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsBarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContractId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsSub { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsConsignor { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

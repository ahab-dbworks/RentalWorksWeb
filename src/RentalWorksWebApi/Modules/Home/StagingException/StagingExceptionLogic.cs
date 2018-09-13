using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.StagingException
{
    public class StagingExceptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StagingExceptionLoader stagingExceptionLoader = new StagingExceptionLoader();
        public StagingExceptionLogic()
        {
            dataLoader = stagingExceptionLoader;
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
        public string NestedOrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsException { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? SomeOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubVendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReservedItems { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityStagedAndOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantitySub { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantitySubStagedAndOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantitySubOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsMissing { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MissingQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MissingColor { get; set; }
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
        public string ContainerInventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

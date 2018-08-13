using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckedOutItem
{
    public class CheckedOutItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckedOutItemLoader checkedOutItemLoader = new CheckedOutItemLoader();
        public CheckedOutItemLogic()
        {
            dataLoader = checkedOutItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ICodeColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? DescriptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryOrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StagedbByUserId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StagedByUser { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AccessoryRatio { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContainerBarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
    }
}

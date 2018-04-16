using FwStandard.BusinessLogic.Attributes;
using System;
using WebApi.Logic;
namespace WebApi.Modules.Home.PickListUtilityItem
{
    public class PickListUtilityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListUtilityItemRecord pickListUtilityItem = new PickListUtilityItemRecord();
        PickListUtilityItemLoader pickListUtilityItemLoader = new PickListUtilityItemLoader();
        public PickListUtilityItemLogic()
        {
            dataRecords.Add(pickListUtilityItem);
            dataLoader = pickListUtilityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SessionId { get { return pickListUtilityItem.SessionId; } set { pickListUtilityItem.SessionId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return pickListUtilityItem.OrderId; } set { pickListUtilityItem.OrderId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderItemId { get { return pickListUtilityItem.OrderItemId; } set { pickListUtilityItem.OrderItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? AccessoryRatio { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeIdNoParent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public decimal? QuantityOrdered { get { return pickListUtilityItem.QuantityOrdered; } set { pickListUtilityItem.QuantityOrdered = Convert.ToInt32(value); } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ConsignQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityInLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PickDate { get; set; }
        public decimal? PickQuantity { get { return pickListUtilityItem.PickQuantity; } set { pickListUtilityItem.PickQuantity = Convert.ToInt32(value); } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? StagedQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? OutQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RemainingQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PickedQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? OptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubVendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

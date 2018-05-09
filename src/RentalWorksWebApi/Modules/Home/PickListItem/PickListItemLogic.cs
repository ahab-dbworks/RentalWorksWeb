using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.PickListItem
{
    public class PickListItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListItemRecord pickListItem = new PickListItemRecord();
        PickListItemLoader pickListItemLoader = new PickListItemLoader();
        public PickListItemLogic()
        {
            dataRecords.Add(pickListItem);
            dataLoader = pickListItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PickListItemId { get { return pickListItem.PickListItemId; } set { pickListItem.PickListItemId = value; } }
        public string PickListId { get { return pickListItem.PickListId; } set { pickListItem.PickListId = value; } }
        public string OrderId { get { return pickListItem.OrderId; } set { pickListItem.OrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        public string MasterItemId { get { return pickListItem.MasterItemId; } set { pickListItem.MasterItemId = value; } }
        public string UsersId { get { return pickListItem.UsersId; } set { pickListItem.UsersId = value; } }
        public int? PickQuantity { get { return pickListItem.PickQuantity; } set { pickListItem.PickQuantity = value; } }
        public int? QuantityOrdered { get { return pickListItem.QuantityOrdered; } set { pickListItem.QuantityOrdered = value; } }
        public int? ConsignQuantity { get { return pickListItem.ConsignQuantity; } set { pickListItem.ConsignQuantity = value; } }
        public int? StagedQuantity { get { return pickListItem.StagedQuantity; } set { pickListItem.StagedQuantity = value; } }
        public int? OutQuantity { get { return pickListItem.OutQuantity; } set { pickListItem.OutQuantity = value; } }
        public int? InLocationQuantity { get { return pickListItem.InLocationQuantity; } set { pickListItem.InLocationQuantity = value; } }
        public string MasterId { get { return pickListItem.MasterId; } set { pickListItem.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeColor { get; set; }
        public string Description { get { return pickListItem.Description; } set { pickListItem.Description = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionColor { get; set; }
        public string OptionColor { get { return pickListItem.OptionColor; } set { pickListItem.OptionColor = value; } }
        public string ItemClass { get { return pickListItem.ItemClass; } set { pickListItem.ItemClass = value; } }
        public string ItemOrder { get { return pickListItem.ItemOrder; } set { pickListItem.ItemOrder = value; } }
        public string PickDate { get { return pickListItem.PickDate; } set { pickListItem.PickDate = value; } }
        public string PickTime { get { return pickListItem.PickTime; } set { pickListItem.PickTime = value; } }
        public string RecType { get { return pickListItem.RecType; } set { pickListItem.RecType = value; } }
        public string VendorId { get { return pickListItem.VendorId; } set { pickListItem.VendorId = value; } }
        public string InventoryTypeId { get { return pickListItem.InventoryTypeId; } set { pickListItem.InventoryTypeId = value; } }
        public string CategoryId { get { return pickListItem.CategoryId; } set { pickListItem.CategoryId = value; } }
        public string WarehouseId { get { return pickListItem.WarehouseId; } set { pickListItem.WarehouseId = value; } }
        public bool? Conflict { get { return pickListItem.Conflict; } set { pickListItem.Conflict = value; } }
        public bool? Bold { get { return pickListItem.Bold; } set { pickListItem.Bold = value; } }
        public string ItemOrderPickList { get { return pickListItem.ItemOrderPickList; } set { pickListItem.ItemOrderPickList = value; } }
        public string Notes { get { return pickListItem.Notes; } set { pickListItem.Notes = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RfId { get; set; }
        public string DateStamp { get { return pickListItem.DateStamp; } set { pickListItem.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
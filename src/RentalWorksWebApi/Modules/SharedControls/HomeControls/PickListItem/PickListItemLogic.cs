using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.PickListItem
{
    [FwLogic(Id:"5IEgQCxlN6li")]
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
        [FwLogicProperty(Id:"7dwKIIXfCIE3", IsPrimaryKey:true)]
        public string PickListItemId { get { return pickListItem.PickListItemId; } set { pickListItem.PickListItemId = value; } }

        [FwLogicProperty(Id:"2x6ebnOizRo7")]
        public string PickListId { get { return pickListItem.PickListId; } set { pickListItem.PickListId = value; } }

        [FwLogicProperty(Id:"p7zjJxChNMFn")]
        public string OrderId { get { return pickListItem.OrderId; } set { pickListItem.OrderId = value; } }

        [FwLogicProperty(Id:"eeXmePKMuxAH", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"iozHXfRQXklo")]
        public string MasterItemId { get { return pickListItem.MasterItemId; } set { pickListItem.MasterItemId = value; } }

        [FwLogicProperty(Id:"Mwut65GOhpoY")]
        public string UsersId { get { return pickListItem.UsersId; } set { pickListItem.UsersId = value; } }

        [FwLogicProperty(Id:"a2Iyzn3Vv5UX")]
        public int? PickQuantity { get { return pickListItem.PickQuantity; } set { pickListItem.PickQuantity = value; } }

        [FwLogicProperty(Id:"IoSAEVilvNOD")]
        public int? QuantityOrdered { get { return pickListItem.QuantityOrdered; } set { pickListItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id:"MAyast8LLBaF")]
        public int? ConsignQuantity { get { return pickListItem.ConsignQuantity; } set { pickListItem.ConsignQuantity = value; } }

        [FwLogicProperty(Id:"0cLNhNLv5W7K")]
        public int? StagedQuantity { get { return pickListItem.StagedQuantity; } set { pickListItem.StagedQuantity = value; } }

        [FwLogicProperty(Id:"dseWjEAg5NLE")]
        public int? OutQuantity { get { return pickListItem.OutQuantity; } set { pickListItem.OutQuantity = value; } }

        [FwLogicProperty(Id:"P6vRfpHiU4Rj")]
        public int? InLocationQuantity { get { return pickListItem.InLocationQuantity; } set { pickListItem.InLocationQuantity = value; } }

        [FwLogicProperty(Id:"RYq6wMTIoOPW")]
        public string MasterId { get { return pickListItem.MasterId; } set { pickListItem.MasterId = value; } }

        [FwLogicProperty(Id:"W10aEwAOV6Jx", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"W10aEwAOV6Jx", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"7mg5zA7vFW0p")]
        public string Description { get { return pickListItem.Description; } set { pickListItem.Description = value; } }

        [FwLogicProperty(Id:"lsmO1ZHw5jfg", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"dNDD6URkGJCr")]
        public string OptionColor { get { return pickListItem.OptionColor; } set { pickListItem.OptionColor = value; } }

        [FwLogicProperty(Id:"9lmK1RV53IcC")]
        public string ItemClass { get { return pickListItem.ItemClass; } set { pickListItem.ItemClass = value; } }

        [FwLogicProperty(Id:"0v64NOgZWUkB")]
        public string ItemOrder { get { return pickListItem.ItemOrder; } set { pickListItem.ItemOrder = value; } }

        [FwLogicProperty(Id:"hEiNcKqZmejv")]
        public string PickDate { get { return pickListItem.PickDate; } set { pickListItem.PickDate = value; } }

        [FwLogicProperty(Id:"vZTwXasnjywE")]
        public string PickTime { get { return pickListItem.PickTime; } set { pickListItem.PickTime = value; } }

        [FwLogicProperty(Id:"eo3XZg4PlbUz")]
        public string RecType { get { return pickListItem.RecType; } set { pickListItem.RecType = value; } }

        [FwLogicProperty(Id:"LXyh1hzs1RkY")]
        public string VendorId { get { return pickListItem.VendorId; } set { pickListItem.VendorId = value; } }

        [FwLogicProperty(Id: "BNTZyyyG3ajMe", IsReadOnly: true)]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"kVEc9qSVffbQ")]
        public string InventoryTypeId { get { return pickListItem.InventoryTypeId; } set { pickListItem.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"wxvu2fh16RJY")]
        public string CategoryId { get { return pickListItem.CategoryId; } set { pickListItem.CategoryId = value; } }

        [FwLogicProperty(Id:"i8N60tJo0Y2l")]
        public string WarehouseId { get { return pickListItem.WarehouseId; } set { pickListItem.WarehouseId = value; } }

        [FwLogicProperty(Id:"jpkknGqFELhZ")]
        public bool? Conflict { get { return pickListItem.Conflict; } set { pickListItem.Conflict = value; } }

        [FwLogicProperty(Id:"JgmLrmh9IqGq")]
        public bool? Bold { get { return pickListItem.Bold; } set { pickListItem.Bold = value; } }

        [FwLogicProperty(Id:"Z2jPdc3OGF62")]
        public string ItemOrderPickList { get { return pickListItem.ItemOrderPickList; } set { pickListItem.ItemOrderPickList = value; } }

        [FwLogicProperty(Id:"AkxB2muOeFU1")]
        public string Notes { get { return pickListItem.Notes; } set { pickListItem.Notes = value; } }

        [FwLogicProperty(Id:"PT7irBGuNwDu", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"8ls2rfw1xYWC", IsReadOnly:true)]
        public string SerialNumber { get; set; }

        [FwLogicProperty(Id:"prFKaTy4ltep", IsReadOnly:true)]
        public string RfId { get; set; }

        [FwLogicProperty(Id:"utAGJnoL5VJc")]
        public string DateStamp { get { return pickListItem.DateStamp; } set { pickListItem.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

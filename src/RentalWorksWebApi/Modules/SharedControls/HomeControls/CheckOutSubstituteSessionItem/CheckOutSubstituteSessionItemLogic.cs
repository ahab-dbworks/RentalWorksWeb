using WebApi.Logic;
using FwStandard.AppManager;
using WebApi.Modules.Home.SearchSessionItem;

namespace WebApi.Modules.HomeControls.CheckOutSubstituteSessionItem
{
    [FwLogic(Id: "qdBmNCyJERMQU")]
    public class CheckOutSubstituteSessionItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SearchSessionItemRecord checkOutSubstituteSessionItem = new SearchSessionItemRecord();
        CheckOutSubstituteSessionItemLoader checkOutSubstituteSessionItemLoader = new CheckOutSubstituteSessionItemLoader();
        public CheckOutSubstituteSessionItemLogic()
        {
            dataRecords.Add(checkOutSubstituteSessionItem);
            dataLoader = checkOutSubstituteSessionItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qdEjCCbvR3HyU", IsPrimaryKey: true, IsReadOnly: true)]
        public int? Id { get { return checkOutSubstituteSessionItem.SearchSessionItemId; } set { checkOutSubstituteSessionItem.SearchSessionItemId = value; } }
        [FwLogicProperty(Id: "qeLD0OENybtLN")]
        public string SessionId { get { return checkOutSubstituteSessionItem.SessionId; } set { checkOutSubstituteSessionItem.SessionId = value; } }
        [FwLogicProperty(Id: "QEqRd90BGt29d")]
        public string InventoryId { get { return checkOutSubstituteSessionItem.InventoryId; } set { checkOutSubstituteSessionItem.InventoryId = value; } }
        [FwLogicProperty(Id: "QEUeHXXWhlS6g")]
        public string ItemId { get { return checkOutSubstituteSessionItem.ItemId; } set { checkOutSubstituteSessionItem.ItemId = value; } }
        [FwLogicProperty(Id: "QEyVODoYvYYwD", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "QEZLTm9WIS62W", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "QGNrN57CA8Pzh", IsReadOnly: true)]
        public string BarCode { get; set; }
        [FwLogicProperty(Id: "Qgz0qAC6m1blv", IsReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwLogicProperty(Id: "qhaIYjYCgegH5", IsReadOnly: true)]
        public decimal? Quantity { get { return checkOutSubstituteSessionItem.Quantity; } set { checkOutSubstituteSessionItem.Quantity = value; } }
        [FwLogicProperty(Id: "VDwZIxm57QZTc", IsReadOnly: true)]
        public string InventoryStatusId { get; set; }
        [FwLogicProperty(Id: "aP6X4EoTeDAT1", IsReadOnly: true)]
        public string InventoryStatus { get; set; }
        [FwLogicProperty(Id: "M6cC4dhOpCVM4", IsReadOnly: true)]
        public string InventoryStatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

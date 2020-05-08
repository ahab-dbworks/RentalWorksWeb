using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.AgentX.CheckOutSubstituteSessionItem
{
    [FwLogic(Id: "qdBmNCyJERMQU")]
    public class CheckOutSubstituteSessionItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckOutSubstituteSessionItemLoader checkOutSubstituteSessionItemLoader = new CheckOutSubstituteSessionItemLoader();
        public CheckOutSubstituteSessionItemLogic()
        {
            dataLoader = checkOutSubstituteSessionItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qdEjCCbvR3HyU", IsPrimaryKey: true, IsReadOnly: true)]
        public int? Id { get; set; }
        [FwLogicProperty(Id: "qeLD0OENybtLN", IsReadOnly: true)]
        public string SessionId { get; set; }
        [FwLogicProperty(Id: "QEqRd90BGt29d", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "QEUeHXXWhlS6g", IsReadOnly: true)]
        public string ItemId { get; set; }
        [FwLogicProperty(Id: "QEyVODoYvYYwD", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "QEZLTm9WIS62W", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "QGNrN57CA8Pzh", IsReadOnly: true)]
        public string BarCode { get; set; }
        [FwLogicProperty(Id: "Qgz0qAC6m1blv", IsReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwLogicProperty(Id: "qhaIYjYCgegH5", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

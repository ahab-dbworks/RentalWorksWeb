using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.InventorySettings.InventoryGroup
{
    [FwLogic(Id:"aZsueakG1nqK")]
    public class InventoryGroupLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryGroupRecord inventoryGroup = new InventoryGroupRecord();
        public InventoryGroupLogic()
        {
            dataRecords.Add(inventoryGroup);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"nzQaU5Q9yoTd", IsPrimaryKey:true)]
        public string InventoryGroupId { get { return inventoryGroup.InventoryGroupId; } set { inventoryGroup.InventoryGroupId = value; } }

        [FwLogicProperty(Id:"nzQaU5Q9yoTd", IsRecordTitle:true)]
        public string InventoryGroup { get { return inventoryGroup.InventoryGroup; } set { inventoryGroup.InventoryGroup = value; } }

        //[FwLogicProperty(Id:"7H3rPf7WucB")]
        //public string WarehouseId { get { return inventoryGroup.WarehouseId; } set { inventoryGroup.WarehouseId = value; } }

        [FwLogicProperty(Id:"FU0R1Q5Cq4M")]
        public string RecType { get { return inventoryGroup.RecType; } set { inventoryGroup.RecType = value; } }

        //[FwLogicProperty(Id:"RcGfJ0pPdy6")]
        //public bool? Includeconsigned { get { return inventoryGroup.Includeconsigned; } set { inventoryGroup.Includeconsigned = value; } }

        //[FwLogicProperty(Id:"hZ9Qbsss1DN")]
        //public bool? Includeowned { get { return inventoryGroup.Includeowned; } set { inventoryGroup.Includeowned = value; } }

        //[FwLogicProperty(Id:"tNNYR7jVTXq")]
        //public bool? Ranka { get { return inventoryGroup.Ranka; } set { inventoryGroup.Ranka = value; } }

        //[FwLogicProperty(Id:"na0hMUAYRjz")]
        //public bool? Rankb { get { return inventoryGroup.Rankb; } set { inventoryGroup.Rankb = value; } }

        //[FwLogicProperty(Id:"k62rph2w1cE")]
        //public bool? Rankc { get { return inventoryGroup.Rankc; } set { inventoryGroup.Rankc = value; } }

        //[FwLogicProperty(Id:"NTOur0pfw2C")]
        //public bool? Rankd { get { return inventoryGroup.Rankd; } set { inventoryGroup.Rankd = value; } }

        //[FwLogicProperty(Id:"K1y2CPBO5HM")]
        //public string Trackedby { get { return inventoryGroup.Trackedby; } set { inventoryGroup.Trackedby = value; } }

        //[FwLogicProperty(Id:"SuVOicJkMR7")]
        //public string Aisle { get { return inventoryGroup.Aisle; } set { inventoryGroup.Aisle = value; } }

        //[FwLogicProperty(Id:"1z2KoPGthNj")]
        //public string Shelf { get { return inventoryGroup.Shelf; } set { inventoryGroup.Shelf = value; } }

        [FwLogicProperty(Id:"uKauFJ1fZwS")]
        public bool? Inactive { get { return inventoryGroup.Inactive; } set { inventoryGroup.Inactive = value; } }

        [FwLogicProperty(Id:"tkFIrE3G8WT")]
        public string DateStamp { get { return inventoryGroup.DateStamp; } set { inventoryGroup.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

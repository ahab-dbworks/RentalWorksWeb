using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Settings.InventorySettings.InventoryType;

namespace WebApi.Modules.Settings.LaborSettings.LaborType
{
    [FwLogic(Id:"F68RQiFdeT8C")]
    public class LaborTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        LaborTypeLoader inventoryTypeLoader = new LaborTypeLoader();
        public LaborTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"3sJqCu4st5sv", IsPrimaryKey:true)]
        public string LaborTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"3sJqCu4st5sv", IsRecordTitle:true)]
        public string LaborType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }

        [FwLogicProperty(Id:"Ma3pkR9oP1W")]
        public bool? Labor { get { return inventoryType.Labor; } set { inventoryType.Labor = value; } }

        [FwLogicProperty(Id:"c1YnVuoyZAP")]
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }

        [FwLogicProperty(Id: "QGImrzeiV7OLC", IsReadOnly: true)]
        public int? CategoryCount { get; set; }

        [FwLogicProperty(Id:"AYF0kY32D9u")]
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }

        [FwLogicProperty(Id:"XlMIyVUUC7i")]
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Labor = true;
        }
        //------------------------------------------------------------------------------------
    }

}

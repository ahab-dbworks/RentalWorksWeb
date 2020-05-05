using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Settings.InventorySettings.InventoryStatus
{
    [FwLogic(Id: "HALAz3U1K9yq")]
    public class InventoryStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryStatusRecord inventoryStatus = new InventoryStatusRecord();
        public InventoryStatusLogic()
        {
            dataRecords.Add(inventoryStatus);
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "6lTc6hhWSWQF", IsPrimaryKey: true)]
        public string InventoryStatusId { get { return inventoryStatus.InventoryStatusId; } set { inventoryStatus.InventoryStatusId = value; } }

        [FwLogicProperty(Id: "6lTc6hhWSWQF", IsRecordTitle: true)]
        public string InventoryStatus { get { return inventoryStatus.InventoryStatus; } set { inventoryStatus.InventoryStatus = value; } }

        [FwLogicProperty(Id: "Ae2wYTa4coP")]
        public string StatusType { get { return inventoryStatus.StatusType; } set { inventoryStatus.StatusType = value; } }

        [FwLogicProperty(Id: "IE2A1sGl70H")]
        public string Color { get { return inventoryStatus.Color; } set { inventoryStatus.Color = value; } }

        [FwLogicProperty(Id: "wWZ41andyKdI8")]
        public string TextColor { get { return inventoryStatus.TextColor; } set { inventoryStatus.TextColor = value; } }

        [FwLogicProperty(Id: "UCfTQwIaRiT")]
        public string DateStamp { get { return inventoryStatus.DateStamp; } set { inventoryStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            RwGlobals.SetGlobalColors(AppConfig.DatabaseSettings);
        }
        //------------------------------------------------------------------------------------
    }

}

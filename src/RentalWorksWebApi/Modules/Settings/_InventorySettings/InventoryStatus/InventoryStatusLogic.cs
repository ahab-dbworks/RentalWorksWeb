using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using WebApi.Logic;
using WebLibrary;

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

        [FwLogicProperty(Id: "5gegGOTq5CC")]
        public bool? WhiteText { get { return inventoryStatus.WhiteText; } set { inventoryStatus.WhiteText = value; } }

        //[FwBusinessLogicField(isReadOnly: true)]
        //public string TextColor { get { return (WhiteText ? FwConvert.OleColorToHtmlColor(System.Drawing.Color.White.) : FwConvert.OleColorToHtmlColor(0)); } }
        //public string TextColor { get { return (WhiteText ? "rgb(255,255,255" : "rgb(0,0,0"); } }
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

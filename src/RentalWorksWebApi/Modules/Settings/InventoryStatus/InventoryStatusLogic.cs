using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using WebApi.Logic;


namespace WebApi.Modules.Settings.InventoryStatus
{
    public class InventoryStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryStatusRecord inventoryStatus = new InventoryStatusRecord();
        public InventoryStatusLogic()
        {
            dataRecords.Add(inventoryStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryStatusId { get { return inventoryStatus.InventoryStatusId; } set { inventoryStatus.InventoryStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryStatus { get { return inventoryStatus.InventoryStatus; } set { inventoryStatus.InventoryStatus = value; } }
        public string StatusType { get { return inventoryStatus.StatusType; } set { inventoryStatus.StatusType = value; } }
        public string Color { get { return inventoryStatus.Color; } set { inventoryStatus.Color = value; } }
        public bool? WhiteText { get { return inventoryStatus.WhiteText; } set { inventoryStatus.WhiteText = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string TextColor { get { return (WhiteText ? FwConvert.OleColorToHtmlColor(System.Drawing.Color.White.) : FwConvert.OleColorToHtmlColor(0)); } }
        //public string TextColor { get { return (WhiteText ? "rgb(255,255,255" : "rgb(0,0,0"); } }
        public string DateStamp { get { return inventoryStatus.DateStamp; } set { inventoryStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

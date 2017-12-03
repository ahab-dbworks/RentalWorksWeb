using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using RentalWorksWebApi.Logic;


namespace RentalWorksWebApi.Modules.Settings.RentalStatus
{
    public class InventoryStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryStatusRecord rentalStatus = new InventoryStatusRecord();
        public InventoryStatusLogic()
        {
            dataRecords.Add(rentalStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryStatusId { get { return rentalStatus.InventoryStatusId; } set { rentalStatus.InventoryStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryStatus { get { return rentalStatus.InventoryStatus; } set { rentalStatus.InventoryStatus = value; } }
        public string StatusType { get { return rentalStatus.StatusType; } set { rentalStatus.StatusType = value; } }
        public string Color { get { return rentalStatus.Color; } set { rentalStatus.Color = value; } }
        public bool? WhiteText { get { return rentalStatus.WhiteText; } set { rentalStatus.WhiteText = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string TextColor { get { return (WhiteText ? FwConvert.OleColorToHtmlColor(System.Drawing.Color.White.) : FwConvert.OleColorToHtmlColor(0)); } }
        //public string TextColor { get { return (WhiteText ? "rgb(255,255,255" : "rgb(0,0,0"); } }
        public string DateStamp { get { return rentalStatus.DateStamp; } set { rentalStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

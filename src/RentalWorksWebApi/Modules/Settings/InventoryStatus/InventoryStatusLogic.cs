using FwStandard.BusinessLogic.Attributes;
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
        public bool WhiteText { get { return rentalStatus.WhiteText; } set { rentalStatus.WhiteText = value; } }
        public string DateStamp { get { return rentalStatus.DateStamp; } set { rentalStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

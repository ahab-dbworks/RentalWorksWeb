using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.RentalStatus
{
    public class RentalStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RentalStatusRecord rentalStatus = new RentalStatusRecord();
        public RentalStatusLogic()
        {
            dataRecords.Add(rentalStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RentalStatusId { get { return rentalStatus.RentalStatusId; } set { rentalStatus.RentalStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RentalStatus { get { return rentalStatus.RentalStatus; } set { rentalStatus.RentalStatus = value; } }
        public string StatusType { get { return rentalStatus.StatusType; } set { rentalStatus.StatusType = value; } }
        public string Color { get { return rentalStatus.Color; } set { rentalStatus.Color = value; } }
        public bool WhiteText { get { return rentalStatus.WhiteText; } set { rentalStatus.WhiteText = value; } }
        public string DateStamp { get { return rentalStatus.DateStamp; } set { rentalStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.UnretiredReason
{
    public class UnretiredReasonLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        UnretiredReasonRecord unretiredReason = new UnretiredReasonRecord();
        public UnretiredReasonLogic()
        {
            dataRecords.Add(unretiredReason);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UnretiredReasonId { get { return unretiredReason.UnretiredReasonId; } set { unretiredReason.UnretiredReasonId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string UnretiredReason { get { return unretiredReason.UnretiredReason; } set { unretiredReason.UnretiredReason = value; } }
        public string ReasonType { get { return unretiredReason.ReasonType; } set { unretiredReason.ReasonType = value; } }
        public bool Inactive { get { return unretiredReason.Inactive; } set { unretiredReason.Inactive = value; } }
        public string DateStamp { get { return unretiredReason.DateStamp; } set { unretiredReason.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

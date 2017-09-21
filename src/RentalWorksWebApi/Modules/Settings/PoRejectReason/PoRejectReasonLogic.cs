using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.PoRejectReason
{
    public class PoRejectReasonLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PoRejectReasonRecord poRejectReason = new PoRejectReasonRecord();
        public PoRejectReasonLogic()
        {
            dataRecords.Add(poRejectReason);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoRejectReasonId { get { return poRejectReason.PoRejectReasonId; } set { poRejectReason.PoRejectReasonId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoRejectReason { get { return poRejectReason.PoRejectReason; } set { poRejectReason.PoRejectReason = value; } }
        public bool Inactive { get { return poRejectReason.Inactive; } set { poRejectReason.Inactive = value; } }
        public string DateStamp { get { return poRejectReason.DateStamp; } set { poRejectReason.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
} 

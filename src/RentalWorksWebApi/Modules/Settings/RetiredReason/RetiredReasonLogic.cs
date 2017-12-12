using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.RetiredReason
{
    public class RetiredReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RetiredReasonRecord retiredReason = new RetiredReasonRecord();
        public RetiredReasonLogic()
        {
            dataRecords.Add(retiredReason);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RetiredReasonId { get { return retiredReason.RetiredReasonId; } set { retiredReason.RetiredReasonId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RetiredReason { get { return retiredReason.RetiredReason; } set { retiredReason.RetiredReason = value; } }
        public string ReasonType { get { return retiredReason.ReasonType; } set { retiredReason.ReasonType = value; } }
        public bool? Inactive { get { return retiredReason.Inactive; } set { retiredReason.Inactive = value; } }
        public string DateStamp { get { return retiredReason.DateStamp; } set { retiredReason.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

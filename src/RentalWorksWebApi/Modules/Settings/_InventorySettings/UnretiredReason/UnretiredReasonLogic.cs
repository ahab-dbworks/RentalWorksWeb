using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.UnretiredReason
{
    [FwLogic(Id:"QmwZR9CsWL70j")]
    public class UnretiredReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        UnretiredReasonRecord unretiredReason = new UnretiredReasonRecord();
        public UnretiredReasonLogic()
        {
            dataRecords.Add(unretiredReason);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"z725l7jCYYbqX", IsPrimaryKey:true)]
        public string UnretiredReasonId { get { return unretiredReason.UnretiredReasonId; } set { unretiredReason.UnretiredReasonId = value; } }

        [FwLogicProperty(Id:"z725l7jCYYbqX", IsRecordTitle:true)]
        public string UnretiredReason { get { return unretiredReason.UnretiredReason; } set { unretiredReason.UnretiredReason = value; } }

        [FwLogicProperty(Id:"Qs6Kk1nUkHdQ")]
        public string ReasonType { get { return unretiredReason.ReasonType; } set { unretiredReason.ReasonType = value; } }

        [FwLogicProperty(Id:"SQ2nGKJqB2Y9")]
        public bool? Inactive { get { return unretiredReason.Inactive; } set { unretiredReason.Inactive = value; } }

        [FwLogicProperty(Id:"VLayl6Sobv3x")]
        public string DateStamp { get { return unretiredReason.DateStamp; } set { unretiredReason.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

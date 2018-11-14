using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PoRejectReason
{
    [FwLogic(Id:"vlJ3478DbYlS")]
    public class PoRejectReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PoRejectReasonRecord poRejectReason = new PoRejectReasonRecord();
        public PoRejectReasonLogic()
        {
            dataRecords.Add(poRejectReason);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"7biUL2CgcwEa", IsPrimaryKey:true)]
        public string PoRejectReasonId { get { return poRejectReason.PoRejectReasonId; } set { poRejectReason.PoRejectReasonId = value; } }

        [FwLogicProperty(Id:"7biUL2CgcwEa", IsRecordTitle:true)]
        public string PoRejectReason { get { return poRejectReason.PoRejectReason; } set { poRejectReason.PoRejectReason = value; } }

        [FwLogicProperty(Id:"WW51V3YVHMma")]
        public bool? Inactive { get { return poRejectReason.Inactive; } set { poRejectReason.Inactive = value; } }

        [FwLogicProperty(Id:"OZCcLotbcQER")]
        public string DateStamp { get { return poRejectReason.DateStamp; } set { poRejectReason.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
} 

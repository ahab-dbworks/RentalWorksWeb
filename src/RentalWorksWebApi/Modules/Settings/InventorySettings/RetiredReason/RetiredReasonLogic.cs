using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventorySettings.RetiredReason
{
    [FwLogic(Id:"RPnS7h6MxuiQO")]
    public class RetiredReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RetiredReasonRecord retiredReason = new RetiredReasonRecord();
        public RetiredReasonLogic()
        {
            dataRecords.Add(retiredReason);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"MdeFcGn9lIGI1", IsPrimaryKey:true)]
        public string RetiredReasonId { get { return retiredReason.RetiredReasonId; } set { retiredReason.RetiredReasonId = value; } }

        [FwLogicProperty(Id:"MdeFcGn9lIGI1", IsRecordTitle:true)]
        public string RetiredReason { get { return retiredReason.RetiredReason; } set { retiredReason.RetiredReason = value; } }

        [FwLogicProperty(Id:"sTkBgW10jDww")]
        public string ReasonType { get { return retiredReason.ReasonType; } set { retiredReason.ReasonType = value; } }

        [FwLogicProperty(Id:"jpZFdA8s9eRN")]
        public bool? Inactive { get { return retiredReason.Inactive; } set { retiredReason.Inactive = value; } }

        [FwLogicProperty(Id:"o3yNplTga0dl")]
        public string DateStamp { get { return retiredReason.DateStamp; } set { retiredReason.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PoSettings.PoApprovalStatus
{
    [FwLogic(Id:"iM7o82nMTHn3")]
    public class PoApprovalStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PoApprovalStatusRecord poApprovalStatus = new PoApprovalStatusRecord();
        public PoApprovalStatusLogic()
        {
            dataRecords.Add(poApprovalStatus);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"gX42aERrbFNX", IsPrimaryKey:true)]
        public string PoApprovalStatusId { get { return poApprovalStatus.PoApprovalStatusId; } set { poApprovalStatus.PoApprovalStatusId = value; } }

        [FwLogicProperty(Id:"gX42aERrbFNX", IsRecordTitle:true)]
        public string PoApprovalStatus { get { return poApprovalStatus.PoApprovalStatus; } set { poApprovalStatus.PoApprovalStatus = value; } }

        [FwLogicProperty(Id:"PBfoa9kMkVag")]
        public string PoApprovalStatusType { get { return poApprovalStatus.PoApprovalStatusType; } set { poApprovalStatus.PoApprovalStatusType = value; } }

        [FwLogicProperty(Id:"2TkVB2XcsOvE")]
        public bool? Inactive { get { return poApprovalStatus.Inactive; } set { poApprovalStatus.Inactive = value; } }

        [FwLogicProperty(Id:"ZPiPjivMrVwR")]
        public string DateStamp { get { return poApprovalStatus.DateStamp; } set { poApprovalStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.PoApprovalStatus
{
    public class PoApprovalStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PoApprovalStatusRecord poApprovalStatus = new PoApprovalStatusRecord();
        public PoApprovalStatusLogic()
        {
            dataRecords.Add(poApprovalStatus);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoApprovalStatusId { get { return poApprovalStatus.PoApprovalStatusId; } set { poApprovalStatus.PoApprovalStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoApprovalStatus { get { return poApprovalStatus.PoApprovalStatus; } set { poApprovalStatus.PoApprovalStatus = value; } }
        public string PoApprovalStatusType { get { return poApprovalStatus.PoApprovalStatusType; } set { poApprovalStatus.PoApprovalStatusType = value; } }
        public bool? Inactive { get { return poApprovalStatus.Inactive; } set { poApprovalStatus.Inactive = value; } }
        public string DateStamp { get { return poApprovalStatus.DateStamp; } set { poApprovalStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
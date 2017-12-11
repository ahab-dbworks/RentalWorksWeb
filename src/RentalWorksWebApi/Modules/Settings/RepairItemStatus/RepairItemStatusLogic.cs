using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.RepairItemStatus
{
    public class RepairItemStatusLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RepairItemStatusRecord repairItemStatus = new RepairItemStatusRecord();
        public RepairItemStatusLogic()
        {
            dataRecords.Add(repairItemStatus);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairItemStatusId { get { return repairItemStatus.RepairItemStatusId; } set { repairItemStatus.RepairItemStatusId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RepairItemStatus { get { return repairItemStatus.RepairItemStatus; } set { repairItemStatus.RepairItemStatus = value; } }
        public bool? Inactive { get { return repairItemStatus.Inactive; } set { repairItemStatus.Inactive = value; } }
        public string DateStamp { get { return repairItemStatus.DateStamp; } set { repairItemStatus.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.RepairItemStatus
{
    [FwLogic(Id:"XEtUv1jy9erwN")]
    public class RepairItemStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        RepairItemStatusRecord repairItemStatus = new RepairItemStatusRecord();
        public RepairItemStatusLogic()
        {
            dataRecords.Add(repairItemStatus);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"OgxAofQcwRksO", IsPrimaryKey:true)]
        public string RepairItemStatusId { get { return repairItemStatus.RepairItemStatusId; } set { repairItemStatus.RepairItemStatusId = value; } }

        [FwLogicProperty(Id:"OgxAofQcwRksO", IsRecordTitle:true)]
        public string RepairItemStatus { get { return repairItemStatus.RepairItemStatus; } set { repairItemStatus.RepairItemStatus = value; } }

        [FwLogicProperty(Id:"jfUK22gHP8Mi")]
        public bool? Inactive { get { return repairItemStatus.Inactive; } set { repairItemStatus.Inactive = value; } }

        [FwLogicProperty(Id:"Uf3vQ3gFgtKM")]
        public string DateStamp { get { return repairItemStatus.DateStamp; } set { repairItemStatus.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

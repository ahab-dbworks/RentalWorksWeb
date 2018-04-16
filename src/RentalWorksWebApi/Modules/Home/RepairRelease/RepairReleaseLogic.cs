using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.RepairRelease
{
    public class RepairReleaseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairReleaseRecord repairRelease = new RepairReleaseRecord();
        RepairReleaseLoader repairReleaseLoader = new RepairReleaseLoader();
        public RepairReleaseLogic()
        {
            dataRecords.Add(repairRelease);
            dataLoader = repairReleaseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairReleaseId { get { return repairRelease.RepairReleaseId; } set { repairRelease.RepairReleaseId = value; } }
        public string RepairId { get { return repairRelease.RepairId; } set { repairRelease.RepairId = value; } }
        public string ReleaseDate { get { return repairRelease.ReleaseDate; } set { repairRelease.ReleaseDate = value; } }
        public string UsersId { get { return repairRelease.UsersId; } set { repairRelease.UsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ReleasedBy { get; set; }
        public decimal? ReleaseQuantity { get { return repairRelease.ReleaseQuantity; } set { repairRelease.ReleaseQuantity = value; } }
        public string DateStamp { get { return repairRelease.DateStamp; } set { repairRelease.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

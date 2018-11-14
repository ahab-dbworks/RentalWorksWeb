using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.RepairRelease
{
    [FwLogic(Id:"ne56IYSJA7rex")]
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
        [FwLogicProperty(Id:"2DglCzCfWcXVq", IsPrimaryKey:true)]
        public string RepairReleaseId { get { return repairRelease.RepairReleaseId; } set { repairRelease.RepairReleaseId = value; } }

        [FwLogicProperty(Id:"7nvXrpViDFTO")]
        public string RepairId { get { return repairRelease.RepairId; } set { repairRelease.RepairId = value; } }

        [FwLogicProperty(Id:"Peatppjo1Lsm")]
        public string ReleaseDate { get { return repairRelease.ReleaseDate; } set { repairRelease.ReleaseDate = value; } }

        [FwLogicProperty(Id:"8E692tHLM9Xy")]
        public string UsersId { get { return repairRelease.UsersId; } set { repairRelease.UsersId = value; } }

        [FwLogicProperty(Id:"zDiqdbS4tKbCC", IsReadOnly:true)]
        public string ReleasedBy { get; set; }

        [FwLogicProperty(Id:"UFSULtzezgdz")]
        public decimal? ReleaseQuantity { get { return repairRelease.ReleaseQuantity; } set { repairRelease.ReleaseQuantity = value; } }

        [FwLogicProperty(Id:"oSUClZAYXjty")]
        public string DateStamp { get { return repairRelease.DateStamp; } set { repairRelease.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

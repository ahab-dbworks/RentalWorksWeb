using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.Floor
{
    [FwLogic(Id:"ksht5SOsdrdq")]
    public class FloorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        FloorRecord floor = new FloorRecord();
        FloorLoader floorLoader = new FloorLoader();
        public FloorLogic()
        {
            dataRecords.Add(floor);
            dataLoader = floorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"vgUrzTlcbsY0", IsPrimaryKey:true)]
        public string FloorId { get { return floor.FloorId; } set { floor.FloorId = value; } }

        [FwLogicProperty(Id:"vgUrzTlcbsY0", IsRecordTitle:true)]
        public string Floor { get { return floor.Floor; } set { floor.Floor = value; } }

        [FwLogicProperty(Id:"yJv6cTmKF7uJ", IsReadOnly:true)]
        public string BuildingId { get { return floor.BuildingId; } set { floor.BuildingId = value; } }

        [FwLogicProperty(Id:"nFcm8w6OdfG")]
        public decimal? SquareFeet { get; set; }

        [FwLogicProperty(Id:"DtvlNZIVhnv0", IsReadOnly:true)]
        public decimal? CommonSquareFeet { get; set; }

        [FwLogicProperty(Id:"2ZFIYtjGQ6T")]
        public string FloorPlanId { get { return floor.FloorPlanId; } set { floor.FloorPlanId = value; } }

        [FwLogicProperty(Id:"vgUrzTlcbsY0", IsReadOnly:true)]
        public bool? HasFloorPlan { get; set; }

        [FwLogicProperty(Id:"84GY1eSZjt1")]
        public int? OrderBy { get { return floor.OrderBy; } set { floor.OrderBy = value; } }

        [FwLogicProperty(Id:"Qinzwv6vgeA")]
        public string DateStamp { get { return floor.DateStamp; } set { floor.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

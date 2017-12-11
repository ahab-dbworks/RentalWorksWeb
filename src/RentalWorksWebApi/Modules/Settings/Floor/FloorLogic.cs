using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.Floor
{
    public class FloorLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string FloorId { get { return floor.FloorId; } set { floor.FloorId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Floor { get { return floor.Floor; } set { floor.Floor = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BuildingId { get { return floor.BuildingId; } set { floor.BuildingId = value; } }
        public decimal? SquareFeet { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? CommonSquareFeet { get; set; }
        public string FloorPlanId { get { return floor.FloorPlanId; } set { floor.FloorPlanId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasFloorPlan { get; set; }
        public int? OrderBy { get { return floor.OrderBy; } set { floor.OrderBy = value; } }
        public string DateStamp { get { return floor.DateStamp; } set { floor.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
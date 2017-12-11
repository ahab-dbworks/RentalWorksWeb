using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.SpaceRate
{
    public class SpaceRateLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SpaceRateRecord spaceRate = new SpaceRateRecord();
        SpaceRateLoader spaceRateLoader = new SpaceRateLoader();
        public SpaceRateLogic()
        {
            dataRecords.Add(spaceRate);
            dataLoader = spaceRateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SpaceRateId { get { return spaceRate.SpaceRateId; } set { spaceRate.SpaceRateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BuildingId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FloorId { get; set; }
        public string SpaceId { get { return spaceRate.SpaceId; } set { spaceRate.SpaceId = value; } }
        public string FacilityTypeId { get { return spaceRate.FacilityTypeId; } set { spaceRate.FacilityTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }
        public string SpaceTypeId { get { return spaceRate.SpaceTypeId; } set { spaceRate.SpaceTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SpaceType { get; set; }
        public string RateId { get { return spaceRate.RateId; } set { spaceRate.RateId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Price { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? HourlyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DailyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Week2Rate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Week3Rate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Week4Rate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Week5Rate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? StageScheduling { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UnitId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SpaceTypeClassification { get; set; }
        public int? OrderBy { get { return spaceRate.OrderBy; } set { spaceRate.OrderBy = value; } }
        public string DateStamp { get { return spaceRate.DateStamp; } set { spaceRate.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
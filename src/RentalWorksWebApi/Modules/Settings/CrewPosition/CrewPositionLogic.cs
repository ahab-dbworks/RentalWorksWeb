using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.CrewPosition
{
    public class CrewPositionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CrewPositionRecord crewPosition = new CrewPositionRecord();
        CrewPositionLoader crewPositionLoader = new CrewPositionLoader();
        public CrewPositionLogic()
        {
            dataRecords.Add(crewPosition);
            dataLoader = crewPositionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CrewPositionId { get { return crewPosition.CrewPositionId; } set { crewPosition.CrewPositionId = value; } }
        public string CrewId { get { return crewPosition.CrewId; } set { crewPosition.CrewId = value; } }
        public string RateId { get { return crewPosition.RateId; } set { crewPosition.RateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public decimal? CostHourly { get { return crewPosition.CostHourly; } set { crewPosition.CostHourly = value; } }
        public decimal? CostDaily { get { return crewPosition.CostDaily; } set { crewPosition.CostDaily = value; } }
        public decimal? CostWeekly { get { return crewPosition.CostWeekly; } set { crewPosition.CostWeekly = value; } }
        public decimal? CostMonthly { get { return crewPosition.CostMonthly; } set { crewPosition.CostMonthly = value; } }
        public bool? IsPrimary { get { return crewPosition.IsPrimary; } set { crewPosition.IsPrimary = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EffectiveDate { get { return crewPosition.EffectiveDate; } set { crewPosition.EffectiveDate = value; } }
        public string EndDate { get { return crewPosition.EndDate; } set { crewPosition.EndDate = value; } }
        public bool? Inactive { get; set; }
        public string DateStamp { get { return crewPosition.DateStamp; } set { crewPosition.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
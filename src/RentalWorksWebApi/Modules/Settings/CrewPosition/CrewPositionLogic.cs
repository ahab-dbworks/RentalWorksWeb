using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.CrewPosition
{
    [FwLogic(Id:"jBJ33XqjxgYY")]
    public class CrewPositionLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"p90TuojzmaYP", IsPrimaryKey:true)]
        public string CrewPositionId { get { return crewPosition.CrewPositionId; } set { crewPosition.CrewPositionId = value; } }

        [FwLogicProperty(Id:"TN76c9CDRhgX")]
        public string CrewId { get { return crewPosition.CrewId; } set { crewPosition.CrewId = value; } }

        [FwLogicProperty(Id:"00mYXm0nATWD")]
        public string RateId { get { return crewPosition.RateId; } set { crewPosition.RateId = value; } }

        [FwLogicProperty(Id:"VZXqfcGjlN8w", IsReadOnly:true)]
        public string LaborTypeId { get; set; }

        [FwLogicProperty(Id:"VZXqfcGjlN8w", IsReadOnly:true)]
        public string LaborType { get; set; }

        [FwLogicProperty(Id:"D4SGJDx51vtl", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"vDoAqDuepAAS")]
        public decimal? CostHourly { get { return crewPosition.CostHourly; } set { crewPosition.CostHourly = value; } }

        [FwLogicProperty(Id:"biLObeVzKpBM")]
        public decimal? CostDaily { get { return crewPosition.CostDaily; } set { crewPosition.CostDaily = value; } }

        [FwLogicProperty(Id:"tRmGnC67y2ra")]
        public decimal? CostWeekly { get { return crewPosition.CostWeekly; } set { crewPosition.CostWeekly = value; } }

        [FwLogicProperty(Id:"VjPxnTbtYiCD")]
        public decimal? CostMonthly { get { return crewPosition.CostMonthly; } set { crewPosition.CostMonthly = value; } }

        [FwLogicProperty(Id:"yuzehNeayk8K")]
        public bool? IsPrimary { get { return crewPosition.IsPrimary; } set { crewPosition.IsPrimary = value; } }

        [FwLogicProperty(Id:"8ChXWbf3Tm03", IsReadOnly:true)]
        public string EffectiveDate { get { return crewPosition.EffectiveDate; } set { crewPosition.EffectiveDate = value; } }

        [FwLogicProperty(Id:"OdirTUmVtKKN")]
        public string EndDate { get { return crewPosition.EndDate; } set { crewPosition.EndDate = value; } }

        [FwLogicProperty(Id:"09VgJgz2hxiu")]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id:"7nTlDylLXPZv")]
        public string DateStamp { get { return crewPosition.DateStamp; } set { crewPosition.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

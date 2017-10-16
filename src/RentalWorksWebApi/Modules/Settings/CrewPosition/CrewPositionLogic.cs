using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Home.Inventory;
using RentalWorksWebApi.Modules.Settings.Rate;

namespace RentalWorksWebApi.Modules.Settings.CrewPosition
{
    public class CrewPositionLogic : RateLogic 
    {
        //------------------------------------------------------------------------------------ 
        CrewPositionLoader crewPositionLoader = new CrewPositionLoader();
        public CrewPositionLogic()
        {
            dataLoader = crewPositionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PositionId { get { return master.MasterId; } set { master.MasterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }

        public override void BeforeSave()
        {
            AvailFor = "L";
            Classification = "LP";
        }
        //------------------------------------------------------------------------------------ 
    }
}
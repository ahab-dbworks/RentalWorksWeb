using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.CrewLocation
{
    public class CrewLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CrewLocationRecord crewLocation = new CrewLocationRecord();
        CrewLocationLoader crewLocationLoader = new CrewLocationLoader();
        public CrewLocationLogic()
        {
            dataRecords.Add(crewLocation);
            dataLoader = crewLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CrewLocationId { get { return crewLocation.CrewLocationId; } set { crewLocation.CrewLocationId = value; } }
        public string CrewId { get { return crewLocation.CrewId; } set { crewLocation.CrewId = value; } }
        public string LocationId { get { return crewLocation.LocationId; } set { crewLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public bool? IsPrimary { get { return crewLocation.IsPrimary; } set { crewLocation.IsPrimary = value; } }
        public string DateStamp { get { return crewLocation.DateStamp; } set { crewLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
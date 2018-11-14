using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.CrewLocation
{
    [FwLogic(Id:"EB3ShYXAXeq6")]
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
        [FwLogicProperty(Id:"uo9BZZH9XvbN", IsPrimaryKey:true)]
        public string CrewLocationId { get { return crewLocation.CrewLocationId; } set { crewLocation.CrewLocationId = value; } }

        [FwLogicProperty(Id:"8u9AxxVz3O5L")]
        public string CrewId { get { return crewLocation.CrewId; } set { crewLocation.CrewId = value; } }

        [FwLogicProperty(Id:"5Y2FUZ5IYq6v")]
        public string LocationId { get { return crewLocation.LocationId; } set { crewLocation.LocationId = value; } }

        [FwLogicProperty(Id:"0OemWePT2s8K", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"xvluRA7BJlvW")]
        public bool? IsPrimary { get { return crewLocation.IsPrimary; } set { crewLocation.IsPrimary = value; } }

        [FwLogicProperty(Id:"iWWdJKdnLeNy")]
        public string DateStamp { get { return crewLocation.DateStamp; } set { crewLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.MasterLocation
{
    public class MasterLocationLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterLocationRecord masterLocation = new MasterLocationRecord();
        MasterLocationLoader masterLocationLoader = new MasterLocationLoader();
        public MasterLocationLogic()
        {
            dataRecords.Add(masterLocation);
            dataLoader = masterLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string MasterId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }
        public string LocationId { get { return masterLocation.LocationId; } set { masterLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public bool Taxable { get { return masterLocation.Taxable; } set { masterLocation.Taxable = value; } }
        public string ModByUsersId { get { return masterLocation.ModByUsersId; } set { masterLocation.ModByUsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ModByUser { get; set; }
        public string DateStamp { get { return masterLocation.DateStamp; } set { masterLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
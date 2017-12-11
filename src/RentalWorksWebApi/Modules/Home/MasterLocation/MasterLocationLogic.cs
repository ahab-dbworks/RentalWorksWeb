using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.MasterLocation
{
    public abstract class MasterLocationLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterLocationRecord masterLocation = new MasterLocationRecord();
        public MasterLocationLogic()
        {
            dataRecords.Add(masterLocation);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string Id { get { return masterLocation.Id; } set { masterLocation.Id = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InternalChar { get { return masterLocation.InternalChar; } set { masterLocation.InternalChar = value; } }
        //public string MasterId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }
        public string LocationId { get { return masterLocation.LocationId; } set { masterLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Location { get; set; }
        public bool? Taxable { get { return masterLocation.Taxable; } set { masterLocation.Taxable = value; } }
        public string ModByUsersId { get { return masterLocation.ModByUsersId; } set { masterLocation.ModByUsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ModByUser { get; set; }
        public string DateStamp { get { return masterLocation.DateStamp; } set { masterLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
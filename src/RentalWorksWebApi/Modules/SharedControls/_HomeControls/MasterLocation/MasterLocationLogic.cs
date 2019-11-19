using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.MasterLocation
{
    [FwLogic(Id:"Wiu8IkYwlo1m")]
    public abstract class MasterLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterLocationRecord masterLocation = new MasterLocationRecord();
        public MasterLocationLogic()
        {
            dataRecords.Add(masterLocation);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"0HksWsb2mFRm", IsPrimaryKey:true)]
        public int? Id { get { return masterLocation.Id; } set { masterLocation.Id = value; } }

        [FwLogicProperty(Id:"XgG4xZ7p4Es7", IsPrimaryKey:true, IsPrimaryKeyOptional: true)]
        public string InternalChar { get { return masterLocation.InternalChar; } set { masterLocation.InternalChar = value; } }

        //[FwLogicProperty(Id:"FnDjjDruAmSr")]
        //public string MasterId { get { return masterLocation.MasterId; } set { masterLocation.MasterId = value; } }

        [FwLogicProperty(Id:"I4mqe4lGvkgc")]
        public string LocationId { get { return masterLocation.LocationId; } set { masterLocation.LocationId = value; } }

        [FwLogicProperty(Id:"Mq3QQJEI8Ytj", IsRecordTitle:true, IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"hzSr29DaaboL")]
        public bool? Taxable { get { return masterLocation.Taxable; } set { masterLocation.Taxable = value; } }

        [FwLogicProperty(Id:"IL8Xyy5dNxZy")]
        public string ModByUsersId { get { return masterLocation.ModByUsersId; } set { masterLocation.ModByUsersId = value; } }

        [FwLogicProperty(Id:"WYal36OACiJg", IsReadOnly:true)]
        public string ModByUser { get; set; }

        [FwLogicProperty(Id:"nGO8Ml7A1M3t")]
        public string DateStamp { get { return masterLocation.DateStamp; } set { masterLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

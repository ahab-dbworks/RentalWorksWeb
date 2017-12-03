using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.WallType
{
    public class WallTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WallTypeRecord wallType = new WallTypeRecord();
        public WallTypeLogic()
        {
            dataRecords.Add(wallType);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WallTypeId { get { return wallType.WallTypeId; } set { wallType.WallTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WallType { get { return wallType.WallType; } set { wallType.WallType = value; } }
        public bool? Inactive { get { return wallType.Inactive; } set { wallType.Inactive = value; } }
        public string DateStamp { get { return wallType.DateStamp; } set { wallType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.SetSettings.WallType
{
    [FwLogic(Id:"pXisPynEBhkPA")]
    public class WallTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WallTypeRecord wallType = new WallTypeRecord();
        public WallTypeLogic()
        {
            dataRecords.Add(wallType);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"RdDK9dHfpP1qj", IsPrimaryKey:true)]
        public string WallTypeId { get { return wallType.WallTypeId; } set { wallType.WallTypeId = value; } }

        [FwLogicProperty(Id:"RdDK9dHfpP1qj", IsRecordTitle:true)]
        public string WallType { get { return wallType.WallType; } set { wallType.WallType = value; } }

        [FwLogicProperty(Id:"sPtJQ6hveCFM")]
        public bool? Inactive { get { return wallType.Inactive; } set { wallType.Inactive = value; } }

        [FwLogicProperty(Id:"EAoy8UuV8c9I")]
        public string DateStamp { get { return wallType.DateStamp; } set { wallType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

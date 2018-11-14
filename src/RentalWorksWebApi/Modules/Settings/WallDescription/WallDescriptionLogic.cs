using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WallDescription
{
    [FwLogic(Id:"8MlOQD5RvFlci")]
    public class WallDescriptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WallDescriptionRecord wallDescription = new WallDescriptionRecord();
        public WallDescriptionLogic()
        {
            dataRecords.Add(wallDescription);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"KuDIJf5G2p7BN", IsPrimaryKey:true)]
        public string WallDescriptionId { get { return wallDescription.WallDescriptionId; } set { wallDescription.WallDescriptionId = value; } }

        [FwLogicProperty(Id:"KuDIJf5G2p7BN", IsRecordTitle:true)]
        public string WallDescription { get { return wallDescription.WallDescription; } set { wallDescription.WallDescription = value; } }

        [FwLogicProperty(Id:"dprxXITxXGJz")]
        public bool? Inactive { get { return wallDescription.Inactive; } set { wallDescription.Inactive = value; } }

        [FwLogicProperty(Id:"tZnrpmFfCNXK")]
        public string DateStamp { get { return wallDescription.DateStamp; } set { wallDescription.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

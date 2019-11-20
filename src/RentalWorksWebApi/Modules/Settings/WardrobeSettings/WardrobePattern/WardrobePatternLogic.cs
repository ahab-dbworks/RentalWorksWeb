using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobePattern
{
    [FwLogic(Id:"yPzkJBdm57FWQ")]
    public class WardrobePatternLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobePatternRecord wardrobePattern = new WardrobePatternRecord();
        public WardrobePatternLogic()
        {
            dataRecords.Add(wardrobePattern);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"5m8MlxnxBdDDY", IsPrimaryKey:true)]
        public string WardrobePatternId { get { return wardrobePattern.WardrobePatternId; } set { wardrobePattern.WardrobePatternId = value; } }

        [FwLogicProperty(Id:"5m8MlxnxBdDDY", IsRecordTitle:true)]
        public string WardrobePattern { get { return wardrobePattern.WardrobePattern; } set { wardrobePattern.WardrobePattern = value; } }

        [FwLogicProperty(Id:"soGWTHD5N7A")]
        public bool? Inactive { get { return wardrobePattern.Inactive; } set { wardrobePattern.Inactive = value; } }

        [FwLogicProperty(Id:"TJxkMzbKazw")]
        public string DateStamp { get { return wardrobePattern.DateStamp; } set { wardrobePattern.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

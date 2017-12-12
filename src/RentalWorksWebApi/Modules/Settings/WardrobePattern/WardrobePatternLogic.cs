using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobePattern
{
    public class WardrobePatternLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobePatternRecord wardrobePattern = new WardrobePatternRecord();
        public WardrobePatternLogic()
        {
            dataRecords.Add(wardrobePattern);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobePatternId { get { return wardrobePattern.WardrobePatternId; } set { wardrobePattern.WardrobePatternId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobePattern { get { return wardrobePattern.WardrobePattern; } set { wardrobePattern.WardrobePattern = value; } }
        public bool? Inactive { get { return wardrobePattern.Inactive; } set { wardrobePattern.Inactive = value; } }
        public string DateStamp { get { return wardrobePattern.DateStamp; } set { wardrobePattern.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

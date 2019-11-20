using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PoSettings.PoClassification
{
    [FwLogic(Id:"aVOcxqvHYF0n")]
    public class PoClassificationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PoClassificationRecord poClassification = new PoClassificationRecord();
        public PoClassificationLogic()
        {
            dataRecords.Add(poClassification);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"MxlzuAHz05qR", IsPrimaryKey:true)]
        public string PoClassificationId { get { return poClassification.PoClassificationId; } set { poClassification.PoClassificationId = value; } }

        [FwLogicProperty(Id:"MxlzuAHz05qR", IsRecordTitle:true)]
        public string PoClassification { get { return poClassification.PoClassification; } set { poClassification.PoClassification = value; } }

        [FwLogicProperty(Id:"9kQeyXwG9ybK")]
        public bool? ExcludeFromRoa { get { return poClassification.ExcludeFromRoa; } set { poClassification.ExcludeFromRoa = value; } }

        [FwLogicProperty(Id:"J54qhQXQHR47")]
        public bool? Inactive { get { return poClassification.Inactive; } set { poClassification.Inactive = value; } }

        [FwLogicProperty(Id:"kJZvnhFqDQ7J")]
        public string DateStamp { get { return poClassification.DateStamp; } set { poClassification.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

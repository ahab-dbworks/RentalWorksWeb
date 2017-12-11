using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PoClassification
{
    public class PoClassificationLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PoClassificationRecord poClassification = new PoClassificationRecord();
        public PoClassificationLogic()
        {
            dataRecords.Add(poClassification);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoClassificationId { get { return poClassification.PoClassificationId; } set { poClassification.PoClassificationId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoClassification { get { return poClassification.PoClassification; } set { poClassification.PoClassification = value; } }
        public bool? ExcludeFromRoa { get { return poClassification.ExcludeFromRoa; } set { poClassification.ExcludeFromRoa = value; } }
        public bool? Inactive { get { return poClassification.Inactive; } set { poClassification.Inactive = value; } }
        public string DateStamp { get { return poClassification.DateStamp; } set { poClassification.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

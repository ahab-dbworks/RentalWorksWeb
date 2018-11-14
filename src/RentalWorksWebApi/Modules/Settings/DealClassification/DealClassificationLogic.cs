using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DealClassification
{
    [FwLogic(Id:"XkxUij23lfCa")]
    public class DealClassificationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealClassificationRecord dealClassification = new DealClassificationRecord();
        public DealClassificationLogic()
        {
            dataRecords.Add(dealClassification);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ZDZOyqjTiBbu", IsPrimaryKey:true)]
        public string DealClassificationId { get { return dealClassification.DealClassificationId; } set { dealClassification.DealClassificationId = value; } }

        [FwLogicProperty(Id:"ZDZOyqjTiBbu", IsRecordTitle:true)]
        public string DealClassification { get { return dealClassification.DealClassification; } set { dealClassification.DealClassification = value; } }

        [FwLogicProperty(Id:"d67s0Uk2OOBx")]
        public bool? Inactive { get { return dealClassification.Inactive; } set { dealClassification.Inactive = value; } }

        [FwLogicProperty(Id:"zGrNUoeiiQxF")]
        public string DateStamp { get { return dealClassification.DateStamp; } set { dealClassification.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OrderSettings.DiscountReason
{
    [FwLogic(Id:"EiUUjsQrVEop")]
    public class DiscountReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DiscountReasonRecord discountReason = new DiscountReasonRecord();
        public DiscountReasonLogic()
        {
            dataRecords.Add(discountReason);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"AdDQBm0zKuCf", IsPrimaryKey:true)]
        public string DiscountReasonId { get { return discountReason.DiscountReasonId; } set { discountReason.DiscountReasonId = value; } }

        [FwLogicProperty(Id:"AdDQBm0zKuCf", IsRecordTitle:true)]
        public string DiscountReason { get { return discountReason.DiscountReason; } set { discountReason.DiscountReason = value; } }

        [FwLogicProperty(Id:"tnmMBOf517Cw")]
        public bool? Inactive { get { return discountReason.Inactive; } set { discountReason.Inactive = value; } }

        [FwLogicProperty(Id:"KcwoKrMk92oa")]
        public string DateStamp { get { return discountReason.DateStamp; } set { discountReason.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

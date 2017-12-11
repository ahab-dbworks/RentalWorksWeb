using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DiscountReason
{
    public class DiscountReasonLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DiscountReasonRecord discountReason = new DiscountReasonRecord();
        public DiscountReasonLogic()
        {
            dataRecords.Add(discountReason);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DiscountReasonId { get { return discountReason.DiscountReasonId; } set { discountReason.DiscountReasonId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DiscountReason { get { return discountReason.DiscountReason; } set { discountReason.DiscountReason = value; } }
        public bool? Inactive { get { return discountReason.Inactive; } set { discountReason.Inactive = value; } }
        public string DateStamp { get { return discountReason.DateStamp; } set { discountReason.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

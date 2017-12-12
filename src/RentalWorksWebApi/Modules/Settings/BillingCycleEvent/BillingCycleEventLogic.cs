using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Settings.BillPeriodEventPercent;

namespace WebApi.Modules.Settings.BillingCycleEvent
{
    public class BillingCycleEventLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        BillPeriodEventPercentRecord billPeriodEventPercent = new BillPeriodEventPercentRecord();
        BillingCycleEventLoader billPeriodEventPercentLoader = new BillingCycleEventLoader();
        public BillingCycleEventLogic()
        {
            dataRecords.Add(billPeriodEventPercent);
            dataLoader = billPeriodEventPercentLoader;
        }
        //------------------------------------------------------------------------------------
        public string BillingCycleId { get { return billPeriodEventPercent.BillPeriodId; } set { billPeriodEventPercent.BillPeriodId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string BillingCycleEventId { get { return billPeriodEventPercent.BillPeriodEventPercentId; } set { billPeriodEventPercent.BillPeriodEventPercentId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string BillingCycleEvent { get { return billPeriodEventPercent.BillPeriodEvent; } set { billPeriodEventPercent.BillPeriodEvent = value; } }
        public int? BillPercent { get { return billPeriodEventPercent.BillPercent;  } set { billPeriodEventPercent.BillPercent = value; } }
        public int? OrderBy { get { return billPeriodEventPercent.OrderBy;  } set { billPeriodEventPercent.OrderBy = value; } }
        public bool? ActualRevenue { get { return billPeriodEventPercent.Revenue;  } set { billPeriodEventPercent.Revenue = value; } }
        public string DateStamp { get { return billPeriodEventPercent.DateStamp; } set { billPeriodEventPercent.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

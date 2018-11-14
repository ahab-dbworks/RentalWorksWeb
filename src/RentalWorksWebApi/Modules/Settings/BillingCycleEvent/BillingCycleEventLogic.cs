using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Settings.BillPeriodEventPercent;

namespace WebApi.Modules.Settings.BillingCycleEvent
{
    [FwLogic(Id:"89Mip6botUZ")]
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
        [FwLogicProperty(Id:"9ulCXDTFEZ76")]
        public string BillingCycleId { get { return billPeriodEventPercent.BillPeriodId; } set { billPeriodEventPercent.BillPeriodId = value; } }

        [FwLogicProperty(Id:"WWaJW9BlUuW", IsPrimaryKey:true)]
        public string BillingCycleEventId { get { return billPeriodEventPercent.BillPeriodEventPercentId; } set { billPeriodEventPercent.BillPeriodEventPercentId = value; } }

        [FwLogicProperty(Id:"WWaJW9BlUuW", IsRecordTitle:true)]
        public string BillingCycleEvent { get { return billPeriodEventPercent.BillPeriodEvent; } set { billPeriodEventPercent.BillPeriodEvent = value; } }

        [FwLogicProperty(Id:"U6dsTFsF29NJ")]
        public int? BillPercent { get { return billPeriodEventPercent.BillPercent;  } set { billPeriodEventPercent.BillPercent = value; } }

        [FwLogicProperty(Id:"BoChnkMz8G8N")]
        public int? OrderBy { get { return billPeriodEventPercent.OrderBy;  } set { billPeriodEventPercent.OrderBy = value; } }

        [FwLogicProperty(Id:"jCnLuRs10eoK")]
        public bool? ActualRevenue { get { return billPeriodEventPercent.Revenue;  } set { billPeriodEventPercent.Revenue = value; } }

        [FwLogicProperty(Id:"inh8l7XvbjTW")]
        public string DateStamp { get { return billPeriodEventPercent.DateStamp; } set { billPeriodEventPercent.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

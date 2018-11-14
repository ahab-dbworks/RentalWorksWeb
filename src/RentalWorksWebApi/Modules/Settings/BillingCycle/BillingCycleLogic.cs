using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Settings.BillPeriod;

namespace WebApi.Modules.Settings.BillingCycle
{
    [FwLogic(Id:"v8jnO0G7ekh")]
    public class BillingCycleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        BillPeriodRecord billPeriod = new BillPeriodRecord();
        BillingCycleLoader billingCycle = new BillingCycleLoader();
        public BillingCycleLogic()
        {
            dataRecords.Add(billPeriod);
            dataLoader = billingCycle;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"afuxK88BggG", IsPrimaryKey:true)]
        public string BillingCycleId { get { return billPeriod.BillPeriodId; } set { billPeriod.BillPeriodId = value; } }

        [FwLogicProperty(Id:"afuxK88BggG", IsRecordTitle:true)]
        public string BillingCycle { get { return billPeriod.BillPeriod; } set { billPeriod.BillPeriod = value; } }

        [FwLogicProperty(Id:"zvNH3jpOG6bm")]
        public string BillingCycleType { get { return billPeriod.PeriodType; } set { billPeriod.PeriodType = value; } }

        [FwLogicProperty(Id:"ag4Sck0FN0Xx")]
        public string NextBillingCycleId { get { return billPeriod.NextBillPeriodId; } set { billPeriod.NextBillPeriodId = value; } }

        [FwLogicProperty(Id:"afuxK88BggG", IsReadOnly:true)]
        public string NextBillingCycle { get; set; }

        [FwLogicProperty(Id:"y6fMgif2x1bq")]
        public bool? ProrateMonthly { get { return billPeriod.ProrateMonthly; } set { billPeriod.ProrateMonthly = value; } }

        [FwLogicProperty(Id:"j7sR6VlKe7ar")]
        public bool? Inactive { get { return billPeriod.Inactive; } set { billPeriod.Inactive = value; } }

        [FwLogicProperty(Id:"sKJfRhk4PIY1")]
        public string DateStamp { get { return billPeriod.DateStamp; } set { billPeriod.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

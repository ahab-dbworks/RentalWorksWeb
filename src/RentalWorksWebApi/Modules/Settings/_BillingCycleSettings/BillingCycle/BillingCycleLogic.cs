using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi.Modules.Settings.BillPeriod;
using WebLibrary;

namespace WebApi.Modules.Settings.BillingCycleSettings.BillingCycle
{
    [FwLogic(Id: "v8jnO0G7ekh")]
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
        [FwLogicProperty(Id: "afuxK88BggG", IsPrimaryKey: true)]
        public string BillingCycleId { get { return billPeriod.BillPeriodId; } set { billPeriod.BillPeriodId = value; } }

        [FwLogicProperty(Id: "afuxK88BggG", IsRecordTitle: true)]
        public string BillingCycle { get { return billPeriod.BillPeriod; } set { billPeriod.BillPeriod = value; } }

        [FwLogicProperty(Id: "zvNH3jpOG6bm")]
        public string BillingCycleType { get { return billPeriod.PeriodType; } set { billPeriod.PeriodType = value; } }

        [FwLogicProperty(Id: "ag4Sck0FN0Xx")]
        public string NextBillingCycleId { get { return billPeriod.NextBillPeriodId; } set { billPeriod.NextBillPeriodId = value; } }

        [FwLogicProperty(Id: "afuxK88BggG", IsReadOnly: true)]
        public string NextBillingCycle { get; set; }

        [FwLogicProperty(Id: "y6fMgif2x1bq")]
        public bool? ProrateMonthly { get { return billPeriod.ProrateMonthly; } set { billPeriod.ProrateMonthly = value; } }

        [FwLogicProperty(Id: "dV5Rstjecwj3f")]
        public string BillOnPeriodStartOrEnd { get { return billPeriod.BillOnPeriodStartOrEnd; } set { billPeriod.BillOnPeriodStartOrEnd = value; } }

        [FwLogicProperty(Id: "j7sR6VlKe7ar")]
        public bool? Inactive { get { return billPeriod.Inactive; } set { billPeriod.Inactive = value; } }

        [FwLogicProperty(Id: "sKJfRhk4PIY1")]
        public string DateStamp { get { return billPeriod.DateStamp; } set { billPeriod.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (isValid)
            {
                PropertyInfo property = typeof(BillingCycleLogic).GetProperty(nameof(BillingCycleLogic.BillingCycleType));
                string[] acceptableValues = {
                                             RwConstants.BILLING_CYCLE_TYPE_WEEKLY,
                                             RwConstants.BILLING_CYCLE_TYPE_BIWEEKLY,
                                             RwConstants.BILLING_CYCLE_TYPE_4WEEKLY,
                                             RwConstants.BILLING_CYCLE_TYPE_MONTHLY,
                                             RwConstants.BILLING_CYCLE_TYPE_CALENDARMONTH,
                                             RwConstants.BILLING_CYCLE_TYPE_EPISODIC,
                                             RwConstants.BILLING_CYCLE_TYPE_IMMEDIATE,
                                             RwConstants.BILLING_CYCLE_TYPE_RATECHANGE,
                                             RwConstants.BILLING_CYCLE_TYPE_EVENTS,
                                             RwConstants.BILLING_CYCLE_TYPE_ONDEMAND,
                                             RwConstants.BILLING_CYCLE_TYPE_ATCLOSE,
                                             RwConstants.BILLING_CYCLE_TYPE_DECREASING
                                             };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                PropertyInfo property = typeof(BillingCycleLogic).GetProperty(nameof(BillingCycleLogic.BillOnPeriodStartOrEnd));
                string[] acceptableValues = { RwConstants.BILLING_CYCLE_BILL_ON_PERIOD_START, RwConstants.BILLING_CYCLE_BILL_ON_PERIOD_END };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;

        }
        //------------------------------------------------------------------------------------
    }
}

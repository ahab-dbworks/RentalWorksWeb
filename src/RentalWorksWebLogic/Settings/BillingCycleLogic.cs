using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class BillingCycleLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string BillingCycleId { get { return billPeriod.BillPeriodId; } set { billPeriod.BillPeriodId = value; } }
        [FwBusinessLogicField(isTitle: true)]
        public string BillingCycle { get { return billPeriod.BillPeriod; } set { billPeriod.BillPeriod = value; } }
        public string BillingCycleType { get { return billPeriod.PeriodType; } set { billPeriod.PeriodType = value; } }
        public string NextBillingCycleId { get { return billPeriod.NextBillPeriodId; } set { billPeriod.NextBillPeriodId = value; } }
        public bool ProrateMonthly { get { return billPeriod.ProrateMonthly; } set { billPeriod.ProrateMonthly = value; } }
        public bool Inactive { get { return billPeriod.Inactive; } set { billPeriod.Inactive = value; } }
        public string DateStamp { get { return billPeriod.DateStamp; } set { billPeriod.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.BillingCycleSettings.BillingCycle
{
    [FwSqlTable("billingcycleview")]
    public class BillingCycleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string BillingCycleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycle", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycletype", modeltype: FwDataTypes.Text)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillingcycleid", modeltype: FwDataTypes.Text)]
        public string NextBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillingcycle", modeltype: FwDataTypes.Text)]
        public string NextBillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proratemonthly", modeltype: FwDataTypes.Boolean)]
        public bool? ProrateMonthly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billonperiodstartend", modeltype: FwDataTypes.Text)]
        public string BillOnPeriodStartOrEnd { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.BillingCycle
{
    [FwSqlTable("billingcycleview")]
    public class BillingCycleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycleid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string BillingCycleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycle", modeltype: FwDataTypes.Text, maxlength: 12)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingcycletype", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillingcycleid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string NextBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillingcycle", modeltype: FwDataTypes.Text, maxlength: 12)]
        public string NextBillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proratemonthly", modeltype: FwDataTypes.Boolean)]
        public bool? ProrateMonthly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

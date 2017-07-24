using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("billingcycleview")]
    public class BillingCycleLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "billingcycleid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string BillingCycleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "billingcycle", dataType: FwDataTypes.Text, length: 12)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "billingcycletype", dataType: FwDataTypes.Text, length: 10)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "nextbillingcycleid", dataType: FwDataTypes.Text, length: 8)]
        public string NextBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "proratemonthly", dataType: FwDataTypes.Boolean)]
        public bool ProrateMonthly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

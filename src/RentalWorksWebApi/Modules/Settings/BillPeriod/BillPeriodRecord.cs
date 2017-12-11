using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.BillPeriod
{
    [FwSqlTable("billperiod")]
    public class BillPeriodRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string BillPeriodId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text, maxlength: 12, required: true)]
        public string BillPeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string PeriodType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillperiodid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string NextBillPeriodId { get; set; }
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

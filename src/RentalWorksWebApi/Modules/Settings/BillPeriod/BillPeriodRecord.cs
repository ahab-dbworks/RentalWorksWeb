using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.BillPeriod
{
    [FwSqlTable("billperiod")]
    public class BillPeriodRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8, isPrimaryKey: true)]
        public string BillPeriodId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 12, required: true)]
        public string BillPeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, required: true)]
        public string PeriodType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nextbillperiodid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string NextBillPeriodId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proratemonthly", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? ProrateMonthly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billonperiodstartend", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, required: true)]
        public string BillOnPeriodStartOrEnd { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prorateweeklywithmaxdw", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? ProrateWeeklyWithMaxDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

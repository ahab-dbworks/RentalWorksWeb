using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("billperiod")]
    public class BillPeriodRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "billperiodid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string BillPeriodId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "billperiod", dataType: FwDataTypes.Text, length: 12)]
        public string BillPeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "periodtype", dataType: FwDataTypes.Text, length: 10)]
        public string PeriodType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "nextbillperiodid", dataType: FwDataTypes.Text, length: 8)]
        public string NextBillPeriodId { get; set; }
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

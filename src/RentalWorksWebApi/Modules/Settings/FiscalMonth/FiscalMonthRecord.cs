using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.FiscalMonth
{
    [FwSqlTable("fiscalmonth")]
    public class FiscalMonthRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalmonthid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string FiscalMonthId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalyearid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string FiscalYearId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Month { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "workdays", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WorkDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalorder", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FiscalOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "closed", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Closed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
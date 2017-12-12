using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.FiscalYear
{
    [FwSqlTable("fiscalyear")]
    public class FiscalYearRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalyearid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string FiscalYearId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4, required: true)]
        public string Year { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
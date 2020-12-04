using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.DataHealth
{
    [FwSqlTable("datahealth")]
    public class DataHealthRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datahealthid", modeltype: FwDataTypes.Integer, sqltype: "int", identity: true, isPrimaryKey: true)]
        public int? DataHealthId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datahealthtype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string DataHealthType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "capturedatetime", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string CaptureDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "severity", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Severity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "resolved", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Resolved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

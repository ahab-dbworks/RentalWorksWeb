using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.Alert
{
    [FwSqlTable("webalert")]
    public class AlertRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AlertName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

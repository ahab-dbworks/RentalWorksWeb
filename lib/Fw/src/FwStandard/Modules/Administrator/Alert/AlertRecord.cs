using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace FwStandard.Modules.Administrator.Alert
{
    [FwSqlTable("webalert")]
    public class AlertRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string AlertId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AlertName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actionnew", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ActionNew { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actionedit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ActionEdit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "actiondelete", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ActionDelete { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertsubject", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AlertSubject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertbody", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string AlertBody { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

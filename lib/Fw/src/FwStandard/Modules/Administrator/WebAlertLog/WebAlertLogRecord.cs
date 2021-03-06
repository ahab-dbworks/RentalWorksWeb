using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace FwStandard.Modules.Administrator.WebAlertLog
{
    [FwSqlTable("webalertlog")]
    public class WebAlertLogRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webalertlogid", modeltype: FwDataTypes.Integer, identity: true, sqltype: "int", isPrimaryKey: true)]
        public int? WebAlertLogId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "createdatetime", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public DateTime? CreateDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertsubject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string AlertSubject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertbody", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string AlertBody { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertfrom", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string AlertFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertto", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string AlertTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errormessage", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ErrorMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

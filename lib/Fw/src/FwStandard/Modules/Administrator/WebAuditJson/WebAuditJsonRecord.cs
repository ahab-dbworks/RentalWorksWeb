using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace FwStandard.Modules.Administrator.WebAuditJson
{
    [FwSqlTable("webauditjson")]
    public class WebAuditJsonRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webauditid", modeltype: FwDataTypes.Integer, sqltype: "int", identity: true, isPrimaryKey: true)]
        public int? WebAuditId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid3", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

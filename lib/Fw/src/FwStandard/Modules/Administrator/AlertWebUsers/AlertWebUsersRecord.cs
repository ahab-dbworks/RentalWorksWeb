using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace FwStandard.Modules.Administrator.AlertWebUsers
{
    [FwSqlTable("webalertwebusers")]
    public class AlertWebUsersRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webalertwebusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string AlertWebUserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

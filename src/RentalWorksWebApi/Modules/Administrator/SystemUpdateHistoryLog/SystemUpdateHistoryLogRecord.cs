using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.SystemUpdateHistoryLog
{
    [FwSqlTable("systemupdatehistorylog")]
    public class SystemUpdateHistoryLogRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistorylogid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? SystemUpdateHistoryLogId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistoryid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? SystemUpdateHistoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "msg", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Messsage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

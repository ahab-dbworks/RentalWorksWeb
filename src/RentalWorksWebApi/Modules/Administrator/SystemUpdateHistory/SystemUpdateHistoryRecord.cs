using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Administrator.SystemUpdateHistory
{
    [FwSqlTable("systemupdatehistory")]
    public class SystemUpdateHistoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistoryid", modeltype: FwDataTypes.Integer, identity: true, sqltype: "int", isPrimaryKey: true)]
        public int? SystemUpdateHistoryId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "updatedatetime", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public DateTime UpdateDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromversion", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string FromVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toversion", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ToVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errormsg", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8000)]
        public string ErrorMessage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.UserSettings
{
    [FwSqlTable("webusers")]
    public class UserSettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
     }
}
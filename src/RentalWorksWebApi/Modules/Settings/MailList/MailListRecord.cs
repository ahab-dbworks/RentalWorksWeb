using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Data.Settings
{
    [FwSqlTable("maillist")]
    public class MailListRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "listid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string MailListId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "list", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string MailList { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

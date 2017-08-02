using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace RentalWorksWebApi.Data.Settings
{
    [FwSqlTable("maillist")]
    public class MailListRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "listid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string MailListId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "list", dataType: FwDataTypes.Text, length: 20)]
        public string MailList { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.ContactTitle
{
    [FwSqlTable("contacttitle")]
    public class ContactTitleRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ContactTitleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text, maxlength: 50, required: true)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "titletype", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string TitleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accountspayableflg", modeltype: FwDataTypes.Boolean)]
        public bool? AccountsPayable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accountsreceivableflg", modeltype: FwDataTypes.Boolean)]
        public bool? AccountsReceivable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

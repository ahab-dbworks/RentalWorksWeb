using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.ContactTitle
{
    [FwSqlTable("contacttitle")]
    public class ContactTitleRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "contacttitleid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string ContactTitleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "contacttitle", dataType: FwDataTypes.Text, length: 50)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "titletype", dataType: FwDataTypes.Text, length: 20)]
        public string TitleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "accountspayableflg", dataType: FwDataTypes.Boolean)]
        public bool AccountsPayable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "accountsreceivableflg", dataType: FwDataTypes.Boolean)]
        public bool AccountsReceivable { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

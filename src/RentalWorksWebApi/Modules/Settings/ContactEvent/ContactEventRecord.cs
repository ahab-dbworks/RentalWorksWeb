using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.ContactEvent
{
    [FwSqlTable("contactevent")]
    public class ContactEventRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "contacteventid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string ContactEventId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "contactevent", dataType: FwDataTypes.Text, length: 15)]
        public string ContactEvent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "color", dataType: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "textcolor", dataType: FwDataTypes.Boolean)]
        public bool WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "recurring", dataType: FwDataTypes.Boolean)]
        public bool Recurring { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

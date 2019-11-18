using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.ContactEvent
{
    [FwSqlTable("contactevent")]
    public class ContactEventRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contacteventid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ContactEventId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contactevent", modeltype: FwDataTypes.Text, maxlength: 15, required: true)]
        public string ContactEvent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Boolean)]
        public bool? WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "recurring", modeltype: FwDataTypes.Boolean)]
        public bool? Recurring { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

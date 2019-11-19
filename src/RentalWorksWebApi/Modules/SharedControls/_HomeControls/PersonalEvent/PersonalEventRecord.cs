using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.PersonalEvent
{
    [FwSqlTable("personalevent")]
    public class PersonalEventRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personaleventid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PersonalEventId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacteventid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ContactEventId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eventdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string EventDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

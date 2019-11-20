using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.EventSettings.EventCategory
{
    [FwSqlTable("eventcategory")]
    public class EventCategoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategoryid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string EventCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategory", modeltype: FwDataTypes.Text, maxlength: 50, required: true)]
        public string EventCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategorycode", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string EventCategoryCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

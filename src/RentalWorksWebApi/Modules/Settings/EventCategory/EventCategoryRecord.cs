using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.EventCategory
{
    [FwSqlTable("eventcategory")]
    public class EventCategoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategoryid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string EventCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategory", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string EventCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "eventcategorycode", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string EventCategoryCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

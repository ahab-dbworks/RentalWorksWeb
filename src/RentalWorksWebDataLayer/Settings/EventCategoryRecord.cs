using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("eventcategory")]
    public class EventCategoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "eventcategoryid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string EventCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "eventcategory", dataType: FwDataTypes.Text, length: 50)]
        public string EventCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "eventcategorycode", dataType: FwDataTypes.Text, length: 10)]
        public string EventCategoryCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

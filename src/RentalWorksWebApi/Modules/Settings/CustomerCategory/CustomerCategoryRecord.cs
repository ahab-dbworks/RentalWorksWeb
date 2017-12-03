using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.CustomerCategory
{
    [FwSqlTable("custcat")]
    public class CustomerCategoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcatid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CustomerCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custcatdesc", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string CustomerCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }

    }
}

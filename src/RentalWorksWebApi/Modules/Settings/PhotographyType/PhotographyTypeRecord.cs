using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.PhotographyType
{
    [FwSqlTable("photographytype")]
    public class PhotographyTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "photographytypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string PhotographyTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "photographytype", dataType: FwDataTypes.Text, length: 50)]
        public string PhotographyType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorRating
{
    [FwSqlTable("generatorratingview")]
    public class GeneratorRatingLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicleratingid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GeneratorRatingId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclerating", modeltype: FwDataTypes.Text)]
        public string GeneratorRating { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

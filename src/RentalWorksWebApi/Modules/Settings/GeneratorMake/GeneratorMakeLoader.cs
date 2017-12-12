using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.GeneratorMake
{
    [FwSqlTable("generatormakeview")]
    public class GeneratorMakeLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemakeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GeneratorMakeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemake", modeltype: FwDataTypes.Text)]
        public string GeneratorMake { get; set; }
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

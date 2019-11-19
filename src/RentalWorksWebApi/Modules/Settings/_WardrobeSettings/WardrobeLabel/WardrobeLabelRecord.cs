using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeLabel
{
    [FwSqlTable("label")]
    public class WardrobeLabelRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labelid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WardrobeLabelId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string WardrobeLabel { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

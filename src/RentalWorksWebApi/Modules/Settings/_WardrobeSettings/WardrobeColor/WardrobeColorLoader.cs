using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeColor
{
    [FwSqlTable("wardrobecolorview")]
    public class WardrobeColorLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "colorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WardrobeColorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Text)]
        public string WardrobeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "colortype", modeltype: FwDataTypes.Text)]
        public string ColorType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

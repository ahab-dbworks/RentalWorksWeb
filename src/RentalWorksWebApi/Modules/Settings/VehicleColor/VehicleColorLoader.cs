using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VehicleColor
{
    [FwSqlTable("vehiclecolorview")]
    public class VehicleColorLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "colorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VehicleColorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Text)]
        public string VehicleColor { get; set; }
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

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VehicleSettings.LicenseClass
{
    [FwSqlTable("licclass")]
    public class LicenseClassRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclassid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string LicenseClassId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclass", modeltype: FwDataTypes.Text, maxlength: 3, required: true)]
        public string LicenseClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text, maxlength: 25, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "regulated", modeltype: FwDataTypes.Boolean)]
        public bool? Regulated { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

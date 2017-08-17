using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.LicenseClass
{
    [FwSqlTable("licclass")]
    public class LicenseClassRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclassid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string LicenseClassId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclass", modeltype: FwDataTypes.Text, maxlength: 3)]
        public string LicenseClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text, maxlength: 25)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "regulated", modeltype: FwDataTypes.Boolean)]
        public bool Regulated { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

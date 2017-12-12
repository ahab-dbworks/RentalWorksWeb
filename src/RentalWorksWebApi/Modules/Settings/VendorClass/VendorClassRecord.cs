using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VendorClass
{
    [FwSqlTable("vendorclass")]
    public class VendorClassRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorclassid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string VendorClassId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorclass", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string VendorClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

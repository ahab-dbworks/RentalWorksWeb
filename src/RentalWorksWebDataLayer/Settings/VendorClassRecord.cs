using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("vendorclass")]
    public class VendorClassRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "vendorclassid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string VendorClassId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "vendorclass", dataType: FwDataTypes.Text, length: 20)]
        public string VendorClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

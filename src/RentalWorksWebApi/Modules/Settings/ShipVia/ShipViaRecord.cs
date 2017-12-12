using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace WebApi.Data.Settings
{
    [FwSqlTable("shipvia")]
    public class ShipViaRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ShipViaId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text, maxlength: 25, required: true)]
        public string ShipVia { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("warehouse")]
    public class WarehouseRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "warehouseid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "warehouse", dataType: FwDataTypes.Text, length: 20)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "whcode", dataType: FwDataTypes.Text, length: 8)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
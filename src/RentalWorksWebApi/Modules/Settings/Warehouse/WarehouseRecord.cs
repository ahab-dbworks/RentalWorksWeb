using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Warehouse
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
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
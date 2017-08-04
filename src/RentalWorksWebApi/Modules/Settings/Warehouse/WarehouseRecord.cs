﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Warehouse
{
    [FwSqlTable("warehouse")]
    public class WarehouseRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
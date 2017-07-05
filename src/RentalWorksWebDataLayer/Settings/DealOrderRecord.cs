﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("dealorder", hasInsert: true, hasUpdate: true, hasDelete: true)]
    public class DealOrderRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderno", dataType: FwDataTypes.Text, length: 16)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdesc", dataType: FwDataTypes.Text, length: 50)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderdate", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealid", dataType: FwDataTypes.Text, length: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

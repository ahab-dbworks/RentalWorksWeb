﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("paytype")]
    public class PaymentTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "paytypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string PaymentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "paytype", dataType: FwDataTypes.Text, length: 20)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "short", dataType: FwDataTypes.Text, length: 11)]
        public string ShortName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "pmttype", dataType: FwDataTypes.Text, length: 20)]
        public string PaymentTypeType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "accttran", dataType: FwDataTypes.Boolean)]
        public string AccountingTransaction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

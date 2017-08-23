using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebApi.Data.Settings
{
    [FwSqlTable("paytype")]
    public class PaymentTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PaymentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "short", modeltype: FwDataTypes.Text, maxlength: 11, required: true)]
        public string ShortName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmttype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string PaymentTypeType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accttran", modeltype: FwDataTypes.Boolean)]
        public bool AccountingTransaction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

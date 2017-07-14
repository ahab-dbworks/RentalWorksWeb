using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("creditstatus")]
    public class CreditStatusRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatusid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CreditStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatus", dataType: FwDataTypes.Text, length: 20)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "orderallow", dataType: FwDataTypes.Boolean)]
        public string CreateContractAllowed { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

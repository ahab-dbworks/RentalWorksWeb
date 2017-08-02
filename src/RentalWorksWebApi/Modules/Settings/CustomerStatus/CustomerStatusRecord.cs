using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.CustomerStatus
{
    [FwSqlTable("custstatus")]
    public class CustomerStatusRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custstatusid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custstatus", dataType: FwDataTypes.Text, length: 12)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "statustype", dataType: FwDataTypes.Text, length: 1)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatusid", dataType: FwDataTypes.Text, length: 8)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

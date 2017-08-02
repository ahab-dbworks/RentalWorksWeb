using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealStatus
{
    [FwSqlTable("dealstatus")]
    public class DealStatusRecord: RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealstatusid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string DealStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealstatus", dataType: FwDataTypes.Text, length: 12)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "statustype", dataType: FwDataTypes.Text, length: 1)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatusid", dataType: FwDataTypes.Text, length: 8)]
        public string CreditStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
    }
}

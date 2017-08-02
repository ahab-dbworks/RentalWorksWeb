using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.CustomerStatus
{
    [FwSqlTable("customerstatusview", hasInsert: false, hasUpdate: false, hasDelete: false)]
    public class CustomerStatusLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "customerstatusid", dataType: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "customerstatus", dataType: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "statustype", dataType: FwDataTypes.Text)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatusid", dataType: FwDataTypes.Text)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatus", dataType: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerStatus
{
    [FwSqlTable("customerstatusview")]
    public class CustomerStatusLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerstatusid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerstatus", modeltype: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

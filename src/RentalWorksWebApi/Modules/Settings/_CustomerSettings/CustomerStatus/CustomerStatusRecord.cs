using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerStatus
{
    [FwSqlTable("custstatus")]
    public class CustomerStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text, maxlength: 12, required: true)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text, maxlength: 1, required: true)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

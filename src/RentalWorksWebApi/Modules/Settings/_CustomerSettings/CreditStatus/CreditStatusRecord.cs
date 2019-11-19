using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.CustomerSettings.CreditStatus
{
    [FwSqlTable("creditstatus")]
    public class CreditStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CreditStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderallow", modeltype: FwDataTypes.Boolean)]
        public bool? CreateContractAllowed { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

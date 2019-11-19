using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.OrderSettings.DiscountReason
{
    [FwSqlTable("invoicediscountreason")]
    public class DiscountReasonRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicediscountreasonid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DiscountReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoicediscountreason", modeltype: FwDataTypes.Text, maxlength: 255, required: true)]
        public string DiscountReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

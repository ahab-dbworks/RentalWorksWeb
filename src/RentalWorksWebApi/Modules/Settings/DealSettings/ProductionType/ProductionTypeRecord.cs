using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.DealSettings.ProductionType
{
    [FwSqlTable("prodtype")]
    public class ProductionTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prodtypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ProductionTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prodtype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string ProductionType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prodcode", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string ProductionTypeCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

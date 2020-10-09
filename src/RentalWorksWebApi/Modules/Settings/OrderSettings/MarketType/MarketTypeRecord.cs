using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderSettings.MarketType
{
    [FwSqlTable("markettype")]
    public class MarketTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markettypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string MarketTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markettype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string MarketType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.MarketSegment
{
    [FwSqlTable("marketsegment")]
    public class MarketSegmentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string MarketSegmentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegment", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string MarketSegment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markettypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string MarketTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

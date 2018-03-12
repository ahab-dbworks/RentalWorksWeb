using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.MarketSegmentJob
{
    [FwSqlTable("marketsegmentjob")]
    public class MarketSegmentJobRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegmentjobid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string MarketSegmentJobId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegmentjob", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string MarketSegmentJob { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marketsegmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string MarketSegmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

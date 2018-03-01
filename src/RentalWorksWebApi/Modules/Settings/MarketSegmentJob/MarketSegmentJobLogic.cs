using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.MarketSegmentJob
{
    public class MarketSegmentJobLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MarketSegmentJobRecord marketSegmentJob = new MarketSegmentJobRecord();
        MarketSegmentJobLoader marketSegmentJobLoader = new MarketSegmentJobLoader();
        public MarketSegmentJobLogic()
        {
            dataRecords.Add(marketSegmentJob);
            dataLoader = marketSegmentJobLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MarketSegmentJobId { get { return marketSegmentJob.MarketSegmentJobId; } set { marketSegmentJob.MarketSegmentJobId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string MarketSegmentJob { get { return marketSegmentJob.MarketSegmentJob; } set { marketSegmentJob.MarketSegmentJob = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketType { get; set; }
        public string MarketSegmentId { get { return marketSegmentJob.MarketSegmentId; } set { marketSegmentJob.MarketSegmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketSegment { get; set; }
        public string DateStamp { get { return marketSegmentJob.DateStamp; } set { marketSegmentJob.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

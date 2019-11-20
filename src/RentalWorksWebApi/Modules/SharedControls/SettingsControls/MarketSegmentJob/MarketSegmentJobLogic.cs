using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.MarketSegmentJob
{
    [FwLogic(Id:"yKORQFX7fRRy")]
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
        [FwLogicProperty(Id:"S2SK1AQNa1YM", IsPrimaryKey:true)]
        public string MarketSegmentJobId { get { return marketSegmentJob.MarketSegmentJobId; } set { marketSegmentJob.MarketSegmentJobId = value; } }

        [FwLogicProperty(Id:"S2SK1AQNa1YM", IsRecordTitle:true)]
        public string MarketSegmentJob { get { return marketSegmentJob.MarketSegmentJob; } set { marketSegmentJob.MarketSegmentJob = value; } }

        [FwLogicProperty(Id:"48K1B3mFKDUh", IsReadOnly:true)]
        public string MarketTypeId { get; set; }

        [FwLogicProperty(Id:"48K1B3mFKDUh", IsReadOnly:true)]
        public string MarketType { get; set; }

        [FwLogicProperty(Id:"kq92JfVctCT")]
        public string MarketSegmentId { get { return marketSegmentJob.MarketSegmentId; } set { marketSegmentJob.MarketSegmentId = value; } }

        [FwLogicProperty(Id:"S2SK1AQNa1YM", IsReadOnly:true)]
        public string MarketSegment { get; set; }

        [FwLogicProperty(Id:"KaNRCBX5vGL")]
        public string DateStamp { get { return marketSegmentJob.DateStamp; } set { marketSegmentJob.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

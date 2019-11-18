using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.MarketSegment
{
    [FwLogic(Id:"BQnbLso07yfx")]
    public class MarketSegmentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MarketSegmentRecord marketSegment = new MarketSegmentRecord();
        MarketSegmentLoader marketSegmentLoader = new MarketSegmentLoader();
        public MarketSegmentLogic()
        {
            dataRecords.Add(marketSegment);
            dataLoader = marketSegmentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qUPqwS7Cokmj", IsPrimaryKey:true)]
        public string MarketSegmentId { get { return marketSegment.MarketSegmentId; } set { marketSegment.MarketSegmentId = value; } }

        [FwLogicProperty(Id:"qUPqwS7Cokmj", IsRecordTitle:true)]
        public string MarketSegment { get { return marketSegment.MarketSegment; } set { marketSegment.MarketSegment = value; } }

        [FwLogicProperty(Id:"HTLBfcbaizW")]
        public string MarketTypeId { get { return marketSegment.MarketTypeId; } set { marketSegment.MarketTypeId = value; } }

        [FwLogicProperty(Id:"KJeLJ8E1QYMj", IsReadOnly:true)]
        public string MarketType { get; set; }

        [FwLogicProperty(Id:"Jjbt5dPG3H4")]
        public bool? Inactive { get { return marketSegment.Inactive; } set { marketSegment.Inactive = value; } }

        [FwLogicProperty(Id:"SjrVrJdnA2s")]
        public string DateStamp { get { return marketSegment.DateStamp; } set { marketSegment.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

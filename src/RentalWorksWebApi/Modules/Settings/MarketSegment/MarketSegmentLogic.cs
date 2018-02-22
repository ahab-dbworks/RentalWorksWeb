using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Settings.MarketSegment
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MarketSegmentId { get { return marketSegment.MarketSegmentId; } set { marketSegment.MarketSegmentId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string MarketSegment { get { return marketSegment.MarketSegment; } set { marketSegment.MarketSegment = value; } }
        public string MarketTypeId { get { return marketSegment.MarketTypeId; } set { marketSegment.MarketTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MarketType { get; set; }
        public bool? Inactive { get { return marketSegment.Inactive; } set { marketSegment.Inactive = value; } }
        public string DateStamp { get { return marketSegment.DateStamp; } set { marketSegment.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

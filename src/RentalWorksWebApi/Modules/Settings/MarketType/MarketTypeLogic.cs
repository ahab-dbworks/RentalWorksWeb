using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.MarketType
{
    public class MarketTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MarketTypeRecord marketType = new MarketTypeRecord();
        MarketTypeLoader marketTypeLoader = new MarketTypeLoader();
        public MarketTypeLogic()
        {
            dataRecords.Add(marketType);
            dataLoader = marketTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MarketTypeId { get { return marketType.MarketTypeId; } set { marketType.MarketTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string MarketType { get { return marketType.MarketType; } set { marketType.MarketType = value; } }
        public bool? Inactive { get { return marketType.Inactive; } set { marketType.Inactive = value; } }
        public string DateStamp { get { return marketType.DateStamp; } set { marketType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
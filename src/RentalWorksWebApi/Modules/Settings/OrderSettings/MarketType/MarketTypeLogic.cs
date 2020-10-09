using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderSettings.MarketType
{
    [FwLogic(Id:"ntJtKUqrb9fy")]
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
        [FwLogicProperty(Id:"nifjEyvBYGbP", IsPrimaryKey:true)]
        public string MarketTypeId { get { return marketType.MarketTypeId; } set { marketType.MarketTypeId = value; } }

        [FwLogicProperty(Id:"nifjEyvBYGbP", IsRecordTitle:true)]
        public string MarketType { get { return marketType.MarketType; } set { marketType.MarketType = value; } }

        [FwLogicProperty(Id: "xxxxxxxxxx")]
        public string ExportCode { get { return marketType.ExportCode; } set { marketType.ExportCode = value; } }

        [FwLogicProperty(Id:"e5euUtECYjY")]
        public bool? Inactive { get { return marketType.Inactive; } set { marketType.Inactive = value; } }

        [FwLogicProperty(Id:"Xut2hGyMK0A")]
        public string DateStamp { get { return marketType.DateStamp; } set { marketType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.CurrencyMissing
{
    [FwLogic(Id: "x01vsNGOE9oar")]
    public class CurrencyMissingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CurrencyMissingLoader currencyMissingLoader = new CurrencyMissingLoader();
        public CurrencyMissingLogic()
        {
            dataLoader = currencyMissingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "X0DFJXy3xaNtR", IsPrimaryKey: true, IsReadOnly: true)]
        public string Id { get; set; }
        [FwLogicProperty(Id: "x1qmS3rgp7LD5", IsReadOnly: true)]
        public string Code { get; set; }
        [FwLogicProperty(Id: "x2HnPi36j2dtw", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "x36GPcGzoCVvy", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "X3MuTH72nKtb8", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "X3weurhBw5e0A", IsReadOnly: true)]
        public string ProposedCurrencyId { get; set; }
        [FwLogicProperty(Id: "X4ukeZZmxJHTN", IsReadOnly: true)]
        public string ProposedCurrencyCode { get; set; }
        [FwLogicProperty(Id: "x5KHTL3RuPcKt", IsReadOnly: true)]
        public string ModuleName { get; set; }
        [FwLogicProperty(Id: "x5UgWIEf7rb0r", IsReadOnly: true)]
        public string DatabaseTableName { get; set; }
        [FwLogicProperty(Id: "x6EsiW87G5MnU", IsReadOnly: true)]
        public string IdFieldName { get; set; }
        [FwLogicProperty(Id: "X6s8IGeZGht1D", IsReadOnly: true)]
        public string CurrencyFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

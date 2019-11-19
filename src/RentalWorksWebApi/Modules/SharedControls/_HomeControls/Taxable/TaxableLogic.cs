using FwStandard.AppManager;
using System.Reflection;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.Taxable
{
    [FwLogic(Id:"a3ImrbPSMopw8")]
    public class TaxableLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        TaxableLoader taxableLoader = new TaxableLoader();
        public TaxableLogic()
        {
            dataLoader = taxableLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"r3ZLqxNZ5wyoh", IsPrimaryKey:true)]
        public string MasterId { get; set; }

        [FwLogicProperty(Id:"vLaIfjaXxob0")]
        public string LocationId { get; set; }

        [FwLogicProperty(Id:"BjcSxWULIWu4")]
        public string Location { get; set; }

        [FwLogicProperty(Id:"AVUz0DrFpzUZ")]
        public bool? Taxable { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

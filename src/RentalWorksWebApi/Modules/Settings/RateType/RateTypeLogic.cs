using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.RateType
{
    [FwLogic(Id:"6m3xsc6qjp7jQ")]
    public class RateTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateTypeLoader rateTypeLoader = new RateTypeLoader();
        public RateTypeLogic()
        {
            dataLoader = rateTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"RVBqiIUewSkND", IsPrimaryKey:true, IsReadOnly:true)]
        public string RateType { get; set; }

        [FwLogicProperty(Id:"RVBqiIUewSkND", IsReadOnly:true)]
        public string RateTypeDisplay { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

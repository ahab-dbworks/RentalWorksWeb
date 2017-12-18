using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.RateType
{
    public class RateTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateTypeLoader rateTypeLoader = new RateTypeLoader();
        public RateTypeLogic()
        {
            dataLoader = rateTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true, isPrimaryKey: true)]
        public string RateType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RateTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
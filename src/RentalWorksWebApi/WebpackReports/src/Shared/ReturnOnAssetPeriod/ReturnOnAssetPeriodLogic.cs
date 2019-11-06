using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.ReturnOnAssetPeriod
{
    [FwLogic(Id: "CvPLX7T86t1v")]
    public class ReturnOnAssetPeriodLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReturnOnAssetPeriodLoader returnOnAssetPeriodLoader = new ReturnOnAssetPeriodLoader();
        public ReturnOnAssetPeriodLogic()
        {
            dataLoader = returnOnAssetPeriodLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "zwPy99V5C1V4", IsReadOnly: true)]
        public string Period { get; set; }
        [FwLogicProperty(Id: "rWckDsxP4wgLQ", IsReadOnly: true)]
        public string Label { get; set; }
        [FwLogicProperty(Id: "8CYsdjusbfBO", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.ReturnOnAssetYear
{
    [FwLogic(Id: "e3n9kRxbz47S")]
    public class ReturnOnAssetYearLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReturnOnAssetYearLoader returnOnAssetYearLoader = new ReturnOnAssetYearLoader();
        public ReturnOnAssetYearLogic()
        {
            dataLoader = returnOnAssetYearLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "PYB620EODRA1", IsReadOnly: true)]
        public int? Year { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

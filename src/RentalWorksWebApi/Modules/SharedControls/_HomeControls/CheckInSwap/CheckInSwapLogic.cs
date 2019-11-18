using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInSwap
{
    [FwLogic(Id:"2p0cIg8YiIgx")]
    public class CheckInSwapLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInSwapLoader checkInSwapLoader = new CheckInSwapLoader();
        public CheckInSwapLogic()
        {
            dataLoader = checkInSwapLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"D7qlxMqTDYOp", IsReadOnly:true)]
        public int? OrderTranId { get; set; }

        [FwLogicProperty(Id:"UuNHfjp41ynp", IsReadOnly:true)]
        public string InternalChar { get; set; }

        [FwLogicProperty(Id:"m9QcmYBFcI1m", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"zA1dde4V3qmw", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"vzNb9POnVpVU", IsReadOnly:true)]
        public string OutOrderNumber { get; set; }

        [FwLogicProperty(Id:"fd7ndetxNcyE", IsReadOnly:true)]
        public string OutBarCode { get; set; }

        [FwLogicProperty(Id:"wRopWrAdDqv4", IsReadOnly:true)]
        public string InOrderNumber { get; set; }

        [FwLogicProperty(Id:"HftZQIaXAbvs", IsReadOnly:true)]
        public string InBarCode { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

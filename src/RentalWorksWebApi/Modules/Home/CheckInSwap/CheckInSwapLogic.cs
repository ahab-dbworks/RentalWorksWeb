using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInSwap
{
    public class CheckInSwapLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInSwapLoader checkInSwapLoader = new CheckInSwapLoader();
        public CheckInSwapLogic()
        {
            dataLoader = checkInSwapLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public int? OrderTranId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InternalChar { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutBarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

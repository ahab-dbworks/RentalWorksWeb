using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.QuikActivity
{
    [FwLogic(Id: "u8LEDq5KKwNY0")]
    public class QuikActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        QuikActivityLoader quikActivityLoader = new QuikActivityLoader();
        public QuikActivityLogic()
        {
            dataLoader = quikActivityLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "SGrnWiB0jbk")]
        public string ActivityDate { get; set; }

        [FwLogicProperty(Id: "32SGrnWiB0jbk")]
        public string ActivityDescription { get; set; }

        [FwLogicProperty(Id: "ppcWLsnx6rXtG")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "WLkc5TblFXg8U")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "DKYiaPmxnoahF")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "yeBNfnVdmT6IS")]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "UlZIuzD8wTj30")]
        public string Description { get; set; }

        [FwLogicProperty(Id: "GUScPBh7s8htg")]
        public decimal? Quantity { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

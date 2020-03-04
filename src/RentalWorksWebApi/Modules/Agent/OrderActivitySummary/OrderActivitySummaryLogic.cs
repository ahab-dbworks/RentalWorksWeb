using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.OrderActivitySummary
{
    [FwLogic(Id: "aNR1hwNW4eD66")]
    public class OrderActivitySummaryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderActivitySummaryLoader orderActivitySummaryLoader = new OrderActivitySummaryLoader();
        public OrderActivitySummaryLogic()
        {
            dataLoader = orderActivitySummaryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "coRwOTtNUb0lh", IsReadOnly: true)]
        public string RowType { get; set; }
        [FwLogicProperty(Id: "AnyOKhgZ8M5cS", IsReadOnly: true)]
        public string Activity { get; set; }
        [FwLogicProperty(Id: "aPbWKvG5KxP4s", IsReadOnly: true)]
        public string GrossTotal { get; set; }
        [FwLogicProperty(Id: "aPgLhfQvNseWA", IsReadOnly: true)]
        public string Discount { get; set; }
        [FwLogicProperty(Id: "APPfDHMCwsALK", IsReadOnly: true)]
        public string SubTotal { get; set; }
        [FwLogicProperty(Id: "aPQe7wM3ZrvVB", IsReadOnly: true)]
        public string SubTotalTaxable { get; set; }
        [FwLogicProperty(Id: "aPtHiAV3geUej", IsReadOnly: true)]
        public string Tax { get; set; }
        [FwLogicProperty(Id: "aQ08FgFmfdr0C", IsReadOnly: true)]
        public string Total { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

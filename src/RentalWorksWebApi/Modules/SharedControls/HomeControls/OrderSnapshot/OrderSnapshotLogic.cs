using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.OrderSnapshot
{
    [FwLogic(Id:"9wiaAZUoRsKs")]
    public class OrderSnapshotLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderSnapshotLoader orderSnapshotLoader = new OrderSnapshotLoader();
        public OrderSnapshotLogic()
        {
            dataLoader = orderSnapshotLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"kyjl0bAYf1d5", IsReadOnly:true)]
        public string SnapshotId { get; set; }

        [FwLogicProperty(Id:"YKo8d3FnjSXv", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"6GLGlOLFkTwz", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"VwvIJpEDhzXF", IsReadOnly:true)]
        public string OrderDate { get; set; }

        [FwLogicProperty(Id:"eai7F8t49MSE", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"Y7VtHW19ji1v", IsReadOnly:true)]
        public string Status { get; set; }

        [FwLogicProperty(Id:"YzrDmM68CH4k", IsReadOnly:true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"YzrDmM68CH4k", IsReadOnly:true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id:"loZsUYuUnWXm", IsReadOnly:true)]
        public string AgentId { get; set; }

        [FwLogicProperty(Id:"loZsUYuUnWXm", IsReadOnly:true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id:"DfCu10i8vgPu", IsReadOnly:true)]
        public string InputById { get; set; }

        [FwLogicProperty(Id:"wynsysk4DeUt", IsReadOnly:true)]
        public decimal? OrderTotal { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

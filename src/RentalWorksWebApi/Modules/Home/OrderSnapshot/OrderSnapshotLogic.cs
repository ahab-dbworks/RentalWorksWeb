using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderSnapshot
{
    public class OrderSnapshotLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderSnapshotLoader orderSnapshotLoader = new OrderSnapshotLoader();
        public OrderSnapshotLogic()
        {
            dataLoader = orderSnapshotLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string SnapshotId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Status { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InputById { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? OrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInOrder
{
    public class CheckInOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInOrderLoader checkInOrderLoader = new CheckInOrderLoader();
        public CheckInOrderLogic()
        {
            dataLoader = checkInOrderLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true, isReadOnly: true)]
        public string ContractId { get; set; }
        [FwBusinessLogicField(isPrimaryKey: true, isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Priority { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IncludeInCheckInSession { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EstimatedStartDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EstimatedStopDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Status { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StatusDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

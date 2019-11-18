using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInOrder
{
    [FwLogic(Id:"kggWpyZEEdbV")]
    public class CheckInOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInOrderLoader checkInOrderLoader = new CheckInOrderLoader();
        public CheckInOrderLogic()
        {
            dataLoader = checkInOrderLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"8qAD2usxRhoo", IsPrimaryKey:true, IsReadOnly:true)]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"7jZyMNnEJCFw", IsPrimaryKey:true, IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"rt3qyI0oSkIA", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"AKj7BlC2f0M3", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"GAkKZiJI1NHf", IsReadOnly:true)]
        public int? Priority { get; set; }

        [FwLogicProperty(Id:"q9sF0ePFI7rD", IsReadOnly:true)]
        public bool? IncludeInCheckInSession { get; set; }

        [FwLogicProperty(Id:"QlVGEFN28diu", IsReadOnly:true)]
        public string OrderDate { get; set; }

        [FwLogicProperty(Id:"mTK9JUI1K9lX", IsReadOnly:true)]
        public string EstimatedStartDate { get; set; }

        [FwLogicProperty(Id:"oIDyurOWIjpX", IsReadOnly:true)]
        public string EstimatedStopDate { get; set; }

        [FwLogicProperty(Id:"5qoEsjH9PBx5", IsReadOnly:true)]
        public string Status { get; set; }

        [FwLogicProperty(Id:"5qoEsjH9PBx5", IsReadOnly:true)]
        public string StatusDate { get; set; }

        [FwLogicProperty(Id:"2RfJjKAWmRFD", IsReadOnly:true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"Rvqswudm97Oe", IsReadOnly:true)]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id:"ub1j73UPYPuX", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.SuspendedSession
{
    [FwLogic(Id: "Z0Z5ZHwa7uSc")]
    public class SuspendedSessionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SuspendedSessionLoader suspendedSessionLoader = new SuspendedSessionLoader();
        public SuspendedSessionLogic()
        {
            dataLoader = suspendedSessionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "AJvFn9YwXWCd", IsPrimaryKey: true, IsReadOnly: true)]
        public string ContractId { get; set; }
        [FwLogicProperty(Id: "LnTmTBC2v2II", IsReadOnly: true)]
        public int? SessionNumber { get; set; }
        [FwLogicProperty(Id: "iIm8Fr8Dynen", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "vxYtq9IH1OVZ", IsReadOnly: true)]
        public string DealNumber { get; set; }
        [FwLogicProperty(Id: "o9HNQ5z1J8kI", IsReadOnly: true)]
        public string DealOrVendor { get; set; }
        [FwLogicProperty(Id: "bEmCu3ZyyXmT", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "7yB9JZE1gOI5", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "TtT5ukxsLFSv8", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "N3T1d1t6FkX1", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "EpgmdQvzyni5", IsReadOnly: true)]
        public string UserNameFirstMiddleLast { get; set; }
        [FwLogicProperty(Id: "PJd2isoymVn1", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "8YJxmrwSQ50P", IsReadOnly: true)]
        public string StatusDate { get; set; }
        [FwLogicProperty(Id: "sQQlHhICiLUL", IsReadOnly: true)]
        public string UsersId { get; set; }
        [FwLogicProperty(Id: "L11xsLHYOgaA", IsReadOnly: true)]
        public string ContractDate { get; set; }
        [FwLogicProperty(Id: "v0neuNWNVyXn", IsReadOnly: true)]
        public string ContractTime { get; set; }
        [FwLogicProperty(Id: "qDK9yEyFGs95", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "RbkXKbQV8PYf", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "DHollCCLHLdfm", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "WVHtMR6eFlGk", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "DEvthzhUsum2", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "4gqoyHjbYNfO", IsReadOnly: true)]
        public string ExchangeContractId { get; set; }
        [FwLogicProperty(Id: "NncDnrO5ou1H", IsReadOnly: true)]
        public string ContractType { get; set; }
        [FwLogicProperty(Id: "isF3UikJV1AV", IsReadOnly: true)]
        public bool? IsForcedSuspend { get; set; }
        [FwLogicProperty(Id: "GfdsT0Y5OyCJe", IsReadOnly: true)]
        public string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

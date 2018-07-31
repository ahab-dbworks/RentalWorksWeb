using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.Contract
{
    public class ContractLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractRecord contract = new ContractRecord();
        ContractLoader contractLoader = new ContractLoader();
        ContractBrowseLoader contractBrowseLoader = new ContractBrowseLoader();
        public ContractLogic()
        {
            dataRecords.Add(contract);
            dataLoader = contractLoader;
            browseLoader = contractBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ContractId { get { return contract.ContractId; } set { contract.ContractId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ContractNumber { get { return contract.ContractNumber; } set { contract.ContractNumber = value; } }
        public string ContractType { get { return contract.ContractType; } set { contract.ContractType = value; } }
        public string ContractDate { get { return contract.ContractDate; } set { contract.ContractDate = value; } }
        public string ContractTime { get { return contract.ContractTime; } set { contract.ContractTime = value; } }
        public string LocationId { get { return contract.LocationId; } set { contract.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string WarehouseId { get { return contract.WarehouseId; } set { contract.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        public string DealId { get { return contract.DealId; } set { contract.DealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        public string DepartmentId { get { return contract.DepartmentId; } set { contract.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RequisitionNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        public bool? Migrated { get { return contract.Migrated; } set { contract.Migrated = value; } }
        public bool? NeedReconcile { get { return contract.NeedReconcile; } set { contract.NeedReconcile = value; } }
        public bool? PendingExchange { get { return contract.PendingExchange; } set { contract.PendingExchange = value; } }
        public string ExchangeContractId { get { return contract.ExchangeContractId; } set { contract.ExchangeContractId = value; } }
        public bool? Rental { get { return contract.Rental; } set { contract.Rental = value; } }
        public bool? Sales { get { return contract.Sales; } set { contract.Sales = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Exchange { get; set; }
        public string InputByUserId { get { return contract.InputByUserId; } set { contract.InputByUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InputByUser { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DealInactive { get; set; }
        public bool? Truck { get { return contract.Truck; } set { contract.Truck = value; } }
        public string BillingDate { get { return contract.BillingDate; } set { contract.BillingDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasAdjustedBillingDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasVoId { get; set; }
        public string SessionId { get { return contract.SessionId; } set { contract.SessionId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        public string DateStamp { get { return contract.DateStamp; } set { contract.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

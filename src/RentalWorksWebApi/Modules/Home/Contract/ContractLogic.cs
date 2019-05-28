using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Home.Delivery;

namespace WebApi.Modules.Home.Contract
{
    [FwLogic(Id:"EHxKFjXfUroP")]
    public class ContractLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractRecord contract = new ContractRecord();
        DeliveryRecord delivery = new DeliveryRecord();


        ContractLoader contractLoader = new ContractLoader();
        ContractBrowseLoader contractBrowseLoader = new ContractBrowseLoader();
        public ContractLogic()
        {
            dataRecords.Add(contract);
            dataRecords.Add(delivery);
            dataLoader = contractLoader;
            browseLoader = contractBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"WON1VkgytZI8", IsPrimaryKey:true)]
        public string ContractId { get { return contract.ContractId; } set { contract.ContractId = value; } }

        [FwLogicProperty(Id:"sPqsdANcVxby", IsRecordTitle:true)]
        public string ContractNumber { get { return contract.ContractNumber; } set { contract.ContractNumber = value; } }

        [FwLogicProperty(Id:"8CCs4HaqLkdv")]
        public string ContractType { get { return contract.ContractType; } set { contract.ContractType = value; } }

        [FwLogicProperty(Id:"HD0vDJtf1yb0")]
        public string ContractDate { get { return contract.ContractDate; } set { contract.ContractDate = value; } }

        [FwLogicProperty(Id:"zfj1WvUia9Sz")]
        public string ContractTime { get { return contract.ContractTime; } set { contract.ContractTime = value; } }

        [FwLogicProperty(Id:"Woa2oSxOr91V")]
        public string LocationId { get { return contract.LocationId; } set { contract.LocationId = value; } }

        [FwLogicProperty(Id:"4HAv2CyRKKRy", IsReadOnly:true)]
        public string LocationCode { get; set; }

        [FwLogicProperty(Id:"4HAv2CyRKKRy", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"j9Sh3vz9n07M")]
        public string WarehouseId { get { return contract.WarehouseId; } set { contract.WarehouseId = value; } }

        [FwLogicProperty(Id:"bne2ZRjKFwhT", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"bne2ZRjKFwhT", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"0qFLtPV8K3nK", IsReadOnly:true)]
        public string CustomerId { get; set; }

        [FwLogicProperty(Id:"yEiu8kbykGAl")]
        public string DealId { get { return contract.DealId; } set { contract.DealId = value; } }

        [FwLogicProperty(Id:"sWr8J7HvRgRZ", IsReadOnly:true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id:"icwdkXZuYp9v")]
        public string DepartmentId { get { return contract.DepartmentId; } set { contract.DepartmentId = value; } }

        [FwLogicProperty(Id:"B8wBpIJcoXQY", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"CIhOMykCQJ0u", IsReadOnly:true)]
        public string PurchaseOrderId { get; set; }

        [FwLogicProperty(Id:"rClktjVOH3ly", IsReadOnly:true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id:"iB3dKP1QeaMT", IsReadOnly:true)]
        public string RequisitionNumber { get; set; }

        [FwLogicProperty(Id:"bKlSBC97Q4qY", IsReadOnly:true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"bKlSBC97Q4qY", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"FCu67vj1XIsA")]
        public bool? Migrated { get { return contract.Migrated; } set { contract.Migrated = value; } }

        [FwLogicProperty(Id:"4nBZnSSPPAPB")]
        public bool? NeedReconcile { get { return contract.NeedReconcile; } set { contract.NeedReconcile = value; } }

        [FwLogicProperty(Id:"ImFcBBWRLy2o")]
        public bool? PendingExchange { get { return contract.PendingExchange; } set { contract.PendingExchange = value; } }

        [FwLogicProperty(Id:"aZdsfF1nTHAy")]
        public string ExchangeContractId { get { return contract.ExchangeContractId; } set { contract.ExchangeContractId = value; } }

        [FwLogicProperty(Id:"VUdYKlRGNfeZ")]
        public bool? Rental { get { return contract.Rental; } set { contract.Rental = value; } }

        [FwLogicProperty(Id:"ems5FF5k8ez4")]
        public bool? Sales { get { return contract.Sales; } set { contract.Sales = value; } }

        [FwLogicProperty(Id:"j98nLoKdIU2Z", IsReadOnly:true)]
        public bool? Exchange { get; set; }

        [FwLogicProperty(Id:"8UM0lpArkPZJ")]
        public string InputByUserId { get { return contract.InputByUserId; } set { contract.InputByUserId = value; } }

        [FwLogicProperty(Id:"jRemMOly8JHJ", IsReadOnly:true)]
        public string InputByUser { get; set; }

        [FwLogicProperty(Id:"sWr8J7HvRgRZ", IsReadOnly:true)]
        public bool? DealInactive { get; set; }

        [FwLogicProperty(Id:"AaaWyLJXAAwK")]
        public bool? Truck { get { return contract.Truck; } set { contract.Truck = value; } }

        [FwLogicProperty(Id:"8lD6XTb9EbO6")]
        public string BillingDate { get { return contract.BillingDate; } set { contract.BillingDate = value; } }

        [FwLogicProperty(Id:"apwnKePW27b2", IsReadOnly:true)]
        public bool? HasAdjustedBillingDate { get; set; }

        [FwLogicProperty(Id:"fQoqFYvaaesY", IsReadOnly:true)]
        public bool? HasVoId { get; set; }

        [FwLogicProperty(Id:"eb7KgfrVo0Or")]
        public string SessionId { get { return contract.SessionId; } set { contract.SessionId = value; } }

        [FwLogicProperty(Id:"Bz8PoIrzCKb3", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"Bz8PoIrzCKb3", IsReadOnly:true)]
        public string PoOrderDescription { get; set; }

        [FwLogicProperty(Id: "MCBigdZOp7K0U")]
        public string DeliveryId { get { return contract.DeliveryId; } set { contract.DeliveryId = value; delivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "ftMG0J65mG8PY")]
        public string CarrierId { get { return delivery.CarrierId; } set { delivery.CarrierId = value; } }

        [FwLogicProperty(Id: "tXE1ta9zHqbZs", IsReadOnly: true)]
        public string Carrier { get; set; }

        [FwLogicProperty(Id: "ks9a0eF5nSXJb")]
        public string DeliveryFreightTrackingNumber { get { return delivery.FreightTrackingNumber; } set { delivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "4LKJeHPt0MGoc", IsReadOnly: true)]
        public string DeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id:"7lTe6d93tfl6")]
        public string DateStamp { get { return contract.DateStamp; } set { contract.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}

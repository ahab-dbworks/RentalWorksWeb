using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.RepairOrder
{
    public class RepairOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairOrderRecord repairOrder = new RepairOrderRecord();
        RepairOrderLoader repairOrderLoader = new RepairOrderLoader();
        public RepairOrderLogic()
        {
            dataRecords.Add(repairOrder);
            dataLoader = repairOrderLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairOrderId { get { return repairOrder.RepairOrderId; } set { repairOrder.RepairOrderId = value; } }
        public string LocationId { get { return repairOrder.LocationId; } set { repairOrder.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string BillingLocationId { get { return repairOrder.BillingLocationId; } set { repairOrder.BillingLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingLocation { get; set; }
        public string WarehouseId { get { return repairOrder.WarehouseId; } set { repairOrder.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemWarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        public string BillingWarehouseId { get { return repairOrder.BillingWarehouseId; } set { repairOrder.BillingWarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingWarehouse { get; set; }
        public string DepartmentId { get { return repairOrder.DepartmentId; } set { repairOrder.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public string RepairNumber { get { return repairOrder.RepairNumber; } set { repairOrder.RepairNumber = value; } }
        public string RepairDate { get { return repairOrder.RepairDate; } set { repairOrder.RepairDate = value; } }
        public bool? OutsideRepair { get { return repairOrder.OutsideRepair; } set { repairOrder.OutsideRepair = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsideRepairPoNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RfId { get; set; }
        public string InventoryId { get { return repairOrder.InventoryId; } set { repairOrder.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailForDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemDescription { get; set; }
        public int? Quantity { get { return repairOrder.Quantity; } set { repairOrder.Quantity = value; } }
        public string DamageDealId { get { return repairOrder.DamageDealId; } set { repairOrder.DamageDealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageDeal { get; set; }
        public string DamageOrderId { get { return repairOrder.DamageOrderId; } set { repairOrder.DamageOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageOrderDescription { get; set; }
        public string DamageContractId { get { return repairOrder.DamageContractId; } set { repairOrder.DamageContractId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageContractNumber { get; set; }
        public string ChargeOrderId { get { return repairOrder.ChargeOrderId; } set { repairOrder.ChargeOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeOrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeInvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeInvoiceNumber { get; set; }
        public string Status { get { return repairOrder.Status; } set { repairOrder.Status = value; } }
        public string StatusDate { get { return repairOrder.StatusDate; } set { repairOrder.StatusDate = value; } }
        public bool? Billable { get { return repairOrder.Billable; } set { repairOrder.Billable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillableDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NotBilled { get; set; }
        public string Priority { get { return repairOrder.Priority; } set { repairOrder.Priority = value; } }
        public string RepairType { get { return repairOrder.RepairType; } set { repairOrder.RepairType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PoPending { get; set; }
        public string PoNumber { get { return repairOrder.PoNumber; } set { repairOrder.PoNumber = value; } }
        public string Damage { get { return repairOrder.Damage; } set { repairOrder.Damage = value; } }
        public string Correction { get { return repairOrder.Correction; } set { repairOrder.Correction = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Released { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReleasedQuantity { get; set; }
        public string TransferId { get { return repairOrder.TransferId; } set { repairOrder.TransferId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransferredFromWarehouseId { get; set; }
        public string DueDate { get { return repairOrder.DueDate; } set { repairOrder.DueDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompletedBy { get; set; }
        public string InputByUserId { get { return repairOrder.InputByUserId; } set { repairOrder.InputByUserId = value; } }
        public string RepairItemStatusId { get { return repairOrder.RepairItemStatusId; } set { repairOrder.RepairItemStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RepairItemStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Cost { get; set; }
        public string CurrencyId { get { return repairOrder.CurrencyId; } set { repairOrder.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

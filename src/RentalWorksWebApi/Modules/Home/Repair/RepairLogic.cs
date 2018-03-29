using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Home.Repair
{
    public class RepairLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairRecord repair = new RepairRecord();
        RepairLoader repairLoader = new RepairLoader();
        public RepairLogic()
        {
            dataRecords.Add(repair);
            dataLoader = repairLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairId { get { return repair.RepairId; } set { repair.RepairId = value; } }
        public string LocationId { get { return repair.LocationId; } set { repair.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string BillingLocationId { get { return repair.BillingLocationId; } set { repair.BillingLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingLocation { get; set; }
        public string WarehouseId { get { return repair.WarehouseId; } set { repair.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemWarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        public string BillingWarehouseId { get { return repair.BillingWarehouseId; } set { repair.BillingWarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingWarehouse { get; set; }
        public string DepartmentId { get { return repair.DepartmentId; } set { repair.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public bool? PendingRepair { get { return repair.PendingRepair; } set { repair.PendingRepair = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string RepairNumber { get { return repair.RepairNumber; } set { repair.RepairNumber = value; } }
        public string RepairDate { get { return repair.RepairDate; } set { repair.RepairDate = value; } }
        public bool? OutsideRepair { get { return repair.OutsideRepair; } set { repair.OutsideRepair = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsideRepairPoNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RfId { get; set; }
        public string InventoryId { get { return repair.InventoryId; } set { repair.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailFor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AvailForDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemDescription { get; set; }
        public int? Quantity { get { return repair.Quantity; } set { repair.Quantity = value; } }
        public string DamageDealId { get { return repair.DamageDealId; } set { repair.DamageDealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageDeal { get; set; }
        public string DamageOrderId { get { return repair.DamageOrderId; } set { repair.DamageOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageOrderDescription { get; set; }
        public string DamageContractId { get { return repair.DamageContractId; } set { repair.DamageContractId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageContractNumber { get; set; }

        public string DamageScannedById { get { return repair.DamageScannedById; } set { repair.DamageScannedById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DamageScannedBy { get; set; }

        public string FinalLandDOrderId { get { return repair.FinalLandDOrderId; } set { repair.FinalLandDOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FinalLandDOrderNumber { get; set; }

        public string ChargeOrderId { get { return repair.ChargeOrderId; } set { repair.ChargeOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeOrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeInvoiceId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeInvoiceNumber { get; set; }
        public string ChargeInvoiceDescription { get; set; }


        public string TaxOptionId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }


        public string TaxId { get { return repair.TaxId; } set { repair.TaxId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTaxRate2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTaxRate2 { get; set; }

        public string Status { get { return repair.Status; } set { repair.Status = value; } }
        public string StatusDate { get { return repair.StatusDate; } set { repair.StatusDate = value; } }
        public bool? Billable { get { return repair.Billable; } set { repair.Billable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillableDisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NotBilled { get; set; }
        public string Priority { get { return repair.Priority; } set { repair.Priority = value; } }
        public string RepairType { get { return repair.RepairType; } set { repair.RepairType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PoPending { get; set; }
        public string PoNumber { get { return repair.PoNumber; } set { repair.PoNumber = value; } }
        public string Damage { get { return repair.Damage; } set { repair.Damage = value; } }
        public string Correction { get { return repair.Correction; } set { repair.Correction = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Released { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReleasedQuantity { get; set; }
        public string TransferId { get { return repair.TransferId; } set { repair.TransferId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransferredFromWarehouseId { get; set; }
        public string DueDate { get { return repair.DueDate; } set { repair.DueDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompletedBy { get; set; }
        public string InputByUserId { get { return repair.InputByUserId; } set { repair.InputByUserId = value; } }
        public string RepairItemStatusId { get { return repair.RepairItemStatusId; } set { repair.RepairItemStatusId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RepairItemStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Cost { get; set; }
        public string CurrencyId { get { return repair.CurrencyId; } set { repair.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        public string Notes { get { return repair.Notes; } set { repair.Notes = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Inactive { get; set; }
        public string DateStamp { get { return repair.DateStamp; } set { repair.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}

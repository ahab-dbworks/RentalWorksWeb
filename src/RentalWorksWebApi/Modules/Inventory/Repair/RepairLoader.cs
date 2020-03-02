using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Inventory.Repair
{
    [FwSqlTable("repairwebview")]
    public class RepairLoader : RepairBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billinglocationid", modeltype: FwDataTypes.Text)]
        public string BillingLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billinglocation", modeltype: FwDataTypes.Text)]
        public string BillingLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemwarehouseid", modeltype: FwDataTypes.Text)]
        public string ItemWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingwarehouseid", modeltype: FwDataTypes.Text)]
        public string BillingWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingwarehouse", modeltype: FwDataTypes.Text)]
        public string BillingWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingrepair", modeltype: FwDataTypes.Boolean)]
        public bool? PendingRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepair", modeltype: FwDataTypes.Boolean)]
        public bool? OutsideRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepairpono", modeltype: FwDataTypes.Text)]
        public string OutsideRepairPoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedealid", modeltype: FwDataTypes.Text)]
        public string DamageDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderid", modeltype: FwDataTypes.Text)]
        public string DamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderno", modeltype: FwDataTypes.Text)]
        public string DamageOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderdesc", modeltype: FwDataTypes.Text)]
        public string DamageOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedbyid", modeltype: FwDataTypes.Text)]
        public string DamageScannedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedby", modeltype: FwDataTypes.Text)]
        public string DamageScannedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderid", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderno", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderdesc", modeltype: FwDataTypes.Text)]
        public string LossAndDamageOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractid", modeltype: FwDataTypes.Text)]
        public string DamageContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractno", modeltype: FwDataTypes.Text)]
        public string DamageContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractdate", modeltype: FwDataTypes.Date)]
        public string DamageContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderid", modeltype: FwDataTypes.Text)]
        public string ChargeOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderno", modeltype: FwDataTypes.Text)]
        public string ChargeOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderdesc", modeltype: FwDataTypes.Text)]
        public string ChargeOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeinvoiceid", modeltype: FwDataTypes.Text)]
        public string ChargeInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeinvoiceno", modeltype: FwDataTypes.Text)]
        public string ChargeInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeinvoicedesc", modeltype: FwDataTypes.Text)]
        public string ChargeInvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean)]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabletext", modeltype: FwDataTypes.Text)]
        public string BillableDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notbilled", modeltype: FwDataTypes.Boolean)]
        public bool? NotBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Text)]
        public string Priority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtype", modeltype: FwDataTypes.Text)]
        public string RepairType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "popending", modeltype: FwDataTypes.Boolean)]
        public bool? PoPending { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damage", modeltype: FwDataTypes.Text)]
        public string Damage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "correction", modeltype: FwDataTypes.Text)]
        public string Correction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "released", modeltype: FwDataTypes.Boolean)]
        public bool? Released { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferid", modeltype: FwDataTypes.Text)]
        public string TransferId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferredfromwarehouseid", modeltype: FwDataTypes.Text)]
        public string TransferredFromWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatebyid", modeltype: FwDataTypes.Text)]
        public string EstimateByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimateby", modeltype: FwDataTypes.Text)]
        public string EstimateBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedate", modeltype: FwDataTypes.Date)]
        public string EstimateDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completebyid", modeltype: FwDataTypes.Text)]
        public string CompleteByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeby", modeltype: FwDataTypes.Text)]
        public string CompleteBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completedate", modeltype: FwDataTypes.Date)]
        public string CompleteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatusid", modeltype: FwDataTypes.Text)]
        public string RepairItemStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatus", modeltype: FwDataTypes.Text)]
        public string RepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairautocompleteqc", modeltype: FwDataTypes.Boolean)]
        public bool? AutoCompleteQC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcnote", modeltype: FwDataTypes.Text)]
        public string QcNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
